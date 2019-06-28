using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;

//System.Net.Mail namespace required to send mail.  
using System.Net.Mail;
using System.IO;

    public partial class ForgotPassword : System.Web.UI.Page
    {
        String email;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
             LogInLink.Visible = false;
            con.Open();
            
        }

    

        protected void ResetBtn_Click(object sender, EventArgs e)
        {
            
            email = ForgotPasswordText.Text;
            string EmailValidation = "SELECT Email FROM [dbo].[User] WHERE Email = @email";
            SqlCommand com = new SqlCommand(EmailValidation,con);
            SqlParameter InputEmail = new SqlParameter("@email",email);
            com.Parameters.Add(InputEmail);
            SqlDataReader ValidEmail = com.ExecuteReader();

           
                if (email == "")
             {
                 PassRecoveryMessage.ForeColor = System.Drawing.Color.Red;
                 PassRecoveryMessage.Text = "Please Enter Your Email Address";

             }
            else
            {
                if (ValidEmail.HasRows)
                {
                    Random r = new Random();
                    int num = r.Next();
                    ValidEmail.Read();
                    
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("yee97917@gmail.com", "eelxusivywsyjmcr");
                    MailMessage msgobj = new MailMessage();
                    msgobj.To.Add(ForgotPasswordText.Text);
                    msgobj.From = new MailAddress("yee97917@gmail.com");
                    msgobj.Subject = "Password Recovery #Do Not Reply";
                    msgobj.Body = "Your New Password is " + num + ". Please change your password after log in.";
                    client.Send(msgobj);
                    con1.Open();
                    String Hash_Pass = EncryptPassword(num.ToString());
                    String StoreNewPass = "UPDATE [dbo].[User] SET Password = '" + Hash_Pass + "' WHERE Email = @email";
                    SqlCommand UpdateCommand = new SqlCommand(StoreNewPass, con1);
                    UpdateCommand.Parameters.AddWithValue("@email", email);
                    UpdateCommand.ExecuteNonQuery();

                    PassRecoveryMessage.ForeColor = System.Drawing.Color.Yellow;
                    PassRecoveryMessage.Text = "A New Password Has Been Sent to Your Email";
                    LogInLink.ForeColor = System.Drawing.Color.Blue;
                    LogInLink.Visible = true;
            }
                else
                {
                    PassRecoveryMessage.ForeColor = System.Drawing.Color.Red;
                    PassRecoveryMessage.Text = "Invalid Email";
                }

            }
             
            con.Close();
            con1.Close();
        }
        private String EncryptPassword(String pass)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(pass);
            string EncryptedPass = Convert.ToBase64String(bytes);
            return EncryptedPass;
        }
    }
