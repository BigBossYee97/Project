<%      Response.WriteFile("NavigationBar.html"); %>
<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="Content/LoginStyle.css" rel="Stylesheet" type="text/css" />
    
    <style>
        .hide{display:none;}
.btn {

font-size: 14px;
vertical-align: middle;
cursor: pointer;
border: 1px solid #bbbbbb;
border-color: #e6e6e6 #e6e6e6 #bfbfbf;
border-color: rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.25);
border-bottom-color: #a2a2a2;
-webkit-border-radius: 4px;
-moz-border-radius: 4px;
border-radius: 4px;
position:absolute;
}

.LabelAlign{
    float:left;
    margin-left:40px;
    font-family:Arial Black;
    font-size:16px;
}

.dropbtn {
  background-color: #3498DB;
  color: white;
  padding: 16px;
  font-size: 16px;
  border: none;
  cursor: pointer;
  width: 80px;
  height: 30px;
  float: right;
}

.dropbtn:hover, .dropbtn:focus {
  background-color: #2980B9;
}

.dropdown {
  position: relative;
  display: inline-block;
}

.dropdown-content {
  display: none;
  position: absolute;
  background-color: #f1f1f1;
  min-width: 160px;
  overflow: auto;
  box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
  z-index: 1;
}

.dropdown-content a {
  color: black;
  padding: 12px 16px;
  text-decoration: none;
  display: block;
}

.dropdown a:hover {background-color: #ddd;}

.show {display: block;}

</style>
</head>
<body>
    <form id="form1" runat="server" enctype = "multipart/form-data">   
    <div id="BtnDiv">
       <asp:Button id ="GoSignUp" class="NonMemberBtn" runat="server" Text ="Sign Up" OnClick="GoSignUp_Click"></asp:Button>  <asp:Button id="GoLogIn" class="NonMemberBtn" runat ="server" Text="Log In" OnClick="GoLogIn_Click" ></asp:Button>
       
        <asp:Button ID ="MemberName"  runat="server" Text="Member" Font-Names="Arial Black" class="NavigateBtn" OnClick="MemberName_Click" ></asp:Button>
        
    
        </div>
    
   
    
         
    <center>
<div style = "margin-top:0px;position:absolute;border-radius:45px;background-color:black;width:300px;height:300px;margin-left:450px">
<asp:Image ImageUrl = "..." id="imagePreview1" alt="Preview Image" runat = "server" Style = "border-radius:45px;width:300px;height:300px"></asp:Image></div>
<div id = "ProfileWrap">
<pre>


<asp:Label ID="FirstName" class ="LabelAlign" runat="server" Text="FirstName"></asp:Label>                             :   <asp:Label ID="GetFirstName"  Font-Names="Arial Black" runat="server" Text=""></asp:Label>


<asp:Label ID="LastName" class ="LabelAlign" runat="server" Text="LastName"></asp:Label>                            :   <asp:Label ID="GetLastName"  Font-Names="Arial Black" runat="server" Text=""></asp:Label>


<asp:Label ID="Email" class ="LabelAlign" runat="server" Text="Email"></asp:Label>                                                :   <asp:Label ID="GetEmail" Font-Names="Arial Black" runat="server" Text=""></asp:Label>


<asp:Label ID="Character" class ="LabelAlign" runat="server" Text="Character"></asp:Label>   :   <asp:Label ID="GetCharacter" Font-Names="Arial Black" runat="server" Text=""></asp:Label>





<asp:Button ID="EditProfile" class = "SbmtBtn" runat="server" Text="Edit Profile" OnClick="EditProfile_Click"></asp:Button>                        <asp:Button ID="ChangePassword" class = "SbmtBtn" runat="server" Text="Change Password" OnClick="ChangePassword_Click"></asp:Button>







</pre>  </div>
        </center>
        
       
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