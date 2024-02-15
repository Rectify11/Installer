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
   lines = data.split("\n");
   for(var i=lines.length; i--;)
   {
     ldata[i] = lines[i].split(",");
     datacode=datacode+"<tr><td>"+ldata[i][0]+" "+ldata[i][1]+"</td><td>"+ldata[i][2]+"</td><td>"+ldata[i][3]+"</td></tr>";
   }
   ipDataTable.innerHTML = "<table id='table-style'><tr><th scope='col' style='width:160px;'>Time</th><th scope='col' style='width:140px;'>Internal IP Address</th><th scope='col' style='width:140px;'>External IP Address</th></tr>"+datacode+"</table>";
 }
 
 