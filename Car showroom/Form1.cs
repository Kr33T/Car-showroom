using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlClient;

namespace Car_showroom
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection = null;
        public Form1()
        {
            InitializeComponent();
            this.MinimumSize = new Size(400, 400);
            this.MaximumSize = new Size(400, 400);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string loc = Environment.CurrentDirectory;
            loc = loc.Replace("bin\\Debug", "");
            string conString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={loc}Database.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(conString);
            sqlConnection.Open();
            //if(sqlConnection.State == ConnectionState.Open)
            //{
            //    MessageBox.Show("Подключение есть!");
            //}
        }

        private void signIn_Click(object sender, EventArgs e)
        {
            MainMenu menu = new MainMenu();
            textBox1.Text = textBox1.Text.Trim();
            textBox2.Text = textBox2.Text.Trim();
            SqlCommand zapros = new SqlCommand($"select count(*) from [autorizathion] where [login] like '{textBox1.Text}' and [password] like '{textBox2.Text}'", sqlConnection);
            if (zapros.ExecuteScalar().ToString() == "1")
            {
                menu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Введен неправильный пароль или логин!");
            }
        }

        private void signUp_Click(object sender, EventArgs e)
        {
            SqlCommand check1 = new SqlCommand($"select count(*) from [autorizathion] where [login] like '{textBox1.Text}'", sqlConnection);
            //SqlCommand check2 = new SqlCommand($"select count(*) from [autorizathion] where [password] like '{textBox2.Text}'", sqlConnection);
            textBox1.Text = textBox1.Text.Trim();
            textBox2.Text = textBox2.Text.Trim();
            if (check1.ExecuteScalar().ToString() == "0")
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Поля не заполнены!");
                }
                else
                {
                    if(textBox2.Text.Length <= 4)
                    {
                        MessageBox.Show("Пароль слишком маленький!");
                    }
                    else
                    {
                        SqlCommand zapros = new SqlCommand($"insert into [autorizathion] (login, password) values (@login, @password)", sqlConnection);
                        zapros.Parameters.AddWithValue("login", textBox1.Text);
                        zapros.Parameters.AddWithValue("password", textBox2.Text);
                        if (zapros.ExecuteNonQuery().ToString() == "1")
                        {
                            MessageBox.Show("Регистрация прошла успешно!");
                            textBox1.Text = "";
                            textBox2.Text = "";
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы уже зарегистрированы!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
