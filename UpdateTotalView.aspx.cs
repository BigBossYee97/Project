using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

    public partial class UpdateTotalView : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        int updateView;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["VideoID"] != null)
            {
                String id = Request.QueryString["VideoID"];
                con.Open();
                String getCurrentView = "SELECT * FROM [dbo].[Video] WHERE VideoID =" + id;
                SqlCommand com = new SqlCommand(getCurrentView,con);
                SqlDataReader retrieve = com.ExecuteReader();
                if (retrieve.Read())
                {
                    int currentView = Convert.ToInt32(retrieve["TotalView"]);
                    updateView = currentView + 1;
                    con1.Open();
                    String UpdateView = "UPDATE [dbo].[Video] SET TotalView = @view WHERE VideoID =" + id;
                    SqlCommand com1 = new SqlCommand(UpdateView,con1);
                    SqlParameter View = new SqlParameter("@view",updateView);
                    com1.Parameters.Add(View);
                    com1.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    }
