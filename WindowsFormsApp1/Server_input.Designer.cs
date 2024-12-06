namespace WindowsFormsApp1
{
    partial class Server_input
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.name_input = new System.Windows.Forms.TextBox();
            this.title_input = new System.Windows.Forms.TextBox();
            this.server_form_request = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "STREAMHUB";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(234, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(344, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "STREAM INFORMATION";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(236, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Stream name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(236, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Stream title:";
            // 
            // name_input
            // 
            this.name_input.Location = new System.Drawing.Point(366, 179);
            this.name_input.Name = "name_input";
            this.name_input.Size = new System.Drawing.Size(212, 22);
            this.name_input.TabIndex = 4;
            // 
            // title_input
            // 
            this.title_input.Location = new System.Drawing.Point(366, 250);
            this.title_input.Name = "title_input";
            this.title_input.Size = new System.Drawing.Size(212, 22);
            this.title_input.TabIndex = 5;
            // 
            // server_form_request
            // 
            this.server_form_request.Location = new System.Drawing.Point(366, 380);
            this.server_form_request.Name = "server_form_request";
            this.server_form_request.Size = new System.Drawing.Size(96, 35);
            this.server_form_request.TabIndex = 6;
            this.server_form_request.Text = "Let\'s go!";
            this.server_form_request.UseVisualStyleBackColor = true;
            this.server_form_request.Click += new System.EventHandler(this.server_form_request_Click);
            // 
            // Server_input
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.server_form_request);
            this.Controls.Add(this.title_input);
            this.Controls.Add(this.name_input);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Server_input";
            this.Text = "Server_input";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox name_input;
        private System.Windows.Forms.TextBox title_input;
        private System.Windows.Forms.Button server_form_request;
    }
}