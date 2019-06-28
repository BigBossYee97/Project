<%      Response.WriteFile("NavigationBar.html"); %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovalHistory.aspx.cs" Inherits="ApprovalHistory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/LoginStyle.css" rel="Stylesheet" type="text/css" />
    <style>
        .btn.active {                
	display: none;		
}

.btn span:nth-of-type(1)  {            	
	display: none;
}
.btn span:last-child  {            	
	display: block;		
}

.btn.active  span:nth-of-type(1)  {            	
	display: block;		
}
.btn.active span:last-child  {            	
	display: none;			
}

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
        <asp:Button id ="GoSignUp" class="NonMemberBtn" runat="server" Text ="Sign Up" ></asp:Button>  <asp:Button id="GoLogIn" class="NonMemberBtn" runat ="server" Text="Log In" ></asp:Button>
          <asp:Button ID ="MemberName" align = "center" runat="server" Text="Member" Font-Names="Arial Black" class="NavigateBtn" OnClick="MemberName_Click"  ></asp:Button>
    </div>
        </form>
         
  
</body>
</html>
<script>
    var title = <%= TitleResult %>;
    var date = <%= DateResult %>;
    var Status = <%= getApprovalResult %>;
    var view = <%= TotalViewResult %>;
    var videoID = <%= IDResult %>;
    var firstname = <%= FirstNameResult %>;
    var lastname = <%= LastNameResult %>;
    var category = <%= CategoriesResult %>;
    
    document.write("<table style='margin-top:80px' id = 'ApprovalHistoryTable'>");
    document.write("<thead>");
    document.write("<tr><th>No</th><th>Title</th><th>Category</th><th>Total View</th><th>Date Uploaded</th><th>Status</th><th>Uploaded By</th><th>Action</th></tr>");
    document.write("</thead>");
    var x = 1;
    document.write("<tbody>");
    for (var j = 0; j < title.length; j++) {
        if (Status[j] == "Approved            ") {
            document.write("<tr><td>" + x + "</td><td>" + title[j] + "</td><td>" + category[j] + "</td><td>" + view[j] + "</td><td>" + date[j] + "</td><td>" + Status[j] + "</td><td>" + firstname[j] + "  " + lastname[j] + "</td><td><button value = " + videoID[j] + " onclick = 'viewApprovalVideo(this)' class = 'SbmtBtn'>View</button> <button value = " + videoID[j] + " onclick = 'DeleteApprovalVideo(this)' class = 'SbmtBtn'>Delete</button></td></tr>");
        }
        else {
           document.write("<tr><td>" + x + "</td><td>" + title[j] + "</td><td>" + category[j] + "</td><td>" + view[j] + "</td><td>" + date[j] + "</td><td>" + Status[j] + "</td><td>" + firstname[j] + "  " + lastname[j] + "</td><td><button value = " + videoID[j] + " onclick = 'DeleteApprovalVideo(this)' class = 'SbmtBtn'>Delete</button></td></tr>");
    
        }
            x++;
    }
    document.write("</tbody>");
    document.write("</table>");

    
    $(document).ready(function(){
  $("#myInput1").on("keyup", function() {
    var value = $(this).val().toLowerCase();
    $("#ApprovalHistoryTable tr").filter(function() {
      $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
  });
});
    function viewApprovalVideo(e) {
        var id = e.value;
        window.location.href = "RevertApprovalVideo.aspx?LoginSuccess=1&User=Admin&VideoID="+id;
    }

     function DeleteApprovalVideo(e) {
        var id = e.value;
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.open("GET", "DeleteApprovalHistory.aspx?VideoID=" + id, false);
         xmlhttp.send(null);
         location.reload();
    }
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