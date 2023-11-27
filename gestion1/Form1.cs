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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace gestion1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            

        }
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""" + DatabaseConfiguration.databaseConnection + @""";Integrated Security=True;Connect Timeout=30");



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Unametb.Text = "";
            passwordtb.Text = "";
        }

        private void passwordtb_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            
        }



        private void cnxbutton_Click(object sender, EventArgs e)
        {
            string username = Unametb.Text.Trim();
            string password = passwordtb.Text.Trim();

            try
            {
                // Open the connection
                connection.Open();

                // Create a query to check if the user exists with the provided username and password
                string query = "SELECT Count(*) FROM tableutilisateur WHERE Uname = @Username AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                // Execute the query to check if the user exists
                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    gestiondespersonnes prsn = new gestiondespersonnes();
                    prsn.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Veuiller entrer des informations valid");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during connection
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection, whether it succeeded or not
                connection.Close();
            }
        }


    }
}
