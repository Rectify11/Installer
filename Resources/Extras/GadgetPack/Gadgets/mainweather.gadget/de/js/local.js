var lang = {
         itemname:          "pd01",
         version:           "39",
         copy_version:      "Version 3.9",
         copy_rights:       "Pat Possible. Alle Rechte vorbehalten<br>",
         copy_rights2:      "Nutzungsbestimmungen",
         copy_rights3:      "Credits",
         copy_rights4:      "Probleme? Ideen?",

         update_txt:    "Eine neue Gadgetversion steht in der Windows Live Gallery kostenlos zum Download bereit.",
         update_txt_dl: "Jetzt herunterladen",

         nav1:  "Einstellungen",
         nav2:  "Gadgetinfos",
         nav3a: "Mehr",
         nav3b: "Update",
         txt3c: "Gadgets werden geladen",
         
         bar1: "Stadt ändern",
         bar2: "Aussehen",
         bar3: "Sonstiges",

         topic1: "Meine Stadt",
         topic2: "Gadgetname",
         topic3a: "Weitere Gadgets",
         topic3b: "Update",

         nichts: "Zur Zeit keine Einstellungen verfügbar.",
         check1: "Datum anzeigen",

         hello: "<div style='padding-top:5px;'><b>Willkommen!</b><br>Gib eine Stadt deiner Wahl ein<br />und klick auf das Lupen-Symbol.</div>",
         hello2: "<div style='padding-top:5px;'>Du kannst die Stadt wechseln. <br>Gib eine Stadt deiner Wahl ein<br />& klick auf das Lupen-Symbol.</div>",
         wait: "Stadtsuche...",
         wait2: "Sorry, keine Stadt gefunden.",

         culture: "de-DE",
         
         skytext:          "", 
         feelslike:        "Gefühlte",
         humidity:         "Luftfeuchte",
         windspeed:        "Wind",
         winddisplay_k:    "km/h",
         winddisplay_m:    "mi/h",
         observationpoint: "Wo",
         observationtime:  "",
         wetter_for:       "für",
         wetterwarnung:    "Bitte besuche die Webseite http://wetter.msn.de für mehr Details.",
         
         cf:               "Celsius / Kilometer pro Stunde",
         ff:               "Fahrenheit / Meilen pro Stunde",
         alert:            "Melde Wetterwarnungen",
         userAgent:        "Großansicht in der Sidebar",

	repeat: false
}
var dateform = new Array();
var dayname = new Array();
    dayname["Monday"]    = "Montag";
    dayname["Tuesday"]   = "Montag";
    dayname["Wednesday"] = "Mittwoch";
    dayname["Thursday"]  = "Donnerstag";
    dayname["Friday"]    = "Freitag";
    dayname["Saturday"]  = "Samstag";
    dayname["Sunday"]    = "Sonntag";
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