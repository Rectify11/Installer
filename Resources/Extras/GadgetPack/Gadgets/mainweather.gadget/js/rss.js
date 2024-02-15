var Datum = new Date();
var Jahr = Datum.getFullYear();

var rssDoc = new ActiveXObject("MSXML2.DOMDocument.3.0");
rssDoc.onreadystatechange = popData;

function popData(){
if(rssDoc.readyState!=4)
return;

var c = 0;
var rssItems = rssDoc.selectNodes("/rss/channel/item");
for(var i=0;i<rssItems.length;i++)
{
var ititle  = rssItems[i].selectSingleNode("./title").text;
var idesc   = rssItems[i].selectSingleNode("./description").text;
var ilink   = rssItems[i].selectSingleNode("./link").text;
var ibild   = rssItems[i].selectSingleNode("./enclosure/@url").text;

var name    = idesc.substr(0, 4);
var version = idesc.substr(5, 2);
if(lang.itemname == name)
   {
      v(version,ititle,ilink,ibild);
      document.getElementById("txt3c").style.display = "none";
   }
else {

var c = c+1;
var max = rssItems.length-1;
document.getElementById("txt3a").innerHTML += '<div id="gadget'+c+'" style="display: none;"><div id="gadgettxt">'+ititle+': '+idesc.substr(8)+'</div><div id="dlbtn"><a href="'+ilink+'"><img src="../img/btn_download.png" border="0"></a></div><div id="bild"><a href="'+ilink+'"><img border="0" width="80" height="80" src="'+ibild+'"></a></div><div id="blaettern"><span id="prev"><a href="javascript:prev('+c+','+max+');"><img src="../img/btn_prev.png" border="0"></a></span><span id="next"><a href="javascript:next('+c+','+max+');"><img src="../img/btn_next.png" border="0"></a></span></div></div>';


}

}
var a = 1 + c*(Math.random());
a = Math.round(a);
var sett  = setTimeout("show("+a+","+c+");",2000);

}
function v(id,title,link,bild){
document.getElementById("topic2").innerHTML = title+'';
var igg = document.getElementById("speicher").innerHTML;
document.getElementById("txt2").innerHTML = '<div><div>&copy;'+Jahr+' '+lang.copy_rights+' <a href="http://patpossible.de/?page_id=62" style="color: #000000;">'+lang.copy_rights2+'</a> &nbsp; <a href="http://patpossible.de/?page_id=95" style="color: #000000;">'+lang.copy_rights3+'</a></div><div id="t" class="topic">'+lang.copy_rights4+'</div><div id="status"><a href="http://patpossible.de/messenger.php"><img border="0" src="'+igg+'"></a></div><div id="dlbtn"><a href="http://patpossible.de"><img src="../img/btn_web.png" border="0"></a> &nbsp; <a href="mailto:kontakt@live.de?subject=[Gadget] '+title+' ('+id+')"><img src="../img/btn_mail.png" border="0"></a> &nbsp; <a href="http://patpossible.de/messenger.php"><img src="../img/btn_im.png" border="0"></a></div></div>';

if(id > lang.version)
   {
      document.getElementById('nav3').innerText = lang.nav3b;
      document.getElementById('topic3').innerText = lang.topic3b;
      document.getElementById('txt3b').innerHTML = '<div id="gadgettxt">'+lang.update_txt+'</div><div id="dlbtn"><a href="'+link+'"><img src="../img/btn_update.png" border="0"></a></div><div id="bild"><a href="'+link+'"><img border="0" width="80" height="80" src="'+bild+'"></a></div>';
      document.getElementById("txt3a").style.display = 'none';
      document.getElementById("txt3b").style.display = 'block';
   }
}
function init(path){rssDoc.load(path);}function next(c,max) {document.getElementById("gadget"+c).style.display = 'none';c=c+1;if(c > max){c=1;}document.getElementById("gadget"+c).style.display = 'block';}function prev(c,max){document.getElementById("gadget"+c).style.display = 'none';c=c-1;if(c == 0){c=max;}document.getElementById("gadget"+c).style.display = 'block';}function show(a,max){document.getElementById("gadget"+a).style.display = "block";document.getElementById("txt3c").style.display = "none";}