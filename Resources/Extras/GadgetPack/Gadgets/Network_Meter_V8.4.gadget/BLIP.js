 nsp='Old browser!';
 dl=document.layers;
 oe=window.opera?1:0;
 da=document.all&&!oe;
 ge=document.getElementById;
 ws=window.sidebar?true:false;
 tN=navigator.userAgent.toLowerCase();
 izN=tN.indexOf('netscape')>=0?true:false;
 zis=tN.indexOf('msie 7')>=0?true:false;
 zis8=tN.indexOf('msie 8')>=0?true:false;
 zis|=zis8;
 if(ws&&!izN)
 {
   quogl='iuy'
 };
 var msg='';
 function nem()
 {
   return true
 };
 window.onerror = nem;
 zOF=window.location.protocol.indexOf("file")!=-1?true:false;
 i7f=zis&&!zOF?true:false;
 var ldata = new Array();
 var datacode='';
 function onLoad()
 {
   blRatio = System.Gadget.Settings.read("blRatio");
   if ( blRatio != "") ;
   else blRatio = "0/0";
   sizeMode();
   lines = blRatio.split(",");
   for (var i=1; i<lines.length; i++)
   {
     ldata[i] = lines[i].split("=");
     datacode=datacode+"<tr><td><img src='list"+ldata[i][0]+".png'></td><td>"+ldata[i][1]+"</td></tr>";
   }
   BLTable.innerHTML = "<span style='margin-left:5px;color:#90EE90'>Blacklisted IP Ratio: "+lines[0]+"</span><table id='table-style'><tr><td style='width:15px;'></td><td style='width:140px;color:#FFF62A'><u>List of DNSBL</u></td></tr>"+datacode+"</table>";
   listed.innerHTML = "<img src='list0.png'> = Not Listed<br><img src='list1.png'> = Listed";
   linkBL.innerHTML ="<a href='http://addgadgets.com/ip_blacklist/'>Click here to check other IP Address</a>";
 }
 function refreshDNSBL()
 {
   System.Gadget.Settings.write("reloadBL", 1);
   System.Gadget.Flyout.show = false;
 }
 function sizeMode()
 {
   document.body.style.width = 280;
   document.body.style.height = 216;
   document.body.style.backgroundColor = "#080808";
   document.body.style.fontFamily = "Segoe UI";
   document.body.style.fontSize = 12;
   document.body.style.color = "#87cefa";
   document.getElementById('refreshBL').style.top = 17;
   document.getElementById('refreshBL').style.left = 145;
   document.getElementById('listed').style.top = 38;
   document.getElementById('listed').style.right = 5;
   document.linkColor = "#ff0000";
   document.vlinkColor = "#ff0000";
   document.alinkColor = "#ff0000";
 }
 