<%      Response.WriteFile("NavigationBar.html"); %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyChannel.aspx.cs" Inherits="MyChannel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="Content/LoginStyle.css" rel="Stylesheet" type="text/css" />
    <style>
 * { box-sizing: border-box; }


    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" >
          <div id="BtnDiv">
        <asp:Button id ="GoSignUp" class="NonMemberBtn" runat="server" Text ="Sign Up" ></asp:Button>  <asp:Button id="GoLogIn" class="NonMemberBtn" runat ="server" Text="Log In" ></asp:Button>
        
    <div class="autocomplete" style="width:300px;position:absolute;margin-left:1390px;margin-top:120px">
    <input id="myInput" style ="float:right" runat ="server" type="text" name="myVideos" placeholder="Search"/> <asp:button runat = "server" ID="searchBtn" style ="position:absolute" Text = "search" OnClick="searchBtn_Click"></asp:button>
    </div>
        <asp:Button ID ="MemberName" align = "center" runat="server" Text="Member" Font-Names="Arial Black" class="NavigateBtn" OnClick="MemberName_Click" ></asp:Button>
 
         </div>
        <center>
        <div>
        <asp:Button style = 'margin-left:1500px;margin-top:40px' ID="GoUploadVideo" class = "SbmtBtn" runat="server" Text="Upload a Video" OnClick="GoUploadVideo_Click" />
         
       
        <asp:Label ID="NonMemberMessage" runat="server" Text=""></asp:Label>
       <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
           
        </div>
            </center>
    </form>
</body>
</html>
<script>
  
   
    function DelVideo(e) {

        if (confirm("Are you sure to delete this video?")) {
        var vid = e.id;
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.open("GET", "DeleteVideo.aspx?VideoID=" + vid, false);
        xmlhttp.send(null);
            location.reload();
        }
     
        
    }
    function EditVideo(e) {
        var id = e.id;
        var url_string = window.location.href; //window.location.href
        var url = new URL(url_string);
        var char = url.searchParams.get("User");
        window.location.href = "EditVideo.aspx?LoginSuccess=1&User=" + char + "&VideoID=" + id;

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

    function LinkVal(e) {
         var x = e.id;
         var currURL = window.location.href; 
         var url = new URL(currURL);
         var Paramemter1 = url.searchParams.get("LoginSuccess");
        var Paramemter2 = url.searchParams.get("User");
        window.location.href = "ChannelSearchResult.aspx?LoginSuccess=" + Paramemter1 + "&User=" + Paramemter2+"&VideoID="+ x;
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
    var arrayList = <%= ArrayResult %>;
    var title = [];
    for(var j = 0; j < arrayList.length; j++){
        title.push(arrayList[j]);
        }
    autocomplete(document.getElementById("myInput"), title);
</script>