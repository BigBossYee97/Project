<%      Response.WriteFile("NavigationBar.html"); %>
<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="EditUserProfile.aspx.cs" Inherits="EditUserProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
        <button id ="GoSignUp" class="NonMemberBtn" onclick="Registration()">Sign Up</button>  <button id="GoLogIn" class="NonMemberBtn" onclick="LogIn()">Log In</button>
      
        <asp:Button ID ="MemberName" align = "center" runat="server" Text="Member" Font-Names="Arial Black" class="NavigateBtn" OnClick="MemberName_Click" ></asp:Button>
        
        </div>
    
   
    
         
    <center>
<div style = "margin-top:0px;position:absolute;border-radius:45px;background-color:black;width:300px;height:300px;margin-left:450px">
<asp:FileUpload  name="image1" id="imageUpload1" runat ="server" class="hide"/>
<label for="imageUpload1" class="btn btn-large" style = "margin-left:15px;color:white;background-color:none;border:none;font-size:14px;font-family:Arial Black">Image 1</label>
<img src = "-" id="imagePreview1" alt="Preview Image" style = "border-radius:45px;width:300px;height:300px"></img></div>
<div id = "ProfileWrap">
<pre>


<asp:Label ID="FirstName" class ="LabelAlign" runat="server" Text="FirstName"></asp:Label>                            :   <asp:TextBox ID="FirstNameText"  Font-Names="Arial Black" runat="server" OnTextChanged="FirstNameText_TextChanged"  ></asp:TextBox>


<asp:Label ID="LastName" class ="LabelAlign" runat="server" Text="LastName"></asp:Label>                            :   <asp:TextBox ID="LastNameText"  Font-Names="Arial Black" runat="server"  ></asp:TextBox>


<asp:Label ID="Email" class ="LabelAlign" runat="server" Text="Email"></asp:Label>                                                         :   <asp:Label ID="GetEmail" Font-Names="Arial Black" runat="server" Text ="" ></asp:Label>


<asp:Label ID="Character" class ="LabelAlign" runat="server" Text="Character"></asp:Label>            :   <asp:Label ID="GetCharacter" Font-Names="Arial Black" runat="server" Text = ""></asp:Label>


<asp:Label ID="SaveChangesMessage" runat="server" Text=""></asp:Label>


<asp:Button ID="SubmitProfile" runat="server" Text="Save Changes" class = "SbmtBtn" OnClick="SubmitProfile_Click"></asp:Button>



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
  


$('#imageUpload1').change(function(){			
			readImgUrlAndPreview(this);
			function readImgUrlAndPreview(input){
			 if (input.files && input.files[0]) {		 
			            var reader = new FileReader();			
			            reader.onload = function (e) {		            	
			                $('#imagePreview1').attr('src', e.target.result);		
							}
			          };
			          reader.readAsDataURL(input.files[0]);
			     }	
		});


</script>