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
    public partial class Salse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getSALESDB();
        }

        protected void getSALESDB()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True";
                con.Open();

                string query = "SELECT ItemID, ItemName, Quantity, TotalCost, SellingPrice, UserID FROM stockBoughtTB";
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
    }
}