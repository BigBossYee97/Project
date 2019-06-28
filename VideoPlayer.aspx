<%      Response.WriteFile("NavigationBar.html"); %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoPlayer.aspx.cs" Inherits="VideoPlayer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link href="Content/LoginStyle.css" rel="Stylesheet" type="text/css" />

<script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-5c8e52de95c4288b"></script>

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
        <asp:Button ID ="GoSignUp" class="NonMemberBtn" runat="server" Text ="Sign Up" OnClick="GoSignUp_Click" ></asp:Button>  <asp:Button ID="GoLogIn" class="NonMemberBtn" runat ="server" Text="Log In" OnClick="GoLogIn_Click" ></asp:Button>
          <asp:Button ID ="MemberName" align = "center" runat="server" Text="Member" Font-Names="Arial Black" class="NavigateBtn"  OnClick="MemberName_Click"  ></asp:Button>
 
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
    var currentLike = <%= LikeResult %>;
        var currentDislike = <%= DislikeResult %>;
   var i = 0;
    if (window.location.search.indexOf("LoginSuccess=1") > -1)
    {
        i = 1;
    }
        

 
    if (window.location.search.indexOf("User=Admin") > -1) {
        document.getElementById("pendingCSS").style.display = "block";
        document.getElementById("MemberNVideo").style.display = "block";
        document.getElementById("ApprovalHistory").style.display = "block";
    }
    
        $(document).ready(function () {
            var CommentList = <%= CommentResult %>;
            var NameList = <%= NameResult %>;
            for (var i = 0; i < CommentList.length; i++) {
                $('<li>').text(CommentList[i]).prependTo('.comments');
                $('<ul>').text(NameList[i]).prependTo('.comments');
                $('button').attr('disabled', 'true');
                $('.counter').text('140');
                $('.commentBox').val('');
            }
        });

        $(document).ready(function () {
            $('button').click(function () {
                if (i == 1) {
                    var comment = $('.commentBox').val();
                    var name = document.getElementById("MemberName").parentNode.childNodes[5].value;
                    $('#TriggerMe').trigger('click');
                    $('<li>').text(comment).prependTo('.comments');
                    $('<ul>').text(name).prependTo('.comments');
                    $('button').attr('disabled', 'true');
                    $('.counter').text('140');
                    $('.commentBox').val('');
                    var url_string = window.location.href;
                    var url = new URL(url_string);
                    var vid = url.searchParams.get("VideoID");
                    var xmlhttp = new XMLHttpRequest();
                    xmlhttp.open("GET", "StoreComment.aspx?Comment=" + comment + "&VideoID=" + vid, false);
                    xmlhttp.send(null);
                    $('.commentBox').val() = "";
                }
                 else {
                alert("Please log in to comment on this video");
                 }
            });

            $('.commentBox').keyup(function () {
                var commentLength = $(this).val().length;
                var charLeft = 140 - commentLength;
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
   
    
        var currentLike = <%= LikeResult %>;
        var currentDislike = <%= DislikeResult %>;

        if (currentLike == '1') {
            document.getElementById(currentLike).style.color = "Yellow";
        }
        if (currentDislike == '1') {
            document.getElementById(currentDislike).style.color = "Yellow";
        }
    function LikeVideo(e) {
            if (i == 1) {
            var url_string = window.location.href; //window.location.href
            var url = new URL(url_string);
            var vid = url.searchParams.get("VideoID");
            var xmlhttp = new XMLHttpRequest();
            var userID = <%= UserIDResult %>;
            var checkLike = e.id;
                if (checkLike == '0') {
                    var Dislike = e.parentNode.childNodes[2].id;
                    if (Dislike == '0') {
                        var likedString = document.getElementById("NumLiked").innerHTML;
                        var numLiked = parseInt(likedString);
                        var latestNum = ++numLiked;
                        document.getElementById("NumLiked").innerHTML = latestNum;
                        e.id = 1;
                        //Like Video
                        xmlhttp.open("GET", "LikeVideo.aspx?VideoID=" + vid + "&UserID=" + userID + "&TotalLike=" + latestNum, false);
                        xmlhttp.send(null);
                        e.parentNode.childNodes[2].style.color = "#4286f4";
                        e.style.color = "Yellow";
                    }
                    else {
                        var likedString = document.getElementById("NumLiked").innerHTML;
                        var numLiked = parseInt(likedString);
                        var latestnumLike = ++numLiked;
                        document.getElementById("NumLiked").innerHTML = latestnumLike;
                        e.id = 1;

                        var DislikedString = document.getElementById("NumDisliked").innerHTML;
                        var numDisliked = parseInt(DislikedString);
                        var latestnumDislike = --numDisliked;
                        document.getElementById("NumDisliked").innerHTML = latestnumDislike;
                        e.parentNode.childNodes[2].id = 0;
                        e.parentNode.childNodes[2].style.color = "#4286f4";
                        e.style.color = "Yellow";
                        //Dislike to like
                        xmlhttp.open("GET", "DisliketoLikeVideo.aspx?VideoID=" + vid + "&UserID=" + userID + "&TotalLike=" + latestnumLike + "&TotalDislike=" + latestnumDislike, false);
                        xmlhttp.send(null);
                    }
                }
                else {

                    var likedString = document.getElementById("NumLiked").innerHTML;
                    var numLiked = parseInt(likedString);
                    var latestNumLiked = --numLiked;
                    document.getElementById("NumLiked").innerHTML = latestNumLiked;
                    e.id = 0;
                    e.style.color = "#4286f4";
                    //Unlike Video

                    xmlhttp.open("GET", "UnlikeVideo.aspx?VideoID=" + vid + "&UserID=" + userID + "&TotalLike=" + latestNumLiked, false);
                    xmlhttp.send(null);
                }
                
            }else {
                    alert("Please log in to vote this video");
                }

        }

    function DislikeVideo(e) {
            if (i == 1) {
            var checkLike = e.id;
            var url_string = window.location.href; //window.location.href
            var url = new URL(url_string);
            var vid = url.searchParams.get("VideoID");
            var xmlhttp = new XMLHttpRequest();
            var userID = <%= UserIDResult %>;
                if (checkLike == '0') {
                    var like = e.parentNode.childNodes[0].id;
                    if (like == '0') {
                        var DislikedString = document.getElementById("NumDisliked").innerHTML;
                        var numDisliked = parseInt(DislikedString);
                        var lastnumDislike = ++numDisliked;
                        document.getElementById("NumDisliked").innerHTML = lastnumDislike;
                        e.id = 1;
                        e.parentNode.childNodes[0].style.color = "#4286f4";
                        e.style.color = "Yellow";
                        //Dislike Video
                        xmlhttp.open("GET", "DislikeVideo.aspx?VideoID=" + vid + "&UserID=" + userID + "&TotalDislike=" + lastnumDislike, false);
                        xmlhttp.send(null);
                    }
                    else {
                        var DislikedString = document.getElementById("NumDisliked").innerHTML;
                        var numDisliked = parseInt(DislikedString);
                        var latestDisliked = ++numDisliked;
                        document.getElementById("NumDisliked").innerHTML = latestDisliked;
                        e.id = 1;

                        var likedString = document.getElementById("NumLiked").innerHTML;
                        var numliked = parseInt(likedString);
                        var latestLiked = --numliked;
                        document.getElementById("NumLiked").innerHTML = latestLiked;
                        e.parentNode.childNodes[0].id = 0;
                        e.parentNode.childNodes[0].style.color = "#4286f4";
                        e.style.color = "Yellow";

                        //Like to Dislike
                        xmlhttp.open("GET", "LiketoDislikeVideo.aspx?VideoID=" + vid + "&UserID=" + userID + "&TotalDislike=" + latestDisliked + "&TotalLike=" + latestLiked, false);
                        xmlhttp.send(null);
                    }
                }
                else {
                    var DislikedString = document.getElementById("NumDisliked").innerHTML;
                    var numDisliked = parseInt(DislikedString);
                    var latestnumDislike = --numDisliked;
                    document.getElementById("NumDisliked").innerHTML = latestnumDislike;
                    e.id = 0;
                    e.style.color = "#4286f4";
                    //Undislike video
                    xmlhttp.open("GET", "UndislikeVideo.aspx?VideoID=" + vid + "&UserID=" + userID + "&TotalDislike=" + latestnumDislike, false);
                    xmlhttp.send(null);
                } 
                
            } else {
                alert("Please log in to vote this video");
            }

    }
   
    if (i == 0) {
            document.getElementById("MemberName").style.display = "none";
            document.getElementById(currentDislike).style.color = "#4286f4";
            document.getElementById(currentLike).style.color = "#4286f4";

    }
    else {
        document.getElementById("GoSignUp").style.display = "none";
        document.getElementById("GoLogIn").style.display = "none";
    }
   
   
   </script>

