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
    public partial class ViewItems : System.Web.UI.Page
    {
        string itemName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetWarehouseData();
            getStrockData();
            GetStoreRoomData();
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

        protected void getStrockData()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True";
                con.Open();

                string query = "SELECT ItemID, ItemName, Quantity, SellingPrice FROM stockTB";
                SqlCommand command = new SqlCommand(query, con);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);
                GridView3.DataSource = dataTable;
                GridView3.DataBind();
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

                string query = "SELECT ItemID, ItemName, Quantity, SellingPrice FROM storeroomTB";
                SqlCommand command = new SqlCommand(query, con);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                GridView2.DataSource = dataTable;
                GridView2.DataBind();
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

                // Search for warehouse items
                string query1 = "SELECT ItemID, ItemName, Quantity, CostPrice FROM warehouseTB WHERE ItemName LIKE @ItemName";
                SqlCommand command1 = new SqlCommand(query1, con);
                command1.Parameters.AddWithValue("@ItemName", "%" + searchValue + "%");

                SqlDataAdapter adapter1 = new SqlDataAdapter(command1);
                DataTable dataTable1 = new DataTable();

                adapter1.Fill(dataTable1);

                GridView1.DataSource = dataTable1;
                GridView1.DataBind();

                // Search for storeroom items
                string query2 = "SELECT ItemID, ItemName, Quantity, SellingPrice FROM storeroomTB WHERE ItemName LIKE @ItemName";
                SqlCommand command2 = new SqlCommand(query2, con);
                command2.Parameters.AddWithValue("@ItemName", "%" + searchValue + "%");

                SqlDataAdapter adapter2 = new SqlDataAdapter(command2);
                DataTable dataTable2 = new DataTable();

                adapter2.Fill(dataTable2);

                GridView2.DataSource = dataTable2;
                GridView2.DataBind();

                // Search for stock items
                string query3 = "SELECT ItemID, ItemName, Quantity, SellingPrice FROM stockTB WHERE ItemName LIKE @ItemName";
                SqlCommand command3 = new SqlCommand(query3, con);
                command3.Parameters.AddWithValue("@ItemName", "%" + searchValue + "%");

                SqlDataAdapter adapter3 = new SqlDataAdapter(command3);
                DataTable dataTable3 = new DataTable();

                adapter3.Fill(dataTable3);

                GridView3.DataSource = dataTable3;
                GridView3.DataBind();

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
            }
        }


        protected void btnRefresh_clicked(object sender, EventArgs e)
        {
            lblSelectedItemName.Text = string.Empty;
            this.GetWarehouseData();

        }
    }
}