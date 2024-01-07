function loading(){

// text color
var tari = "";
var txtcolor_var = "";
    txtcolor_var = System.Gadget.Settings.read("txtcolor");
if(txtcolor_var == '1')
{
tari  = "<div id='txtcolor1'><div id='txtC1'><input type='radio' name='txtcolor' onclick='txtcolory(\"1\");' value='1' checked></div><div id='txtC2'><img src='img/txtC1.png'></div>";
tari += "<div id='txtC3'><input type='radio' name='txtcolor' onclick='txtcolory(\"2\");' value='2'></div><div id='txtC4'><img src='img/txtC2.png'></div></div>";
}else{
tari  = "<div id='txtcolor1'><div id='txtC1'><input type='radio' name='txtcolor' onclick='txtcolory(\"1\");' value='1'></div><div id='txtC2'><img src='img/txtC1.png'></div>";
tari += "<div id='txtC3'><input type='radio' name='txtcolor' onclick='txtcolory(\"2\");' value='2' checked></div><div id='txtC4'><img src='img/txtC2.png'></div></div>";
}   
// text color



var tar = "";
var userAgenty_var = "";
    userAgenty_var = System.Gadget.Settings.read("userAgenty");
    //confirm(userAgenty_var);

var win = System.Gadget.Settings.read("win"); 
if(win == 'vista'){
if(userAgenty_var == false)
{
tar="<div id='userAgenty1'><div id='userA1'><input type='checkbox' name='userAgenty' id='userAgenty' value='0'></div><div id='userA2'>"+lang.userAgent+"</div></div>";
}else{
tar="<div id='userAgenty1'><div id='userA1'><input type='checkbox' name='userAgenty' id='userAgenty' value='0' checked></div><div id='userA2'>"+lang.userAgent+"</div></div>";
}
}


document.getElementById("txt1").innerHTML += '<div id="paypal_btn" style="position:absolute; top: 144px; left: 179px;"><a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=4149245"><img border="0" src="../img/btn_spenden.png"></a></div>'; 

var s = "";
var styley_var = "";
    styley_var = System.Gadget.Settings.read("styley");
if(styley_var == '1')
{    
s  = "<div id='styley1'><div id='sty1'><input type='radio' name='styley' onclick='styley(\"1\");' value='1' checked></div><div id='sty2'><img src='img/m1/28.png'></div>";
s += "<div id='sty3'><input type='radio' name='styley' onclick='styley(\"2\");' value='2'></div><div id='sty4'><img src='img/m2/28.png'></div></div>";
}else{ 
s  = "<div id='styley1'><div id='sty1'><input type='radio' name='styley' onclick='styley(\"1\");' value='1'></div><div id='sty2'><img src='img/m1/28.png'></div>";
s += "<div id='sty3'><input type='radio' name='styley' onclick='styley(\"2\");' value='2' checked></div><div id='sty4'><img src='img/m2/28.png'></div></div>";
}




var gg = "";
var confirmy_var = "";
    confirmy_var = System.Gadget.Settings.read("confirmy"); 
if(confirmy_var == false)
{ 
gg="<div id='alert'><div id='al1'><input type='checkbox' name='confirmy' id='confirmy' value='0'></div><div id='al2'>"+lang.alert+"</div></div>";  
}else{ 
gg="<div id='alert'><div id='al1'><input type='checkbox' name='confirmy' id='confirmy' value='0' checked></div><div id='al2'>"+lang.alert+"</div></div>";    
}



    
var e = "";
var CF_var = "";
    CF_var = System.Gadget.Settings.read("CF");
    
if(CF_var == 'F')
{
e = "<div id='cf1'><div id='cf_radio'><input type='radio' name='cf' onclick='CF(\"C\");' value='C'></div><div id='cf_image'>"+lang.cf+"</div></div>";
e += "<div id='cf2'><div id='cf_radio'><input type='radio' name='cf' onclick='CF(\"F\");' value='F' checked></div><div id='cf_image'>"+lang.ff+"</div></div>";
}else{
e = "<div id='cf1'><div id='cf_radio'><input type='radio' name='cf' onclick='CF(\"C\");' value='C' checked></div><div id='cf_image'>"+lang.cf+"</div></div>";
e += "<div id='cf2'><div id='cf_radio'><input type='radio' name='cf' onclick='CF(\"F\");' value='F'></div><div id='cf_image'>"+lang.ff+"</div></div>";
} 

var d = "";
var style_var = "";
    style_var = System.Gadget.Settings.read("style");

d = style_var;
if(style_var == 'c')
{
d = "<div style='padding-top: 8px;'><div id='c01'><div id='c_radio'><input type='radio' name='style' onclick='st(\"c\");' value='c' checked></div><div id='c_image'><img src='../img/c.png'></div></div>";
d += "<div id='c02'><div id='c_radio'><input type='radio' name='style' onclick='st(\"w\");' value='w'></div><div id='c_image'><img src='../img/w.png'></div></div>";
d += "<div id='c03'><div id='c_radio'><input type='radio' name='style' onclick='st(\"b\");' value='b' ></div><div id='c_image'><img src='../img/b.png'></div></div>";
d += "<div id='c04'><div id='c_radio'><input type='radio' name='style' onclick='st(\"blue\");' value='blue' ></div><div id='c_image'><img src='../img/blue.png'></div></div>";
d += "<div id='c05'><div id='c_radio'><input type='radio' name='style' onclick='st(\"r\");' value='r' ></div><div id='c_image'><img src='../img/r.png'></div></div>";
d += "</div>";
}
else if(style_var == 'w')
{
d = "<div style='padding-top: 8px;'><div id='c01'><div id='c_radio'><input type='radio' name='style' onclick='st(\"c\");' value='c'></div><div id='c_image'><img src='../img/c.png'></div></div>";
d += "<div id='c02'><div id='c_radio'><input type='radio' name='style' onclick='st(\"w\");' value='w' checked></div><div id='c_image'><img src='../img/w.png'></div></div>";
d += "<div id='c03'><div id='c_radio'><input type='radio' name='style' onclick='st(\"b\");' value='b' ></div><div id='c_image'><img src='../img/b.png'></div></div>";
d += "<div id='c04'><div id='c_radio'><input type='radio' name='style' onclick='st(\"blue\");' value='blue' ></div><div id='c_image'><img src='../img/blue.png'></div></div>";
d += "<div id='c05'><div id='c_radio'><input type='radio' name='style' onclick='st(\"r\");' value='r' ></div><div id='c_image'><img src='../img/r.png'></div></div>";
d += "</div>";
}
else if(style_var == 'blue')
{
d = "<div style='padding-top: 8px;'><div id='c01'><div id='c_radio'><input type='radio' name='style' onclick='st(\"c\");' value='c'></div><div id='c_image'><img src='../img/c.png'></div></div>";
d += "<div id='c02'><div id='c_radio'><input type='radio' name='style' onclick='st(\"w\");' value='w'></div><div id='c_image'><img src='../img/w.png'></div></div>";
d += "<div id='c03'><div id='c_radio'><input type='radio' name='style' onclick='st(\"b\");' value='b' ></div><div id='c_image'><img src='../img/b.png'></div></div>";
d += "<div id='c04'><div id='c_radio'><input type='radio' name='style' onclick='st(\"blue\");' value='blue' checked></div><div id='c_image'><img src='../img/blue.png'></div></div>";
d += "<div id='c05'><div id='c_radio'><input type='radio' name='style' onclick='st(\"r\");' value='r' ></div><div id='c_image'><img src='../img/r.png'></div></div>";
d += "</div>";
}
else if(style_var == 'r')
{
d = "<div style='padding-top: 8px;'><div id='c01'><div id='c_radio'><input type='radio' name='style' onclick='st(\"c\");' value='c'></div><div id='c_image'><img src='../img/c.png'></div></div>";
d += "<div id='c02'><div id='c_radio'><input type='radio' name='style' onclick='st(\"w\");' value='w'></div><div id='c_image'><img src='../img/w.png'></div></div>";
d += "<div id='c03'><div id='c_radio'><input type='radio' name='style' onclick='st(\"b\");' value='b' ></div><div id='c_image'><img src='../img/b.png'></div></div>";
d += "<div id='c04'><div id='c_radio'><input type='radio' name='style' onclick='st(\"blue\");' value='blue' ></div><div id='c_image'><img src='../img/blue.png'></div></div>";
d += "<div id='c05'><div id='c_radio'><input type='radio' name='style' onclick='st(\"r\");' value='r' checked></div><div id='c_image'><img src='../img/r.png'></div></div>";
d += "</div>";
}
else 
{
d = "<div style='padding-top: 8px;'><div id='c01'><div id='c_radio'><input type='radio' name='style' onclick='st(\"c\");' value='c'></div><div id='c_image'><img src='../img/c.png'></div></div>";
d += "<div id='c02'><div id='c_radio'><input type='radio' name='style' onclick='st(\"w\");' value='w'></div><div id='c_image'><img src='../img/w.png'></div></div>";
d += "<div id='c03'><div id='c_radio'><input type='radio' name='style' onclick='st(\"b\");' value='b' checked></div><div id='c_image'><img src='../img/b.png'></div></div>";
d += "<div id='c04'><div id='c_radio'><input type='radio' name='style' onclick='st(\"blue\");' value='blue' ></div><div id='c_image'><img src='../img/blue.png'></div></div>";
d += "<div id='c05'><div id='c_radio'><input type='radio' name='style' onclick='st(\"r\");' value='r' ></div><div id='c_image'><img src='../img/r.png'></div></div>";
d += "</div>";
}
/*    
if(style_var == "c")
{
d = "<input type='radio' name='style' onclick='style(\"c\");' value='c' checked> <img src='../img/c.png'><br><input type='radio' name='style' onclick='style(\"w\");' value='w'> e"+style_var+"<img src='../img/w.png'><br><input type='radio' name='style' onclick='style(\"b\");' value='b' checked> <img src='../img/b.png'>";
} 
else if (style_var == "w")
{
d = "<input type='radio' name='style' onclick='style(\"c\");' value='c'> <img src='../img/c.png'><br><input type='radio' name='style' onclick='style(\"w\");' value='w' checked> e"+style_var+"<img src='../img/w.png'><br><input type='radio' name='style' onclick='style(\"b\");' value='b' checked> <img src='../img/b.png'>";
}
else
{

d = "<input type='radio' name='style' onclick='style(\"c\");' value='c'> <img src='../img/c.png'><br><input type='radio' name='style' onclick='style(\"w\");' value='w'> e"+style_var+"<img src='../img/w.png'><br><input type='radio' name='style' onclick='style(\"b\");' value='b' checked> <img src='../img/b.png' checked>";
}      
*/ 


   var show  = "";        
       show  = System.Gadget.Settings.read("name");
       show  = show.toString();
   var hello = "";
       if(show == ''){hello = lang.hello;document.getElementById("paypal_btn").style.display="none";}else{hello = lang.hello2;}
   
//document.getElementById("txt1").innerHTML += '<div><div id="CF">'+e+'</div><div id="style">'+d+'</div><div id="search"><img onclick="search();" src="../img/search.png"></div><div><input id="show" class="box" type="text" name="show" value="'+show+'"></div><div id="erg">'+hello+'</div><div id="erg2"></div><!--<div id="dlbtn"><a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=4149245"><img border="0" src="../img/btn_spenden.png"></a></div>--><!--<div class="topic" style="padding-top: 15px;">Mein Wetter Pro</div><div style="padding-top: 0px;">• Weltweites Wetter<br />• 3-Tages-Vorschau<br />• Exklusive Previews</div></div>-->';




document.getElementById("txt1").innerHTML += '<div class="barname bar1">'+lang.bar1+'</div>';
document.getElementById("txt1").innerHTML += '<div class="barname bar2">'+lang.bar2+'</div>';
document.getElementById("txt1").innerHTML += '<div class="barname bar3">'+lang.bar3+'</div>';
document.getElementById("txt1").innerHTML += '<div id="barchose1"><img src="../img/bar_chose.png"></div>';
document.getElementById("txt1").innerHTML += '<div id="barchose2"><img src="../img/bar_chose.png"></div>';
document.getElementById("txt1").innerHTML += '<div id="barchose3"><img src="../img/bar_chose.png"></div>';
document.getElementById("txt1").innerHTML += '<div class="chose1" onclick="c(\'1\');"><img src="../img/blank.png" width="73" height="32"></div>';
document.getElementById("txt1").innerHTML += '<div class="chose2" onclick="c(\'2\');"><img src="../img/blank.png" width="73" height="32"></div>';
document.getElementById("txt1").innerHTML += '<div class="chose3" onclick="c(\'3\');"><img src="../img/blank.png" width="73" height="32"></div>';
document.getElementById("txt1").innerHTML += '<div id="bar"><img src="../img/bar.png"></div>';

document.getElementById("txt1").innerHTML += '<div id="con1"><div id="search"><img onclick="search();" src="../img/search.png"></div><div><input id="show" class="box" type="text" name="show" value="'+show+'"></div><div id="erg">'+hello+'</div><div id="erg2"></div></div>';
document.getElementById("txt1").innerHTML += '<div id="con2">'+d+''+s+''+tari+'</div>';
document.getElementById("txt1").innerHTML += '<div id="con3">'+e+''+gg+''+tar+'</div>';


   var bar  = "";
       bar  = System.Gadget.Settings.read("bar");
       if(bar == ''){bar=1;}
       c(bar)

document.getElementById("topic1").innerHTML = "";
}

