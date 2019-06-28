<%      Response.WriteFile("NavigationBar.html"); %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <link href="Content/LoginStyle.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
         <div id="BtnDiv">
        <asp:Button ID ="GoSignUp" Class="NonMemberBtn" runat="server" Text ="Sign Up" OnClick="GoSignUp_Click" ></asp:Button>  <asp:Button id="GoLogIn" class="NonMemberBtn" runat ="server" Text="Log In" OnClick="GoLogIn_Click" ></asp:Button>
        <asp:Button ID ="MemberName" align = "center" runat="server" Text="Member" Font-Names="Arial Black" class="NavigateBtn" OnClick="MemberName_Click"    ></asp:Button>
    </div>
        <div>
        
      
           
        </div>

    </form>
    
</body>
</html>
<script>
   

     var categoryAvailable = <%= CategoriesResult %>;
    var titleList = <%= TitleResult %>;
    var viewList = <%= ViewResult %>;
    var videoCategoryList = <%= VideoCategoryResult %>;
    var nameList = <%= NameResult %>;
    var dateList = <%= DateResult %>;
    var pathList = <%= PathResult %>;
    var IDList = <%= VideoIDResult %>;

  

  
    var x = 1;
    var prev = 0;
    document.write("<table style = 'margin-left:350px;margin-top:20px;width:auto'>");
    document.write("<tr>");
    for (var i = 0; i < categoryAvailable.length; i++) {
   document.write("<tr><td style = 'color:white'>" + categoryAvailable[i] + "</td></tr>");
               
        for (var y = 0; y < titleList.length; y++) {
            if (prev !== categoryAvailable[i]) {
                document.write("</tr>");
                 document.write("<tr>");
               
                 x = 1;
            }
           
            if (categoryAvailable[i] == videoCategoryList[y]) {

                document.write("<td><video width='300' height='200'  onclick = 'VideoLink(this)' id = '" + IDList[y] + "' style = 'margin-left:50px'><source src=" + pathList[y] + " type='video/mp4'><source src=" + pathList[y] + " type='video/ogg'><source src=" + pathList[y] + " type='video/webm'></video><br><a href = '#' onclick = 'TitleLink(this)' id = '"+ IDList[y] +"' style = 'margin-left:50px;color:white'>" + titleList[y] + "</a><i style = 'color:white;float:right'>" + viewList[y] + " views</i><br><i style = 'font-size:14px;margin-left:50px;color:white'>" + nameList[y] +"<br><i style = 'font-size:14px;margin-left:50px;color:white'>" + dateList[y] +"</i></td>");
                
                if (x == 4) {
                    document.write("</tr>");
                    document.write("<tr>");
                    x = 1;
                } else {
                        x++;        
                }

                var prev = categoryAvailable[i];
            }
         

           
        }
       
    }
    document.write("</table>");

   
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

    var userID = <%= userIDResult %>;

     function TitleLink(e) {
        var id = e.id;
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.open("GET", "UpdateTotalView.aspx?VideoID=" + id, false);
        xmlhttp.send(null);
         if (i == 1) {
             xmlhttp.open("GET", "StoreHistory.aspx?VideoID=" + id + "&UserID= " + userID, false);
             xmlhttp.send(null);
         }
        var url_string = window.location.href; 
        var url = new URL(url_string);
        var login = url.searchParams.get("LoginSuccess");
        var character = url.searchParams.get("User");
        if (i == 0) {
            window.location.href = "VideoPlayer.aspx?VideoID=" + id;
      
        }
        else {
            window.location.href = "VideoPlayer.aspx?VideoID=" + id +"&LoginSuccess=" + login + "&User=" + character;
        }
    }

    function VideoLink(e) {
        var id = e.id;
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.open("GET", "UpdateTotalView.aspx?VideoID=" + id, false);
        xmlhttp.send(null);
         if (i == 1) {
             xmlhttp.open("GET", "StoreHistory.aspx?VideoID=" + id + "&UserID= " + userID, false);
             xmlhttp.send(null);
         }
        var url_string = window.location.href; 
        var url = new URL(url_string);
        var login = url.searchParams.get("LoginSuccess");
        var character = url.searchParams.get("User");
        if (i == 0) {
            window.location.href = "VideoPlayer.aspx?VideoID=" + id;
      
        }
        else {
            window.location.href = "VideoPlayer.aspx?VideoID=" + id +"&LoginSuccess=" + login + "&User=" + character;
        }
    }


      function SearchBtn() {
        var searchValue = document.getElementById("myInput1").value;
        if (searchValue != "") {
            var validVideo = 0;
            var videoName;
            var videoID;
            for (var x = 0; x < titleList.length; x++) {
                if (searchValue == titleList[x]) {
                    validVideo = 1;
                    videoName = titleList[x];
                    videoID = IDList[x];
                }
            }

            if (validVideo == 1) {
                 var xmlhttp = new XMLHttpRequest();
                xmlhttp.open("GET", "UpdateTotalView.aspx?VideoID=" + videoID, false);
                xmlhttp.send(null);
                 if (i == 1) {
                     xmlhttp.open("GET", "StoreHistory.aspx?VideoID=" + videoID + "&UserID= " + userID, false);
                     xmlhttp.send(null);
                 }
                window.location.href = "SearchResult.aspx?Title=" + videoName;
                
            }
            else {
                alert("Video Not Found");
            }

        }
        else {
            alert("Please enter video name to search for the result");
        }
    }






     function autocomplete(inp, arr) {
  /*the autocomplete function takes two arguments,
  the text field element and an array of possible autocompleted values:*/
  var currentFocus;
  /*execute a function when someone writes in the text field:*/
  inp.addEventListener("input", function(e) {
      var a, b, j, val = this.value;
      /*close any already open lists of autocompleted values*/
      closeAllLists();
      if (!val) { return false;}
      currentFocus = -1;
      /*create a DIV element that will contain the items (values):*/
      a = document.createElement("DIV");
      a.setAttribute("id", this.id + "autocomplete-list");
      a.setAttribute("class", "autocomplete-items");
      /*append the DIV element as a child of the autocomplete container:*/
      this.parentNode.appendChild(a);
      /*for each item in the array...*/
      for (j = 0; j < arr.length; j++) {
        /*check if the item starts with the same letters as the text field value:*/
        if (arr[j].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
          /*create a DIV element for each matching element:*/
          b = document.createElement("DIV");
          /*make the matching letters bold:*/
          b.innerHTML = "<strong>" + arr[j].substr(0, val.length) + "</strong>";
          b.innerHTML += arr[j].substr(val.length);
          /*insert a input field that will hold the current array item's value:*/
          b.innerHTML += "<input type='hidden' value='" + arr[j] + "'>";
          /*execute a function when someone clicks on the item value (DIV element):*/
              b.addEventListener("click", function(e) {
              /*insert the value for the autocomplete text field:*/
              inp.value = this.getElementsByTagName("input")[0].value;
              /*close the list of autocompleted values,
              (or any other open lists of autocompleted values:*/
              closeAllLists();
          });
          a.appendChild(b);
        }
      }
  });
  /*execute a function presses a key on the keyboard:*/
  inp.addEventListener("keydown", function(e) {
      var x = document.getElementById(this.id + "autocomplete-list");
      if (x) x = x.getElementsByTagName("div");
      if (e.keyCode == 40) {
        /*If the arrow DOWN key is pressed,
        increase the currentFocus variable:*/
        currentFocus++;
        /*and and make the current item more visible:*/
        addActive(x);
      } else if (e.keyCode == 38) { //up
        /*If the arrow UP key is pressed,
        decrease the currentFocus variable:*/
        currentFocus--;
        /*and and make the current item more visible:*/
        addActive(x);
      } else if (e.keyCode == 13) {
        /*If the ENTER key is pressed, prevent the form from being submitted,*/
        e.preventDefault();
        if (currentFocus > -1) {
          /*and simulate a click on the "active" item:*/
          if (x) x[currentFocus].click();
        }
      }
  });
  function addActive(x) {
    /*a function to classify an item as "active":*/
    if (!x) return false;
    /*start by removing the "active" class on all items:*/
    removeActive(x);
    if (currentFocus >= x.length) currentFocus = 0;
    if (currentFocus < 0) currentFocus = (x.length - 1);
    /*add class "autocomplete-active":*/
    x[currentFocus].classList.add("autocomplete-active");
  }
  function removeActive(x) {
    /*a function to remove the "active" class from all autocomplete items:*/
    for (var j = 0; j < x.length; j++) {
      x[j].classList.remove("autocomplete-active");
    }
  }
  function closeAllLists(elmnt) {
    /*close all autocomplete lists in the document,
    except the one passed as an argument:*/
    var x = document.getElementsByClassName("autocomplete-items");
    for (var j = 0; j < x.length; j++) {
      if (elmnt != x[j] && elmnt != inp) {
      x[j].parentNode.removeChild(x[j]);
    }
  }
}
/*execute a function when someone clicks in the document:*/
document.addEventListener("click", function (e) {
    closeAllLists(e.target);
});
} 
   
    var title = [];
    for(var j = 0; j < titleList.length; j++){
        title.push(titleList[j]);
        }
    autocomplete(document.getElementById("myInput1"), title);
</script>