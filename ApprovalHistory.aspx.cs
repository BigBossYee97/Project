using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Text;
public partial class ApprovalHistory : System.Web.UI.Page
{
    public String getApprovalResult { get; set; }
    public String TitleResult { get; set; }
    public String FirstNameResult { get; set; }
    public String LastNameResult { get; set; }
    public String TotalViewResult { get; set; }   
    public String DateResult { get; set; }    
    public String IDResult { get; set; }
    public String userIDResult { get; set; }
    public String CategoriesResult { get; set; }
    
    ArrayList TitleGet = new ArrayList();
    ArrayList FirstNameGet = new ArrayList();
    ArrayList LastNameGet = new ArrayList();
    ArrayList TotalViewGet = new ArrayList();
    ArrayList DateGet = new ArrayList();
    ArrayList userIDGet = new ArrayList();
    ArrayList IDGet = new ArrayList();
   
    ArrayList CategoryGet = new ArrayList();
    ArrayList ApprovalResult = new ArrayList();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
    SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

    String Character;
    protected void Page_Load(object sender, EventArgs e)
    {
        con.Open();
        String UserQuery = "SELECT * FROM [dbo].[User] WHERE Email = @email";
        SqlCommand com = new SqlCommand(UserQuery,con);
        SqlParameter getEmail = new SqlParameter("@email",Session["LoginEmail"]);
        com.Parameters.Add(getEmail);
        SqlDataReader retrieve = com.ExecuteReader();

        if (retrieve.Read())
        {
            MemberName.Text = retrieve["FirstName"].ToString();
            Character = retrieve["Character"].ToString();
        }

        con.Close();

        con1.Open();
        String ApprovalHistoryQuery = "SELECT * FROM [dbo].[ApprovalHistory]";
        SqlCommand com1 = new SqlCommand(ApprovalHistoryQuery,con1);
        SqlDataReader retrieve1 = com1.ExecuteReader();
        if (retrieve1.HasRows)
        {
            while (retrieve1.Read())
            {
                con2.Open();
                String getVideo = "SELECT * FROM [dbo].[Video] WHERE VideoID="+retrieve1["ApprovalVideoID"].ToString();
                SqlCommand com2 = new SqlCommand(getVideo,con2);
                SqlDataReader retrieve2 = com2.ExecuteReader();
                while (retrieve2.Read())
                {   ApprovalResult.Add(retrieve2["Status"].ToString());
                    TitleGet.Add(retrieve2["Title"].ToString());
                    TotalViewGet.Add(retrieve2["TotalView"].ToString());
                   
                    IDGet.Add(retrieve2["VideoID"].ToString());
                    DateGet.Add(retrieve2["Date"].ToString());
                    CategoryGet.Add(retrieve2["Categories"].ToString());
                }
                con2.Close();

                con3.Open();
                String getUser = "SELECT * FROM [dbo].[User] WHERE UserID =" + retrieve1["ApprovalUserID"].ToString();
                SqlCommand com3 = new SqlCommand(getUser,con3);
                SqlDataReader retrieve3 = com3.ExecuteReader();
                while (retrieve3.Read())
                {
                    FirstNameGet.Add(retrieve3["FirstName"].ToString());
                    LastNameGet.Add(retrieve3["LastName"].ToString());
                }

                con3.Close();
            }
        }
        TitleResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(TitleGet);
        FirstNameResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(FirstNameGet);
        LastNameResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(LastNameGet);
        TotalViewResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(TotalViewGet);
        DateResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(DateGet);
        IDResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(IDGet);
        getApprovalResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ApprovalResult);
        CategoriesResult = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(CategoryGet);


        con1.Close();
    }

    protected void MemberName_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserProfile.aspx?LoginSuccess=1&User="+Character);
    }
}