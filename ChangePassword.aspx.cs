using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

    public partial class ChangePassword : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        String CurrentPass,Character;
        protected void Page_Load(object sender, EventArgs e)
        {   
            con.Open();
            String query = "SELECT * FROM [dbo].[User] WHERE Email = @email";
            SqlCommand com = new SqlCommand(query, con);
            SqlParameter CheckEmail = new SqlParameter("@email", Session["LoginEmail"]);
            com.Parameters.Add(CheckEmail);
            SqlDataReader retrieve = com.ExecuteReader();
            if (retrieve.Read())
            {
                MemberName.Text = retrieve["FirstName"].ToString();
                CurrentPass = DecryptPassword(retrieve["Password"].ToString());
                Character = retrieve["Character"].ToString();
            }
            con.Close();

        }
        private String DecryptPassword(String EncryptedPass)
        {
            byte[] bytes = Convert.FromBase64String(EncryptedPass);
            string DecryptedPass = System.Text.Encoding.Unicode.GetString(bytes);
            return DecryptedPass;
        }

       

        protected void GoLogIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void GoSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }

        protected void ChangePassBtn_Click(object sender, EventArgs e)
        {
            if(CurrentPass == CurrentPassText.Text)
            {
                if(NewPassText.Text == ConfNewPassText.Text)
                {
                    String New = NewPassText.Text;
                    if(New.Length > 7)
                    {
                        con2.Open();
                        String Hash_Pass = EncryptPassword(NewPassText.Text);
                        String ChgPassQuery = "UPDATE [dbo].[User] SET Password = '" + Hash_Pass + "' WHERE Email = @email1";
                        SqlCommand com1 = new SqlCommand(ChgPassQuery, con2);
                        SqlParameter CheckEmail1 = new SqlParameter("@email1", Session["LoginEmail"]);
                        com1.Parameters.Add(CheckEmail1);
                        com1.ExecuteNonQuery();
                    
                        ChangePasswordLabel.ForeColor = System.Drawing.Color.Yellow;
                        ChangePasswordLabel.Text = "Password Changed Successfully";
                        con2.Close();
                    }
                    else
                    {
                        ChangePasswordLabel.ForeColor = System.Drawing.Color.Red;
                        ChangePasswordLabel.Text = "Password Length Must At Least 8 Characters";
                    }
                }
                else
                {
                    ChangePasswordLabel.ForeColor = System.Drawing.Color.Red;
                    ChangePasswordLabel.Text = "New Password Doesn't Matched with Confirm New Password. Please Try Again";
                }
            }
            else
            {
                ChangePasswordLabel.ForeColor = System.Drawing.Color.Red;
                ChangePasswordLabel.Text = "Invalid Current Password. Please Try Again";
            }
        }
        private String EncryptPassword(String pass)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(pass);
            string EncryptedPass = Convert.ToBase64String(bytes);
            return EncryptedPass;
        }

    protected void MemberName_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserProfile.aspx?LoginSuccess=1&User="+Character);
    }
}

    
