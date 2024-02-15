System.Gadget.settingsUI = "settings.html";
System.Gadget.onSettingsClosed = settingsClosed;
System.Gadget.onDock = docked;
System.Gadget.onUndock = undocked;


var txtcolor = System.Gadget.Settings.read("txtcolor");
if(txtcolor==''){txtcolor="1";System.Gadget.Settings.write("txtcolor", txtcolor);}
var userAgenty = System.Gadget.Settings.read("userAgenty");
if(userAgenty==''){userAgenty=false;System.Gadget.Settings.write("userAgenty", userAgenty);}
var win        = System.Gadget.Settings.read("win");
if(win=='')
{
   var userAgent = navigator.userAgent.toLowerCase();
   var userAgenty=System.Gadget.Settings.read("userAgenty");
   if(userAgenty==''){userAgenty=false;System.Gadget.Settings.write("userAgenty", userAgenty);}   
   if(userAgent.indexOf("windows nt 6.0")>-1){var win="vista";}else{var win="win7";}
   System.Gadget.Settings.write("win", win);    
}

var wireless = System.Network.Wireless;
var status;
wireless.connectionChanged = go;
var statuscheck=0;
var count = 0;
var styley=System.Gadget.Settings.read("styley");
if(styley==''){styley="1";System.Gadget.Settings.write("styley", styley);}
var confirmy=System.Gadget.Settings.read("confirmy");
if(confirmy==''){confirmy=false;System.Gadget.Settings.write("confirmy", confirmy);}
function update(){load('0');}
function start(){if(System.Gadget.docked) {docked();}else{undocked();}load('0');}

