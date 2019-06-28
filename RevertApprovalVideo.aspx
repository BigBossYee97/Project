<%      Response.WriteFile("NavigationBar.html"); %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RevertApprovalVideo.aspx.cs" Inherits="RevertApprovalVideo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/LoginStyle.css" rel="Stylesheet" type="text/css" />
 <style>
        #VideoWrap {
            background: black;
            width: 900px;
            height: 900px;
            border-radius: 45px;
            margin-top: 100px;
            margin-left: 550px;
            color: white;
            margin-bottom:50px;
        }
        td{
            width:200px;
        }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div id="BtnDiv">
        <asp:Button id ="GoSignUp" class="NonMemberBtn" runat="server" Text ="Sign Up" ></asp:Button>  <asp:Button id="GoLogIn" class="NonMemberBtn" runat ="server" Text="Log In" ></asp:Button>
         
        <asp:Button ID ="MemberName" align = "center" runat="server" Text="Member" Font-Names="Arial Black" class="NavigateBtn" OnClick="MemberName_Click" ></asp:Button>
 
         </div>

        <div id = "VideoWrap">
            <asp:Literal ID="Video" runat="server"></asp:Literal>
            <table style = "margin-left:200px">
<tr><td><pre></pre></td></tr>
<tr><td><pre></pre></td></tr>
<tr><td><pre></pre></td></tr>
<tr><td>Title</td><td><asp:TextBox disabled = "true" ID="TitleText" Class ="InputField" placeholder = "Video Title" runat="server" style ="width:300px"  ></asp:TextBox></td></tr>
<tr><td><pre></pre></td></tr>
<tr><td><pre></pre></td></tr>
<tr><td>Description</td><td><textarea disabled = "disabled" runat ="server" id ="DescriptionText" name ="DescriptionText" class ="InputField" placeholder = "Description.." style ="width:300px;height:100px;overflow:auto;line-height:18px" cols ="20"></textarea></td></tr>
<tr><td><pre></pre></td></tr>
<tr><td><pre></pre></td></tr>
<tr><td>Categories</td><td><asp:TextBox disabled = "true" ID="CategoryText" Class ="InputField" style ="width:300px" runat="server"></asp:TextBox></td></tr>
<tr><td><pre></pre></td></tr>
<tr><td><pre></pre></td></tr>          
<tr><td><pre></pre></td></tr>

</table>       
     <center>
     <asp:Label ID="ApprovalLabel" runat="server" Text=""></asp:Label><br />
     <asp:Button ID="DeclineBtn" class = "SbmtBtn" runat="server" Text="Reject" style="font-size:16px" OnClick="DeclineBtn_Click"  ></asp:Button> <asp:Button ID="CancelBtn" style="font-size:16px" class = "SbmtBtn" runat="server" Text="Cancel" OnClick="CancelBtn_Click"  ></asp:Button> 
     </center>   

        </div>
    </form>
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