using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

    public partial class DeleteVideo : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["VideoID"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["VideoID"]);
                con.Open();
                String DeleteQuery = "DELETE FROM [dbo].[Video] WHERE VideoID =" + id;
                SqlCommand com = new SqlCommand(DeleteQuery,con);
                com.ExecuteNonQuery();
                con.Close();
                con1.Open();
                String DeleteComment = "DELETE FROM[dbo].[Comment] WHERE VideoID = " + id;
                SqlCommand com1 = new SqlCommand(DeleteComment,con1);
                com1.ExecuteNonQuery();
                con1.Close();


            }
        }
    }
