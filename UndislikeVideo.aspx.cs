using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

    public partial class UndislikeVideo : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["VideoID"] != null)
            {
                string video = Request.QueryString["VideoID"];
                string user = Request.QueryString["UserID"];               
                string dislike = Request.QueryString["TotalDislike"];

                con.Open();
                String UpdateVote = "UPDATE [dbo].[Video] SET TotalDislike = @dislike WHERE VideoID =" + video;
                SqlCommand com = new SqlCommand(UpdateVote, con);          
                SqlParameter getDislike = new SqlParameter("@dislike", dislike);             
                com.Parameters.Add(getDislike);
                com.ExecuteNonQuery();
                con.Close();
                con1.Open();
                String InsertVote = "INSERT INTO [dbo].[Vote] (UserID,VideoID,VoteLike,VoteDislike) VALUES (@user,@video,'0','0')";
                SqlCommand com1 = new SqlCommand(InsertVote, con1);
                SqlParameter addUser = new SqlParameter("@user", user);
                SqlParameter addVideo = new SqlParameter("@video", video);
                com1.Parameters.Add(addUser);
                com1.Parameters.Add(addVideo);
                com1.ExecuteNonQuery();
                con1.Close();

            }
        }
    }
