nsp = 'Old browser!';
dl = document.layers;
oe = window.opera ? 1 : 0;
da = document.all &&! oe;
ge = document.getElementById;
ws = window.sidebar ? true : false;
tN = navigator.userAgent.toLowerCase();
izN = tN.indexOf('netscape') >= 0 ? true : false;
zis = tN.indexOf('msie 7') >= 0 ? true : false;
zis8 = tN.indexOf('msie 8') >= 0 ? true : false;
zis |= zis8;
if (ws &&! izN){
  quogl = 'iuy'
}
;
var msg = '';
function nem(){
  return true
}
;
window.onerror = nem;
zOF = window.location.protocol.indexOf("file") !=- 1 ? true : false;
i7f = zis &&! zOF ? true : false;
var O = document.getElementById;
function onLoad(){
  initSettings();
  System.Gadget.onSettingsClosing = SettingsClosing;
}
function initSettings(){
  loadSettings();
  soundSettings();
  sizeSettings();
  graphSettings();
  colorSettings();
  showColor();
}
function SettingsClosing(event){
  if (event.closeAction == event.Action.commit){
    System.Gadget.Settings.write("drawstyle", drawstyle.value);
    System.Gadget.Settings.write("graph", graph.value);
    System.Gadget.Settings.write("fixsize", fixsize.value);
    System.Gadget.Settings.write("ssize", ssize.value);
    if (fixsize.value == "Custom"){
      System.Gadget.Settings.write("size", ssize.value / 100);
    }
    else {
      System.Gadget.Settings.write("size", fixsize.value / 100)
    }
    System.Gadget.Settings.write("settimer", settimer.value);
    System.Gadget.Settings.write("update", update.value);
    System.Gadget.Settings.write("showIcon", showIcon.value);
    System.Gadget.Settings.write("doubleClick", doubleClick.value);
    System.Gadget.Settings.write("cpuName", cpuName.value);
    System.Gadget.Settings.write("clockFre", clockFre.value);
    System.Gadget.Settings.write("showUsername", showUsername.value);
    System.Gadget.Settings.write("showMem", showMem.value);
    System.Gadget.Settings.write("showMemBar", showMemBar.value);
    System.Gadget.Settings.write("showMemGraph", showMemGraph.value);
    System.Gadget.Settings.write("showPageFile", showPageFile.value);
    System.Gadget.Settings.write("showPageFileBar", showPageFileBar.value);
    System.Gadget.Settings.write("showPageFileGraph", showPageFileGraph.value);
    System.Gadget.Settings.write("showTem", showTem.value);
    System.Gadget.Settings.write("temperature", temperature.value);
    System.Gadget.Settings.write("showErr", showErr.value);
    System.Gadget.Settings.write("soundCPUTem", soundCPUTem.value);
    System.Gadget.Settings.write("soundCPUTemurl", soundCPUTemurl.innerText);
    System.Gadget.Settings.write("alertCPU1", alertCPU1.value);
    System.Gadget.Settings.write("alertCPUTem", alertCPUTem.value);
    System.Gadget.Settings.write("soundCPUTemVol", soundCPUTemVol.value);
    System.Gadget.Settings.write("soundCPUTemRepeats", soundCPUTemRepeats.value);
    System.Gadget.Settings.write("soundCPUTemCount", soundCPUTemCount.value);
    System.Gadget.Settings.write("backg", "#" + backg.value);
    System.Gadget.Settings.write("fixBackg", fixBackg.value);
    System.Gadget.Settings.write("title", "#" + title.value);
    System.Gadget.Settings.write("fixTitle", fixTitle.value);
    System.Gadget.Settings.write("clocks", "#" + clocks.value);
    System.Gadget.Settings.write("fixClock", fixClock.value);
    System.Gadget.Settings.write("UFT", "#" + UFT.value);
    System.Gadget.Settings.write("fixUFT", fixUFT.value);
    System.Gadget.Settings.write("rams", "#" + rams.value);
    System.Gadget.Settings.write("fixRam", fixRam.value);
    System.Gadget.Settings.write("pagec", "#" + pagec.value);
    System.Gadget.Settings.write("fixPagec", fixPagec.value);
    System.Gadget.Settings.write("processor", "#" + processor.value);
    System.Gadget.Settings.write("fixProcessor", fixProcessor.value);
    System.Gadget.Settings.write("DomUse", "#" + DomUse.value);
    System.Gadget.Settings.write("fixDomUse", fixDomUse.value);
    System.Gadget.Settings.write("colorc1", "#" + colorc1.value);
    System.Gadget.Settings.write("fixcolor1", fixcolor1.value);
    System.Gadget.Settings.write("colorc2", "#" + colorc2.value);
    System.Gadget.Settings.write("fixcolor2", fixcolor2.value);
    System.Gadget.Settings.write("colorc3", "#" + colorc3.value);
    System.Gadget.Settings.write("fixcolor3", fixcolor3.value);
    System.Gadget.Settings.write("colorc4", "#" + colorc4.value);
    System.Gadget.Settings.write("fixcolor4", fixcolor4.value);
    System.Gadget.Settings.write("colorc5", "#" + colorc5.value);
    System.Gadget.Settings.write("fixcolor5", fixcolor5.value);
    System.Gadget.Settings.write("colorc6", "#" + colorc6.value);
    System.Gadget.Settings.write("fixcolor6", fixcolor6.value);
    System.Gadget.Settings.write("colorc7", "#" + colorc7.value);
    System.Gadget.Settings.write("fixcolor7", fixcolor7.value);
    System.Gadget.Settings.write("colorc8", "#" + colorc8.value);
    System.Gadget.Settings.write("fixcolor8", fixcolor8.value);
    System.Gadget.Settings.write("colorc9", "#" + colorc9.value);
    System.Gadget.Settings.write("fixcolor9", fixcolor9.value);
    System.Gadget.Settings.write("colorc10", "#" + colorc10.value);
    System.Gadget.Settings.write("fixcolor10", fixcolor10.value);
    System.Gadget.Settings.write("colorc11", "#" + colorc11.value);
    System.Gadget.Settings.write("fixcolor11", fixcolor11.value);
    System.Gadget.Settings.write("colorc12", "#" + colorc12.value);
    System.Gadget.Settings.write("fixcolor12", fixcolor12.value);
    System.Gadget.Settings.write("colorc13", "#" + colorc13.value);
    System.Gadget.Settings.write("fixcolor13", fixcolor13.value);
    System.Gadget.Settings.write("colorc14", "#" + colorc14.value);
    System.Gadget.Settings.write("fixcolor14", fixcolor14.value);
    System.Gadget.Settings.write("colorc15", "#" + colorc15.value);
    System.Gadget.Settings.write("fixcolor15", fixcolor15.value);
    System.Gadget.Settings.write("colorc16", "#" + colorc16.value);
    System.Gadget.Settings.write("fixcolor16", fixcolor16.value);
    System.Gadget.Settings.write("colorc17", "#" + colorc17.value);
    System.Gadget.Settings.write("fixcolor17", fixcolor17.value);
    System.Gadget.Settings.write("colorc18", "#" + colorc18.value);
    System.Gadget.Settings.write("fixcolor18", fixcolor18.value);
    System.Gadget.Settings.write("colorc19", "#" + colorc19.value);
    System.Gadget.Settings.write("fixcolor19", fixcolor19.value);
    System.Gadget.Settings.write("colorc20", "#" + colorc20.value);
    System.Gadget.Settings.write("fixcolor20", fixcolor20.value);
    System.Gadget.Settings.write("colorc21", "#" + colorc21.value);
    System.Gadget.Settings.write("fixcolor21", fixcolor21.value);
    System.Gadget.Settings.write("colorc22", "#" + colorc22.value);
    System.Gadget.Settings.write("fixcolor22", fixcolor22.value);
    System.Gadget.Settings.write("colorc23", "#" + colorc23.value);
    System.Gadget.Settings.write("fixcolor23", fixcolor23.value);
    System.Gadget.Settings.write("colorc24", "#" + colorc24.value);
    System.Gadget.Settings.write("fixcolor24", fixcolor24.value);
    System.Gadget.Settings.write("colorc25", "#" + colorc25.value);
    System.Gadget.Settings.write("fixcolor25", fixcolor25.value);
    System.Gadget.Settings.write("colorc26", "#" + colorc26.value);
    System.Gadget.Settings.write("fixcolor26", fixcolor26.value);
    System.Gadget.Settings.write("colorc27", "#" + colorc27.value);
    System.Gadget.Settings.write("fixcolor27", fixcolor27.value);
    System.Gadget.Settings.write("colorc28", "#" + colorc28.value);
    System.Gadget.Settings.write("fixcolor28", fixcolor28.value);
    System.Gadget.Settings.write("colorc29", "#" + colorc29.value);
    System.Gadget.Settings.write("fixcolor29", fixcolor29.value);
    System.Gadget.Settings.write("colorc30", "#" + colorc30.value);
    System.Gadget.Settings.write("fixcolor30", fixcolor30.value);
    System.Gadget.Settings.write("colorc31", "#" + colorc31.value);
    System.Gadget.Settings.write("fixcolor31", fixcolor31.value);
    System.Gadget.Settings.write("colorc32", "#" + colorc32.value);
    System.Gadget.Settings.write("fixcolor32", fixcolor32.value);
    System.Gadget.Settings.write("alertIcon", "#" + alertIcon.value);
    System.Gadget.Settings.write("fixAlertIcon", fixAlertIcon.value);
    System.Gadget.Settings.write("FlyoutBac", "#" + FlyoutBac.value);
    System.Gadget.Settings.write("fixFlyoutBac", fixFlyoutBac.value);
    System.Gadget.Settings.write("FlyoutTit", "#" + FlyoutTit.value);
    System.Gadget.Settings.write("fixFlyoutTit", fixFlyoutTit.value);
    System.Gadget.Settings.write("FlyoutDet", "#" + FlyoutDet.value);
    System.Gadget.Settings.write("fixFlyoutDet", fixFlyoutDet.value);
    savefilesettings();
  }
  event.cancel = false;
}
function loadSettings(){
  gshowIcon = System.Gadget.Settings.read("showIcon");
  if (gshowIcon != '')showIcon.value = gshowIcon;
  else showIcon.value = '1';
  gupdate = System.Gadget.Settings.read("update");
  if (gupdate != '')update.value = gupdate;
  else update.value = '1';
  gfixsize = System.Gadget.Settings.read("fixsize");
  if (gfixsize != '')fixsize.value = gfixsize;
  else fixsize.value = '100';
  gssize = System.Gadget.Settings.read("ssize");
  if (gssize != '')ssize.value = gssize;
  else ssize.value = '100';
  gsettimer = System.Gadget.Settings.read("settimer");
  if (gsettimer != '')settimer.value = gsettimer;
  else settimer.value = '1';
  gdrawstyle = System.Gadget.Settings.read("drawstyle");
  if (gdrawstyle != '')drawstyle.value = gdrawstyle;
  else drawstyle.value = '1';
  ggraph = System.Gadget.Settings.read("graph");
  if (ggraph != '')graph.value = ggraph;
  else graph.value = '1';
  gdoubleClick = System.Gadget.Settings.read("doubleClick");
  if (gdoubleClick != '')doubleClick.value = gdoubleClick;
  else doubleClick.value = '1';
  gcpuName = System.Gadget.Settings.read("cpuName");
  if (gcpuName != '')cpuName.value = gcpuName;
  else cpuName.value = '1';
  gclockFre = System.Gadget.Settings.read("clockFre");
  if (gclockFre != '')clockFre.value = gclockFre;
  else clockFre.value = '1';
  gshowUsername = System.Gadget.Settings.read("showUsername");
  if (gshowUsername != '')showUsername.value = gshowUsername;
  else showUsername.value = '1';
  gshowMem = System.Gadget.Settings.read("showMem");
  if (gshowMem != '')showMem.value = gshowMem;
  else showMem.value = '1';
  gshowMemBar = System.Gadget.Settings.read("showMemBar");
  if (gshowMemBar != '')showMemBar.value = gshowMemBar;
  else showMemBar.value = '1';
  gshowMemGraph = System.Gadget.Settings.read("showMemGraph");
  if (gshowMemGraph != '')showMemGraph.value = gshowMemGraph;
  else showMemGraph.value = '1';
  gshowPageFile = System.Gadget.Settings.read("showPageFile");
  if (gshowPageFile != '')showPageFile.value = gshowPageFile;
  else showPageFile.value = '1';
  gshowPageFileBar = System.Gadget.Settings.read("showPageFileBar");
  if (gshowPageFileBar != '')showPageFileBar.value = gshowPageFileBar;
  else showPageFileBar.value = '1';
  gshowPageFileGraph = System.Gadget.Settings.read("showPageFileGraph");
  if (gshowPageFileGraph != '')showPageFileGraph.value = gshowPageFileGraph;
  else showPageFileGraph.value = '1';
  gshowTem = System.Gadget.Settings.read("showTem");
  if (gshowTem != '')showTem.value = gshowTem;
  else showTem.value = '2';
  gtemperature = System.Gadget.Settings.read("temperature");
  if (gtemperature != '')temperature.value = gtemperature;
  else temperature.value = '1';
  gshowErr = System.Gadget.Settings.read("showErr");
  if (gshowErr != '')showErr.value = gshowErr;
  else showErr.value = '1';
  gsoundCPUTem = System.Gadget.Settings.read("soundCPUTem");
  if (gsoundCPUTem != '')soundCPUTem.value = gsoundCPUTem;
  else soundCPUTem.value = '1';
  gsoundCPUTemurl = System.Gadget.Settings.read("soundCPUTemurl");
  if (gsoundCPUTemurl != '')soundCPUTemurl.innerText = gsoundCPUTemurl;
  else soundCPUTemurl.innerText = '';
  galertCPU1 = System.Gadget.Settings.read("alertCPU1");
  if (galertCPU1 != '')alertCPU1.value = galertCPU1;
  else alertCPU1.value = '2';
  galertCPUTem = System.Gadget.Settings.read("alertCPUTem");
  if (galertCPUTem != '')alertCPUTem.value = galertCPUTem;
  else alertCPUTem.value = '80';
  gsoundCPUTemVol = System.Gadget.Settings.read("soundCPUTemVol");
  if (gsoundCPUTemVol != '')soundCPUTemVol.value = gsoundCPUTemVol;
  else soundCPUTemVol.value = '100';
  gsoundCPUTemRepeats = System.Gadget.Settings.read("soundCPUTemRepeats");
  if (gsoundCPUTemRepeats != '')soundCPUTemRepeats.value = gsoundCPUTemRepeats;
  else soundCPUTemRepeats.value = '3';
  gsoundCPUTemCount = System.Gadget.Settings.read("soundCPUTemCount");
  if (gsoundCPUTemCount != '')soundCPUTemCount.value = gsoundCPUTemCount;
  else soundCPUTemCount.value = '1';
  var gbackg = System.Gadget.Settings.read("backg");
  if (gbackg != '')backg.value = gbackg;
  else backg.value = '080808';
  var gfixBackg = System.Gadget.Settings.read("fixBackg");
  if (gfixBackg != '')fixBackg.value = gfixBackg;
  else fixBackg.value = '';
  gtitle = System.Gadget.Settings.read("title");
  if (gtitle != '')title.value = gtitle;
  else title.value = 'ffffff';
  gfixTitle = System.Gadget.Settings.read("fixTitle");
  if (gfixTitle != '')fixTitle.value = gfixTitle;
  else fixTitle.value = '';
  gclocks = System.Gadget.Settings.read("clocks");
  if (gclocks != '')clocks.value = gclocks;
  else clocks.value = '90ee90';
  gfixClock = System.Gadget.Settings.read("fixClock");
  if (gfixClock != '')fixClock.value = gfixClock;
  else fixClock.value = '';
  gprocessor = System.Gadget.Settings.read("processor");
  if (gprocessor != '')processor.value = gprocessor;
  else processor.value = 'fff62a';
  gfixProcessor = System.Gadget.Settings.read("fixProcessor");
  if (gfixProcessor != '')fixProcessor.value = gfixProcessor;
  else fixProcessor.value = '';
  gDomUse = System.Gadget.Settings.read("DomUse");
  if (gDomUse != '')DomUse.value = gDomUse;
  else DomUse.value = 'fff62a';
  gfixDomUse = System.Gadget.Settings.read("fixDomUse");
  if (gfixDomUse != '')fixDomUse.value = gfixDomUse;
  else fixDomUse.value = '';
  gUFT = System.Gadget.Settings.read("UFT");
  if (gUFT != '')UFT.value = gUFT;
  else UFT.value = 'ffffff';
  gfixUFT = System.Gadget.Settings.read("fixUFT");
  if (gfixUFT != '')fixUFT.value = gfixUFT;
  else fixUFT.value = '';
  grams = System.Gadget.Settings.read("rams");
  if (grams != '')rams.value = grams;
  else rams.value = '87cefa';
  gfixRam = System.Gadget.Settings.read("fixRam");
  if (gfixRam != '')fixRam.value = gfixRam;
  else fixRam.value = '';
  gpagec = System.Gadget.Settings.read("pagec");
  if (gpagec != '')pagec.value = gpagec;
  else pagec.value = 'ffcc00';
  gfixPagec = System.Gadget.Settings.read("fixPagec");
  if (gfixPagec != '')fixPagec.value = gfixPagec;
  else fixPagec.value = '';
  gcolorc1 = System.Gadget.Settings.read("colorc1");
  if (gcolorc1 != '')colorc1.value = gcolorc1;
  else colorc1.value = '90ee90';
  gfixcolor1 = System.Gadget.Settings.read("fixcolor1");
  if (gfixcolor1 != '')fixcolor1.value = gfixcolor1;
  else fixcolor1.value = '';
  gcolorc2 = System.Gadget.Settings.read("colorc2");
  if (gcolorc2 != '')colorc2.value = gcolorc2;
  else colorc2.value = 'fff62a';
  gfixcolor2 = System.Gadget.Settings.read("fixcolor2");
  if (gfixcolor2 != '')fixcolor2.value = gfixcolor2;
  else fixcolor2.value = '';
  gcolorc3 = System.Gadget.Settings.read("colorc3");
  if (gcolorc3 != '')colorc3.value = gcolorc3;
  else colorc3.value = 'faba00';
  gfixcolor3 = System.Gadget.Settings.read("fixcolor3");
  if (gfixcolor3 != '')fixcolor3.value = gfixcolor3;
  else fixcolor3.value = '';
  gcolorc4 = System.Gadget.Settings.read("colorc4");
  if (gcolorc4 != '')colorc4.value = gcolorc4;
  else colorc4.value = 'ec7527';
  gfixcolor4 = System.Gadget.Settings.read("fixcolor4");
  if (gfixcolor4 != '')fixcolor4.value = gfixcolor4;
  else fixcolor4.value = '';
  gcolorc5 = System.Gadget.Settings.read("colorc5");
  if (gcolorc5 != '')colorc5.value = gcolorc5;
  else colorc5.value = 'e5316c';
  gfixcolor5 = System.Gadget.Settings.read("fixcolor5");
  if (gfixcolor5 != '')fixcolor5.value = gfixcolor5;
  else fixcolor5.value = '';
  gcolorc6 = System.Gadget.Settings.read("colorc6");
  if (gcolorc6 != '')colorc6.value = gcolorc6;
  else colorc6.value = 'e2003b';
  gfixcolor6 = System.Gadget.Settings.read("fixcolor6");
  if (gfixcolor6 != '')fixcolor6.value = gfixcolor6;
  else fixcolor6.value = '';
  gcolorc7 = System.Gadget.Settings.read("colorc7");
  if (gcolorc7 != '')colorc7.value = gcolorc7;
  else colorc7.value = 'd7007a';
  gfixcolor7 = System.Gadget.Settings.read("fixcolor7");
  if (gfixcolor7 != '')fixcolor7.value = gfixcolor7;
  else fixcolor7.value = '';
  gcolorc8 = System.Gadget.Settings.read("colorc8");
  if (gcolorc8 != '')colorc8.value = gcolorc8;
  else colorc8.value = 'ae3288';
  gfixcolor8 = System.Gadget.Settings.read("fixcolor8");
  if (gfixcolor8 != '')fixcolor8.value = gfixcolor8;
  else fixcolor8.value = '';
  gcolorc9 = System.Gadget.Settings.read("colorc9");
  if (gcolorc9 != '')colorc9.value = gcolorc9;
  else colorc9.value = '894b94';
  gfixcolor9 = System.Gadget.Settings.read("fixcolor9");
  if (gfixcolor9 != '')fixcolor9.value = gfixcolor9;
  else fixcolor9.value = '';
  gcolorc10 = System.Gadget.Settings.read("colorc10");
  if (gcolorc10 != '')colorc10.value = gcolorc10;
  else colorc10.value = '5b5099';
  gfixcolor10 = System.Gadget.Settings.read("fixcolor10");
  if (gfixcolor10 != '')fixcolor10.value = gfixcolor10;
  else fixcolor10.value = '';
  gcolorc11 = System.Gadget.Settings.read("colorc11");
  if (gcolorc11 != '')colorc11.value = gcolorc11;
  else colorc11.value = '4d61a8';
  gfixcolor11 = System.Gadget.Settings.read("fixcolor11");
  if (gfixcolor11 != '')fixcolor11.value = gfixcolor11;
  else fixcolor11.value = '';
  gcolorc12 = System.Gadget.Settings.read("colorc12");
  if (gcolorc12 != '')colorc12.value = gcolorc12;
  else colorc12.value = '4d81a2';
  gfixcolor12 = System.Gadget.Settings.read("fixcolor12");
  if (gfixcolor12 != '')fixcolor12.value = gfixcolor12;
  else fixcolor12.value = '';
  gcolorc13 = System.Gadget.Settings.read("colorc13");
  if (gcolorc13 != '')colorc13.value = gcolorc13;
  else colorc13.value = '4da0ae';
  gfixcolor13 = System.Gadget.Settings.read("fixcolor13");
  if (gfixcolor13 != '')fixcolor13.value = gfixcolor13;
  else fixcolor13.value = '';
  gcolorc14 = System.Gadget.Settings.read("colorc14");
  if (gcolorc14 != '')colorc14.value = gcolorc14;
  else colorc14.value = '4db896';
  gfixcolor14 = System.Gadget.Settings.read("fixcolor14");
  if (gfixcolor14 != '')fixcolor14.value = gfixcolor14;
  else fixcolor14.value = '';
  gcolorc15 = System.Gadget.Settings.read("colorc15");
  if (gcolorc15 != '')colorc15.value = gcolorc15;
  else colorc15.value = '64c566';
  gfixcolor15 = System.Gadget.Settings.read("fixcolor15");
  if (gfixcolor15 != '')fixcolor15.value = gfixcolor15;
  else fixcolor15.value = '';
  gcolorc16 = System.Gadget.Settings.read("colorc16");
  if (gcolorc16 != '')colorc16.value = gcolorc16;
  else colorc16.value = '79b63e';
  gfixcolor16 = System.Gadget.Settings.read("fixcolor16");
  if (gfixcolor16 != '')fixcolor16.value = gfixcolor16;
  else fixcolor16.value = '';
  gcolorc17 = System.Gadget.Settings.read("colorc17");
  if (gcolorc17 != '')colorc17.value = gcolorc17;
  else colorc17.value = '90ee90';
  gfixcolor17 = System.Gadget.Settings.read("fixcolor17");
  if (gfixcolor17 != '')fixcolor17.value = gfixcolor17;
  else fixcolor17.value = '';
  gcolorc18 = System.Gadget.Settings.read("colorc18");
  if (gcolorc18 != '')colorc18.value = gcolorc18;
  else colorc18.value = 'fff62a';
  gfixcolor18 = System.Gadget.Settings.read("fixcolor18");
  if (gfixcolor18 != '')fixcolor18.value = gfixcolor18;
  else fixcolor18.value = '';
  gcolorc19 = System.Gadget.Settings.read("colorc19");
  if (gcolorc19 != '')colorc19.value = gcolorc19;
  else colorc19.value = 'faba00';
  gfixcolor19 = System.Gadget.Settings.read("fixcolor19");
  if (gfixcolor19 != '')fixcolor19.value = gfixcolor19;
  else fixcolor19.value = '';
  gcolorc20 = System.Gadget.Settings.read("colorc20");
  if (gcolorc20 != '')colorc20.value = gcolorc20;
  else colorc20.value = 'ec7527';
  gfixcolor20 = System.Gadget.Settings.read("fixcolor20");
  if (gfixcolor20 != '')fixcolor20.value = gfixcolor20;
  else fixcolor20.value = '';
  gcolorc21 = System.Gadget.Settings.read("colorc21");
  if (gcolorc21 != '')colorc21.value = gcolorc21;
  else colorc21.value = 'e5316c';
  gfixcolor21 = System.Gadget.Settings.read("fixcolor21");
  if (gfixcolor21 != '')fixcolor21.value = gfixcolor21;
  else fixcolor21.value = '';
  gcolorc22 = System.Gadget.Settings.read("colorc22");
  if (gcolorc22 != '')colorc22.value = gcolorc22;
  else colorc22.value = 'e2003b';
  gfixcolor22 = System.Gadget.Settings.read("fixcolor22");
  if (gfixcolor22 != '')fixcolor22.value = gfixcolor22;
  else fixcolor22.value = '';
  gcolorc23 = System.Gadget.Settings.read("colorc23");
  if (gcolorc23 != '')colorc23.value = gcolorc23;
  else colorc23.value = 'd7007a';
  gfixcolor23 = System.Gadget.Settings.read("fixcolor23");
  if (gfixcolor23 != '')fixcolor23.value = gfixcolor23;
  else fixcolor23.value = '';
  gcolorc24 = System.Gadget.Settings.read("colorc24");
  if (gcolorc24 != '')colorc24.value = gcolorc24;
  else colorc24.value = 'ae3288';
  gfixcolor24 = System.Gadget.Settings.read("fixcolor24");
  if (gfixcolor24 != '')fixcolor24.value = gfixcolor24;
  else fixcolor24.value = '';
  gcolorc25 = System.Gadget.Settings.read("colorc25");
  if (gcolorc25 != '')colorc25.value = gcolorc25;
  else colorc25.value = '894b94';
  gfixcolor25 = System.Gadget.Settings.read("fixcolor25");
  if (gfixcolor25 != '')fixcolor25.value = gfixcolor25;
  else fixcolor25.value = '';
  gcolorc26 = System.Gadget.Settings.read("colorc26");
  if (gcolorc26 != '')colorc26.value = gcolorc26;
  else colorc26.value = '5b5099';
  gfixcolor26 = System.Gadget.Settings.read("fixcolor26");
  if (gfixcolor26 != '')fixcolor26.value = gfixcolor26;
  else fixcolor26.value = '';
  gcolorc27 = System.Gadget.Settings.read("colorc27");
  if (gcolorc27 != '')colorc27.value = gcolorc27;
  else colorc27.value = '4d61a8';
  gfixcolor27 = System.Gadget.Settings.read("fixcolor27");
  if (gfixcolor27 != '')fixcolor27.value = gfixcolor27;
  else fixcolor27.value = '';
  gcolorc28 = System.Gadget.Settings.read("colorc28");
  if (gcolorc28 != '')colorc28.value = gcolorc28;
  else colorc28.value = '4d81a2';
  gfixcolor28 = System.Gadget.Settings.read("fixcolor28");
  if (gfixcolor28 != '')fixcolor28.value = gfixcolor28;
  else fixcolor28.value = '';
  gcolorc29 = System.Gadget.Settings.read("colorc29");
  if (gcolorc29 != '')colorc29.value = gcolorc29;
  else colorc29.value = '4da0ae';
  gfixcolor29 = System.Gadget.Settings.read("fixcolor29");
  if (gfixcolor29 != '')fixcolor29.value = gfixcolor29;
  else fixcolor29.value = '';
  gcolorc30 = System.Gadget.Settings.read("colorc30");
  if (gcolorc30 != '')colorc30.value = gcolorc30;
  else colorc30.value = '4db896';
  gfixcolor30 = System.Gadget.Settings.read("fixcolor30");
  if (gfixcolor30 != '')fixcolor30.value = gfixcolor30;
  else fixcolor30.value = '';
  gcolorc31 = System.Gadget.Settings.read("colorc31");
  if (gcolorc31 != '')colorc31.value = gcolorc31;
  else colorc31.value = '64c566';
  gfixcolor31 = System.Gadget.Settings.read("fixcolor31");
  if (gfixcolor31 != '')fixcolor31.value = gfixcolor31;
  else fixcolor31.value = '';
  gcolorc32 = System.Gadget.Settings.read("colorc32");
  if (gcolorc32 != '')colorc32.value = gcolorc32;
  else colorc32.value = '79b63e';
  gfixcolor32 = System.Gadget.Settings.read("fixcolor32");
  if (gfixcolor32 != '')fixcolor32.value = gfixcolor32;
  else fixcolor32.value = '';
  galertIcon = System.Gadget.Settings.read("alertIcon");
  if (galertIcon != '')alertIcon.value = galertIcon;
  else alertIcon.value = '90EE90';
  gfixAlertIcon = System.Gadget.Settings.read("fixAlertIcon");
  if (gfixAlertIcon != '')fixAlertIcon.value = gfixAlertIcon;
  else fixAlertIcon.value = '';
  gFlyoutBac = System.Gadget.Settings.read("FlyoutBac");
  if (gFlyoutBac != '')FlyoutBac.value = gFlyoutBac;
  else FlyoutBac.value = '080808';
  gfixFlyoutBac = System.Gadget.Settings.read("fixFlyoutBac");
  if (gfixFlyoutBac != '')fixFlyoutBac.value = gfixFlyoutBac;
  else fixFlyoutBac.value = '';
  gFlyoutTit = System.Gadget.Settings.read("FlyoutTit");
  if (gFlyoutTit != '')FlyoutTit.value = gFlyoutTit;
  else FlyoutTit.value = '87cefa';
  gfixFlyoutTit = System.Gadget.Settings.read("fixFlyoutTit");
  if (gfixFlyoutTit != '')fixFlyoutTit.value = gfixFlyoutTit;
  else fixFlyoutTit.value = '';
  gFlyoutDet = System.Gadget.Settings.read("FlyoutDet");
  if (gFlyoutDet != '')FlyoutDet.value = gFlyoutDet;
  else FlyoutDet.value = 'ffcc00';
  gfixFlyoutDet = System.Gadget.Settings.read("fixFlyoutDet");
  if (gfixFlyoutDet != '')fixFlyoutDet.value = gfixFlyoutDet;
  else fixFlyoutDet.value = '';
}
function savefilesettings(){
  var fs = new ActiveXObject("Scripting.FileSystemObject");
  var inifilename = System.Environment.getEnvironmentVariable("APPDATA") + "\\" + System.
  Gadget.name + "V3_Settings.ini";
  try {
    var inifile = fs.OpenTextFile(inifilename, 2, true);
    try {
      inifile.WriteLine(";All CPU Meter (c) 2007-2012 AddGadgets.com");
      inifile.WriteLine(";v4");
      inifile.WriteLine(drawstyle.value);
      inifile.WriteLine(graph.value);
      inifile.WriteLine(fixsize.value);
      inifile.WriteLine(ssize.value);
      inifile.WriteLine(settimer.value);
      inifile.WriteLine(update.value);
      inifile.WriteLine(showIcon.value);
      inifile.WriteLine(doubleClick.value);
      inifile.WriteLine(cpuName.value);
      inifile.WriteLine(clockFre.value);
      inifile.WriteLine(showMem.value);
      inifile.WriteLine(showMemBar.value);
      inifile.WriteLine(showTem.value);
      inifile.WriteLine(temperature.value);
      inifile.WriteLine(showErr.value);
      inifile.WriteLine(soundCPUTem.value);
      inifile.WriteLine(soundCPUTemurl.innerText);
      inifile.WriteLine(alertCPU1.value);
      inifile.WriteLine(alertCPUTem.value);
      inifile.WriteLine(soundCPUTemVol.value);
      inifile.WriteLine(soundCPUTemRepeats.value);
      inifile.WriteLine(soundCPUTemCount.value);
      inifile.WriteLine("#" + backg.value);
      inifile.WriteLine(fixBackg.value);
      inifile.WriteLine("#" + title.value);
      inifile.WriteLine(fixTitle.value);
      inifile.WriteLine("#" + clocks.value);
      inifile.WriteLine(fixClock.value);
      inifile.WriteLine("#" + rams.value);
      inifile.WriteLine(fixRam.value);
      inifile.WriteLine("#" + processor.value);
      inifile.WriteLine(fixProcessor.value);
      inifile.WriteLine("#" + colorc1.value);
      inifile.WriteLine(fixcolor1.value);
      inifile.WriteLine("#" + colorc2.value);
      inifile.WriteLine(fixcolor2.value);
      inifile.WriteLine("#" + colorc3.value);
      inifile.WriteLine(fixcolor3.value);
      inifile.WriteLine("#" + colorc4.value);
      inifile.WriteLine(fixcolor4.value);
      inifile.WriteLine("#" + colorc5.value);
      inifile.WriteLine(fixcolor5.value);
      inifile.WriteLine("#" + colorc6.value);
      inifile.WriteLine(fixcolor6.value);
      inifile.WriteLine("#" + colorc7.value);
      inifile.WriteLine(fixcolor7.value);
      inifile.WriteLine("#" + colorc8.value);
      inifile.WriteLine(fixcolor8.value);
      inifile.WriteLine("#" + colorc9.value);
      inifile.WriteLine(fixcolor9.value);
      inifile.WriteLine("#" + colorc10.value);
      inifile.WriteLine(fixcolor10.value);
      inifile.WriteLine("#" + colorc11.value);
      inifile.WriteLine(fixcolor11.value);
      inifile.WriteLine("#" + colorc12.value);
      inifile.WriteLine(fixcolor12.value);
      inifile.WriteLine("#" + colorc13.value);
      inifile.WriteLine(fixcolor13.value);
      inifile.WriteLine("#" + colorc14.value);
      inifile.WriteLine(fixcolor14.value);
      inifile.WriteLine("#" + colorc15.value);
      inifile.WriteLine(fixcolor15.value);
      inifile.WriteLine("#" + colorc16.value);
      inifile.WriteLine(fixcolor16.value);
      inifile.WriteLine("#" + colorc17.value);
      inifile.WriteLine(fixcolor17.value);
      inifile.WriteLine("#" + colorc18.value);
      inifile.WriteLine(fixcolor18.value);
      inifile.WriteLine("#" + colorc19.value);
      inifile.WriteLine(fixcolor19.value);
      inifile.WriteLine("#" + colorc20.value);
      inifile.WriteLine(fixcolor20.value);
      inifile.WriteLine("#" + colorc21.value);
      inifile.WriteLine(fixcolor21.value);
      inifile.WriteLine("#" + colorc22.value);
      inifile.WriteLine(fixcolor22.value);
      inifile.WriteLine("#" + colorc23.value);
      inifile.WriteLine(fixcolor23.value);
      inifile.WriteLine("#" + colorc24.value);
      inifile.WriteLine(fixcolor24.value);
      inifile.WriteLine("#" + colorc25.value);
      inifile.WriteLine(fixcolor25.value);
      inifile.WriteLine("#" + colorc26.value);
      inifile.WriteLine(fixcolor26.value);
      inifile.WriteLine("#" + colorc27.value);
      inifile.WriteLine(fixcolor27.value);
      inifile.WriteLine("#" + colorc28.value);
      inifile.WriteLine(fixcolor28.value);
      inifile.WriteLine("#" + colorc29.value);
      inifile.WriteLine(fixcolor29.value);
      inifile.WriteLine("#" + colorc30.value);
      inifile.WriteLine(fixcolor30.value);
      inifile.WriteLine("#" + colorc31.value);
      inifile.WriteLine(fixcolor31.value);
      inifile.WriteLine("#" + colorc32.value);
      inifile.WriteLine(fixcolor32.value);
      if (fixsize.value == "Custom"){
        inifile.WriteLine(ssize.value / 100)
      }
      else {
        inifile.WriteLine(fixsize.value / 100)
      }
      inifile.WriteLine("#" + alertIcon.value);
      inifile.WriteLine(fixAlertIcon.value);
      inifile.WriteLine("#" + FlyoutBac.value);
      inifile.WriteLine(fixFlyoutBac.value);
      inifile.WriteLine("#" + FlyoutTit.value);
      inifile.WriteLine(fixFlyoutTit.value);
      inifile.WriteLine("#" + FlyoutDet.value);
      inifile.WriteLine(fixFlyoutDet.value);
      inifile.WriteLine(showPageFile.value);
      inifile.WriteLine(showPageFileBar.value);
      inifile.WriteLine(showPageFileGraph.value);
      inifile.WriteLine(showMemGraph.value);
      inifile.WriteLine("#" + UFT.value);
      inifile.WriteLine(fixUFT.value);
      inifile.WriteLine("#" + pagec.value);
      inifile.WriteLine(fixPagec.value);
      inifile.WriteLine(showUsername.value);
      inifile.WriteLine("#" + DomUse.value);
      inifile.WriteLine(fixDomUse.value);
    }
    finally{
      inifile.Close();
    }
  }
  catch (err){
  }
}
function temerr(){
  if (showTem.value == 2){
    O('showErrText').style.visibility = "hidden";
    O('showErr').style.visibility = "hidden";
  }
  else {
    O('showErrText').style.visibility = "visible";
    O('showErr').style.visibility = "visible";
  }
}
function chooseSound(){
  var shellitem = System.Shell.chooseFile(true, 
  "Music files:*.asx;*.wpl;*.mp3;*.wav;*.wma::", "", "");
  if (shellitem != null){
    soundCPUTemurl.innerText = shellitem.path;
    Player.settings.volume = soundCPUTemVol.value;
    Player.URL = soundCPUTemurl.innerText;
    Player.Controls.play();
  }
}
function playchoosehour(){
  if (soundCPUTem.value == 100){
  }
  else if (soundCPUTem.value == 50){
    Player.URL = soundCPUTemurl.innerText;
  }
  else {
    Player.URL = System.Gadget.path + "\\alarm" + soundCPUTem.value + ".mp3";
  }
  Player.settings.volume = soundCPUTemVol.value;
  Player.Controls.play();
}
function clickstop(){
  if (Player.controls.isAvailable('Stop')){
    Player.controls.stop();
  }
}
function soundSettings(){
  if (soundCPUTem.value == "50"){
    O('csButton').style.visibility = "visible";
    O('soundCPUTemurl').style.visibility = "visible";
  }
  else {
    O('csButton').style.visibility = "hidden";
    O('soundCPUTemurl').style.visibility = "hidden";
  }
}
function soundCPUTemRepeatsSettings(){
  if (soundCPUTemRepeats.value == "3"){
    O('soundCPUTemCount').style.visibility = "visible";
    O('timestext').style.visibility = "visible";
  }
  else {
    O('soundCPUTemCount').style.visibility = "hidden";
    O('timestext').style.visibility = "hidden";
  }
}
function getURL(a){
  try {
    var xmlReq = new ActiveXObject("Microsoft.XMLHTTP");
    xmlReq.open("GET", a, false);
    xmlReq.setRequestHeader("If-Modified-Since", "Sat, 1 Jan 2000 00:00:00 GMT");
    xmlReq.send();
    if (xmlReq.status == 200){
      return xmlReq.responseText
    }
    else {
      return false
    }
  }
  catch (urlData){
    return false
  }
}
function updateAvailable(){
  var urlData = getURL("http://addgadgets.com/all_cpu_meter/version.htm");
  if (urlData === false){
    return false
  }
  var version = "4.7";
  var a = parseFloat(version);
  var b = parseFloat(urlData);
  return b > a;
}
function showUpdate(){
  if (updateAvailable()){
    O("newUpdate").style.display = "block"
  }
  else {
    O("noUpdate").style.display = "block"
  }
}
function sizeSettings(){
  if (fixsize.value == "Custom"){
    O('ssize').style.visibility = "visible";
    O('sizetext').style.visibility = "visible";
  }
  else {
    O('ssize').style.visibility = "hidden";
    O('sizetext').style.visibility = "hidden";
  }
}
function graphSettings(){
  if (graph.value == 1){
    O('drawstyle').style.visibility = "visible";
    O('drawtext').style.visibility = "visible";
  }
  else {
    O('drawstyle').style.visibility = "hidden";
    O('drawtext').style.visibility = "hidden";
  }
}
function colorSettings(){
  if (fixBackg.value == ''){
    O('backg').style.visibility = "visible";
    O('showBackg').style.visibility = "hidden";
  }
  else {
    O('backg').style.visibility = "hidden";
    O('showBackg').style.visibility = "visible";
  }
  if (fixTitle.value == ''){
    O('title').style.visibility = "visible";
    O('showTitle').style.visibility = "hidden";
  }
  else {
    O('title').style.visibility = "hidden";
    O('showTitle').style.visibility = "visible";
  }
  if (fixClock.value == ''){
    O('clocks').style.visibility = "visible";
    O('showClock').style.visibility = "hidden";
  }
  else {
    O('clocks').style.visibility = "hidden";
    O('showClock').style.visibility = "visible";
  }
  if (fixProcessor.value == ''){
    O('processor').style.visibility = "visible";
    O('showProcessor').style.visibility = "hidden";
  }
  else {
    O('processor').style.visibility = "hidden";
    O('showProcessor').style.visibility = "visible";
  }
  if (fixDomUse.value == ''){
    O('DomUse').style.visibility = "visible";
    O('showDomUse').style.visibility = "hidden";
  }
  else {
    O('DomUse').style.visibility = "hidden";
    O('showDomUse').style.visibility = "visible";
  }
  if (fixUFT.value == ''){
    O('UFT').style.visibility = "visible";
    O('showUFT').style.visibility = "hidden";
  }
  else {
    O('UFT').style.visibility = "hidden";
    O('showUFT').style.visibility = "visible";
  }
  if (fixRam.value == ''){
    O('rams').style.visibility = "visible";
    O('showRam').style.visibility = "hidden";
  }
  else {
    O('rams').style.visibility = "hidden";
    O('showRam').style.visibility = "visible";
  }
  if (fixPagec.value == ''){
    O('pagec').style.visibility = "visible";
    O('showPagec').style.visibility = "hidden";
  }
  else {
    O('pagec').style.visibility = "hidden";
    O('showPagec').style.visibility = "visible";
  }
  if (fixcolor1.value == ''){
    O('colorc1').style.visibility = "visible";
    O('showColor1').style.visibility = "hidden";
  }
  else {
    O('colorc1').style.visibility = "hidden";
    O('showColor1').style.visibility = "visible";
  }
  if (fixcolor2.value == ''){
    O('colorc2').style.visibility = "visible";
    O('showColor2').style.visibility = "hidden";
  }
  else {
    O('colorc2').style.visibility = "hidden";
    O('showColor2').style.visibility = "visible";
  }
  if (fixcolor3.value == ''){
    O('colorc3').style.visibility = "visible";
    O('showColor3').style.visibility = "hidden";
  }
  else {
    O('colorc3').style.visibility = "hidden";
    O('showColor3').style.visibility = "visible";
  }
  if (fixcolor4.value == ''){
    O('colorc4').style.visibility = "visible";
    O('showColor4').style.visibility = "hidden";
  }
  else {
    O('colorc4').style.visibility = "hidden";
    O('showColor4').style.visibility = "visible";
  }
  if (fixcolor5.value == ''){
    O('colorc5').style.visibility = "visible";
    O('showColor5').style.visibility = "hidden";
  }
  else {
    O('colorc5').style.visibility = "hidden";
    O('showColor5').style.visibility = "visible";
  }
  if (fixcolor6.value == ''){
    O('colorc6').style.visibility = "visible";
    O('showColor6').style.visibility = "hidden";
  }
  else {
    O('colorc6').style.visibility = "hidden";
    O('showColor6').style.visibility = "visible";
  }
  if (fixcolor7.value == ''){
    O('colorc7').style.visibility = "visible";
    O('showColor7').style.visibility = "hidden";
  }
  else {
    O('colorc7').style.visibility = "hidden";
    O('showColor7').style.visibility = "visible";
  }
  if (fixcolor8.value == ''){
    O('colorc8').style.visibility = "visible";
    O('showColor8').style.visibility = "hidden";
  }
  else {
    O('colorc8').style.visibility = "hidden";
    O('showColor8').style.visibility = "visible";
  }
  if (fixcolor9.value == ''){
    O('colorc9').style.visibility = "visible";
    O('showColor9').style.visibility = "hidden";
  }
  else {
    O('colorc9').style.visibility = "hidden";
    O('showColor9').style.visibility = "visible";
  }
  if (fixcolor10.value == ''){
    O('colorc10').style.visibility = "visible";
    O('showColor10').style.visibility = "hidden";
  }
  else {
    O('colorc10').style.visibility = "hidden";
    O('showColor10').style.visibility = "visible";
  }
  if (fixcolor11.value == ''){
    O('colorc11').style.visibility = "visible";
    O('showColor11').style.visibility = "hidden";
  }
  else {
    O('colorc11').style.visibility = "hidden";
    O('showColor11').style.visibility = "visible";
  }
  if (fixcolor12.value == ''){
    O('colorc12').style.visibility = "visible";
    O('showColor12').style.visibility = "hidden";
  }
  else {
    O('colorc12').style.visibility = "hidden";
    O('showColor12').style.visibility = "visible";
  }
  if (fixcolor13.value == ''){
    O('colorc13').style.visibility = "visible";
    O('showColor13').style.visibility = "hidden";
  }
  else {
    O('colorc13').style.visibility = "hidden";
    O('showColor13').style.visibility = "visible";
  }
  if (fixcolor14.value == ''){
    O('colorc14').style.visibility = "visible";
    O('showColor14').style.visibility = "hidden";
  }
  else {
    O('colorc14').style.visibility = "hidden";
    O('showColor14').style.visibility = "visible";
  }
  if (fixcolor15.value == ''){
    O('colorc15').style.visibility = "visible";
    O('showColor15').style.visibility = "hidden";
  }
  else {
    O('colorc15').style.visibility = "hidden";
    O('showColor15').style.visibility = "visible";
  }
  if (fixcolor16.value == ''){
    O('colorc16').style.visibility = "visible";
    O('showColor16').style.visibility = "hidden";
  }
  else {
    O('colorc16').style.visibility = "hidden";
    O('showColor16').style.visibility = "visible";
  }
  if (fixcolor17.value == ''){
    O('colorc17').style.visibility = "visible";
    O('showColor17').style.visibility = "hidden";
  }
  else {
    O('colorc17').style.visibility = "hidden";
    O('showColor17').style.visibility = "visible";
  }
  if (fixcolor18.value == ''){
    O('colorc18').style.visibility = "visible";
    O('showColor18').style.visibility = "hidden";
  }
  else {
    O('colorc18').style.visibility = "hidden";
    O('showColor18').style.visibility = "visible";
  }
  if (fixcolor19.value == ''){
    O('colorc19').style.visibility = "visible";
    O('showColor19').style.visibility = "hidden";
  }
  else {
    O('colorc19').style.visibility = "hidden";
    O('showColor19').style.visibility = "visible";
  }
  if (fixcolor20.value == ''){
    O('colorc20').style.visibility = "visible";
    O('showColor20').style.visibility = "hidden";
  }
  else {
    O('colorc20').style.visibility = "hidden";
    O('showColor20').style.visibility = "visible";
  }
  if (fixcolor21.value == ''){
    O('colorc21').style.visibility = "visible";
    O('showColor21').style.visibility = "hidden";
  }
  else {
    O('colorc21').style.visibility = "hidden";
    O('showColor21').style.visibility = "visible";
  }
  if (fixcolor22.value == ''){
    O('colorc22').style.visibility = "visible";
    O('showColor22').style.visibility = "hidden";
  }
  else {
    O('colorc22').style.visibility = "hidden";
    O('showColor22').style.visibility = "visible";
  }
  if (fixcolor23.value == ''){
    O('colorc23').style.visibility = "visible";
    O('showColor23').style.visibility = "hidden";
  }
  else {
    O('colorc23').style.visibility = "hidden";
    O('showColor23').style.visibility = "visible";
  }
  if (fixcolor24.value == ''){
    O('colorc24').style.visibility = "visible";
    O('showColor24').style.visibility = "hidden";
  }
  else {
    O('colorc24').style.visibility = "hidden";
    O('showColor24').style.visibility = "visible";
  }
  if (fixcolor25.value == ''){
    O('colorc25').style.visibility = "visible";
    O('showColor25').style.visibility = "hidden";
  }
  else {
    O('colorc25').style.visibility = "hidden";
    O('showColor25').style.visibility = "visible";
  }
  if (fixcolor26.value == ''){
    O('colorc26').style.visibility = "visible";
    O('showColor26').style.visibility = "hidden";
  }
  else {
    O('colorc26').style.visibility = "hidden";
    O('showColor26').style.visibility = "visible";
  }
  if (fixcolor27.value == ''){
    O('colorc27').style.visibility = "visible";
    O('showColor27').style.visibility = "hidden";
  }
  else {
    O('colorc27').style.visibility = "hidden";
    O('showColor27').style.visibility = "visible";
  }
  if (fixcolor28.value == ''){
    O('colorc28').style.visibility = "visible";
    O('showColor28').style.visibility = "hidden";
  }
  else {
    O('colorc28').style.visibility = "hidden";
    O('showColor28').style.visibility = "visible";
  }
  if (fixcolor29.value == ''){
    O('colorc29').style.visibility = "visible";
    O('showColor29').style.visibility = "hidden";
  }
  else {
    O('colorc29').style.visibility = "hidden";
    O('showColor29').style.visibility = "visible";
  }
  if (fixcolor30.value == ''){
    O('colorc30').style.visibility = "visible";
    O('showColor30').style.visibility = "hidden";
  }
  else {
    O('colorc30').style.visibility = "hidden";
    O('showColor30').style.visibility = "visible";
  }
  if (fixcolor31.value == ''){
    O('colorc31').style.visibility = "visible";
    O('showColor31').style.visibility = "hidden";
  }
  else {
    O('colorc31').style.visibility = "hidden";
    O('showColor31').style.visibility = "visible";
  }
  if (fixcolor32.value == ''){
    O('colorc32').style.visibility = "visible";
    O('showColor32').style.visibility = "hidden";
  }
  else {
    O('colorc32').style.visibility = "hidden";
    O('showColor32').style.visibility = "visible";
  }
  if (fixAlertIcon.value == ''){
    O('alertIcon').style.visibility = "visible";
    O('showAlertIcon').style.visibility = "hidden";
  }
  else {
    O('alertIcon').style.visibility = "hidden";
    O('showAlertIcon').style.visibility = "visible";
  }
  if (fixFlyoutBac.value == ''){
    O('FlyoutBac').style.visibility = "visible";
    O('showFlyoutBac').style.visibility = "hidden";
  }
  else {
    O('FlyoutBac').style.visibility = "hidden";
    O('showFlyoutBac').style.visibility = "visible";
  }
  if (fixFlyoutTit.value == ''){
    O('FlyoutTit').style.visibility = "visible";
    O('showFlyoutTit').style.visibility = "hidden";
  }
  else {
    O('FlyoutTit').style.visibility = "hidden";
    O('showFlyoutTit').style.visibility = "visible";
  }
  if (fixFlyoutDet.value == ''){
    O('FlyoutDet').style.visibility = "visible";
    O('showFlyoutDet').style.visibility = "hidden";
  }
  else {
    O('FlyoutDet').style.visibility = "hidden";
    O('showFlyoutDet').style.visibility = "visible";
  }
}
function showColor(){
  if (fixBackg.value != "")sBackg = fixBackg.value;
  else sBackg = backg.value;
  O('showBackg').style.color = sBackg;
  O('showBackg').style.width = 50;
  O('showBackg').style.height = 21;
  O('showBackg').style.top = 6;
  O('showBackg').style.left = 206;
  if (fixTitle.value != "")sTitle = fixTitle.value;
  else sTitle = title.value;
  O('showTitle').style.color = sTitle;
  O('showTitle').style.width = 50;
  O('showTitle').style.height = 21;
  O('showTitle').style.top = 29;
  O('showTitle').style.left = 206;
  if (fixClock.value != "")sClock = fixClock.value;
  else sClock = clocks.value;
  O('showClock').style.color = sClock;
  O('showClock').style.width = 50;
  O('showClock').style.height = 21;
  O('showClock').style.top = 52;
  O('showClock').style.left = 206;
  if (fixAlertIcon.value != "")sAlertIcon = fixAlertIcon.value;
  else sAlertIcon = alertIcon.value;
  O('showAlertIcon').style.color = sAlertIcon;
  O('showAlertIcon').style.width = 50;
  O('showAlertIcon').style.height = 21;
  O('showAlertIcon').style.top = 75;
  O('showAlertIcon').style.left = 206;
  if (fixProcessor.value != "")sProcessor = fixProcessor.value;
  else sProcessor = processor.value;
  O('showProcessor').style.color = sProcessor;
  O('showProcessor').style.width = 50;
  O('showProcessor').style.height = 21;
  O('showProcessor').style.top = 98;
  O('showProcessor').style.left = 206;
  if (fixDomUse.value != "")sDomUse = fixDomUse.value;
  else sDomUse = DomUse.value;
  O('showDomUse').style.color = sDomUse;
  O('showDomUse').style.width = 50;
  O('showDomUse').style.height = 21;
  O('showDomUse').style.top = 121;
  O('showDomUse').style.left = 206;
  if (fixUFT.value != "")sUFT = fixUFT.value;
  else sUFT = UFT.value;
  O('showUFT').style.color = sUFT;
  O('showUFT').style.width = 50;
  O('showUFT').style.height = 21;
  O('showUFT').style.top = 144;
  O('showUFT').style.left = 206;
  if (fixRam.value != "")sRam = fixRam.value;
  else sRam = rams.value;
  O('showRam').style.color = sRam;
  O('showRam').style.width = 50;
  O('showRam').style.height = 21;
  O('showRam').style.top = 167;
  O('showRam').style.left = 206;
  if (fixPagec.value != "")sPagec = fixPagec.value;
  else sPagec = pagec.value;
  O('showPagec').style.color = sPagec;
  O('showPagec').style.width = 50;
  O('showPagec').style.height = 21;
  O('showPagec').style.top = 190;
  O('showPagec').style.left = 206;
  if (fixcolor1.value != "")scolor1 = fixcolor1.value;
  else scolor1 = colorc1.value;
  O('showColor1').style.color = scolor1;
  O('showColor1').style.width = 50;
  O('showColor1').style.height = 21;
  O('showColor1').style.top = 213;
  O('showColor1').style.left = 206;
  if (fixcolor2.value != "")scolor2 = fixcolor2.value;
  else scolor2 = colorc2.value;
  O('showColor2').style.color = scolor2;
  O('showColor2').style.width = 50;
  O('showColor2').style.height = 21;
  O('showColor2').style.top = 236;
  O('showColor2').style.left = 206;
  if (fixcolor3.value != "")scolor3 = fixcolor3.value;
  else scolor3 = colorc3.value;
  O('showColor3').style.color = scolor3;
  O('showColor3').style.width = 50;
  O('showColor3').style.height = 21;
  O('showColor3').style.top = 259;
  O('showColor3').style.left = 206;
  if (fixcolor4.value != "")scolor4 = fixcolor4.value;
  else scolor4 = colorc4.value;
  O('showColor4').style.color = scolor4;
  O('showColor4').style.width = 50;
  O('showColor4').style.height = 21;
  O('showColor4').style.top = 282;
  O('showColor4').style.left = 206;
  if (fixcolor5.value != "")scolor5 = fixcolor5.value;
  else scolor5 = colorc5.value;
  O('showColor5').style.color = scolor5;
  O('showColor5').style.width = 50;
  O('showColor5').style.height = 21;
  O('showColor5').style.top = 305;
  O('showColor5').style.left = 206;
  if (fixcolor6.value != "")scolor6 = fixcolor6.value;
  else scolor6 = colorc6.value;
  O('showColor6').style.color = scolor6;
  O('showColor6').style.width = 50;
  O('showColor6').style.height = 21;
  O('showColor6').style.top = 328;
  O('showColor6').style.left = 206;
  if (fixcolor7.value != "")scolor7 = fixcolor7.value;
  else scolor7 = colorc7.value;
  O('showColor7').style.color = scolor7;
  O('showColor7').style.width = 50;
  O('showColor7').style.height = 21;
  O('showColor7').style.top = 351;
  O('showColor7').style.left = 206;
  if (fixcolor8.value != "")scolor8 = fixcolor8.value;
  else scolor8 = colorc8.value;
  O('showColor8').style.color = scolor8;
  O('showColor8').style.width = 50;
  O('showColor8').style.height = 21;
  O('showColor8').style.top = 374;
  O('showColor8').style.left = 206;
  if (fixcolor9.value != "")scolor9 = fixcolor9.value;
  else scolor9 = colorc9.value;
  O('showColor9').style.color = scolor9;
  O('showColor9').style.width = 50;
  O('showColor9').style.height = 21;
  O('showColor9').style.top = 397;
  O('showColor9').style.left = 206;
  if (fixcolor10.value != "")scolor10 = fixcolor10.value;
  else scolor10 = colorc10.value;
  O('showColor10').style.color = scolor10;
  O('showColor10').style.width = 50;
  O('showColor10').style.height = 21;
  O('showColor10').style.top = 420;
  O('showColor10').style.left = 206;
  if (fixcolor11.value != "")scolor11 = fixcolor11.value;
  else scolor11 = colorc11.value;
  O('showColor11').style.color = scolor11;
  O('showColor11').style.width = 50;
  O('showColor11').style.height = 21;
  O('showColor11').style.top = 443;
  O('showColor11').style.left = 206;
  if (fixcolor12.value != "")scolor12 = fixcolor12.value;
  else scolor12 = colorc12.value;
  O('showColor12').style.color = scolor12;
  O('showColor12').style.width = 50;
  O('showColor12').style.height = 21;
  O('showColor12').style.top = 466;
  O('showColor12').style.left = 206;
  if (fixcolor13.value != "")scolor13 = fixcolor13.value;
  else scolor13 = colorc13.value;
  O('showColor13').style.color = scolor13;
  O('showColor13').style.width = 50;
  O('showColor13').style.height = 21;
  O('showColor13').style.top = 489;
  O('showColor13').style.left = 206;
  if (fixcolor14.value != "")scolor14 = fixcolor14.value;
  else scolor14 = colorc14.value;
  O('showColor14').style.color = scolor14;
  O('showColor14').style.width = 50;
  O('showColor14').style.height = 21;
  O('showColor14').style.top = 512;
  O('showColor14').style.left = 206;
  if (fixcolor15.value != "")scolor15 = fixcolor15.value;
  else scolor15 = colorc15.value;
  O('showColor15').style.color = scolor15;
  O('showColor15').style.width = 50;
  O('showColor15').style.height = 21;
  O('showColor15').style.top = 535;
  O('showColor15').style.left = 206;
  if (fixcolor16.value != "")scolor16 = fixcolor16.value;
  else scolor16 = colorc16.value;
  O('showColor16').style.color = scolor16;
  O('showColor16').style.width = 50;
  O('showColor16').style.height = 21;
  O('showColor16').style.top = 558;
  O('showColor16').style.left = 206;
  if (fixcolor17.value != "")scolor17 = fixcolor17.value;
  else scolor17 = colorc17.value;
  O('showColor17').style.color = scolor17;
  O('showColor17').style.width = 50;
  O('showColor17').style.height = 21;
  O('showColor17').style.top = 581;
  O('showColor17').style.left = 206;
  if (fixcolor18.value != "")scolor18 = fixcolor18.value;
  else scolor18 = colorc18.value;
  O('showColor18').style.color = scolor18;
  O('showColor18').style.width = 50;
  O('showColor18').style.height = 21;
  O('showColor18').style.top = 604;
  O('showColor18').style.left = 206;
  if (fixcolor19.value != "")scolor19 = fixcolor19.value;
  else scolor19 = colorc19.value;
  O('showColor19').style.color = scolor19;
  O('showColor19').style.width = 50;
  O('showColor19').style.height = 21;
  O('showColor19').style.top = 627;
  O('showColor19').style.left = 206;
  if (fixcolor20.value != "")scolor20 = fixcolor20.value;
  else scolor20 = colorc20.value;
  O('showColor20').style.color = scolor20;
  O('showColor20').style.width = 50;
  O('showColor20').style.height = 21;
  O('showColor20').style.top = 650;
  O('showColor20').style.left = 206;
  if (fixcolor21.value != "")scolor21 = fixcolor21.value;
  else scolor21 = colorc21.value;
  O('showColor21').style.color = scolor21;
  O('showColor21').style.width = 50;
  O('showColor21').style.height = 21;
  O('showColor21').style.top = 673;
  O('showColor21').style.left = 206;
  if (fixcolor22.value != "")scolor22 = fixcolor22.value;
  else scolor22 = colorc22.value;
  O('showColor22').style.color = scolor22;
  O('showColor22').style.width = 50;
  O('showColor22').style.height = 21;
  O('showColor22').style.top = 696;
  O('showColor22').style.left = 206;
  if (fixcolor23.value != "")scolor23 = fixcolor23.value;
  else scolor23 = colorc23.value;
  O('showColor23').style.color = scolor23;
  O('showColor23').style.width = 50;
  O('showColor23').style.height = 21;
  O('showColor23').style.top = 719;
  O('showColor23').style.left = 206;
  if (fixcolor24.value != "")scolor24 = fixcolor24.value;
  else scolor24 = colorc24.value;
  O('showColor24').style.color = scolor24;
  O('showColor24').style.width = 50;
  O('showColor24').style.height = 21;
  O('showColor24').style.top = 742;
  O('showColor24').style.left = 206;
  if (fixcolor25.value != "")scolor25 = fixcolor25.value;
  else scolor25 = colorc25.value;
  O('showColor25').style.color = scolor25;
  O('showColor25').style.width = 50;
  O('showColor25').style.height = 21;
  O('showColor25').style.top = 765;
  O('showColor25').style.left = 206;
  if (fixcolor26.value != "")scolor26 = fixcolor26.value;
  else scolor26 = colorc26.value;
  O('showColor26').style.color = scolor26;
  O('showColor26').style.width = 50;
  O('showColor26').style.height = 21;
  O('showColor26').style.top = 788;
  O('showColor26').style.left = 206;
  if (fixcolor27.value != "")scolor27 = fixcolor27.value;
  else scolor27 = colorc27.value;
  O('showColor27').style.color = scolor27;
  O('showColor27').style.width = 50;
  O('showColor27').style.height = 21;
  O('showColor27').style.top = 811;
  O('showColor27').style.left = 206;
  if (fixcolor28.value != "")scolor28 = fixcolor28.value;
  else scolor28 = colorc28.value;
  O('showColor28').style.color = scolor28;
  O('showColor28').style.width = 50;
  O('showColor28').style.height = 21;
  O('showColor28').style.top = 834;
  O('showColor28').style.left = 206;
  if (fixcolor29.value != "")scolor29 = fixcolor29.value;
  else scolor29 = colorc29.value;
  O('showColor29').style.color = scolor29;
  O('showColor29').style.width = 50;
  O('showColor29').style.height = 21;
  O('showColor29').style.top = 857;
  O('showColor29').style.left = 206;
  if (fixcolor30.value != "")scolor30 = fixcolor30.value;
  else scolor30 = colorc30.value;
  O('showColor30').style.color = scolor30;
  O('showColor30').style.width = 50;
  O('showColor30').style.height = 21;
  O('showColor30').style.top = 880;
  O('showColor30').style.left = 206;
  if (fixcolor31.value != "")scolor31 = fixcolor31.value;
  else scolor31 = colorc31.value;
  O('showColor31').style.color = scolor31;
  O('showColor31').style.width = 50;
  O('showColor31').style.height = 21;
  O('showColor31').style.top = 903;
  O('showColor31').style.left = 206;
  if (fixcolor32.value != "")scolor32 = fixcolor32.value;
  else scolor32 = colorc32.value;
  O('showColor32').style.color = scolor32;
  O('showColor32').style.width = 50;
  O('showColor32').style.height = 21;
  O('showColor32').style.top = 926;
  O('showColor32').style.left = 206;
  if (fixFlyoutBac.value != "")sFlyoutBac = fixFlyoutBac.value;
  else sFlyoutBac = FlyoutBac.value;
  O('showFlyoutBac').style.color = sFlyoutBac;
  O('showFlyoutBac').style.width = 50;
  O('showFlyoutBac').style.height = 21;
  O('showFlyoutBac').style.top = 979;
  O('showFlyoutBac').style.left = 206;
  if (fixFlyoutTit.value != "")sFlyoutTit = fixFlyoutTit.value;
  else sFlyoutTit = FlyoutTit.value;
  O('showFlyoutTit').style.color = sFlyoutTit;
  O('showFlyoutTit').style.width = 50;
  O('showFlyoutTit').style.height = 21;
  O('showFlyoutTit').style.top = 1002;
  O('showFlyoutTit').style.left = 206;
  if (fixFlyoutDet.value != "")sFlyoutDet = fixFlyoutDet.value;
  else sFlyoutDet = FlyoutDet.value;
  O('showFlyoutDet').style.color = sFlyoutDet;
  O('showFlyoutDet').style.width = 50;
  O('showFlyoutDet').style.height = 21;
  O('showFlyoutDet').style.top = 1025;
  O('showFlyoutDet').style.left = 206;
}
function DefDisplaySetting(){
  ssize.value = 100;
  fixsize.value = 100;
  showIcon.value = 1;
  clockFre.value = 1;
  cpuName.value = 1;
  showUsername.value = 1;
  showMem.value = 1;
  showMemBar.value = 1;
  showMemGraph.value = 1;
  showPageFile.value = 1;
  showPageFileBar.value = 1;
  showPageFileGraph.value = 1;
  graph.value = 1;
  drawstyle.value = 1;
  doubleClick.value = 1;
}
function DefOptionsSetting(){
  settimer.value = 1;
  showTem.value = 2;
  showErr.value = 1;
  soundCPUTem.value = 1;
  soundCPUTemurl.innerText = '';
  alertCPU1.value = 2;
  alertCPUTem.value = 80;
  soundCPUTemVol.value = 100;
  soundCPUTemRepeats.value = 3;
  soundCPUTemCount.value = 1;
  update.value = 1;
}
function DefColorSetting(){
  backg.value = "080808";
  fixBackg.value = "";
  title.value = "FFFFFF";
  fixTitle.value = "";
  clocks.value = "90EE90";
  fixClock.value = "";
  alertIcon.value = "90EE90";
  fixAlertIcon.value = "";
  processor.value = "FFF62A";
  fixProcessor.value = "";
  DomUse.value = "FFF62A";
  fixDomUse.value = "";
  UFT.value = "FFFFFF";
  fixUFT.value = "";
  rams.value = "87CEFA";
  fixRam.value = "";
  pagec.value = "FFCC00";
  fixPagec.value = "";
  colorc1.value = "90EE90";
  fixcolor1.value = "";
  colorc2.value = "FFF62A";
  fixcolor2.value = "";
  colorc3.value = "FABA00";
  fixcolor3.value = "";
  colorc4.value = "EC7527";
  fixcolor4.value = "";
  colorc5.value = "E5316C";
  fixcolor5.value = "";
  colorc6.value = "E2003B";
  fixcolor6.value = "";
  colorc7.value = "D7007A";
  fixcolor7.value = "";
  colorc8.value = "AE3288";
  fixcolor8.value = "";
  colorc9.value = "894B94";
  fixcolor9.value = "";
  colorc10.value = "5B5099";
  fixcolor10.value = "";
  colorc11.value = "4D61A8";
  fixcolor11.value = "";
  colorc12.value = "4D81A2";
  fixcolor12.value = "";
  colorc13.value = "4DA0AE";
  fixcolor13.value = "";
  colorc14.value = "4DB896";
  fixcolor14.value = "";
  colorc15.value = "64C566";
  fixcolor15.value = "";
  colorc16.value = "79B63E";
  fixcolor16.value = "";
  colorc17.value = "90EE90";
  fixcolor17.value = "";
  colorc18.value = "FFF62A";
  fixcolor18.value = "";
  colorc19.value = "FABA00";
  fixcolor19.value = "";
  colorc20.value = "EC7527";
  fixcolor20.value = "";
  colorc21.value = "E5316C";
  fixcolor21.value = "";
  colorc22.value = "E2003B";
  fixcolor22.value = "";
  colorc23.value = "D7007A";
  fixcolor23.value = "";
  colorc24.value = "AE3288";
  fixcolor24.value = "";
  colorc25.value = "894B94";
  fixcolor25.value = "";
  colorc26.value = "5B5099";
  fixcolor26.value = "";
  colorc27.value = "4D61A8";
  fixcolor27.value = "";
  colorc28.value = "4D81A2";
  fixcolor28.value = "";
  colorc29.value = "4DA0AE";
  fixcolor29.value = "";
  colorc30.value = "4DB896";
  fixcolor30.value = "";
  colorc31.value = "64C566";
  fixcolor31.value = "";
  colorc32.value = "79B63E";
  fixcolor32.value = "";
  FlyoutBac.value = "080808";
  fixFlyoutBac.value = "";
  FlyoutTit.value = "87CEFA";
  fixFlyoutTit.value = "";
  FlyoutDet.value = "FFCC00";
  fixFlyoutDet.value = "";
}
function tabberObj(argsObj){
  var arg;
  this .div = null;
  this .classMain = "tabber";
  this .classMainLive = "tabberlive";
  this .classTab = "tabbertab";
  this .classTabDefault = "tabbertabdefault";
  this .classNav = "tabbernav";
  this .classTabHide = "tabbertabhide";
  this .classNavActive = "tabberactive";
  this .titleElements = ['h2', 'h3', 'h4', 'h5', 'h6'];
  this .titleElementsStripHTML = true;
  this .removeTitle = true;
  this .addLinkId = false;
  this .linkIdFormat = '<tabberid>nav<tabnumberone>';
  for (arg in argsObj){
    this [arg] = argsObj[arg];
  }
  this .REclassMain = new RegExp('\\b' + this .classMain + '\\b', 'gi');
  this .REclassMainLive = new RegExp('\\b' + this .classMainLive + '\\b', 'gi');
  this .REclassTab = new RegExp('\\b' + this .classTab + '\\b', 'gi');
  this .REclassTabDefault = new RegExp('\\b' + this .classTabDefault + '\\b', 'gi');
  this .REclassTabHide = new RegExp('\\b' + this .classTabHide + '\\b', 'gi');
  this .tabs = new Array();
  if (this .div){
    this .init(this .div);
    this .div = null;
  }
}
tabberObj.prototype.init = function (e){
  var childNodes, i, i2, t, defaultTab = 0, DOM_ul, DOM_li, DOM_a, aId, headingElement;
  if (!document.getElementsByTagName){
    return false;
  }
  if (e.id){
    this .id = e.id;
  }
  this .tabs.length = 0;
  childNodes = e.childNodes;
  for (i = 0; i < childNodes.length; i ++ ){
    if (childNodes[i].className && childNodes[i].className.match(this .REclassTab)){
      t = new Object();
      t.div = childNodes[i];
      this .tabs[this .tabs.length] = t;
      if (childNodes[i].className.match(this .REclassTabDefault)){
        defaultTab = this .tabs.length - 1;
      }
    }
  }
  DOM_ul = document.createElement("ul");
  DOM_ul.className = this .classNav;
  for (i = 0; i < this .tabs.length; i ++ ){
    t = this .tabs[i];
    t.headingText = t.div.title;
    if (this .removeTitle){
      t.div.title = '';
    }
    if (!t.headingText){
      for (i2 = 0; i2 < this .titleElements.length; i2 ++ ){
        headingElement = t.div.getElementsByTagName(this .titleElements[i2])[0];
        if (headingElement){
          t.headingText = headingElement.innerHTML;
          if (this .titleElementsStripHTML){
            t.headingText.replace(/<br>/gi, " ");
            t.headingText = t.headingText.replace(/<[^>]+>/g, "");
          }
          break ;
        }
      }
    }
    if (!t.headingText){
      t.headingText = i + 1;
    }
    DOM_li = document.createElement("li");
    t.li = DOM_li;
    DOM_a = document.createElement("a");
    DOM_a.appendChild(document.createTextNode(t.headingText));
    DOM_a.href = "javascript:void(null);";
    DOM_a.title = t.headingText;
    DOM_a.onclick = this .navClick;
    DOM_a.tabber = this ;
    DOM_a.tabberIndex = i;
    if (this .addLinkId && this .linkIdFormat){
      aId = this .linkIdFormat;
      aId = aId.replace(/<tabberid>/gi, this .id);
      aId = aId.replace(/<tabnumberzero>/gi, i);
      aId = aId.replace(/<tabnumberone>/gi, i + 1);
      aId = aId.replace(/<tabtitle>/gi, t.headingText.replace(/[^a-zA-Z0-9\-]/gi, ''));
      DOM_a.id = aId;
    }
    DOM_li.appendChild(DOM_a);
    DOM_ul.appendChild(DOM_li);
  }
  e.insertBefore(DOM_ul, e.firstChild);
  e.className = e.className.replace(this .REclassMain, this .classMainLive);
  this .tabShow(defaultTab);
  if (typeof this .onLoad == 'function'){
    this .onLoad({
      tabber : this 
    }
    );
  }
  return this ;
}
;
tabberObj.prototype.navClick = function (event){
  var rVal, a, self, tabberIndex, onClickArgs;
  a = this ;
  if (!a.tabber){
    return false;
  }
  self = a.tabber;
  tabberIndex = a.tabberIndex;
  a.blur();
  if (typeof self.onClick == 'function'){
    onClickArgs = {
      'tabber' : self, 'index' : tabberIndex, 'event' : event
    }
    ;
    if (!event){
      onClickArgs.event = window.event;
    }
    rVal = self.onClick(onClickArgs);
    if (rVal === false){
      return false;
    }
  }
  self.tabShow(tabberIndex);
  return false;
}
;
tabberObj.prototype.tabHideAll = function (){
  var i;
  for (i = 0; i < this .tabs.length; i ++ ){
    this .tabHide(i);
  }
}
;
tabberObj.prototype.tabHide = function (tabberIndex){
  var div;
  if (!this .tabs[tabberIndex]){
    return false;
  }
  div = this .tabs[tabberIndex].div;
  if (!div.className.match(this .REclassTabHide)){
    div.className += ' ' + this .classTabHide;
  }
  this .navClearActive(tabberIndex);
  return this ;
}
;
tabberObj.prototype.tabShow = function (tabberIndex){
  var div;
  if (!this .tabs[tabberIndex]){
    return false;
  }
  this .tabHideAll();
  div = this .tabs[tabberIndex].div;
  div.className = div.className.replace(this .REclassTabHide, '');
  this .navSetActive(tabberIndex);
  if (typeof this .onTabDisplay == 'function'){
    this .onTabDisplay({
      'tabber' : this , 'index' : tabberIndex
    }
    );
  }
  return this ;
}
;
tabberObj.prototype.navSetActive = function (tabberIndex){
  this .tabs[tabberIndex].li.className = this .classNavActive;
  return this ;
}
;
tabberObj.prototype.navClearActive = function (tabberIndex){
  this .tabs[tabberIndex].li.className = '';
  return this ;
}
;
function tabberAutomatic(tabberArgs){
  var tempObj, divs, i;
  if (!tabberArgs){
    tabberArgs = {
    }
    ;
  }
  tempObj = new tabberObj(tabberArgs);
  divs = document.getElementsByTagName("div");
  for (i = 0; i < divs.length; i ++ ){
    if (divs[i].className && divs[i].className.match(tempObj.REclassMain)){
      tabberArgs.div = divs[i];
      divs[i].tabber = new tabberObj(tabberArgs);
    }
  }
  return this ;
}
function tabberAutomaticOnLoad(tabberArgs){
  var oldOnLoad;
  if (!tabberArgs){
    tabberArgs = {
    }
    ;
  }
  oldOnLoad = window.onload;
  if (typeof window.onload != 'function'){
    window.onload = function (){
      tabberAutomatic(tabberArgs);
    }
    ;
  }
  else {
    window.onload = function (){
      oldOnLoad();
      tabberAutomatic(tabberArgs);
    }
    ;
  }
}
if (typeof tabberOptions == 'undefined'){
  tabberAutomaticOnLoad();
}
else {
  if (!tabberOptions['manualStartup']){
    tabberAutomaticOnLoad(tabberOptions);
  }
}
