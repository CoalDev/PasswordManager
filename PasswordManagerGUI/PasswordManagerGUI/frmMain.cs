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

//SQL Inserting to a database video
// https://youtu.be/hxOB5ALWQMQ


namespace PasswordManagerGUI
{
    public partial class frmMain : Form
    {
        public string salt;

        private bool mouseDown;
        private Point lastLocation;

        public frmMain()
        {
            InitializeComponent();
            
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'storageDataDataSet.AppData' table. You can move, or remove it, as needed.
            this.appDataTableAdapter.Fill(this.storageDataDataSet.AppData);
            //WriteData();
            AddUser.Hide();
            settingsUC.Hide();
            picAdd.Hide();
            picSetting.Hide();
            lblAdd.Hide();
            lblSetting.Hide();
            this.CenterToScreen();
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

        //private void WriteData()
        //{
        //    string connectionString =
        //        "Data Source=LAPTOP-IRITE623;Initial Catalog=StorageData;Integrated Security=True";
        //    string queryString =
        //        "SELECT Website, Username, Email, Password, UserLogin, Saltting FROM dbo.AppData;";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand command = new SqlCommand(
        //            queryString, connection);
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        try
        //        {
        //            txtDisplay.Text = "";
        //            // var passwordd = new String(txtDisplay.Text.Where(Char.IsDigit).ToArray());
        //            while (reader.Read())
        //            {

        //                //txtDisplay.Text += (string)reader[1];


        //                if (GUI.UsernameLogin == (string)reader[4])
        //                {
        //                    //reader[0], reader[1]));
        //                    //reader.Read(); 
        //                    //Encrypt pass = new Encrypt();
        //                    //string username = pass.ATOZ((string)reader[0]);
        //                    //string password = pass.ATOZ((string)reader[1]);

        //                    var usernameDecrypt = Cipher.Decrypt((string)reader[3], (string)reader[5]);
        //                    var passwordDecryot = Cipher.Decrypt((string)reader[1], (string)reader[5]);



        //                    txtDisplay.Text += "Website: " + reader[0] + "\n";
        //                    txtDisplay.Text += "Email: " + reader[2] + "\n";
        //                    txtDisplay.Text += "Username: " + usernameDecrypt + "\n";
        //                    txtDisplay.Text += "Password: " + passwordDecryot + "\n";
        //                    txtDisplay.Text +=
        //                        "---------------------------------------------";
        //                }
        //            }
        //        }
        //        finally
        //        {
        //            reader.Close();
        //        }
        //    }
        //}

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void BunifuFlatButton1_Click(object sender, EventArgs e)
        {
            AddUser.Show();
            settingsUC.Hide();
            picAdd.Show();
            picSetting.Hide();
            lblAdd.Show();
            lblSetting.Hide();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            AddUser.Hide();
            settingsUC.Show();
            picAdd.Hide();
            picSetting.Show();
            lblAdd.Hide();
            lblSetting.Show();
        }

        private void TxtDisplay_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        //private void BtnAdd_Click(object sender, EventArgs e)
        //{
        //    if (VaildateReg())
        //    {
        //        //Encrypt pass = new Encrypt();
        //        //string username = pass.ATOZ(txtUserAdd.Text);
        //        string email = txtEmailAdd.Text;
        //        var Website = txtSiteAdd.Text;
        //        //string password = pass.ATOZ(txtPassAdd.Text);
        //        CryptographyProcessor crypt = new CryptographyProcessor();

        //        salt = crypt.CreateSalt(10);
        //        var username = Cipher.Encrypt(txtPassAdd.Text, salt);
        //        var password = Cipher.Encrypt(txtUserAdd.Text, salt);

        //        //Decrypting method
        //        //var strDecrypted = Cipher.Decrypt(strEncryptred, salt);


        //        string queryString =
        //            "SELECT AppData FROM StorageData INSERT INTO AppData (Username, Email, Password, UserLogin, Salting) " +
        //            "VALUES (" + (string)username + ", " + (string)email + ", " + (string)password + ", " + GUI.UsernameLogin + ", " + (string)salt + ");";
        //        string connectionString =
        //            "Data Source=LAPTOP-IRITE623;Initial Catalog=StorageData;Integrated Security=True";

        //        try
        //        {
        //            using (SqlConnection sqlcon = new SqlConnection(connectionString))
        //            {
        //                sqlcon.Open();
        //                SqlCommand sqlcmd = new SqlCommand("DataAdd", sqlcon);
        //                sqlcmd.CommandType = CommandType.StoredProcedure;
        //                sqlcmd.Parameters.AddWithValue("@Website", Website);
        //                sqlcmd.Parameters.AddWithValue("@Username", username);
        //                sqlcmd.Parameters.AddWithValue("@Email", email);
        //                sqlcmd.Parameters.AddWithValue("@Password", password);
        //                sqlcmd.Parameters.AddWithValue("@UserLogin", GUI.UsernameLogin);
        //                sqlcmd.Parameters.AddWithValue("@Saltting", salt);

        //                sqlcmd.ExecuteNonQuery();

        //                WriteData();
        //            }
        //        }
        //        catch
        //        {
        //            MessageBox.Show("Failed to store data.");
        //        }
        //    }
        //    else
        //    {

        //    }
        //    //txtDisplay.Text +=
        //}

        //private bool VaildateReg()
        //{
        //    int count = 0;

        //    if (txtUserAdd.Text.Trim().Length <= 4)
        //    {
        //        errorProvider1.SetError(txtUserAdd, "Please Enter Correct User Name, Needs to be atleast 5 characters long");
        //        count++;
        //    }

        //    if (txtPassAdd.Text.Trim().Length <= 7)
        //    {
        //        errorProvider1.SetError(txtPassAdd, "Password needs to be at least 8 characters!");
        //        count++;
        //    }

        //    if (count > 0 && count <= 3)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            GUI login = new GUI();
            this.Hide();
            login.Show();
        }

        private void AddUser1_Load(object sender, EventArgs e)
        {

        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnExit_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
