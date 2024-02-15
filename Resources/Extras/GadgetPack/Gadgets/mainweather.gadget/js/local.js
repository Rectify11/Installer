var lang = {
         itemname:          "pd01",
         version:           "39",
         copy_version:      "3.9",
         copy_rights:       "Pat Possible. All rights reserved.<br>",
         copy_rights2:      "Terms of use (german)",
         copy_rights3:      "Credits",
         copy_rights4:      "Problems? Ideas?",

         update_txt:    "Download the new gadget version for free at the Windows Live Gallery.",
         update_txt_dl: "Download now!",

         nav1:  "Settings",
         nav2:  "About",
         nav3a: "More",
         nav3b: "Update",
         txt3c: "loading",
         
         bar1: "Change city",
         bar2: "Design",
         bar3: "Other",

         topic1: "My city",
         topic2: "Gadgetname",
         topic3a: "More Gadgets",
         topic3b: "Update",

         nichts: "Currently no settings available.",
         check1: "Show date.",

         hello: "<div style='padding-top:5px;'><b>Welcome!</b><br>Enter a city, click on the glass icon <br />and select the place of your choice.</div>",
         hello2: "<div style='padding-top:12px;'>Enter a city, click on the glass icon <br />and select the place of your choice.</div>",
         wait:  "Search...",
         wait2: "Sorry, no city found.",

         culture: "",

         skytext:          "",
         feelslike:        "Feels like",
         humidity:         "Humidity",
         windspeed:        "Wind",
         winddisplay_k:    "kmh",
         winddisplay_m:    "mph",
         observationpoint: "Where",
         observationtime:  "",
         wetter_for:       "for",
         wetterwarnung:    "Please check http://weather.msn.com for more details.",
         
         cf:               "Celsius / Kilometers per hour",
         ff:               "Fahrenheit / Miles per hour",
         alert:            "Show weather warnings",
         userAgent:        "Sidebar FullView",

	repeat: false
}
var dateform = new Array();
var dayname = new Array();
    dayname["Monday"]    = "Monday";
    dayname["Tuesday"]   = "Tuesday";
    dayname["Wednesday"] = "Wednesday";
    dayname["Thursday"]  = "Thursday";
    dayname["Friday"]    = "Friday";
    dayname["Saturday"]  = "Saturday";
    dayname["Sunday"]    = "Sunday";
var monthname = new Array();
    monthname["01"] = "Jan";
    monthname["02"] = "Feb";
    monthname["03"] = "Mär";
    monthname["04"] = "Apr";
    monthname["05"] = "Mai";
    monthname["06"] = "Jun";
    monthname["07"] = "Jul";
    monthname["08"] = "Aug";
    monthname["09"] = "Sep";
    monthname["10"] = "Okt";
    monthname["11"] = "Nov";
    monthname["12"] = "Dez";
function setlang()
{
document.getElementById('nav1').innerText = lang.nav1;
document.getElementById('nav2').innerText = lang.nav2;
document.getElementById('nav3').innerText = lang.nav3a;

document.getElementById('topic1').innerText = lang.topic1;
document.getElementById('topic2').innerText = lang.topic2;
document.getElementById('topic3').innerText = lang.topic3a;

document.getElementById('txt3c').innerText = lang.txt3c;
}