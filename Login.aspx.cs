using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


    public partial class Login : System.Web.UI.Page
    {   String email, password;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            ForgotPassword.ForeColor = System.Drawing.Color.Blue;
            con.Open();
            con2.Open();
            
        }

        protected void InputEmail_TextChanged(object sender, EventArgs e)
        {

        }

        protected void InputPassword_TextChanged(object sender, EventArgs e)
        {

        }
       
        protected void LogInBtn_Click(object sender, EventArgs e)
        {
            email = InputEmail.Text;
            password = InputPassword.Text;
           
            if (email == "" || password == "" )
            {
                LoginMessage.ForeColor = System.Drawing.Color.Red;
                LoginMessage.Text = "Please Make Sure Email and Password is Entered Correctly";
                
            }
            else
            {
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(password);
                string ValidationPass = Convert.ToBase64String(bytes);
               
                SqlCommand com = new SqlCommand("LoginValidation", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlParameter username = new SqlParameter("Email", email);
                SqlParameter pass = new SqlParameter("Password", ValidationPass);
                com.Parameters.Add(username);
                com.Parameters.Add(pass);
                SqlDataReader validation = com.ExecuteReader();
                if (validation.HasRows)
                {
                    validation.Read();
                    LoginMessage.ForeColor = System.Drawing.Color.Yellow;
                    LoginMessage.Text = "Login Successfully";
                    String getChar = "SELECT * FROM [dbo].[User] WHERE Email = @email AND Password = @pass";
                    SqlCommand query = new SqlCommand(getChar,con2);
                    query.Parameters.AddWithValue("@email",email);
                    query.Parameters.AddWithValue("@pass", ValidationPass);
                    SqlDataReader retrieve = query.ExecuteReader();
                    if (retrieve.Read()) {
                        Session["LoginEmail"] = email;
                        Response.Redirect("Home.aspx?LoginSuccess=1&User=" + retrieve["Character"].ToString());
                        
                    }
                }
                else
                {
                    LoginMessage.ForeColor = System.Drawing.Color.Red;
                    LoginMessage.Text = "Invalid Email or Password";
                }
            }
           
            
            con.Close();
            con2.Close();
        }
     /*   private String DecryptPassword(String EncryptedPass)
        {
            byte[] bytes = Convert.FromBase64String(EncryptedPass);
            string DecryptedPass = System.Text.Encoding.Unicode.GetString(bytes);
            return DecryptedPass;
        }*/

    }
