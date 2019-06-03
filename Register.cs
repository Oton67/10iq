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
    public partial class Register : Form
    {
        string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|testlogin.mdf;Integrated Security=True;Connect Timeout=30";
        public Register()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Register_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Reg_Click(object sender, EventArgs e)
        {
            int flag = 0;

            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                string CheckUsername;

                SqlCommand oCmd = new SqlCommand("SELECT username from login", sqlCon);
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        CheckUsername = (oReader["username"]).ToString();
                        if (CheckUsername == username.Text.ToString() || CheckUsername == username.Text.ToString().ToLower() || CheckUsername == username.Text.ToString().ToUpper()) flag = 1;
                    }

                }
                sqlCon.Close();
            }
            if (flag == 1) MessageBox.Show("Username already exists!");
            else if (username.Text.ToString().ToLower() == "admin") MessageBox.Show("Can't make ADMIN account!");
            else if (username.Text == "" || password.Text == "") MessageBox.Show("Please fill all the fields!");
            else if (password.Text != Cpassword.Text) MessageBox.Show("Password does not match!");
            else
            {

                MD5 md5 = new MD5CryptoServiceProvider();
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password.Text.Trim()));
                byte[] result = md5.Hash;

                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    strBuilder.Append(result[i].ToString("x2"));
                }

                using (SqlConnection sqlCon = new SqlConnection(connection))
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("UserAdd", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@username", username.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@password", strBuilder.ToString());
                    sqlCmd.Parameters.AddWithValue("@FirstName", FirstName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@LastName", LastName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Email", Email.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Contact", Contact.Text.Trim());
                    sqlCmd.ExecuteNonQuery();

                    MessageBox.Show("Registration Successgull!");
                    Clear();

                    this.Close();
                    Form1 login = new Form1();
                    login.Show();
                }
            }
        }

        void Clear()
        {
            username.Text = password.Text = Cpassword.Text = FirstName.Text = LastName.Text = Email.Text = Contact.Text = "";
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 login = new Form1();
            login.Show();
        }

        private void Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (Char.IsDigit(chr))
            {
                e.Handled = true;
                MessageBox.Show("Please enter a valid value!");
            }
            
        }

        private void Letter_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (Char.IsLetter(chr))
            {
                e.Handled = true;
                MessageBox.Show("Please enter a valid value!");
            }
            if (char.IsWhiteSpace(chr))
            {
                e.Handled = true;
                MessageBox.Show("No spaces!");
            }
        }

        private void IsSpace(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (char.IsWhiteSpace(chr))
            {
                e.Handled = true;
                MessageBox.Show("No spaces!");
            }
        }


    }
}
