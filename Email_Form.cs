using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARMUNK
{
    public partial class Email_Form : Form
    {
        Form1 mainForm;

        public Email_Form()
        {
            InitializeComponent();
        }

        public Email_Form(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            current_Email.Text = mainForm.Get_Email();
        }

        private void change_Email_Click(object sender, EventArgs e)
        {
            if (new_Email.Text != "" &&
                old_Password.Text != "" &&
                new_Password.Text != "")
            {
                if (old_Password.Text == mainForm.Get_Email_Pass())
                {
                    mainForm.Change_Email(new_Email.Text, new_Password.Text);
                    current_Email.Text = mainForm.Get_Email();
                    new_Email.Text = "";
                    old_Password.Text = "";
                    new_Password.Text = "";

                    MessageBox.Show("Пароль успешно изменен!", "Смена пароля", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Неверно указан старый пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
