using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Server_input : Form
    {
        public Server_input()
        {
            InitializeComponent();
        }

        private void server_form_request_Click(object sender, EventArgs e)
        {
            string streamName = name_input.Text.Trim();
            string streamTitle = title_input.Text.Trim();
            if (string.IsNullOrEmpty(streamName) || string.IsNullOrEmpty(streamTitle))
            {
                MessageBox.Show("Please enter all fields", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var serverSingleton = Singleton.Instance;
            serverSingleton.serverName = streamName;
            serverSingleton.serverTitle = streamTitle;

            MessageBox.Show($"Server Name: {streamName}\n" +
                            $"Server Title: {streamTitle}",
                            "Server Information");

            ServerForm svForm = new ServerForm();
            svForm.Show();
            this.Hide();
        }
    }
}
