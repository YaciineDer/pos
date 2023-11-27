using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using Guna.UI2.WinForms;
using System.Runtime.CompilerServices;

namespace gestion1
{
    public partial class Usermanager : Form
    {
        public Usermanager()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""" + DatabaseConfiguration.databaseConnection + @""";Integrated Security=True;Connect Timeout=30");

        void populate()
        {
        try
        {
        con.Open();
        String Myquery = "select * from tableutilisateur ";
        SqlDataAdapter da = new SqlDataAdapter(Myquery, con);
        SqlCommandBuilder builder = new SqlCommandBuilder(da);
        var ds = new DataSet();
        da.Fill(ds);
        UsersGV.DataSource = ds.Tables[0];
        UsersGV.Columns["Id"].Visible = false;
        UsersGV.Columns["Uname"].HeaderText = "Nom d'utilisateur";
        UsersGV.Columns["Password"].HeaderText = "Mot de passe";
        }
         catch (Exception ex)
        {
        MessageBox.Show("Une erreur s'est produite : " + ex.Message);
        }
        finally
        {
        con.Close();
        }
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Unameaddtb.Text) || string.IsNullOrWhiteSpace(passwordaddtb.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs.");
                    return;
                }

                // Check if the user has the necessary role to perform the action
                string selectedUserRole = usertypecmbx.SelectedItem?.ToString(); // Use null propagation
                if (selectedUserRole != "Admin" && selectedUserRole != "Gestionnaire de stock")
                {
                    MessageBox.Show("Rôle non autorisé pour ajouter un utilisateur.");
                    return;
                }

                if (con.State == ConnectionState.Closed)
                    con.Open();

                // Check if the username already exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM tableutilisateur WHERE Uname = @Uname", con);
                checkCmd.Parameters.AddWithValue("@Uname", Unameaddtb.Text);
                int existingCount = (int)checkCmd.ExecuteScalar();

                if (existingCount > 0)
                {
                    MessageBox.Show("Le nom d'utilisateur existe déjà.");
                    // Clear the text boxes
                    Unameaddtb.Text = "";
                    passwordaddtb.Text = "";
                    usertypecmbx.SelectedItem = null; // Clear the combo box selection
                }
                else
                {
                    // Insert the record if the username doesn't exist
                    SqlCommand insertCmd = new SqlCommand("INSERT INTO tableutilisateur (Uname, Password, Role) VALUES (@Uname, @Password, @Role); SELECT SCOPE_IDENTITY();", con);
                    insertCmd.Parameters.AddWithValue("@Uname", Unameaddtb.Text);
                    insertCmd.Parameters.AddWithValue("@Password", passwordaddtb.Text);
                    insertCmd.Parameters.AddWithValue("@Role", selectedUserRole); // Use the selected role
                    int newUserId = Convert.ToInt32(insertCmd.ExecuteScalar());
                    MessageBox.Show("Utilisateur ajouté avec succès.");

                    // Add the new user to the DataGridView directly
                    DataTable dt = (DataTable)UsersGV.DataSource;
                    DataRow newRow = dt.NewRow();
                    newRow["Id"] = newUserId; // Assign the new user's ID
                    newRow["Uname"] = Unameaddtb.Text;
                    newRow["Password"] = passwordaddtb.Text;
                    newRow["Role"] = selectedUserRole; // Assign the selected role
                    dt.Rows.Add(newRow);

                    // Clear the text boxes
                    Unameaddtb.Text = "";
                    passwordaddtb.Text = "";
                    usertypecmbx.SelectedItem = null; // Clear the combo box selection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }





        private void Delete_button_Click(object sender, EventArgs e)
        {

                if (UsersGV.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = UsersGV.SelectedRows[0].Index;
                    int selectedUserId = Convert.ToInt32(UsersGV.SelectedRows[0].Cells["Id"].Value);

                    try
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        SqlCommand deleteCmd = new SqlCommand("DELETE FROM tableutilisateur WHERE Id = @Id", con);
                        deleteCmd.Parameters.AddWithValue("@Id", selectedUserId);
                        int rowsAffected = deleteCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Utilisateur supprimé avec succès.");

                            // Remove the deleted row from the DataGridView
                            UsersGV.Rows.RemoveAt(selectedRowIndex);

                            // Clear the text boxes
                            Unameaddtb.Text = "";
                            passwordaddtb.Text = "";
                            usertypecmbx.SelectedItem = null;
                    }
                        else
                        {
                            MessageBox.Show("La suppression a échoué.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Une erreur s'est produite : " + ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Sélectionnez un utilisateur à supprimer.");
                }
            

        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (UsersGV.SelectedRows.Count > 0)
            {
                Unameaddtb.Text = UsersGV.SelectedRows[0].Cells["Uname"].Value.ToString();
                passwordaddtb.Text = UsersGV.SelectedRows[0].Cells["Password"].Value.ToString();

                usertypecmbx.SelectedItem = UsersGV.SelectedRows[0].Cells["Role"].Value.ToString();
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (UsersGV.SelectedRows.Count > 0)
            {
                int selectedUserId = Convert.ToInt32(UsersGV.SelectedRows[0].Cells["Id"].Value);

                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilenam=""" + DatabaseConfiguration.databaseConnection + @""";Integrated Security=True;Connect Timeout=30"))
                    {
                        con.Open();

                        // Retrieve the selected user's data from the database
                        SqlCommand selectCmd = new SqlCommand("SELECT * FROM tableutilisateur WHERE Id = @Id", con);
                        selectCmd.Parameters.AddWithValue("@Id", selectedUserId);
                        SqlDataReader reader = selectCmd.ExecuteReader();

                        if (reader.Read())
                        {
                            // Close the reader
                            reader.Close();

                            // Update the record with edited values
                            SqlCommand updateCmd = new SqlCommand("UPDATE tableutilisateur SET Uname = @Uname, Password = @Password, Role = @Role WHERE Id = @Id", con);
                            updateCmd.Parameters.AddWithValue("@Uname", Unameaddtb.Text);
                            updateCmd.Parameters.AddWithValue("@Password", passwordaddtb.Text);
                            updateCmd.Parameters.AddWithValue("@Role", usertypecmbx.SelectedItem?.ToString());
                            updateCmd.Parameters.AddWithValue("@Id", selectedUserId);
                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Utilisateur mis à jour avec succès.");

                                // Update the corresponding row in the DataGridView
                                DataGridViewRow selectedRow = UsersGV.SelectedRows[0];
                                selectedRow.Cells["Uname"].Value = Unameaddtb.Text;
                                selectedRow.Cells["Password"].Value = passwordaddtb.Text;
                                selectedRow.Cells["Role"].Value = usertypecmbx.SelectedItem?.ToString();

                                // Clear the text boxes
                                Unameaddtb.Text = "";
                                passwordaddtb.Text = "";
                                usertypecmbx.SelectedItem = null;

                            }
                            else
                            {
                                MessageBox.Show("La mise à jour a échoué.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Utilisateur introuvable.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Une erreur s'est produite : " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Sélectionnez un utilisateur à éditer.");
            }
        }




        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Usermanager_Load(object sender, EventArgs e)
        {

        }

        private void homereturn_Click(object sender, EventArgs e)
        {
            Form existingForm = Application.OpenForms.OfType<interfaceprin>().FirstOrDefault();

            if (existingForm != null)
            {
                existingForm.Show();
                existingForm.BringToFront(); // Bring the existing form to the front
                this.Close();
            }
            else
            {
                interfaceprin intrface = new interfaceprin();
                intrface.Show();
                this.Close();
            }
        }

    }
}
