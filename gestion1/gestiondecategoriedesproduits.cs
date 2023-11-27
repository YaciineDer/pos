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

namespace gestion1
{
    public partial class gestiondecategoriedesproduits : Form
    {
        public gestiondecategoriedesproduits()
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
                String Myquery = "select * from tablecategorie ";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CategorieGV.DataSource = ds.Tables[0];
                CategorieGV.Columns["Id"].Visible = false;
                CategorieGV.Columns["NomCategorie"].HeaderText = "Nom du Categorie";
               
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
        private void nomclientaddtb_TextChanged(object sender, EventArgs e)
        {

        }

        private void addctgrbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nomctgrtb.Text))
                {
                    MessageBox.Show("Veuillez remplir le champ.");
                    return;
                }

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                // Check if the category name already exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM tablecategorie WHERE NomCategorie = @NomCategorie", connection);
                checkCmd.Parameters.AddWithValue("@NomCategorie", nomctgrtb.Text);
                int existingCount = (int)checkCmd.ExecuteScalar();

                if (existingCount > 0)
                {
                    MessageBox.Show("Le nom de la catégorie existe déjà.");
                    // Clear the text box
                    nomctgrtb.Text = "";
                }
                else
                {
                    // Insert the category record
                    SqlCommand insertCmd = new SqlCommand("INSERT INTO tablecategorie (Nomcategorie) VALUES (@NomCategorie); SELECT SCOPE_IDENTITY();", connection);
                    insertCmd.Parameters.AddWithValue("@NomCategorie", nomctgrtb.Text);

                    int newCategoryId = Convert.ToInt32(insertCmd.ExecuteScalar());
                    MessageBox.Show("Catégorie ajoutée avec succès.");

                    // Add the new category to the DataGridView directly
                    DataTable dt = (DataTable)CategorieGV.DataSource;
                    DataRow newRow = dt.NewRow();
                    newRow["Id"] = newCategoryId;
                    newRow["NomCategorie"] = nomctgrtb.Text;
                    dt.Rows.Add(newRow);

                    // Clear the text box
                    nomctgrtb.Text = "";
                }
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

        private void deletectgrbtn_Click(object sender, EventArgs e)
        {
            if (CategorieGV.SelectedRows.Count > 0)
            {
                int selectedRowIndex = CategorieGV.SelectedRows[0].Index;
                int selectedCategoryId = Convert.ToInt32(CategorieGV.SelectedRows[0].Cells["Id"].Value);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM tablecategorie WHERE Id = @Id", connection);
                    deleteCmd.Parameters.AddWithValue("@Id", selectedCategoryId);
                    int rowsAffected = deleteCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Catégorie supprimée avec succès.");

                        // Remove the deleted row from the DataGridView
                        CategorieGV.Rows.RemoveAt(selectedRowIndex);

                        // Clear the text boxes
                        nomctgrtb.Text = "";
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
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Sélectionnez une Catégorie à supprimer.");
            }

        }

        private void Editcatgrbtn_Click(object sender, EventArgs e)
        {
            if (CategorieGV.SelectedRows.Count > 0)
            {
                int selectedCategoryId = Convert.ToInt32(CategorieGV.SelectedRows[0].Cells["Id"].Value);

                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""" + DatabaseConfiguration.databaseConnection + @""";Integrated Security=True;Connect Timeout=30"))
                    {
                        con.Open();

                        // Retrieve the selected category's data from the database
                        SqlCommand selectCmd = new SqlCommand("SELECT * FROM tablecategorie WHERE Id = @Id", con);
                        selectCmd.Parameters.AddWithValue("@Id", selectedCategoryId);
                        SqlDataReader reader = selectCmd.ExecuteReader();

                        if (reader.Read())
                        {
                            // Close the reader
                            reader.Close();

                            // Update the record with edited values
                            SqlCommand updateCmd = new SqlCommand("UPDATE tablecategorie SET NomCategorie = @NomCategorie WHERE Id = @Id", con);
                            updateCmd.Parameters.AddWithValue("@NomCategorie", nomctgrtb.Text);
                            updateCmd.Parameters.AddWithValue("@Id", selectedCategoryId);
                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Catégorie mise à jour avec succès.");

                                // Update the corresponding row in the DataGridView
                                DataGridViewRow selectedRow = CategorieGV.SelectedRows[0];
                                selectedRow.Cells["NomCategorie"].Value = nomctgrtb.Text;

                                // Clear the text box
                                nomctgrtb.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("La mise à jour a échoué.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Catégorie introuvable.");
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
                MessageBox.Show("Sélectionnez une catégorie à éditer.");
            }

        }

        private void CategorieGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CategorieGV.SelectedRows.Count > 0)
            {
                nomctgrtb.Text = CategorieGV.SelectedRows[0].Cells["NomCategorie"].Value.ToString();
                
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

    }
}
