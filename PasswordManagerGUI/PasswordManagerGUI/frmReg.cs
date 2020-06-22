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
using System.Runtime.InteropServices;

namespace PasswordManagerGUI
{
    public partial class frmReg : Form
    {
        private int checkUser = -1;

        private bool mouseDown;
        private Point lastLocation;

        public frmReg()
        {
            InitializeComponent();
        }

        private void frmReg_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void frmReg_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void frmReg_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void FrmReg_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void FrmReg_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TxtEmailReg_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnReg_Click(object sender, EventArgs e)
        {

            string connectionStringg = "Data Source=LAPTOP-IRITE623;Initial Catalog=Users;Integrated Security=True";
            string queryStringg =
                "SELECT Username, Password, Saltting FROM dbo.Data;";
            using (SqlConnection connection = new SqlConnection(connectionStringg))
            {
                SqlCommand command = new SqlCommand(
                    queryStringg, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    
                    checkUser = 0;

                    while (reader.Read())
                    {
                        string readerr = (string)reader[0];
                        if (txtUserReg.Text.ToLower() != readerr.ToLower())
                        {
                            checkUser++;
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
            }


            if (GUI.count == checkUser)
            {

                if (VaildateReg())
                {
                    //Encrypt pass = new Encrypt();
                    //string username = pass.ATOZ(txtUserReg.Text);
                    //string email = txtEmailReg.Text;
                    //string password = pass.ATOZ(txtPassReg.Text);z

                    CryptographyProcessor crypt = new CryptographyProcessor();
                    string salt = crypt.CreateSalt(10);
                    string password = crypt.GenerateHash(txtPassReg.Text, salt);
                    string username = txtUserReg.Text;
                    string email = txtEmailReg.Text;

                    string queryString =
                        "SELECT Data FROM Users INSERT INTO Data (Username, Email, Password, Admin, Saltting) " +
                        "VALUES (" + (string)username + ", " + (string)email + ", " + (string)password + ", " + 0 + ", " + salt + ");";
                    string connectionString = "Data Source=LAPTOP-IRITE623;Initial Catalog=Users;Integrated Security=True";

                    try
                    {
                        using (SqlConnection sqlcon = new SqlConnection(connectionString))
                        {
                            sqlcon.Open();
                            SqlCommand sqlcmd = new SqlCommand("UserADD", sqlcon);
                            sqlcmd.CommandType = CommandType.StoredProcedure;
                            sqlcmd.Parameters.AddWithValue("@Username", username);
                            sqlcmd.Parameters.AddWithValue("@Email", email);
                            sqlcmd.Parameters.AddWithValue("@Password", password);
                            sqlcmd.Parameters.AddWithValue("@Admin", 0);
                            sqlcmd.Parameters.AddWithValue("@Saltting", salt);

                            sqlcmd.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Failed to store user");
                    }


                    GUI login = new GUI();
                    this.Hide();
                    login.Show();
                }
                else
                {

                }
            }else
            {
                MessageBox.Show("Name isn't available!");
            }
        }

        private bool VaildateReg()
        {
            int count = 0;

            if (txtUserReg.Text.Trim().Length <= 4)
            {
                errorProvider1.SetError(txtUserReg, "Please Enter Correct User Name, Needs to be atleast 5 characters long");
                count++;
            }

            if (txtEmailReg.Text.Trim().Length <= 14)
            {
                errorProvider1.SetError(txtEmailReg, "Please Enter Correct Email");
                count++;
            }

            if (txtPassReg.Text.Trim().Length <= 7)
            {
                errorProvider1.SetError(txtPassReg, "Password needs to be at least 8 characters!");
                count++;
            }

            if(txtRePassReg.Text != txtPassReg.Text)
            {
                errorProvider1.SetError(txtPassReg, "Passwords do not match!");
                count++;
            }

            if(count > 0 && count <= 3)
            {
                return false;
            }

            return true;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            GUI login = new GUI();
            this.Hide();
            login.Show();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.FromArgb(255, 78, 0);
        }

        private void PictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.FromArgb(255, 128, 0);
        }

        private void Minimize_MouseHover(object sender, EventArgs e)
        {
            minimize.BackColor = Color.FromArgb(255, 78, 0);
        }

        private void Minimize_MouseLeave(object sender, EventArgs e)
        {
            minimize.BackColor = Color.FromArgb(255, 128, 0);
        }

        private void PictureBox7_Click(object sender, EventArgs e)
        {
            GUI login = new GUI();
            this.Hide();
            login.Show();
        }

        private void PictureBox7_MouseHover(object sender, EventArgs e)
        {
            pictureBox7.BackColor = Color.FromArgb(255, 78, 0);
        }

        private void PictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.BackColor = Color.FromArgb(255, 128, 0);
        }
    }
}
