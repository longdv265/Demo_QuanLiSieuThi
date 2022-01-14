// import axios from "axios"
const str = location.href;
const numb = str.length;
/*alert(str[numb-1]);*/

if (str[numb - 1] == "1") {
	changeTab_1();
}
else if (str[numb - 1] == "2") {
	changeTab_2()
}
else {
	changeTab_3();

}



function changeTab_1() {
	document.getElementById('main-content-1').style.display = 'block';
	document.getElementById('main-content-2').style.display = 'none';
	document.getElementById('main-content-3').style.display = 'none';

}

function changeTab_2() {
	document.getElementById('main-content-1').style.display = 'none';
	document.getElementById('main-content-2').style.display = 'block';
	document.getElementById('main-content-3').style.display = 'none';

}


function changeTab_3() {	
	document.getElementById('main-content-1').style.display = 'none';
	document.getElementById('main-content-2').style.display = 'none';
	document.getElementById('main-content-3').style.display = 'block';
}



/*
window.addEventListener("load", (event) => {
  changeTab(event, "main-content-1");
});

function changeTab(evt, indexTab) {
  // Declare all variables
  var i, tabcontent, tablinks;

  // Get all elements with class="tabcontent" and hide them
  tabcontent = document.getElementsByClassName("main-content");
  for (i = 0; i < tabcontent.length; i++) {
	tabcontent[i].style.display = "none";
  }

  // Get all elements with class="tablinks" and remove the class "active"
  tablinks = document.getElementsByClassName("sidebar-item");
  for (i = 0; i < tablinks.length; i++) {
	tablinks[i].className = tablinks[i].className.replace(" active", "");
  }

  // Show the current tab, and add an "active" class to the button that opened the tab
  document.getElementById(indexTab).style.display = "block";
  evt.currentTarget.className += " active";
}

*/