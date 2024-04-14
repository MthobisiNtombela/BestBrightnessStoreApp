using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;


namespace Best_Brightness.AdminPages
{
    public partial class NewItems : System.Web.UI.Page
    {
        string itemUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnExisting_clicked(object sender, EventArgs e)
        {
            Response.Redirect("existingItems.aspx");
        }
            protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
                {
                    string itemName = txtItemName.Text;
                  
                   

                    string fileName = Path.GetFileName(fileUpload.FileName);
                    string filePath = Server.MapPath(("/Pic/")+ fileName);
                    fileUpload.SaveAs(filePath);
                    itemUrl =@"/Pic/"+ fileName;


                    if (string.IsNullOrEmpty(itemName))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' Enter Item Name ');", true);
                        return;
                    }
                    if (string.IsNullOrEmpty(txtQuantity.Text))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' Enter Quantiy ');", true);
                        return;
                    }
                    if (string.IsNullOrEmpty(txtCostPrice.Text))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' Enter Cost Price');", true);
                        return;
                    }

                if (string.IsNullOrEmpty(itemUrl))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Upload item image ');", true);
                    return;
                }

               
                int quantity = int.Parse(txtQuantity.Text);
                decimal costPrice = decimal.Parse(txtCostPrice.Text);

                using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True"))
                {
                    conn.Open();
                    string checkUsernameQuery = "SELECT COUNT(*) FROM warehouseTB WHERE ItemName = @1";
                    using (SqlCommand checkUsernameCommand = new SqlCommand(checkUsernameQuery, conn))
                    {
                        checkUsernameCommand.Parameters.AddWithValue("@1", itemName);
                        int existingUserCount = (int)checkUsernameCommand.ExecuteScalar();

                        if (existingUserCount > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Item name already exists. Click on Existing items');", true);
                            return;
                        }
                    }
                }

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True";
                con.Open();
                string query = "INSERT INTO warehouseTB (ItemName, Quantity, CostPrice, ItemUrl) VALUES (@1, @2, @3, @4)";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@1", itemName);
                command.Parameters.AddWithValue("@2", quantity);
                command.Parameters.AddWithValue("@3", costPrice);
                command.Parameters.AddWithValue("@4", itemUrl);
                



                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Item: " + itemName + " saved successful');", true);
                    cliear();

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' failed to save new item. Please try again.');", true);
                }
            

            }
                catch (Exception ex)
                {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' Error: "+ex.Message+ ".');", true);
            }
            
        }
        protected void cliear()
        {
            txtQuantity.Text = null;
            txtItemName.Text = null;
            txtCostPrice.Text = null;
            itemUrl = null;
        }
       
    }
    
}