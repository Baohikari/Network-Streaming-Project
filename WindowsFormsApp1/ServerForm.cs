using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace WindowsFormsApp1
{
    public partial class ServerForm : Form
    {
        private FilterInfoCollection videoDevices; // Danh sách thiết bị video (camera)
        private VideoCaptureDevice videoSource;   // Camera được chọn
        private TcpListener server;
        private Thread serverThread;
        private Thread sendThread;
        private Bitmap currentFrame; // Frame hiện tại để gửi đi
        private object frameLock = new object(); // Đảm bảo an toàn truy cập đa luồng
        private bool isScreenSharing = false;
        public ServerForm()
        {
            InitializeComponent();
            lbl_name.Text = Singleton.Instance.serverName;
            lbl_title.Text = Singleton.Instance.serverTitle;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string serverIP = GetLocalIPAddress();
                MessageBox.Show($"Server IP: {serverIP}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Khởi động server socket
                serverThread = new Thread(StartServer);
                serverThread.IsBackground = true;
                serverThread.Start();

                // Lấy danh sách các thiết bị camera
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thiết bị camera!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Chọn camera đầu tiên
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
                
                videoSource.Start(); // Bắt đầu chia sẻ camera
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                // Clone frame để hiển thị và gửi
                lock (frameLock)
                {
                    currentFrame?.Dispose();
                    currentFrame = (Bitmap)eventArgs.Frame.Clone();
                }

                // Hiển thị trên PictureBox
                streaming_screen.Invoke((MethodInvoker)(() =>
                {
                    streaming_screen.Image?.Dispose();
                    streaming_screen.Image = (Bitmap)currentFrame.Clone();
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing frame: {ex.Message}");
            }
        }

        private void StartServer()
        {
            try
            {
                server = new TcpListener(System.Net.IPAddress.Any, 8888);
                server.Start();
                Console.WriteLine("Server started...");

                // Luồng chờ kết nối và xử lý client
                Thread acceptThread = new Thread(AcceptClients);
                acceptThread.IsBackground = true;
                acceptThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting server: {ex.Message}");
            }
        }

        private void SendFrames()
        {
            try
            {
                while (true)
                {
                    TcpClient client = null;

                    // Kiểm tra và chấp nhận kết nối mới
                    if (server.Pending())
                    {
                        client = server.AcceptTcpClient();
                        Console.WriteLine("Client connected.");

                        // Tạo một luồng riêng để xử lý bình luận
                        Thread commentThread = new Thread(() => ReceiveComments(client));
                        commentThread.IsBackground = true;
                        commentThread.Start();
                    }

                    if (client != null)
                    {
                        SendFrameToClient(client);
                    }

                    Thread.Sleep(30); // Điều chỉnh FPS
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendFrames: {ex.Message}");
            }
        }

        private void SendFrameToClient(TcpClient client)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    while (client.Connected)
                    {
                        // Lấy frame hiện tại để gửi
                        Bitmap frameToSend;
                        lock (frameLock)
                        {
                            if (currentFrame == null) continue;
                            frameToSend = (Bitmap)currentFrame.Clone();
                        }

                        // Gửi frame qua socket
                        using (MemoryStream ms = new MemoryStream())
                        {
                            frameToSend.Save(ms, ImageFormat.Jpeg);
                            byte[] imageBytes = ms.ToArray();

                            byte[] lengthBytes = BitConverter.GetBytes(imageBytes.Length);
                            stream.Write(lengthBytes, 0, lengthBytes.Length);
                            stream.Write(imageBytes, 0, imageBytes.Length);
                        }

                        frameToSend.Dispose();
                        Thread.Sleep(30); // Giữ FPS
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending frame: {ex.Message}");
            }
        }


        private void StartScreenSharing()
        {
            // Chế độ chia sẻ màn hình, không cần camera
            isScreenSharing = true;

            // Thực hiện chụp màn hình trong vòng lặp gửi frame
            sendThread = new Thread(SendScreenFrames);
            sendThread.IsBackground = true;
            sendThread.Start();
        }

        private void SendScreenFrames()
        {
            while (true)
            {
                try
                {
                    // Kiểm tra kết nối mới
                    if (server.Pending())
                    {
                        TcpClient client = server.AcceptTcpClient();
                        Console.WriteLine("Client connected.");

                        // Tạo luồng để nhận bình luận từ client
                        Thread commentThread = new Thread(() => ReceiveComments(client));
                        commentThread.IsBackground = true;
                        commentThread.Start();
                    }

                    using (TcpClient client = server.AcceptTcpClient())
                    using (NetworkStream stream = client.GetStream())
                    {
                        while (client.Connected)
                        {
                            // Chụp màn hình
                            Bitmap screenFrame = CaptureScreen();
                            using (MemoryStream ms = new MemoryStream())
                            {
                                screenFrame.Save(ms, ImageFormat.Jpeg);
                                byte[] imageBytes = ms.ToArray();

                                // Gửi kích thước và dữ liệu ảnh
                                byte[] lengthBytes = BitConverter.GetBytes(imageBytes.Length);
                                stream.Write(lengthBytes, 0, lengthBytes.Length);
                                stream.Write(imageBytes, 0, imageBytes.Length);
                            }

                            screenFrame.Dispose();

                            // Chờ một chút trước khi gửi frame tiếp theo
                            Thread.Sleep(30); // 30ms tương đương khoảng 33 FPS
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending screen frame: {ex.Message}");
                }
            }
        }

        private Bitmap CaptureScreen()
        {
            // Lấy kích thước màn hình
            Rectangle bounds = Screen.PrimaryScreen.Bounds;

            // Tạo Bitmap từ toàn bộ màn hình
            Bitmap screenBitmap = new Bitmap(bounds.Width, bounds.Height);

            using (Graphics g = Graphics.FromImage(screenBitmap))
            {
                g.CopyFromScreen(0, 0, 0, 0, bounds.Size);
            }

            return screenBitmap;
        }



        private void ReceiveComments(TcpClient client)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    while (client.Connected)
                    {
                        // Nhận kích thước bình luận
                        byte[] lengthBytes = new byte[4];
                        int lengthRead = stream.Read(lengthBytes, 0, lengthBytes.Length);
                        if (lengthRead < 4) break; // Đảm bảo đọc đủ 4 byte

                        int length = BitConverter.ToInt32(lengthBytes, 0);

                        // Nhận nội dung bình luận
                        byte[] commentBytes = new byte[length];
                        int bytesRead = 0;
                        while (bytesRead < length)
                        {
                            int read = stream.Read(commentBytes, bytesRead, length - bytesRead);
                            if (read <= 0) throw new IOException("Client disconnected during comment transfer.");
                            bytesRead += read;
                        }

                        // Hiển thị bình luận trên ListBox
                        string comment = System.Text.Encoding.UTF8.GetString(commentBytes);
                        commentBox.Invoke((MethodInvoker)(() =>
                        {
                            commentBox.Items.Add("Client: " + comment);
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ReceiveComments: {ex.Message}");
            }
        }


        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Dừng camera và socket
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }

            if (server != null)
            {
                server.Stop();
            }

            if (sendThread != null && sendThread.IsAlive)
            {
                sendThread.Abort();
            }

            if (serverThread != null && serverThread.IsAlive)
            {
                serverThread.Abort();
            }
        }

        private void AcceptClients()
        {
            while (true)
            {
                try
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Client connected.");

                    // Tạo luồng để xử lý client này
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error accepting client: {ex.Message}");
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            Thread receiveThread = new Thread(() => ReceiveComments(client));
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }
        public string GetLocalIPAddress()
        {
            foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                    networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    var ipProperties = networkInterface.GetIPProperties();
                    foreach (var ip in ipProperties.UnicastAddresses)
                    {
                        // Chỉ lấy địa chỉ IPv4
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
            }
            return "127.0.0.1"; // Trả về localhost nếu không tìm thấy
        }

        private void startShareScreen_Click(object sender, EventArgs e)
        {
            StartScreenSharing();
        }
    }
}
