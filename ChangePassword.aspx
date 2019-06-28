<%      Response.WriteFile("NavigationBar.html"); %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="Content/LoginStyle.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">   
     <div id="BtnDiv">
        <asp:Button id ="GoSignUp" class="NonMemberBtn" runat="server" Text ="Sign Up" OnClick="GoSignUp_Click"></asp:Button>  <asp:Button id="GoLogIn" class="NonMemberBtn" runat ="server" Text="Log In" OnClick="GoLogIn_Click" ></asp:Button>
         
        <asp:Button ID ="MemberName" align = "center" runat="server" Text="Member" Font-Names="Arial Black" class="NavigateBtn" OnClick="MemberName_Click" ></asp:Button>
 
         </div>
        
    
     <center>
        <div id= "ProfileWrap" style = "margin-right:200px">
            <pre>


Current Password           <asp:TextBox ID="CurrentPassText" Class ="InputField" placeholder = "Current Password" runat="server" TextMode="Password"></asp:TextBox>


New Password                <asp:TextBox ID="NewPassText" Class ="InputField" placeholder = "New Password" runat="server" TextMode="Password"></asp:TextBox>


Confirm New Password    <asp:TextBox ID="ConfNewPassText" Class ="InputField" placeholder = "Confirm New Password" runat="server" TextMode="Password"></asp:TextBox>
           
     
             <asp:Label ID="ChangePasswordLabel" runat="server" Text= ""></asp:Label>

<asp:Button ID="ChangePassBtn" class = "SbmtBtn" runat="server" Text="Submit" OnClick="ChangePassBtn_Click"></asp:Button>
            
            
            
            </pre>
            </div>
    </form>
        </center>
</body>
</html>
<script>
 var i = 0;
    if (window.location.search.indexOf("LoginSuccess=1") > -1)
    {
        i = 1;
    }
        if (i == 0) {
        document.getElementById("MemberName").style.display = "none";
    }
    else {
        document.getElementById("GoSignUp").style.display = "none";
        document.getElementById("GoLogIn").style.display = "none";
    }

 
    if (window.location.search.indexOf("User=Admin") > -1) {
        document.getElementById("pendingCSS").style.display = "block";
        document.getElementById("ApprovalHistory").style.display = "block";
        document.getElementById("MemberNVideo").style.display = "block";
    }

   
</script>