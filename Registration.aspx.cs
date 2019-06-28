using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Net;

    public partial class Registration : System.Web.UI.Page
    {
        String first, last, email, password,confpass;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            LogInLink.Text = "";
            con.Open();
            con2.Open();
        }

        protected void LogInBtn_Click(object sender, EventArgs e)
        {
            first = FirstNameText.Text;
            last = LastNameText.Text;
            email = EmailText.Text;
            password = PasswordText.Text;
            confpass = ConfirmPassText.Text;
            

            if (first == "" || last == "" || email == "" || password == "" || confpass == "" )
            {
                RegistrationMessage.ForeColor = System.Drawing.Color.Red;
                RegistrationMessage.Text = "Please Do Not Leave Any Input Field with Blank";
                LogInLink.Text = "";
            }
            else
            {   
                
                string query2 = "SELECT Email FROM [dbo].[User] WHERE Email = @email";
                SqlCommand insertCommand2 = new SqlCommand(query2, con2);
                SqlParameter CheckEmail = new SqlParameter("Email", email);
                insertCommand2.Parameters.Add(CheckEmail);
                SqlDataReader DuplicateEmail = insertCommand2.ExecuteReader();

                    if (DuplicateEmail.HasRows)
                    {
                        DuplicateEmail.Read();
                        RegistrationMessage.ForeColor = System.Drawing.Color.Red;
                        RegistrationMessage.Text = "Email Already Registered Before";
                        LogInLink.ForeColor = System.Drawing.Color.Blue;
                        LogInLink.Text = "Log In Now";
                        DuplicateEmail.Close();
                        con2.Close();
                    }

                    else
                    {

                        if (password == confpass)
                        {
                            if (password.Length > 7) {
                            String Hash_Pass = EncryptPassword(password);

                            string query = "Insert into [dbo].[User] (FirstName,LastName,Email,Password,Character) Values (@First,@Last,@Email,@Pass,@C)";
                            SqlCommand insertCommand = new SqlCommand(query, con);
                            insertCommand.Parameters.AddWithValue("@First", first);
                            insertCommand.Parameters.AddWithValue("@Last", last);
                            insertCommand.Parameters.AddWithValue("@Email", email);
                            insertCommand.Parameters.AddWithValue("@Pass", Hash_Pass);
                            insertCommand.Parameters.AddWithValue("@C", "Member");

                            insertCommand.ExecuteNonQuery();

                            FirstNameText.Text = "";
                            LastNameText.Text = "";
                            EmailText.Text = "";

                            RegistrationMessage.ForeColor = System.Drawing.Color.Yellow;
                            RegistrationMessage.Text = "Account Created Successfully";
                            LogInLink.ForeColor = System.Drawing.Color.Blue;
                            LogInLink.Text = "Log In Now";

                            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                            client.EnableSsl = true;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Credentials = new NetworkCredential("yee97917@gmail.com", "eelxusivywsyjmcr");
                            MailMessage msgobj = new MailMessage();
                            msgobj.To.Add(email);
                            msgobj.From = new MailAddress("yee97917@gmail.com");
                            msgobj.Subject = "Account Created Successfully #Do Not Reply";
                            msgobj.Body = "Congratulation " + first + " " + last + "! Your account is successfully created.";
                            client.Send(msgobj);
                        }
                        else
                        {
                            RegistrationMessage.ForeColor = System.Drawing.Color.Red;
                            RegistrationMessage.Text = "Password Length Must At Least 8 Characters";
                        }
                        }
                        else
                        {
                            RegistrationMessage.ForeColor = System.Drawing.Color.Red;
                            RegistrationMessage.Text = "Please Make Sure Password Is Same With the Confirm Password";
                            LogInLink.Text = "";
                        }
                        con.Close();
                    }
                    
                    
                
            }
            }
        
        private String EncryptPassword(String pass)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(pass);
            string EncryptedPass = Convert.ToBase64String(bytes);
            return EncryptedPass;
        }
    }
