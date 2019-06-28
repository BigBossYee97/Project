using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
public partial class RevertApprovalVideo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
    SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
    SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
 
    String Character, videoid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["VideoID"] != null)
        { videoid = Request.QueryString["VideoID"];
            if (Session["LoginEmail"] != null)
            {
                con.Open();
                String UserQuery = "SELECT * FROM [dbo].[User] WHERE Email = @email";
                SqlCommand com = new SqlCommand(UserQuery, con);
                SqlParameter getEmail = new SqlParameter("@email", Session["LoginEmail"]);
                com.Parameters.Add(getEmail);
                SqlDataReader retrieve = com.ExecuteReader();

                if (retrieve.Read())
                {
                    MemberName.Text = retrieve["FirstName"].ToString();
                    Character = retrieve["Character"].ToString();
                }
                con.Close();

                con1.Open();
                String VideoQuery = "SELECT * FROM [dbo].[Video] WHERE VideoID = @video";
                SqlCommand com1 = new SqlCommand(VideoQuery, con1);
                SqlParameter getVideo = new SqlParameter("@video", videoid);
                com1.Parameters.Add(getVideo);
                SqlDataReader retrieve1 = com1.ExecuteReader();
                if (retrieve1.Read())
                {
                    String VideoFile = retrieve1["VideoFile"].ToString();
                    //  String path = VideoFile.Replace(" ","");

                    String link = "../Content/" + VideoFile;
                    Video.Text = "<video style = 'margin-top:20px' height = 450 width = 900 Controls><Source src =" + link + "type = video/mp4><Source src =" + link + "type = video/webm><Source src =" + link + "type = video/ogg></video>";
                    TitleText.Text = retrieve1["Title"].ToString();
                    DescriptionText.Value = retrieve1["Description"].ToString();
                    CategoryText.Text = retrieve1["Categories"].ToString();
                }
                con1.Close();


            }
        }



    }

    protected void MemberName_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserProfile.aspx?LoginSuccess=1&User=" + Character);
    }

    protected void CancelBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApprovalHistory.aspx?LoginSuccess=1&User=" + Character);
    }

    protected void DeclineBtn_Click(object sender, EventArgs e)
    {
        con2.Open();
        String RejectQuery = "UPDATE [dbo].[Video] SET Status = 'Rejected' WHERE VideoID = @id";
        SqlCommand com2 = new SqlCommand(RejectQuery, con2);
        SqlParameter CheckID = new SqlParameter("@id", videoid);
        com2.Parameters.Add(CheckID);
        com2.ExecuteNonQuery();
        con2.Close();

        con3.Open();
        String GetUserID = "SELECT * FROM [dbo].[Video] WHERE VideoID = @id";
        SqlCommand com3 = new SqlCommand(GetUserID, con3);
        SqlParameter CheckGetUserID = new SqlParameter("@id", videoid);
        com3.Parameters.Add(CheckGetUserID);
        SqlDataReader retrieveUserID = com3.ExecuteReader();
        string UploadBy;
        string VideoName, VideoCategory;
        if (retrieveUserID.Read())
        {
            VideoName = retrieveUserID["Title"].ToString();
            VideoCategory = retrieveUserID["Categories"].ToString();
            UploadBy = retrieveUserID["UserID"].ToString();
            con4.Open();
            String GetUserEmail = "SELECT * FROM [dbo].[User] WHERE UserID =" + UploadBy;
            SqlCommand com4 = new SqlCommand(GetUserEmail, con4);
            SqlDataReader retrieveEmail = com4.ExecuteReader();

            if (retrieveEmail.Read())
            {
                Character = retrieveEmail["Character"].ToString();
                String email = retrieveEmail["Email"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("yee97917@gmail.com", "eelxusivywsyjmcr");
                MailMessage msgobj = new MailMessage();
                msgobj.To.Add(email);
                msgobj.From = new MailAddress("yee97917@gmail.com");
                msgobj.Subject = "Video Rejected #Do Not Reply";
                msgobj.Body = "Sorry, your video was rejected. Please try again or upload another video. Sorry for any incovenience caused." +
                    Environment.NewLine + "Video Name: " + VideoName + Environment.NewLine +
                    "Video Category: " + VideoCategory + Environment.NewLine +
                    Environment.NewLine +
                    "Thank You. Have a Nice Day.";

                client.Send(msgobj);


                con3.Close();
                con4.Close();

                ApprovalLabel.ForeColor = System.Drawing.Color.Red;
                ApprovalLabel.Text = "Video #" + videoid + " Rejected Successfully";

                Response.AddHeader("REFRESH", "2;URL=ApprovalHistory.aspx?LoginSuccess=1&User=" + Character);

            }
        }
    }
}