function load(run){
count = count+1;

if(count == 50){location.href = "index.html";}
if(run==0){go();/*confirm('lade daten');*/}

var show  = "";show  = System.Gadget.Settings.read("show");show  = show.toString();if(show.length == 4){show = "0"+show;}if(show == ''){show="wc:USNY0996";}var CF_var = "";CF_var = System.Gadget.Settings.read("CF");if(CF_var == ''){CF_var="C";}
/**/
var rssDoc2 = new ActiveXObject("MSXML2.DOMDocument.3.0");
rssDoc2.onreadystatechange = popData2;
rssDoc2.load("http://weather.service.msn.com/data.aspx?src=vista&wealocations="+show+"&weadegreetype="+CF_var+"&culture="+lang.culture);
function popData2()
{
if(rssDoc2.readyState!=4)return;
var c2 = 0;
var rssItems2 = rssDoc2.selectNodes("/weatherdata/weather");

for(var i=0;i<rssItems2.length;i++)
{
var stadt = rssItems2[i].selectSingleNode("./@weatherlocationname").text;
var arrLine = stadt.split(",");
System.Gadget.Settings.write("current_stadt", arrLine[0]);

var alert = rssItems2[i].selectSingleNode("./@alert").text;

System.Gadget.Settings.write("current_temperature", rssItems2[i].selectSingleNode("./current/@temperature").text+"°"+CF_var);
System.Gadget.Settings.write("current_skycode", rssItems2[i].selectSingleNode("./current/@skycode").text);
System.Gadget.Settings.write("current_skytext", rssItems2[i].selectSingleNode("./current/@skytext").text);
System.Gadget.Settings.write("current_date", rssItems2[i].selectSingleNode("./current/@date").text);
System.Gadget.Settings.write("current_day", rssItems2[i].selectSingleNode("./current/@day").text);
System.Gadget.Settings.write("current_shortday", rssItems2[i].selectSingleNode("./current/@shortday").text);
System.Gadget.Settings.write("current_observationtime", rssItems2[i].selectSingleNode("./current/@observationtime").text);
System.Gadget.Settings.write("current_observationpoint", rssItems2[i].selectSingleNode("./current/@observationpoint").text);
System.Gadget.Settings.write("current_feelslike", rssItems2[i].selectSingleNode("./current/@feelslike").text);
System.Gadget.Settings.write("current_humidity", rssItems2[i].selectSingleNode("./current/@humidity").text);
System.Gadget.Settings.write("current_windspeed", rssItems2[i].selectSingleNode("./current/@windspeed").text);
System.Gadget.Settings.write("current_winddisplay", rssItems2[i].selectSingleNode("./current/@winddisplay").text);

//2010-07-02
var observationtime = rssItems2[i].selectSingleNode("./current/@observationtime").text;
var arrLine1 = observationtime.split(":");
var currentdate = rssItems2[i].selectSingleNode("./current/@date").text;
var arrLine2 = currentdate.split("-");

//svar dateform1 = arrLine2[2]+" "+monthname[arrLine2[1]]+" "+arrLine2[0];  /* 01 Jul 2010*/
dateform["de-DE"] = arrLine2[2]+"."+arrLine2[1]+"."+arrLine2[0];             /* 01.07.2010*/
dateform[""] = rssItems2[i].selectSingleNode("./current/@date").text;   /*2010-07-02*/


System.Gadget.Settings.write("current_observationtime", dateform[lang.culture]+" - "+arrLine1[0]+":"+arrLine1[1]);

System.Gadget.Settings.write("forecast0_low", rssItems2[i].selectSingleNode("./forecast[0]/@low").text);
System.Gadget.Settings.write("forecast0_high", rssItems2[i].selectSingleNode("./forecast[0]/@high").text);
System.Gadget.Settings.write("forecast0_skycodeday", rssItems2[i].selectSingleNode("./forecast[0]/@skycodeday").text);
System.Gadget.Settings.write("forecast0_skytextday", rssItems2[i].selectSingleNode("./forecast[0]/@skytextday").text);
System.Gadget.Settings.write("forecast0_date", rssItems2[i].selectSingleNode("./forecast[0]/@date").text);
System.Gadget.Settings.write("forecast0_day", rssItems2[i].selectSingleNode("./forecast[0]/@day").text);
System.Gadget.Settings.write("forecast0_shortday", rssItems2[i].selectSingleNode("./forecast[0]/@shortday").text);
System.Gadget.Settings.write("forecast0_precip", rssItems2[i].selectSingleNode("./forecast[0]/@precip").text);
var today_vorschau = System.Gadget.Settings.read("forecast0_low")+"° / "+System.Gadget.Settings.read("forecast0_high")+"°";
System.Gadget.Settings.write("today_vorschau", today_vorschau);

System.Gadget.Settings.write("forecast1_low", rssItems2[i].selectSingleNode("./forecast[1]/@low").text);
System.Gadget.Settings.write("forecast1_high", rssItems2[i].selectSingleNode("./forecast[1]/@high").text);
System.Gadget.Settings.write("forecast1_skycodeday", rssItems2[i].selectSingleNode("./forecast[1]/@skycodeday").text);
System.Gadget.Settings.write("forecast1_skytextday", rssItems2[i].selectSingleNode("./forecast[1]/@skytextday").text);
System.Gadget.Settings.write("forecast1_date", rssItems2[i].selectSingleNode("./forecast[1]/@date").text);
System.Gadget.Settings.write("forecast1_day", rssItems2[i].selectSingleNode("./forecast[1]/@day").text);
System.Gadget.Settings.write("forecast1_shortday", rssItems2[i].selectSingleNode("./forecast[1]/@shortday").text);
System.Gadget.Settings.write("forecast1_precip", rssItems2[i].selectSingleNode("./forecast[1]/@precip").text);

System.Gadget.Settings.write("forecast2_low", rssItems2[i].selectSingleNode("./forecast[2]/@low").text);
System.Gadget.Settings.write("forecast2_high", rssItems2[i].selectSingleNode("./forecast[2]/@high").text);
System.Gadget.Settings.write("forecast2_skycodeday", rssItems2[i].selectSingleNode("./forecast[2]/@skycodeday").text);
System.Gadget.Settings.write("forecast2_skytextday", rssItems2[i].selectSingleNode("./forecast[2]/@skytextday").text);
System.Gadget.Settings.write("forecast2_date", rssItems2[i].selectSingleNode("./forecast[2]/@date").text);
System.Gadget.Settings.write("forecast2_day", rssItems2[i].selectSingleNode("./forecast[2]/@day").text);
System.Gadget.Settings.write("forecast2_shortday", rssItems2[i].selectSingleNode("./forecast[2]/@shortday").text);
System.Gadget.Settings.write("forecast2_precip", rssItems2[i].selectSingleNode("./forecast[2]/@precip").text);

System.Gadget.Settings.write("forecast3_low", rssItems2[i].selectSingleNode("./forecast[3]/@low").text);
System.Gadget.Settings.write("forecast3_high", rssItems2[i].selectSingleNode("./forecast[3]/@high").text);
System.Gadget.Settings.write("forecast3_skycodeday", rssItems2[i].selectSingleNode("./forecast[3]/@skycodeday").text);
System.Gadget.Settings.write("forecast3_skytextday", rssItems2[i].selectSingleNode("./forecast[3]/@skytextday").text);
System.Gadget.Settings.write("forecast3_date", rssItems2[i].selectSingleNode("./forecast[3]/@date").text);
System.Gadget.Settings.write("forecast3_day", rssItems2[i].selectSingleNode("./forecast[3]/@day").text);
System.Gadget.Settings.write("forecast3_shortday", rssItems2[i].selectSingleNode("./forecast[3]/@shortday").text);
System.Gadget.Settings.write("forecast3_precip", rssItems2[i].selectSingleNode("./forecast[3]/@precip").text);

System.Gadget.Settings.write("forecast4_low", rssItems2[i].selectSingleNode("./forecast[4]/@low").text);
System.Gadget.Settings.write("forecast4_high", rssItems2[i].selectSingleNode("./forecast[4]/@high").text);
System.Gadget.Settings.write("forecast4_skycodeday", rssItems2[i].selectSingleNode("./forecast[4]/@skycodeday").text);
System.Gadget.Settings.write("forecast4_skytextday", rssItems2[i].selectSingleNode("./forecast[4]/@skytextday").text);
System.Gadget.Settings.write("forecast4_date", rssItems2[i].selectSingleNode("./forecast[4]/@date").text);
System.Gadget.Settings.write("forecast4_day", rssItems2[i].selectSingleNode("./forecast[4]/@day").text);
System.Gadget.Settings.write("forecast4_shortday", rssItems2[i].selectSingleNode("./forecast[4]/@shortday").text);
System.Gadget.Settings.write("forecast4_precip", rssItems2[i].selectSingleNode("./forecast[4]/@precip").text);
}
if(System.Gadget.docked) {docked();}else{undocked();}
if(alert != '' && confirmy==true){if(typeof alert!='undefined'){confirm(alert+" "+lang.wetter_for+" "+System.Gadget.Settings.read("current_stadt")+"!\n"+lang.wetterwarnung);}}

}
/**/

/*if(run==0){confirm('daten gefunden');}*/
}



