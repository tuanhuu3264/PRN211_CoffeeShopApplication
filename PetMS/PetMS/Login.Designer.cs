
namespace PetMS
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            panel1 = new System.Windows.Forms.Panel();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            txt_Phonenumber = new System.Windows.Forms.TextBox();
            txt_Password = new System.Windows.Forms.TextBox();
            LoginBtn = new System.Windows.Forms.Button();
            ExitBtn = new System.Windows.Forms.Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Crimson;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label1);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(358, 126);
            panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new System.Drawing.Point(128, 60);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(89, 53);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.ForeColor = System.Drawing.Color.MidnightBlue;
            label1.Location = new System.Drawing.Point(117, 18);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(120, 29);
            label1.TabIndex = 2;
            label1.Text = "Pet Shop";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label2.ForeColor = System.Drawing.Color.MidnightBlue;
            label2.Location = new System.Drawing.Point(81, 174);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(183, 29);
            label2.TabIndex = 3;
            label2.Text = "Phone number";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label3.ForeColor = System.Drawing.Color.MidnightBlue;
            label3.Location = new System.Drawing.Point(111, 273);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(128, 29);
            label3.TabIndex = 4;
            label3.Text = "Password";
            // 
            // txt_Phonenumber
            // 
            txt_Phonenumber.Location = new System.Drawing.Point(59, 206);
            txt_Phonenumber.Name = "txt_Phonenumber";
            txt_Phonenumber.Size = new System.Drawing.Size(232, 30);
            txt_Phonenumber.TabIndex = 5;
            // 
            // txt_Password
            // 
            txt_Password.Location = new System.Drawing.Point(59, 303);
            txt_Password.Name = "txt_Password";
            txt_Password.Size = new System.Drawing.Size(232, 30);
            txt_Password.TabIndex = 6;
            txt_Password.UseSystemPasswordChar = true;
            // 
            // LoginBtn
            // 
            LoginBtn.BackColor = System.Drawing.Color.Crimson;
            LoginBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            LoginBtn.ForeColor = System.Drawing.Color.MidnightBlue;
            LoginBtn.Location = new System.Drawing.Point(81, 357);
            LoginBtn.Name = "LoginBtn";
            LoginBtn.Size = new System.Drawing.Size(190, 44);
            LoginBtn.TabIndex = 7;
            LoginBtn.Text = "LOGIN";
            LoginBtn.UseVisualStyleBackColor = false;
            LoginBtn.Click += LoginBtn_Click;
            // 
            // ExitBtn
            // 
            ExitBtn.BackColor = System.Drawing.Color.Crimson;
            ExitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            ExitBtn.ForeColor = System.Drawing.Color.MidnightBlue;
            ExitBtn.Location = new System.Drawing.Point(81, 407);
            ExitBtn.Name = "ExitBtn";
            ExitBtn.Size = new System.Drawing.Size(190, 44);
            ExitBtn.TabIndex = 8;
            ExitBtn.Text = "Exit";
            ExitBtn.UseVisualStyleBackColor = false;
            ExitBtn.Click += ExitBtn_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(358, 479);
            Controls.Add(ExitBtn);
            Controls.Add(LoginBtn);
            Controls.Add(txt_Password);
            Controls.Add(txt_Phonenumber);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(panel1);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "Login";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Login";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox EmpNameTb;
        private System.Windows.Forms.TextBox EmpPassTb;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.TextBox txt_Phonenumber;
        private System.Windows.Forms.TextBox txt_Password;
    }
}