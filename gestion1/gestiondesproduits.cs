using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace gestion1
{
    public partial class gestiondesproduits : Form
    {
        public gestiondesproduits()
        {
            InitializeComponent();
            fillCategory();
            fillCategory1();
            // Add this line in your constructor after InitializeComponent();
            ProduitGV.CellFormatting += ProduitGV_CellFormatting;

            populate();


        }


        SqlConnection connection = new SqlConnection($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={DatabaseConfiguration.databaseConnection};Integrated Security=True;Connect Timeout=30");



        private void ProduitGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Check for valid row and column indices
            {
                DataGridViewRow row = ProduitGV.Rows[e.RowIndex];
                DataGridViewCell cell = row.Cells["Quantite"]; // Replace "Quantite" with your actual column name

                if (cell != null && cell.Value != null)
                {
                    // Get the Quantite value from the cell
                    if (int.TryParse(cell.Value.ToString(), out int quantite))
                    {
                        // Check the value of Quantite and set the row's background color accordingly
                        if (quantite <= 5 && quantite > 0)
                        {
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else if (quantite == 0)
                        {
                            row.DefaultCellStyle.BackColor = Color.Red;
                        }
                        else
                        {
                            // If neither condition is met, set the default row background color
                            row.DefaultCellStyle.BackColor = ProduitGV.DefaultCellStyle.BackColor;
                        }
                    }
                }
            }
        }

        private void PerformSearch(string searchQuery)
        {
            try
            {
                connection.Open();

                // Perform the search operation in your database based on the user's input
                SqlCommand searchCmd = new SqlCommand("SELECT * FROM TableProduit WHERE Nomproduit LIKE @SearchQuery", connection);
                searchCmd.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");

                DataTable dt = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(searchCmd))
                {
                    adapter.Fill(dt);
                }

                ProduitGV.DataSource = dt; // Update the DataGridView in real-time with the search results

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during the search: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        void populate()
        {
            try
            {
                connection.Open();
                String Myquery = "select * from TableProduit ";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProduitGV.DataSource = ds.Tables[0];
                ProduitGV.Columns["Id"].Visible = false;
                ProduitGV.Columns["Nomproduit"].HeaderText = "Nom Produit";
                ProduitGV.Columns["Categorie"].HeaderText = "Categorie";
                ProduitGV.Columns["prixachat"].HeaderText = "Prix D'achat";
                ProduitGV.Columns["prixvente"].HeaderText = "Prix de Vente";
                ProduitGV.Columns["fournisseur"].HeaderText = "fournisseur";
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
        void fillCategory()
        {
            String query = "select * from tablecategorie";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader;
            try
            {
                connection.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("NomCategorie", typeof(String));
                reader = cmd.ExecuteReader();
                dt.Load(reader);
                catecmbx.ValueMember = "NomCategorie";
                catecmbx.DataSource = dt;
                connection.Close();

            }
            catch (Exception ex) { }
        }

        void fillCategory1()
        {
            String query = "select * from tablefournisseurs";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader;
            try
            {
                connection.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("Nom", typeof(String));
                reader = cmd.ExecuteReader();
                dt.Load(reader);
                Frnsrcmbx.ValueMember = "Nom";
                Frnsrcmbx.DataSource = dt;
                connection.Close();

            }
            catch (Exception ex) { }
        }
        private void FormatNumericInput(Bunifu.UI.WinForms.BunifuTextBox textBox)
        {
            // Store the current cursor position
            int cursorPosition = textBox.SelectionStart;

            // Remove non-numeric characters and spaces from the current text
            string numericInput = Regex.Replace(textBox.Text, "[^0-9]", "");

            // Check if the numeric input is not empty
            if (!string.IsNullOrEmpty(numericInput))
            {
                // Convert the numeric input to a long (int64)
                int numericValue;
                if (int.TryParse(numericInput, out numericValue))
                {
                    // Format the numeric value with a space as a thousands separator
                    string formattedValue = numericValue.ToString("N0");

                    // Update the textbox with the formatted value
                    textBox.Text = formattedValue;

                    // Set the cursor position to the correct location
                    textBox.SelectionStart = cursorPosition + (formattedValue.Length - numericInput.Length);
                }
                else
                {
                    // Display an error message if the numeric input is too large for a long
                    MessageBox.Show("Enterer un nombre logic .");

                    // Optionally, you can clear the textbox or take other actions as needed
                }
            }
            else
            {
                // If the input is empty, set the text to an empty string
                textBox.Text = string.Empty;
            }
        }





        private void Qntttb_TextChanged(object sender, EventArgs e)
        {
            // Store the current cursor position
            int cursorPosition = Qntttb.SelectionStart;

            // Remove non-numeric characters from the current text
            string numericInput = Regex.Replace(Qntttb.Text, "[^0-9]", "");

            // Update the textbox with the numeric input
            Qntttb.Text = numericInput;

            // Set the cursor position to the correct location
            Qntttb.SelectionStart = cursorPosition;
        }


        private void prixventetb_TextChanged(object sender, EventArgs e)
        {
            FormatNumericInput(prixventetb);
        }

        private void prixachattb_TextChanged_1(object sender, EventArgs e)
        {
            FormatNumericInput(prixachattb);
        }


        private void addprdbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nomprdtb.Text) || string.IsNullOrWhiteSpace(catecmbx.Text) || string.IsNullOrWhiteSpace(Qntttb.Text) || string.IsNullOrWhiteSpace(prixachattb.Text) || string.IsNullOrWhiteSpace(prixventetb.Text) || string.IsNullOrWhiteSpace(Frnsrcmbx.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs.");
                    return;
                }

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                // Check if the product name already exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM TableProduit WHERE Nomproduit = @Nomproduit", connection);
                checkCmd.Parameters.AddWithValue("@Nomproduit", nomprdtb.Text);
                int existingCount = (int)checkCmd.ExecuteScalar();

                if (existingCount > 0)
                {
                    MessageBox.Show("Ce produit existe déjà.");
                    // Clear the text boxes
                    nomprdtb.Text = "";
                    catecmbx.Text = "";
                    Qntttb.Text = "";
                    prixachattb.Text = "";
                    prixventetb.Text = "";
                    Frnsrcmbx.Text = "";
                }
                else
                {
                    // Insert the new product record
                    SqlCommand insertCmd = new SqlCommand("INSERT INTO TableProduit (Nomproduit, Categorie, Quantite, prixachat, prixvente, fournisseur) VALUES (@Nomproduit, @Categorie, @Quantite, @prixachat, @prixvente, @fournisseur); SELECT SCOPE_IDENTITY();", connection);
                    insertCmd.Parameters.AddWithValue("@Nomproduit", nomprdtb.Text);
                    insertCmd.Parameters.AddWithValue("@Categorie", catecmbx.Text);
                    insertCmd.Parameters.AddWithValue("@Quantite", Qntttb.Text);
                    insertCmd.Parameters.AddWithValue("@prixachat", prixachattb.Text);
                    insertCmd.Parameters.AddWithValue("@prixvente", prixventetb.Text);
                    insertCmd.Parameters.AddWithValue("@fournisseur", Frnsrcmbx.Text);

                    // Get the ID of the newly inserted product
                    int newProductId = Convert.ToInt32(insertCmd.ExecuteScalar());

                    if (newProductId > 0)
                    {
                        MessageBox.Show("Produit ajouté avec succès.");

                        // Clear the text boxes
                        nomprdtb.Text = "";
                        catecmbx.Text = "";
                        Qntttb.Text = "";
                        prixachattb.Text = "";
                        prixventetb.Text = "";
                        Frnsrcmbx.Text = "";

                        // Update the DataGridView with the latest data
                        DataTable dt = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TableProduit", connection))
                        {
                            adapter.Fill(dt);
                        }

                        ProduitGV.DataSource = dt; // Set the DataGridView data source to the updated DataTable
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de l'ajout du produit.");
                    }
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

        private void Frnsrcmbx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Qntttb_TextChanged_1(object sender, EventArgs e)
        {
            // Use a regular expression to remove non-numeric characters
            string numericInput = Regex.Replace(Qntttb.Text, "[^0-9]", "");

            // Update the textbox with the numeric input
            Qntttb.Text = numericInput;
        }

        private void Editprdbtn_Click(object sender, EventArgs e)
        {
            if (ProduitGV.SelectedRows.Count > 0)
            {
                int selectedProductId = Convert.ToInt32(ProduitGV.SelectedRows[0].Cells["Id"].Value);

                try
                {
                    SqlConnection con = new SqlConnection($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={DatabaseConfiguration.databaseConnection};Integrated Security=True;Connect Timeout=30");
                    {
                        con.Open();

                        // Retrieve the selected product's data from the database
                        SqlCommand selectCmd = new SqlCommand("SELECT * FROM TableProduit WHERE Id = @Id", con);
                        selectCmd.Parameters.AddWithValue("@Id", selectedProductId);
                        SqlDataReader reader = selectCmd.ExecuteReader();

                        if (reader.Read())
                        {
                            // Close the reader
                            reader.Close();

                            // Update the record with edited values
                            SqlCommand updateCmd = new SqlCommand("UPDATE TableProduit SET Nomproduit = @Nomproduit, Categorie = @Categorie, Quantite = @Quantite, prixachat = @prixachat, prixvente = @prixvente, fournisseur = @fournisseur WHERE Id = @Id", con);
                            updateCmd.Parameters.AddWithValue("@Nomproduit", nomprdtb.Text);
                            updateCmd.Parameters.AddWithValue("@Categorie", catecmbx.Text);
                            updateCmd.Parameters.AddWithValue("@Quantite", Convert.ToInt32(Qntttb.Text));
                            updateCmd.Parameters.AddWithValue("@prixachat", Convert.ToDecimal(prixachattb.Text));
                            updateCmd.Parameters.AddWithValue("@prixvente", Convert.ToDecimal(prixventetb.Text));
                            updateCmd.Parameters.AddWithValue("@fournisseur", Frnsrcmbx.Text);
                            updateCmd.Parameters.AddWithValue("@Id", selectedProductId);
                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Produit mis à jour avec succès.");

                                // Update the corresponding row in the DataGridView
                                DataGridViewRow selectedRow = ProduitGV.SelectedRows[0];
                                selectedRow.Cells["Nomproduit"].Value = nomprdtb.Text; // Update the product name
                                selectedRow.Cells["Categorie"].Value = catecmbx.Text;
                                selectedRow.Cells["Quantite"].Value = Convert.ToInt32(Qntttb.Text);
                                selectedRow.Cells["prixachat"].Value = Convert.ToDecimal(prixachattb.Text);
                                selectedRow.Cells["prixvente"].Value = Convert.ToDecimal(prixventetb.Text);
                                selectedRow.Cells["fournisseur"].Value = Frnsrcmbx.Text;

                                // Clear the text boxes
                                nomprdtb.Text = "";
                                catecmbx.Text = "";
                                Qntttb.Text = "";
                                prixachattb.Text = "";
                                prixventetb.Text = "";
                                Frnsrcmbx.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("La mise à jour a échoué.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Produit introuvable.");
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
                MessageBox.Show("Sélectionnez un produit à éditer.");
            }







        }

        private void deleteprdbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string productNameToDelete = nomprdtb.Text; // Get the product name to delete

                // Check if the product exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM TableProduit WHERE Nomproduit = @ProductName", connection);
                checkCmd.Parameters.AddWithValue("@ProductName", productNameToDelete);
                int existingCount = (int)checkCmd.ExecuteScalar();

                if (existingCount > 0)
                {
                    // Confirm deletion
                    DialogResult result = MessageBox.Show("Voulez-vous vraiment supprimer ce produit ?", "Confirmation", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        // Delete the product
                        SqlCommand deleteCmd = new SqlCommand("DELETE FROM TableProduit WHERE Nomproduit = @ProductName", connection);
                        deleteCmd.Parameters.AddWithValue("@ProductName", productNameToDelete);
                        int rowsAffected = deleteCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Produit supprimé avec succès.");

                            // Clear the input fields
                            nomprdtb.Text = "";
                            catecmbx.Text = "";
                            Qntttb.Text = "";
                            prixachattb.Text = "";
                            prixventetb.Text = "";
                            Frnsrcmbx.Text = "";

                            DataTable dt = new DataTable();
                            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TableProduit", connection))
                            {
                                adapter.Fill(dt);
                            }

                            ProduitGV.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la suppression du produit.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Produit introuvable.");
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

        private void ProduitGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ProduitGV.SelectedRows.Count > 0)
            {
                nomprdtb.Text = ProduitGV.SelectedRows[0].Cells["Nomproduit"].Value.ToString();
                catecmbx.Text = ProduitGV.SelectedRows[0].Cells["Categorie"].Value.ToString();
                Qntttb.Text = ProduitGV.SelectedRows[0].Cells["Quantite"].Value.ToString();
                prixachattb.Text = ProduitGV.SelectedRows[0].Cells["prixachat"].Value.ToString();
                prixventetb.Text = ProduitGV.SelectedRows[0].Cells["prixvente"].Value.ToString();

                // Assuming "Fournisseur" is the correct column name for Frnsrcmbx
                Frnsrcmbx.Text = ProduitGV.SelectedRows[0].Cells["fournisseur"].Value.ToString();
            }

        }



        private void searchprdttb_TextChanged(object sender, EventArgs e)
        {
            // Get the user's search query from the TextBox
            string searchQuery = searchprdttb.Text.Trim();

            // Call the search function
            PerformSearch(searchQuery);
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

        private void gestiondesproduits_Load(object sender, EventArgs e)
        {

        }
    }
}