function docked() {
if(userAgenty==true)
{
var style=System.Gadget.Settings.read("style");if(style==''){style="c";System.Gadget.Settings.write("style", style);}
    document.body.style.height = "280px";
var oBackground = document.getElementById("bg");
    oBackground.src = "url(img/bg-undocked4_"+style+".png)";
big();
}
else
{
var style=System.Gadget.Settings.read("style");if(style==''){style="c";System.Gadget.Settings.write("style", style);}
    document.body.style.height = "67px";
var oBackground = document.getElementById("bg");

    oBackground.src = "url(img/bg-docked4_"+style+".png)";
small();
}
}


function undocked() {
var style=System.Gadget.Settings.read("style");
    document.body.style.height = "280px";
var oBackground = document.getElementById("bg");
    oBackground.src = "url(img/bg-undocked4_"+style+".png)";
big();
}

function showFlyout () {
System.Gadget.Flyout.file = "flyout.html";
hideFlyout();
System.Gadget.Flyout.show = true;
System.Gadget.Flyout.onShow = function() {
System.Gadget.Flyout.document.getElementById("iframe").src = flyoutHTML;
}
}

function hideFlyout () {
if (System.Gadget.Flyout.show) {
System.Gadget.Flyout.show = false;
}
}
function go() {
   var sett1  = setTimeout("load('0');",1000 * 60 * 30);
   var sett2  = setTimeout("load('1');",1000 * 5);
   var sett3  = setTimeout("load('1');",1000 * 10);
   var sett4  = setTimeout("load('1');",1000 * 15);
   var sett5  = setTimeout("load('1');",1000 * 30);
   var sett6  = setTimeout("load('1');",1000 * 60);
   var sett7  = setTimeout("load('1');",1000 * 60 * 5);
   var sett8  = setTimeout("load('1');",1000 * 60 * 10);
   var sett9  = setTimeout("load('1');",1000 * 60 * 20);
}
function small(){
document.getElementById("bg").removeObjects();


if(System.Gadget.Settings.read("current_skycode") != ''){bg.addImageObject("img/w"+styley+"/"+System.Gadget.Settings.read("current_skycode")+".png", 5, 8);}

if(System.Gadget.Settings.read("txtcolor") == '1')
{

/*black*/
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_temperature"), "Calibri", 21, "black", 122,6);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("today_vorschau"), "Calibri", 12, "black", 121,30);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_stadt"), "Calibri", 12, "black", 120,44);txtAlign.align = 2;
/*black*/
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_temperature"), "Calibri", 21, "white", 121,5);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("today_vorschau"), "Calibri", 12, "white", 120,29);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_stadt"), "Calibri", 12, "white", 119,43);txtAlign.align = 2;

}else{

var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_temperature"), "Calibri", 21, "white", 122,6);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("today_vorschau"), "Calibri", 12, "white", 121,30);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_stadt"), "Calibri", 12, "white", 120,44);txtAlign.align = 2;
/*black*/
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_temperature"), "Calibri", 21, "black", 121,5);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("today_vorschau"), "Calibri", 12, "black", 120,29);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_stadt"), "Calibri", 12, "black", 119,43);txtAlign.align = 2;
/*black*/

}




}
function big() {
document.getElementById("bg").removeObjects();
var CF_var = "";CF_var = System.Gadget.Settings.read("CF");if(CF_var == ''){CF_var="C";}

if(System.Gadget.Settings.read("current_skycode") != ''){bg.addImageObject("img/w"+styley+"/"+System.Gadget.Settings.read("current_skycode")+".png", 5, 8);}



if(System.Gadget.Settings.read("txtcolor") == '1')
{

/*black*/
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_temperature"), "Calibri", 21, "black", 122,6);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("today_vorschau"), "Calibri", 12, "black", 121,30);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_stadt"), "Calibri", 12, "black", 120,44);txtAlign.align = 2;
/*black*/
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_temperature"), "Calibri", 21, "white", 121,5);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("today_vorschau"), "Calibri", 12, "white", 120,29);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_stadt"), "Calibri", 12, "white", 119,43);txtAlign.align = 2;

}else{

var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_temperature"), "Calibri", 21, "white", 122,6);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("today_vorschau"), "Calibri", 12, "white", 121,30);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_stadt"), "Calibri", 12, "white", 120,44);txtAlign.align = 2;
/*black*/
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_temperature"), "Calibri", 21, "black", 121,5);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("today_vorschau"), "Calibri", 12, "black", 120,29);txtAlign.align = 2;
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_stadt"), "Calibri", 12, "black", 119,43);txtAlign.align = 2;
/*black*/

}

