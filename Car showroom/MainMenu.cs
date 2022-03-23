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
    public partial class MainMenu : Form
    {
        SqlConnection sqlConnection = null;
        public MainMenu()
        {
            InitializeComponent();
            this.MinimumSize = new Size(816, 489);
            this.MaximumSize = new Size(816, 489);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            string loc = Environment.CurrentDirectory;
            loc = loc.Replace("bin\\Debug", "");
            string conString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={loc}Database.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(conString);
            sqlConnection.Open();

            //SqlCommand a = new SqlCommand($"select count(*) from [Car_Colors]", sqlConnection);
            //int n = Convert.ToInt32(a.ExecuteScalar());
            //for (int i = 10; i < n + 10; i++)
            //{
            //    SqlCommand b = new SqlCommand($"select \"Name_Color\" from [Car_Colors] where \"Code_Color\" like '{i + 1}'", sqlConnection);
            //    comboBox1.Items.Add(b.ExecuteScalar().ToString());
            //}

            SqlCommand aboba = new SqlCommand($"select Name_Color from [Car_Colors]", sqlConnection);
            SqlDataReader reader = aboba.ExecuteReader();
            while (reader.Read())
            {
                string result = reader.GetString(0);
                comboBox1.Items.Add(result);
            }
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
