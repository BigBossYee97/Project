<%      Response.WriteFile("NavigationBar.html"); %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditVideo.aspx.cs" Inherits="EditVideo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/LoginStyle.css" rel="Stylesheet" type="text/css" />
    <style>
       
        td{
            width:200px;
        }
       
    </style>
</head>
<body>
   <form id="form1" runat="server">
        <div id="BtnDiv">
       <asp:Button id ="GoSignUp" class="NonMemberBtn" runat="server" Text ="Sign Up" ></asp:Button>  <asp:Button id="GoLogIn" class="NonMemberBtn" runat ="server" Text="Log In"  ></asp:Button>
       
        <asp:Button ID ="MemberName"  runat="server" Text="Member" Font-Names="Arial Black" class="NavigateBtn"  OnClick="MemberName_Click"  ></asp:Button>
        
    
        </div>

       <center>
        
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
<div style = "margin-top:20px;margin-bottom:30px;width:790px;margin-left:350px;background-color:black;border-radius:10px;padding:20px">
<table>
<tr><td><pre></pre></td></tr>
<tr><td><pre></pre></td></tr>
<tr><td><pre></pre></td></tr>
<tr><td style ="color:white">Title</td><td><asp:TextBox ID="TitleText" Class ="InputField" placeholder = "Video Title" runat="server" style ="width:300px"  ></asp:TextBox></td></tr>
<tr><td><pre></pre></td></tr>
<tr><td><pre></pre></td></tr>
<tr><td style ="color:white">Description</td><td><textarea  runat ="server" id ="DescriptionText" name ="DescriptionText" class ="InputField" placeholder = "Description.." style ="width:300px;height:100px;overflow:auto;line-height:18px" cols ="20"></textarea></td></tr>
<tr><td><pre></pre></td></tr>
<tr><td><pre></pre></td></tr>
<tr><td style ="color:white">Categories</td><td><asp:DropDownList ID="CategoriesText" Class ="InputField" style ="width:300px" runat="server">
<asp:ListItem></asp:ListItem>
<asp:ListItem Value ="1">Action</asp:ListItem>
<asp:ListItem Value ="2">Adventure</asp:ListItem>
<asp:ListItem Value ="3">Animation</asp:ListItem>
<asp:ListItem Value ="4">Drama</asp:ListItem>
<asp:ListItem Value ="5">History</asp:ListItem>
<asp:ListItem Value ="6">Horror</asp:ListItem>
<asp:ListItem Value ="7">Music</asp:ListItem>
<asp:ListItem Value ="8">Mystery</asp:ListItem>
<asp:ListItem Value ="9">War</asp:ListItem>
<asp:ListItem Value ="10">Sport</asp:ListItem>
<asp:ListItem Value ="11">Others</asp:ListItem>
</asp:DropDownList></td></tr>

</table>       
     
     <asp:Label ID="EditVideoLabel" runat = "server" style ="font-size:16px"></asp:Label><br />
    
    <asp:Button id="ConfirmEditBtn" runat ="server" style="font-size:16px" class = "SbmtBtn" OnClick="ConfirmEditBtn_Click"  Text ="Submit"></asp:Button>
        
</div></center></form>
        
        
    
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
        document.getElementById("MemberNVideo").style.display = "block";
        document.getElementById("ApprovalHistory").style.display = "block";
    }
</script>