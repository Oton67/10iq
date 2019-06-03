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
using System.Security.Cryptography;
using System.Diagnostics;

namespace Item_menagment
{
    public partial class Form1 : Form
    {
        string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|testlogin.mdf;Integrated Security=True;Connect Timeout=30";
        int LoginID;

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           // Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);


            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password.Text.Trim()));
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from login where username ='"+username.Text+"' and password='"+strBuilder.ToString()+"'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            SqlCommand oCmd = new SqlCommand("SELECT id from login where username ='" + username.Text + "'", conn);
            conn.Open();
            using (SqlDataReader oReader = oCmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    LoginID = Convert.ToInt32(oReader["id"]);
                   // MessageBox.Show(matchingUsername);
                }

                conn.Close();
            }
     
            if (dt.Rows[0][0].ToString() == "1")
            {
                if (LoginID == 1 || LoginID == 2)
                {
                    this.Hide();
                    AdminMain admin = new AdminMain();
                    admin.Show();
                }

                else
                {
                    this.Hide();
                    Main user = new Main(LoginID);
                    user.Show();
                }
            }
            else MessageBox.Show("Please enter the correct username and password", "alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register register = new Register();
            register.Show();
        }
    }
}
