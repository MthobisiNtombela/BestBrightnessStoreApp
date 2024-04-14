using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Best_Brightness
{
    public partial class CreatAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string firstname = txtFname.Text;
                string surname = txtLname.Text;
                string username = txtUsername.Text;
                string password = txtpassword.Text;
                string confirmPassword = txtConfirmPassword.Text;

                if (string.IsNullOrEmpty(firstname))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' Enter Firstname ');", true);
                    return;
                }
                if (string.IsNullOrEmpty(surname))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' Enter Surname ');", true);
                    return;
                }
                if (string.IsNullOrEmpty(username))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' Enter username ');", true);
                    return;
                }

                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' Enter password ');", true);
                    return;
                }


                if (password != confirmPassword)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Passwords do not match. Please try again.');", true);
                    return;
                }

                string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]).{5,}$";

                if (!System.Text.RegularExpressions.Regex.IsMatch(confirmPassword, passwordPattern))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Password must be at least 5 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.');", true);
                    return;
                }
                using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True"))
                {
                    conn.Open();
                    string checkUsernameQuery = "SELECT COUNT(*) FROM usersTB WHERE username = @Username";
                    using (SqlCommand checkUsernameCommand = new SqlCommand(checkUsernameQuery, conn))
                    {
                        checkUsernameCommand.Parameters.AddWithValue("@Username", username);
                        int existingUserCount = (int)checkUsernameCommand.ExecuteScalar();

                        if (existingUserCount > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Username already exists. Please choose another username.');", true);
                            return;
                        }
                    }
                }

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\Best_Brightness\App_Data\Best_Brightness.mdf;Integrated Security=True";
                con.Open();
                string query = "INSERT INTO usersTB (Firstname, Lastname, username, password, usertype) VALUES (@FirstName, @LastName, @Username, @Password, @usertype)";
                    SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@Firstname", firstname);
                command.Parameters.AddWithValue("@LastName", surname);
                command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", confirmPassword);
                command.Parameters.AddWithValue("@usertype", "User");



                int rowsAffected = command.ExecuteNonQuery();
                   
                    if (rowsAffected > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Registration successful. You can now log in.');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Registration failed. Please try again.');", true);
                    }
                }
            
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred: " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }
    }
}