function c(id)
{
System.Gadget.Settings.write("bar", id);
document.getElementById("barchose1").style.display = "none";
document.getElementById("barchose2").style.display = "none";
document.getElementById("barchose3").style.display = "none";
document.getElementById("con1").style.display = "none";
document.getElementById("con2").style.display = "none";
document.getElementById("con3").style.display = "none";
document.getElementById("barchose"+id).style.display = "block";
document.getElementById("con"+id).style.display = "block";
}
function navigation(id)
{
   document.getElementById('nav1').className = "deaktiv";
   document.getElementById('nav2').className = "deaktiv";
   document.getElementById('nav3').className = "deaktiv";
   document.getElementById('nav'+id).className = "aktiv";
   document.getElementById('content1').style.display   = "none";
   document.getElementById('content2').style.display   = "none";
   document.getElementById('content3').style.display   = "none";
   document.getElementById('content'+id).style.display = "block";
}


function search()
{

document.getElementById('erg').innerHTML = "<div style='padding-top:5px;'>"+lang.wait+"</div>";
var rssDoc2 = new ActiveXObject("MSXML2.DOMDocument.3.0");
rssDoc2.onreadystatechange = popData2;
var search=document.getElementById('show').value;
search = encodeURI(search);
if(search == ''){search="New York";}

rssDoc2.load("http://weather.service.msn.com/data.aspx?src=vista&weasearchstr="+search+"&weadegreetype=C&culture=de-DE");

function popData2()
{
if(rssDoc2.readyState!=4)return;
var c2 = 0;
var rssItems2 = rssDoc2.selectNodes("/weatherdata/weather");
var max = 3;
if(rssItems2.length < max){max = rssItems2.length;}
document.getElementById('erg').innerHTML = "";
if(rssItems2.length == 0){document.getElementById('erg').innerHTML = "<div style='padding-top:5px;'>"+wait2+"</div>";}
for(var i=0;i<max;i++)
{
var icode  = rssItems2[i].selectSingleNode("./@weatherlocationcode").text;
var iname  = rssItems2[i].selectSingleNode("./@weatherlocationname").text;

document.getElementById('erg').innerHTML += "<input type='radio' name='i' onclick='save(\""+icode+"\", \""+iname+"\");' value='"+icode+"'> "+iname+"<br>";

document.getElementById("paypal_btn").style.display="block";
}
}
}


function save(code,name)
{
System.Gadget.Settings.write("show", code);
System.Gadget.Settings.write("name", name);
}
function CF(CF)
{
System.Gadget.Settings.write("CF", CF);
}
function st(CF)
{
System.Gadget.Settings.write("style", CF);
}
function styley(CF)
{
System.Gadget.Settings.write("styley", CF);
}
function txtcolory(CF)
{
System.Gadget.Settings.write("txtcolor", CF);
}




function close() {
   closeAction();
}

System.Gadget.onSettingsClosing = SettingsClosing;
function SettingsClosing(event)
   {
      if (event.closeAction == event.Action.commit)
         {
             var id = ""; 
                 id = id.toString(); 
                 //id = document.getElementById('show').value;
                 id = document.getElementsByName('i').value;
             //System.Gadget.Settings.write("show", id);  
             var win        = System.Gadget.Settings.read("win"); 
             if(win=='vista'){
             var userAgenty = document.getElementById('userAgenty').checked;
             System.Gadget.Settings.write("userAgenty", userAgenty);             
             }
                                     
             var confirmy = document.getElementById('confirmy').checked;
             System.Gadget.Settings.write("confirmy", confirmy);
             
         }
      event.cancel = false;
   }