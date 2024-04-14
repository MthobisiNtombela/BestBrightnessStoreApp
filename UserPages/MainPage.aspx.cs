using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Best_Brightness.UserPages
{
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindItems();
            }
        }

        private void BindItems()
        {
            DataTable itemTable = GetItemData();

            if (itemTable != null)
            {
                rptItems.DataSource = itemTable;
                rptItems.DataBind();
            }
        }

        private DataTable GetItemData()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True"))
            {
                con.Open();
                string query = "SELECT ItemName, ItemUrl,SellingPrice FROM stockTB";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }

        }

        protected void btnCart_Click(object sender, EventArgs e)
        {
            Button btnCart = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnCart.NamingContainer;
            string itemName = ((Literal)item.FindControl("litItemName")).Text;

            string itemPrice = ((Literal)item.FindControl("litItemPrice")).Text;
            itemPrice = itemPrice.Replace("R", "").Replace(",", "");
            decimal sellingPrice = decimal.Parse(itemPrice) / 100;
            string userID = (string)Session["UserID"];
            bool itemExists = false;
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True"))
            {
                con.Open();
                string selectQuery = "SELECT Quantity, TotalCost, SellingPrice FROM myCartTB WHERE ItemName = @ItemName AND UserID = @ID";
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, con))
                {
                    selectCommand.Parameters.AddWithValue("@ItemName", itemName);
                    selectCommand.Parameters.AddWithValue("@ID", userID); 
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                           
                            int currentQuantity = reader.GetInt32(0);
                            currentQuantity++;

                          
                            decimal currentTotalCost = reader.GetDecimal(1);
                            decimal sellingPriceFromDB = reader.GetDecimal(2);
                            decimal newTotalCost = currentQuantity * sellingPriceFromDB;

                            
                            reader.Close();

                            
                            string updateQuery = "UPDATE myCartTB SET Quantity = @Quantity, TotalCost = @TotalCost WHERE ItemName = @ItemName AND UserID = @ID";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                            {
                                updateCommand.Parameters.AddWithValue("@Quantity", currentQuantity);
                                updateCommand.Parameters.AddWithValue("@TotalCost", newTotalCost);
                                updateCommand.Parameters.AddWithValue("@ItemName", itemName);
                                updateCommand.Parameters.AddWithValue("@ID", userID);
                                int updatedRows = updateCommand.ExecuteNonQuery();

                                if (updatedRows > 0)
                                {
                                    itemExists = true;
                                }
                            }
                        }
                    }
                }

                if (!itemExists)
                {
                    
                    int quantity = 1;
                    decimal totalCost = quantity * sellingPrice; 
                    string insertQuery = "INSERT INTO myCartTB (ItemName, SellingPrice, UserID, Quantity, TotalCost) VALUES (@ItemName, @SellingPrice, @ID, @Quantity, @TotalCost)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, con))
                    {
                        insertCommand.Parameters.AddWithValue("@ItemName", itemName);
                        insertCommand.Parameters.AddWithValue("@SellingPrice", sellingPrice); 
                        insertCommand.Parameters.AddWithValue("@ID", userID); 
                        insertCommand.Parameters.AddWithValue("@Quantity", quantity);
                        insertCommand.Parameters.AddWithValue("@TotalCost", totalCost);
                        int insertedRows = insertCommand.ExecuteNonQuery();

                        if (insertedRows > 0)
                        {
                            itemExists = true;
                        }
                    }
                }
            }

            if (itemExists)
            {
                string script = "alert('You have added " + itemName + " to your cart. Price: $" + sellingPrice.ToString("C") + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
            }
        }

    }
}