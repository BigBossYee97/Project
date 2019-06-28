using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class UnlikeVideo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        if (Request.QueryString["VideoID"] != null)
        {
            string video = Request.QueryString["VideoID"];
            string user = Request.QueryString["UserID"];
            string like = Request.QueryString["TotalLike"];

            con.Open();
            String UpdateVote = "UPDATE [dbo].[Video] SET TotalLike = @like WHERE VideoID =" + video;
            SqlCommand com = new SqlCommand(UpdateVote, con);
            SqlParameter getDislike = new SqlParameter("@like", like);
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