/*
System.Gadget.Settings.write("current_temperature", rssItems2[i].selectSingleNode("./current/@temperature").text+"°"+CF_var);
System.Gadget.Settings.write("current_skycode", rssItems2[i].selectSingleNode("./current/@skycode").text);
System.Gadget.Settings.write("current_skytext", rssItems2[i].selectSingleNode("./current/@skytext").text);
System.Gadget.Settings.write("current_date", rssItems2[i].selectSingleNode("./current/@date").text);
System.Gadget.Settings.write("current_day", rssItems2[i].selectSingleNode("./current/@day").text);
System.Gadget.Settings.write("current_shortday", rssItems2[i].selectSingleNode("./current/@shortday").text);
System.Gadget.Settings.write("current_observationtime", rssItems2[i].selectSingleNode("./current/@observationtime").text);
System.Gadget.Settings.write("current_observationpoint", rssItems2[i].selectSingleNode("./current/@observationpoint").text);
System.Gadget.Settings.write("current_feelslike", rssItems2[i].selectSingleNode("./current/@feelslike").text);
System.Gadget.Settings.write("current_humidity", rssItems2[i].selectSingleNode("./current/@humidity").text);
System.Gadget.Settings.write("current_windspeed", rssItems2[i].selectSingleNode("./current/@windspeed").text);
System.Gadget.Settings.write("current_winddisplay", rssItems2[i].selectSingleNode("./current/@winddisplay").text);
*/
var observationpoint = System.Gadget.Settings.read("current_observationpoint");
if(observationpoint.indexOf(",") > -1){var o = observationpoint.split(",");observationpoint=o[1];}
if(observationpoint.indexOf("-") > -1){var o = observationpoint.split("-");observationpoint=o[1];}
if(observationpoint.indexOf("/") > -1){var o = observationpoint.split("/");observationpoint=o[1];}
if(observationpoint.length > 15){var o = observationpoint.split(" ");observationpoint=o[1];}
if(observationpoint.length == 0){observationpoint=System.Gadget.Settings.read("current_stadt");}

