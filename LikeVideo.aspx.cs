using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

    public partial class LikeVideo : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {   if (Request.QueryString["VideoID"] != null)
            {
                string video = Request.QueryString["VideoID"];
                string user = Request.QueryString["UserID"];
                string like = Request.QueryString["TotalLike"];

                con.Open();
                String TotalLikeQuery = "UPDATE [dbo].[Video] SET TotalLike = @totalLike WHERE VideoID="+video;
                SqlCommand com = new SqlCommand(TotalLikeQuery,con);
                SqlParameter getLike = new SqlParameter("@totalLike",like);
                com.Parameters.Add(getLike);
                com.ExecuteNonQuery();
                con.Close();
                
                con1.Open();
                String query = "INSERT INTO [dbo].[Vote] (UserID,VideoID,VoteLike,VoteDislike) VALUES (@user,@video,1,0)";
                SqlCommand com2 = new SqlCommand(query, con1);
                SqlParameter getUser = new SqlParameter("@user",user);
                SqlParameter getVideo = new SqlParameter("@video",video);
                
                com2.Parameters.Add(getUser);
                com2.Parameters.Add(getVideo);
               
               
                com2.ExecuteNonQuery();
                con1.Close();
            }
        }
    }
