using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Best_Brightness.UserPages
{
    public partial class cart : System.Web.UI.Page
    {
        string itemName = string.Empty;
        string itemID = string.Empty;
     
        GridViewRow selectedRow;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindCartData();
            CalculateTotalCost();
        }
        private void BindCartData()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True"))
            {
                string userID = (string)Session["UserID"];
                con.Open();
                string query = "SELECT ItemID, ItemName, Quantity, SellingPrice, TotalCost FROM myCartTB WHERE UserID = @ID";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@ID", userID);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        GridView1.DataSource = dataTable;
                        GridView1.DataBind();
                        
                    }
                }
            }
            CalculateTotalCost();
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            selectedRow = GridView1.SelectedRow;

            if (selectedRow != null)
            {

                itemName = selectedRow.Cells[2].Text;
              

                lblSelectedItemName.Text = "Selected Item Name: " + itemName;
            }
        }
        protected void btnADD_cliked(object sender, EventArgs e)
        {
            selectedRow = GridView1.SelectedRow;
            this.itemID = selectedRow.Cells[1].Text;

            if (selectedRow != null)
            {
                try
                {
                    int itemID = int.Parse(selectedRow.Cells[1].Text);
                    int quantityChange = 1;
                    using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True"))
                    {
                        con.Open();
                        string updateQuery = "UPDATE myCartTB SET Quantity = Quantity + @QuantityChange, TotalCost = Quantity * SellingPrice WHERE ItemID = @ItemID";
                        using (SqlCommand command = new SqlCommand(updateQuery, con))
                        {
                            command.Parameters.AddWithValue("@QuantityChange", quantityChange);
                            command.Parameters.AddWithValue("@ItemID", itemID);
                            int dt = command.ExecuteNonQuery();
                            if (dt > 0)
                            {
                                BindCartData();
                               
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string script = "alert('Error: " + ex.Message + "');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error: Select a row');", true);
            }
            CalculateTotalCost();
        }

        protected void btnLESS_cliked(object sender, EventArgs e)
        {
            selectedRow = GridView1.SelectedRow;
            this.itemID = selectedRow.Cells[1].Text;
            if (selectedRow != null)
            {
                try
                {
                    int quantityChange = -1;
                    using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True"))
                    {
                        con.Open();
                        string updateQuery = "UPDATE myCartTB SET Quantity = Quantity + @QuantityChange, TotalCost = Quantity * SellingPrice WHERE ItemID = @ItemID";
                        using (SqlCommand command = new SqlCommand(updateQuery, con))
                        {
                            command.Parameters.AddWithValue("@QuantityChange", quantityChange);
                            command.Parameters.AddWithValue("@ItemID", itemID);
                            int dt = command.ExecuteNonQuery();
                            if (dt > 0)
                            {
                                BindCartData();
                               
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string script = "alert('Error: " + ex.Message + "');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                }
            }
            CalculateTotalCost();
        }


        protected void btnDELETE_cliked(object sender, EventArgs e)
        {
            selectedRow = GridView1.SelectedRow;
            this.itemID = selectedRow.Cells[1].Text;

            if (selectedRow != null)
            {
                try
                {


                    using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True"))
                    {
                        con.Open();
                        string updateQuery = "DELETE FROM myCartTB WHERE ItemID = @ItemID";
                        using (SqlCommand command = new SqlCommand(updateQuery, con))
                        {
                            command.Parameters.AddWithValue("@ItemID", itemID);
                            int dt = command.ExecuteNonQuery();
                            if (dt > 0)
                            {
                                string script1 = "alert('Error: ITEM REMOVED');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script1, true);
                                BindCartData();
                               
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string script = "alert('Error: " + ex.Message + "');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                }
            }
            CalculateTotalCost();
        }

        private void CalculateTotalCost()
        {


            decimal totalCost = 0;

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    TableCell totalCostCell = row.Cells[row.Cells.Count - 1];

                    decimal itemTotalCost;

                    if (decimal.TryParse(totalCostCell.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out itemTotalCost))
                    {
                        totalCost += itemTotalCost;
                    }
                }
            }
            txtTOTAL.Text = totalCost.ToString("C");
        }

        protected void btnPAY_Click(object sender, EventArgs e)
        {

            ;
            decimal total = decimal.Parse(txtTOTAL.Text, NumberStyles.Currency);
            decimal amountPaid = decimal.Parse(txtPay.Text.Trim('R').Replace(",", ""), NumberStyles.Currency);
            decimal change = amountPaid - total;

            if (change < 0)
            {

                string script4 = "alert('Error: Insufficient payment. Please enter a valid amount.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script4, true);
                return;
            }


            txtChange.Text = change.ToString("C");

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string userID = (string)Session["UserID"];
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string itemID = row.Cells[1].Text;
                        string itemName = row.Cells[2].Text;
                        string sellingPrice = row.Cells[3].Text;
                        string quantity = row.Cells[4].Text;
                        string totalCost = row.Cells[5].Text;


                        string insertQuery = "INSERT INTO stockBoughtTB (ItemID,ItemName, SellingPrice, Quantity, TotalCost, UserID) VALUES (@ID,@ItemName, @SellingPrice, @Quantity, @TotalCost,@user)";
                        try
                        {
                            using (SqlCommand command = new SqlCommand(insertQuery, con))
                            {
                                command.Parameters.AddWithValue("@ID", itemID);
                                command.Parameters.AddWithValue("@ItemName", itemName);
                                command.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@TotalCost", totalCost);
                                command.Parameters.AddWithValue("@user", userID);

                                command.ExecuteNonQuery();

                            }

                        }
                        catch (Exception ex)
                        {
                            string script2 = "alert('Error: " + ex.Message + "');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script2, true);
                        }
                    }

                }
            }


            string id =(string)Session["UserID"];
            DeleteCartItemsForUser(id);

            UpdateStockTable();

            GridView1.DataSource = null;
            GridView1.DataBind();
            txtTOTAL.Text = "";

            string script = "alert('Stock Purchased successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
        }
        private void UpdateStockTable()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string ItemName = row.Cells[2].Text;
                        string quantity = row.Cells[3].Text;



                        string updateQuery = "UPDATE stockTB SET Quantity = Quantity - @Quantity WHERE ItemName = @ItemName";

                        using (SqlCommand command = new SqlCommand(updateQuery, con))
                        {
                            command.Parameters.AddWithValue("@Quantity", quantity);
                            command.Parameters.AddWithValue("@ItemName", ItemName);
                            command.ExecuteNonQuery();
                        }
                    }

                }
            }
        }

        private void DeleteCartItemsForUser(string userID)
        {
            try
            {
                
                using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True"))
                {
                    con.Open();
                    string deleteQuery = "DELETE FROM myCartTB WHERE UserID = @UserID";
                    using (SqlCommand command = new SqlCommand(deleteQuery, con))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                string script = "alert('Error: " + ex.Message + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
            }

        }
    }
}