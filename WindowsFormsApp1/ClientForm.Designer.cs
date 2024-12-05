namespace WindowsFormsApp1
{
    partial class ClientForm
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
            this.watching_screen = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.serverIP_input = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.commentBox = new System.Windows.Forms.TextBox();
            this.commenBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.watching_screen)).BeginInit();
            this.SuspendLayout();
            // 
            // watching_screen
            // 
            this.watching_screen.Location = new System.Drawing.Point(29, 28);
            this.watching_screen.Name = "watching_screen";
            this.watching_screen.Size = new System.Drawing.Size(560, 308);
            this.watching_screen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.watching_screen.TabIndex = 0;
            this.watching_screen.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(370, 385);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 395);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter server IP:";
            // 
            // serverIP_input
            // 
            this.serverIP_input.Location = new System.Drawing.Point(115, 392);
            this.serverIP_input.Name = "serverIP_input";
            this.serverIP_input.Size = new System.Drawing.Size(167, 22);
            this.serverIP_input.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(668, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Comment";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(612, 28);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(176, 308);
            this.listBox1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(653, 339);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Comment here";
            // 
            // commentBox
            // 
            this.commentBox.Location = new System.Drawing.Point(612, 369);
            this.commentBox.Name = "commentBox";
            this.commentBox.Size = new System.Drawing.Size(176, 22);
            this.commentBox.TabIndex = 7;
            // 
            // commenBtn
            // 
            this.commenBtn.Location = new System.Drawing.Point(657, 408);
            this.commenBtn.Name = "commenBtn";
            this.commenBtn.Size = new System.Drawing.Size(90, 30);
            this.commenBtn.TabIndex = 8;
            this.commenBtn.Text = "comment!";
            this.commenBtn.UseVisualStyleBackColor = true;
            this.commenBtn.Click += new System.EventHandler(this.commenBtn_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.commenBtn);
            this.Controls.Add(this.commentBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.serverIP_input);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.watching_screen);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            ((System.ComponentModel.ISupportInitialize)(this.watching_screen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox watching_screen;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox serverIP_input;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox commentBox;
        private System.Windows.Forms.Button commenBtn;
    }
}