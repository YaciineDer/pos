using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestion1
{
    public partial class gestiondespersonnes : Form
    {
        public gestiondespersonnes()
        {
            InitializeComponent();
            populate();

        }
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""" + DatabaseConfiguration.databaseConnection + @""";Integrated Security=True;Connect Timeout=30");

        void populate()
        {
            try
            {
                connection.Open();
                String Myquery = "SELECT * FROM tableclient " +
                    "UNION " +
                    "SELECT * FROM tablefournisseurs;";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                PersonnesGV.DataSource = ds.Tables[0];
                PersonnesGV.Columns["Id"].Visible = false;
                PersonnesGV.Columns["Nom"].HeaderText = "Nom";
                PersonnesGV.Columns["Telephone"].HeaderText = "Num Telephone";
                PersonnesGV.Columns["Adresse"].HeaderText = "Adresse";
                PersonnesGV.Columns["PersonType"].HeaderText = "Type";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }




        private void addclientbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nomprsntb.Text) || string.IsNullOrWhiteSpace(numtlphnprsntb.Text) || string.IsNullOrWhiteSpace(Adrsprsntb.Text) || typepersonnescmbx.SelectedItem == null)
                {
                    MessageBox.Show("Veuillez remplir tous les champs et choisir un type de personne.");
                    return;
                }

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string personType = typepersonnescmbx.SelectedItem.ToString();
                string nom = nomprsntb.Text;
                string telephone = numtlphnprsntb.Text;
                string adresse = Adrsprsntb.Text;

                // Check if the person with the same name already exists in the combined result
                SqlCommand checkCmd = new SqlCommand($"SELECT COUNT(*) FROM (SELECT DISTINCT Nom FROM tableclient UNION SELECT DISTINCT Nom FROM tablefournisseurs) AS Combined WHERE Nom = @Nom", connection);
                checkCmd.Parameters.AddWithValue("@Nom", nom);
                int existingCount = (int)checkCmd.ExecuteScalar();

                if (existingCount > 0)
                {
                    MessageBox.Show("La personne existe déjà en tant que client ou fournisseur.");
                    return;
                }

                // Insert into the appropriate table
                string insertQuery = "INSERT INTO ";
                if (personType == "Client et Fournisseur")
                {
                    insertQuery += "tableclient (Nom, Telephone, Adresse, PersonType) VALUES (@Nom, @Telephone, @Adresse, @PersonType); INSERT INTO tablefournisseurs (Nom, Telephone, Adresse, PersonType) VALUES (@Nom, @Telephone, @Adresse, @PersonType);";
                }
                else if (personType == "Client")
                {
                    insertQuery += "tableclient (Nom, Telephone, Adresse, PersonType) VALUES (@Nom, @Telephone, @Adresse, @PersonType);";
                }
                else if (personType == "Fournisseur")
                {
                    insertQuery += "tablefournisseurs (Nom, Telephone, Adresse, PersonType) VALUES (@Nom, @Telephone, @Adresse, @PersonType);";
                }
                else
                {
                    MessageBox.Show("Type de personne non pris en charge.");
                    return;
                }

                SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
                insertCmd.Parameters.AddWithValue("@Nom", nom);
                insertCmd.Parameters.AddWithValue("@Telephone", telephone);
                insertCmd.Parameters.AddWithValue("@Adresse", adresse);
                insertCmd.Parameters.AddWithValue("@PersonType", personType);
                insertCmd.ExecuteNonQuery();

                // Fetch updated data from the combined result
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT DISTINCT * FROM tableclient UNION SELECT DISTINCT * FROM tablefournisseurs", connection);
                DataTable combinedTable = new DataTable();
                adapter.Fill(combinedTable);

                // Set the DataGridView's DataSource to the combined result
                PersonnesGV.DataSource = combinedTable;

                // Clear the text boxes
                nomprsntb.Text = "";
                numtlphnprsntb.Text = "";
                Adrsprsntb.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }









        private void numtlphnclienttb_TextChanged(object sender, EventArgs e)
        {

            // Use a regular expression to remove non-numeric characters
            string numericInput = Regex.Replace(numtlphnprsntb.Text, "[^0-9]", "");

            // Update the textbox with the numeric input
            numtlphnprsntb.Text = numericInput;

        }


        private void deleteclientbtn_Click(object sender, EventArgs e)
        {
            if (PersonnesGV.SelectedRows.Count > 0)
            {
                int selectedRowIndex = PersonnesGV.SelectedRows[0].Index;
                int selectedUserId = Convert.ToInt32(PersonnesGV.SelectedRows[0].Cells["Id"].Value);
                string personType = PersonnesGV.SelectedRows[0].Cells["PersonType"].Value.ToString();

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlCommand deleteCmd;
                    if (personType == "Client et Fournisseur")
                    {
                        // If the person is both a client and a supplier, delete from both tables
                        deleteCmd = new SqlCommand(
                            "BEGIN " +
                            "DELETE FROM tableclient WHERE Id = @Id; " +
                            "DELETE FROM tablefournisseurs WHERE Id = @Id; " +
                            "END;", connection);
                    }
                    else if (personType == "Client")
                    {
                        // If the person is only a client, delete from the client table
                        deleteCmd = new SqlCommand("DELETE FROM tableclient WHERE Id = @Id", connection);
                    }
                    else if (personType == "Fournisseur")
                    {
                        // If the person is only a supplier, delete from the supplier table
                        deleteCmd = new SqlCommand("DELETE FROM tablefournisseurs WHERE Id = @Id", connection);
                    }
                    else
                    {
                        MessageBox.Show("Type de personne non pris en charge.");
                        return;
                    }

                    deleteCmd.Parameters.AddWithValue("@Id", selectedUserId);
                    int rowsAffected = deleteCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Personne supprimée avec succès.");
                    }
                    else
                    {
                        MessageBox.Show("La suppression de la personne a échoué.");
                    }

                    // Remove the deleted row from the DataGridView
                    PersonnesGV.Rows.RemoveAt(selectedRowIndex);

                    // Clear the text boxes
                    nomprsntb.Text = "";
                    numtlphnprsntb.Text = "";
                    Adrsprsntb.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Une erreur s'est produite : " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Sélectionnez une personne à supprimer.");
            }
        }





        private void ClientssGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (PersonnesGV.SelectedRows.Count > 0)
            {
                nomprsntb.Text = PersonnesGV.SelectedRows[0].Cells["Nom"].Value.ToString();
                numtlphnprsntb.Text = PersonnesGV.SelectedRows[0].Cells["Telephone"].Value.ToString();
                Adrsprsntb.Text = PersonnesGV.SelectedRows[0].Cells["Adresse"].Value.ToString();
                typepersonnescmbx.SelectedItem = PersonnesGV.SelectedRows[0].Cells["PersonType"].Value.ToString();
            }
        }

        private void Editclientbtn_Click(object sender, EventArgs e)
        {
            if (PersonnesGV.SelectedRows.Count > 0)
            {
                int selectedClientId = Convert.ToInt32(PersonnesGV.SelectedRows[0].Cells["Id"].Value);

                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""" + DatabaseConfiguration.databaseConnection + @""";Integrated Security=True;Connect Timeout=30"))
                    {
                        con.Open();

                        // Retrieve the selected client's data from the database
                        SqlCommand selectCmd = new SqlCommand("SELECT * FROM tableclient WHERE Id = @Id", con);
                        selectCmd.Parameters.AddWithValue("@Id", selectedClientId);
                        SqlDataReader reader = selectCmd.ExecuteReader();

                        if (reader.Read())
                        {
                            // Close the reader
                            reader.Close();

                            // Update the record with edited values
                            SqlCommand updateCmd = new SqlCommand("UPDATE tableclient SET Nomduclient = @Nomduclient, Numtelephone = @Numtelephone, Adresse = @Adresse, Numpiecedidentite = @Numpiecedidentite WHERE Id = @Id", con);
                            updateCmd.Parameters.AddWithValue("@Nomduclient", nomprsntb.Text);
                            updateCmd.Parameters.AddWithValue("@Numtelephone", numtlphnprsntb.Text);
                            updateCmd.Parameters.AddWithValue("@Adresse", Adrsprsntb.Text);
                            updateCmd.Parameters.AddWithValue("@Id", selectedClientId);
                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Client mis à jour avec succès.");

                                // Update the corresponding row in the DataGridView
                                DataGridViewRow selectedRow = PersonnesGV.SelectedRows[0];
                                selectedRow.Cells["Nomduclient"].Value = nomprsntb.Text;
                                selectedRow.Cells["Numtelephone"].Value = numtlphnprsntb.Text;
                                selectedRow.Cells["Adresse"].Value = Adrsprsntb.Text;


                                // Clear the text boxes
                                nomprsntb.Text = "";
                                numtlphnprsntb.Text = "";
                                Adrsprsntb.Text = "";

                            }
                            else
                            {
                                MessageBox.Show("La mise à jour a échoué.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Client introuvable.");
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
                MessageBox.Show("Sélectionnez un client à éditer.");
            }

        }

        private void retourbtn_Click(object sender, EventArgs e)
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

        private void gestiondespersonnes_Load(object sender, EventArgs e)
        {

        }
    }
}
