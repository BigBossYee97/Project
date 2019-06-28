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


    public partial class MyChannel : System.Web.UI.Page
    {
        public String ArrayResult { get; set; }
        public String SearchResult { get; set; }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        StringBuilder List = new StringBuilder();
        String UserCharacter,UserID;
        protected void Page_Load(object sender, EventArgs e)
        {   
                ArrayList VideoTitle = new ArrayList();
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




                con.Open();
                String VideoQuery = "SELECT * FROM [dbo].[Video] WHERE Status = 'Approved' AND UserID =" + UserID;
                SqlCommand com = new SqlCommand(VideoQuery, con);
                //SqlParameter CheckEmail = new SqlParameter("@email", Session["LoginEmail"]);
                // com1.Parameters.Add(CheckEmail);
                SqlDataReader retrieve = com.ExecuteReader();
                List.Append("<table style = 'margin-left:350px;margin-top:50px'>");
                List.Append("<tr>");
                int i = 1;
                if (retrieve.HasRows)
                {

                    while (retrieve.Read())
                    {
                        String VideoFile = retrieve["VideoFile"].ToString();
                        String link = "../Content/" + VideoFile;
                        String title = retrieve["Title"].ToString();
                        String View = retrieve["TotalView"].ToString();
                        String Date = retrieve["Date"].ToString();
                        String VidID = retrieve["VideoID"].ToString();
                        VideoTitle.Add(retrieve["Title"].ToString());
                        GenerateVideo(i, link, title, View, Date, VidID);

                        if (i == 4)
                        {
                            List.Append("</tr>");
                            List.Append("<tr>");
                            i = 1;
                        }
                        else
                        {
                            i++;
                        }



                    }


                }

                List.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = List.ToString() });
                ArrayResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(VideoTitle);
            
           

    }
        public void GenerateVideo(int x, String path, String t, String v, String d, String id)
        {
            List.Append("<td><video style = 'margin-right:50px' width = 300 height = 200 Controls><Source src =" + path + "type = video/mp4><Source src =" + path + "type = video/webm><Source src =" + path + "type = video/ogg></video><br><a href = '#' style = 'color:white' onclick = 'LinkVal(this)' class = 'getVideoID' id = "+id+">"+t+ "<i style = 'float:right;margin-right:50px;'><a href = '#' id = " + id + " onclick = 'EditVideo(this)'>Edit</a> <a href = '#' onclick = 'DelVideo(this)' id = " + id + ">Delete</a></i><br><i style = 'font-size:14px;color:white'>" + v+ " views <i><br>" + d+ "</i></i></a></td>");
            
        }
        protected void GoUploadVideo_Click(object sender, EventArgs e)
        {   
            Response.Redirect("UploadVideo.aspx?LoginSuccess=1&User="+UserCharacter);
        }

        protected void MemberName_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx?LoginSuccess=1&User="+UserCharacter);
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            con2.Open();
            String SearchQuery = "SELECT * FROM [dbo].[Video] WHERE Title = @title";
            SqlCommand com2 = new SqlCommand(SearchQuery,con2);
            SqlParameter CheckTitle = new SqlParameter("@title",myInput.Value);
            com2.Parameters.Add(CheckTitle);
            SqlDataReader retrieve2 = com2.ExecuteReader();
            if (retrieve2.Read())
            {
                Response.Redirect("ChannelSearchResult.aspx?LoginSuccess=1&User="+UserCharacter+"&VideoID="+retrieve2["VideoID"].ToString());
            }
            else
            {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Video Not Found');", true);
           
            }
        }
       



    }
