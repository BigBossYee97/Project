using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
public partial class SearchResult : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
    String LoginSuccess,Character;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["Title"] != null)
        {
            string title = Request.QueryString["Title"];
            if(Session["LoginEmail"] != null)
            {
                con1.Open();
                String UserQuery = "SELECT * FROM [dbo].[User] WHERE Email = @email" ;
                SqlCommand com1 = new SqlCommand(UserQuery,con1);
                SqlParameter getEmail = new SqlParameter("@email",Session["LoginEmail"]);
                com1.Parameters.Add(getEmail);
                SqlDataReader retrieve1 = com1.ExecuteReader();
                LoginSuccess = "1";
                if (retrieve1.Read())
                {
                    Character = retrieve1["Character"].ToString();
                }
                con1.Close();
            }


            con.Open();
            String SearchQuery = "SELECT * FROM [dbo].[Video] WHERE Title = @title" ;
            SqlCommand com = new SqlCommand(SearchQuery,con);
            SqlParameter getTitle = new SqlParameter("@title",title);
            com.Parameters.Add(getTitle);
            SqlDataReader retrieve = com.ExecuteReader();
            if (retrieve.HasRows)
            {
                if (retrieve.Read())
                { if (LoginSuccess == "1")
                    {
                        Response.Redirect("VideoPlayer.aspx?LoginSuccess="+LoginSuccess + "&User="+ Character + "&VideoID=" + retrieve["VideoID"].ToString());

                    }
                    else
                    {
                        Response.Redirect("VideoPlayer.aspx?VideoID=" + retrieve["VideoID"].ToString());
                    }
                    }
            }
           
            con.Close();
        }
    }
}