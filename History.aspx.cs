using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

    public partial class History : System.Web.UI.Page
    {
        public String TitleResult { get; set; }
        public String FirstNameResult { get; set; }
        public String LastNameResult { get; set; }
        public String TotalViewResult { get; set; }
        public String DescriptionResult { get; set; }
        public String DateResult { get; set; }
        public String VideoResult { get; set; }
        public String IDResult { get; set; }
        public String userIDResult { get; set; }
        ArrayList TitleGet = new ArrayList();
        ArrayList FirstNameGet = new ArrayList();
        ArrayList LastNameGet = new ArrayList();
        ArrayList TotalViewGet = new ArrayList();
        ArrayList DescriptionGet = new ArrayList();
        ArrayList DateGet = new ArrayList();
        ArrayList VideoGet = new ArrayList();
        ArrayList IDGet = new ArrayList();
        ArrayList getIDfromHistory = new ArrayList();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        String UserID, UserCharacter;
        protected void Page_Load(object sender, EventArgs e)
        {
            con.Open();
            String UserQuery = "SELECT * FROM [dbo].[User] WHERE Email = @email";
            SqlCommand com = new SqlCommand(UserQuery, con);
            SqlParameter CheckEmail = new SqlParameter("@email", Session["LoginEmail"]);
            com.Parameters.Add(CheckEmail);
            SqlDataReader retrieve1 = com.ExecuteReader();

            if (retrieve1.Read())
            {
                UserID = retrieve1["UserID"].ToString();
                MemberName.Text = retrieve1["FirstName"].ToString();
                UserCharacter = retrieve1["Character"].ToString();

            }

            con1.Open();
            String GetFromHistory = "SELECT * FROM [dbo].[History] WHERE UserID="+UserID;
            SqlCommand com1 = new SqlCommand(GetFromHistory,con1);
            SqlDataReader retrieve = com1.ExecuteReader();

            if (retrieve.HasRows)
            {
                while (retrieve.Read())
                {
                    getIDfromHistory.Add(retrieve["VideoID"].ToString());
                }
            }
            
            
            for(int i = 0; i < getIDfromHistory.Count; i++)
            {   
                con2.Open(); 
                String getVideo = "SELECT * FROM [dbo].[Video] WHERE VideoID ="+ getIDfromHistory[i];
                SqlCommand com2 = new SqlCommand(getVideo,con2);
                SqlDataReader retrieve2 = com2.ExecuteReader();
                if (retrieve2.HasRows)
                {
                    while (retrieve2.Read())
                    {
                        TitleGet.Add(retrieve2["Title"].ToString());
                        TotalViewGet.Add(retrieve2["TotalView"].ToString());
                        DescriptionGet.Add(retrieve2["Description"].ToString());
                        DateGet.Add(retrieve2["Date"].ToString());
                        String link = "../Content/" + retrieve2["VideoFile"].ToString();
                        VideoGet.Add(link);
                        IDGet.Add(retrieve2["VideoID"].ToString());

                        con3.Open();
                        String NameQuery = "SELECT * FROM [dbo].[User] WHERE UserID = " + retrieve2["UserID"].ToString();
                        SqlCommand com3 = new SqlCommand(NameQuery, con3);
                        SqlDataReader retrieveName = com3.ExecuteReader();
                        while (retrieveName.Read())
                        {
                            FirstNameGet.Add(retrieveName["FirstName"].ToString());
                            LastNameGet.Add(retrieveName["LastName"].ToString());
                        }
                        con3.Close();
                    }
                }
                con2.Close();
                retrieve2.Close();
            }
           
                    TitleResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(TitleGet);
                    FirstNameResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(FirstNameGet);
                    LastNameResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(LastNameGet);
                    TotalViewResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(TotalViewGet);
                    DateResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(DateGet);
                    DescriptionResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(DescriptionGet);
                    VideoResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(VideoGet);
                    IDResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(IDGet);
                    userIDResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(UserID);

                    
                
            
            

        }
            
       

        

        protected void MemberName_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx?LoginSuccess=1&User=" + UserCharacter);
        }
    }
