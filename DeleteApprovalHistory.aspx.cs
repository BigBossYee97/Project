using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
public partial class DeleteApprovalHistory : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {   
        if(Request.QueryString["VideoID"] != null)
        {
            string id = Request.QueryString["VideoID"];
            con.Open();
            String deleteQuery = "DELETE FROM [dbo].[ApprovalHistory] WHERE ApprovalVideoID = @id";
            SqlCommand com = new SqlCommand(deleteQuery,con);
            SqlParameter getID = new SqlParameter("@id",id);
            com.Parameters.Add(getID);
            com.ExecuteNonQuery();
            con.Close();
        }
    }
}