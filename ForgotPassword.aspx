<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="Content/LoginStyle.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <center>
        <div id ="LoginWrap">
<pre id="ResetPassBtn">


    <b>Password Recovery</b>


<asp:Label ID="PassRecoveryMessage" runat="server" Text="Please Enter Your Email Address to Reset Password"></asp:Label>


Email   <asp:TextBox ID="ForgotPasswordText" style = 'width:300px' CssClass ="InputField" runat="server" Placeholder = "Email Address" ></asp:TextBox>
  
<asp:HyperLink ID="LogInLink" runat="server" NavigateUrl="Login.aspx">Log In Now</asp:HyperLink>

<asp:Button ID="ResetBtn"  runat="server" Text="Submit" OnClick="ResetBtn_Click" />
       
       </pre> </div>
        </center>
    </form>
</body>
</html>
