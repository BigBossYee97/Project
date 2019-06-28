<%      Response.WriteFile("NavigationBar.html"); %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChannelSearchResult.aspx.cs" Inherits="ChannelSearchResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link href="Content/LoginStyle.css" rel="Stylesheet" type="text/css" />
<link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'/>
 <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>

<style>
 body {
	padding: 0;
	margin: 0 auto;
	
}

.clearfix {
	clear: both;
}

h1, p, textarea,ul, li, button {
	margin: 0;
	padding: 0;
	color: white;
	
	font-weight: 400;
	font-family: 'Open Sans', sans-serif;
}

header {
	min-height: 100px;
}

header h1 {
	font-size: 150%;
	font-weight: 600;
	line-height: 400%;
	text-align: center;
}

section {
	width: 400px;
	margin: 0 auto;
}

section textarea {
	width: 100%;
	height: 70px;
	padding: 5px;
	outline: none;
	font-size: 80%;
	resize: vertical;
	margin-bottom: 20px;
	border: 1px solid #0EBFE9;
	border-bottom: 4px solid #63D1F4;
    
}

section p {
	
	line-height: 30px;
}

section span {
	font-weight: bold;
}

section button {
	border: 0;
	width:	30%;
	color: #fff;
	height: 35px;
	float: right;
	outline: none;
	cursor: pointer;
	font-weight: 700;
	background-color: #0EBFE9;
	border-bottom: 3px solid #63D1F4;
}

section button:hover {
	background-color: #0099CC;
	border-bottom: 3px solid #00688B;
}

section button:disabled {
	background-color: #d3d3d3 ;
	border-bottom: 3px solid #d3d3d3;
}

section ul {
	margin-top: 60px;
	list-style: none;
	padding-left: 0px;
}

section ul li {
	padding: 10px 10px;
  word-wrap: break-word;
	border-bottom: 3px solid #63D1F4;
}

section ul li:hover {
	border-bottom: 3px solid #00688B;
}


    </style>
</head>
<body>
    
      <form id="form1" runat="server" autocomplete="off" >
          <div id="BtnDiv">
        <asp:Button id ="GoSignUp" class="NonMemberBtn" runat="server" Text ="Sign Up" ></asp:Button>  <asp:Button id="GoLogIn" class="NonMemberBtn" runat ="server" Text="Log In" ></asp:Button>
          <asp:Button ID ="MemberName" align = "center" runat="server" Text="Member" Font-Names="Arial Black" class="NavigateBtn" OnClick="MemberName_Click" ></asp:Button>
 
    </div>
      
        
        
        <div>
      <center>
       
     
       <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
       </center>
        </div>
           
         
    </form>
  <div style ="padding:30px;margin-top:20px;margin-bottom:30px;background-color:black;border-radius:10px;width:790px;margin-left:730px">
  	<section>
			<textarea id = 'desc' runat ="server" name ="Desc" class="commentBox" style ="color:black" placeholder="Place your comments here" type="textarea"></textarea>
			<p><span class="counter">140</span> Characters left</p>
			<button>Post</button>
			<ul class="clearfix comments" style = "margin-bottom:30px">
				
			</ul>
		</section>
        </div>
</body>
</html>
<script>
    function editVideo() {
        var url_string = window.location.href; //window.location.href
        var url = new URL(url_string);
        var vid = url.searchParams.get("VideoID");
        var char = url.searchParams.get("User");
        window.location.href = "EditVideo.aspx?LoginSuccess=1&User="+char+"&VideoID="+vid;
    }

    function DelVideo() {

        if (confirm("Are you sure to delete this video?")) {
        var url_string = window.location.href; //window.location.href
        var url = new URL(url_string);
        var vid = url.searchParams.get("SearchResult");
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.open("GET", "DeleteVideo.aspx?VideoID=" + vid, false);
        xmlhttp.send(null);
        var char = url.searchParams.get("User");
        window.location.href = "MyChannel.aspx?LoginSuccess=1&User="+char;
        }
     
        
    }
    $(document).ready(function () {
          var CommentList = <%= CommentResult %>;
        var NameList = <%= NameResult %>;
       
          for(var i = 0; i < CommentList.length; i++){
          $('<li>').text(CommentList[i]).prependTo('.comments');
          $('<ul>').text(NameList[i]).prependTo('.comments');
		  $('button').attr('disabled', 'true');
		  $('.counter').text('140');
          $('.commentBox').val('');
          } 
    });
    
 $(document).ready(function() {
	$('button').click(function() {
        var comment = $('.commentBox').val();
        var name = document.getElementById("MemberName").parentNode.childNodes[5].value;             
        $('#TriggerMe').trigger('click');
        $('<li>').text(comment).prependTo('.comments');
        $('<ul>').text(name).prependTo('.comments');
		$('button').attr('disabled', 'true');
		$('.counter').text('140');
        $('.commentBox').val('');        
        $('.commentBox').val() = "";
        var url_string = window.location.href; 
        var url = new URL(url_string);
        var vid = url.searchParams.get("VideoID");
        var xmlhttp = new XMLHttpRequest();
        
        xmlhttp.open("GET", "StoreComment.aspx?Comment=" + comment + "&VideoID=" + vid, false);
        xmlhttp.send(null);

	});
	
	$('.commentBox').keyup(function() {
		var commentLength = $(this).val().length;
		var charLeft =  140 - commentLength;
		$('.counter').text(charLeft);
		
		if (commentLength == 0) {
			$('button').attr('disabled', 'true');
		}
		else if (commentLength > 140) {
			$('button').attr('disabled', 'true');
		}
		else {
			$('button').removeAttr('disabled', 'true');
		}
	});
	
	$('button').attr('disabled', 'true');
	
});
   
  
        
      
  

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
