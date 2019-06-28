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

    public partial class EditUserProfile : System.Web.UI.Page
    { 
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
       
        Boolean TextChanged = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            FirstNameText.TextChanged += new System.EventHandler(this.FirstNameText_TextChanged);
            

            con.Open();
            con2.Open();
            String query = "SELECT * FROM [dbo].[User] WHERE Email = @email";
            SqlCommand com = new SqlCommand(query, con);
            SqlParameter CheckEmail = new SqlParameter("Email", Session["LoginEmail"]);
            com.Parameters.Add(CheckEmail);
            SqlDataReader retrieve = com.ExecuteReader();
            if (!Page.IsPostBack) { 
            if (retrieve.Read())
            {
                FirstNameText.Text = retrieve["FirstName"].ToString();
                LastNameText.Text = retrieve["LastName"].ToString();
                GetEmail.Text = retrieve["Email"].ToString();
                GetCharacter.Text = retrieve["Character"].ToString();
                MemberName.Text = retrieve["FirstName"].ToString();

            }
            }
            con.Close();
        }

        protected void MemberName_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx?LoginSuccess=1&User=" + GetCharacter.Text);
        }

        

        protected void FirstNameText_TextChanged(object sender, EventArgs e)
        {
            TextChanged = true;
        }

        protected void SubmitProfile_Click(object sender, EventArgs e)
        {
            
            HttpPostedFile ChoosenImage = imageUpload1.PostedFile;
            string ImageName = Path.GetFileName(ChoosenImage.FileName);
            string FileFormat = Path.GetExtension(ImageName);
           // int ImageSize = ChoosenImage.ContentLength;
            if(ImageName == "")
            {
                String UpdateImage = "UPDATE [dbo].[User] SET FirstName = @newFirst, LastName = @newLast WHERE Email = @email";
                SqlCommand ImageStore = new SqlCommand(UpdateImage, con2);
                
                ImageStore.Parameters.AddWithValue("@newFirst", FirstNameText.Text);
                ImageStore.Parameters.AddWithValue("@newLast", LastNameText.Text);
                ImageStore.Parameters.AddWithValue("@email", Session["LoginEmail"]);
                ImageStore.ExecuteNonQuery();
                SaveChangesMessage.ForeColor = System.Drawing.Color.Yellow;
                SaveChangesMessage.Text = "Profile Updated Successfully";

               

                Response.AddHeader("REFRESH", "2;URL=UserProfile.aspx?LoginSuccess=1&User=" + GetCharacter.Text);
            }
            else { 
            if (FileFormat.ToLower() == ".jpg" || FileFormat.ToLower() == ".bmp" || FileFormat.ToLower() == ".gif" || FileFormat.ToLower() == ".png")
            {
                String UpdateImage = "UPDATE [dbo].[User] SET IMAGE = @img, FirstName = @newFirst, LastName = @newLast WHERE Email = @email";
                SqlCommand ImageStore = new SqlCommand(UpdateImage,con2);
                ImageStore.Parameters.AddWithValue("@img", ImageName);
                ImageStore.Parameters.AddWithValue("@newFirst", FirstNameText.Text);
                ImageStore.Parameters.AddWithValue("@newLast", LastNameText.Text);
                ImageStore.Parameters.AddWithValue("@email",Session["LoginEmail"]);
                ImageStore.ExecuteNonQuery();
                SaveChangesMessage.ForeColor = System.Drawing.Color.Yellow;
                SaveChangesMessage.Text = "Profile Updated Successfully";

                imageUpload1.SaveAs(Server.MapPath("Content\\" + ImageName));

                Response.AddHeader("REFRESH", "2;URL=UserProfile.aspx?LoginSuccess=1&User=" + GetCharacter.Text);
            }
            else
            {
                SaveChangesMessage.ForeColor = System.Drawing.Color.Red;
                SaveChangesMessage.Text = "Invalid Format. Only JPG, GIF, PNG, BMP Can Be Uploaded";
            }
            con2.Close();
            }
        }

        
    }

