namespace WindowsFormsApp1
{
    partial class ServerForm
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
            this.streaming_screen = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.commentBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.streaming_screen)).BeginInit();
            this.SuspendLayout();
            // 
            // streaming_screen
            // 
            this.streaming_screen.Location = new System.Drawing.Point(21, 21);
            this.streaming_screen.Name = "streaming_screen";
            this.streaming_screen.Size = new System.Drawing.Size(582, 307);
            this.streaming_screen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.streaming_screen.TabIndex = 0;
            this.streaming_screen.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(346, 379);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 46);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // commentBox
            // 
            this.commentBox.FormattingEnabled = true;
            this.commentBox.ItemHeight = 16;
            this.commentBox.Location = new System.Drawing.Point(619, 21);
            this.commentBox.Name = "commentBox";
            this.commentBox.Size = new System.Drawing.Size(169, 308);
            this.commentBox.TabIndex = 2;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.commentBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.streaming_screen);
            this.Name = "ServerForm";
            this.Text = "ServerForm";
            ((System.ComponentModel.ISupportInitialize)(this.streaming_screen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox streaming_screen;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox commentBox;
    }
}