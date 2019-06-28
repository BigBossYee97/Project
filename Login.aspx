<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title> 
    <link href="Content/LoginStyle.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <div id ="LoginWrap" >
    
<pre>  


<b>Login</b>


<asp:Label ID="LoginMessage" runat="server" Text="Please Enter Your Email and Password"></asp:Label>
        
Email         <asp:TextBox ID="InputEmail" class ="InputField" runat="server" placeholder ="Email   " style ="width:300px" OnTextChanged="InputEmail_TextChanged"  ></asp:TextBox>

Password    <asp:TextBox ID="InputPassword" class ="InputField" runat="server" placeholder ="Password" style ="width:300px" TextMode="Password" OnTextChanged="InputPassword_TextChanged"></asp:TextBox>

<asp:HyperLink ID="ForgotPassword" runat="server" NavigateUrl="ForgotPassword.aspx">Forgot Password?</asp:HyperLink>
    
<asp:Button ID="LogInBtn" runat="server" Text="Log In" OnClick="LogInBtn_Click" />


</pre>

    </div>
    </center>
    </form>
    </body>
</html>
