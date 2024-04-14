using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Best_Brightness
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        string userID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.userID = (string)Session["UserID"];
           
        }

     protected void ChangePass(object sender, EventArgs e)
        {
            try
            {
                string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]).{5,}$";

                string newPassword =txtNewPassword.Text;
                string pass = txtPassword.Text;

                if (!pass.Equals(newPassword))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' Password Dont match');", true);
                    return;
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(newPassword, passwordPattern))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Password must be at least 5 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.');", true);
                    return;
                }

                    SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True";
                con.Open();

                string query = "UPDATE usersTB SET password = @NewPassword WHERE userID = @UserID";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@NewPassword", newPassword);
                command.Parameters.AddWithValue("@UserID", this.userID);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Password updated successfully.');", true);
                    txtNewPassword.Text = null;
                    txtPassword.Text = null;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to update password. Please try again.');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
            }
        }
    }
}