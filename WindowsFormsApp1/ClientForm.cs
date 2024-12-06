using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ClientForm : Form
    {
        private TcpClient client;
        private Thread receiveThread;
        private bool isReceiving = false;

        public ClientForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string serverIP = serverIP_input.Text;
                // Kết nối tới server
                client = new TcpClient(serverIP, 8888);
                isReceiving = true;

                receiveThread = new Thread(ReceiveFrames);
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối tới server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReceiveFrames()
        {
            try
            {
                NetworkStream stream = client.GetStream();

                while (isReceiving)
                {
                    try
                    {
                        // Nhận kích thước dữ liệu
                        byte[] lengthBytes = new byte[4];
                        int lengthRead = stream.Read(lengthBytes, 0, lengthBytes.Length);
                        if (lengthRead == 0) break; // Ngắt kết nối nếu không nhận được dữ liệu

                        int length = BitConverter.ToInt32(lengthBytes, 0);

                        // Nhận nội dung ảnh
                        byte[] imageBytes = new byte[length];
                        int bytesRead = 0;
                        while (bytesRead < length)
                        {
                            int read = stream.Read(imageBytes, bytesRead, length - bytesRead);
                            if (read == 0) throw new IOException("Mất kết nối với server.");
                            bytesRead += read;
                        }

                        // Hiển thị hình ảnh trên PictureBox
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            Image image = Image.FromStream(ms);
                            watching_screen.Invoke((MethodInvoker)(() =>
                            {
                                watching_screen.Image?.Dispose();
                                watching_screen.Image = (Bitmap)image.Clone();
                            }));
                        }
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"Mất kết nối: {ex.Message}");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi nhận frame: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Disconnect();
            }
        }

        private void Disconnect()
        {
            isReceiving = false;

            if (client != null)
            {
                client.Close();
                client = null;
            }

            if (receiveThread != null && receiveThread.IsAlive)
            {
                receiveThread.Join();
            }
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void commenBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (client != null && client.Connected)
                {
                    string comment = commentBox.Text.Trim();
                    if (!string.IsNullOrEmpty(comment))
                    {
                        NetworkStream stream = client.GetStream();
                        byte[] commentBytes = System.Text.Encoding.UTF8.GetBytes(comment);
                        byte[] lengthBytes = BitConverter.GetBytes(commentBytes.Length);

                        // Gửi kích thước và nội dung bình luận
                        stream.Write(lengthBytes, 0, lengthBytes.Length);
                        stream.Write(commentBytes, 0, commentBytes.Length);

                        // Hiển thị bình luận ở phía client
                        listBox1.Items.Add("Me: " + comment);
                        commentBox.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gửi bình luận: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
