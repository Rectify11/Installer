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
 var O=document.getElementById;
 function onLoad()
 {
   initSettings();
   System.Gadget.onSettingsClosing = SettingsClosing;
 }
 function initSettings()
 {
   loadSettings();
   sizeSettings();
   colorSettings();
   showColor();
 }
 function SettingsClosing(event)
 {
   if (event.closeAction == event.Action.commit)
   {
     System.Gadget.Settings.write("fixsize", fixsize.value);
     System.Gadget.Settings.write("ssize", ssize.value);
     if(fixsize.value == "Custom")
     {
       System.Gadget.Settings.write("size", ssize.value / 100) ;
     }
     else
     {
       System.Gadget.Settings.write("size", fixsize.value / 100)
     }
     System.Gadget.Settings.write("showClock", showClock.value);
     System.Gadget.Settings.write("update", update.value);
     System.Gadget.Settings.write("backg","#" + backg.value);
     System.Gadget.Settings.write("fixBackg",fixBackg.value);
     System.Gadget.Settings.write("title","#" + title.value);
     System.Gadget.Settings.write("fixTitle",fixTitle.value);
     savefilesettings();
   }
   event.cancel = false;
 }
 function loadSettings()
 {
   gfixsize = System.Gadget.Settings.read("fixsize");
   if (gfixsize != '') fixsize.value = gfixsize;
   else fixsize.value='100';
   gssize = System.Gadget.Settings.read("ssize");
   if (gssize != '') ssize.value = gssize;
   else ssize.value='100';
   gshowClock = System.Gadget.Settings.read("showClock");
   if (gshowClock != '') showClock.value = gshowClock;
   else showClock.value='1';
   gupdate = System.Gadget.Settings.read("update");
   if (gupdate != '') update.value = gupdate;
   else update.value='1';
   var gbackg = System.Gadget.Settings.read("backg");
   if (gbackg != '') backg.value = gbackg;
   else backg.value='080808';
   var gfixBackg = System.Gadget.Settings.read("fixBackg");
   if (gfixBackg != '') fixBackg.value = gfixBackg;
   else fixBackg.value='';
   gtitle = System.Gadget.Settings.read("title");
   if (gtitle != '') title.value = gtitle;
   else title.value='ffffff';
   gfixTitle = System.Gadget.Settings.read("fixTitle");
   if (gfixTitle != '') fixTitle.value = gfixTitle;
   else fixTitle.value='';
 }
 function savefilesettings()
 {
   var fs = new ActiveXObject("Scripting.FileSystemObject");
   var inifilename = System.Environment.getEnvironmentVariable("APPDATA") + "\\" + System.Gadget.name + "_Settings.ini";
   try
   {
     var inifile = fs.OpenTextFile(inifilename, 2, true);
     try
     {
       inifile.WriteLine(";Control System (c) 2007-2013 AddGadgets.com");
       inifile.WriteLine(";v1");
       inifile.WriteLine(fixsize.value);
       inifile.WriteLine(ssize.value);
       if(fixsize.value == "Custom")
       {
         inifile.WriteLine(ssize.value / 100)
       }
       else
       {
         inifile.WriteLine(fixsize.value / 100)
       }
       inifile.WriteLine(showClock.value);
       inifile.WriteLine(update.value);
       inifile.WriteLine("#" + backg.value);
       inifile.WriteLine(fixBackg.value);
       inifile.WriteLine("#" + title.value);
       inifile.WriteLine(fixTitle.value);
     }
     finally
     {
       inifile.Close();
     }
   }
   catch (err)
   {
   }
 }
 function sizeSettings()
 {
   if(fixsize.value == "Custom")
   {
     O('ssize').style.visibility  = "visible";
     O('sizetext').style.visibility  = "visible";
   }
   else
   {
     O('ssize').style.visibility  = "hidden";
     O('sizetext').style.visibility  = "hidden";
   }
 }
 function colorSettings()
 {
   if(fixBackg.value == '')
   {
     O('backg').style.visibility  = "visible";
     O('showBackg').style.visibility  = "hidden";
   }
   else
   {
     O('backg').style.visibility  = "hidden";
     O('showBackg').style.visibility  = "visible";
   }
   if(fixTitle.value == '')
   {
     O('title').style.visibility  = "visible";
     O('showTitle').style.visibility  = "hidden";
   }
   else
   {
     O('title').style.visibility  = "hidden";
     O('showTitle').style.visibility  = "visible";
   }
 }
 function showColor()
 {
   if (fixBackg.value != "") sBackg = fixBackg.value ;
   else sBackg = backg.value;
   O('showBackg').style.color = sBackg;
   O('showBackg').style.width = 50;
   O('showBackg').style.height = 21;
   O('showBackg').style.top = 6;
   O('showBackg').style.left = 206;
   if (fixTitle.value != "") sTitle = fixTitle.value ;
   else sTitle = title.value;
   O('showTitle').style.color = sTitle;
   O('showTitle').style.width = 50;
   O('showTitle').style.height = 21;
   O('showTitle').style.top = 29;
   O('showTitle').style.left = 206;
 }
 function getURL(a)
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
     return false
   }
 }
 function updateAvailable()
 {
   var urlData=getURL("http://addgadgets.com/control_system/version.htm");
   if(urlData===false)
   {
     return false
   }
   var version="2.0";
   var a=parseFloat(version);
   var b=parseFloat(urlData);
   return b>a;
 }
 function showUpdate()
 {
   if(updateAvailable())
   {
     O("newUpdate").style.display="block"
   }
   else
   {
     O("noUpdate").style.display="block"
   }
 }
 function DefOptionsSetting()
 {
   showClock.value= 1;
   update.value= 1;
 }
 function DefDisplaySetting()
 {
   ssize.value= 100;
   fixsize.value= 100;
 }
 function DefColorSetting()
 {
   backg.value= "080808";
   fixBackg.value= "";
   title.value="FFFFFF";
   fixTitle.value="";
 }
 function tabberObj(argsObj)
 {
   var arg;
   this.div = null;
   this.classMain = "tabber";
   this.classMainLive = "tabberlive";
   this.classTab = "tabbertab";
   this.classTabDefault = "tabbertabdefault";
   this.classNav = "tabbernav";
   this.classTabHide = "tabbertabhide";
   this.classNavActive = "tabberactive";
   this.titleElements = ['h2','h3','h4','h5','h6'];
   this.titleElementsStripHTML = true;
   this.removeTitle = true;
   this.addLinkId = false;
   this.linkIdFormat = '<tabberid>nav<tabnumberone>';
   for (arg in argsObj)
   {
     this[arg] = argsObj[arg];
   }
   this.REclassMain = new RegExp('\\b' + this.classMain + '\\b', 'gi');
   this.REclassMainLive = new RegExp('\\b' + this.classMainLive + '\\b', 'gi');
   this.REclassTab = new RegExp('\\b' + this.classTab + '\\b', 'gi');
   this.REclassTabDefault = new RegExp('\\b' + this.classTabDefault + '\\b', 'gi');
   this.REclassTabHide = new RegExp('\\b' + this.classTabHide + '\\b', 'gi');
   this.tabs = new Array();
   if (this.div)
   {
     this.init(this.div);
     this.div = null;
   }
 }
 tabberObj.prototype.init = function(e)
 {
   var childNodes, i, i2, t, defaultTab=0, DOM_ul, DOM_li, DOM_a, aId, headingElement;
   if (!document.getElementsByTagName)
   {
     return false;
   }
   if (e.id)
   {
     this.id = e.id;
   }
   this.tabs.length = 0;
   childNodes = e.childNodes;
   for(i=0; i < childNodes.length; i++)
   {
     if(childNodes[i].className && childNodes[i].className.match(this.REclassTab))
     {
       t = new Object();
       t.div = childNodes[i];
       this.tabs[this.tabs.length] = t;
       if (childNodes[i].className.match(this.REclassTabDefault))
       {
         defaultTab = this.tabs.length-1;
       }
     }
   }
   DOM_ul = document.createElement("ul");
   DOM_ul.className = this.classNav;
   for (i=0; i < this.tabs.length; i++)
   {
     t = this.tabs[i];
     t.headingText = t.div.title;
     if (this.removeTitle)
     {
       t.div.title = '';
     }
     if (!t.headingText)
     {
       for (i2=0; i2<this.titleElements.length; i2++)
       {
         headingElement = t.div.getElementsByTagName(this.titleElements[i2])[0];
         if (headingElement)
         {
           t.headingText = headingElement.innerHTML;
           if (this.titleElementsStripHTML)
           {
             t.headingText.replace(/<br>/gi," ");
             t.headingText = t.headingText.replace(/<[^>]+>/g,"");
           }
           break;
         }
       }
     }
     if (!t.headingText)
     {
       t.headingText = i + 1;
     }
     DOM_li = document.createElement("li");
     t.li = DOM_li;
     DOM_a = document.createElement("a");
     DOM_a.appendChild(document.createTextNode(t.headingText));
     DOM_a.href = "javascript:void(null);";
     DOM_a.title = t.headingText;
     DOM_a.onclick = this.navClick;
     DOM_a.tabber = this;
     DOM_a.tabberIndex = i;
     if (this.addLinkId && this.linkIdFormat)
     {
       aId = this.linkIdFormat;
       aId = aId.replace(/<tabberid>/gi, this.id);
       aId = aId.replace(/<tabnumberzero>/gi, i);
       aId = aId.replace(/<tabnumberone>/gi, i+1);
       aId = aId.replace(/<tabtitle>/gi, t.headingText.replace(/[^a-zA-Z0-9\-]/gi, ''));
       DOM_a.id = aId;
     }
     DOM_li.appendChild(DOM_a);
     DOM_ul.appendChild(DOM_li);
   }
   e.insertBefore(DOM_ul, e.firstChild);
   e.className = e.className.replace(this.REclassMain, this.classMainLive);
   this.tabShow(defaultTab);
   if (typeof this.onLoad == 'function')
   {
     this.onLoad(
     {
       tabber:this
     }
     );
   }
   return this;
 };
 tabberObj.prototype.navClick = function(event)
 {
   var rVal, a, self, tabberIndex, onClickArgs;
   a = this;
   if (!a.tabber)
   {
     return false;
   }
   self = a.tabber;
   tabberIndex = a.tabberIndex;
   a.blur();
   if (typeof self.onClick == 'function')
   {
     onClickArgs =
     {
       'tabber':self, 'index':tabberIndex, 'event':event
     };
     if (!event)
     {
       onClickArgs.event = window.event;
     }
     rVal = self.onClick(onClickArgs);
     if (rVal === false)
     {
       return false;
     }
   }
   self.tabShow(tabberIndex);
   return false;
 };
 tabberObj.prototype.tabHideAll = function()
 {
   var i;
   for (i = 0; i < this.tabs.length; i++)
   {
     this.tabHide(i);
   }
 };
 tabberObj.prototype.tabHide = function(tabberIndex)
 {
   var div;
   if (!this.tabs[tabberIndex])
   {
     return false;
   }
   div = this.tabs[tabberIndex].div;
   if (!div.className.match(this.REclassTabHide))
   {
     div.className += ' ' + this.classTabHide;
   }
   this.navClearActive(tabberIndex);
   return this;
 };
 tabberObj.prototype.tabShow = function(tabberIndex)
 {
   var div;
   if (!this.tabs[tabberIndex])
   {
     return false;
   }
   this.tabHideAll();
   div = this.tabs[tabberIndex].div;
   div.className = div.className.replace(this.REclassTabHide, '');
   this.navSetActive(tabberIndex);
   if (typeof this.onTabDisplay == 'function')
   {
     this.onTabDisplay(
     {
       'tabber':this, 'index':tabberIndex
     }
     );
   }
   return this;
 };
 tabberObj.prototype.navSetActive = function(tabberIndex)
 {
   this.tabs[tabberIndex].li.className = this.classNavActive;
   return this;
 };
 tabberObj.prototype.navClearActive = function(tabberIndex)
 {
   this.tabs[tabberIndex].li.className = '';
   return this;
 };
 function tabberAutomatic(tabberArgs)
 {
   var tempObj, divs, i;
   if (!tabberArgs)
   {
     tabberArgs =
     {
     };
   }
   tempObj = new tabberObj(tabberArgs);
   divs = document.getElementsByTagName("div");
   for (i=0; i < divs.length; i++)
   {
     if (divs[i].className && divs[i].className.match(tempObj.REclassMain))
     {
       tabberArgs.div = divs[i];
       divs[i].tabber = new tabberObj(tabberArgs);
     }
   }
   return this;
 }
 function tabberAutomaticOnLoad(tabberArgs)
 {
   var oldOnLoad;
   if (!tabberArgs)
   {
     tabberArgs =
     {
     };
   }
   oldOnLoad = window.onload;
   if (typeof window.onload != 'function')
   {
     window.onload = function()
     {
       tabberAutomatic(tabberArgs);
     };
   }
   else
   {
     window.onload = function()
     {
       oldOnLoad();
       tabberAutomatic(tabberArgs);
     };
   }
 }
 if (typeof tabberOptions == 'undefined')
 {
   tabberAutomaticOnLoad();
 }
 else
 {
   if (!tabberOptions['manualStartup'])
   {
     tabberAutomaticOnLoad(tabberOptions);
   }
 }
 