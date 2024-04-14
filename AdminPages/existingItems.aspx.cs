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
    public partial class existingItems : System.Web.UI.Page
    {
        string itemName = string.Empty;
        int quantity;
        decimal price;
        GridViewRow selectedRow;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetWarehouseData();
        }
        protected void GetWarehouseData()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True";
                con.Open();

                string query = "SELECT ItemID, ItemName, Quantity, CostPrice FROM warehouseTB";
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


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchValue = txtSearch.Text.Trim();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True";
                con.Open();
                string query = "SELECT  ItemID, ItemName, Quantity, CostPrice FROM warehouseTB WHERE ItemName LIKE @ItemName";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@ItemName", "%" + searchValue + "%");

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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            selectedRow = GridView1.SelectedRow;

            if (selectedRow != null)
            {

                itemName = selectedRow.Cells[2].Text;
                price = decimal.Parse(selectedRow.Cells[4].Text);
                quantity = int.Parse(selectedRow.Cells[3].Text);

                lblSelectedItemName.Text = "Selected Item Name: " + itemName;
            }
        }

        protected void btnRefresh_clicked(object sender, EventArgs e)
        {
            lblSelectedItemName.Text = string.Empty;
            this.GetWarehouseData();
        }

        protected void btnSave_cliked(object sender, EventArgs e)
        {

            try
            {
                selectedRow = GridView1.SelectedRow;

                if (selectedRow != null)
                {

                    itemName = selectedRow.Cells[2].Text;
                    price = decimal.Parse(selectedRow.Cells[4].Text);
                    quantity = int.Parse(selectedRow.Cells[3].Text);

                    lblSelectedItemName.Text = "Selected Item Name: " + itemName;
                }

                if (string.IsNullOrEmpty(lblSelectedItemName.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Select Item from  the Table.');", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtQuantity.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Quantity requred');", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtCostPrice.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cost price requred');", true);
                    return;
                }
                string selectedName = lblSelectedItemName.Text.Replace("Selected Item Name: ", "");

                int updatedQuantity = int.Parse(txtQuantity.Text);
                decimal updatedCostPrice = decimal.Parse(txtCostPrice.Text);

                updatedQuantity += quantity;
                updatedCostPrice += price;

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True";
                con.Open();

                string updateQuery = "UPDATE warehouseTB SET Quantity = @Quantity, CostPrice = @CostPrice WHERE ItemName = @ItemName";
                SqlCommand updateCommand = new SqlCommand(updateQuery, con);
                updateCommand.Parameters.AddWithValue("@Quantity", updatedQuantity);
                updateCommand.Parameters.AddWithValue("@CostPrice", updatedCostPrice);
                updateCommand.Parameters.AddWithValue("@ItemName", selectedName);

                int rowsAffected = updateCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Item updated successfully.');", true);
                    GetWarehouseData();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to update the item.');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
            }
        }
    }
}