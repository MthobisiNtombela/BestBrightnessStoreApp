using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Best_Brightness
{
    public partial class login : System.Web.UI.Page
    {
       
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnForgotMethod(object sender, EventArgs e)
        {
            try
            {
              
                string user = string.Empty;
                string userID = string.Empty;

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\BestBrightnessDB.mdf;Integrated Security=True;Connect Timeout=30";
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                string username = Request.Form["txtUsername"];
           

                if (string.IsNullOrEmpty(username))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' Enter username ');", true);
                    return;
                }
              

                string q = "SELECT * FROM usersTB WHERE username = @Username";
                cmd.CommandText = q;
                cmd.Parameters.AddWithValue("@Username", username);

                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    user = da.GetValue(3).ToString();
                    userID = da.GetValue(0).ToString();
                  
                }

                if (user.Equals(username))
                {
                    Session["UserID"] = userID;
                    Response.Redirect("ForgotPassword.aspx");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('user not found');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
            }
          
           
        }
        protected void btnAcc(object sender, EventArgs e)
        {
            Response.Redirect("CreatAccount.aspx");
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string pas = string.Empty;
            string user = string.Empty;
            string userID = string.Empty;
            string utype = string.Empty;

            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\BestBrightnessDB.mdf;Integrated Security=True;Connect Timeout=30";
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                string username = Request.Form["txtUsername"];
                string password = Request.Form["txtPassword"];

                if (string.IsNullOrEmpty(username))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' Enter username ');", true);
                    return;
                }
                if (string.IsNullOrEmpty(password))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' Enter password ');", true);
                    return;
                }

                string q = "SELECT * FROM usersTB WHERE username = @Username";
                cmd.CommandText = q;
                cmd.Parameters.AddWithValue("@Username", username);

                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    pas = da.GetValue(4).ToString();
                    user = da.GetValue(3).ToString();
                    userID = da.GetValue(0).ToString();
                    utype = da.GetValue(5).ToString();
                }

                if (pas.Equals(password) && user.Equals(username))
                {
                    if (utype.Equals("User"))
                    {

                        Session["UserID"] = userID;
                        Response.Redirect("UserPages/MainPage.aspx");
                    }
                    if (utype.Equals("Admin"))
                    {

                        Session["UserID"] = userID;
                        Response.Redirect("AdminPages/main.aspx");
                    }
                  
                   
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('user not found');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
            }
        }

      
    }
  
}