var winddisplay = "";
if(CF_var == 'C'){winddisplay = lang.winddisplay_k;}else{winddisplay = lang.winddisplay_m;}


if(System.Gadget.Settings.read("txtcolor") == '1')
{

/*black*/
document.getElementById("bg").addTextObject(lang.skytext, "Calibri", 12, "black", 10,71);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_skytext"), "Calibri", 12, "black", 120,71);
txtAlign.align = 2;
document.getElementById("bg").addTextObject(lang.feelslike, "Calibri", 12, "black", 10,83);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_feelslike")+"°"+CF_var, "Calibri", 12, "black", 121,83);
txtAlign.align = 2;
document.getElementById("bg").addTextObject(lang.humidity, "Calibri", 12, "black", 10,95);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_humidity")+"%", "Calibri", 12, "black", 122,95);txtAlign.align = 2;
document.getElementById("bg").addTextObject(lang.windspeed, "Calibri", 12, "black", 10,107);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_windspeed")+" "+winddisplay, "Calibri", 12, "black", 121,107);
txtAlign.align = 2;
document.getElementById("bg").addTextObject(lang.observationpoint, "Calibri", 12, "black", 10,119);
var txtAlign = bg.addTextObject(observationpoint, "Calibri", 12, "black", 120,119);
txtAlign.align = 2;
/*black*/


document.getElementById("bg").addTextObject(lang.skytext, "Calibri", 12, "white", 9,70);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_skytext"), "Calibri", 12, "white", 119,70);
txtAlign.align = 2;

document.getElementById("bg").addTextObject(lang.feelslike, "Calibri", 12, "white", 9,82);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_feelslike")+"°"+CF_var, "Calibri", 12, "white", 120,82);
txtAlign.align = 2;
document.getElementById("bg").addTextObject(lang.humidity, "Calibri", 12, "white", 9,94);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_humidity")+"%", "Calibri", 12, "white", 121,94);txtAlign.align = 2;
document.getElementById("bg").addTextObject(lang.windspeed, "Calibri", 12, "white", 9,106);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_windspeed")+" "+winddisplay, "Calibri", 12, "white", 120,106);
txtAlign.align = 2;
document.getElementById("bg").addTextObject(lang.observationpoint, "Calibri", 12, "white", 9,118);
var txtAlign = bg.addTextObject(observationpoint, "Calibri", 12, "white", 119,118);
txtAlign.align = 2;

}else{

document.getElementById("bg").addTextObject(lang.skytext, "Calibri", 12, "white", 10,71);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_skytext"), "Calibri", 12, "white", 120,71);
txtAlign.align = 2;

document.getElementById("bg").addTextObject(lang.feelslike, "Calibri", 12, "white", 10,83);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_feelslike")+"°"+CF_var, "Calibri", 12, "white", 121,83);
txtAlign.align = 2;
document.getElementById("bg").addTextObject(lang.humidity, "Calibri", 12, "white", 10,95);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_humidity")+"%", "Calibri", 12, "white", 122,95);txtAlign.align = 2;
document.getElementById("bg").addTextObject(lang.windspeed, "Calibri", 12, "white", 10,107);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_windspeed")+" "+winddisplay, "Calibri", 12, "white", 121,107);
txtAlign.align = 2;
document.getElementById("bg").addTextObject(lang.observationpoint, "Calibri", 12, "white", 10,118);
var txtAlign = bg.addTextObject(observationpoint, "Calibri", 12, "white", 120,119);
txtAlign.align = 2;
/*black*/
document.getElementById("bg").addTextObject(lang.skytext, "Calibri", 12, "black", 9,70);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_skytext"), "Calibri", 12, "black", 119,70);
txtAlign.align = 2;
document.getElementById("bg").addTextObject(lang.feelslike, "Calibri", 12, "black", 9,82);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_feelslike")+"°"+CF_var, "Calibri", 12, "black", 120,82);
txtAlign.align = 2;
document.getElementById("bg").addTextObject(lang.humidity, "Calibri", 12, "black", 9,94);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_humidity")+"%", "Calibri", 12, "black", 121,94);txtAlign.align = 2;
document.getElementById("bg").addTextObject(lang.windspeed, "Calibri", 12, "black", 9,106);
var txtAlign = bg.addTextObject(System.Gadget.Settings.read("current_windspeed")+" "+winddisplay, "Calibri", 12, "black", 120,106);
txtAlign.align = 2;
document.getElementById("bg").addTextObject(lang.observationpoint, "Calibri", 12, "black", 9,119);
var txtAlign = bg.addTextObject(observationpoint, "Calibri", 12, "black", 119,118);
txtAlign.align = 2;
/*black*/
} 

