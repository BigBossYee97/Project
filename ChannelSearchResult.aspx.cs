using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Collections;

    public partial class ChannelSearchResult : System.Web.UI.Page
    {
       
        public String CommentResult { get; set; }
        public String NameResult { get; set; }
       
        ArrayList CommentGet = new ArrayList();
        ArrayList NameGet = new ArrayList();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        String UserID,UserCharacter,id;
        StringBuilder List = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            

            con1.Open();
            String UserQuery = "SELECT * FROM [dbo].[User] WHERE Email = @email";
            SqlCommand com1 = new SqlCommand(UserQuery, con1);
            SqlParameter CheckEmail = new SqlParameter("@email", Session["LoginEmail"]);
            com1.Parameters.Add(CheckEmail);
            SqlDataReader retrieve1 = com1.ExecuteReader();

            if (retrieve1.Read())
            {
                UserID = retrieve1["UserID"].ToString();
                MemberName.Text = retrieve1["FirstName"].ToString();
                UserCharacter = retrieve1["Character"].ToString();

            }
            if (Request.QueryString["VideoID"] != null)
            {
                id = Request.QueryString["VideoID"];
                con.Open();
                String ResultQuery = "SELECT * FROM [dbo].[Video] WHERE VideoID =" + id;
                SqlCommand com = new SqlCommand(ResultQuery, con);
                SqlDataReader retrieve = com.ExecuteReader();
                List.Append("<table style = 'margin-left:350px;margin-top:50px'>");
                List.Append("<tr>");

                if (retrieve.Read())
                {
                    String VideoFile = retrieve["VideoFile"].ToString();
                    String link = "../Content/" + VideoFile;
                    String title = retrieve["Title"].ToString();
                    String View = retrieve["TotalView"].ToString();
                    String Date = retrieve["Date"].ToString();
                    String Descrp = retrieve["Description"].ToString();
                    String VidID = retrieve["VideoID"].ToString();
                    String Like = retrieve["TotalLike"].ToString();
                    String Dislike = retrieve["TotalDislike"].ToString();
                 
                    List.Append("<tr>");

                    List.Append("<td style ='margin-left:20px;background-color:black;border-radius:10px;padding:20px'><video style = 'margin-right:50px' width = 700 height = 400 Controls><Source src =" + link + "type = video/mp4><Source src =" + link + "type = video/webm><Source src =" + link + "type = video/ogg></video><br><a href = '#' style = 'color:white' onclick = 'LinkVal(this)' class = 'getVideoID' id = " + id + ">" + title + "<i style = 'float:right;margin-right:50px;'><a href = '#' onclick = 'editVideo()'>Edit</a> <a href = '#' onclick = 'DelVideo()'>Delete</a></i><br><i style = 'font-size:14px;color:white'>" + View + " views <i style = 'margin-left:520px'>Likes "+ Like +" </i> <i>Dislikes "+Dislike+"</i><br><i>" + Date + "</i></i></a><br><br><h3 style = 'color:white'>Description</h3><i style = 'color:white'>"+Descrp+"</i>");
               
                    List.Append("</td></tr>");

                    con3.Open();
                    con4.Open();
                    String CommentQuery = "SELECT * FROM [dbo].[Comment] WHERE VideoID = @vID";
                    SqlCommand com3 = new SqlCommand(CommentQuery,con3);
                    SqlParameter CheckID = new SqlParameter("@vID",VidID);
                    com3.Parameters.Add(CheckID);
                    SqlDataReader retrieveComment = com3.ExecuteReader();
                    while (retrieveComment.Read())
                    {
                        String comm = retrieveComment["Comment"].ToString();
                        String UpBy = retrieveComment["UserID"].ToString();
                        CommentGet.Add(comm);
                        GetUploadUser(UpBy);
                        

                    }
                    con4.Close();
                    con3.Close();
                }
                List.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = List.ToString() });

                CommentResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(CommentGet);
                NameResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(NameGet);
            }
            else
            {
                String user = Request.QueryString["User"];
                Response.Redirect("MyChannel.aspx?LoginSuccess=1&User=" + user);
            }
        }

     public void GetUploadUser(String id)
        {
            String TraceUser = "SELECT * FROM [dbo].[User] WHERE UserID = " + id;
            SqlCommand com4 = new SqlCommand(TraceUser, con4);
            SqlDataReader retrieveUserName = com4.ExecuteReader();
            if (retrieveUserName.Read())
            {
                String name = retrieveUserName["FirstName"].ToString();
                NameGet.Add(name);
            }
            retrieveUserName.Close();
        }

        protected void MemberName_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx?LoginSuccess=1&User="+UserCharacter);
        }

        
    }
