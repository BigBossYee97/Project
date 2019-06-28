<%      Response.WriteFile("NavigationBar.html"); %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="History.aspx.cs" Inherits="History" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style>
 #VideoWrap{
     background-color:black;
     color:white;
     border-radius:10px;
     width:1500px;
     padding:20px;
     height:200px;
     margin-left:350px;
     margin-top:20px;
 }
 #InfoWrap{
     background-color:black;
     width:1000px;
     height:180px;
     padding:20px;
     position:absolute;
     margin-left:350px;
     margin-top:-180px;
 }
    </style>
    <link href="Content/LoginStyle.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="BtnDiv">
       <asp:Button id ="GoSignUp" class="NonMemberBtn" runat="server" Text ="Sign Up" ></asp:Button>  <asp:Button id="GoLogIn" class="NonMemberBtn" runat ="server" Text="Log In"  ></asp:Button>
       
        <asp:Button ID ="MemberName"  runat="server" Text="Member" Font-Names="Arial Black" class="NavigateBtn" OnClick="MemberName_Click"  ></asp:Button>
        
    
        </div>
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>    
	
                <script>
                      var FirstNameList = <%= FirstNameResult %>;
                      var LastNameList = <%= LastNameResult %>;
                      var TitleList = <%= TitleResult %>;
                      var TotalViewList = <%= TotalViewResult %>;
                      var DescriptionList = <%= DescriptionResult %>;
                      var DateList = <%= DateResult %>;
                      var VideoList = <%= VideoResult %>;
                      var IDList = <%= IDResult %>;
                    for (var i = IDList.length - 1; i >= 0; i--) {
                        document.write("<div id = 'VideoWrap'>");
                        document.write("<video width='320' height='160' id = "+ IDList[i] +" onclick = 'VideoLink(this)' ><source src=" + VideoList[i] + " type='video/mp4'><source src=" + VideoList[i] + " type='video/ogg'><source src=" + VideoList[i] + " type='video/webm'></video> ");
                        document.write("<div id = 'InfoWrap'><a href = '#' id =" + IDList[i] + " onclick = 'TitleLink(this)' >" + TitleList[i] + "</a><br><i style = 'font-size:12px;' >" + FirstNameList[i] + " " + LastNameList[i] + " " + TotalViewList[i] + " Views " + "<br>" + DateList[i] + "</i ><br><i style = 'font-size:16px'>" + DescriptionList[i]  + "</i></div> ");
                        
                        document.write("</div>")
            }
                </script>

    </form>
</body>
</html>
<script>
    var userID = <%= userIDResult %>;
    console.log(userID);
    function TitleLink(e) {
        var id = e.id;
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.open("GET", "UpdateTotalView.aspx?VideoID=" + id, false);
        xmlhttp.send(null);
        
        
        xmlhttp.open("GET", "StoreHistory.aspx?VideoID=" + id + "&UserID= "+ userID, false);
        xmlhttp.send(null);
        var url_string = window.location.href; 
        var url = new URL(url_string);
        var login = url.searchParams.get("LoginSuccess");
        var character = url.searchParams.get("User");
        window.location.href = "VideoPlayer.aspx?LoginSuccess="+ login +"&User="+character + "&VideoID=" + id;
    }
     function VideoLink(e) {
        var id = e.id;
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.open("GET", "UpdateTotalView.aspx?VideoID=" + id, false);
         xmlhttp.send(null);

         xmlhttp.open("GET", "StoreHistory.aspx?VideoID=" + id + "&UserID= " + userID, false);
         xmlhttp.send(null);
        var url_string = window.location.href; 
        var url = new URL(url_string);
        var login = url.searchParams.get("LoginSuccess");
        var character = url.searchParams.get("User");
        window.location.href = "VideoPlayer.aspx?LoginSuccess="+ login +"&User="+character + "&VideoID=" + id;
   
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
