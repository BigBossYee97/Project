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

    public partial class VideoPlayer : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con5 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        public String CommentResult { get; set; }
        public String NameResult { get; set; }
        public String UserIDResult { get; set; }
        public String LikeResult { get; set; }
        public String DislikeResult { get; set; }

        ArrayList CommentGet = new ArrayList();
        ArrayList NameGet = new ArrayList();
        String userID,character,VidID,getLike,getDislike;

        protected void GoLogIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void GoSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }

        StringBuilder List = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {   
            if(Request.QueryString["VideoID"] != null)
            {
                String videoID = Request.QueryString["VideoID"];
            if (Session["LoginEmail"] != null)
            {
                con1.Open();
                String UserQuery = "SELECT * FROM [dbo].[User] WHERE Email = @email";
                SqlCommand com1 = new SqlCommand(UserQuery, con1);
                SqlParameter CheckEmail = new SqlParameter("@email", Session["LoginEmail"]);
                com1.Parameters.Add(CheckEmail);
                SqlDataReader retrieve1 = com1.ExecuteReader();

                if (retrieve1.Read())
                {
                    userID = retrieve1["UserID"].ToString();
                    MemberName.Text = retrieve1["FirstName"].ToString();
                    character = retrieve1["Character"].ToString();
                }

                con1.Close();
                if (Session["LoginEmail"] != null)
                {
                    con4.Open();
                    String VoteQuery = "SELECT * FROM [dbo].[Vote] WHERE VideoID =" + videoID + "AND UserID=" + userID + "ORDER BY VoteID DESC";
                    SqlCommand com4 = new SqlCommand(VoteQuery, con4);
                    SqlDataReader retrieve4 = com4.ExecuteReader();
                    if (retrieve4.HasRows)
                    {
                        if (retrieve4.Read())
                        {
                            getLike = retrieve4["VoteLike"].ToString();
                            getDislike = retrieve4["VoteDislike"].ToString();
                        }
                    }
                    else
                    {
                        getLike = "0";
                        getDislike = "0";
                    }
                    con.Close();
                    con4.Close();
                }

            }
                

                con.Open();
                String VideoQuery = "SELECT * FROM [dbo].[Video] WHERE VideoID = " + videoID;
                SqlCommand com = new SqlCommand(VideoQuery,con);
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
                    VidID = retrieve["VideoID"].ToString();
                    String Like = retrieve["TotalLike"].ToString();
                    String Dislike = retrieve["TotalDislike"].ToString();
                    
                    List.Append("<tr>");

                    List.Append("<td style ='margin-left:20px;background-color:black;border-radius:10px;padding:20px;color:white'><video style = 'margin-right:50px' width = 700 height = 400 Controls><Source src =" + link + "type = video/mp4><Source src =" + link + "type = video/webm><Source src =" + link + "type = video/ogg></video><br>" + title + "<i style = 'float:right;margin-right:50px;'><a href = '#' id = '"+ getLike +"' style = 'color:#4286f4' onclick = 'LikeVideo(this)'>Like</a> <a href = '#' style = 'color:#4286f4' onclick = 'DislikeVideo(this)' id = '"+getDislike+"'>Dislike</a></i><br><i style = 'font-size:14px;color:white'>" + View + " views <i style = 'margin-left:520px' >Likes <i id = 'NumLiked'>" + Like + " </i></i> <i>Dislikes <i id = 'NumDisliked'>" + Dislike + "</i></i><br><i>" + Date + "</i></i><br><br><h3 style = 'color:white'>Description</h3><i style = 'color:white'>" + Descrp + "</i>");

                    List.Append("</td></tr>");
                }
               

                List.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = List.ToString() });

                
                con2.Open();
                String CommentQuery = "SELECT * FROM [dbo].[Comment] WHERE VideoID = @vID";
                SqlCommand com3 = new SqlCommand(CommentQuery, con2);
                SqlParameter CheckID = new SqlParameter("@vID", videoID);
                com3.Parameters.Add(CheckID);
                SqlDataReader retrieveComment = com3.ExecuteReader();
                while (retrieveComment.Read())
                {
                    String comm = retrieveComment["Comment"].ToString();
                    String UpBy = retrieveComment["UserID"].ToString();
                    CommentGet.Add(comm);
                    GetUploadUser(UpBy);


                }
                CommentResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(CommentGet);
                NameResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(NameGet);
                UserIDResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(userID);
                LikeResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(getLike);
                DislikeResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(getDislike);

            }

        }
        public void GetUploadUser(String id)
        {
            con3.Open();
            String TraceUser = "SELECT * FROM [dbo].[User] WHERE UserID = " + id;
            SqlCommand com4 = new SqlCommand(TraceUser, con3);
            SqlDataReader retrieveUserName = com4.ExecuteReader();
            if (retrieveUserName.Read())
            {
                String name = retrieveUserName["FirstName"].ToString();
                NameGet.Add(name);
            }
            retrieveUserName.Close();
            con3.Close();
        }

        protected void MemberName_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx?LoginSuccess=1&User="+character);
        }
    }
