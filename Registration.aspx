<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/LoginStyle.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <center>
        <div id ="LoginWrap" style ="height:600px">
<pre>


<b>Sign Up</b>

First Name              <asp:TextBox ID="FirstNameText" style ="width:300px" class ="InputField" runat="server" placeholder ="First Name"></asp:TextBox>

Last Name               <asp:TextBox ID="LastNameText" style ="width:300px" class ="InputField" runat="server" placeholder ="Last Name"></asp:TextBox>

Email                       <asp:TextBox ID="EmailText" style ="width:300px" class ="InputField" runat="server" placeholder ="Email Address"  ></asp:TextBox>

Password                 <asp:TextBox ID="PasswordText" style ="width:300px" class ="InputField" runat="server" placeholder ="Password" TextMode="Password"></asp:TextBox>

Confirm Password     <asp:TextBox ID="ConfirmPassText" style ="width:300px" class ="InputField" runat="server" placeholder ="Confirm Password" TextMode="Password"></asp:TextBox>

<asp:Label ID="RegistrationMessage" runat="server" Text=""></asp:Label>
<asp:HyperLink ID="LogInLink" runat="server" NavigateUrl="Login.aspx">[LogInLink]</asp:HyperLink>

<asp:Button ID="LogInBtn" runat="server" Text="Submit" OnClick="LogInBtn_Click" /></pre>    
        </div>
        </center>
    </form>
</body>
</html>
