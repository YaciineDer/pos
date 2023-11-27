using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace gestion1
{
    public partial class ManageOrders : Form
    {
        private DataTable orderTable;
        public ManageOrders()
        {
            InitializeComponent();
            prdtOrdrGV.CellFormatting += prdtOrdrGV_CellFormatting;

            populateClient();
            populateProduit();
            // Initialize the order DataTable
            orderTable = new DataTable();
            orderTable.Columns.Add("NomProduit");
            orderTable.Columns.Add("Quantite");
            orderTable.Columns.Add("prixvente");
            orderTable.Columns.Add("Total");
            ordrGV.DataSource = orderTable;

        }
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + DatabaseConfiguration.databaseConnection + ";Integrated Security=True;Connect Timeout=30");

        private void prdtOrdrGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Check for valid row and column indices
            {
                DataGridViewRow row = prdtOrdrGV.Rows[e.RowIndex];
                DataGridViewCell cell = row.Cells["Quantite"]; // Replace "Quantite" with your actual column name

                if (cell != null && cell.Value != null)
                {
                    // Get the quantite (quantity) value from the cell
                    if (int.TryParse(cell.Value.ToString(), out int quantite))
                    {
                        // Check the value of quantite and set the row's background color accordingly
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
                            row.DefaultCellStyle.BackColor = prdtOrdrGV.DefaultCellStyle.BackColor;
                        }
                    }
                }
            }
        }




        void populateClient()
        {
            try
            {
                connection.Open();
                String Myquery = "SELECT Nom, Telephone, Adresse, Id FROM tableclient"; // Include "Id" in the query
                SqlDataAdapter da = new SqlDataAdapter(Myquery, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);

                ClientsordrGV.DataSource = ds.Tables[0];

                // Check if the "Id" column exists before modifying it
                if (ClientsordrGV.Columns.Contains("Id"))
                {
                    ClientsordrGV.Columns["Id"].Visible = false; // Hide the "Id" column
                }

                ClientsordrGV.Columns["Nom"].HeaderText = "Nom";
                ClientsordrGV.Columns["Telephone"].HeaderText = "Num Telephone";
                ClientsordrGV.Columns["Adresse"].HeaderText = "Adresse";
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

        void populateProduit()
        {
            try
            {
                connection.Open();
                String Myquery = "select * from TableProduit ";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                prdtOrdrGV.DataSource = ds.Tables[0];
                prdtOrdrGV.Columns["Id"].Visible = false;
                prdtOrdrGV.Columns["Nomproduit"].HeaderText = "Nom Produit";
                prdtOrdrGV.Columns["Categorie"].HeaderText = "Categorie";
                prdtOrdrGV.Columns["prixachat"].HeaderText = "Prix D'achat";
                prdtOrdrGV.Columns["prixvente"].HeaderText = "Prix de Vente";
                prdtOrdrGV.Columns["fournisseur"].HeaderText = "fournisseur";
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
        private void UpdateStockQuantity(string productName, int quantity)
        {
            try
            {
                // Update the stock quantity in the database based on the product name
                string updateQuery = "UPDATE TableProduit SET Quantite = Quantite - @Quantity WHERE Nomproduit = @NomProduit";

                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@NomProduit", productName);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de la mise à jour de la quantité en stock : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                prdtOrdrGV.DataSource = dt; // Update the DataGridView in real-time with the search results

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



        private void prdtOrdrGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        // Define a List to keep track of products in the order
        List<string> productsInOrder = new List<string>();
        decimal orderTotal = 0; // Variable to store the order total



        private void addordrbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Quantiteordrtb.Text))
                {
                    MessageBox.Show("Veuillez spécifier la quantité.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method if quantity is not specified
                }

                if (int.TryParse(Quantiteordrtb.Text, out int quantite) && quantite >= 0) // Check if quantity is greater than or equal to zero
                {
                    if (prdtOrdrGV.SelectedRows.Count > 0)
                    {
                        DataGridViewRow selectedRow = prdtOrdrGV.SelectedRows[0];
                        if (selectedRow.Cells["Nomproduit"].Value != null && selectedRow.Cells["prixvente"].Value != null)
                        {
                            string nomProduit = selectedRow.Cells["Nomproduit"].Value.ToString();
                            string prixVenteStr = selectedRow.Cells["prixvente"].Value.ToString();

                            if (decimal.TryParse(prixVenteStr, out decimal prixVente))
                            {
                                int stockQuantity = 0; // Initialize with a default value

                                // Initialize with a default value

                                // Query your database to get the stock quantity for the selected product
                                // Use the correct column name 'Quantite' here
                                string query = "SELECT Quantite FROM TableProduit WHERE Nomproduit = @NomProduit";
                                using (SqlCommand cmd = new SqlCommand(query, connection))
                                {
                                    cmd.Parameters.AddWithValue("@NomProduit", nomProduit);
                                    connection.Open();

                                    var result = cmd.ExecuteScalar(); // Get the result

                                    if (result != null) // Check if the result is not null
                                    {
                                        stockQuantity = int.Parse(result.ToString()); // Convert to integer
                                    }

                                    connection.Close();
                                }

                                if (quantite <= stockQuantity)
                                {
                                    int total = quantite * (int)prixVente;

                                    // Check if the product is already in the order
                                    if (!productsInOrder.Contains(nomProduit))
                                    {
                                        // Check if the quantity is greater than 0 before adding to the order
                                        if (quantite > 0)
                                        {
                                            // Add the product to the orderTable and the list of products in the order
                                            orderTable.Rows.Add(nomProduit, quantite, prixVente, total);
                                            productsInOrder.Add(nomProduit);
                                            orderTotal += total; // Update the order total

                                            // Update the stock quantity in the database
                                            UpdateStockQuantity(nomProduit, quantite);
                                        }
                                        else
                                        {
                                            MessageBox.Show("La quantité doit être un nombre entier supérieur à zéro.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        // Find the existing row in orderTable and update the quantity
                                        foreach (DataRow row in orderTable.Rows)
                                        {
                                            if (row["Nomproduit"].ToString() == nomProduit)
                                            {
                                                // Explicitly convert the values to integers for updating
                                                int currentQuantity = int.Parse(row["Quantite"].ToString());
                                                int currentTotal = int.Parse(row["Total"].ToString());

                                                // Check if the current quantity is greater than 0 before adding
                                                if (currentQuantity > 0)
                                                {
                                                    row["Quantite"] = (currentQuantity + quantite).ToString();
                                                    row["Total"] = (currentTotal + total).ToString();
                                                    orderTotal += total; // Update the order total
                                                                         // Update the stock quantity in the database
                                                    UpdateStockQuantity(nomProduit, quantite);
                                                }
                                                else
                                                {
                                                    MessageBox.Show("La quantité en stock est épuisée.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    return; // Exit the method to prevent adding the order
                                                }

                                                break;
                                            }
                                        }
                                    }

                                    // Update the quantity in the prdtOrdrGV DataGridView
                                    foreach (DataGridViewRow row in prdtOrdrGV.Rows)
                                    {
                                        string productName = row.Cells["Nomproduit"].Value.ToString();
                                        if (productName == nomProduit)
                                        {
                                            // Explicitly convert the value to an integer for updating
                                            int currentQuantity = int.Parse(row.Cells["Quantite"].Value.ToString());

                                            // Check if the current quantity is greater than 0 before subtracting
                                            if (currentQuantity > 0)
                                            {
                                                // Check if the quantity to be subtracted is not greater than the current stock
                                                if (quantite <= currentQuantity)
                                                {
                                                    row.Cells["Quantite"].Value = (currentQuantity - quantite).ToString();
                                                }
                                                else
                                                {
                                                    MessageBox.Show("La quantité demandée dépasse la quantité en stock.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    return; // Exit the method to prevent adding the order
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("La quantité en stock est épuisée.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                return; // Exit the method to prevent adding the order
                                            }

                                            break;
                                        }
                                    }

                                    Quantiteordrtb.Text = string.Empty; // Clear the quantity TextBox

                                    // Update the TotalLabel with the order total
                                    totallabel.Text = "Total: " + orderTotal.ToString("0.00"); // Assumes you want a currency format
                                }
                                else
                                {
                                    MessageBox.Show("La quantité demandée n'est pas disponible en stock.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        
                            else
                            {
                                MessageBox.Show("Prix de Vente invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Les données du produit sélectionné sont manquantes.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Veuillez sélectionner un produit.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("La quantité doit être un nombre entier supérieur ou égal à zéro.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void ordrGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void searchprdttb_TextChanged(object sender, EventArgs e)
        {
            // Get the user's search query from the TextBox
            string searchQuery = searchprdttb.Text.Trim();

            // Call the search function to update the DataGridView
            PerformSearch(searchQuery);
        }

        private void ClientsordrGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ClientsordrGV.SelectedRows.Count > 0)
            {
                nomclientordrtb.Text = ClientsordrGV.SelectedRows[0].Cells["Nom"].Value.ToString();
                numtlphnordrtb.Text = ClientsordrGV.SelectedRows[0].Cells["Telephone"].Value.ToString();
            }
        }

        private void cnfrmordrsbtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if there are products in the order
                if (orderTable.Rows.Count == 0)
                {
                    MessageBox.Show("Aucun produit dans la commande.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the client name and phone number from the textboxes
                string clientName = nomclientordrtb.Text.Trim();
                string clientPhoneNumber = numtlphnordrtb.Text.Trim();

                // Validate the order total
                if (orderTotal <= 0)
                {
                    MessageBox.Show("Le montant total de la commande doit être supérieur à zéro.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Calculate the total amount for the order
                int orderTotalAmount = (int)orderTotal;

                // Get the current system date and time
                DateTime currentDate = DateTime.Now;

                // Use the existing SqlConnection
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                // Insert the order into the TableCmnd table
                string insertOrderQuery = "INSERT INTO TableCmnd (ClientNom, ClientPhoneNumber, Dateordr, totalamnt) VALUES (@ClientNom, @ClientPhoneNumber, @CurrentDate, @TotalAmount); SELECT SCOPE_IDENTITY();";
                int orderId;
                using (SqlCommand cmd = new SqlCommand(insertOrderQuery, connection))
                {
                    // Use DBNull.Value to handle null values for ClientNom and ClientPhoneNumber
                    cmd.Parameters.AddWithValue("@ClientNom", string.IsNullOrEmpty(clientName) ? DBNull.Value : (object)clientName);
                    cmd.Parameters.AddWithValue("@ClientPhoneNumber", string.IsNullOrEmpty(clientPhoneNumber) ? DBNull.Value : (object)clientPhoneNumber);
                    cmd.Parameters.AddWithValue("@CurrentDate", currentDate);
                    cmd.Parameters.AddWithValue("@TotalAmount", orderTotalAmount);

                    // Execute the query and retrieve the newly inserted OrderId
                    orderId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Insert the order details into the OrderDetails table
                foreach (DataRow row in orderTable.Rows)
                {
                    string productName = row["NomProduit"].ToString();
                    int quantity = Convert.ToInt32(row["Quantite"]);
                    decimal subtotal = Convert.ToDecimal(row["Total"]);

                    // Query the ProductId based on the product name
                    string productIdQuery = "SELECT Id FROM TableProduit WHERE Nomproduit = @NomProduit";
                    int productId; // Declare an integer variable to store the ProductId
                    using (SqlCommand cmd = new SqlCommand(productIdQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@NomProduit", productName);
                        productId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Insert the order detail using the same orderId and the actual product name
                    string insertOrderDetailQuery = "INSERT INTO OrderDetails (OrderId, ProductId, ProductName, Quantity, Subtotal) VALUES (@OrderId, @ProductId, @ProductName, @Quantity, @Subtotal)";
                    using (SqlCommand cmd = new SqlCommand(insertOrderDetailQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        cmd.Parameters.AddWithValue("@ProductId", productId); // Use the retrieved ProductId
                        cmd.Parameters.AddWithValue("@ProductName", productName); // Add the product name
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Subtotal", subtotal);

                        cmd.ExecuteNonQuery();
                    }
                }

                // Clear the orderTable, productsInOrder list, and orderTotal
                orderTable.Rows.Clear();
                productsInOrder.Clear();
                orderTotal = 0;

                // Update the TotalLabel with the new order total (should be 0 now)
                totallabel.Text = "Total: 0.00"; // Assumes you want a currency format

                // Clear the client name and phone number textboxes
                nomclientordrtb.Text = "";
                numtlphnordrtb.Text = "";

                // Show a confirmation message
                MessageBox.Show("Commande confirmée avec succès.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Log the exception details (consider using a logging framework)
                Console.WriteLine("Exception: " + ex.Message);

                MessageBox.Show("Une erreur s'est produite lors de la confirmation de la commande : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure that the connection is closed when done
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void retourbtn_Click(object sender, EventArgs e)
        {
            Form existingForm = Application.OpenForms.OfType<interfaceprin>().FirstOrDefault();

            if (existingForm != null)
            {
                existingForm.Show();
                this.Close();
            }
            else
            {
                interfaceprin intrface = new interfaceprin();
                intrface.Show();
                this.Close();
            }
        }



        private void viewordrsbtn_Click(object sender, EventArgs e)
        {
            // Close all other instances of the VoirCmnd form
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(VoirCmnd) && form != this)
                {
                    form.Close();
                }
            }

            // Show the VoirCmnd form
            VoirCmnd ordrsview = new VoirCmnd();
            ordrsview.Show();
            this.Hide();
        }



    }

}

