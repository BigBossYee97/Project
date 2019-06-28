using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;


    public partial class UserProfile : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);


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
                GetFirstName.Text = retrieve["FirstName"].ToString();
                GetLastName.Text = retrieve["LastName"].ToString();
                GetEmail.Text = retrieve["Email"].ToString();
                GetCharacter.Text = retrieve["Character"].ToString();
                MemberName.Text = retrieve["FirstName"].ToString();
                string img = retrieve["IMAGE"].ToString();
                imagePreview1.ImageUrl = "../Content/" + img;
               
            }
        

        }
       
            
        
        protected void MemberName_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx?LoginSuccess=1&User=" + GetCharacter.Text);
        }

        protected void EditProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditUserProfile.aspx?LoginSuccess=1&User=" + GetCharacter.Text);
        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx?LoginSuccess=1&User=" + GetCharacter.Text);
        }

        protected void GoLogIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void GoSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }
    }
