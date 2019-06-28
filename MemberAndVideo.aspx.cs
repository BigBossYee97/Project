using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Data.SqlClient;
using System.Configuration;
public partial class _Default : System.Web.UI.Page {
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Member"].ConnectionString);
    String Name, character;
    protected void Page_Load(object sender, EventArgs e) {
        System.Collections.IList visibleTables = ASP.global_asax.DefaultModel.VisibleTables;
        if (visibleTables.Count == 0)
        {
            throw new InvalidOperationException("There are no accessible tables. Make sure that at least one data model is registered in Global.asax and scaffolding is enabled or implement custom pages.");
        }
        Menu1.DataSource = visibleTables;
        Menu1.DataBind();
        con.Open();
        String query = "SELECT * FROM [dbo].[User] WHERE Email = @email";
        SqlCommand com = new SqlCommand(query, con);
        SqlParameter CheckEmail = new SqlParameter("@email", Session["LoginEmail"]);
        com.Parameters.Add(CheckEmail);
        SqlDataReader retrieve = com.ExecuteReader();
        if (retrieve.Read())
        {
            Name = retrieve["FirstName"].ToString();
            character = retrieve["Character"].ToString();
           
        }


        (this.Master).nameValue = Name;
        (this.Master).CharValue = character;
    }
    
}
