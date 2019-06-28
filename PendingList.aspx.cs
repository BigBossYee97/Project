using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;


    public partial class PendingList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        int UserID;
        StringBuilder table = new StringBuilder();
        int i = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            con.Open();
            con2.Open();
            String UserQuery = "SELECT * FROM [dbo].[User] WHERE Email = @email";
            SqlCommand com2 = new SqlCommand(UserQuery,con2);
            SqlParameter CheckEmail = new SqlParameter("@email",Session["LoginEmail"]);
            com2.Parameters.Add(CheckEmail);
            SqlDataReader retrieve2 = com2.ExecuteReader();
            if (retrieve2.Read())
            {
                MemberName.Text = retrieve2["FirstName"].ToString();
                UserID = Convert.ToInt32(retrieve2["UserID"]);
            }
            String VideoQuery = "SELECT * FROM [dbo].[Video] WHERE Status = 'Pending'";
            SqlCommand com = new SqlCommand(VideoQuery, con);
            SqlParameter CheckID = new SqlParameter("@id", UserID);
            com.Parameters.Add(CheckID);
            SqlDataReader retrieve = com.ExecuteReader();
            table.Append("<table id = 'pendingTable' ");
            table.Append("<tr><th>No</th><th>Video Title</th><th>Category</th><th>Date</th><th>Action</th></tr>");
            if (retrieve.HasRows)
            {   
               while (retrieve.Read())
                {   
                    i++;
                    int id = Convert.ToInt32(retrieve["VideoID"]);
                    table.Append("<tr>");
                    table.Append("<td>" + i + "</td>");  
                    table.Append("<td>" + retrieve["Title"].ToString() + "</td>");
                    table.Append("<td>" + retrieve["Categories"].ToString() + "</td>");
                    table.Append("<td>" + retrieve["Date"].ToString() + "</td>");
                    table.Append("<td><button onclick = 'ViewVideo(this); return false;' id = 'ViewBtn' class = 'SbmtBtn' > View </button><button hidden id = 'HiddenID' value = "+ id +">"+ id +"</button></td>");
                    table.Append("</tr>");
                    


                }
               

            }
            
            table.Append("</table>");
            PlaceHolder1.Controls.Add(new Literal { Text = table.ToString() });
            retrieve.Close();
        }

        protected void MemberName_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx?LoginSuccess=1&User=Admin");
        }
    }
