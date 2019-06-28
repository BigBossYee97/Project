using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
public partial class Site : System.Web.UI.MasterPage {

    public string nameValue
    {
        get { return this.MemberName.Text; }
        set { this.MemberName.Text = value; }
    }
    string character;
    public string CharValue
    {
        get { return character; }
        set { character = value; }
    }

    protected void MemberName_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("UserProfile.aspx?LoginSuccess=1&User="+character);
    }
}
