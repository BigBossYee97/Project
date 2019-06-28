using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

    public partial class EditVideo : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        StringBuilder vid = new StringBuilder();
        String userID,character,videoID;
        
        protected void ConfirmEditBtn_Click(object sender, EventArgs e)
        {
            
                string title = TitleText.Text;
                string descrp = DescriptionText.Value;
                string category = CategoriesText.SelectedItem.Text;
                int index = Convert.ToInt32(CategoriesText.SelectedItem.Value);
                
                if (title == "" || descrp == "" || index < 1)
                {
                    EditVideoLabel.ForeColor = System.Drawing.Color.Red;
                    EditVideoLabel.Text = "Please Make Sure Every Column is Filled Before Submit Changes";
                }
                else
                {
                    con2.Open();
                    String EditQuery = "UPDATE [dbo].[Video] SET Title = @title1, Description = @descrip1, Categories = @categories1, CategoriesIndex = @index1 WHERE VideoID = @id1";
                    SqlCommand com2 = new SqlCommand(EditQuery, con2);
                    SqlParameter checkTitle1 = new SqlParameter("@title1", title);
                    SqlParameter checkDescrp1 = new SqlParameter("@descrip1", descrp);
                    SqlParameter checkCategory1 = new SqlParameter("@categories1", category);
                    SqlParameter checkIndex1 = new SqlParameter("@index1", index);
                    SqlParameter checkID1 = new SqlParameter("@id1", videoID);

                    com2.Parameters.Add(checkTitle1);
                    com2.Parameters.Add(checkDescrp1);
                    com2.Parameters.Add(checkCategory1);
                    com2.Parameters.Add(checkIndex1);
                    com2.Parameters.Add(checkID1);
                    com2.ExecuteNonQuery();


                    EditVideoLabel.ForeColor = System.Drawing.Color.Yellow;
                    EditVideoLabel.Text = "Submitted Changes Successfully";

                    Response.AddHeader("REFRESH", "2;URL=MyChannel.aspx?LoginSuccess=1&User=" + character);

                }
                con2.Close();
            
            
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["VideoID"] != null)
                {
                    videoID = Request.QueryString["VideoID"];
                }
                con.Open();
                String UserQuery = "SELECT * FROM [dbo].[User] WHERE Email = @email";
                SqlCommand com = new SqlCommand(UserQuery, con);
                SqlParameter CheckEmail = new SqlParameter("@email", Session["LoginEmail"]);
                com.Parameters.Add(CheckEmail);
                SqlDataReader retrieve = com.ExecuteReader();

                if (retrieve.Read())
                {
                    MemberName.Text = retrieve["FirstName"].ToString();
                    userID = retrieve["UserID"].ToString();
                    character = retrieve["Character"].ToString();
                }

                con.Close();
                con1.Open();
                String VideoQuery = "SELECT * FROM [dbo].[Video] WHERE Status = 'Approved' AND VideoID =" + videoID;
                SqlCommand com1 = new SqlCommand(VideoQuery, con1);
                SqlDataReader retrieve1 = com1.ExecuteReader();
                vid.Append("<table style = 'margin-left:350px;margin-top:50px'>");
                vid.Append("<tr>");

                if (retrieve1.HasRows)
                {

                    if (retrieve1.Read())
                    {
                        String VideoFile = retrieve1["VideoFile"].ToString();
                        String link = "../Content/" + VideoFile;
                        String title = retrieve1["Title"].ToString();
                        String View = retrieve1["TotalView"].ToString();
                        String Date = retrieve1["Date"].ToString();
                        String Descrp = retrieve1["Description"].ToString();
                        String VidID = retrieve1["VideoID"].ToString();
                        String Like = retrieve1["TotalLike"].ToString();
                        String Dislike = retrieve1["TotalDislike"].ToString();
                        int Categories = Convert.ToInt32(retrieve1["CategoriesIndex"]);

                    if (!Page.IsPostBack) { 
                        TitleText.Text = title;
                        DescriptionText.InnerText = Descrp;
                        CategoriesText.SelectedIndex = Categories;

                        }
                        vid.Append("<tr>");

                        vid.Append("<td style ='margin-left:20px;background-color:black;border-radius:10px;padding:20px'><video style = 'margin-right:50px' width = 700 height = 400 Controls><Source src =" + link + "type = video/mp4><Source src =" + link + "type = video/webm><Source src =" + link + "type = video/ogg></video><br><a href = '#' style = 'color:white' onclick = 'LinkVal(this)' class = 'getVideoID' id = " + videoID + ">" + title + "</a><i style = 'float:right;margin-right:50px;'></i><br><i style = 'font-size:14px;color:white'>" + View + " views <i style = 'margin-left:520px'>Likes " + Like + " </i> <i>Dislikes " + Dislike + "</i><br><i>" + Date + "</i></i></a><br><br><h3 style = 'color:white'>Description</h3><i style = 'color:white'>" + Descrp + "</i>");

                        vid.Append("</td></tr>");







                    }


                }

                vid.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = vid.ToString() });
            
        }
       
         
        


        protected void MemberName_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx?LoginSuccess=1&User="+character);
        }
    }
