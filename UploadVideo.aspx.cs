using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;

    public partial class UploadVideo : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con5 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        String UserUploadString, UserCharacter,Email;
        protected void Page_Load(object sender, EventArgs e)
        {
            con2.Open();
            String UserQuery = "SELECT * FROM [dbo].[User] WHERE Email = @email";
            SqlCommand com1 = new SqlCommand(UserQuery, con2);
            SqlParameter CheckEmail = new SqlParameter("@email", Session["LoginEmail"]);
            com1.Parameters.Add(CheckEmail);
            SqlDataReader retrieve = com1.ExecuteReader();
            
                if (retrieve.Read())
            {
                UserUploadString = retrieve["UserID"].ToString();
                MemberName.Text = retrieve["FirstName"].ToString();
                UserCharacter = retrieve["Character"].ToString();
                Email = retrieve["Email"].ToString();
            }
            con2.Close();
        }

        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            HttpPostedFile ChoosenVideo = VideoUpload.PostedFile;
            string VideoName = Path.GetFileName(ChoosenVideo.FileName);
            string VideoFormat = Path.GetExtension(VideoName);
            string CatIndex = CategoriesText.SelectedItem.Value;
            int VideoSize = ChoosenVideo.ContentLength;
            if (VideoName == "")
            {
                UploadLabel.ForeColor = System.Drawing.Color.Red;
                UploadLabel.Text = "Please Select a Video";
            }
            else
            {   if (VideoFormat.ToLower() == ".mp4" || VideoFormat.ToLower() == ".ogg" || VideoFormat.ToLower() == ".webm")
                {
                    if (VideoSize > 50000000)
                    {

                        UploadLabel.ForeColor = System.Drawing.Color.Red;
                        UploadLabel.Text = "Video Exceeded the Size Limit. Please Try Again";
                    }
                    else
                    {
                       
                      
                        string textarea = DescriptionText.Value;
                        string category = CategoriesText.SelectedItem.Text;
                        string today = DateTime.Today.ToString("dd/MM/yyyy");
                        int UserUpload = Convert.ToInt32(UserUploadString);

                        if (TitleText.Text == "" || category == "" || textarea == "")
                        {
                            UploadLabel.ForeColor = System.Drawing.Color.Red;
                            UploadLabel.Text = "Please Make Sure All Information are Filled";
                        }
                        else
                        {

                        con3.Open();
                        String DuplicateVideoName = "SELECT * FROM [dbo].[Video] WHERE Title = @title";
                        SqlCommand com3 = new SqlCommand(DuplicateVideoName, con3);
                        SqlParameter getVideoName = new SqlParameter("@title", TitleText.Text);
                        com3.Parameters.Add(getVideoName);
                        SqlDataReader retrieve3 = com3.ExecuteReader();
                        if (retrieve3.HasRows)
                        {
                            UploadLabel.ForeColor = System.Drawing.Color.Red;
                            UploadLabel.Text = "Video's Title Existed. Please Try Another One";

                        }
                        else
                        {
                            String path = VideoName.Replace(" ", "");
                            con.Open();
                            String query = "INSERT INTO [dbo].[Video] (VideoFile,Title,Description,Categories,CategoriesIndex,TotalView,TotalLike,TotalDislike,Date,Status,UserID) VALUES (@video,@title,@description,@category,@index,'0','0','0',@today,'Pending',@userID)";
                            SqlCommand com = new SqlCommand(query, con);
                            SqlParameter addVideo = new SqlParameter("@video", path);
                            SqlParameter addTitle = new SqlParameter("@title", TitleText.Text);
                            SqlParameter addDesc = new SqlParameter("@description", textarea);
                            SqlParameter addcategory = new SqlParameter("@category", category);
                            SqlParameter addIndex = new SqlParameter("@index", CatIndex);
                            SqlParameter addDate = new SqlParameter("@today", today);
                            SqlParameter addUserID = new SqlParameter("@userID", UserUpload);
                            com.Parameters.Add(addVideo);
                            com.Parameters.Add(addTitle);
                            com.Parameters.Add(addDesc);
                            com.Parameters.Add(addcategory);
                            com.Parameters.Add(addIndex);
                            com.Parameters.Add(addDate);
                            com.Parameters.Add(addUserID);
                            com.ExecuteNonQuery();


                            VideoUpload.SaveAs(Server.MapPath("Content\\" + path));
                            UploadLabel.ForeColor = System.Drawing.Color.Yellow;
                            UploadLabel.Text = "Video Uploaded Successfully";
                            con.Close();
                            DescriptionText.Value = "";
                            TitleText.Text = "";

                            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                            client.EnableSsl = true;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Credentials = new NetworkCredential("yee97917@gmail.com", "eelxusivywsyjmcr");
                            MailMessage msgobj = new MailMessage();
                            msgobj.To.Add(Email);
                            msgobj.From = new MailAddress("yee97917@gmail.com");
                            msgobj.Subject = "Video Uploaded Successfully #Do Not Reply";
                            msgobj.Body = "Congratulation, your video was uploaded successfully. Please be patient for the approval." +
                                Environment.NewLine + "Video Name: " + VideoName + Environment.NewLine +
                                "Video Category: " + category + Environment.NewLine +
                                Environment.NewLine +
                                "Thank You. Have a Nice Day.";

                            client.Send(msgobj);


                           
                        }
                        con3.Close();
                        con.Close();

                        }
                    }
                }
                else
                {
                    UploadLabel.ForeColor = System.Drawing.Color.Red;
                    UploadLabel.Text = "Video Format Not Supported. Please Try Again.";
                }
            }
           
        }

        protected void MemberName_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx?LoginSuccess=1&User=" + UserCharacter);
        }
    }
