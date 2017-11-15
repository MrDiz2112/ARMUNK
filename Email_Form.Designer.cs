namespace ARMUNK
{
    partial class Email_Form
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
            this.current_Email = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.new_Email = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.old_Password = new System.Windows.Forms.TextBox();
            this.new_Password = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.change_Email = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Текущий E-Mail:";
            // 
            // current_Email
            // 
            this.current_Email.Location = new System.Drawing.Point(111, 18);
            this.current_Email.Name = "current_Email";
            this.current_Email.ReadOnly = true;
            this.current_Email.Size = new System.Drawing.Size(219, 20);
            this.current_Email.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.change_Email);
            this.groupBox1.Controls.Add(this.new_Password);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.old_Password);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.new_Email);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 149);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Изменить E-Mail";
            // 
            // new_Email
            // 
            this.new_Email.Location = new System.Drawing.Point(99, 28);
            this.new_Email.Name = "new_Email";
            this.new_Email.Size = new System.Drawing.Size(219, 20);
            this.new_Email.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Новый E-Mail:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Старый пароль:";
            // 
            // old_Password
            // 
            this.old_Password.Location = new System.Drawing.Point(99, 54);
            this.old_Password.Name = "old_Password";
            this.old_Password.Size = new System.Drawing.Size(219, 20);
            this.old_Password.TabIndex = 5;
            this.old_Password.UseSystemPasswordChar = true;
            // 
            // new_Password
            // 
            this.new_Password.Location = new System.Drawing.Point(99, 80);
            this.new_Password.Name = "new_Password";
            this.new_Password.Size = new System.Drawing.Size(219, 20);
            this.new_Password.TabIndex = 7;
            this.new_Password.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Новый пароль:";
            // 
            // change_Email
            // 
            this.change_Email.Location = new System.Drawing.Point(116, 115);
            this.change_Email.Name = "change_Email";
            this.change_Email.Size = new System.Drawing.Size(75, 23);
            this.change_Email.TabIndex = 8;
            this.change_Email.Text = "Изменить";
            this.change_Email.UseVisualStyleBackColor = true;
            this.change_Email.Click += new System.EventHandler(this.change_Email_Click);
            // 
            // Email_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 213);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.current_Email);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(367, 252);
            this.MinimumSize = new System.Drawing.Size(367, 252);
            this.Name = "Email_Form";
            this.Text = "Информация об E-Mail";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox current_Email;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button change_Email;
        private System.Windows.Forms.TextBox new_Password;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox old_Password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox new_Email;
        private System.Windows.Forms.Label label2;
    }
}