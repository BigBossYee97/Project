using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

    public partial class StoreComment : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        String UserID;
        protected void Page_Load(object sender, EventArgs e)
        {   if(Session["LoginEmail"] != null) { 
            con1.Open();
            String UserQuery = "SELECT * FROM [dbo].[User] WHERE Email = @email";
            SqlCommand com1 = new SqlCommand(UserQuery, con1);
            SqlParameter CheckEmail = new SqlParameter("@email", Session["LoginEmail"]);
            com1.Parameters.Add(CheckEmail);
            SqlDataReader retrieve1 = com1.ExecuteReader();

            if (retrieve1.Read())
            {
                UserID = retrieve1["UserID"].ToString();                     
            }
            con1.Close();
                if (Request.QueryString["Comment"] != null)
                {
                    String comment = Request.QueryString["Comment"].ToString();
                    String VID = Request.QueryString["VideoID"].ToString();
                    con.Open();
                    String CommentQuery = "INSERT INTO [dbo].[Comment] (Comment,VideoID,UserID) VALUES (@comment,@video,@user)";
                    SqlCommand com = new SqlCommand(CommentQuery, con);
                    SqlParameter StoreComment = new SqlParameter("@comment", comment);
                    SqlParameter StoreVideo = new SqlParameter("@video", VID);
                    SqlParameter StoreUser = new SqlParameter("@user", UserID);
                    com.Parameters.Add(StoreComment);
                    com.Parameters.Add(StoreVideo);
                    com.Parameters.Add(StoreUser);
                    com.ExecuteNonQuery();
                }
            
            }
        }
    }
