<%      Response.WriteFile("NavigationBar.html"); %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PendingList.aspx.cs" Inherits="PendingList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style>
table{
    background-color:white;
    color:black;
    margin-top:30px;
    margin-left:450px;
    width:70%;
    text-align:center;
}
th{
    background-color:#4cbac4;
    height:50px;
}
tr:nth-child(even) {background-color: #f2f2f2;}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="BtnDiv">
        <button id ="GoSignUp" class="NonMemberBtn" onclick="Registration()">Sign Up</button>  <button id="GoLogIn" class="NonMemberBtn" onclick="LogIn()">Log In</button>
      
        <asp:Button ID ="MemberName" align = "center" runat="server" Text="Member" Font-Names="Arial Black" class="NavigateBtn" OnClick="MemberName_Click" ></asp:Button>
        
        </div>
       
        <div>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
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

    function ViewVideo(e) {
      
        var VideoID = e.parentNode.childNodes[1].innerHTML;
        window.location.href = "ViewPendingVideo.aspx?LoginSuccess=1&User=Admin&VideoID="+VideoID;
    }
    
   
</script>