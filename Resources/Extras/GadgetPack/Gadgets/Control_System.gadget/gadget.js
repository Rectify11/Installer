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
 System.Gadget.settingsUI = "settings.html";
 System.Gadget.onSettingsClosed = onSettingsClosed;
 var size = 1;
 var update = 1;
 var sizeUpdate = 1;
 var sliderOn=2;
 var sliderleft=-123;
 var O=document.getElementById;
 function onLoad()
 {
   var Startup = System.Gadget.Settings.read ("Startup");
   if ( Startup == "1")
   {
     loadSettings();
   }
   else
   {
     loadfilesettings();
   }
   slider();
 }
 function onSettingsClosed()
 {
   loadSettings();
 }
 function loadSettings()
 {
   size = System.Gadget.Settings.read("size");
   if ( size != "") ;
   else size = "1";
   if (size <= "4") ;
   else size = "4";
   showClock = System.Gadget.Settings.read("showClock");
   if ( showClock != "") ;
   else showClock = "1";
   update = System.Gadget.Settings.read("update");
   if ( update != "") ;
   else update = "1";
   backg = System.Gadget.Settings.read("backg");
   if ( backg != "") ;
   else backg = "#080808";
   fixBackg = System.Gadget.Settings.read("fixBackg");
   if (fixBackg != "") sBackg = fixBackg ;
   else sBackg = backg;
   title = System.Gadget.Settings.read("title");
   if ( title != "") ;
   else title = "#ffffff";
   fixTitle = System.Gadget.Settings.read("fixTitle");
   if (fixTitle != "") sTitle = fixTitle ;
   else sTitle = title;
   timedMsg();
   sizeMode();
   startClock();
 }
 function loadfilesettings()
 {
   var fso = new ActiveXObject("Scripting.FileSystemObject");
   var inifilename = System.Environment.getEnvironmentVariable("APPDATA") + "\\" + System.Gadget.name + "_Settings.ini";
   try
   {
     var inifile = fso.OpenTextFile(inifilename, 1);
     try
     {
       var tmp = inifile.ReadLine();
       tmp = inifile.ReadLine();
       if (tmp != ";v1") throw "old";
       fixsize = inifile.ReadLine();
       System.Gadget.Settings.write("fixsize", fixsize);
       ssize = inifile.ReadLine();
       System.Gadget.Settings.write("ssize", ssize);
       size = inifile.ReadLine();
       System.Gadget.Settings.write("size", size);
       showClock = inifile.ReadLine();
       System.Gadget.Settings.write("showClock", showClock);
       update = inifile.ReadLine();
       System.Gadget.Settings.write("update", update);
       backg = inifile.ReadLine();
       System.Gadget.Settings.write("backg", backg);
       fixBackg = inifile.ReadLine();
       System.Gadget.Settings.write("fixBackg", fixBackg);
       title = inifile.ReadLine();
       System.Gadget.Settings.write("title", title);
       fixTitle = inifile.ReadLine();
       System.Gadget.Settings.write("fixTitle", fixTitle);
     }
     finally
     {
       inifile.Close();
     }
   }
   catch (err)
   {
   }
   var Startup = 1;
   System.Gadget.Settings.write("Startup", Startup);
   loadSettings();
 }
 function startClock()
 {
   if(showClock==1)
   {
     stopclock();
     showtime();
   }
 }
 var timerID = null;
 var timerRunning = false;
 function MakeArray(size)
 {
   this.length = size;
   for(var i = 1; i <= size; i++)
   {
     this[i] = "";
   }
   return this;
 }
 function stopclock ()
 {
   if(timerRunning) clearTimeout(timerID);
   timerRunning = false
 }
 function showtime ()
 {
   var now = new Date();
   var hours = now.getHours();
   var minutes = now.getMinutes();
   var seconds = now.getSeconds();
   var timeValue = "";
   timeValue += ((hours <= 12) ? hours : hours - 12);
   timeValue += ((minutes < 10) ? ":0" : ":") + minutes;
   timeValue += ((seconds < 10) ? ":0" : ":") + seconds;
   timeValue += (hours < 12) ? " AM" : " PM";
   clock.innerHTML = timeValue;
   timerID = setTimeout("showtime()",1000);
   timerRunning = true
 }
 function timedMsg()
 {
   if (update == 1)
   {
     setTimeout("showUpdate2()",60000);
   }
   else if (update == 2)
   {
     sizeUpdate = 0;
     O('newUpdate').style.visibility = "hidden";
   }
 }
 function getURL2(a)
 {
   try
   {
     var xmlReq=new ActiveXObject("Microsoft.XMLHTTP");
     xmlReq.open("GET",a,false);
     xmlReq.setRequestHeader("If-Modified-Since", "Sat, 1 Jan 2000 00:00:00 GMT");
     xmlReq.send();
     if(xmlReq.status==200)
     {
       return xmlReq.responseText
     }
     else
     {
       return false
     }
   }
   catch(urlData)
   {
     setTimeout("showUpdate2()",600000);
   }
 }
 function updateAvailable()
 {
   var urlData=getURL2("http://addgadgets.com/control_system/version.htm");
   if(urlData===false)
   {
     return false
   }
   var version="2.0";
   var a=parseFloat(version);
   var b=parseFloat(urlData);
   return b>a;
 }
 function showUpdate2()
 {
   if(updateAvailable())
   {
     O('newUpdate').style.visibility = "visible";
     O('newUpdate').innerHTML = ("New version is available");
     sizeUpdate = 10;
     sizeMode();
   }
 }
 function slider()
 {
   if(sliderOn==1)
   {
     sliderInClear = setInterval("sliderIn()",parseInt(1));
     sliderOn=2;
     clearInterval(sliderOutClear);
   }
   else
   {
     sliderOutClear = setInterval("sliderOut()",parseInt(1));
     sliderOn=1;
     clearInterval(sliderInClear);
   }
 }
 function sliderIn()
 {
   sliderleft=sliderleft-5;
   O('slider').style.left = parseInt( sliderleft * size );
   if(sliderleft<=-123)
   {
     clearInterval(sliderInClear);
   }
 }
 function sliderOut()
 {
   sliderleft=sliderleft+5;
   O('slider').style.left = parseInt( sliderleft * size );
   if(sliderleft>=7)
   {
     clearInterval(sliderOutClear);
   }
 }
 function sizeMode()
 {
   if(showClock==1)
   {
     background.src = "70_back.png";
     document.body.style.height = Math.round( ( 70 + sizeUpdate ) * size );
     O('background').style.height = Math.round( ( 70 + sizeUpdate ) * size );
     allHeight = parseInt( 70 + sizeUpdate );
     O('clock').style.visibility = "visible";
     O('clock').style.top = parseInt( 4 * size );
     O('slider').style.top = parseInt( 20 * size );
     O('lock').style.top = parseInt( 23 * size );
     O('lock2').style.top = parseInt( 46 * size );
     O('newUpdate').style.top = parseInt( 64 * size );
   }
   else
   {
     background.src = "56_back.png";
     document.body.style.height = Math.round( ( 56 + sizeUpdate ) * size );
     O('background').style.height = Math.round( ( 56 + sizeUpdate ) * size );
     allHeight = parseInt( 56 + sizeUpdate );
     O('clock').style.visibility = "hidden";
     O('clock').style.top = parseInt( 4 * size );
     O('slider').style.top = parseInt( 4 * size );
     O('lock').style.top = parseInt( 7 * size );
     O('lock2').style.top = parseInt( 30 * size );
     O('newUpdate').style.top = parseInt( 48 * size );
   }
   document.body.style.width = Math.round( 130 * size );
   O('background').style.width = Math.round( 130 * size );
   O('backgc').style.background = sBackg;
   O('backgc').style.top = Math.round( 3 * size );
   O('backgc').style.left = Math.round( 3 * size );
   O('backgc').style.width =  Math.round( 124 * size );
   O('backgc').style.height = Math.round( ( allHeight - 6 ) * size );
   O('blackwhite').style.top = Math.round( 3 * size );
   O('blackwhite').style.left = Math.round( 3 * size );
   O('blackwhite').style.width =  Math.round( 124 * size );
   O('blackwhite').style.height = Math.round( ( allHeight - 6 ) * size );
   O('clock').style.width = parseInt( 130 * size );
   O('lock').style.width = parseInt( 130 * size );
   O('Standby').style.width =  parseInt( 20 * size );
   O('Standby').style.height = parseInt( 20 * size );
   O('Shutdown').style.width =  parseInt( 20 * size );
   O('Shutdown').style.height = parseInt( 20 * size );
   O('Restart').style.width =  parseInt( 20 * size );
   O('Restart').style.height = parseInt( 20 * size );
   O('Logoff').style.width =  parseInt( 20 * size );
   O('Logoff').style.height = parseInt( 20 * size );
   O('Hibernate').style.width =  parseInt( 20 * size );
   O('Hibernate').style.height = parseInt( 20 * size );
   O('lock2').style.left = parseInt( 15 * size );
   O('lock2').style.width =  parseInt( 100 * size );
   O('lock2').style.height = parseInt( 20 * size );
   O('slider').style.width =  parseInt( 116 * size );
   O('slider').style.height = parseInt( 25 * size );
   O('newUpdate').style.width = parseInt( 130 * size );
   O('newUpdate').style.color = "#FF0000";
   document.body.style.fontSize = parseInt( 9 * size );
   O('clock').style.fontSize = parseInt( 12 * size );
   O('clock').style.color = sTitle;
 }
 