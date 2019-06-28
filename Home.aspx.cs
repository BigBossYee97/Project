using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Collections;

    public partial class Home : System.Web.UI.Page
    {
        public String CategoriesResult { get; set; }
        public String TitleResult { get; set; }
        public String ViewResult { get; set; }
        public String VideoCategoryResult { get; set; }
        public String NameResult { get; set; }
        public String DateResult { get; set; }
        public String PathResult { get; set; }
        public String VideoIDResult { get; set; }
        public String userIDResult { get; set; }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);


        String UserID, userCharacter;
        ArrayList CategoriesAvailable = new ArrayList();
        ArrayList TitleList = new ArrayList();
        ArrayList ViewList = new ArrayList();
        ArrayList VideoCategoryList = new ArrayList();
        ArrayList DateList = new ArrayList();
        ArrayList NameList = new ArrayList();
        ArrayList PathList = new ArrayList();
        ArrayList IDList = new ArrayList();

        protected void GoSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }

        protected void GoLogIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginEmail"] != null)
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
                    userCharacter = retrieve1["Character"].ToString();

                }
                con.Close();



            }
            con1.Open();
            String getCategories = "SELECT DISTINCT Categories FROM [dbo].[Video] WHERE Status = 'Approved'";
            SqlCommand com1 = new SqlCommand(getCategories, con1);
            SqlDataReader retrieve2 = com1.ExecuteReader();
            while (retrieve2.Read())
            {
                CategoriesAvailable.Add(retrieve2["Categories"].ToString());

            }
            con1.Close();

            con3.Open();
            String getVideo = "SELECT * FROM [dbo].[Video] WHERE Status = 'Approved'";
            SqlCommand com3 = new SqlCommand(getVideo, con3);
            SqlDataReader retrieve3 = com3.ExecuteReader();
            while (retrieve3.Read())
            {
                IDList.Add(retrieve3["VideoID"].ToString());
                TitleList.Add(retrieve3["Title"].ToString());
                ViewList.Add(retrieve3["TotalView"].ToString());
                VideoCategoryList.Add(retrieve3["Categories"].ToString());
                DateList.Add(retrieve3["Date"].ToString());
                String link = "../Content/" + retrieve3["VideoFile"].ToString();
                PathList.Add(link);
                con4.Open();
                String getName = "SELECT * FROM [dbo].[User] WHERE UserID=" + retrieve3["UserID"].ToString();
                SqlCommand com4 = new SqlCommand(getName, con4);
                SqlDataReader retrieve4 = com4.ExecuteReader();
                while (retrieve4.Read())
                {
                    NameList.Add(retrieve4["FirstName"].ToString() + " " + retrieve4["LastName"].ToString());
                }
                con4.Close();
            }
            con3.Close();
            CategoriesResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(CategoriesAvailable);
            TitleResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(TitleList);
            ViewResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ViewList);
            VideoCategoryResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(VideoCategoryList);
            NameResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(NameList);
            DateResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(DateList);
            PathResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(PathList);
            VideoIDResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(IDList);
            userIDResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(UserID);


        }

    protected void MemberName_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserProfile.aspx?LoginSuccess=1&User="+userCharacter);
    }
}
