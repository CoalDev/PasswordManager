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

    public partial class GUI : Form
    {
        public static string UsernameLogin;
        public static int count = 0;

        private bool mouseDown;
        private Point lastLocation;

        public GUI()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            try
            {
                string connectionString = "Data Source=LAPTOP-IRITE623;Initial Catalog=Users;Integrated Security=True";
                string queryString =
                    "SELECT Username, Password, Saltting FROM dbo.Data;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(
                        queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {

                        while (reader.Read())
                        {
                            count++;
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Failed to read tables");
            }
        }

        private void GUI_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void GUI_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void GUI_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void GUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void BtnReg_Click(object sender, EventArgs e)
        {
            this.Hide();

            frmReg objfrmReg = new frmReg();
            objfrmReg.Show();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateLogin())
            {
                string connectionString = "Data Source=LAPTOP-IRITE623;Initial Catalog=Users;Integrated Security=True";
                string queryString =
                    "SELECT Username, Password, Saltting FROM dbo.Data;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(
                        queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        string username = txtUser.Text;

                        UsernameLogin = username;
                        while (reader.Read())
                        {
                            //var passwordd = new String(reader[1].Where(Char.IsDigit).ToArray());
                            //reader[0], reader[1]));
                            //reader.Read(); 
                            //Encrypt pass = new Encrypt();
                            //string username = pass.ATOZ(txtUser.Text);
                            //string password = pass.ATOZ(txtPass.Text);

                            //count++;

                            CryptographyProcessor crypt = new CryptographyProcessor();
                            //string password = crypt.GenerateHash(txtPass.Text, salt);
                            

                            if (username == (string)reader[0] && crypt.AreEqual(txtPass.Text, (string)reader[1], (string)reader[2]))
                            {
                                this.Hide();
                                frmMain main = new frmMain();
                                main.Show();
                                break;
                            }
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }
        }

        private bool ValidateLogin()
        {
            int count = 0;

            if (txtUser.Text.Trim().Length == 0)
            {
                count++;
                errorProvider1.SetError(txtUser, "Please enter a Username");
            }
            if (txtPass.Text.Trim().Length == 0)
            {
                count++;
                errorProvider1.SetError(txtPass, "Please enter a Password");
            }

            if (count > 0)
            {
                return false;
            }
            return true;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void BunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
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
    }
    //80, 96, 255 color kind of blue for login button, DO NOT REMOVE COMMENT!
    public partial class CustomTextBox : TextBox
    {
        public CustomTextBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
        }
    }
}
