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
            this.lbl_title = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
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
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.Location = new System.Drawing.Point(18, 341);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(35, 18);
            this.lbl_title.TabIndex = 3;
            this.lbl_title.Text = "Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 409);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Your name is:";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name.Location = new System.Drawing.Point(102, 409);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(67, 16);
            this.lbl_name.TabIndex = 5;
            this.lbl_name.Text = "Bảo Bảo";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.commentBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.streaming_screen);
            this.Name = "ServerForm";
            this.Text = "ServerForm";
            ((System.ComponentModel.ISupportInitialize)(this.streaming_screen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox streaming_screen;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox commentBox;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_name;
    }
}