using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Net;
using System.Net.Mail;

namespace ARMUNK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        // Обновить по поиску
        public void Update_Search(string table, string column)
        {
            dataGridView1.DataSource = SG(table, column, textBox2.Text);
        }

        // Поиск по дате
        public void Search_Marks_With_Date()
        {
            firstDateTimePicker.Format = DateTimePickerFormat.Custom;
            firstDateTimePicker.CustomFormat = "yyyy-MM-dd";
            string date1 = firstDateTimePicker.Text;
            firstDateTimePicker.Format = DateTimePickerFormat.Long;

            secondDateTimePicker.Format = DateTimePickerFormat.Custom;
            secondDateTimePicker.CustomFormat = "yyyy-MM-dd";
            string date2 = secondDateTimePicker.Text;
            secondDateTimePicker.Format = DateTimePickerFormat.Long;

            DataTable table = new DataTable();
            try
            {
                string SQLtext = "SELECT * FROM students_marks WHERE [Дата выставления] >= '" + date1 +
                    "' AND [Дата выставления] <= '" + date2 + "'" +
                    " AND  [ФИО] like @mask + '%'";
                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    SqlCommand SQLListAll = new SqlCommand(SQLtext, conn);
                    SQLListAll.Parameters.AddWithValue("@mask", textBox2.Text);
                    conn.Open();
                    table.Load(SQLListAll.ExecuteReader());
                    conn.Close();
                    dataGridView1.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = new DataTable();
            }
        }

        // Поиск по четверти
        public void Search_Marks_With_Fourth(string fourth)
        {
            DataTable table = new DataTable();
            try
            {
                string SQLtext = "SELECT * FROM students_marks WHERE [Четверть] = '" + fourth +
                    "' AND [ФИО] like @mask + '%'";
                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    SqlCommand SQLListAll = new SqlCommand(SQLtext, conn);
                    SQLListAll.Parameters.AddWithValue("@mask", textBox2.Text);
                    conn.Open();
                    table.Load(SQLListAll.ExecuteReader());
                    conn.Close();
                    dataGridView1.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = new DataTable();
            }
        }

        // Обновление DataGridView и списка учеников
        public void Update_Info()
        {
            dataGridView1.DataSource = SG("students_marks", "ФИО", "");
            dataGridView2.DataSource = SG("students_parents", "ФИО", "");
            deleteGridView.DataSource = SG("Students", "FIO_students", "");

            comboBox1.Items.Clear();

            try
            {
                string SQLtext = "select FIO_students from Students";
                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    conn.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        comboBox1.Items.Add(myReader["FIO_students"].ToString());
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Событие для RadioButton'ов
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            // приводим отправителя к элементу типа RadioButton
            RadioButton radioButton = (RadioButton)sender;
            
            // Все оценки
            if (radioButton == allMarksRadio)
            {
                Update_Search("students_marks", "ФИО");
                firstDateTimePicker.Enabled = false;
                secondDateTimePicker.Enabled = false;
                checkBox1.Enabled = true;
            }

            // Оценки по дате
            if (radioButton == dateMarksRadio)
            {
                Search_Marks_With_Date();
                firstDateTimePicker.Enabled = true;
                secondDateTimePicker.Enabled = true;
                checkBox1.Enabled = false;
            }

            // Оценки по четверти
            if (radioButton == firstFourthRadio)
            {
                Search_Marks_With_Fourth("1");
                firstDateTimePicker.Enabled = false;
                secondDateTimePicker.Enabled = false;
                checkBox1.Enabled = false;
            }
            if (radioButton == secondFourthRadio)
            {
                Search_Marks_With_Fourth("2");
                firstDateTimePicker.Enabled = false;
                secondDateTimePicker.Enabled = false;
                checkBox1.Enabled = false;
            }
            if (radioButton == thirdFourthRadio)
            {
                Search_Marks_With_Fourth("3");
                firstDateTimePicker.Enabled = false;
                secondDateTimePicker.Enabled = false;
                checkBox1.Enabled = false;
            }
            if (radioButton == fourthFourthRadio)
            {
                Search_Marks_With_Fourth("4");
                firstDateTimePicker.Enabled = false;
                secondDateTimePicker.Enabled = false;
                checkBox1.Enabled = false;
            }
        }

        // Обовить при смене даты
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker radioButton = (DateTimePicker)sender;

            if (dateMarksRadio.Checked)
            {
                Search_Marks_With_Date();
            }
            
        }

        // Список тем собраний
        public void Get_Meeting_Themes()
        {
            meetingComboBox.Items.Clear();
            try
            {
                string SQLtext = "select name_meeting from Meeting";
                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    conn.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        meetingComboBox.Items.Add(myReader["name_meeting"].ToString());
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Получить текущий Email
        public string Get_Email()
        {
            string email = "";

            try
            {
                string SQLtext = "SELECT email FROM teacher_email";
                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    conn.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        email = myReader["email"].ToString();
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return email;
        }

        // Получить текущий пароль к Email
        public string Get_Email_Pass()
        {
            string email_pass = "";

            try
            {
                string SQLtext = "SELECT email_pass FROM teacher_email";
                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    conn.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        email_pass = myReader["email_pass"].ToString();
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return email_pass;
        }

        // Изменить Email и пароль
        public void Change_Email(string email, string pass)
        {
            try
            {
                string SQLtext = "DELETE FROM teacher_email WHERE id_email>0";

                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand(SQLtext, conn);

                    myCommand.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                string SQLtext = "INSERT INTO teacher_email VALUES" +
                    "(" + 1 + "," +
                    "'" + email + "'," +
                    "'" + pass + "')";

                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand(SQLtext, conn);

                    myCommand.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Действия при создании формы
        private void Form1_Load(object sender, EventArgs e)
        {
            Update_Info();

            firstDateTimePicker.Enabled = false;
            secondDateTimePicker.Enabled = false;

            deleteGridView.Columns[0].HeaderText = "ID";
            deleteGridView.Columns[1].HeaderText = "ФИО";
            deleteGridView.Columns[2].Visible = false;

            isComeGridView.DataSource = SG("Students", "id_student", "", "FIO_students");

            var column = new DataGridViewComboBoxColumn();
            column.HeaderText = "Явка родителей   ";
            column.Name = "isCome";
            column.Items.AddRange("Присутвовал", "Отсутствовал");

            isComeGridView.Columns.Add(column);

            isComeGridView.Columns[0].HeaderText = "ФИО ученика";
            isComeGridView.Columns[0].ReadOnly = true;

            try
            {
                string SQLtext = "select name_subjects from Subjects";
                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    conn.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        comboBox2.Items.Add(myReader["name_subjects"].ToString());
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Get_Meeting_Themes();

            if (meetingComboBox.Items.Count != 0)
            {
                meetingComboBox.SelectedIndex = 0;
            }

            meetingGridView.DataSource = SG("meeting_students", "[Тема собрания]",
                meetingComboBox.Text);

            meetingGridView.Columns[0].Visible = false;

            Update_Email_ComboBox();
        }

        // Функция для заполнения DataGridView
        public DataTable SG(string view, string column, string mask, string selection="*")
        {
            DataTable table = new DataTable();
            try
            {
                string SQLtext = "select " + selection + " from " + view + " where " + column + " like @mask + '%'";
                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    SqlCommand SQLListAll = new SqlCommand(SQLtext, conn);
                    SQLListAll.Parameters.AddWithValue("@mask",  mask);
                    conn.Open();
                    table.Load(SQLListAll.ExecuteReader());
                    conn.Close();
                    return table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }

        // Функция для соединения с SQL
        public SqlConnectionStringBuilder DBConnect()
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = "DESKTOP-CAEN3UQ\\SQLEXPRESS",
                InitialCatalog = "ARMUNK",
                ConnectTimeout = 120,
                IntegratedSecurity = true
            };
        }

        //Фильтр по поиску
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Update_Search("avg_marks", "FIO_students");
            }
            else
            {
                if (allMarksRadio.Checked)
                    Update_Search("students_marks", "ФИО");

                if (dateMarksRadio.Checked)
                    Search_Marks_With_Date();

                if (firstFourthRadio.Checked)
                    Search_Marks_With_Fourth("1");
                if (secondFourthRadio.Checked)
                    Search_Marks_With_Fourth("2");
                if (thirdFourthRadio.Checked)
                    Search_Marks_With_Fourth("3");
                if (fourthFourthRadio.Checked)
                    Search_Marks_With_Fourth("4");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = SG("students_parents", "ФИО", textBox3.Text);
        }

        // Добавление оценки !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 &&
                comboBox2.SelectedIndex != -1 &&
                comboBox3.SelectedIndex != -1 &&
                comboBox4.SelectedIndex != -1 &&
                comboBox5.SelectedIndex != -1)
            {
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "yyyy-MM-dd";

                string date = dateTimePicker1.Text;

                dateTimePicker1.Format = DateTimePickerFormat.Long;

                // Получить ID студента

                string id_student = "";

                string SQLtext = "SELECT * FROM Students WHERE [FIO_students] = '" + comboBox1.Text + "'";
                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    conn.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        id_student = myReader["id_student"].ToString();
                    }

                    conn.Close();
                }

                // Добавление строки в Marks
                try
                {
                    SQLtext = "INSERT INTO Marks VALUES" + 
                        "(" + (comboBox2.SelectedIndex + 1) + "," +
                        id_student + "," +
                        comboBox5.Text + "," + "'" + comboBox3.Text + "'" + "," +
                       "'" + date + "'" + "," + comboBox4.Text + ")";
                    
                    using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                    {
                        conn.Open();
                        SqlCommand myCommand = new SqlCommand(SQLtext, conn);

                        myCommand.ExecuteNonQuery();

                        conn.Close();
                    }

                    dataGridView1.DataSource = SG("students_marks", "ФИО", "");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все пункты!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Вывод среднего балла
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dataGridView1.DataSource = SG("avg_marks", "FIO_students", "");
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                dataGridView1.Columns[0].HeaderText = "ФИО";
                dataGridView1.Columns[1].HeaderText = "Предмет";

                firstDateTimePicker.Enabled = false;
                secondDateTimePicker.Enabled = false;
                dateMarksRadio.Enabled = false;
                firstFourthRadio.Enabled = false;
                secondFourthRadio.Enabled = false;
                thirdFourthRadio.Enabled = false;
                fourthFourthRadio.Enabled = false;
            }
            else
            {
                dataGridView1.DataSource = SG("students_marks", "ФИО", "");
                firstDateTimePicker.Enabled = false;
                secondDateTimePicker.Enabled = false;
                dateMarksRadio.Enabled = true;
                firstFourthRadio.Enabled = true;
                secondFourthRadio.Enabled = true;
                thirdFourthRadio.Enabled = true;
                fourthFourthRadio.Enabled = true;
            }
        }

        // Добавление ученика
        private void button2_Click(object sender, EventArgs e)
        {
            if (fio_append.Text != "" &&
                tel_append.Text != "" &&
                father_append.Text != "" &&
                tel_father_append.Text != "" &&
                mother_append.Text != "" &&
                adress_append.Text != "" &&
                email_father_append.Text != "" &&
                email_mother_append.Text != "")
            {
                
                try
                {
                    string SQLtext_Student = "INSERT INTO Students VALUES" +
                        "(" + "'" + fio_append.Text + "'" + "," +
                        "'" + tel_append.Text + "'" + ")";

                    using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                    {
                        conn.Open();
                        SqlCommand myCommand = new SqlCommand(SQLtext_Student, conn);

                        myCommand.ExecuteNonQuery();

                        conn.Close();
                    }

                    int student_id = -1;

                    try
                    {
                        string SQLtext = "SELECT * FROM Students WHERE id_student=(SELECT MAX(id_student) FROM Students)";
                        using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                        {
                            conn.Open();
                            SqlDataReader myReader = null;
                            SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                            myReader = myCommand.ExecuteReader();

                            while (myReader.Read())
                            {
                                 student_id = Convert.ToInt32(myReader["id_student"].ToString());
                            }

                            conn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    string SQLtext_Parents = "INSERT INTO Parents VALUES" +
                        "(" + student_id + "," +
                        "'" + mother_append.Text + "'" + "," +
                        "'" + father_append.Text + "'" + "," +
                        "'" + tel_mother_append.Text + "'" + "," +
                        "'" + tel_father_append.Text + "'" + "," +
                        "'" + email_father_append.Text + "'" + "," +
                        "'" + email_mother_append.Text + "'" + "," +
                        "'" + adress_append.Text + "'" + ")";

                    using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                    {
                        conn.Open();
                        SqlCommand myCommand = new SqlCommand(SQLtext_Parents, conn);

                        myCommand.ExecuteNonQuery();

                        conn.Close();
                    }

                    Update_Info();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все пункты!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Удаление ученика
        private void deleteButton_Click(object sender, EventArgs e)
        {
            string id_student = deleteGridView.SelectedRows[0].Cells[0].Value.ToString();

            try
            {
                string SQLtext = "DELETE FROM Marks WHERE id_student2=" + id_student + " " +
                    "DELETE FROM Parents WHERE id_student1=" + id_student + " " +
                    "DELETE FROM Students WHERE id_student=" + id_student;

                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand(SQLtext, conn);

                    myCommand.ExecuteNonQuery();

                    conn.Close();
                }

                Update_Info();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
        }
        
        // Печать успеваемости
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }
        
        private void printButton_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
            printDocument1.Print();
        }

        // Получить динамический список из ID студентов
        public void Get_Students_IDs(ref List<string> arr_ids)
        {
            arr_ids.Clear();

            try
            {
                string SQLtext = "select id_student from Students";
                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    conn.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        arr_ids.Add(myReader["id_student"].ToString());
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Создать собрание
        private void newMeetingButton_Click(object sender, EventArgs e)
        {
            bool isAllChecked = true;

            for (int i = 0; i < isComeGridView.Rows.Count; i++)
            {
                if (isComeGridView.Rows[i].Cells["isCome"].Value != null)
                {
                    continue;
                }
                else
                {
                    isAllChecked = false;
                    break;
                }
            }

            if ((meetingTextbox.Text != "") && (isAllChecked == true))
            {
                meetingTimePicker.Format = DateTimePickerFormat.Custom;
                meetingTimePicker.CustomFormat = "yyyy-MM-dd";

                string date = meetingTimePicker.Text;

                meetingTimePicker.Format = DateTimePickerFormat.Long;

                #region Вставка в Meeting

                try
                {
                    string SQLtext = "INSERT INTO Meeting VALUES" +
                        "(" + "'" + meetingTextbox.Text + "'" + "," +
                       "'" + date + "'" + ")";

                    using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                    {
                        conn.Open();
                        SqlCommand myCommand = new SqlCommand(SQLtext, conn);

                        myCommand.ExecuteNonQuery();

                        conn.Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                #endregion

                #region Вставка в Meeting_isCome

                // Получаем последний id
                int meeting_id = -1;

                try
                {
                    string SQLtext = "SELECT * FROM Meeting WHERE id_meeting=(SELECT MAX(id_meeting) FROM Meeting)";
                    using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                    {
                        conn.Open();
                        SqlDataReader myReader = null;
                        SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                        myReader = myCommand.ExecuteReader();

                        while (myReader.Read())
                        {
                            meeting_id = Convert.ToInt32(myReader["id_meeting"].ToString());
                        }

                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Получить список id учеников
                List<string> ids_Students = new List<string>();

                Get_Students_IDs(ref ids_Students);

                try
                {
                    for (int i = 0; i < isComeGridView.RowCount; i++)
                    {
                        string SQLtext = "INSERT INTO Meeting_isCome VALUES" +
                        "(" + meeting_id + "," + Convert.ToInt32(ids_Students[i]) + "," +
                        "'" + isComeGridView.Rows[i].Cells["isCome"].Value.ToString() + "')";

                    using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                        {
                            conn.Open();
                            SqlCommand myCommand = new SqlCommand(SQLtext, conn);

                            myCommand.ExecuteNonQuery();

                            conn.Close();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Get_Meeting_Themes();
                meetingComboBox.SelectedIndex = meetingComboBox.Items.Count-1;

                meetingGridView.DataSource = SG("meeting_students", "[Тема собрания]",
                meetingComboBox.Text);
            }

            #endregion
            else
            {
                MessageBox.Show("Заполните все пункты!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Показать информацию о собрании
        private void meetingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            meetingGridView.DataSource = SG("meeting_students", "[Тема собрания]",
                meetingComboBox.Text);
        }

        // Отправка E-Mail
        public void Send_Email(string email_from, string password, string email_to, string theme, string text)
        {
            MailAddress from = new MailAddress(email_from, "Учитель Лукин В.А.");
            MailAddress to = new MailAddress(email_to);
            MailMessage message = new MailMessage(from, to);

            message.Subject = theme;

            message.Body = text;
            message.IsBodyHtml = true;

            string smtp_dom = "";
            int port = 25;

            if (email_from.ToLower().EndsWith("gmail.com"))
            {
                smtp_dom = "smtp.gmail.com";
                port = 587;
            }

            if (email_from.ToLower().EndsWith("yandex.ru"))
            {
                smtp_dom = "smtp.yandex.ru";
                port = 25;
            }

            if (email_from.ToLower().EndsWith("mail.ru"))
            {
                smtp_dom = "smtp.mail.ru";
                port = 25;
            }

            if (smtp_dom != "")
            {
                SmtpClient smtp = new SmtpClient(smtp_dom, port);
                smtp.Credentials = new NetworkCredential(email_from, password); //npuqvvyrluqeyrmt
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
            else
            {
                MessageBox.Show("Неизвестный домен! Смените E-Mail!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Отправление уведомлений о собрании
        private void notification_Button_Click(object sender, EventArgs e)
        {
            if (notification_TextBox.Text != "")
            {
                string bodyText = "<h1>Уважаемые родители!</h1>" +
                    "<p>" + notification_TimePicker.Text + " состоится собрание на тему " + 
                    "<b>\"" + notification_TextBox.Text + "\"</b>" + "</p>" +
                    "<p>Просьба явится на собрание!</p>";
                
                try
                {
                    string SQLtext = "select Email_father from Parents";
                    using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                    {
                        conn.Open();
                        SqlDataReader myReader = null;
                        SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                        myReader = myCommand.ExecuteReader();

                        while (myReader.Read())
                        {
                            Send_Email(Get_Email(), Get_Email_Pass(), myReader["Email_father"].ToString(), "Оповещение о собрании", bodyText);
                        }

                        conn.Close();
                    }

                    SQLtext = "select Email_mother from Parents";
                    using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                    {
                        conn.Open();
                        SqlDataReader myReader = null;
                        SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                        myReader = myCommand.ExecuteReader();

                        while (myReader.Read())
                        {
                            Send_Email(Get_Email(), Get_Email_Pass(), myReader["Email_mother"].ToString(), "Оповещение о собрании", bodyText);
                        }

                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                notification_TextBox.Clear();
            }
            else
            {
                MessageBox.Show("Введите тему собрания!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void информацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Email_Form emailForm = new Email_Form(this);
            emailForm.Show();
        }

        // Обновить ComboBox для E-Mail
        public void Update_Email_ComboBox()
        {
            try
            {
                string SQLtext = "select FIO_students from Students";
                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    conn.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        email_ComboBox.Items.Add(myReader["FIO_students"].ToString());
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Поиск по дате
        public string Select_Marks_With_Date()
        {
            string markString = "<h2>Оценки с " + dateTimePicker2.Text + " по " + dateTimePicker3.Text + "</h2>";

            if (email_ComboBox.Text != "")
            {
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
                dateTimePicker2.CustomFormat = "yyyy-MM-dd";
                string date1 = dateTimePicker2.Text;
                dateTimePicker2.Format = DateTimePickerFormat.Long;

                dateTimePicker3.Format = DateTimePickerFormat.Custom;
                dateTimePicker3.CustomFormat = "yyyy-MM-dd";
                string date2 = dateTimePicker3.Text;
                dateTimePicker3.Format = DateTimePickerFormat.Long;
                
                try
                {
                    string SQLtext = "SELECT * FROM students_marks WHERE [Дата выставления] >= '" + date1 +
                        "' AND [Дата выставления] <= '" + date2 + "'" +
                        " AND  [ФИО] = '" + email_ComboBox.Text + "'";
                    using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                    {
                        conn.Open();
                        SqlDataReader myReader = null;
                        SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                        myReader = myCommand.ExecuteReader();

                        while (myReader.Read())
                        {
                            markString = markString.Insert(markString.Length, "<p><b>" +
                                myReader["Предмет"].ToString() +
                                "</b> - " + myReader["Оценка"].ToString() +
                                ", <i>" + myReader["Тип"].ToString() +  
                                "</i>; <b>Дата:</b> " + 
                                myReader["Дата выставления"].ToString().Substring(0,
                                myReader["Дата выставления"].ToString().Length - 8) + 
                                "</p>");
                        }

                        conn.Close();
                    }
                }
                
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    markString = "";
                }
            } else
            {
                MessageBox.Show("Выберите ученика!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                markString = "";
            }

            return markString;
        }

        // Поиск по четверти
        public string Select_Marks_With_Fourth(string fourth)
        {
            //SELECT * FROM students_marks WHERE [Четверть] = '" + fourth + "' AND [ФИО] like @mask + '%'

            string markString = "<h2>Оценки за " + fourth + " четверть</h2>";

            if (email_ComboBox.Text != "")
            {
                try
                {
                    string SQLtext = "SELECT * FROM students_marks WHERE [Четверть] = " + fourth +
                        " AND  [ФИО] = '" + email_ComboBox.Text + "'";
                    using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                    {
                        conn.Open();
                        SqlDataReader myReader = null;
                        SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                        myReader = myCommand.ExecuteReader();

                        while (myReader.Read())
                        {
                            markString = markString.Insert(markString.Length, "<p><b>" +
                                myReader["Предмет"].ToString() +
                                "</b> - " + myReader["Оценка"].ToString() +
                                ", <i>" + myReader["Тип"].ToString() +
                                "</i>; <b>Дата:</b> " +
                                myReader["Дата выставления"].ToString().Substring(0,
                                myReader["Дата выставления"].ToString().Length - 8) +
                                "</p>");
                        }

                        conn.Close();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    markString = "";
                }
            }
            else
            {
                MessageBox.Show("Выберите ученика!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                markString = "";
            }

            return markString;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string bodyText = "";

            if (emailDate_Radio.Checked)
            {
                bodyText = Select_Marks_With_Date();
            }
            if (emailFirst_Radio.Checked)
            {
                bodyText = Select_Marks_With_Fourth("1");
            }
            if (emailSecond_Radio.Checked)
            {
                bodyText = Select_Marks_With_Fourth("2");
            }
            if (emailThird_Radio.Checked)
            {
                bodyText = Select_Marks_With_Fourth("3");
            }
            if (emailFourth_Radio.Checked)
            {
                bodyText = Select_Marks_With_Fourth("4");
            }

            try
            {
                string SQLtext = "select [E-Mail Отца] from students_parents WHERE [ФИО]='" +
                    email_ComboBox.Text + "'";
                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    conn.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        Send_Email(Get_Email(), Get_Email_Pass(), myReader["E-Mail Отца"].ToString(),
                            "Оценки " + email_ComboBox.Text, bodyText);
                    }

                    conn.Close();
                }

                SQLtext = "select [E-Mail Матери] from students_parents WHERE [ФИО]='" +
                    email_ComboBox.Text + "'";
                using (SqlConnection conn = new SqlConnection(DBConnect().ConnectionString))
                {
                    conn.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQLtext, conn);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        Send_Email(Get_Email(), Get_Email_Pass(), myReader["E-Mail Матери"].ToString(),
                            "Оценки " + email_ComboBox.Text, bodyText);
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
