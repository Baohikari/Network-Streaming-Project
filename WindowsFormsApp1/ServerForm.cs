using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
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

        public ServerForm()
        {
            InitializeComponent();
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
                videoSource.Start();
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

                // Luồng để gửi frame
                sendThread = new Thread(SendFrames);
                sendThread.IsBackground = true;
                sendThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting server: {ex.Message}");
            }
        }

        private void SendFrames()
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
                            // Lấy frame hiện tại để gửi
                            Bitmap frameToSend;
                            lock (frameLock)
                            {
                                if (currentFrame == null) continue;
                                frameToSend = (Bitmap)currentFrame.Clone();
                            }

                            using (MemoryStream ms = new MemoryStream())
                            {
                                frameToSend.Save(ms, ImageFormat.Jpeg);
                                byte[] imageBytes = ms.ToArray();

                                // Gửi kích thước và dữ liệu ảnh
                                byte[] lengthBytes = BitConverter.GetBytes(imageBytes.Length);
                                stream.Write(lengthBytes, 0, lengthBytes.Length);
                                stream.Write(imageBytes, 0, imageBytes.Length);
                            }

                            frameToSend.Dispose();

                            // Chờ một chút trước khi gửi frame tiếp theo
                            Thread.Sleep(30); // 30ms tương đương khoảng 33 FPS
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending frame: {ex.Message}");
                }
            }
        }


        private void ReceiveComments(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();

                while (client.Connected)
                {
                    // Nhận kích thước bình luận
                    byte[] lengthBytes = new byte[4];
                    int lengthRead = stream.Read(lengthBytes, 0, lengthBytes.Length);
                    if (lengthRead == 0) break;

                    int length = BitConverter.ToInt32(lengthBytes, 0);

                    // Nhận nội dung bình luận
                    byte[] commentBytes = new byte[length];
                    int bytesRead = 0;
                    while (bytesRead < length)
                    {
                        int read = stream.Read(commentBytes, bytesRead, length - bytesRead);
                        if (read == 0) throw new IOException("Mất kết nối với client.");
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
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi nhận bình luận: {ex.Message}");
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

        public string GetLocalIPAddress()
        {
            foreach (var ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                // Kiểm tra xem IP có phải là IPv4 và không phải là địa chỉ loopback (127.0.0.1)
                if (ip.AddressFamily == AddressFamily.InterNetwork && !ip.ToString().StartsWith("127"))
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1"; // Nếu không tìm thấy IP hợp lệ, trả về localhost
        }
    }
}
