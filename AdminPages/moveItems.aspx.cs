using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Best_Brightness.AdminPages
{
    public partial class moveItems : System.Web.UI.Page
    {
        string itemName = string.Empty;
        int quantity;
        decimal price;
        DataTable dataTable;
GridViewRow selectedRow;
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }


        protected void RDstore_CheckedChanged(object sender, EventArgs e)
        {
            if (RDstore.Checked)
            {
                this.GetStoreRoomData();
               
            }
        }

        protected void RDwarehouse_CheckedChanged(object sender, EventArgs e)
        {
            if (RDwarehouse.Checked)
            {
                GetWarehouseData();
              
            }
        }

        protected void GetWarehouseData()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True";
                con.Open();

                string query = "SELECT ItemID, ItemName, Quantity, CostPrice AS Price FROM warehouseTB";
                SqlCommand command = new SqlCommand(query, con);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
            }
        }
        protected void GetStoreRoomData()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True";
                con.Open();

                string query = "SELECT ItemID, ItemName, Quantity, SellingPrice  AS Price FROM storeroomTB";
                SqlCommand command = new SqlCommand(query, con);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                 dataTable = new DataTable();

                adapter.Fill(dataTable);
              
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchValue = txtSearch.Text.Trim();
                if (RDwarehouse.Checked)
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True";
                    con.Open();
                    string query = "SELECT  ItemID, ItemName, Quantity, CostPrice FROM warehouseTB WHERE ItemName LIKE @ItemName";
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@ItemName", "%" + searchValue + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                }
                if (RDstore.Checked)
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True";
                    conn.Open();
                    string query = "SELECT  ItemID, ItemName, Quantity, SellingPrice FROM storeroomTB WHERE ItemName LIKE @ItemName";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@ItemName", "%" + searchValue + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                   dataTable = new DataTable();

                    adapter.Fill(dataTable);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' storeroom cliked');", true);
                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
            }
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

        protected void btnRefresh_clicked(object sender, EventArgs e)
        {
            lblSelectedItemName.Text = string.Empty;

            if (RDwarehouse.Checked)
            {
                this.GetWarehouseData();
            }

            if (RDstore.Checked)
            {
                this.GetStoreRoomData();
            }
        }

        protected void MoveToStore()
        {
            selectedRow = GridView1.SelectedRow;

            if (selectedRow != null)
            {
                string itemID = selectedRow.Cells[1].Text;
                string itemName = selectedRow.Cells[2].Text;
                int quantityToMove = int.Parse(txtQuantity.Text);
                string itemUrl = this.GetItemUrl(itemID);

                try
                {
                    if (string.IsNullOrEmpty(txtQuantity.Text))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Quantity required');", true);
                        return;
                    }
                    if (string.IsNullOrEmpty(txtSellingPrice.Text))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Selling price required');", true);
                        return;
                    }

                    if (string.IsNullOrEmpty(this.GetItemUrl(itemID)))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Can't find Item Image');", true);
                        return;
                    }

                    decimal sellingPrice = decimal.Parse(txtSellingPrice.Text);

                    using (SqlConnection sourceConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True"))
                    {
                        sourceConnection.Open();

                      
                        string checkItemExistQuery = "SELECT COUNT(*) FROM storeroomTB WHERE ItemName = @ItemName";
                        SqlCommand checkItemExistCommand = new SqlCommand(checkItemExistQuery, sourceConnection);
                        checkItemExistCommand.Parameters.AddWithValue("@ItemName", itemName);
                        int itemCount = (int)checkItemExistCommand.ExecuteScalar();

                        if (itemCount > 0)
                        {
                          
                            string updateItemQuery = "UPDATE storeroomTB SET Quantity = Quantity + @QuantityToMove, SellingPrice = @SellingPrice WHERE ItemName = @ItemName";
                            SqlCommand updateItemCommand = new SqlCommand(updateItemQuery, sourceConnection);
                            updateItemCommand.Parameters.AddWithValue("@QuantityToMove", quantityToMove);
                            updateItemCommand.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                            updateItemCommand.Parameters.AddWithValue("@ItemName", itemName);
                            updateItemCommand.ExecuteNonQuery();
                        }
                        else
                        {
                          
                            string insertQuery = "INSERT INTO storeroomTB (ItemID, ItemName, Quantity, SellingPrice, ItemUrl) VALUES (@ID, @ItemName, @Quantity, @SellingPrice, @url)";
                            SqlCommand insertCommand = new SqlCommand(insertQuery, sourceConnection);
                            insertCommand.Parameters.AddWithValue("@ItemName", itemName);
                            insertCommand.Parameters.AddWithValue("@ID", itemID);
                            insertCommand.Parameters.AddWithValue("@Quantity", quantityToMove);
                            insertCommand.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                            insertCommand.Parameters.AddWithValue("@url", itemUrl);
                            insertCommand.ExecuteNonQuery();
                        }

                     
                        string updateQuantityQuery = "UPDATE warehouseTB SET Quantity = Quantity - @QuantityToMove WHERE ItemID = @ItemID";
                        SqlCommand updateQuantityCommand = new SqlCommand(updateQuantityQuery, sourceConnection);
                        updateQuantityCommand.Parameters.AddWithValue("@QuantityToMove", quantityToMove);
                        updateQuantityCommand.Parameters.AddWithValue("@ItemID", itemID);
                        updateQuantityCommand.ExecuteNonQuery();

                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Item: " + itemName + " Moved successfully to STORE ROOM');", true);
                        GetWarehouseData();
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select a row to move.');", true);
            }
        }

        protected string GetItemUrl(string ITEMID)
        {
            string itemUrl = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True"))
                {
                    con.Open();

                    string query = "SELECT ItemUrl FROM warehouseTB WHERE ItemID = @ItemID";
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@ItemID", ITEMID);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        itemUrl = reader["ItemUrl"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred retriving ItemUrl: " + ex.Message + "');", true);
                itemUrl = string.Empty;
            }

            return itemUrl;
        }

        protected void MoveToStock()
        {
            selectedRow = GridView1.SelectedRow;

            if (selectedRow != null)
            {
                string itemID = selectedRow.Cells[1].Text;
                string itemName = selectedRow.Cells[2].Text;
                int quantityToMove = int.Parse(txtQuantity.Text);
                string itemUrl = this.GetItemUrl(itemID);

                try
                {
                    if (string.IsNullOrEmpty(txtQuantity.Text))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Quantity required');", true);
                        return;
                    }
                    if (string.IsNullOrEmpty(txtSellingPrice.Text))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Selling price required');", true);
                        return;
                    }

                    if (string.IsNullOrEmpty(this.GetItemUrl(itemID)))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Can't find Item Image');", true);
                        return;
                    }

                    decimal sellingPrice = decimal.Parse(txtSellingPrice.Text);

                    using (SqlConnection sourceConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True"))
                    {
                        sourceConnection.Open();


                        string checkItemExistQuery = "SELECT COUNT(*) FROM stockTB WHERE ItemName = @ItemName";
                        SqlCommand checkItemExistCommand = new SqlCommand(checkItemExistQuery, sourceConnection);
                        checkItemExistCommand.Parameters.AddWithValue("@ItemName", itemName);
                        int itemCount = (int)checkItemExistCommand.ExecuteScalar();

                        if (itemCount > 0)
                        {

                            string updateItemQuery = "UPDATE stockTB SET Quantity = Quantity + @QuantityToMove, SellingPrice = @SellingPrice WHERE ItemName = @ItemName";
                            SqlCommand updateItemCommand = new SqlCommand(updateItemQuery, sourceConnection);
                            updateItemCommand.Parameters.AddWithValue("@QuantityToMove", quantityToMove);
                            updateItemCommand.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                            updateItemCommand.Parameters.AddWithValue("@ItemName", itemName);
                            updateItemCommand.ExecuteNonQuery();
                        }
                        else
                        {

                            string insertQuery = "INSERT INTO stockTB (ItemID, ItemName, Quantity, SellingPrice, ItemUrl) VALUES (@ID, @ItemName, @Quantity, @SellingPrice, @url)";
                            SqlCommand insertCommand = new SqlCommand(insertQuery, sourceConnection);
                            insertCommand.Parameters.AddWithValue("@ItemName", itemName);
                            insertCommand.Parameters.AddWithValue("@ID", itemID);
                            insertCommand.Parameters.AddWithValue("@Quantity", quantityToMove);
                            insertCommand.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                            insertCommand.Parameters.AddWithValue("@url", itemUrl);
                            insertCommand.ExecuteNonQuery();
                        }


                        string updateQuantityQuery = "UPDATE storeroomTB SET Quantity = Quantity - @QuantityToMove WHERE ItemID = @ItemID";
                        SqlCommand updateQuantityCommand = new SqlCommand(updateQuantityQuery, sourceConnection);
                        updateQuantityCommand.Parameters.AddWithValue("@QuantityToMove", quantityToMove);
                        updateQuantityCommand.Parameters.AddWithValue("@ItemID", itemID);
                        updateQuantityCommand.ExecuteNonQuery();

                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Item: " + itemName + " Moved successfully to STOCK');", true);
                        this.GetStoreRoomData();
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select a row to move.');", true);
            }
        }
        protected void btnMoveItems_clicked(object sender, EventArgs e)
        {
            if (RDwarehouse.Checked)
            {
                this.MoveToStore();
            }

            if (RDstore.Checked)
            {
                this.MoveToStock();
            }
           
        }
    }
}