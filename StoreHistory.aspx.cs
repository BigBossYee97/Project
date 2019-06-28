using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

    public partial class StoreHistory : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {   if (Session["LoginEmail"] != null)
            {
                if (Request.QueryString["VideoID"] != null)
                {
                    string video = Request.QueryString["VideoID"];
                    string user = Request.QueryString["UserID"];
                    string HistoryID;
                    con1.Open();
                    String checkHistory = "SELECT * FROM [dbo].[History] WHERE UserID =" + user + "AND VideoID=" + video;
                    SqlCommand com1 = new SqlCommand(checkHistory, con1);
                    SqlDataReader retrieve = com1.ExecuteReader();
                    if (retrieve.HasRows)
                    {
                        con2.Open();
                        if (retrieve.Read())
                        {
                            HistoryID = retrieve["HistoryID"].ToString();
                            String DeletePrevious = "DELETE FROM [dbo].[History] WHERE HistoryID=" + HistoryID;
                            SqlCommand com2 = new SqlCommand(DeletePrevious, con2);
                            com2.ExecuteNonQuery();
                        }
                    }


                    con1.Close();
                    con.Open();
                    String HistoryQuery = "INSERT INTO [dbo].[History] (UserID,VideoID) VALUES (@user,@video)";
                    SqlCommand com = new SqlCommand(HistoryQuery, con);
                    SqlParameter getUser = new SqlParameter("@user", user);
                    SqlParameter getVideo = new SqlParameter("@video", video);
                    com.Parameters.Add(getUser);
                    com.Parameters.Add(getVideo);
                    com.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
