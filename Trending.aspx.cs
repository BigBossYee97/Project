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

    public partial class Trending : System.Web.UI.Page
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
        StringBuilder List = new StringBuilder();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        String UserID, UserCharacter;
        protected void Page_Load(object sender, EventArgs e)
        {       if (Session["LoginEmail"] != null)
            {
                con1.Open();
                String UserQuery = "SELECT * FROM [dbo].[User] WHERE Email = @email";
                SqlCommand com1 = new SqlCommand(UserQuery, con1);
                SqlParameter CheckEmail = new SqlParameter("@email", Session["LoginEmail"]);
                com1.Parameters.Add(CheckEmail);
                SqlDataReader retrieve1 = com1.ExecuteReader();

                if (retrieve1.Read())
                {
                    UserID = retrieve1["UserID"].ToString();
                    MemberName.Text = retrieve1["FirstName"].ToString();
                    UserCharacter = retrieve1["Character"].ToString();

                }
            }     
            
            con.Open();
            String TrendingQuery = "SELECT TOP 10 * FROM [dbo].[Video] WHERE Status = 'Approved' ORDER BY TotalView DESC";
            SqlCommand com = new SqlCommand(TrendingQuery,con);
            SqlDataReader retrieve = com.ExecuteReader();
            while (retrieve.Read())
            {

                con2.Open();
                String NameQuery = "SELECT * FROM [dbo].[User] WHERE UserID = " + retrieve["UserID"].ToString();
                SqlCommand com2 = new SqlCommand(NameQuery,con2);
                SqlDataReader retrieveName = com2.ExecuteReader();
                while (retrieveName.Read())
                {
                    FirstNameGet.Add(retrieveName["FirstName"].ToString());
                    LastNameGet.Add(retrieveName["LastName"].ToString());
                }
                con2.Close();
                TitleGet.Add(retrieve["Title"].ToString());
                TotalViewGet.Add(retrieve["TotalView"].ToString());
                DescriptionGet.Add(retrieve["Description"].ToString());
                DateGet.Add(retrieve["Date"].ToString());
                String link = "../Content/" + retrieve["VideoFile"].ToString();
                VideoGet.Add(link);
                IDGet.Add(retrieve["VideoID"].ToString());

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







        }

        protected void GoSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }

        protected void GoLogIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void MemberName_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx?LoginSuccess=1&User="+ UserCharacter);
        }
    }
