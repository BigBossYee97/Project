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

    public partial class ViewPendingVideo : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con5 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con6 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con7 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con8 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con9 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

    String Character;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["VideoID"] != null)
            {
            if (Session["LoginEmail"] != null)
            {
                con9.Open();
                String UserQuery = "SELECT * FROM [dbo].[User] WHERE Email = @email";
                SqlCommand com9 = new SqlCommand(UserQuery, con9);
                SqlParameter getEmail = new SqlParameter("@email", Session["LoginEmail"]);
                com9.Parameters.Add(getEmail);
                SqlDataReader retrieve9 = com9.ExecuteReader();

                if (retrieve9.Read())
                {
                    MemberName.Text = retrieve9["FirstName"].ToString();
                    Character = retrieve9["Character"].ToString();
                }
                con.Close();

            }
            ID = Request.QueryString["VideoID"];
                con.Open();
          
                String VideoQuery = "SELECT * FROM [dbo].[Video] WHERE VideoID = @id";
                SqlCommand com = new SqlCommand(VideoQuery, con);
                SqlParameter CheckID = new SqlParameter("@id", ID);
                com.Parameters.Add(CheckID);
                SqlDataReader retrieve = com.ExecuteReader();
                
                if (retrieve.Read())
                {
                    
                    String VideoFile = retrieve["VideoFile"].ToString();
                    //  String path = VideoFile.Replace(" ","");
                   
                    String link = "../Content/" + VideoFile;
                    Video.Text = "<video style = 'margin-top:20px' height = 450 width = 900 Controls><Source src =" + link + "type = video/mp4><Source src =" + link + "type = video/webm><Source src =" + link + "type = video/ogg></video>";
                    TitleText.Text = retrieve["Title"].ToString();
                    DescriptionText.Value = retrieve["Description"].ToString();
                    CategoryText.Text = retrieve["Categories"].ToString();
                }
            }
        }

        protected void ApproveBtn_Click(object sender, EventArgs e)
        {
            con2.Open();
            con3.Open();
            String ApproveQuery = "UPDATE [dbo].[Video] SET Status = 'Approved' WHERE VideoID = @id";
            SqlCommand com2 = new SqlCommand(ApproveQuery, con2);
            SqlParameter CheckID = new SqlParameter("@id", ID);
            com2.Parameters.Add(CheckID);
            com2.ExecuteNonQuery();

            String GetUserID = "SELECT * FROM [dbo].[Video] WHERE VideoID = @id";
            SqlCommand com3 = new SqlCommand(GetUserID, con3);
            SqlParameter CheckGetUserID = new SqlParameter("@id", ID);
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
                   // Character = retrieveEmail["Character"].ToString();
                    String email = retrieveEmail["Email"].ToString();
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("yee97917@gmail.com", "eelxusivywsyjmcr");
                    MailMessage msgobj = new MailMessage();
                    msgobj.To.Add(email);
                    msgobj.From = new MailAddress("yee97917@gmail.com");
                    msgobj.Subject = "Video Approved #Do Not Reply";
                    msgobj.Body = "Congratulation, your video was approved by admin. Your video is now available on the system." +
                        Environment.NewLine + "Video Name: " + VideoName + Environment.NewLine +
                        "Video Category: " + VideoCategory + Environment.NewLine +
                        Environment.NewLine +
                        "Thank You. Have a Nice Day.";

                    client.Send(msgobj);

                //Store Approval History
                    con8.Open();
                    String getVideoID = "INSERT INTO [dbo].[ApprovalHistory] (ApprovalVideoID,ApprovalUserID) VALUES (@videoID,@userID)";
                    SqlCommand com8 = new SqlCommand(getVideoID, con8);
                    SqlParameter getApprovalVideoID = new SqlParameter("@videoID",ID);
                    SqlParameter getApprovalUserID = new SqlParameter("@userID",UploadBy);
                    com8.Parameters.Add(getApprovalVideoID);
                    com8.Parameters.Add(getApprovalUserID);
                    com8.ExecuteNonQuery();
                    con8.Close();
                }

            }
            ApprovalLabel.ForeColor = System.Drawing.Color.Yellow;
            ApprovalLabel.Text = "Video #"+ ID +" Approved Successfully";
          
            con2.Close();
            con3.Close();
            con4.Close();
            Response.AddHeader("REFRESH", "2;URL=PendingList.aspx?LoginSuccess=1&User=" + Character);

        }

        protected void DeclineBtn_Click(object sender, EventArgs e)
        {
            con5.Open();
            con6.Open();
            String RejectQuery = "UPDATE [dbo].[Video] SET Status = 'Rejected' WHERE VideoID = @id";
            SqlCommand com5 = new SqlCommand(RejectQuery, con5);
            SqlParameter CheckID = new SqlParameter("@id", ID);
            com5.Parameters.Add(CheckID);
            com5.ExecuteNonQuery();

            String GetUserID = "SELECT * FROM [dbo].[Video] WHERE VideoID = @id";
            SqlCommand com6 = new SqlCommand(GetUserID, con6);
            SqlParameter CheckGetUserID = new SqlParameter("@id", ID);
            com6.Parameters.Add(CheckGetUserID);
            SqlDataReader retrieveUserID = com6.ExecuteReader();
            string UploadBy;
            string VideoName, VideoCategory;
            if (retrieveUserID.Read())
            {   
                VideoName = retrieveUserID["Title"].ToString();
                VideoCategory = retrieveUserID["Categories"].ToString();
                UploadBy = retrieveUserID["UserID"].ToString();
                con7.Open();
                String GetUserEmail = "SELECT * FROM [dbo].[User] WHERE UserID =" + UploadBy;
                SqlCommand com7 = new SqlCommand(GetUserEmail, con7);
                SqlDataReader retrieveEmail = com7.ExecuteReader();

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

                con8.Open();
                String getVideoID = "INSERT INTO [dbo].[ApprovalHistory] (ApprovalVideoID,ApprovalUserID) VALUES (@videoID,@userID)";
                SqlCommand com8 = new SqlCommand(getVideoID, con8);
                SqlParameter getApprovalVideoID = new SqlParameter("@videoID", ID);
                SqlParameter getApprovalUserID = new SqlParameter("@userID", UploadBy);
                com8.Parameters.Add(getApprovalVideoID);
                com8.Parameters.Add(getApprovalUserID);
                com8.ExecuteNonQuery();
                con8.Close();
            }

            }
            ApprovalLabel.ForeColor = System.Drawing.Color.Red;
            ApprovalLabel.Text = "Video #" + ID + " Rejected Successfully";

       

        Response.AddHeader("REFRESH", "2;URL=PendingList.aspx?LoginSuccess=1&User=" + Character);

        }

        protected void MemberName_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx?LoginSuccess=1&User=" + Character);
        }
    }
