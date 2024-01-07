nsp = 'Old browser!';
dl = document.layers;
oe = window.opera ? 1 : 0;
da = document.all && !oe;
ge = document.getElementById;
ws = window.sidebar ? true : false;
tN = navigator.userAgent.toLowerCase();
izN = tN.indexOf('netscape') >= 0 ? true : false;
zis = tN.indexOf('msie 7') >= 0 ? true : false;
zis8 = tN.indexOf('msie 8') >= 0 ? true : false;
zis |= zis8;
if (ws && !izN) {
    quogl = 'iuy'
}
;
var msg = '';
function nem() {
    return true
}
;
window.onerror = nem;
zOF = window.location.protocol.indexOf("file") != -1 ? true : false;
i7f = zis && !zOF ? true : false;
System.Gadget.settingsUI = "settings.html";
System.Gadget.onSettingsClosed = onSettingsClosed;
var coreCount = System.Machine.CPUs.count;
var size = 1;
var sizeUpdate = 0;
var totalUsage = 0;
var availableMemory = 0;
var totalMemory = 0;
var readRamTotal = 0;
var readRamFree = 0;
var readRamUsage = 0;
var readRamPerc = 0;
var totalPage = 0;
var usagePage = 0;
var readPageTotal = 0;
var readPageFree = 0;
var readPageUsage = 0;
var readPagePerc = 0;
var coreUsage = new Array();
var temp = new Array();
var loadTem = new Array();
var coreHist = new Array();
var c = new Array();
var cpath = new Array();
var scolor = new Array();
var CPUFHist = new Array();
var ramHist = new Array();
var pageHist = new Array();
var totalPagefile = new Array();
var usagePagefile = new Array();
for (var i = 0; i < coreCount; i++) {
    coreUsage[i] = 0;
    temp[i] = 0;
    loadTem[i] = 0;
}
for (var i = 1; i < coreCount + 1; i++) {
    coreHist[i] = new Array();
}
var maxCoreCount = 32;
var maxHist = 110;
var stime;
var allSizeChange = 0;
var sizeChange = 0;
var sizeChange2 = 10;
var sizeChange3 = 10;
var sizeChange4 = 0;
var sizeChange5 = 0;
var sizeChange6 = 10;
var sizeChange7 = 10;
var sizeChange8 = 10;
var howCPU = -1;
var AMDProcessor = -1;
var oneTempSensor = 0;
var numOfPro = 1;
var CPU0NC = 1;
var coreThread = 2;
var coreTemLoad = 4;
var CPUTemperature = 0;
var CoreTemp;
var O = document.getElementById;
var locator = new ActiveXObject("WbemScripting.SWbemLocator");
var wmiService = locator.ConnectServer(null, "root\\cimv2");
function onLoad() {
    var Startup = System.Gadget.Settings.read("Startup");
    if (Startup == "1") {
        loadSettings();
    }
    else {
        loadfilesettings();
    }
    initMain();
    initHist();
    onTimer();
    setonTimer();
    CoreTemp = CreateObjectFromDLL();
    if (CoreTemp == null) {
        O('coreTempErr').innerHTML = "Load library error!";
        return;
    }
    try {
        CoreTemp.Refresh();
    }
    catch (err) {
    }
}
function onSettingsClosed() {
    clearInterval(stime);
    loadSettings();
    initMain();
    initHist();
    setTimeout("setonTimer()", 500);
}
function setonTimer() {
    settimer = System.Gadget.Settings.read("settimer");
    if (settimer != "");
    else settimer = "1";
    stime = setInterval("onTimer()", parseInt(settimer * 1000));
}
function loadSettings() {
    showIcon = System.Gadget.Settings.read("showIcon");
    if (showIcon != "");
    else showIcon = "1";
    drawstyle = System.Gadget.Settings.read("drawstyle");
    if (drawstyle != "");
    else drawstyle = "1";
    graph = System.Gadget.Settings.read("graph");
    if (graph != "");
    else graph = "1";
    size = System.Gadget.Settings.read("size");
    if (size != "");
    else size = "1";
    if (size <= "4");
    else size = "4";
    cpuName = System.Gadget.Settings.read("cpuName");
    if (cpuName != "");
    else cpuName = "1";
    clockFre = System.Gadget.Settings.read("clockFre");
    if (clockFre != "");
    else clockFre = "1";
    showUsername = System.Gadget.Settings.read("showUsername");
    if (showUsername != "");
    else showUsername = "1";
    showMem = System.Gadget.Settings.read("showMem");
    if (showMem != "");
    else showMem = "1";
    showMemBar = System.Gadget.Settings.read("showMemBar");
    if (showMemBar != "");
    else showMemBar = "1";
    showMemGraph = System.Gadget.Settings.read("showMemGraph");
    if (showMemGraph != "");
    else showMemGraph = "1";
    showPageFile = System.Gadget.Settings.read("showPageFile");
    if (showPageFile != "");
    else showPageFile = "1";
    showPageFileBar = System.Gadget.Settings.read("showPageFileBar");
    if (showPageFileBar != "");
    else showPageFileBar = "1";
    showPageFileGraph = System.Gadget.Settings.read("showPageFileGraph");
    if (showPageFileGraph != "");
    else showPageFileGraph = "1";
    showTem = System.Gadget.Settings.read("showTem");
    if (showTem != "");
    else showTem = "2";
    temperature = System.Gadget.Settings.read("temperature");
    if (temperature != "");
    else temperature = "2";
    showErr = System.Gadget.Settings.read("showErr");
    if (showErr != "");
    else showErr = "1";
    soundCPUTem = System.Gadget.Settings.read("soundCPUTem");
    if (soundCPUTem != "");
    else soundCPUTem = "1";
    soundCPUTemurl = System.Gadget.Settings.read("soundCPUTemurl");
    if (soundCPUTemurl != "");
    else soundCPUTemurl = "";
    alertCPU1 = System.Gadget.Settings.read("alertCPU1");
    if (alertCPU1 != "");
    else alertCPU1 = "2";
    alertCPUTem = System.Gadget.Settings.read("alertCPUTem");
    if (alertCPUTem != "");
    else alertCPUTem = "80";
    soundCPUTemVol = System.Gadget.Settings.read("soundCPUTemVol");
    if (soundCPUTemVol != "");
    else soundCPUTemVol = "100";
    soundCPUTemRepeats = System.Gadget.Settings.read("soundCPUTemRepeats");
    if (soundCPUTemRepeats != "");
    else soundCPUTemRepeats = "3";
    soundCPUTemCount = System.Gadget.Settings.read("soundCPUTemCount");
    if (soundCPUTemCount != "");
    else soundCPUTemCount = "1";
    update = System.Gadget.Settings.read("update");
    if (update != "");
    else update = "1";
    doubleClick = System.Gadget.Settings.read("doubleClick");
    if (doubleClick != "");
    else doubleClick = "1";
    backg = System.Gadget.Settings.read("backg");
    if (backg != "");
    else backg = "#080808";
    fixBackg = System.Gadget.Settings.read("fixBackg");
    if (fixBackg != "") sBackg = fixBackg;
    else sBackg = backg;
    title = System.Gadget.Settings.read("title");
    if (title != "");
    else title = "#ffffff";
    fixTitle = System.Gadget.Settings.read("fixTitle");
    if (fixTitle != "") sTitle = fixTitle;
    else sTitle = title;
    clocks = System.Gadget.Settings.read("clocks");
    if (clocks != "");
    else clocks = "#90ee90";
    fixClock = System.Gadget.Settings.read("fixClock");
    if (fixClock != "") sClock = fixClock;
    else sClock = clocks;
    processor = System.Gadget.Settings.read("processor");
    if (processor != "");
    else processor = "#fff62a";
    fixProcessor = System.Gadget.Settings.read("fixProcessor");
    if (fixProcessor != "") sProcessor = fixProcessor;
    else sProcessor = processor;
    DomUse = System.Gadget.Settings.read("DomUse");
    if (DomUse != "");
    else DomUse = "#fff62a";
    fixDomUse = System.Gadget.Settings.read("fixDomUse");
    if (fixDomUse != "") sDomUse = fixDomUse;
    else sDomUse = DomUse;
    UFT = System.Gadget.Settings.read("UFT");
    if (UFT != "");
    else UFT = "#ffffff";
    fixUFT = System.Gadget.Settings.read("fixUFT");
    if (fixUFT != "") sUFT = fixUFT;
    else sUFT = UFT;
    rams = System.Gadget.Settings.read("rams");
    if (rams != "");
    else rams = "#87cefa";
    fixRam = System.Gadget.Settings.read("fixRam");
    if (fixRam != "") sRam = fixRam;
    else sRam = rams;
    pagec = System.Gadget.Settings.read("pagec");
    if (pagec != "");
    else pagec = "#ffcc00";
    fixPagec = System.Gadget.Settings.read("fixPagec");
    if (fixPagec != "") sPagec = fixPagec;
    else sPagec = pagec;
    colorc1 = System.Gadget.Settings.read("colorc1");
    if (colorc1 != "");
    else colorc1 = "#90ee90";
    fixcolor1 = System.Gadget.Settings.read("fixcolor1");
    if (fixcolor1 != "") scolor[1] = fixcolor1;
    else scolor[1] = colorc1;
    colorc2 = System.Gadget.Settings.read("colorc2");
    if (colorc2 != "");
    else colorc2 = "#fff62a";
    fixcolor2 = System.Gadget.Settings.read("fixcolor2");
    if (fixcolor2 != "") scolor[2] = fixcolor2;
    else scolor[2] = colorc2;
    colorc3 = System.Gadget.Settings.read("colorc3");
    if (colorc3 != "");
    else colorc3 = "#faba00";
    fixcolor3 = System.Gadget.Settings.read("fixcolor3");
    if (fixcolor3 != "") scolor[3] = fixcolor3;
    else scolor[3] = colorc3;
    colorc4 = System.Gadget.Settings.read("colorc4");
    if (colorc4 != "");
    else colorc4 = "#ec7527";
    fixcolor4 = System.Gadget.Settings.read("fixcolor4");
    if (fixcolor4 != "") scolor[4] = fixcolor4;
    else scolor[4] = colorc4;
    colorc5 = System.Gadget.Settings.read("colorc5");
    if (colorc5 != "");
    else colorc5 = "#e5316c";
    fixcolor5 = System.Gadget.Settings.read("fixcolor5");
    if (fixcolor5 != "") scolor[5] = fixcolor5;
    else scolor[5] = colorc5;
    colorc6 = System.Gadget.Settings.read("colorc6");
    if (colorc6 != "");
    else colorc6 = "#e2003b";
    fixcolor6 = System.Gadget.Settings.read("fixcolor6");
    if (fixcolor6 != "") scolor[6] = fixcolor6;
    else scolor[6] = colorc6;
    colorc7 = System.Gadget.Settings.read("colorc7");
    if (colorc7 != "");
    else colorc7 = "#d7007a";
    fixcolor7 = System.Gadget.Settings.read("fixcolor7");
    if (fixcolor7 != "") scolor[7] = fixcolor7;
    else scolor[7] = colorc7;
    colorc8 = System.Gadget.Settings.read("colorc8");
    if (colorc8 != "");
    else colorc8 = "#ae3288";
    fixcolor8 = System.Gadget.Settings.read("fixcolor8");
    if (fixcolor8 != "") scolor[8] = fixcolor8;
    else scolor[8] = colorc8;
    colorc9 = System.Gadget.Settings.read("colorc9");
    if (colorc9 != "");
    else colorc9 = "#894b94";
    fixcolor9 = System.Gadget.Settings.read("fixcolor9");
    if (fixcolor9 != "") scolor[9] = fixcolor9;
    else scolor[9] = colorc9;
    colorc10 = System.Gadget.Settings.read("colorc10");
    if (colorc10 != "");
    else colorc10 = "#5b5099";
    fixcolor10 = System.Gadget.Settings.read("fixcolor10");
    if (fixcolor10 != "") scolor[10] = fixcolor10;
    else scolor[10] = colorc10;
    colorc11 = System.Gadget.Settings.read("colorc11");
    if (colorc11 != "");
    else colorc11 = "#4d61a8";
    fixcolor11 = System.Gadget.Settings.read("fixcolor11");
    if (fixcolor11 != "") scolor[11] = fixcolor11;
    else scolor[11] = colorc11;
    colorc12 = System.Gadget.Settings.read("colorc12");
    if (colorc12 != "");
    else colorc12 = "#4d81a2";
    fixcolor12 = System.Gadget.Settings.read("fixcolor12");
    if (fixcolor12 != "") scolor[12] = fixcolor12;
    else scolor[12] = colorc12;
    colorc13 = System.Gadget.Settings.read("colorc13");
    if (colorc13 != "");
    else colorc13 = "#4da0ae";
    fixcolor13 = System.Gadget.Settings.read("fixcolor13");
    if (fixcolor13 != "") scolor[13] = fixcolor13;
    else scolor[13] = colorc13;
    colorc14 = System.Gadget.Settings.read("colorc14");
    if (colorc14 != "");
    else colorc14 = "#4db896";
    fixcolor14 = System.Gadget.Settings.read("fixcolor14");
    if (fixcolor14 != "") scolor[14] = fixcolor14;
    else scolor[14] = colorc14;
    colorc15 = System.Gadget.Settings.read("colorc15");
    if (colorc15 != "");
    else colorc15 = "#64c566";
    fixcolor15 = System.Gadget.Settings.read("fixcolor15");
    if (fixcolor15 != "") scolor[15] = fixcolor15;
    else scolor[15] = colorc15;
    colorc16 = System.Gadget.Settings.read("colorc16");
    if (colorc16 != "");
    else colorc16 = "#79b63e";
    fixcolor16 = System.Gadget.Settings.read("fixcolor16");
    if (fixcolor16 != "") scolor[16] = fixcolor16;
    else scolor[16] = colorc16;
    colorc17 = System.Gadget.Settings.read("colorc17");
    if (colorc17 != "");
    else colorc17 = "#90ee90";
    fixcolor17 = System.Gadget.Settings.read("fixcolor17");
    if (fixcolor17 != "") scolor[17] = fixcolor17;
    else scolor[17] = colorc17;
    colorc18 = System.Gadget.Settings.read("colorc18");
    if (colorc18 != "");
    else colorc18 = "#fff62a";
    fixcolor18 = System.Gadget.Settings.read("fixcolor18");
    if (fixcolor18 != "") scolor[18] = fixcolor18;
    else scolor[18] = colorc18;
    colorc19 = System.Gadget.Settings.read("colorc19");
    if (colorc19 != "");
    else colorc19 = "#faba00";
    fixcolor19 = System.Gadget.Settings.read("fixcolor19");
    if (fixcolor19 != "") scolor[19] = fixcolor19;
    else scolor[19] = colorc19;
    colorc20 = System.Gadget.Settings.read("colorc20");
    if (colorc20 != "");
    else colorc20 = "#ec7527";
    fixcolor20 = System.Gadget.Settings.read("fixcolor20");
    if (fixcolor20 != "") scolor[20] = fixcolor20;
    else scolor[20] = colorc20;
    colorc21 = System.Gadget.Settings.read("colorc21");
    if (colorc21 != "");
    else colorc21 = "#e5316c";
    fixcolor21 = System.Gadget.Settings.read("fixcolor21");
    if (fixcolor21 != "") scolor[21] = fixcolor21;
    else scolor[21] = colorc21;
    colorc22 = System.Gadget.Settings.read("colorc22");
    if (colorc22 != "");
    else colorc22 = "#e2003b";
    fixcolor22 = System.Gadget.Settings.read("fixcolor22");
    if (fixcolor22 != "") scolor[22] = fixcolor22;
    else scolor[22] = colorc22;
    colorc23 = System.Gadget.Settings.read("colorc23");
    if (colorc23 != "");
    else colorc23 = "#d7007a";
    fixcolor23 = System.Gadget.Settings.read("fixcolor23");
    if (fixcolor23 != "") scolor[23] = fixcolor23;
    else scolor[23] = colorc23;
    colorc24 = System.Gadget.Settings.read("colorc24");
    if (colorc24 != "");
    else colorc24 = "#ae3288";
    fixcolor24 = System.Gadget.Settings.read("fixcolor24");
    if (fixcolor24 != "") scolor[24] = fixcolor24;
    else scolor[24] = colorc24;
    colorc25 = System.Gadget.Settings.read("colorc25");
    if (colorc25 != "");
    else colorc25 = "#894b94";
    fixcolor25 = System.Gadget.Settings.read("fixcolor25");
    if (fixcolor25 != "") scolor[25] = fixcolor25;
    else scolor[25] = colorc25;
    colorc26 = System.Gadget.Settings.read("colorc26");
    if (colorc26 != "");
    else colorc26 = "#5b5099";
    fixcolor26 = System.Gadget.Settings.read("fixcolor26");
    if (fixcolor26 != "") scolor[26] = fixcolor26;
    else scolor[26] = colorc26;
    colorc27 = System.Gadget.Settings.read("colorc27");
    if (colorc27 != "");
    else colorc27 = "#4d61a8";
    fixcolor27 = System.Gadget.Settings.read("fixcolor27");
    if (fixcolor27 != "") scolor[27] = fixcolor27;
    else scolor[27] = colorc27;
    colorc28 = System.Gadget.Settings.read("colorc28");
    if (colorc28 != "");
    else colorc28 = "#4d81a2";
    fixcolor28 = System.Gadget.Settings.read("fixcolor28");
    if (fixcolor28 != "") scolor[28] = fixcolor28;
    else scolor[28] = colorc28;
    colorc29 = System.Gadget.Settings.read("colorc29");
    if (colorc29 != "");
    else colorc29 = "#4da0ae";
    fixcolor29 = System.Gadget.Settings.read("fixcolor29");
    if (fixcolor29 != "") scolor[29] = fixcolor29;
    else scolor[29] = colorc29;
    colorc30 = System.Gadget.Settings.read("colorc30");
    if (colorc30 != "");
    else colorc30 = "#4db896";
    fixcolor30 = System.Gadget.Settings.read("fixcolor30");
    if (fixcolor30 != "") scolor[30] = fixcolor30;
    else scolor[30] = colorc30;
    colorc31 = System.Gadget.Settings.read("colorc31");
    if (colorc31 != "");
    else colorc31 = "#64c566";
    fixcolor31 = System.Gadget.Settings.read("fixcolor31");
    if (fixcolor31 != "") scolor[31] = fixcolor31;
    else scolor[31] = colorc31;
    colorc32 = System.Gadget.Settings.read("colorc32");
    if (colorc32 != "");
    else colorc32 = "#79b63e";
    fixcolor32 = System.Gadget.Settings.read("fixcolor32");
    if (fixcolor32 != "") scolor[32] = fixcolor32;
    else scolor[32] = colorc32;
    alertIcon = System.Gadget.Settings.read("alertIcon");
    if (alertIcon != "");
    else alertIcon = "#90EE90";
    fixAlertIcon = System.Gadget.Settings.read("fixAlertIcon");
    if (fixAlertIcon != "") sAlertIcon = fixAlertIcon;
    else sAlertIcon = alertIcon;
    sizeMode();
    timedMsg();
}
function loadfilesettings() {
    var fso = new ActiveXObject("Scripting.FileSystemObject");
    var inifilename = System.Environment.getEnvironmentVariable("APPDATA") + "\\" + System.
  Gadget.name + "V3_Settings.ini";
    try {
        var inifile = fso.OpenTextFile(inifilename, 1);
        try {
            var tmp = inifile.ReadLine();
            tmp = inifile.ReadLine();
            if (tmp != ";v4") throw "old";
            drawstyle = inifile.ReadLine();
            System.Gadget.Settings.write("drawstyle", drawstyle);
            graph = inifile.ReadLine();
            System.Gadget.Settings.write("graph", graph);
            fixsize = inifile.ReadLine();
            System.Gadget.Settings.write("fixsize", fixsize);
            ssize = inifile.ReadLine();
            System.Gadget.Settings.write("ssize", ssize);
            settimer = inifile.ReadLine();
            System.Gadget.Settings.write("settimer", settimer);
            update = inifile.ReadLine();
            System.Gadget.Settings.write("update", update);
            showIcon = inifile.ReadLine();
            System.Gadget.Settings.write("showIcon", showIcon);
            doubleClick = inifile.ReadLine();
            System.Gadget.Settings.write("doubleClick", doubleClick);
            cpuName = inifile.ReadLine();
            System.Gadget.Settings.write("cpuName", cpuName);
            clockFre = inifile.ReadLine();
            System.Gadget.Settings.write("clockFre", clockFre);
            showMem = inifile.ReadLine();
            System.Gadget.Settings.write("showMem", showMem);
            showMemBar = inifile.ReadLine();
            System.Gadget.Settings.write("showMemBar", showMemBar);
            showTem = inifile.ReadLine();
            System.Gadget.Settings.write("showTem", showTem);
            temperature = inifile.ReadLine();
            System.Gadget.Settings.write("temperature", temperature);
            showErr = inifile.ReadLine();
            System.Gadget.Settings.write("showErr", showErr);
            soundCPUTem = inifile.ReadLine();
            System.Gadget.Settings.write("soundCPUTem", soundCPUTem);
            soundCPUTemurl = inifile.ReadLine();
            System.Gadget.Settings.write("soundCPUTemurl", soundCPUTemurl);
            alertCPU1 = inifile.ReadLine();
            System.Gadget.Settings.write("alertCPU1", alertCPU1);
            alertCPUTem = inifile.ReadLine();
            System.Gadget.Settings.write("alertCPUTem", alertCPUTem);
            soundCPUTemVol = inifile.ReadLine();
            System.Gadget.Settings.write("soundCPUTemVol", soundCPUTemVol);
            soundCPUTemRepeats = inifile.ReadLine();
            System.Gadget.Settings.write("soundCPUTemRepeats", soundCPUTemRepeats);
            soundCPUTemCount = inifile.ReadLine();
            System.Gadget.Settings.write("soundCPUTemCount", soundCPUTemCount);
            backg = inifile.ReadLine();
            System.Gadget.Settings.write("backg", backg);
            fixBackg = inifile.ReadLine();
            System.Gadget.Settings.write("fixBackg", fixBackg);
            title = inifile.ReadLine();
            System.Gadget.Settings.write("title", title);
            fixTitle = inifile.ReadLine();
            System.Gadget.Settings.write("fixTitle", fixTitle);
            clocks = inifile.ReadLine();
            System.Gadget.Settings.write("clocks", clocks);
            fixClock = inifile.ReadLine();
            System.Gadget.Settings.write("fixClock", fixClock);
            rams = inifile.ReadLine();
            System.Gadget.Settings.write("rams", rams);
            fixRam = inifile.ReadLine();
            System.Gadget.Settings.write("fixRam", fixRam);
            processor = inifile.ReadLine();
            System.Gadget.Settings.write("processor", processor);
            fixProcessor = inifile.ReadLine();
            System.Gadget.Settings.write("fixProcessor", fixProcessor);
            colorc1 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc1", colorc1);
            fixcolor1 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor1", fixcolor1);
            colorc2 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc2", colorc2);
            fixcolor2 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor2", fixcolor2);
            colorc3 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc3", colorc3);
            fixcolor3 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor3", fixcolor3);
            colorc4 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc4", colorc4);
            fixcolor4 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor4", fixcolor4);
            colorc5 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc5", colorc5);
            fixcolor5 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor5", fixcolor5);
            colorc6 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc6", colorc6);
            fixcolor6 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor6", fixcolor6);
            colorc7 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc7", colorc7);
            fixcolor7 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor7", fixcolor7);
            colorc8 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc8", colorc8);
            fixcolor8 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor8", fixcolor8);
            colorc9 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc9", colorc9);
            fixcolor9 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor9", fixcolor9);
            colorc10 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc10", colorc10);
            fixcolor10 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor10", fixcolor10);
            colorc11 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc11", colorc11);
            fixcolor11 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor11", fixcolor11);
            colorc12 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc12", colorc12);
            fixcolor12 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor12", fixcolor12);
            colorc13 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc13", colorc13);
            fixcolor13 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor13", fixcolor13);
            colorc14 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc14", colorc14);
            fixcolor14 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor14", fixcolor14);
            colorc15 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc15", colorc15);
            fixcolor15 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor15", fixcolor15);
            colorc16 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc16", colorc16);
            fixcolor16 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor16", fixcolor16);
            colorc17 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc17", colorc17);
            fixcolor17 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor17", fixcolor17);
            colorc18 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc18", colorc18);
            fixcolor18 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor18", fixcolor18);
            colorc19 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc19", colorc19);
            fixcolor19 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor19", fixcolor19);
            colorc20 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc20", colorc20);
            fixcolor20 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor20", fixcolor20);
            colorc21 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc21", colorc21);
            fixcolor21 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor21", fixcolor21);
            colorc22 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc22", colorc22);
            fixcolor22 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor22", fixcolor22);
            colorc23 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc23", colorc23);
            fixcolor23 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor23", fixcolor23);
            colorc24 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc24", colorc24);
            fixcolor24 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor24", fixcolor24);
            colorc25 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc25", colorc25);
            fixcolor25 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor25", fixcolor25);
            colorc26 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc26", colorc26);
            fixcolor26 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor26", fixcolor26);
            colorc27 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc27", colorc27);
            fixcolor27 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor27", fixcolor27);
            colorc28 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc28", colorc28);
            fixcolor28 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor28", fixcolor28);
            colorc29 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc29", colorc29);
            fixcolor29 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor29", fixcolor29);
            colorc30 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc30", colorc30);
            fixcolor30 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor30", fixcolor30);
            colorc31 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc31", colorc31);
            fixcolor31 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor31", fixcolor31);
            colorc32 = inifile.ReadLine();
            System.Gadget.Settings.write("colorc32", colorc32);
            fixcolor32 = inifile.ReadLine();
            System.Gadget.Settings.write("fixcolor32", fixcolor32);
            size = inifile.ReadLine();
            System.Gadget.Settings.write("size", size);
            alertIcon = inifile.ReadLine();
            System.Gadget.Settings.write("alertIcon", alertIcon);
            fixAlertIcon = inifile.ReadLine();
            System.Gadget.Settings.write("fixAlertIcon", fixAlertIcon);
            FlyoutBac = inifile.ReadLine();
            System.Gadget.Settings.write("FlyoutBac", FlyoutBac);
            fixFlyoutBac = inifile.ReadLine();
            System.Gadget.Settings.write("fixFlyoutBac", fixFlyoutBac);
            FlyoutTit = inifile.ReadLine();
            System.Gadget.Settings.write("FlyoutTit", FlyoutTit);
            fixFlyoutTit = inifile.ReadLine();
            System.Gadget.Settings.write("fixFlyoutTit", fixFlyoutTit);
            FlyoutDet = inifile.ReadLine();
            System.Gadget.Settings.write("FlyoutDet", FlyoutDet);
            fixFlyoutDet = inifile.ReadLine();
            System.Gadget.Settings.write("fixFlyoutDet", fixFlyoutDet);
            showPageFile = inifile.ReadLine();
            System.Gadget.Settings.write("showPageFile", showPageFile);
            showPageFileBar = inifile.ReadLine();
            System.Gadget.Settings.write("showPageFileBar", showPageFileBar);
            showPageFileGraph = inifile.ReadLine();
            System.Gadget.Settings.write("showPageFileGraph", showPageFileGraph);
            showMemGraph = inifile.ReadLine();
            System.Gadget.Settings.write("showMemGraph", showMemGraph);
            UFT = inifile.ReadLine();
            System.Gadget.Settings.write("UFT", UFT);
            fixUFT = inifile.ReadLine();
            System.Gadget.Settings.write("fixUFT", fixUFT);
            pagec = inifile.ReadLine();
            System.Gadget.Settings.write("pagec", pagec);
            fixPagec = inifile.ReadLine();
            System.Gadget.Settings.write("fixPagec", fixPagec);
            showUsername = inifile.ReadLine();
            System.Gadget.Settings.write("showUsername", showUsername);
            DomUse = inifile.ReadLine();
            System.Gadget.Settings.write("DomUse", DomUse);
            fixDomUse = inifile.ReadLine();
            System.Gadget.Settings.write("fixDomUse", fixDomUse);
        }
        finally {
            inifile.Close();
        }
    }
    catch (err) {
    }
    var Startup = 1;
    System.Gadget.Settings.write("Startup", Startup);
    loadSettings();
}
function onTimer() {
    refreshdisplay();
    if (clockFre == 1) {
        refreshdisplay1();
    }
    else if (clockFre == 3) {
        refreshdisplay3();
    }
    refreshdisplay2();
    updateHist();
    if (graph == 1) {
        updateGraph();
    }
}
function initMain() {
    try {
        var Win32Processor = wmiService.ExecQuery("SELECT Manufacturer FROM Win32_Processor");
        manufacturerCPU = (new Enumerator(Win32Processor)).item().Manufacturer;
        howCPU = manufacturerCPU.indexOf("AMD");
        if (showIcon == 1) {
            if (howCPU == -1) {
                O('IntelCPU').style.visibility = "visible";
                O('AMDCPU').style.visibility = "hidden";
                O('IconCPU').style.visibility = "hidden";
            }
            else {
                O('IntelCPU').style.visibility = "hidden";
                O('AMDCPU').style.visibility = "visible";
                O('IconCPU').style.visibility = "hidden";
            }
        }
        else {
            O('IntelCPU').style.visibility = "hidden";
            O('AMDCPU').style.visibility = "hidden";
            O('IconCPU').style.visibility = "visible";
        }
        O('cpuModel').innerHTML = System.Machine.CPUs.item(0).name.replace("Intel", "").
    replace("AMD", "").replace("(R)", "").replace("(TM)", "").replace("(tm)", "").replace(
    " @", "").replace(" Processor", "").replace(" CPU", "").replace("Mobile ", "").replace
    ("Genuine ", "").replace("Dual Core ", "").replace("Technology ", "").replace("Core 2"
    , "Core2");
    }
    catch (err) {
        O('cpuModel').innerHTML = "";
        O('clock').style.visibility = "hidden";
    }
    if (howCPU == -1) {
        CPUPro = "intelcpu"
    }
    else {
        CPUPro = "amdcpu";
        oneTempSensor = 1;
    }
    try {
        var getValue = wmiService.ExecQuery(
    "SELECT NumberOfProcessors FROM Win32_ComputerSystem");
        numOfPro = (new Enumerator(getValue)).item().NumberOfProcessors;
    }
    catch (err) {
        numOfPro = 1;
    }
    try {
        var getValue = wmiService.ExecQuery(
    "SELECT NumberOfCores FROM Win32_Processor Where DeviceID='CPU0'");
        CPU0NC = (new Enumerator(getValue)).item().NumberOfCores;
    }
    catch (err) {
        CPU0NC = 0;
    }
    coreThread = parseInt(coreCount / CPU0NC);
    coreTemLoad = Math.round(coreCount / coreThread);
    totalMemory = System.Machine.totalMemory;
    readRamTotal = parseInt(totalMemory);
    ramTotalGB = readRamTotal / 1024;
    if ((Math.round(ramTotalGB) - ramTotalGB) < 0.05 && (Math.round(ramTotalGB) - ramTotalGB
  ) > -0.05) {
        O('totalValue').innerHTML = Math.round(ramTotalGB) + "GB";
    }
    else {
        O('totalValue').innerHTML = ramTotalGB.toFixed(1) + "GB";
    }
}
function updateHist() {
    var t = 0;
    totalUsage = 0;
    for (var i = 0; i < coreCount; i++) {
        coreUsage[t] = parseInt(System.Machine.CPUs.item(i).usagePercentage);
        if (coreUsage[t] < 0) coreUsage[t] = 0;
        totalUsage += parseInt(coreUsage[t]);
        t++;
    }
    totalUsage = Math.round(totalUsage / coreCount);
    availableMemory = System.Machine.availableMemory;
    readRamPerc = parseInt(100 - Math.round(availableMemory / totalMemory * 100));
    readRamFree = parseInt(availableMemory);
    readRamUsage = parseInt(Math.round(totalMemory - availableMemory));
    if (readRamPerc <= 0) readRamPerc = 0;
    if (readRamTotal <= 0) readRamTotal = 0;
    if (readRamFree <= 0) readRamFree = 0;
    if (readRamUsage <= 0) readRamUsage = 0;
    if (ramHist.push(readRamPerc) > maxHist) {
        ramHist.shift();
    }
    for (var i = 0; i < coreCount; i++) {
        if (coreHist[i + 1].push(coreUsage[i]) > maxHist) {
            coreHist[i + 1].shift();
        }
    }
    if (showPageFile == 1 || showPageFileBar == 1 || showPageFileGraph == 1) {
        pageFile();
    }
}
function pageFile() {
    var totalPage = 0;
    var usagePage = 0;
    try {
        var Win32PageFileUsage = wmiService.ExecQuery(
    "SELECT AllocatedBaseSize,CurrentUsage FROM Win32_PageFileUsage");
        var pageFileItems = new Enumerator(Win32PageFileUsage);
        for (var i = 0; !pageFileItems.atEnd();
    pageFileItems.moveNext()) {
            var objItem = pageFileItems.item();
            totalPagefile[i] = objItem.AllocatedBaseSize;
            usagePagefile[i] = objItem.CurrentUsage;
            var totalPage = totalPage + totalPagefile[i];
            var usagePage = usagePage + usagePagefile[i];
            i++;
        }
        if (totalPage > 0) {
            readPageTotal = parseInt(totalPage);
            readPageFree = parseInt(Math.round(totalPage - usagePage));
            readPageUsage = parseInt(usagePage);
            readPagePerc = parseInt(Math.round(readPageUsage / totalPage * 100));
            if (pageHist.push(readPagePerc) > maxHist) {
                pageHist.shift();
            }
        }
    }
    catch (err) {
    }
}
function fourdigits(val) {
    if (val > 1000) return val.toFixed(0);
    else if (val > 100) return val.toFixed(1);
    else if (val > 10) return val.toFixed(2);
    else return val.toFixed(3);
}
function formatBytes(bytes) {
    if (bytes >= 10000) return fourdigits(bytes / 1024) + "G";
    return (bytes) + "M";
}
function refreshdisplay() {
    O('usgTotal').innerHTML = totalUsage + "%";
    if (showTem == 1) {
        pcmeterMethod();
    }
    else if (showTem == 3) {
        coretempMethod();
    }
    if (alertCPU1 == 1) {
        try {
            CoreTemp.Refresh();
            CPUTemperature = (CoreTemp.Fahrenheit ? (((parseInt(CoreTemp.GetTemp(0)) - 32) * 5) /
      9).toFixed(0) : parseInt(CoreTemp.GetTemp(0)));
            O('coreTempErr').innerHTML = "";
        }
        catch (err) {
            try {
                WMI = GetObject("winmgmts:\\\\.\\root\\AddGadgets");
                var getValue = WMI.ExecQuery("SELECT Value FROM Sensor Where DeviceId=" + "'/" +
        CPUPro + "/0/temperature/0'" + "");
                CPUTemperature = (new Enumerator(getValue)).item().Value;
                O('coreTempErr').innerHTML = "";
            }
            catch (err) {
                CPUTemperature = 0;
                O('coreTempErr').innerHTML = "PC Meter or Core Temp not running!";
            }
        }
        if (CPUTemperature >= alertCPUTem) {
            if (Player.controls.isAvailable('Stop')) {
            }
            else {
                if (soundCPUTem == 50) {
                    Player.URL = soundCPUTemurl;
                    Player.settings.volume = soundCPUTemVol;
                    Player.Controls.play();
                    if (soundCPUTemRepeats == 1) {
                        Player.Settings.setMode("loop", true)
                    }
                    else if (soundCPUTemRepeats == 3) {
                        Player.Settings.playCount = soundCPUTemCount;
                    }
                    alertIconRed();
                }
                else {
                    Player.URL = System.Gadget.path + "\\alarm" + soundCPUTem + ".mp3";
                    Player.settings.volume = soundCPUTemVol;
                    Player.Controls.play();
                    if (soundCPUTemRepeats == 1) {
                        Player.Settings.setMode("loop", true)
                    }
                    else if (soundCPUTemRepeats == 3) {
                        Player.Settings.playCount = soundCPUTemCount;
                    }
                    alertIconRed();
                }
            }
        }
    }
}
function alertIconRed() {
    if (Player.controls.isAvailable('Stop')) {
        O('iconAlert').style.color = "#ff0033";
        setTimeout("alertIconGreen()", 500);
    }
}
function alertIconGreen() {
    O('iconAlert').style.color = sAlertIcon;
    setTimeout("alertIconRed()", 500);
}
function coretempMethod() {
    try {
        CoreTemp.Refresh();
    }
    catch (err) {
    }
    try {
        var y = 0;
        var tempIdx = 0;
        // Check if core count matches temp count
        var smtCores = CoreTemp.GetTemp(coreTemLoad - 1) === 0;
        for (var i = 0; i < coreTemLoad; i++) {
            for (var x = 0; x < coreThread; x++) {
                // List same temp for paired logical cores when SMT enabled
                tempIdx = smtCores ? Math.floor(i / 2) : i;
                temp[y] = parseInt(CoreTemp.GetTemp(tempIdx));
                loadTem[y] = CoreTemp.GetLoad(i);
                y++
            }
        }
        if (temperature == 2) {
            for (var i = 0; i < coreCount; i++) {
                O('tmpCore' + (i + 1)).innerHTML = (temp[i] == 0) ? "Core " + (i + 1) : (i + 1) +
        "[" + (CoreTemp.Fahrenheit ? temp[i] : (((temp[i] * 9) / 5) + 32).toFixed(0)) +
        "F]";
            }
        }
        else {
            for (var i = 0; i < coreCount; i++) {
                O('tmpCore' + (i + 1)).innerHTML = (temp[i] == 0) ? "Core " + (i + 1) : (i + 1) +
        "[" + (CoreTemp.Fahrenheit ? (((temp[i] - 32) * 5) / 9).toFixed(0) : temp[i]) +
        "C]";
            }
        }
        O('coreTempErr').innerHTML = "";
    }
    catch (err) {
        for (var t = 1; t < coreCount + 1; t++) {
            O('tmpCore' + t).innerHTML = "Core " + t;
        }
        O('coreTempErr').innerHTML = "CoreTemp is not running!";
    }
}
function pcmeterMethod() {
    try {
        WMI = GetObject("winmgmts:\\\\.\\root\\AddGadgets");
    }
    catch (err) {
    }
    if (oneTempSensor == 1) {
        if (numOfPro >= 2) {
            if (temperature == 2) {
                try {
                    var getValue = WMI.ExecQuery("SELECT Value FROM Sensor Where DeviceId=" + "'/" +
          CPUPro + "/0/temperature/0'" + "");
                    temp[0] = "[" + ((((new Enumerator(getValue)).item().Value * 9) / 5) + 32).
          toFixed(0) + "F]";
                }
                catch (err) {
                    temp[0] = "[0F]";
                }
                try {
                    var getValue = WMI.ExecQuery("SELECT Value FROM Sensor Where DeviceId=" + "'/" +
          CPUPro + "/1/temperature/0'" + "");
                    temp[8] = "[" + ((((new Enumerator(getValue)).item().Value * 9) / 5) + 32).
          toFixed(0) + "F]";
                }
                catch (err) {
                    temp[8] = "[0F]";
                }
            }
            else {
                try {
                    var getValue = WMI.ExecQuery("SELECT Value FROM Sensor Where DeviceId=" + "'/" +
          CPUPro + "/0/temperature/0'" + "");
                    temp[0] = "[" + ((new Enumerator(getValue)).item().Value).toFixed(0) + "C]";
                }
                catch (err) {
                    temp[0] = "[0C]";
                }
                try {
                    var getValue = WMI.ExecQuery("SELECT Value FROM Sensor Where DeviceId=" + "'/" +
          CPUPro + "/1/temperature/0'" + "");
                    temp[8] = "[" + ((new Enumerator(getValue)).item().Value).toFixed(0) + "C]";
                }
                catch (err) {
                    temp[8] = "[0C]";
                }
            }
            var z = parseInt(coreCount / 2);
            try {
                for (var i = 1; i < z + 1; i++) {
                    O('tmpCore' + i).innerHTML = i + temp[0];
                    O('tmpCore' + (i + z)).innerHTML = (i + z) + temp[8];
                }
            }
            catch (err) {
            }
        }
        else {
            if (temperature == 2) {
                try {
                    var getValue = WMI.ExecQuery("SELECT Value FROM Sensor Where DeviceId=" + "'/" +
          CPUPro + "/0/temperature/0'" + "");
                    temp[0] = "[" + ((((new Enumerator(getValue)).item().Value * 9) / 5) + 32).
          toFixed(0) + "F]";
                }
                catch (err) {
                    temp[0] = "[0F]";
                }
            }
            else {
                try {
                    var getValue = WMI.ExecQuery("SELECT Value FROM Sensor Where DeviceId=" + "'/" +
          CPUPro + "/0/temperature/0'" + "");
                    temp[0] = "[" + ((new Enumerator(getValue)).item().Value).toFixed(0) + "C]";
                }
                catch (err) {
                    temp[0] = "[0C]";
                }
            }
            try {
                for (var i = 1; i < coreCount + 1; i++) {
                    O('tmpCore' + i).innerHTML = i + temp[0];
                }
            }
            catch (err) {
            }
        }
    }
    else {
        if (numOfPro >= 2) {
            if (temperature == 2) {
                for (var i = 0; i < CPU0NC; i++) {
                    try {
                        var getValue = WMI.ExecQuery("SELECT Value FROM Sensor Where DeviceId=" + "'/"
             + CPUPro + "/0/temperature/" + i + "'" + "");
                        temp[i] = "[" + ((((new Enumerator(getValue)).item().Value * 9) / 5) + 32).
            toFixed(0) + "F]";
                    }
                    catch (err) {
                        temp[i] = "[0F]";
                    }
                    try {
                        var getValue = WMI.ExecQuery("SELECT Value FROM Sensor Where DeviceId=" + "'/"
             + CPUPro + "/1/temperature/" + i + "'" + "");
                        temp[i + 8] = "[" + ((((new Enumerator(getValue)).item().Value * 9) / 5) + 32
            ).toFixed(0) + "F]";
                    }
                    catch (err) {
                        temp[i + 8] = "[0F]";
                    }
                }
            }
            else {
                for (var i = 0; i < CPU0NC; i++) {
                    try {
                        var getValue = WMI.ExecQuery("SELECT Value FROM Sensor Where DeviceId=" + "'/"
             + CPUPro + "/0/temperature/" + i + "'" + "");
                        temp[i] = "[" + ((new Enumerator(getValue)).item().Value).toFixed(0) + "C]";
                    }
                    catch (err) {
                        temp[i] = "[0C]";
                    }
                    try {
                        var getValue = WMI.ExecQuery("SELECT Value FROM Sensor Where DeviceId=" + "'/"
             + CPUPro + "/1/temperature/" + i + "'" + "");
                        temp[i + 8] = "[" + ((new Enumerator(getValue)).item().Value).toFixed(0) +
            "C]";
                    }
                    catch (err) {
                        temp[i + 8] = "[0C]";
                    }
                }
            }
            if (coreCount <= 6) {
                try {
                    for (var i = 0; i < CPU0NC; i++) {
                        O('tmpCore' + (i + 1)).innerHTML = (i + 1) + temp[i];
                        O('tmpCore' + (i + 1 + CPU0NC)).innerHTML = (i + 1 + CPU0NC) + temp[i + 8];
                    }
                }
                catch (err) {
                }
            }
            else {
                try {
                    var y = 0;
                    for (var i = 0; i < CPU0NC; i++) {
                        for (var x = 0; x < 2; x++) {
                            O('tmpCore' + (y + 1)).innerHTML = (y + 1) + temp[i];
                            O('tmpCore' + (y + 1 + CPU0NC)).innerHTML = (y + 1 + CPU0NC) + temp[i + 8];
                            y++
                        }
                    }
                }
                catch (err) {
                }
            }
        }
        else {
            if (temperature == 2) {
                for (var i = 0; i < CPU0NC; i++) {
                    try {
                        var getValue = WMI.ExecQuery("SELECT Value FROM Sensor Where DeviceId=" + "'/"
             + CPUPro + "/0/temperature/" + i + "'" + "");
                        temp[i] = "[" + ((((new Enumerator(getValue)).item().Value * 9) / 5) + 32).
            toFixed(0) + "F]";
                    }
                    catch (err) {
                        temp[i] = "[0F]";
                    }
                }
            }
            else {
                for (var i = 0; i < CPU0NC; i++) {
                    try {
                        var getValue = WMI.ExecQuery("SELECT Value FROM Sensor Where DeviceId=" + "'/"
             + CPUPro + "/0/temperature/" + i + "'" + "");
                        temp[i] = "[" + ((new Enumerator(getValue)).item().Value).toFixed(0) + "C]";
                    }
                    catch (err) {
                        temp[i] = "[0C]";
                    }
                }
            }
            if (coreThread == 2) {
                try {
                    var y = 0;
                    for (var i = 0; i < CPU0NC; i++) {
                        for (var x = 0; x < 2; x++) {
                            O('tmpCore' + (y + 1)).innerHTML = (y + 1) + temp[i];
                            y++
                        }
                    }
                }
                catch (err) {
                }
            }
            else {
                try {
                    for (var i = 0; i < coreCount; i++) {
                        O('tmpCore' + (i + 1)).innerHTML = (i + 1) + temp[i];
                    }
                }
                catch (err) {
                }
            }
        }
    }
    if (temperature == 2) {
        if (temp[0] == "[0F]") {
            O('coreTempErr').innerHTML = "PC Meter is not running!";
        }
        else {
            O('coreTempErr').innerHTML = "";
        }
    }
    else {
        if (temp[0] == "[0C]") {
            O('coreTempErr').innerHTML = "PC Meter is not running!";
        }
        else {
            O('coreTempErr').innerHTML = "";
        }
    }
}
function refreshdisplay1() {
    try {
        var Win32Processor1 = wmiService.ExecQuery(
    "SELECT CurrentClockSpeed FROM Win32_Processor");
        freqCPU = new Enumerator(Win32Processor1).item().CurrentClockSpeed;
        O('cpuSpeed').innerHTML = freqCPU + "MHz";
    }
    catch (err) {
        O('cpuSpeed').innerHTML = "0MHz";
    }
}
function refreshdisplay3() {
    try {
        WMI = GetObject("winmgmts:\\\\.\\root\\AddGadgets");
        var getValue = WMI.ExecQuery(
    "SELECT Value FROM Sensor Where DeviceId='/intelcpu/0/clock/1'");
        freqCPU = Math.round((new Enumerator(getValue)).item().Value);
        O('cpuSpeed').innerHTML = freqCPU + "MHz";
        O('coreTempErr').innerHTML = "";
    }
    catch (err) {
        O('cpuSpeed').innerHTML = "0MHz";
        O('coreTempErr').innerHTML = "PC Meter is not running!";
    }
}
function clickAlert() {
    if (alertCPU1 == 1) {
        if (Player.controls.isAvailable('Stop')) {
            Player.controls.stop();
        }
        else {
            O('iconAlert').style.color = "#808080";
            alertCPU1 = "2";
            System.Gadget.Settings.write("alertCPU1", 2);
        }
        O('coreTempErr').innerHTML = "";
    }
    else {
        O('iconAlert').style.color = sAlertIcon;
        alertCPU1 = "1";
        System.Gadget.Settings.write("alertCPU1", 1);
    }
}
function refreshdisplay2() {
    O('freeValue').innerHTML = formatBytes(readRamFree) + "B";
    O('usedValue').innerHTML = formatBytes(readRamUsage) + "B";
    O('barRam').style.width = parseInt(0.5 * readRamPerc * size);
    O('ramUsage').innerHTML = readRamPerc + "%";
    O('totalPageValue').innerHTML = Math.round(readPageTotal / 1024) + "GB";
    O('freePageValue').innerHTML = formatBytes(readPageFree) + "B";
    O('usedPageValue').innerHTML = formatBytes(readPageUsage) + "B";
    O('barPage').style.width = parseInt(0.5 * readPagePerc * size);
    if (readPagePerc == 0) {
        O('barPage').style.visibility = "hidden";
    }
    else {
        O('barPage').style.visibility = "visible";
    }
    O('pageUsage').innerHTML = readPagePerc + "%";
    for (var i = 0; i < coreCount; i++) {
        O('usgCore' + [i + 1]).innerHTML = coreUsage[i] + "%";
        if (coreUsage[i] == 0) {
            O('bar' + [i + 1]).style.visibility = "hidden";
        }
        else {
            O('bar' + [i + 1]).style.visibility = "visible";
        }
        O('bar' + [i + 1]).style.width = parseInt(0.5 * coreUsage[i] * size);
    }
}
function initHist() {
    for (var i = 0; i < maxHist; i++) {
        ramHist[i] = 0;
        pageHist[i] = 0;
        for (var t = 1; t < coreCount + 1; t++) {
            coreHist[t][i] = 0;
        }
    }
}
function updateGraph() {
    var rampath = "m 0,100 ";
    var pagepath = "m 0,100 ";
    for (var t = 1; t < coreCount + 1; t++) {
        cpath[t] = "m 0,100 ";
    }
    var cram = 0;
    var cpage = 0;
    for (var t = 1; t < coreCount + 1; t++) {
        c[t] = 0;
    }
    var param = "";
    var papage = "";
    for (var i = 0; i < maxHist; i++) {
        cram = parseInt(ramHist[i]);
        cpage = parseInt(pageHist[i]);
        for (var t = 1; t < coreCount + 1; t++) {
            c[t] = parseInt(coreHist[t][i]);
            if (drawstyle == 2) {
                param = (cram == 0) ? "m" : "l";
                rampath += " m " + parseInt(i + 1) + ",100 " + param + " " + parseInt(i + 1) + ","
         + parseInt(100 - (cram)) + "";
                papage = (cpage == 0) ? "m" : "l";
                pagepath += " m " + parseInt(i + 1) + ",100 " + papage + " " + parseInt(i + 1) +
        "," + parseInt(100 - (cpage)) + "";
                param = (c[t] == 0) ? "m" : "l";
                cpath[t] += " m " + parseInt(i + 1) + ",100 " + param + " " + parseInt(i + 1) +
        "," + parseInt(100 - (c[t])) + "";
            }
            else {
                rampath += " l " + parseInt(i + 1) + "," + parseInt(100 - (cram));
                pagepath += " l " + parseInt(i + 1) + "," + parseInt(100 - (cpage));
                cpath[t] += " l " + parseInt(i + 1) + "," + parseInt(100 - (c[t]));
            }
        }
    }
    rampath = rampath + " e";
    pagepath = pagepath + " e";
    for (var t = 1; t < coreCount + 1; t++) {
        cpath[t] = cpath[t] + " e";
    }
    O('ramgraph').path = rampath;
    O('pagegraph').path = pagepath;
    for (var t = 1; t < coreCount + 1; t++) {
        O('coregraph' + t).path = cpath[t];
    }
}
function sizeMode() {
    if (showErr == 1) {
        O('coreTempErr').style.visibility = "visible";
    }
    else {
        O('coreTempErr').style.visibility = "hidden";
    }
    if (showTem == 2) {
        for (var t = 1; t < coreCount + 1; t++) {
            O('tmpCore' + t).innerHTML = "Core " + t;
        }
        O('coreTempErr').innerHTML = "";
    }
    if (clockFre == 2) {
        O('clock').style.visibility = "hidden";
        O('cpuSpeed').style.visibility = "hidden";
    }
    else {
        O('clock').style.visibility = "visible";
        O('cpuSpeed').style.visibility = "visible";
    }
    if (cpuName == 2) {
        sizeChange = 0;
        O('cpuModel').style.visibility = "hidden";
    }
    else {
        sizeChange = 10;
        O('cpuModel').style.visibility = "visible";
    }
    if (showUsername == 4) {
        sizeChange8 = 0;
        O('username').style.visibility = "hidden";
    }
    else {
        try {
            var Win32ComputerSystem = wmiService.ExecQuery(
      "SELECT Name, UserName FROM Win32_ComputerSystem");
            getUserName = (new Enumerator(Win32ComputerSystem)).item().UserName;
            getName = (new Enumerator(Win32ComputerSystem)).item().Name;
            if (showUsername == 2) {
                O('username').innerHTML = getName;
            }
            else if (showUsername == 3) {
                O('username').innerHTML = getUserName.replace(getName + "\\", "");
            }
            else {
                O('username').innerHTML = getUserName;
            }
        }
        catch (err) {
        }
        sizeChange8 = 10;
        O('username').style.visibility = "visible";
    }
    if (showMem == 1 || showPageFile == 1) {
        sizeChange2 = 10;
        O('used').style.visibility = "visible";
        O('free').style.visibility = "visible";
        O('total').style.visibility = "visible";
    }
    else {
        sizeChange2 = 0;
        O('used').style.visibility = "hidden";
        O('free').style.visibility = "hidden";
        O('total').style.visibility = "hidden";
    }
    if (showMem == 1) {
        sizeChange3 = 10;
        O('usedValue').style.visibility = "visible";
        O('freeValue').style.visibility = "visible";
        O('totalValue').style.visibility = "visible";
    }
    else {
        sizeChange3 = 0;
        O('usedValue').style.visibility = "hidden";
        O('freeValue').style.visibility = "hidden";
        O('totalValue').style.visibility = "hidden";
    }
    if (showMemBar == 1) {
        sizeChange5 = 0;
        O('backRam').style.visibility = "visible";
        O('barRam').style.visibility = "visible";
        O('topRam').style.visibility = "visible";
        O('ram').style.visibility = "visible";
        O('ramUsage').style.visibility = "visible";
    }
    else {
        sizeChange5 = 10;
        O('backRam').style.visibility = "hidden";
        O('barRam').style.visibility = "hidden";
        O('topRam').style.visibility = "hidden";
        O('ram').style.visibility = "hidden";
        O('ramUsage').style.visibility = "hidden";
    }
    if (showPageFile == 1) {
        sizeChange6 = 10;
        O('usedPageValue').style.visibility = "visible";
        O('freePageValue').style.visibility = "visible";
        O('totalPageValue').style.visibility = "visible";
    }
    else {
        sizeChange6 = 0;
        O('usedPageValue').style.visibility = "hidden";
        O('freePageValue').style.visibility = "hidden";
        O('totalPageValue').style.visibility = "hidden";
    }
    if (showPageFileBar == 1) {
        sizeChange7 = 10;
        O('backPage').style.visibility = "visible";
        O('barPage').style.visibility = "visible";
        O('topPage').style.visibility = "visible";
        O('page').style.visibility = "visible";
        O('pageUsage').style.visibility = "visible";
    }
    else {
        sizeChange7 = 0;
        O('backPage').style.visibility = "hidden";
        O('barPage').style.visibility = "hidden";
        O('topPage').style.visibility = "hidden";
        O('page').style.visibility = "hidden";
        O('pageUsage').style.visibility = "hidden";
    }
    if (graph == 2) {
        sizeChange4 = 35;
        O('linesx').style.visibility = "hidden";
        O('linesy').style.visibility = "hidden";
        O('border').style.visibility = "hidden";
        O('ramgraph').style.visibility = "hidden";
        O('pagegraph').style.visibility = "hidden";
        for (var t = 1; t < coreCount + 1; t++) {
            O('coregraph' + t).style.visibility = "hidden";
        }
    }
    else {
        sizeChange4 = 0;
        if (showMemGraph == 1) {
            O('ramgraph').style.visibility = "visible";
        }
        else {
            O('ramgraph').style.visibility = "hidden";
        }
        if (showPageFileGraph == 1) {
            O('pagegraph').style.visibility = "visible";
        }
        else {
            O('pagegraph').style.visibility = "hidden";
        }
        O('linesx').style.visibility = "visible";
        O('linesy').style.visibility = "visible";
        O('border').style.visibility = "visible";
        for (var t = 1; t < coreCount + 1; t++) {
            O('coregraph' + t).style.visibility = "visible";
        }
    }
    for (var t = coreCount + 1; t < maxCoreCount; t++) {
        O('tmpCore' + t).style.visibility = "hidden";
        O('usgCore' + t).style.visibility = "hidden";
        O('bar' + t).style.visibility = "hidden";
        O('back' + t).style.visibility = "hidden";
        O('top' + t).style.visibility = "hidden";
    }
    allSizeChange = parseInt(sizeChange + sizeChange2 + sizeChange3 - sizeChange5 +
  sizeChange6 + sizeChange7 + sizeChange8);
    allHeight = parseInt(80 + sizeUpdate + allSizeChange - sizeChange4 + (10 * coreCount));
    if (allHeight >= 430) {
        background.src = "bg_440.png";
    }
    else if (allHeight >= 400) {
        background.src = "bg_410.png";
    }
    else if (allHeight >= 370) {
        background.src = "bg_380.png";
    }
    else if (allHeight >= 340) {
        background.src = "bg_350.png";
    }
    else if (allHeight >= 310) {
        background.src = "bg_320.png";
    }
    else if (allHeight >= 280) {
        background.src = "bg_290.png";
    }
    else if (allHeight >= 250) {
        background.src = "bg_260.png";
    }
    else if (allHeight >= 220) {
        background.src = "bg_230.png";
    }
    else if (allHeight >= 190) {
        background.src = "bg_200.png";
    }
    else if (allHeight >= 160) {
        background.src = "bg_170.png";
    }
    else if (allHeight >= 130) {
        background.src = "bg_140.png";
    }
    else {
        background.src = "bg_110.png";
    }
    document.body.style.height = Math.round(allHeight * size);
    background.style.height = Math.round(allHeight * size);
    var btop = parseInt((43 + allSizeChange + (10 * coreCount)) * size);
    O('newUpdate').style.top = parseInt((73 + allSizeChange - sizeChange4 + (10 * coreCount
  )) * size);
    document.body.style.width = Math.round(130 * size);
    background.style.width = Math.round(130 * size);
    O('backgc').style.top = Math.round(3 * size);
    O('backgc').style.left = Math.round(3 * size);
    O('backgc').style.width = Math.round(124 * size);
    O('backgc').style.height = Math.round((allHeight - 6) * size);
    O('backgc').style.background = sBackg;
    O('blackwhite').style.top = Math.round(3 * size);
    O('blackwhite').style.left = Math.round(3 * size);
    O('blackwhite').style.width = Math.round(124 * size);
    O('blackwhite').style.height = Math.round((allHeight - 6) * size);
    document.body.style.fontSize = parseInt(9 * size);
    var bheight = parseInt(30 * size);
    var bwidth = parseInt(116 * size);
    var bleft = parseInt(9 * size);
    var gbleft = parseInt(7 * size);
    var backwidth = parseInt(50 * size);
    var backheight = parseInt(6 * size);
    var tleft = parseInt(64 * size);
    var uwidth = parseInt(120 * size);
    O('linesx').style.top = btop;
    O('linesy').style.top = btop;
    O('ramgraph').style.top = btop;
    O('pagegraph').style.top = btop;
    for (var t = 1; t < coreCount + 1; t++) {
        O('coregraph' + t).style.top = btop;
        O('coregraph' + t).style.height = bheight;
        O('coregraph' + t).style.width = bwidth;
        O('coregraph' + t).style.left = gbleft;
        O('back' + t).style.width = backwidth;
        O('back' + t).style.height = backheight;
        O('back' + t).style.left = bleft;
        O('bar' + t).style.height = backheight;
        O('bar' + t).style.left = bleft;
        O('top' + t).style.width = backwidth;
        O('top' + t).style.height = backheight;
        O('top' + t).style.left = bleft;
        O('tmpCore' + t).style.left = tleft;
        O('usgCore' + t).style.width = uwidth;
        O('bar' + t).style.color = scolor[t];
        O('tmpCore' + t).style.color = scolor[t];
        O('usgCore' + t).style.color = scolor[t];
        O('coregraph' + t).strokecolor = scolor[t];
        O('coregraph' + t).fillcolor = scolor[t];
        O('back' + t).style.top = parseInt((((t * 10) + 32) + allSizeChange) * size);
        O('top' + t).style.top = parseInt((((t * 10) + 32) + allSizeChange) * size);
        O('bar' + t).style.top = parseInt((((t * 10) + 32) + allSizeChange) * size);
        O('tmpCore' + t).style.top = parseInt((((t * 10) + 28) + allSizeChange) * size);
        O('usgCore' + t).style.top = parseInt((((t * 10) + 28) + allSizeChange) * size);
    }
    O('border').style.top = btop;
    O('coreTempErr').style.top = btop;
    O('linesx').style.height = bheight;
    O('linesy').style.height = bheight;
    O('ramgraph').style.height = bheight;
    O('pagegraph').style.height = bheight;
    O('border').style.height = bheight+1;
    O('linesx').style.width = bwidth;
    O('linesy').style.width = bwidth;
    O('ramgraph').style.width = bwidth;
    O('pagegraph').style.width = bwidth;
    O('border').style.width = bwidth+1;
    O('coreTempErr').style.width = bwidth;
    O('linesx').style.left = gbleft;
    O('linesy').style.left = gbleft;
    O('ramgraph').style.left = gbleft;
    O('pagegraph').style.left = gbleft;
    O('border').style.left = gbleft;
    O('backRam').style.width = backwidth;
    O('topRam').style.width = backwidth;
    O('backRam').style.height = backheight;
    O('topRam').style.height = backheight;
    O('barRam').style.height = backheight;
    O('backPage').style.width = backwidth;
    O('topPage').style.width = backwidth;
    O('backPage').style.height = backheight;
    O('topPage').style.height = backheight;
    O('barPage').style.height = backheight;
    O('cpuUsage').style.color = sTitle;
    O('usgTotal').style.color = sTitle;
    O('clock').style.color = sClock;
    O('cpuSpeed').style.color = sClock;
    O('cpuModel').style.color = sProcessor;
    O('username').style.color = sDomUse;
    O('used').style.color = sUFT;
    O('free').style.color = sUFT;
    O('total').style.color = sUFT;
    O('ram').style.color = sRam;
    O('usedValue').style.color = sRam;
    O('freeValue').style.color = sRam;
    O('totalValue').style.color = sRam;
    O('page').style.color = sPagec;
    O('usedPageValue').style.color = sPagec;
    O('freePageValue').style.color = sPagec;
    O('totalPageValue').style.color = sPagec;
    O('ramUsage').style.color = sRam;
    O('barRam').style.color = sRam;
    O('ramgraph').strokecolor = sRam;
    O('ramgraph').fillcolor = sRam;
    O('backRam').style.left = bleft;
    O('topRam').style.left = bleft;
    O('barRam').style.left = bleft;
    O('pageUsage').style.color = sPagec;
    O('barPage').style.color = sPagec;
    O('pagegraph').strokecolor = sPagec;
    O('pagegraph').fillcolor = sPagec;
    O('backPage').style.left = bleft;
    O('topPage').style.left = bleft;
    O('barPage').style.left = bleft;
    O('IntelCPU').style.top = parseInt(2 * size);
    O('IntelCPU').style.left = parseInt(4 * size);
    O('IntelCPU').style.height = parseInt(30 * size);
    O('IntelCPU').style.width = parseInt(30 * size);
    O('AMDCPU').style.top = parseInt(2 * size);
    O('AMDCPU').style.left = parseInt(4 * size);
    O('AMDCPU').style.height = parseInt(30 * size);
    O('AMDCPU').style.width = parseInt(30 * size);
    O('IconCPU').style.top = parseInt(2 * size);
    O('IconCPU').style.left = parseInt(4 * size);
    O('IconCPU').style.height = parseInt(30 * size);
    O('IconCPU').style.width = parseInt(30 * size);
    O('cpuUsage').style.fontSize = parseInt(11 * size);
    O('cpuUsage').style.top = parseInt(5 * size);
    O('cpuUsage').style.left = parseInt(36 * size);
    O('usgTotal').style.fontSize = parseInt(11 * size);
    O('usgTotal').style.top = parseInt(5 * size);
    O('usgTotal').style.right = parseInt(9 * size);
    O('clock').style.top = parseInt(18 * size);
    O('clock').style.left = parseInt(36 * size);
    O('cpuSpeed').style.top = parseInt(18 * size);
    O('cpuSpeed').style.right = parseInt(32 * size);
    if (alertCPU1 == 1) {
        O('iconAlert').style.color = sAlertIcon;
    }
    else if (alertCPU1 == 2) {
        O('iconAlert').style.color = "#808080";
    }
    O('iconAlert').innerHTML = "&#9834;";
    O('iconAlert').style.top = parseInt(14 * size);
    O('iconAlert').style.right = parseInt(9 * size);
    O('iconAlert').style.fontSize = parseInt(16 * size);
    O('cpuModel').style.top = parseInt(28 * size);
    O('cpuModel').style.left = parseInt(3 * size);
    O('cpuModel').style.width = parseInt(124 * size);
    O('cpuModel').style.height = parseInt(12 * size);
    O('backRam').style.top = parseInt((32 + sizeChange + sizeChange2 + sizeChange3 -
  sizeChange5 + sizeChange6 + sizeChange8) * size);
    O('topRam').style.top = parseInt((32 + sizeChange + sizeChange2 + sizeChange3 -
  sizeChange5 + sizeChange6 + sizeChange8) * size);
    O('barRam').style.top = parseInt((32 + sizeChange + sizeChange2 + sizeChange3 -
  sizeChange5 + sizeChange6 + sizeChange8) * size);
    O('backPage').style.top = parseInt((32 + allSizeChange) * size);
    O('topPage').style.top = parseInt((32 + allSizeChange) * size);
    O('barPage').style.top = parseInt((32 + allSizeChange) * size);
    O('username').style.top = parseInt((28 + sizeChange) * size);
    O('username').style.left = parseInt(3 * size);
    O('username').style.width = parseInt(124 * size);
    O('username').style.height = parseInt(12 * size);
    O('used').style.top = parseInt((28 + sizeChange + sizeChange8) * size);
    O('used').style.left = parseInt(13 * size);
    O('free').style.top = parseInt((28 + sizeChange + sizeChange8) * size);
    O('free').style.left = parseInt(61 * size);
    O('total').style.top = parseInt((28 + sizeChange + sizeChange8) * size);
    O('total').style.left = parseInt(100 * size);
    O('usedValue').style.top = parseInt((38 + sizeChange + sizeChange8) * size);
    O('usedValue').style.left = parseInt(8 * size);
    O('freeValue').style.top = parseInt((38 + sizeChange + sizeChange8) * size);
    O('freeValue').style.left = parseInt(53 * size);
    O('totalValue').style.top = parseInt((38 + sizeChange + sizeChange8) * size);
    O('totalValue').style.width = parseInt(120 * size);
    O('usedPageValue').style.top = parseInt((38 + sizeChange + sizeChange3 + sizeChange8) *
  size);
    O('usedPageValue').style.left = parseInt(8 * size);
    O('freePageValue').style.top = parseInt((38 + sizeChange + sizeChange3 + sizeChange8) *
  size);
    O('freePageValue').style.left = parseInt(53 * size);
    O('totalPageValue').style.top = parseInt((38 + sizeChange + sizeChange3 + sizeChange8) *
  size);
    O('totalPageValue').style.width = parseInt(120 * size);
    O('ram').style.top = parseInt((28 + sizeChange + sizeChange2 + sizeChange3 - sizeChange5
   + sizeChange6 + sizeChange8) * size);
    O('ram').style.left = parseInt(64 * size);
    O('ramUsage').style.top = parseInt((28 + sizeChange + sizeChange2 + sizeChange3 -
  sizeChange5 + sizeChange6 + sizeChange8) * size);
    O('ramUsage').style.width = parseInt(120 * size);
    O('page').style.top = parseInt((28 + allSizeChange) * size);
    O('page').style.left = parseInt(64 * size);
    O('pageUsage').style.top = parseInt((28 + allSizeChange) * size);
    O('pageUsage').style.width = parseInt(120 * size);
    O('newUpdate').style.left = parseInt(15 * size);
    O('newUpdate').style.width = parseInt(120 * size);
    O('newUpdate').style.color = "#FF0000";
}
function runTaskManager() {
    if (doubleClick == 1) {
        System.Shell.execute("taskmgr.exe");
    }
    if (doubleClick == 2) {
        System.Shell.execute("perfmon", "/res");
    }
    else {
    }
}
function getURL(a) {
    try {
        var xmlReq = new ActiveXObject("Microsoft.XMLHTTP");
        xmlReq.open("GET", a, false);
        xmlReq.setRequestHeader("If-Modified-Since", "Sat, 1 Jan 2000 00:00:00 GMT");
        xmlReq.send();
        if (xmlReq.status == 200) {
            return xmlReq.responseText
        }
        else {
            return false
        }
    }
    catch (urlData) {
        setTimeout("showUpdate2()", 600000);
    }
}
function updateAvailable() {
    var urlData = getURL("http://addgadgets.com/all_cpu_meter/version.htm");
    if (urlData === false) {
        return false
    }
    var version = "4.7";
    var a = parseFloat(version);
    var b = parseFloat(urlData);
    return b > a;
}
function showUpdate2() {
    if (updateAvailable()) {
        newUpdate.style.visibility = "visible";
        newUpdate.innerHTML = ("New version is available");
        sizeUpdate = 10;
        sizeMode();
    }
}
function timedMsg() {
    if (update == 1) {
        setTimeout("showUpdate2()", 60000);
    }
    else if (update == 2) {
        sizeUpdate = 0;
        newUpdate.style.visibility = "hidden";
        sizeMode();
    }
}
function show_flyout() {
    System.Gadget.Flyout.file = "flyout.html";
    System.Gadget.Flyout.show = true;
}