using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PasswordManagerGUI
{
    public partial class AddUser : UserControl
    {

        public string salt;

        public AddUser()
        {
            
            InitializeComponent();
            WriteData();
        }

        private void TxtDisplay_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddUser_Load(object sender, EventArgs e)
        {

        }

        private void WriteData()
        {
            string connectionString =
                "Data Source=LAPTOP-IRITE623;Initial Catalog=StorageData;Integrated Security=True";
            string queryString =
                "SELECT Website, Username, Email, Password, UserLogin, Saltting FROM dbo.AppData;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    txtDisplay.Text = "";
                    // var passwordd = new String(txtDisplay.Text.Where(Char.IsDigit).ToArray());
                    while (reader.Read())
                    {

                        //txtDisplay.Text += (string)reader[1];


                        if (GUI.UsernameLogin == (string)reader[4])
                        {
                            //reader[0], reader[1]));
                            //reader.Read(); 
                            //Encrypt pass = new Encrypt();
                            //string username = pass.ATOZ((string)reader[0]);
                            //string password = pass.ATOZ((string)reader[1]);

                            var usernameDecrypt = Cipher.Decrypt((string)reader[3], (string)reader[5]);
                            var passwordDecryot = Cipher.Decrypt((string)reader[1], (string)reader[5]);



                            txtDisplay.Text += "Website: " + reader[0] + "\n";
                            txtDisplay.Text += "Email: " + reader[2] + "\n";
                            txtDisplay.Text += "Username: " + usernameDecrypt + "\n";
                            txtDisplay.Text += "Password: " + passwordDecryot + "\n";
                            txtDisplay.Text +=
                                "---------------------------------------------";
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (VaildateReg())
            {
                //Encrypt pass = new Encrypt();
                //string username = pass.ATOZ(txtUserAdd.Text);
                string email = txtEmailAdd.Text;
                var Website = txtSiteAdd.Text;
                //string password = pass.ATOZ(txtPassAdd.Text);
                CryptographyProcessor crypt = new CryptographyProcessor();

                salt = crypt.CreateSalt(10);
                var username = Cipher.Encrypt(txtPassAdd.Text, salt);
                var password = Cipher.Encrypt(txtUserAdd.Text, salt);

                //Decrypting method
                //var strDecrypted = Cipher.Decrypt(strEncryptred, salt);


                string queryString =
                    "SELECT AppData FROM StorageData INSERT INTO AppData (Username, Email, Password, UserLogin, Salting) " +
                    "VALUES (" + (string)username + ", " + (string)email + ", " + (string)password + ", " + GUI.UsernameLogin + ", " + (string)salt + ");";
                string connectionString =
                    "Data Source=LAPTOP-IRITE623;Initial Catalog=StorageData;Integrated Security=True";

                try
                {
                    using (SqlConnection sqlcon = new SqlConnection(connectionString))
                    {
                        sqlcon.Open();
                        SqlCommand sqlcmd = new SqlCommand("DataAdd", sqlcon);
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddWithValue("@Website", Website);
                        sqlcmd.Parameters.AddWithValue("@Username", username);
                        sqlcmd.Parameters.AddWithValue("@Email", email);
                        sqlcmd.Parameters.AddWithValue("@Password", password);
                        sqlcmd.Parameters.AddWithValue("@UserLogin", GUI.UsernameLogin);
                        sqlcmd.Parameters.AddWithValue("@Saltting", salt);

                        sqlcmd.ExecuteNonQuery();

                        WriteData();
                    }
                }
                catch
                {
                    MessageBox.Show("Failed to store data.");
                }
            }
            else
            {

            }
            //txtDisplay.Text +=
        }

        private bool VaildateReg()
        {
            int count = 0;

            if (txtUserAdd.Text.Trim().Length <= 4)
            {
                errorProvider1.SetError(txtUserAdd, "Please Enter Correct User Name, Needs to be atleast 5 characters long");
                count++;
            }

            if (txtPassAdd.Text.Trim().Length <= 7)
            {
                errorProvider1.SetError(txtPassAdd, "Password needs to be at least 8 characters!");
                count++;
            }

            if (count > 0 && count <= 3)
            {
                return false;
            }

            return true;
        }
    }
}
