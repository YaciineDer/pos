using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gestion1
{
    public partial class VoirCmnd : Form
    {
        public VoirCmnd()
        {
            InitializeComponent();
            InitializePrintDocument();
            populateCmnd();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""" + DatabaseConfiguration.databaseConnection + @""";Integrated Security=True;Connect Timeout=30");

        void populateCmnd()
        {
            try
            {
                connection.Open();
                String Myquery = "select * from OrderDetails ";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ViewOrdrGV.DataSource = ds.Tables[0];
                ViewOrdrGV.Columns["Id"].Visible = false;
                ViewOrdrGV.Columns["OrderId"].HeaderText = "Commandes";
                ViewOrdrGV.Columns["ProductId"].Visible = false;
                ViewOrdrGV.Columns["ProductName"].HeaderText = "Produits";
                ViewOrdrGV.Columns["Quantity"].HeaderText = "Quantite";
                ViewOrdrGV.Columns["Subtotal"].HeaderText = "Total";
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


        private void ordrGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Get the selected order details from the clicked cell
                DataGridViewCell orderIdCell = ViewOrdrGV.Rows[e.RowIndex].Cells["OrderId"];

                if (orderIdCell.Value != null && int.TryParse(orderIdCell.Value.ToString(), out int desiredOrderId))
                {
                    // Create a StringBuilder to accumulate the content for the printout
                    StringBuilder contentToPrint = new StringBuilder();

                    // Loop through the DataGridView and accumulate content for the desired OrderId
                    foreach (DataGridViewRow row in ViewOrdrGV.Rows)
                    {
                        // Check if the "OrderId" in the current row matches the desired OrderId
                        if (row.Cells["OrderId"].Value != null && int.TryParse(row.Cells["OrderId"].Value.ToString(), out int orderId))
                        {
                            if (orderId == desiredOrderId)
                            {
                                // Get the data you want to print from the DataGridView
                                string productName = row.Cells["ProductName"].Value?.ToString(); // Use ?. to handle null
                                string quantity = row.Cells["Quantity"].Value?.ToString(); // Use ?. to handle null
                                string subtotal = row.Cells["Subtotal"].Value?.ToString(); // Use ?. to handle null

                                // Append the data to the content
                                contentToPrint.AppendLine("Product Name: " + productName);
                                contentToPrint.AppendLine("Quantity: " + quantity);
                                contentToPrint.AppendLine("Subtotal: " + subtotal);
                                contentToPrint.AppendLine(); // Add a blank line between products
                            }
                        }
                    }

                    // Create a PrintDocument and show a print preview for the accumulated content
                    PrintDocument printDoc = new PrintDocument();
                    printDoc.PrintPage += (s, ev) =>
                    {
                        // Create a Graphics object to draw on the page
                        Graphics g = ev.Graphics;

                        // Define the font and brush for text
                        Font font = new Font("Arial", 12);
                        SolidBrush brush = new SolidBrush(Color.Black);

                        // Define the starting position for printing
                        float x = 50;
                        float y = 50;

                        // Print the accumulated content
                        g.DrawString(contentToPrint.ToString(), font, brush, x, y);
                    };

                    // Create a PrintPreviewDialog and display the preview
                    PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                    printPreviewDialog.Document = printDoc; // Set the document to be previewed
                    printPreviewDialog.ShowDialog(); // Show the preview dialog
                }
            }
        }



        private void InitializePrintDocument()
        {
            printDocument1 = new PrintDocument();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
        }





        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Create a Graphics object to draw on the page
            Graphics g = e.Graphics;

            // Define the font and brush for text
            Font titleFont = new Font("Arial", 18, FontStyle.Bold);
            Font headerFont = new Font("Arial", 12, FontStyle.Bold);
            Font cellFont = new Font("Arial", 12);
            SolidBrush brush = new SolidBrush(Color.Black);

            // Define the starting position for printing
            float x = 50;
            float y = 50;

            // Define the OrderId to filter (Replace with the desired OrderId)
            int desiredOrderId = 110;

            // Create a DataTable to represent the table structure
            DataTable table = new DataTable();
            table.Columns.Add("Produit", typeof(string));
            table.Columns.Add("Quantité", typeof(int));
            table.Columns.Add("Prix unitaire", typeof(decimal));
            table.Columns.Add("Sous-total", typeof(decimal));

            // Loop through the DataGridView and add rows to the DataTable for the desired OrderId
            decimal orderTotal = 0; // To calculate the total of the whole order
            foreach (DataGridViewRow row in ViewOrdrGV.Rows)
            {
                // Check if the "OrderId" in the current row matches the desired OrderId
                if (row.Cells["OrderId"].Value != null && int.TryParse(row.Cells["OrderId"].Value.ToString(), out int orderId))
                {
                    if (orderId == desiredOrderId)
                    {
                        // Get the data you want to print from the DataGridView
                        string productName = row.Cells["ProductName"].Value?.ToString() ?? "";
                        int quantity = int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int parsedQuantity) ? parsedQuantity : 0;
                        decimal price = decimal.TryParse(row.Cells["Price"].Value?.ToString(), out decimal parsedPrice) ? parsedPrice : 0.00M;
                        decimal subtotal = decimal.TryParse(row.Cells["Subtotal"].Value?.ToString(), out decimal parsedSubtotal) ? parsedSubtotal : 0.00M;

                        // Calculate the total price for this product
                        orderTotal += subtotal; // Accumulate the total for the whole order

                        // Add a row to the DataTable
                        table.Rows.Add(productName, quantity, price, subtotal);
                    }
                }
            }

            // Add the title "Bon de Commande" at the top-middle of the page
            string title = "Bon de Commande";
            SizeF titleSize = g.MeasureString(title, titleFont);
            float titleX = (e.PageBounds.Width - titleSize.Width) / 2;
            g.DrawString(title, titleFont, brush, titleX, y);
            y += titleSize.Height + 20; // Add spacing after the title

            // Convert the DataTable to a printable table
            float tableHeight = PrintDataTable(g, table, headerFont, cellFont, brush, x, y);

            // Add a line break before the order total
            tableHeight += 20;

            // Add the order total as a summary
            g.DrawString("Total de la Commande: " + orderTotal.ToString("C"), headerFont, brush, x, y + tableHeight);

        }

        // Function to print a DataTable as a table
        private float PrintDataTable(Graphics g, DataTable table, Font headerFont, Font cellFont, SolidBrush brush, float x, float y)
        {
            float rowHeight = Math.Max(headerFont.Height, cellFont.Height);
            float col1Width = g.MeasureString(table.Columns[0].ColumnName, headerFont).Width + 10; // Add some padding
            float col2Width = g.MeasureString(table.Columns[1].ColumnName, headerFont).Width + 10;
            float col3Width = g.MeasureString(table.Columns[2].ColumnName, headerFont).Width + 10;
            float col4Width = g.MeasureString(table.Columns[3].ColumnName, headerFont).Width + 10;

            // Draw headers
            g.DrawString(table.Columns[0].ColumnName, headerFont, brush, x, y);
            g.DrawString(table.Columns[1].ColumnName, headerFont, brush, x + col1Width, y);
            g.DrawString(table.Columns[2].ColumnName, headerFont, brush, x + col1Width + col2Width, y);
            g.DrawString(table.Columns[3].ColumnName, headerFont, brush, x + col1Width + col2Width + col3Width, y);

            y += rowHeight;

            // Draw table rows
            foreach (DataRow row in table.Rows)
            {
                g.DrawString(row[0].ToString(), cellFont, brush, x, y);
                g.DrawString(row[1].ToString(), cellFont, brush, x + col1Width, y);
               // g.DrawString(row[2].ToString("C"), cellFont, brush, x + col1Width + col2Width, y);
               // g.DrawString(row[3].ToString("C"), cellFont, brush, x + col1Width + col2Width + col3Width, y);
                y += rowHeight;
            }

            return y;
        }

        private void VoirCmnd_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form existingForm = Application.OpenForms.OfType<ManageOrders>().FirstOrDefault();

            if (existingForm != null)
            {
                existingForm.Show();
                this.Close();
            }
            else
            {
                ManageOrders gestionordr = new ManageOrders();
                gestionordr.Show();
                this.Close();
            }
        }

    }

}