/*
document.getElementById("bg").addTextObject(lang.observationpoint, "Calibri", 12, "white", 9,126);
var txtAlign = bg.addTextObject(observationpoint, "Calibri", 12, "white", 119,126);txtAlign.align = 2;
*/


if(System.Gadget.Settings.read("txtcolor") == '1')
{

/*black*/
document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast1_day"), "Calibri", 12, "black", 50,146);
var tz1 = System.Gadget.Settings.read("forecast1_low")+"° / "+System.Gadget.Settings.read("forecast1_high")+"°";
document.getElementById("bg").addTextObject(tz1, "Calibri", 12, "black", 51,158);

document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast2_day"), "Calibri", 12, "black", 50,177);
var tz2 = System.Gadget.Settings.read("forecast2_low")+"° / "+System.Gadget.Settings.read("forecast2_high")+"°";
document.getElementById("bg").addTextObject(tz2, "Calibri", 12, "black", 51,189);

document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast3_day"), "Calibri", 12, "black", 50,208);
var tz3 = System.Gadget.Settings.read("forecast3_low")+"° / "+System.Gadget.Settings.read("forecast3_high")+"°";
document.getElementById("bg").addTextObject(tz3, "Calibri", 12, "black", 51,220);

document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast4_day"), "Calibri", 12, "black", 50,239);
var tz4 = System.Gadget.Settings.read("forecast4_low")+"° / "+System.Gadget.Settings.read("forecast4_high")+"°";
document.getElementById("bg").addTextObject(tz4, "Calibri", 12, "black", 51,251);
/*black*/

document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast1_day"), "Calibri", 12, "white", 49,145);
var tz1 = System.Gadget.Settings.read("forecast1_low")+"° / "+System.Gadget.Settings.read("forecast1_high")+"°";
document.getElementById("bg").addTextObject(tz1, "Calibri", 12, "white", 50,157);
bg.addImageObject("img/m"+styley+"/"+System.Gadget.Settings.read("forecast1_skycodeday")+".png", 15, 146);


document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast2_day"), "Calibri", 12, "white", 49,176);
var tz2 = System.Gadget.Settings.read("forecast2_low")+"° / "+System.Gadget.Settings.read("forecast2_high")+"°";
document.getElementById("bg").addTextObject(tz2, "Calibri", 12, "white", 50,188);
bg.addImageObject("img/m"+styley+"/"+System.Gadget.Settings.read("forecast2_skycodeday")+".png", 15, 177);


document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast3_day"), "Calibri", 12, "white", 49,207);
var tz3 = System.Gadget.Settings.read("forecast3_low")+"° / "+System.Gadget.Settings.read("forecast3_high")+"°";
document.getElementById("bg").addTextObject(tz3, "Calibri", 12, "white", 50,219);
bg.addImageObject("img/m"+styley+"/"+System.Gadget.Settings.read("forecast3_skycodeday")+".png", 15, 208);


document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast4_day"), "Calibri", 12, "white", 49,238);
var tz4 = System.Gadget.Settings.read("forecast4_low")+"° / "+System.Gadget.Settings.read("forecast4_high")+"°";
document.getElementById("bg").addTextObject(tz4, "Calibri", 12, "white", 50,250);
bg.addImageObject("img/m"+styley+"/"+System.Gadget.Settings.read("forecast4_skycodeday")+".png", 15, 239);

}else{

document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast1_day"), "Calibri", 12, "white", 50,146);
var tz1 = System.Gadget.Settings.read("forecast1_low")+"° / "+System.Gadget.Settings.read("forecast1_high")+"°";
document.getElementById("bg").addTextObject(tz1, "Calibri", 12, "white", 51,158);

document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast2_day"), "Calibri", 12, "white", 50,177);
var tz2 = System.Gadget.Settings.read("forecast2_low")+"° / "+System.Gadget.Settings.read("forecast2_high")+"°";
document.getElementById("bg").addTextObject(tz2, "Calibri", 12, "white", 51,189);

document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast3_day"), "Calibri", 12, "white", 50,208);
var tz3 = System.Gadget.Settings.read("forecast3_low")+"° / "+System.Gadget.Settings.read("forecast3_high")+"°";
document.getElementById("bg").addTextObject(tz3, "Calibri", 12, "white", 51,220);

document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast4_day"), "Calibri", 12, "white", 50,239);
var tz4 = System.Gadget.Settings.read("forecast4_low")+"° / "+System.Gadget.Settings.read("forecast4_high")+"°";
document.getElementById("bg").addTextObject(tz4, "Calibri", 12, "white", 51,251);



document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast1_day"), "Calibri", 12, "black", 49,145);
var tz1 = System.Gadget.Settings.read("forecast1_low")+"° / "+System.Gadget.Settings.read("forecast1_high")+"°";
document.getElementById("bg").addTextObject(tz1, "Calibri", 12, "black", 50,157);
bg.addImageObject("img/m"+styley+"/"+System.Gadget.Settings.read("forecast1_skycodeday")+".png", 15, 146);


document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast2_day"), "Calibri", 12, "black", 49,176);
var tz2 = System.Gadget.Settings.read("forecast2_low")+"° / "+System.Gadget.Settings.read("forecast2_high")+"°";
document.getElementById("bg").addTextObject(tz2, "Calibri", 12, "black", 50,188);
bg.addImageObject("img/m"+styley+"/"+System.Gadget.Settings.read("forecast2_skycodeday")+".png", 15, 177);


document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast3_day"), "Calibri", 12, "black", 49,207);
var tz3 = System.Gadget.Settings.read("forecast3_low")+"° / "+System.Gadget.Settings.read("forecast3_high")+"°";
document.getElementById("bg").addTextObject(tz3, "Calibri", 12, "black", 50,219);
bg.addImageObject("img/m"+styley+"/"+System.Gadget.Settings.read("forecast3_skycodeday")+".png", 15, 208);


document.getElementById("bg").addTextObject(System.Gadget.Settings.read("forecast4_day"), "Calibri", 12, "black", 49,238);
var tz4 = System.Gadget.Settings.read("forecast4_low")+"° / "+System.Gadget.Settings.read("forecast4_high")+"°";
document.getElementById("bg").addTextObject(tz4, "Calibri", 12, "black", 50,250);
bg.addImageObject("img/m"+styley+"/"+System.Gadget.Settings.read("forecast4_skycodeday")+".png", 15, 239);

}









}
function settingsClosed(p_event) {
    //OK clicked?
    if (p_event.closeAction == p_event.Action.commit) {
        //yes, read settings here
        load();
    }
}