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
 System.Gadget.Flyout.file = "flyout.html";
 System.Gadget.settingsUI = "settings.html";
 System.Gadget.onSettingsClosed = onSettingsClosed;
 System.Gadget.onShowSettings = SettingsShow;
 System.Gadget.Flyout.onHide = hideFlyout;
 var shell = new ActiveXObject('WScript.Shell');
 var objLocator = new ActiveXObject("WbemScripting.SWbemLocator");
 var objWMIService = objLocator.ConnectServer(null, "root\\cimv2");
 var fso = new ActiveXObject("Scripting.FileSystemObject");
 var NetLib;
 var interfaceCount = 0;
 var nameItem = 0;
 var nameItem2 = 0;
 var nameItem3 = 0;
 var bytesReceived = 0;
 var bytesSent = 0;
 var lastBytesReceived = 0;
 var lastBytesSent = 0;
 var bytesReceivedSec = 0;
 var bytesSentSec = 0;
 var sentHist = new Array();
 var receivedHist = new Array();
 var maxHist = 110;
 var showextip = 1;
 var eipStatus = 1;
 var sizeUpdate = 2;
 var networkType = 2;
 var multiplexor = 2;
 var multiplexorNum=0;
 var multiplexorInt = new Array();
 var signalStrength = 0;
 var settimer = 1;
 var wbemFlagReturnImmediately = 0x10;
 var wbemFlagForwardOnly = 0x20;
 var ipcount= 0;
 var stime;
 var clearShowIIP;
 var clearShowEIP;
 var clearCheckAutoNet;
 var loadIP = 1;
 var bytesSentTotal = 0;
 var bytesReceivedTotal = 0;
 var bytesSentTot = 0;
 var bytesReceivedTot = 0;
 var bytesTotalTotal = 0;
 var bytesTotalPeak = 0;
 var bytesTotalOffPeak = 0;
 var clearSaveTotal;
 var ipListLength=0;
 var remainingTotal = 0;
 var billingDate = 1;
 var checkadjustUsage=2;
 var checkPeakAdjustUsage=2;
 var checkOffpeakAdjustUsage=2;
 var networkIntName='';
 var networkIntName2='';
 var networkIntName3='';
 var networkName='';
 var networkName2='';
 var intIPAddress='Not Connected';
 var IPenabledDes='';
 var IP=new Array();
 IP[0]='';
 var saveintIPAddress='';
 var saveextIPAddress='';
 var xmlReq;
 var xmlReq2;
 var clearGetIP;
 var getIPTime=1;
 var checkDescription='';
 var getintIPError=2;
 var autoNetworkName = new Array();
 var replaceNetworkName = '';
 var countAutoNetwork=0;
 var getAutoNetworkName='';
 var checkIPenabled=false;
 var sizeLine = new Array();
 var months = new Array('Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec');
 var checkAlert=2;
 var startCheckAlert=2;
 var O=document.getElementById;
 try
 {
   var fwPolicy2 = new ActiveXObject("HNetCfg.FwPolicy2");
 }
 catch (err)
 {
 }
 var NET_FW_PROFILE2_DOMAIN = 1, NET_FW_PROFILE2_PRIVATE = 2, NET_FW_PROFILE2_PUBLIC = 4;
 var allData="";
 var allIPData="";
 var dataDate="";
 var dataHour="";
 var todayDate="";
 var lines = new Array();
 query.value="";
 function onLoad()
 {
   var Startup = System.Gadget.Settings.read ("Startup");
   if ( Startup == 1)
   {
     loadSettings();
   }
   else
   {
     loadfilesettings();
   }
   NetLib = GetLibrary();
   autoNetworkInt();
   onTimer();
   clearSaveTotal = setInterval("saveTotal()",10000);
 }
 function onSettingsClosed()
 {
   loadSettings();
   autoNetworkInt();
 }
 function SettingsShow()
 {
   clearTimeout(clearCheckAutoNet);
   if (Player.controls.isAvailable('Stop'))
   {
     Player.controls.stop();
   }
 }
 function loadSettings()
 {
   size = System.Gadget.Settings.read("size");
   if ( size != "") ;
   else size = 1;
   if (size <= "4") ;
   else size = 4;
   method = System.Gadget.Settings.read("method");
   if (method != "") ;
   else method = 2;
   autoInterface = System.Gadget.Settings.read("autoInterface");
   if (autoInterface != "") ;
   else autoInterface = 1;
   scanNetwork = System.Gadget.Settings.read("scanNetwork");
   if (scanNetwork != "") ;
   else scanNetwork = 10;
   Int_Nr5 = System.Gadget.Settings.read("interface5");
   if (Int_Nr5 != "") ;
   else Int_Nr5 = "";
   Int_Nr6 = System.Gadget.Settings.read("interface6");
   if (Int_Nr6 != "") ;
   else Int_Nr6 = "";
   Int_Nr7 = System.Gadget.Settings.read("interface7");
   if (Int_Nr7 != "") ;
   else Int_Nr7 = "";
   showNetworkAdaName = System.Gadget.Settings.read("showNetworkAdaName");
   if (showNetworkAdaName != "") ;
   else showNetworkAdaName = 1;
   showInternalIP = System.Gadget.Settings.read("showInternalIP");
   if (showInternalIP != "") ;
   else showInternalIP = 1;
   showBlacklisted = System.Gadget.Settings.read("showBlacklisted");
   if (showBlacklisted != "") ;
   else showBlacklisted = 1;
   saveextip = System.Gadget.Settings.read("saveextip");
   if (saveextip != "") ;
   else saveextip = "0.0.0.0";
   blRatio = System.Gadget.Settings.read("blRatio");
   if (blRatio != "") blRatioTotal = blRatio.split(",");
   else blRatio = "";
   showSTIL = System.Gadget.Settings.read("showSTIL");
   if (showSTIL != "") ;
   else showSTIL = 1;
   showFirewall = System.Gadget.Settings.read("showFirewall");
   if (showFirewall != "") ;
   else showFirewall = 1;
   graph = System.Gadget.Settings.read("graph");
   if (graph != "") ;
   else graph = 1;
   drawstyle = System.Gadget.Settings.read("drawstyle");
   if (drawstyle != "") ;
   else drawstyle = 0;
   scaletype = System.Gadget.Settings.read("scaletype");
   if (scaletype != "") ;
   else scaletype = 0;
   scalesize = System.Gadget.Settings.read("scalesize");
   if (scalesize != "") ;
   else scalesize = 0;
   vismode = System.Gadget.Settings.read("vismode");
   if (vismode != "") ;
   else vismode = 0;
   barup = System.Gadget.Settings.read("barup") * 1024;
   if (barup != "") ;
   else barup = 0;
   bardown = System.Gadget.Settings.read("bardown") * 1024;
   if (bardown != "") ;
   else bardown = 0;
   update = System.Gadget.Settings.read("update");
   if (update != "") ;
   else update = 1;
   netType = System.Gadget.Settings.read("netType");
   if (netType != "") ;
   else netType = 3;
   autoNetwork = System.Gadget.Settings.read("autoNetwork");
   if (autoNetwork != "") ;
   else autoNetwork = 1;
   totalbandwidth = System.Gadget.Settings.read("totalbandwidth");
   if (totalbandwidth != "") ;
   else totalbandwidth = 1;
   showextip = System.Gadget.Settings.read("showextip");
   if (showextip != "") ;
   else showextip = 1;
   autoiip = System.Gadget.Settings.read("autoiip");
   if (autoiip != "") ;
   else autoiip = 1;
   setiiptimer = System.Gadget.Settings.read("setiiptimer");
   if (setiiptimer != "") ;
   else setiiptimer = 10;
   autoeip = System.Gadget.Settings.read("autoeip");
   if (autoeip != "") ;
   else autoeip = 2;
   seteiptimer = System.Gadget.Settings.read("seteiptimer");
   if (seteiptimer != "") ;
   else seteiptimer = 10;
   if (seteiptimer<1)
   {
     seteiptimer=1;
   }
   fixUnit = System.Gadget.Settings.read("fixUnit");
   if (fixUnit != "") ;
   else fixUnit = 1;
   backg = System.Gadget.Settings.read("backg");
   if (backg != "") ;
   else backg = "#080808";
   fixBackg = System.Gadget.Settings.read("fixBackg");
   if (fixBackg != "") sBackg = fixBackg ;
   else sBackg = backg;
   title = System.Gadget.Settings.read("title");
   if (title != "") ;
   else title = "#ffffff";
   fixTitle = System.Gadget.Settings.read("fixTitle");
   if (fixTitle != "") sTitle = fixTitle ;
   else sTitle = title;
   NetIN = System.Gadget.Settings.read("NetIN");
   if (NetIN != "") ;
   else NetIN = "#90ee90";
   fixNetIN = System.Gadget.Settings.read("fixNetIN");
   if (fixNetIN != "") sNetIN = fixNetIN ;
   else sNetIN = NetIN;
   SSID = System.Gadget.Settings.read("SSID");
   if (SSID != "") ;
   else SSID = "#87cefa";
   fixSSID = System.Gadget.Settings.read("fixSSID");
   if (fixSSID != "") sSSID = fixSSID ;
   else sSSID = SSID;
   INTIP = System.Gadget.Settings.read("INTIP");
   if (INTIP != "") ;
   else INTIP = "#87cefa";
   fixINTIP = System.Gadget.Settings.read("fixINTIP");
   if (fixINTIP != "") sINTIP = fixINTIP ;
   else sINTIP = INTIP;
   EXTIP = System.Gadget.Settings.read("EXTIP");
   if (EXTIP != "") ;
   else EXTIP = "#87cefa";
   fixEXTIP = System.Gadget.Settings.read("fixEXTIP");
   if (fixEXTIP != "") sEXTIP = fixEXTIP ;
   else sEXTIP = EXTIP;
   BLIP = System.Gadget.Settings.read("BLIP");
   if (BLIP != "") ;
   else BLIP = "#fff62a";
   fixBLIP = System.Gadget.Settings.read("fixBLIP");
   if (fixBLIP != "") sBLIP = fixBLIP ;
   else sBLIP = BLIP;
   But = System.Gadget.Settings.read("But");
   if (But != "") ;
   else But = "#ffcc00";
   fixBut = System.Gadget.Settings.read("fixBut");
   if (fixBut != "") sBut = fixBut ;
   else sBut = But;
   FWPF = System.Gadget.Settings.read("FWPF");
   if (FWPF != "") ;
   else FWPF = "#ec7527";
   fixFWPF = System.Gadget.Settings.read("fixFWPF");
   if (fixFWPF != "") sFWPF = fixFWPF ;
   else sFWPF = FWPF;
   Conn = System.Gadget.Settings.read("Conn");
   if (Conn != "") ;
   else Conn = "#87cefa";
   fixConn = System.Gadget.Settings.read("fixConn");
   if (fixConn != "") sConn = fixConn ;
   else sConn = Conn;
   Sign = System.Gadget.Settings.read("Sign");
   if (Sign != "") ;
   else Sign = "#87cefa";
   fixSign = System.Gadget.Settings.read("fixSign");
   if (fixSign != "") sSign = fixSign ;
   else sSign = Sign;
   Uplo = System.Gadget.Settings.read("Uplo");
   if (Uplo != "") ;
   else Uplo = "#90ee90";
   fixUplo = System.Gadget.Settings.read("fixUplo");
   if (fixUplo != "") sUplo = fixUplo ;
   else sUplo = Uplo;
   Downlo = System.Gadget.Settings.read("Downlo");
   if (Downlo != "") ;
   else Downlo = "#fff62a";
   fixDownlo = System.Gadget.Settings.read("fixDownlo");
   if (fixDownlo != "") sDownlo = fixDownlo ;
   else sDownlo = Downlo;
   Curr = System.Gadget.Settings.read("Curr");
   if (Curr != "") ;
   else Curr = "#87cefa";
   fixCurr = System.Gadget.Settings.read("fixCurr");
   if (fixCurr != "") sCurr = fixCurr ;
   else sCurr = Curr;
   CurrUp = System.Gadget.Settings.read("CurrUp");
   if (CurrUp != "") ;
   else CurrUp = "#90ee90";
   fixCurrUp = System.Gadget.Settings.read("fixCurrUp");
   if (fixCurrUp != "") sCurrUp = fixCurrUp ;
   else sCurrUp = CurrUp;
   CurrDown = System.Gadget.Settings.read("CurrDown");
   if (CurrDown != "") ;
   else CurrDown = "#fff62a";
   fixCurrDown = System.Gadget.Settings.read("fixCurrDown");
   if (fixCurrDown != "") sCurrDown = fixCurrDown ;
   else sCurrDown = CurrDown;
   Tota = System.Gadget.Settings.read("Tota");
   if (Tota != "") ;
   else Tota = "#87cefa";
   fixTota = System.Gadget.Settings.read("fixTota");
   if (fixTota != "") sTota = fixTota ;
   else sTota = Tota;
   TotaUp = System.Gadget.Settings.read("TotaUp");
   if (TotaUp != "") ;
   else TotaUp = "#90ee90";
   fixTotaUp = System.Gadget.Settings.read("fixTotaUp");
   if (fixTotaUp != "") sTotaUp = fixTotaUp ;
   else sTotaUp = TotaUp;
   TotaDown = System.Gadget.Settings.read("TotaDown");
   if (TotaDown != "") ;
   else TotaDown = "#fff62a";
   fixTotaDown = System.Gadget.Settings.read("fixTotaDown");
   if (fixTotaDown != "") sTotaDown = fixTotaDown ;
   else sTotaDown = TotaDown;
   RemPercent = System.Gadget.Settings.read("RemPercent");
   if (RemPercent != "") ;
   else RemPercent = "#87cefa";
   fixRemPercent = System.Gadget.Settings.read("fixRemPercent");
   if (fixRemPercent != "") sRemPercent = fixRemPercent ;
   else sRemPercent = RemPercent;
   RemDays = System.Gadget.Settings.read("RemDays");
   if (RemDays != "") ;
   else RemDays = "#87cefa";
   fixRemDays = System.Gadget.Settings.read("fixRemDays");
   if (fixRemDays != "") sRemDays = fixRemDays ;
   else sRemDays = RemDays;
   RemQuota = System.Gadget.Settings.read("RemQuota");
   if (RemQuota != "") ;
   else RemQuota = "#87cefa";
   fixRemQuota = System.Gadget.Settings.read("fixRemQuota");
   if (fixRemQuota != "") sRemQuota = fixRemQuota ;
   else sRemQuota = RemQuota;
   RemPerDay = System.Gadget.Settings.read("RemPerDay");
   if (RemPerDay != "") ;
   else RemPerDay = "#87cefa";
   fixRemPerDay = System.Gadget.Settings.read("fixRemPerDay");
   if (fixRemPerDay != "") sRemPerDay = fixRemPerDay ;
   else sRemPerDay = RemPerDay;
   RemUsed = System.Gadget.Settings.read("RemUsed");
   if (RemUsed != "") ;
   else RemUsed = "#90ee90";
   fixRemUsed = System.Gadget.Settings.read("fixRemUsed");
   if (fixRemUsed != "") sRemUsed = fixRemUsed ;
   else sRemUsed = RemUsed;
   PeakUsed = System.Gadget.Settings.read("PeakUsed");
   if (PeakUsed != "") ;
   else PeakUsed = "#90ee90";
   fixPeakUsed = System.Gadget.Settings.read("fixPeakUsed");
   if (fixPeakUsed != "") sPeakUsed = fixPeakUsed ;
   else sPeakUsed = PeakUsed;
   OffpeakUsed = System.Gadget.Settings.read("OffpeakUsed");
   if (OffpeakUsed != "") ;
   else OffpeakUsed = "#90ee90";
   fixOffpeakUsed = System.Gadget.Settings.read("fixOffpeakUsed");
   if (fixOffpeakUsed != "") sOffpeakUsed = fixOffpeakUsed ;
   else sOffpeakUsed = OffpeakUsed;
   dayQuota = System.Gadget.Settings.read("dayQuota");
   if (dayQuota != "") ;
   else dayQuota = new Date(2013,0,1);
   billingDate = System.Gadget.Settings.read("billingDate");
   setRemaining = System.Gadget.Settings.read("setRemaining");
   if (setRemaining != "") ;
   else setRemaining = 2;
   checkRemaining = System.Gadget.Settings.read("checkRemaining");
   if (checkRemaining != "") ;
   else checkRemaining = 1;
   billingStarts = System.Gadget.Settings.read("billingStarts");
   if (billingStarts != "") ;
   else billingStarts = 1;
   cycleQuota = System.Gadget.Settings.read("cycleQuota");
   if (cycleQuota != "") ;
   else cycleQuota = 100;
   billingWeek = System.Gadget.Settings.read("billingWeek");
   if (billingWeek != "") ;
   else billingWeek = 1;
   billingMonth = System.Gadget.Settings.read("billingMonth");
   if (billingMonth != "") ;
   else billingMonth = 1;
   cycleQuotaSize = System.Gadget.Settings.read("cycleQuotaSize");
   if (cycleQuotaSize != "") ;
   else cycleQuotaSize = 1;
   readTotalTotal = System.Gadget.Settings.read ("saveTotalTotal");
   if (readTotalTotal == "")
   {
     readTotalTotal = 104857600;
     System.Gadget.Settings.write("readTotalTotal", readTotalTotal);
   }
   var readTotal = System.Gadget.Settings.read ("saveTotalTot");
   if (readTotal != "")
   {
     bytesTotalTotal = parseInt(readTotal)
   }
   readPeakTotal = System.Gadget.Settings.read ("savePeakTotal");
   if (readPeakTotal == "")
   {
     readPeakTotal = 104857600;
     System.Gadget.Settings.write("readPeakTotal", readPeakTotal);
   }
   var readTotalPeak = System.Gadget.Settings.read ("saveTotalPeak");
   if (readTotalPeak != "")
   {
     bytesTotalPeak = parseInt(readTotalPeak)
   }
   readOffpeakTotal = System.Gadget.Settings.read ("saveOffpeakTotal");
   if (readOffpeakTotal == "")
   {
     readOffpeakTotal = 0;
     System.Gadget.Settings.write("readOffpeakTotal", readOffpeakTotal);
   }
   var readTotalOffPeak = System.Gadget.Settings.read ("saveTotalOffPeak");
   if (readTotalOffPeak != "")
   {
     bytesTotalOffPeak = parseInt(readTotalOffPeak)
   }
   peakHour = System.Gadget.Settings.read("peakHour");
   if (peakHour != "") ;
   else peakHour = 0;
   peakMin = System.Gadget.Settings.read("peakMin");
   if (peakMin != "") ;
   else peakMin = 0;
   peakCycleQuota = System.Gadget.Settings.read("peakCycleQuota");
   if (peakCycleQuota != "") ;
   else peakCycleQuota = 100;
   peakCycleQuotaSize = System.Gadget.Settings.read("peakCycleQuotaSize");
   if (peakCycleQuotaSize != "") ;
   else peakCycleQuotaSize = 1;
   offpeakHour = System.Gadget.Settings.read("offpeakHour");
   if (offpeakHour != "") ;
   else offpeakHour = 0;
   offpeakMin = System.Gadget.Settings.read("offpeakMin");
   if (offpeakMin != "") ;
   else offpeakMin = 0;
   offpeakCycleQuota = System.Gadget.Settings.read("offpeakCycleQuota");
   offpeakCycleQuotaSize = System.Gadget.Settings.read("offpeakCycleQuotaSize");
   if (offpeakCycleQuotaSize != "") ;
   else offpeakCycleQuotaSize = 4;
   saveChartData = System.Gadget.Settings.read("saveChartData");
   if (saveChartData != "") ;
   else saveChartData = 2;
   var savedataSentTotal = System.Gadget.Settings.read ("savedataSentTotal");
   if (savedataSentTotal != "")
   {
     dataSentTotal = parseInt(savedataSentTotal);
   }
   else
   {
     dataSentTotal = 0;
   }
   var savedataReceivedTotal = System.Gadget.Settings.read ("savedataReceivedTotal");
   if (savedataReceivedTotal != "")
   {
     dataReceivedTotal = parseInt(savedataReceivedTotal);
   }
   else
   {
     dataReceivedTotal = 0;
   }
   checkadjustUsage = System.Gadget.Settings.read("checkadjustUsage");
   checkPeakAdjustUsage = System.Gadget.Settings.read("checkPeakAdjustUsage");
   checkOffpeakAdjustUsage = System.Gadget.Settings.read("checkOffpeakAdjustUsage");
   dataDate = System.Gadget.Settings.read("dataDate");
   dataHour = System.Gadget.Settings.read("dataHour");
   readIniUsage = System.Gadget.Settings.read("readIniUsage");
   if (readIniUsage != "") ;
   else readIniUsage = 1;
   saveWriteUsage = System.Gadget.Settings.read("saveWriteUsage");
   if (saveWriteUsage != "") ;
   else saveWriteUsage = 1;
   alertLostCon = System.Gadget.Settings.read("alertLostCon");
   if ( alertLostCon != "") ;
   else alertLostCon = 2;
   soundLostCon = System.Gadget.Settings.read("soundLostCon");
   if ( soundLostCon != "") ;
   else soundLostCon = 1;
   soundLostConurl = System.Gadget.Settings.read("soundLostConurl");
   if ( soundLostConurl != "") ;
   else soundLostConurl = "";
   soundLostConVol = System.Gadget.Settings.read("soundLostConVol");
   if ( soundLostConVol != "") ;
   else soundLostConVol = 100;
   soundLostConRepeats = System.Gadget.Settings.read("soundLostConRepeats");
   if ( soundLostConRepeats != "") ;
   else soundLostConRepeats = 3;
   soundLostConCount = System.Gadget.Settings.read("soundLostConCount");
   if ( soundLostConCount != "") ;
   else soundLostConCount = 1;
   AlertIcon = System.Gadget.Settings.read("AlertIcon");
   if ( AlertIcon != "") ;
   else AlertIcon = "#90EE90";
   fixAlertIcon = System.Gadget.Settings.read("fixAlertIcon");
   if (fixAlertIcon != "") sAlertIcon = fixAlertIcon ;
   else sAlertIcon = AlertIcon;
   saveIPAddress = System.Gadget.Settings.read("saveIPAddress");
   if ( saveIPAddress != "") ;
   else saveIPAddress = 1;
   showSearch = System.Gadget.Settings.read("showSearch");
   if ( showSearch != "") ;
   else showSearch = 1;
   getPeakHour = parseInt(peakHour*60*60*1000);
   getPeakMin = parseInt(peakMin*60*1000);
   getOffpeakHour = parseInt(offpeakHour*60*60*1000);
   getOffpeakMin = parseInt(offpeakMin*60*1000);
   readUsage();
   timedMsg();
   sizeMode();
   resetBar();
   if(startCheckAlert==2)
   {
     setTimeout("startCheckAlert=1", 10000);
   }
 }
 function loadfilesettings()
 {
   var inifilename = System.Environment.getEnvironmentVariable("APPDATA") + "\\" + System.Gadget.name + "_Settings.ini";
   try
   {
     var inifile = fso.OpenTextFile(inifilename, 1);
     try
     {
       var tmp = inifile.ReadLine();
       tmp = inifile.ReadLine();
       if (tmp != ";v3") throw "old";
       netType = inifile.ReadLine();
       System.Gadget.Settings.write("netType", netType);
       method = inifile.ReadLine();
       System.Gadget.Settings.write("method", method);
       interface = inifile.ReadLine();
       System.Gadget.Settings.write("interface", interface);
       interface2 = inifile.ReadLine();
       System.Gadget.Settings.write("interface2", interface2);
       interface3 = inifile.ReadLine();
       System.Gadget.Settings.write("interface3", interface3);
       autoNetwork = inifile.ReadLine();
       System.Gadget.Settings.write("autoNetwork", autoNetwork);
       settimer = inifile.ReadLine();
       System.Gadget.Settings.write("settimer", settimer);
       update = inifile.ReadLine();
       System.Gadget.Settings.write("update", update);
       fixsize = inifile.ReadLine();
       System.Gadget.Settings.write("fixsize", fixsize);
       ssize = inifile.ReadLine();
       System.Gadget.Settings.write("ssize", ssize);
       showextip = inifile.ReadLine();
       System.Gadget.Settings.write("showextip", showextip);
       VisMode = inifile.ReadLine();
       System.Gadget.Settings.write("VisMode", VisMode);
       barup = inifile.ReadLine();
       System.Gadget.Settings.write("barup", barup);
       bardown = inifile.ReadLine();
       System.Gadget.Settings.write("bardown", bardown);
       fixUnit = inifile.ReadLine();
       System.Gadget.Settings.write("fixUnit", fixUnit);
       drawstyle = inifile.ReadLine();
       System.Gadget.Settings.write("drawstyle", drawstyle);
       ScaleType = inifile.ReadLine();
       System.Gadget.Settings.write("ScaleType", ScaleType);
       ScaleSize = inifile.ReadLine();
       System.Gadget.Settings.write("ScaleSize", ScaleSize);
       totalbandwidth = inifile.ReadLine();
       System.Gadget.Settings.write("totalbandwidth", totalbandwidth);
       bytesSentSize = inifile.ReadLine();
       System.Gadget.Settings.write("bytesSentSize", bytesSentSize);
       bytesReceivedSize = inifile.ReadLine();
       System.Gadget.Settings.write("bytesReceivedSize", bytesReceivedSize);
       backg = inifile.ReadLine();
       System.Gadget.Settings.write("backg", backg);
       fixBackg = inifile.ReadLine();
       System.Gadget.Settings.write("fixBackg", fixBackg);
       title = inifile.ReadLine();
       System.Gadget.Settings.write("title", title);
       fixTitle = inifile.ReadLine();
       System.Gadget.Settings.write("fixTitle", fixTitle);
       SSID = inifile.ReadLine();
       System.Gadget.Settings.write("SSID", SSID);
       fixSSID = inifile.ReadLine();
       System.Gadget.Settings.write("fixSSID", fixSSID);
       INTIP = inifile.ReadLine();
       System.Gadget.Settings.write("INTIP", INTIP);
       fixINTIP = inifile.ReadLine();
       System.Gadget.Settings.write("fixINTIP", fixINTIP);
       EXTIP = inifile.ReadLine();
       System.Gadget.Settings.write("EXTIP", EXTIP);
       fixEXTIP = inifile.ReadLine();
       System.Gadget.Settings.write("fixEXTIP", fixEXTIP);
       But = inifile.ReadLine();
       System.Gadget.Settings.write("But", But);
       fixBut = inifile.ReadLine();
       System.Gadget.Settings.write("fixBut", fixBut);
       Conn = inifile.ReadLine();
       System.Gadget.Settings.write("Conn", Conn);
       fixConn = inifile.ReadLine();
       System.Gadget.Settings.write("fixConn", fixConn);
       Sign = inifile.ReadLine();
       System.Gadget.Settings.write("Sign", Sign);
       fixSign = inifile.ReadLine();
       System.Gadget.Settings.write("fixSign", fixSign);
       Uplo = inifile.ReadLine();
       System.Gadget.Settings.write("Uplo", Uplo);
       fixUplo = inifile.ReadLine();
       System.Gadget.Settings.write("fixUplo", fixUplo);
       Downlo = inifile.ReadLine();
       System.Gadget.Settings.write("Downlo", Downlo);
       fixDownlo = inifile.ReadLine();
       System.Gadget.Settings.write("fixDownlo", fixDownlo);
       Curr = inifile.ReadLine();
       System.Gadget.Settings.write("Curr", Curr);
       fixCurr = inifile.ReadLine();
       System.Gadget.Settings.write("fixCurr", fixCurr);
       CurrUp = inifile.ReadLine();
       System.Gadget.Settings.write("CurrUp", CurrUp);
       fixCurrUp = inifile.ReadLine();
       System.Gadget.Settings.write("fixCurrUp", fixCurrUp);
       CurrDown = inifile.ReadLine();
       System.Gadget.Settings.write("CurrDown", CurrDown);
       fixCurrDown = inifile.ReadLine();
       System.Gadget.Settings.write("fixCurrDown", fixCurrDown);
       Tota = inifile.ReadLine();
       System.Gadget.Settings.write("Tota", Tota);
       fixTota = inifile.ReadLine();
       System.Gadget.Settings.write("fixTota", fixTota);
       TotaUp = inifile.ReadLine();
       System.Gadget.Settings.write("TotaUp", TotaUp);
       fixTotaUp = inifile.ReadLine();
       System.Gadget.Settings.write("fixTotaUp", fixTotaUp);
       TotaDown = inifile.ReadLine();
       System.Gadget.Settings.write("TotaDown", TotaDown);
       fixTotaDown = inifile.ReadLine();
       System.Gadget.Settings.write("fixTotaDown", fixTotaDown);
       autoeip = inifile.ReadLine();
       System.Gadget.Settings.write("autoeip", autoeip);
       seteiptimer = inifile.ReadLine();
       System.Gadget.Settings.write("seteiptimer", seteiptimer);
       graph = inifile.ReadLine();
       System.Gadget.Settings.write("graph", graph);
       setRemaining = inifile.ReadLine();
       System.Gadget.Settings.write("setRemaining", setRemaining);
       billingStarts = inifile.ReadLine();
       System.Gadget.Settings.write("billingStarts", billingStarts);
       cycleQuota = inifile.ReadLine();
       System.Gadget.Settings.write("cycleQuota", cycleQuota);
       billingWeek = inifile.ReadLine();
       System.Gadget.Settings.write("billingWeek", billingWeek);
       billingMonth = inifile.ReadLine();
       System.Gadget.Settings.write("billingMonth", billingMonth);
       cycleQuotaSize = inifile.ReadLine();
       System.Gadget.Settings.write("cycleQuotaSize", cycleQuotaSize);
       RemPercent = inifile.ReadLine();
       System.Gadget.Settings.write("RemPercent", RemPercent);
       fixRemPercent = inifile.ReadLine();
       System.Gadget.Settings.write("fixRemPercent", fixRemPercent);
       RemDays = inifile.ReadLine();
       System.Gadget.Settings.write("RemDays", RemDays);
       fixRemDays = inifile.ReadLine();
       System.Gadget.Settings.write("fixRemDays", fixRemDays);
       RemQuota = inifile.ReadLine();
       System.Gadget.Settings.write("RemQuota", RemQuota);
       fixRemQuota = inifile.ReadLine();
       System.Gadget.Settings.write("fixRemQuota", fixRemQuota);
       RemPerDay = inifile.ReadLine();
       System.Gadget.Settings.write("RemPerDay", RemPerDay);
       fixRemPerDay = inifile.ReadLine();
       System.Gadget.Settings.write("fixRemPerDay", fixRemPerDay);
       RemUsed = inifile.ReadLine();
       System.Gadget.Settings.write("RemUsed", RemUsed);
       fixRemUsed = inifile.ReadLine();
       System.Gadget.Settings.write("fixRemUsed", fixRemUsed);
       size = inifile.ReadLine();
       System.Gadget.Settings.write("size", size);
       interface5 = inifile.ReadLine();
       System.Gadget.Settings.write("interface5", interface5);
       interface6 = inifile.ReadLine();
       System.Gadget.Settings.write("interface6", interface6);
       interface7 = inifile.ReadLine();
       System.Gadget.Settings.write("interface7", interface7);
       allName = inifile.ReadLine();
       allName2 = inifile.ReadLine();
       allName3 = inifile.ReadLine();
       showInternalIP = inifile.ReadLine();
       System.Gadget.Settings.write("showInternalIP", showInternalIP);
       showBlacklisted = inifile.ReadLine();
       System.Gadget.Settings.write("showBlacklisted", showBlacklisted);
       showSTIL = inifile.ReadLine();
       System.Gadget.Settings.write("showSTIL", showSTIL);
       showFirewall = inifile.ReadLine();
       System.Gadget.Settings.write("showFirewall", showFirewall);
       peakHour = inifile.ReadLine();
       System.Gadget.Settings.write("peakHour", peakHour);
       peakMin = inifile.ReadLine();
       System.Gadget.Settings.write("peakMin", peakMin);
       peakCycleQuota = inifile.ReadLine();
       System.Gadget.Settings.write("peakCycleQuota", peakCycleQuota);
       peakCycleQuotaSize = inifile.ReadLine();
       System.Gadget.Settings.write("peakCycleQuotaSize", peakCycleQuotaSize);
       offpeakHour = inifile.ReadLine();
       System.Gadget.Settings.write("offpeakHour", offpeakHour);
       offpeakMin = inifile.ReadLine();
       System.Gadget.Settings.write("offpeakMin", offpeakMin);
       offpeakCycleQuota = inifile.ReadLine();
       System.Gadget.Settings.write("offpeakCycleQuota", offpeakCycleQuota);
       offpeakCycleQuotaSize = inifile.ReadLine();
       System.Gadget.Settings.write("offpeakCycleQuotaSize", offpeakCycleQuotaSize);
       BLIP = inifile.ReadLine();
       System.Gadget.Settings.write("BLIP", BLIP);
       fixBLIP = inifile.ReadLine();
       System.Gadget.Settings.write("fixBLIP", fixBLIP);
       FWPF = inifile.ReadLine();
       System.Gadget.Settings.write("FWPF", FWPF);
       fixFWPF = inifile.ReadLine();
       System.Gadget.Settings.write("fixFWPF", fixFWPF);
       PeakUsed = inifile.ReadLine();
       System.Gadget.Settings.write("PeakUsed", PeakUsed);
       fixPeakUsed = inifile.ReadLine();
       System.Gadget.Settings.write("fixPeakUsed", fixPeakUsed);
       OffpeakUsed = inifile.ReadLine();
       System.Gadget.Settings.write("OffpeakUsed", OffpeakUsed);
       fixOffpeakUsed = inifile.ReadLine();
       System.Gadget.Settings.write("fixOffpeakUsed", fixOffpeakUsed);
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
       saveChartData = inifile.ReadLine();
       System.Gadget.Settings.write("saveChartData", saveChartData);
       autoInterface = inifile.ReadLine();
       System.Gadget.Settings.write("autoInterface", autoInterface);
       scanNetwork = inifile.ReadLine();
       System.Gadget.Settings.write("scanNetwork", scanNetwork);
       showNetworkAdaName = inifile.ReadLine();
       System.Gadget.Settings.write("showNetworkAdaName", showNetworkAdaName);
       NetIN = inifile.ReadLine();
       System.Gadget.Settings.write("NetIN", NetIN);
       fixNetIN = inifile.ReadLine();
       System.Gadget.Settings.write("fixNetIN", fixNetIN);
       autoiip = inifile.ReadLine();
       System.Gadget.Settings.write("autoiip", autoiip);
       setiiptimer = inifile.ReadLine();
       System.Gadget.Settings.write("setiiptimer", setiiptimer);
       saveWriteUsage = inifile.ReadLine();
       System.Gadget.Settings.write("saveWriteUsage", saveWriteUsage);
       alertLostCon = inifile.ReadLine();
       System.Gadget.Settings.write("alertLostCon", alertLostCon);
       soundLostCon = inifile.ReadLine();
       System.Gadget.Settings.write("soundLostCon", soundLostCon);
       soundLostConurl = inifile.ReadLine();
       System.Gadget.Settings.write("soundLostConurl", soundLostConurl);
       soundLostConVol = inifile.ReadLine();
       System.Gadget.Settings.write("soundLostConVol", soundLostConVol);
       soundLostConRepeats = inifile.ReadLine();
       System.Gadget.Settings.write("soundLostConRepeats", soundLostConRepeats);
       soundLostConCount = inifile.ReadLine();
       System.Gadget.Settings.write("soundLostConCount", soundLostConCount);
       AlertIcon = inifile.ReadLine();
       System.Gadget.Settings.write("AlertIcon", AlertIcon);
       fixAlertIcon = inifile.ReadLine();
       System.Gadget.Settings.write("fixAlertIcon", fixAlertIcon);
       saveIPAddress = inifile.ReadLine();
       System.Gadget.Settings.write("saveIPAddress", saveIPAddress);
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
 function setonTimer()
 {
   settimer = System.Gadget.Settings.read("settimer");
   if (settimer == "")
   {
     settimer = 1;
     System.Gadget.Settings.write("settimer", settimer);
   }
   clearInterval(stime);
   stime = setInterval("onTimer()",parseInt( settimer * 1000 ));
 }
 function onTimer()
 {
   if(saveChartData==1)
   {
     now = new Date();
     date = now.getDate();
     month = now.getMonth();
     year = now.getFullYear();
     if(dataDate!="")
     {
       todayDate=now.valueOf();
     }
     else
     {
       var d = new Date(year,month,date+1);
       dataDate=d.getTime();
       System.Gadget.Settings.write ("dataDate", dataDate);
       dataHour="";
       System.Gadget.Settings.write ("dataHour", dataHour);
     }
     if(todayDate>=dataDate)
     {
       loadData();
     }
   }
   else if(saveChartData==2)
   {
     now = new Date();
     hour = now.getHours();
     date = now.getDate();
     month = now.getMonth();
     year = now.getFullYear();
     if(dataHour!="")
     {
       todayDate=now.valueOf();
     }
     else
     {
       var d = new Date(year,month,date,hour+1);
       dataHour=d.getTime();
       System.Gadget.Settings.write ("dataHour", dataHour);
       dataDate="";
       System.Gadget.Settings.write ("dataDate", dataDate);
     }
     if(todayDate>=dataHour)
     {
       loadData();
     }
   }
   retFirewall();
   GadgetUpdate();
   updateNetVars();
   refreshdisplay();
   if(setRemaining!=2)
   {
     refreshRemaining();
   }
   if(graph||VisMode == 1)
   {
     updateGraph();
   }
 }
 function saveIPLog()
 {
   if(saveIPAddress==1)
   {
     var dataSaveDate = new Date();
     if(dataSaveDate.getSeconds()>=10)
     {
       var dataGetSeconds = dataSaveDate.getSeconds();
     }
     else
     {
       var dataGetSeconds = '0'+dataSaveDate.getSeconds();
     }
     if(dataSaveDate.getMinutes()>=10)
     {
       var dataGetMinutes = dataSaveDate.getMinutes();
     }
     else
     {
       var dataGetMinutes = '0'+dataSaveDate.getMinutes();
     }
     var dataGetHours = dataSaveDate.getHours();
     var dataGetDate = dataSaveDate.getDate();
     var dataGetMonth = dataSaveDate.getMonth();
     var dataGetMonth = dataGetMonth+1;
     var dataGetFullYear = dataSaveDate.getFullYear();
     saveintIPAddress=intIPAddress;
     saveextIPAddress=IP[0];
     var ipLogfilename = System.Environment.getEnvironmentVariable("USERPROFILE") + "\\" + "IP_Log_Data.js";
     try
     {
       var ipdatafile = fso.OpenTextFile(ipLogfilename, 1);
       try
       {
       allIPData = ipdatafile.ReadAll().replace("';",""); } finally { ipdatafile.Close(); } } catch (err) {}  try { var ipdatafile = fso.OpenTextFile(ipLogfilename, 2, true); try { if(allIPData!=""){ipdatafile.Write(allIPData);ipdatafile.WriteLine("\\n\\");} else{ipdatafile.WriteLine("data='\\");
       }
     ipdatafile.Write(dataGetDate+"-"+dataGetMonth+"-"+dataGetFullYear+","+dataGetHours+":"+dataGetMinutes+":"+dataGetSeconds+","+saveintIPAddress+","+saveextIPAddress+"';"); } finally { ipdatafile.Close(); } } catch (err) {} } }  function loadData(){ var datafilename = System.Environment.getEnvironmentVariable("USERPROFILE") + "\\" + "Network_Meter_Data.js"; try { var datafile = fso.OpenTextFile(datafilename, 1); try { allData = datafile.ReadAll().replace("';","");
     }
     finally
     {
       datafile.Close();
     }
   }
   catch (err)
   {
   }
   saveData();
 }
 function saveData()
 {
   var dataSaveDate = new Date();
   dataSaveDate.setTime(dataHour);
   var dataGetHours = dataSaveDate.getHours();
   var dataGetDate = dataSaveDate.getDate();
   var dataGetMonth = dataSaveDate.getMonth();
   var dataGetMonth = dataGetMonth+1;
   var dataGetFullYear = dataSaveDate.getFullYear();
   var datafilename = System.Environment.getEnvironmentVariable("USERPROFILE") + "\\" + "Network_Meter_Data.js";
   if(saveChartData==1)
   {
     try
     {
       var datafile = fso.OpenTextFile(datafilename, 2, true);
       try
       {
         if(allData!="")
         {
           datafile.Write(allData);
           datafile.WriteLine("\\n\\");
         }
         else
         {
         datafile.WriteLine("data='\\");} datafile.Write(dataGetDate+"-"+dataGetMonth+"-"+dataGetFullYear+"-0,"+dataReceivedTotal+","+dataSentTotal+"';");
       }
       finally
       {
         datafile.Close();
       }
     }
     catch (err)
     {
     }
     var d = new Date(year,month,date+1);
     dataDate=d.getTime();
     System.Gadget.Settings.write ("dataDate", dataDate);
   }
   else
   {
     try
     {
       var datafile = fso.OpenTextFile(datafilename, 2, true);
       try
       {
         if(allData!="")
         {
           datafile.Write(allData);
           datafile.WriteLine("\\n\\");
         }
         else
         {
         datafile.WriteLine("data='\\");} datafile.Write(dataGetDate+"-"+dataGetMonth+"-"+dataGetFullYear+"-"+dataGetHours+","+dataReceivedTotal+","+dataSentTotal+"';");
       }
       finally
       {
         datafile.Close();
       }
     }
     catch (err)
     {
     }
     var d = new Date(year,month,date,hour+1);
     dataHour=d.getTime();
     System.Gadget.Settings.write ("dataHour", dataHour);
   }
   dataSentTotal=0;
   dataReceivedTotal=0;
 }
 function GadgetUpdate()
 {
   if (System.Network.Wireless.signalStrength == null)
   {
     signalStrength = 0
   }
   else
   {
     signalStrength = System.Network.Wireless.signalStrength
   }
   if (netType == 3)
   {
     if (signalStrength == 0)
     {
       if(networkType==2)
       {
         networkType=1;
         sizeMode();
       }
     }
     else
     {
       if(networkType==1)
       {
         networkType=2;
         sizeMode();
       }
     }
   }
   if (networkType == 2)
   {
     if (signalStrength == 0)
     {
       bar3.style.visibility = "hidden";
       O('networkid').innerHTML = "ID: ----";
       O('signalper').innerHTML = "Signal: None Detected";
     }
     else
     {
       bar3.style.visibility = "visible";
       O('bar3').style.width = parseInt( 0.5 * signalStrength * size );
       O('networkid').innerHTML = "ID: "+System.Network.Wireless.ssid;
       O('signalper').innerHTML = "Signal: "+signalStrength+"%";
     }
     secu = System.Network.Wireless.secureConnection;
     if (secu == true)
     {
       O('secure').innerHTML = "Connection: Secure";
     }
     else
     {
       O('secure').innerHTML = "Connection: Not Secure";
     }
   }
 }
 function retFirewall()
 {
   try
   {
     var CurrentProfiles = fwPolicy2.CurrentProfileTypes;
     if (CurrentProfiles & NET_FW_PROFILE2_DOMAIN)
     {
       O('profile').innerHTML = "Profile: Domain";
       try
       {
         if (fwPolicy2.FirewallEnabled(NET_FW_PROFILE2_DOMAIN))
         {
           O('firewall').innerHTML = "Firewall: On";
         }
         else
         {
           O('firewall').innerHTML = "Firewall: Off";
         }
       }
       catch (err)
       {
         O('firewall').innerHTML = "Firewall:"
       }
     }
     else if (CurrentProfiles & NET_FW_PROFILE2_PRIVATE)
     {
       O('profile').innerHTML = "Profile: Private";
       try
       {
         if (fwPolicy2.FirewallEnabled(NET_FW_PROFILE2_PRIVATE))
         {
           O('firewall').innerHTML = "Firewall: On";
         }
         else
         {
           O('firewall').innerHTML = "Firewall: Off";
         }
       }
       catch (err)
       {
         O('firewall').innerHTML = "Firewall:"
       }
     }
     else if (CurrentProfiles & NET_FW_PROFILE2_PUBLIC)
     {
       O('profile').innerHTML = "Profile: Public";
       try
       {
         if (fwPolicy2.FirewallEnabled(NET_FW_PROFILE2_PUBLIC))
         {
           O('firewall').innerHTML = "Firewall: On";
         }
         else
         {
           O('firewall').innerHTML = "Firewall: Off";
         }
       }
       catch (err)
       {
         O('firewall').innerHTML = "Firewall:"
       }
     }
     else
     {
       O('firewall').innerHTML = "Firewall: ----";
       O('profile').innerHTML = "Profile: ----";
     }
   }
   catch (err)
   {
     O('firewall').innerHTML = "Firewall:";
     O('profile').innerHTML = "Profile:";
   }
 }
 function autoNetworkInt()
 {
   if(autoInterface==2)
   {
     initHist();
     networkIntName=Int_Nr5.replace("[R]","").replace("(R)","").replace("[TM]","").replace("(TM)","").replace("/"," ").replace("_"," ").replace("#"," ");
     networkIntName2=Int_Nr6.replace("[R]","").replace("(R)","").replace("[TM]","").replace("(TM)","").replace("/"," ").replace("_"," ").replace("#"," ");
     networkIntName3=Int_Nr7.replace("[R]","").replace("(R)","").replace("[TM]","").replace("(TM)","").replace("/"," ").replace("_"," ").replace("#"," ");
     System.Gadget.Settings.write("foNetworkInt", Int_Nr6);
     setonTimer();
     findNetworkItem();
     findNetworkItem3();
     getIntIp();
     if(loadIP!=2)
     {
       getIP();
       loadIP=2;
       setTimeout("loadIP=1",30000);
     }
   }
   else
   {
     try
     {
       var netName = objWMIService.ExecQuery("SELECT Description,IPAddress FROM Win32_NetworkAdapterConfiguration Where IPenabled=True", "WQL", wbemFlagReturnImmediately | wbemFlagForwardOnly);
       var networkInt = new Enumerator(netName);
       for (var i=0; !networkInt.atEnd();
       networkInt.moveNext())
       {
         var objItem = networkInt.item();
         autoNetworkName[i]=objItem.Description;
         if(objItem.Description=="Microsoft Network Adapter Multiplexor Driver")
         {
           multiplexor=1;
         }
         countAutoNetwork=i;
         i++;
       }
     }
     catch (err)
     {
     }
     if(multiplexor==1)
     {
       method=2;
       try
       {
         multiplexorNum=0;
         interfaceCount = NetLib.Initialize();
         for (var n=0;n<interfaceCount+1;n++)
         {
           if(NetLib.BytesReceived(n)>=1)
           {
             multiplexorNum=multiplexorNum+1;
             multiplexorInt[multiplexorNum]=n;
           }
         }
       }
       catch (err)
       {
       }
       getAutoNetworkName="Microsoft Network Adapter Multiplexor Driver";
       setonTimer();
       initHist();
       networkIntName=getAutoNetworkName;
       networkIntName2=getAutoNetworkName;
       networkIntName3=getAutoNetworkName;
       System.Gadget.Settings.write("foNetworkInt", getAutoNetworkName);
       getIntIp();
       if(loadIP!=2)
       {
         getIP();
         loadIP=2;
         setTimeout("loadIP=1",30000);
       }
       clearCheckAutoNet=setTimeout("checkAutoNet()", parseInt(scanNetwork*1000));
     }
     else
     {
       try
       {
         interfaceCount = NetLib.Initialize();
         for (var n=0;n<interfaceCount+1;n++)
         {
           for (var i=0; i<countAutoNetwork+1; i++)
           {
             if(NetLib.Description(n)==autoNetworkName[i])
             {
               if(NetLib.BytesReceived(n)>=1)
               {
                 method=2;
                 nameItem3=n;
                 getAutoNetworkName=autoNetworkName[i];
                 setonTimer();
                 initHist();
                 networkIntName=getAutoNetworkName.replace("[R]","").replace("(R)","").replace("[TM]","").replace("(TM)","").replace("/"," ").replace("_"," ").replace("#"," ");
                 networkIntName2=getAutoNetworkName.replace("[R]","").replace("(R)","").replace("[TM]","").replace("(TM)","").replace("/"," ").replace("_"," ").replace("#"," ");
                 networkIntName3=getAutoNetworkName.replace("[R]","").replace("(R)","").replace("[TM]","").replace("(TM)","").replace("/"," ").replace("_"," ").replace("#"," ");
                 System.Gadget.Settings.write("foNetworkInt", getAutoNetworkName);
                 getIntIp();
                 if(loadIP!=2)
                 {
                   getIP();
                   loadIP=2;
                   setTimeout("loadIP=1",30000);
                 }
                 break;
               }
             }
           }
         }
       }
       catch (err)
       {
       }
       clearCheckAutoNet=setTimeout("checkAutoNet()", parseInt(scanNetwork*1000));
     }
   }
 }
 function updateNetVars()
 {
   lastBytesReceived = bytesReceived;
   lastBytesSent = bytesSent;
   if(method==2)
   {
     if(multiplexor==1)
     {
       try
       {
         var multibytesReceived=0;
         var multibytesSent=0;
         for (var i=1;i<multiplexorNum+1;i++)
         {
           var multibytesReceived = multibytesReceived+parseInt(NetLib.BytesReceived(multiplexorInt[i]));
           var multibytesSent = multibytesSent+parseInt(NetLib.BytesSent(multiplexorInt[i]));
         }
         O('networkAdaName').innerHTML = "Network Multiplexor "+multiplexorNum;
         bytesReceived = parseInt(multibytesReceived);
         bytesSent = parseInt(multibytesSent);
       }
       catch (err)
       {
       }
     }
     else
     {
       try
       {
         bytesReceived = parseInt(NetLib.BytesReceived(nameItem3));
         bytesSent = parseInt(NetLib.BytesSent(nameItem3));
       }
       catch (err)
       {
       }
       O('networkAdaName').innerHTML = networkIntName3;
     }
   }
   else
   {
     if(networkIntName!="")
     {
       try
       {
         var netName = objWMIService.ExecQuery("SELECT BytesReceivedPersec, BytesSentPersec FROM Win32_PerfRawData_Tcpip_NetworkInterface", "WQL", wbemFlagReturnImmediately | wbemFlagForwardOnly);
         var networkInt = new Enumerator(netName);
         for(var i=0; i!=nameItem; i++)
         {
           networkInt.moveNext()
         }
         var objItem2 = networkInt.item();
         bytesReceived = parseInt(objItem2.BytesReceivedPersec);
         bytesSent = parseInt(objItem2.BytesSentPersec);
       }
       catch (err)
       {
       }
       O('networkAdaName').innerHTML = networkIntName;
     }
   }
   bytesReceivedSec = bytesReceived - lastBytesReceived;
   bytesSentSec = bytesSent - lastBytesSent;
   if (lastBytesReceived == 0) bytesReceivedSec = 0;
   if (lastBytesSent == 0) bytesSentSec = 0;
   if (bytesReceivedSec < 0) bytesReceivedSec = 0;
   if (bytesSentSec < 0) bytesSentSec = 0;
   if (receivedHist.push(bytesReceivedSec) > maxHist) receivedHist.shift();
   if (sentHist.push(bytesSentSec) > maxHist) sentHist.shift();
 }
 function checkAutoNet()
 {
   if(multiplexor==1)
   {
     try
     {
       multiplexorNum=0;
       interfaceCount = NetLib.Initialize();
       for (var n=0;n<interfaceCount+1;n++)
       {
         if(NetLib.BytesReceived(n)>=1)
         {
           multiplexorNum=multiplexorNum+1;
           multiplexorInt[multiplexorNum]=n;
         }
       }
     }
     catch (err)
     {
     }
   }
   try
   {
     var netName = objWMIService.ExecQuery("SELECT IPenabled FROM Win32_NetworkAdapterConfiguration Where Description="+"'"+getAutoNetworkName+"'"+"", "WQL", wbemFlagReturnImmediately | wbemFlagForwardOnly);
     checkIPenabled = (new Enumerator(netName)).item().IPenabled;
   }
   catch (err)
   {
     checkIPenabled=false;
   }
   if(checkIPenabled==true)
   {
     clearCheckAutoNet=setTimeout("checkAutoNet()", parseInt(scanNetwork*1000));
   }
   else
   {
     if(intIPAddress!="Not Connected")
     {
       try
       {
         var netName = objWMIService.ExecQuery("SELECT Description FROM Win32_NetworkAdapterConfiguration Where IPenabled=True", "WQL", wbemFlagReturnImmediately | wbemFlagForwardOnly);
         IPenabledDes = (new Enumerator(netName)).item().Description;
       }
       catch (err)
       {
         IPenabledDes="";
       }
       if(IPenabledDes==getAutoNetworkName)
       {
         clearCheckAutoNet=setTimeout("checkAutoNet()", parseInt(scanNetwork*1000));
       }
       else
       {
         autoNetworkInt();
       }
     }
     else
     {
       autoNetworkInt();
     }
   }
 }
 function getIntIp()
 {
   try
   {
     var IPinternal = objWMIService.ExecQuery("SELECT IPAddress,Description FROM Win32_NetworkAdapterConfiguration Where IPenabled=True", "WQL", wbemFlagReturnImmediately | wbemFlagForwardOnly);
     var internalIP = new Enumerator(IPinternal);
     for (var i=0; !internalIP.atEnd();
     internalIP.moveNext())
     {
       objItem3 = internalIP.item();
       checkDescription = objItem3.Description.replace("[R]","").replace("(R)","").replace("[TM]","").replace("(TM)","").replace("/"," ").replace("_"," ").replace("#"," ");
       getintIPError=2;
       if(checkDescription==networkIntName2)
       {
         intIPAddress = objItem3.IPAddress.toArray()[0];
         O('intIP').innerHTML = "Int. IP: "+intIPAddress;
         if(eipStatus==1)
         {
           con.src = ("/wrong.png");
           if(checkAlert==1)
           {
             checkAlert=2;
             if(startCheckAlert==1)
             {
               if(alertLostCon==1||alertLostCon==3)
               {
                 playAlert();
               }
             }
           }
           if(loadIP!=2&&autoNetwork==1)
           {
             getIP();
             loadIP=2;
             setTimeout("loadIP=1",30000);
           }
         }
         else
         {
           con.src = ("/world.png");
           if(checkAlert==2)
           {
             checkAlert=1;
             if(startCheckAlert==1)
             {
               if(alertLostCon==1||alertLostCon==4)
               {
                 playAlert();
               }
             }
           }
         }
         getintIPError=1;
         break;
       }
     }
   }
   catch (err)
   {
     intIPAddress="Not Connected";
     saveextIPAddress="";
     O('intIP').innerHTML = "Int. IP: "+intIPAddress;
     getintIPError=2;
     O('ip').innerHTML="Ext. IP: ----";
     O('blIP').innerHTML="Blacklisted IP Ratio: ";
   }
   if(getintIPError==2)
   {
     intIPAddress="Not Connected";
     IP[0]="";
     O('intIP').innerHTML = "Int. IP: "+intIPAddress;
     con.src = ("/wrong.png");
     if(checkAlert==1)
     {
       checkAlert=2;
       if(startCheckAlert==1)
       {
         if(alertLostCon==1||alertLostCon==3)
         {
           playAlert();
         }
       }
     }
   }
   if(autoiip==1)
   {
     clearTimeout(clearShowIIP);
     clearShowIIP = setTimeout("getIntIp()",parseInt( setiiptimer * 1000 ));
   }
   if(saveintIPAddress!=intIPAddress)
   {
     saveIPLog();
   }
 }
 function findNetworkItem()
 {
   try
   {
     var netName = objWMIService.ExecQuery("SELECT Name FROM Win32_PerfRawData_Tcpip_NetworkInterface", "WQL", wbemFlagReturnImmediately | wbemFlagForwardOnly);
     var networkInt = new Enumerator(netName);
     for (var i=0; !networkInt.atEnd();
     networkInt.moveNext())
     {
       var objItem = networkInt.item();
       var name=objItem.Name.replace("[R]","").replace("(R)","").replace("[TM]","").replace("(TM)","").replace("/"," ").replace("_"," ").replace("#"," ");
       if(networkIntName==name)
       {
         nameItem=i;
       }
       i++;
     }
   }
   catch (err)
   {
   }
 }
 function findNetworkItem3()
 {
   try
   {
     interfaceCount = NetLib.Initialize();
   }
   catch (err)
   {
     interfaceCount=0;
   }
   for (var i=0;i<interfaceCount;i++)
   {
     var name = NetLib.Description(i).replace("[R]","").replace("(R)","").replace("[TM]","").replace("(TM)","").replace("/"," ").replace("_"," ").replace("#"," ");
     if(networkIntName3==name)
     {
       nameItem3=i;
     }
   }
 }
 function fourdigits(val)
 {
   if (val >= 1000) return val.toFixed(0);
   else if (val >= 100) return val.toFixed(1);
   else if (val >= 10) return val.toFixed(2);
   else return val.toFixed(3);
 }
 function formatBytes(bytes)
 {
   if (bytes > 1125899906842624)  return fourdigits(((((bytes / 1024) / 1024) / 1024) / 1024) / 1024) + "P";
   if (bytes > 1099511627776) return fourdigits((((bytes / 1024) / 1024) / 1024) / 1024) + "T";
   if (bytes > 1073741824) return fourdigits(((bytes / 1024) / 1024) / 1024) + "G";
   if (bytes > 1048576) return fourdigits((bytes / 1024) / 1024) + "M";
   if (bytes > 1024) return fourdigits(bytes / 1024) + "k";
   return fourdigits(bytes);
 }
 function formatKBytes(bytes)
 {
   if (bytes > 1125899906842624)  return fourdigits(((((bytes / 1024) / 1024) / 1024) / 1024) / 1024) + "P";
   if (bytes > 1099511627776) return fourdigits((((bytes / 1024) / 1024) / 1024) / 1024) + "T";
   if (bytes > 1073741824) return fourdigits(((bytes / 1024) / 1024) / 1024) + "G";
   if (bytes > 1048576) return fourdigits((bytes / 1024) / 1024) + "M";
   return fourdigits(bytes / 1024) + "k";
 }
 function formatMBytes(bytes)
 {
   if (bytes > 1125899906842624)  return fourdigits(((((bytes / 1024) / 1024) / 1024) / 1024) / 1024) + "P";
   if (bytes > 1099511627776) return fourdigits((((bytes / 1024) / 1024) / 1024) / 1024) + "T";
   if (bytes > 1073741824) return fourdigits(((bytes / 1024) / 1024) / 1024) + "G";
   return fourdigits((bytes / 1024) / 1024) + "M";
 }
 function sixdigits(val)
 {
   if (val >= 100000) return val.toFixed(0);
   else if (val >= 10000) return val.toFixed(1);
   else if (val >= 1000) return val.toFixed(2);
   else if (val >= 100) return val.toFixed(3);
   else if (val >= 10) return val.toFixed(4);
   else return val.toFixed(5);
 }
 function formatBytes2(bytes)
 {
   if (bytes > 1125899906842624)  return sixdigits(((((bytes / 1024) / 1024) / 1024) / 1024) / 1024) + "P";
   if (bytes > 1099511627776) return sixdigits((((bytes / 1024) / 1024) / 1024) / 1024) + "T";
   if (bytes > 1073741824) return sixdigits(((bytes / 1024) / 1024) / 1024) + "G";
   if (bytes > 1048576) return sixdigits((bytes / 1024) / 1024) + "M";
   if (bytes > 1024) return sixdigits(bytes / 1024) + "k";
   return sixdigits(bytes);
 }
 function refreshdisplay()
 {
   bytesSentTotal += parseInt(bytesSentSec);
   bytesReceivedTotal += parseInt(bytesReceivedSec);
   bytesSentTot += parseInt(bytesSentSec);
   bytesReceivedTot += parseInt(bytesReceivedSec);
   if(saveChartData!=3)
   {
     dataSentTotal += parseInt(bytesSentSec);
     dataReceivedTotal += parseInt(bytesReceivedSec);
   }
   if (vismode == 1)
   {
     if (settimer == 1)
     {
       if (fixUnit == 1)
       {
         O('txtUpBits').innerHTML = formatBytes(bytesSentSec)+"B/s";
         O('txtDownBits').innerHTML = formatBytes(bytesReceivedSec)+"B/s";
       }
       else if (fixUnit == 2)
       {
         O('txtUpBits').innerHTML = formatKBytes(bytesSentSec)+"B/s";
         O('txtDownBits').innerHTML = formatKBytes(bytesReceivedSec)+"B/s";
       }
       else
       {
         O('txtUpBits').innerHTML = formatMBytes(bytesSentSec)+"B/s";
         O('txtDownBits').innerHTML = formatMBytes(bytesReceivedSec)+"B/s";
       }
     }
     else
     {
       bytesSentSec1 = parseInt (bytesSentSec/settimer);
       bytesReceivedSec1 = parseInt (bytesReceivedSec/settimer);
       if (fixUnit == 1)
       {
         O('txtUpBits').innerHTML = formatBytes(bytesSentSec1)+"B/s";
         O('txtDownBits').innerHTML = formatBytes(bytesReceivedSec1)+"B/s";
       }
       else if (fixUnit == 2)
       {
         O('txtUpBits').innerHTML = formatKBytes(bytesSentSec1)+"B/s";
         O('txtDownBits').innerHTML = formatKBytes(bytesReceivedSec1)+"B/s";
       }
       else
       {
         O('txtUpBits').innerHTML = formatMBytes(bytesSentSec1)+"B/s";
         O('txtDownBits').innerHTML = formatMBytes(bytesReceivedSec1)+"B/s";
       }
     }
     O('uptot').innerHTML = formatBytes(bytesSentTot)+"B";
     O('downtot').innerHTML = formatBytes(bytesReceivedTot)+"B";
     O('UpTotal').innerHTML = formatBytes2(bytesSentTotal)+"B";
     O('DownTotal').innerHTML = formatBytes2(bytesReceivedTotal)+"B";
   }
   else
   {
     if (settimer == 1)
     {
       if (fixUnit == 1)
       {
         O('txtUpBits').innerHTML = formatBytes(bytesSentSec*8)+"bit/s";
         O('txtDownBits').innerHTML = formatBytes(bytesReceivedSec*8)+"bit/s";
         O('txtUpBytes').innerHTML = formatBytes(bytesSentSec)+"B/s";
         O('txtDownBytes').innerHTML = formatBytes(bytesReceivedSec)+"B/s";
       }
       else if (fixUnit == 2)
       {
         O('txtUpBits').innerHTML = formatKBytes(bytesSentSec*8)+"bit/s";
         O('txtDownBits').innerHTML = formatKBytes(bytesReceivedSec*8)+"bit/s";
         O('txtUpBytes').innerHTML = formatKBytes(bytesSentSec)+"B/s";
         O('txtDownBytes').innerHTML = formatKBytes(bytesReceivedSec)+"B/s";
       }
       else
       {
         O('txtUpBits').innerHTML = formatMBytes(bytesSentSec*8)+"bit/s";
         O('txtDownBits').innerHTML = formatMBytes(bytesReceivedSec*8)+"bit/s";
         O('txtUpBytes').innerHTML = formatMBytes(bytesSentSec)+"B/s";
         O('txtDownBytes').innerHTML = formatMBytes(bytesReceivedSec)+"B/s";
       }
     }
     else
     {
       bytesSentSec1 = parseInt (bytesSentSec/settimer);
       bytesReceivedSec1 = parseInt (bytesReceivedSec/settimer);
       if (fixUnit == 1)
       {
         O('txtUpBits').innerHTML = formatBytes(bytesSentSec1*8)+"bit/s";
         O('txtDownBits').innerHTML = formatBytes(bytesReceivedSec1*8)+"bit/s";
         O('txtUpBytes').innerHTML = formatBytes(bytesSentSec1)+"B/s";
         O('txtDownBytes').innerHTML = formatBytes(bytesReceivedSec1)+"B/s";
       }
       else if (fixUnit == 2)
       {
         O('txtUpBits').innerHTML = formatKBytes(bytesSentSec1*8)+"bit/s";
         O('txtDownBits').innerHTML = formatKBytes(bytesReceivedSec1*8)+"bit/s";
         O('txtUpBytes').innerHTML = formatKBytes(bytesSentSec1)+"B/s";
         O('txtDownBytes').innerHTML = formatKBytes(bytesReceivedSec1)+"B/s";
       }
       else
       {
         O('txtUpBits').innerHTML = formatMBytes(bytesSentSec1*8)+"bit/s";
         O('txtDownBits').innerHTML = formatMBytes(bytesReceivedSec1*8)+"bit/s";
         O('txtUpBytes').innerHTML = formatMBytes(bytesSentSec1)+"B/s";
         O('txtDownBytes').innerHTML = formatMBytes(bytesReceivedSec1)+"B/s";
       }
     }
     O('uptot').innerHTML = formatBytes(bytesSentTot)+"B";
     O('downtot').innerHTML = formatBytes(bytesReceivedTot)+"B";
     O('UpTotal').innerHTML = formatBytes2(bytesSentTotal)+"B";
     O('DownTotal').innerHTML = formatBytes2(bytesReceivedTotal)+"B";
   }
 }
 function playAlert()
 {
   if (Player.controls.isAvailable('Stop'))
   {
   }
   else
   {
     if(soundLostCon==50)
     {
       Player.URL = soundLostConurl;
       Player.settings.volume = soundLostConVol;
       Player.Controls.play();
       if(soundLostConRepeats==1)
       {
         Player.Settings.setMode("loop",true)
       }
       else if(soundLostConRepeats==3)
       {
         Player.Settings.playCount = soundLostConCount;
       }
       alertIconRed();
     }
     else
     {
       Player.URL = System.Gadget.path + "\\alarm"+soundLostCon+".mp3";
       Player.settings.volume = soundLostConVol;
       Player.Controls.play();
       if(soundLostConRepeats==1)
       {
         Player.Settings.setMode("loop",true)
       }
       else if(soundLostConRepeats==3)
       {
         Player.Settings.playCount = soundLostConCount;
       }
       alertIconRed();
     }
   }
 }
 function alertIconRed()
 {
   if(Player.controls.isAvailable('Stop'))
   {
     O('iconAlert').style.color = "#ff0033";
     setTimeout("alertIconGreen()",500);
   }
 }
 function alertIconGreen()
 {
   O('iconAlert').style.color = sAlertIcon;
   setTimeout("alertIconRed()",500);
 }
 function clickAlert()
 {
   if (alertLostCon==1)
   {
     if (Player.controls.isAvailable('Stop'))
     {
       Player.controls.stop();
     }
     else
     {
       O('iconAlert').style.color = "#808080";
       alertLostCon="2";
       System.Gadget.Settings.write("alertLostCon", 2);
     }
   }
   else
   {
     O('iconAlert').style.color = sAlertIcon;
     alertLostCon="1";
     System.Gadget.Settings.write("alertLostCon", 1);
   }
 }
 function refreshRemaining()
 {
   if (checkadjustUsage==1)
   {
     bytesTotalTotal=0;
     checkadjustUsage=2;
     System.Gadget.Settings.write ("checkadjustUsage", checkadjustUsage);
   }
   if (checkPeakAdjustUsage==1)
   {
     bytesTotalPeak=0;
     checkPeakAdjustUsage=2;
     System.Gadget.Settings.write ("checkPeakAdjustUsage", checkPeakAdjustUsage);
   }
   if (checkOffpeakAdjustUsage==1)
   {
     bytesTotalOffPeak=0;
     checkOffpeakAdjustUsage=2;
     System.Gadget.Settings.write ("checkOffpeakAdjustUsage", checkOffpeakAdjustUsage);
   }
   today = new Date();
   if (checkRemaining == 1)
   {
     var saveday=new Date();
     var now = today.getDate();
     var month = today.getMonth();
     if (billingStarts==1)
     {
       if(billingMonth>now)
       {
         saveday.setMonth(month,billingMonth);
       }
       else
       {
         saveday.setMonth(month+1,billingMonth);
       }
     }
     else if (billingStarts==2)
     {
       var nowWeek = today.getDay();
       var totalDay = nowWeek-billingWeek;
       if(totalDay<0)
       {
         var allDay = now-totalDay;
       }
       else
       {
         var allDay = now+7-totalDay;
       }
       saveday.setDate(allDay);
     }
     saveday.setHours(0,0,0);
     billingDate = Date.parse(saveday);
     System.Gadget.Settings.write("billingDate",billingDate);
     checkRemaining=2;
     System.Gadget.Settings.write("checkRemaining", checkRemaining);
   }
   if(billingDate<=today.getTime())
   {
     var now = today.getDate();
     var month = today.getMonth();
     var saveday=new Date();
     if (billingStarts==1)
     {
       if(billingMonth<now)
       {
         saveday.setMonth(month,billingMonth);
       }
       else
       {
         saveday.setMonth(month+1,billingMonth);
       }
     }
     else if (billingStarts==2)
     {
       var nowWeek = today.getDay();
       var totalDay = nowWeek-billingWeek;
       if(totalDay<0)
       {
         var allDay = now-totalDay;
       }
       else
       {
         var allDay = now+7-totalDay;
       }
       saveday.setDate(allDay);
     }
     saveday.setHours(0,0,0);
     billingDate = Date.parse(saveday);
     System.Gadget.Settings.write("billingDate",billingDate);
     bytesTotalTotal=0;
     bytesTotalPeak=0;
     bytesTotalOffPeak=0;
   }
   var gap = billingDate - today.getTime();
   gap = Math.floor(gap / (1000 * 60 * 60 * 24))+1;
   O('rDays').innerHTML = gap;
   var month2=new Array(12);
   month2[0]="Jan";
   month2[1]="Feb";
   month2[2]="Mar";
   month2[3]="Apr";
   month2[4]="May";
   month2[5]="Jun";
   month2[6]="Jul";
   month2[7]="Aug";
   month2[8]="Sep";
   month2[9]="Oct";
   month2[10]="Nov";
   month2[11]="Dec";
   var setdaytime=new Date();
   setdaytime.setTime(billingDate);
   O('remainingDate').innerHTML = month2[setdaytime.getMonth()]+"-"+setdaytime.getDate();
   if (setRemaining==1)
   {
     readQuota=readTotalTotal;
     bytesTotalTotal += parseInt(bytesSentSec+bytesReceivedSec);
   }
   else if (setRemaining==3)
   {
     var getTodayTime = today.getTime();
     var getTodayDate = Date.parse(months[today.getMonth()]+" "+today.getDate()+", "+today.getFullYear());
     getPeak = parseInt(getPeakHour+getPeakMin+getTodayDate);
     getOffpeak = parseInt(getOffpeakHour+getOffpeakMin+getTodayDate);
     if(getPeak<=getTodayTime&&getOffpeak>=getTodayTime)
     {
       bytesTotalPeak += parseInt(bytesSentSec+bytesReceivedSec);
       O('peak').innerHTML = "Peak : "+ formatBytes(bytesTotalPeak)+"B";
       O('offPeak').innerHTML = "Off Peak : "+formatBytes(bytesTotalOffPeak)+"B";
     }
     else
     {
       bytesTotalOffPeak += parseInt(bytesSentSec+bytesReceivedSec);
       O('peak').innerHTML = "Peak : "+formatBytes(bytesTotalPeak)+"B";
       O('offPeak').innerHTML = "Off Peak : "+formatBytes(bytesTotalOffPeak)+"B";
     }
     readQuota=parseInt(readPeakTotal+readOffpeakTotal);
     bytesTotalTotal=parseInt(bytesTotalPeak+bytesTotalOffPeak);
     if(offpeakCycleQuotaSize!=4)
     {
       if(readPeakTotal<=bytesTotalPeak)
       {
         O('peak').style.color = "#E2003B";
       }
       if(readOffpeakTotal<=bytesTotalOffPeak)
       {
         O('offPeak').style.color = "#E2003B";
       }
     }
   }
   O('usedTotal').innerHTML = formatBytes(bytesTotalTotal)+"B";
   remainingTotal = parseInt(( bytesTotalTotal / readQuota)* 100);
   if (remainingTotal<100)
   {
     O('bar4').style.color = sDownlo;
     O('bar4').style.width = parseInt(( remainingTotal / 2 ) * size );
     O('remainingPre').innerHTML = parseInt( 100 - remainingTotal ) + "%";
     O('rQuota').innerHTML = formatBytes(readQuota - bytesTotalTotal)+"B";
     O('rPreDay').innerHTML = formatBytes((readQuota - bytesTotalTotal) / gap)+"B";
     O('bar4').style.color = sRemUsed;
   }
   else
   {
     O('rQuota').innerHTML = "0B";
     O('rPreDay').innerHTML = "0B";
     O('bar4').style.color = "#E2003B";
     O('bar4').style.width = parseInt( 50 * size );
     O('remainingPre').style.color = "#E2003B";
     O('remainingPre').innerHTML = "0%";
   }
 }
 function initHist()
 {
   for (var i=0; i<maxHist; i++)
   {
     sentHist[i]=0;
     receivedHist[i]=0;
   }
 }
 function getMaxValue()
 {
   var max = 0;
   for (var i=0; i<maxHist; i++)
   {
     if (sentHist[i] > max) max = sentHist[i];
     if (receivedHist[i] > max) max = receivedHist[i];
   }
   return max;
 }
 function updateGraph()
 {
   var downpath = "m 0,30 ";
   var uppath = "m 0,30 ";
   var ub = 0;
   var db = 0;
   var param="";
   var max = 0;
   var v_max = getMaxValue();
   max = scalesize*1024;
   if ( (v_max > max) || (scaletype == 0) ) max = v_max;
   else  max = scalesize*1024;
   try
   {
     if (vismode == 1)
     {
       if ( barup == 0 )
       {
         O('bar1').style.color = sUplo;
         O('bar1').style.width = parseInt( 50 / (max / bytesSentSec) * size );
       }
       else if ( bytesSentSec > barup )
       {
         O('bar1').style.color = "#e2003b";
         O('bar1').style.width = 50;
       }
       else
       {
         O('bar1').style.color = sUplo;
         O('bar1').style.width = parseInt( 50 / (barup / bytesSentSec) * size );
       }
       if ( bardown == 0 )
       {
         O('bar2').style.color = sDownlo;
         O('bar2').style.width = parseInt( 50 / (max / bytesReceivedSec) * size );
       }
       else if ( bytesReceivedSec > bardown )
       {
         O('bar2').style.color = "#e2003b";
         O('bar2').style.width = 50;
       }
       else
       {
         O('bar2').style.color = sDownlo;
         O('bar2').style.width = parseInt( 50 / (bardown / bytesReceivedSec) * size );
       }
     }
     for (var i=0; i<maxHist; i++)
     {
       ub = parseInt( 30 / (max / sentHist[i]) ) ;
       db = parseInt( 30 / (max / receivedHist[i]) );
       if (ub > 30) ub = 30;
       if (db > 30) db = 30;
       if (drawstyle==1)
       {
         param = (ub == 0) ? "m" : "l";
         uppath += " m " + parseInt(i+1) + ",30 " + param + " " + parseInt(i+1) + "," +parseInt(30-(ub)) + "";
         param = (db == 0) ? "m" : "l";
         downpath += " m " + parseInt(i+1) + ",30 " + param + " " + parseInt(i+1) + "," +parseInt(30-(db)) + "";
       }
       else
       {
         uppath += " l " + parseInt(i+1) + "," +parseInt(30-(ub));
         downpath += " l " + parseInt(i+1) + "," +parseInt(30-(db));
       }
     }
   }
   catch(err)
   {
   }
   if (vismode == 1)
   {
     if(bytesSentSec==0)
     {
       O('bar1').style.visibility = "hidden";
     }
     else
     {
       O('bar1').style.visibility = "visible";
     }
     if (bytesReceivedSec==0)
     {
       O('bar2').style.visibility = "hidden";
     }
     else
     {
       O('bar2').style.visibility = "visible";
     }
   }
   else
   {
     O('bar1').style.visibility = "hidden";
     O('bar2').style.visibility = "hidden";
   }
   if(graph == 1)
   {
     uppath = uppath + " e";
     downpath = downpath + " e";
     downgraph.path = downpath;
     upgraph.path= uppath;
   }
 }
 function getDNSBL()
 {
   getBL('http://addgadget.com/ip_blacklist/ratio2.php?ipaddr='+IP[0]);
 }
 function getBL(DNSBL)
 {
   if(xmlReq2)
   {
     xmlReq2.abort();
   }
   xmlReq2 = new XMLHttpRequest();
   xmlReq2.open("GET", DNSBL);
   xmlReq2.setRequestHeader("If-Modified-Since", "Sat, 1 Jan 2000 00:00:00 GMT");
   xmlReq2.onreadystatechange = retrievedBL;
   xmlReq2.send(null);
 }
 function retrievedBL()
 {
   if (xmlReq2.readyState == 4 && xmlReq2.status == 200)
   {
     blRatio = xmlReq2.responseText;
     blRatioTotal = blRatio.split(",");
     O('blIP').innerHTML = "Blacklisted IP Ratio: "+blRatioTotal[0];
     System.Gadget.Settings.write("blRatio", blRatio);
     xmlReq2.abort();
     xmlReq2 = null;
   }
 }
 function getIP()
 {
   if (showextip!=2)
   {
     randomNumber=Math.floor(Math.random() * 13) + 1;
     if(randomNumber==1)
     {
       getURL('http://addgadgets.com/ip.php')
     }
     else if(randomNumber==2)
     {
       getURL('http://checkip.dyndns.org')
     }
     else if(randomNumber==3)
     {
       getURL('http://whatismyipaddress.com')
     }
     else if(randomNumber==4)
     {
       getURL('http://ip-lookup.net')
     }
     else if(randomNumber==5)
     {
       getURL('http://ipaddress.com')
     }
     else if(randomNumber==6)
     {
       getURL('http://www.ip-adress.com')
     }
     else if(randomNumber==7)
     {
       getURL('http://www.ipchicken.com')
     }
     else if(randomNumber==8)
     {
       getURL('http://www.infosniper.net')
     }
     else if(randomNumber==9)
     {
       getURL('http://www.hostip.info')
     }
     else if(randomNumber==10)
     {
       getURL('http://www.whatsmyip.us')
     }
     else if(randomNumber==11)
     {
       getURL('http://www.showmemyip.com')
     }
     else if(randomNumber==12)
     {
       getURL('http://www.123myip.co.uk')
     }
     else if(randomNumber==13)
     {
       getURL('http://www.myiponline.com')
     }
     if (autoeip == 1)
     {
       clearTimeout(clearGetIP);
       clearGetIP=setTimeout("getIP()",parseInt( seteiptimer * 60000 ));
     }
   }
 }
 function getURL(url)
 {
   if(xmlReq)
   {
     xmlReq.abort();
   }
   xmlReq = new XMLHttpRequest();
   xmlReq.open("GET", url);
   xmlReq.setRequestHeader("If-Modified-Since", "Sat, 1 Jan 2000 00:00:00 GMT");
   xmlReq.onreadystatechange = retrievedData;
   xmlReq.send(null);
 }
 function retrievedData()
 {
   if (xmlReq.readyState == 4 && xmlReq.status == 200)
   {
     var urlData = xmlReq.responseText;
     var re =/\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)/;
     rv = new String([urlData.match(re)]);
     IP = rv.split(",");
     if(IP[0]=='')
     {
       if(getIPTime<=5)
       {
         clearTimeout(clearGetIP);
         clearGetIP=setTimeout("getIP()",3000);
         getIPTime=getIPTime+1;
       }
     }
     else
     {
       O('ip').innerHTML = "Ext. IP: "+IP[0];
       if(saveextIPAddress!=IP[0])
       {
         saveIPLog();
       }
       eipStatus=2;
       con.src = ("/world.png");
       if(checkAlert==2)
       {
         checkAlert=1;
         if(startCheckAlert==1)
         {
           if(alertLostCon==1||alertLostCon==4)
           {
             playAlert();
           }
         }
       }
       if(showBlacklisted==1)
       {
         if(saveextip!=IP[0])
         {
           O('blIP').innerHTML = "Blacklisted IP Ratio: Loading";
           getDNSBL();
           saveextip=IP[0];
           System.Gadget.Settings.write("saveextip", saveextip);
         }
         else
         {
           O('blIP').innerHTML = "Blacklisted IP Ratio: "+blRatioTotal[0];
         }
       }
     }
     getIPTime=1;
     xmlReq.abort();
     xmlReq = null;
   }
   else if(xmlReq.readyState == 4)
   {
     if(getIPTime>=5)
     {
       eipStatus=1;
       con.src = ("/wrong.png");
       if(checkAlert==1)
       {
         checkAlert=2;
         if(startCheckAlert==1)
         {
           if(alertLostCon==1||alertLostCon==3)
           {
             playAlert();
           }
         }
       }
       if (xmlReq.status==12029 || xmlReq.status==12002 || xmlReq.status==12007 || xmlReq.status==12030 || xmlReq.status==12031)
       {
         O('ip').innerHTML = "Ext. IP: No Connection";
       }
       else
       {
         O('ip').innerHTML = "Ext. IP: Error " + xmlReq.status;
       }
       IP[0]="";
       if(saveextIPAddress!=IP[0])
       {
         saveIPLog();
       }
     }
     xmlReq.abort();
     xmlReq = null;
     if(autoNetwork==1&&getIPTime<=5)
     {
       clearTimeout(clearGetIP);
       clearGetIP=setTimeout("getIP()",10000);
       getIPTime=getIPTime+1;
     }
   }
 }
 function ShowIP()
 {
   getIP();
   getIntIp();
 }
 function copyIIP()
 {
   window.clipboardData.setData('Text',intIPAddress);
 }
 function copyEIP()
 {
   var myVar = O('ip').innerHTML.replace("Ext. IP: ","");
   window.clipboardData.setData('text',myVar);
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
   var urlData=getURL2("http://addgadgets.com/network_meter/version.htm");
   if(urlData===false)
   {
     return false
   }
   var version="9.6";
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
     sizeUpdate = 1;
     sizeMode();
   }
 }
 function timedMsg()
 {
   if (update == 1)
   {
     setTimeout("showUpdate2()",60000);
   }
   else
   {
     sizeUpdate = 2;
     O('newUpdate').style.visibility = "hidden";
   }
 }
 function saveTotal()
 {
   System.Gadget.Settings.write ("saveTotalSend", bytesSentTotal);
   System.Gadget.Settings.write ("saveTotalReceived", bytesReceivedTotal);
   System.Gadget.Settings.write ("saveTotalTot", bytesTotalTotal);
   System.Gadget.Settings.write ("saveTotalPeak", bytesTotalPeak);
   System.Gadget.Settings.write ("saveTotalOffPeak", bytesTotalOffPeak);
   if(saveChartData!=3)
   {
     System.Gadget.Settings.write ("savedataSentTotal", dataSentTotal);
     System.Gadget.Settings.write ("savedataReceivedTotal", dataReceivedTotal);
   }
 }
 function writeUsage()
 {
   var inifilename = System.Environment.getEnvironmentVariable("APPDATA") + "\\" + System.Gadget.name + "_Usage.ini";
   try
   {
     var inifile = fso.OpenTextFile(inifilename, 2, true);
     try
     {
       inifile.WriteLine(bytesSentTotal);
       inifile.WriteLine(bytesReceivedTotal);
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
 function readUsage()
 {
   if(readIniUsage==1)
   {
     var inifilename = System.Environment.getEnvironmentVariable("APPDATA") + "\\" + System.Gadget.name + "_Usage.ini";
     try
     {
       var inifile = fso.OpenTextFile(inifilename, 1);
       try
       {
         readSend = inifile.ReadLine();
         readReceived = inifile.ReadLine();
         if (readSend != "") bytesSentTotal = parseInt(readSend);
         if (readReceived != "") bytesReceivedTotal = parseInt(readReceived);
       }
       finally
       {
         inifile.Close();
       }
     }
     catch (err)
     {
     }
     System.Gadget.Settings.write("readIniUsage", 2);
   }
   else
   {
     var readReceived = System.Gadget.Settings.read ("saveTotalReceived");
     if (readReceived != "") bytesReceivedTotal = parseInt(readReceived);
     var readSend = System.Gadget.Settings.read ("saveTotalsend");
     if (readSend != "") bytesSentTotal = parseInt(readSend);
   }
 }
 function clickResetCurrent()
 {
   bytesSentTot = 0;
   bytesReceivedTot = 0;
 }
 function clickResetTotal()
 {
   bytesSentTotal = 0;
   bytesReceivedTotal = 0;
 }
 function sizeMode()
 {
   var sizeLine = new Array('1','2','3','4','5','6','7','8','9','10','11','12','13','14','15','16','17','18','19','20','21','22');
   O('networkAdaName').style.visibility = "visible";
   O('networkid').style.visibility = "visible";
   O('intIP').style.visibility = "visible";
   O('ip').style.visibility = "visible";
   O('refreshe').style.visibility = "visible";
   O('blIP').style.visibility = "visible";
   O('lookup').style.visibility = "visible";
   O('speed').style.visibility = "visible";
   O('iplog').style.visibility = "visible";
   O('firewall').style.visibility = "visible";
   O('profile').style.visibility = "visible";
   O('secure').style.visibility = "visible";
   O('signalper').style.visibility = "visible";
   O('back3').style.visibility = "visible";
   O('bar3').style.visibility = "visible";
   O('top3').style.visibility = "visible";
   O('txtUpBits').style.visibility = "visible";
   O('txtUpBytes').style.visibility = "visible";
   O('txtDownBits').style.visibility = "visible";
   O('txtDownBytes').style.visibility = "visible";
   O('linesx').style.visibility = "visible";
   O('linesy').style.visibility = "visible";
   O('downgraph').style.visibility = "visible";
   O('upgraph').style.visibility = "visible";
   O('graphImg').style.visibility = "visible";
   O('border').style.visibility = "visible";
   O('current').style.visibility = "visible";
   O('resetCurrent').style.visibility = "visible";
   O('total').style.visibility = "visible";
   O('resetTotal').style.visibility = "visible";
   O('arrowup2').style.visibility = "visible";
   O('arrowdown2').style.visibility = "visible";
   O('uptot').style.visibility = "visible";
   O('downtot').style.visibility = "visible";
   O('arrowup3').style.visibility = "visible";
   O('arrowdown3').style.visibility = "visible";
   O('UpTotal').style.visibility = "visible";
   O('DownTotal').style.visibility = "visible";
   O('hr1').style.visibility = "visible";
   O('remaining').style.visibility = "visible";
   O('remainingPre').style.visibility = "visible";
   O('remainingDate').style.visibility = "visible";
   O('days').style.visibility = "visible";
   O('rDays').style.visibility = "visible";
   O('quota').style.visibility = "visible";
   O('rQuota').style.visibility = "visible";
   O('perDay').style.visibility = "visible";
   O('rPreDay').style.visibility = "visible";
   O('used').style.visibility = "visible";
   O('usedTotal').style.visibility = "visible";
   O('back4').style.visibility = "visible";
   O('bar4').style.visibility = "visible";
   O('top4').style.visibility = "visible";
   O('peak').style.visibility = "visible";
   O('offPeak').style.visibility = "visible";
   if (showNetworkAdaName == 2)
   {
     O('networkAdaName').style.visibility = "hidden";
     sizeLine[0]=0;
   }
   if (networkType==1||netType==1)
   {
     O('networkid').style.visibility = "hidden";
     O('secure').style.visibility = "hidden";
     O('signalper').style.visibility = "hidden";
     O('bar3').style.visibility = "hidden";
     O('back3').style.visibility = "hidden";
     O('top3').style.visibility = "hidden";
     sizeLine[1]=0;
     sizeLine[7]=0;
     sizeLine[8]=0;
   }
   if (showInternalIP == 2)
   {
     O('intIP').style.visibility = "hidden";
     sizeLine[2]=0;
   }
   if (showextip == 2)
   {
     O('ip').style.visibility = "hidden";
     O('refreshe').style.visibility = "hidden";
     sizeLine[3]=0;
   }
   if(showBlacklisted==2)
   {
     O('blIP').style.visibility = "hidden";
     sizeLine[4]=0;
   }
   if(showSTIL==2)
   {
     O('lookup').style.visibility = "hidden";
     O('speed').style.visibility = "hidden";
     O('iplog').style.visibility = "hidden";
     sizeLine[5]=0;
   }
   if(saveIPAddress==2)
   {
     O('iplog').style.visibility = "hidden";
   }
   if(showFirewall==2)
   {
     O('firewall').style.visibility = "hidden";
     O('profile').style.visibility = "hidden";
     sizeLine[6]=0;
   }
   if (vismode == 1)
   {
     O('back1').style.visibility = "visible";
     O('back2').style.visibility = "visible";
     O('top1').style.visibility = "visible";
     O('top2').style.visibility = "visible";
     O('txtDownBytes').style.visibility = "hidden";
     O('txtUpBytes').style.visibility = "hidden";
   }
   else
   {
     O('back1').style.visibility = "hidden";
     O('back2').style.visibility = "hidden";
     O('top1').style.visibility = "hidden";
     O('top2').style.visibility = "hidden";
     O('txtDownBytes').style.visibility = "visible";
     O('txtUpBytes').style.visibility = "visible";
   }
   if (graph == 2)
   {
     sizegraph = -11;
     O('bar1').style.visibility = "hidden";
     O('bar2').style.visibility = "hidden";
     O('linesx').style.visibility = "hidden";
     O('linesy').style.visibility = "hidden";
     O('downgraph').style.visibility = "hidden";
     O('upgraph').style.visibility = "hidden";
     O('graphImg').style.visibility = "hidden";
     O('border').style.visibility = "hidden";
   }
   else
   {
     sizegraph = 24;
   }
   if (totalbandwidth == 2)
   {
     O('current').style.visibility = "hidden";
     O('resetCurrent').style.visibility = "hidden";
     O('total').style.visibility = "hidden";
     O('resetTotal').style.visibility = "hidden";
     O('arrowup2').style.visibility = "hidden";
     O('arrowdown2').style.visibility = "hidden";
     O('uptot').style.visibility = "hidden";
     O('downtot').style.visibility = "hidden";
     O('arrowup3').style.visibility = "hidden";
     O('arrowdown3').style.visibility = "hidden";
     O('UpTotal').style.visibility = "hidden";
     O('DownTotal').style.visibility = "hidden";
     O('hr1').style.visibility = "hidden";
     sizeLine[12]=0;
     sizeLine[13]=0;
     sizeLine[14]=0;
   }
   if(setRemaining==1)
   {
     O('peak').style.visibility = "hidden";
     O('offPeak').style.visibility = "hidden";
     sizeLine[19]=0;
   }
   else if(setRemaining==3)
   {
     O('used').style.visibility = "hidden";
     O('usedTotal').style.visibility = "hidden";
     O('back4').style.visibility = "hidden";
     O('bar4').style.visibility = "hidden";
     O('top4').style.visibility = "hidden";
   }
   else
   {
     O('hr1').style.visibility = "hidden";
     O('remaining').style.visibility = "hidden";
     O('remainingPre').style.visibility = "hidden";
     O('remainingDate').style.visibility = "hidden";
     O('days').style.visibility = "hidden";
     O('rDays').style.visibility = "hidden";
     O('quota').style.visibility = "hidden";
     O('rQuota').style.visibility = "hidden";
     O('perDay').style.visibility = "hidden";
     O('rPreDay').style.visibility = "hidden";
     O('used').style.visibility = "hidden";
     O('usedTotal').style.visibility = "hidden";
     O('back4').style.visibility = "hidden";
     O('bar4').style.visibility = "hidden";
     O('top4').style.visibility = "hidden";
     O('peak').style.visibility = "hidden";
     O('offPeak').style.visibility = "hidden";
     sizeLine[15]=0;
     sizeLine[16]=0;
     sizeLine[17]=0;
     sizeLine[18]=0;
     sizeLine[19]=0;
   }
   if(showSearch==1)
   {
     O('query').style.visibility = "visible";
     O('go').style.visibility = "visible";
     addSearch=5;
   }
   else
   {
     O('query').style.visibility = "hidden";
     O('go').style.visibility = "hidden";
     addSearch=0;
     sizeLine[20]=0;
   }
   if(sizeUpdate==2)
   {
     sizeLine[21]=0;
   }
   for (var i=0; i<22; i++)
   {
     if(sizeLine[21-i]==0)
     {
       for (var h=22-i; h<22; h++)
       {
         sizeLine[h]=parseInt(sizeLine[h]-1);
       }
     }
   }
   O('wpng').style.top = parseInt( 5 * size );
   O('network').style.top = parseInt( 6 * size );
   O('con').style.top = parseInt( 6 * size );
   O('networkAdaName').style.top = parseInt(((sizeLine[0]*11)+9)*size);
   O('networkid').style.top = parseInt(((sizeLine[1]*11)+9)*size);
   O('intIP').style.top = parseInt(((sizeLine[2]*11)+9)*size);
   O('ip').style.top = parseInt(((sizeLine[3]*11)+9)*size);
   O('refreshe').style.top = parseInt(((sizeLine[3]*11)+7)*size);
   O('blIP').style.top = parseInt(((sizeLine[4]*11)+9)*size);
   O('speed').style.top = parseInt(((sizeLine[5]*11)+9)*size);
   O('lookup').style.top = parseInt(((sizeLine[5]*11)+9)*size);
   O('iplog').style.top = parseInt(((sizeLine[5]*11)+9)*size);
   O('firewall').style.top = parseInt(((sizeLine[6]*11)+9)*size);
   O('profile').style.top = parseInt(((sizeLine[6]*11)+9)*size);
   O('secure').style.top = parseInt(((sizeLine[7]*11)+9)*size);
   O('signalper').style.top = parseInt(((sizeLine[8]*11)+9)*size);
   O('back3').style.top = parseInt(((sizeLine[8]*11)+13)*size);
   O('bar3').style.top = parseInt(((sizeLine[8]*11)+13)*size);
   O('top3').style.top = parseInt(((sizeLine[8]*11)+13)*size);
   O('arrowup').style.top = parseInt(((sizeLine[9]*11)+8)*size);
   O('txtUpBits').style.top = parseInt(((sizeLine[9]*11)+9)*size);
   O('txtUpBytes').style.top = parseInt(((sizeLine[9]*11)+9)*size);
   O('back1').style.top = parseInt(((sizeLine[9]*11)+13)*size);
   O('bar1').style.top = parseInt(((sizeLine[9]*11)+13)*size);
   O('top1').style.top = parseInt(((sizeLine[9]*11)+13)*size);
   O('arrowdown').style.top = parseInt(((sizeLine[10]*11)+8)*size);
   O('txtDownBits').style.top = parseInt(((sizeLine[10]*11)+9)*size);
   O('txtDownBytes').style.top = parseInt(((sizeLine[10]*11)+9)*size);
   O('back2').style.top = parseInt(((sizeLine[10]*11)+13)*size);
   O('bar2').style.top = parseInt(((sizeLine[10]*11)+13)*size);
   O('top2').style.top = parseInt(((sizeLine[10]*11)+13)*size);
   O('linesx').style.top = parseInt(((sizeLine[11]*11)+13)*size);
   O('linesy').style.top = parseInt(((sizeLine[11]*11)+13)*size);
   O('downgraph').style.top = parseInt(((sizeLine[11]*11)+13)*size);
   O('upgraph').style.top = parseInt(((sizeLine[11]*11)+13)*size);
   O('graphImg').style.top = parseInt(((sizeLine[11]*11)+13)*size);
   O('border').style.top = parseInt(((sizeLine[11]*11)+13)*size);
   O('current').style.top = parseInt(((sizeLine[12]*11)+9+sizegraph)*size);
   O('resetCurrent').style.top = parseInt(((sizeLine[12]*11)+8+sizegraph)*size);
   O('total').style.top = parseInt(((sizeLine[12]*11)+9+sizegraph)*size);
   O('resetTotal').style.top = parseInt(((sizeLine[12]*11)+8+sizegraph)*size);
   O('uptot').style.top = parseInt(((sizeLine[13]*11)+9+sizegraph)*size);
   O('downtot').style.top = parseInt(((sizeLine[14]*11)+9+sizegraph)*size);
   O('UpTotal').style.top = parseInt(((sizeLine[13]*11)+9+sizegraph)*size);
   O('DownTotal').style.top = parseInt(((sizeLine[14]*11)+9+sizegraph)*size);
   O('arrowup2').style.top = parseInt(((sizeLine[13]*11)+8+sizegraph)*size);
   O('arrowdown2').style.top = parseInt(((sizeLine[14]*11)+8+sizegraph)*size);
   O('arrowup3').style.top = parseInt(((sizeLine[13]*11)+8+sizegraph)*size);
   O('arrowdown3').style.top = parseInt(((sizeLine[14]*11)+8+sizegraph)*size);
   O('hr1').style.top = parseInt(((sizeLine[14]*11)+10+sizegraph)*size);
   O('remaining').style.top = parseInt(((sizeLine[15]*11)+12+sizegraph)*size);
   O('remainingPre').style.top = parseInt(((sizeLine[15]*11)+12+sizegraph)*size);
   O('remainingDate').style.top = parseInt(((sizeLine[15]*11)+12+sizegraph)*size);
   O('days').style.top = parseInt(((sizeLine[16]*11)+12+sizegraph)*size);
   O('quota').style.top = parseInt(((sizeLine[16]*11)+12+sizegraph)*size);
   O('perDay').style.top = parseInt(((sizeLine[16]*11)+12+sizegraph)*size);
   O('rDays').style.top = parseInt(((sizeLine[17]*11)+12+sizegraph)*size);
   O('rQuota').style.top = parseInt(((sizeLine[17]*11)+12+sizegraph)*size);
   O('rPreDay').style.top = parseInt(((sizeLine[17]*11)+12+sizegraph)*size);
   O('used').style.top = parseInt(((sizeLine[18]*11)+12+sizegraph)*size);
   O('usedTotal').style.top = parseInt(((sizeLine[18]*11)+12+sizegraph)*size);
   O('back4').style.top = parseInt(((sizeLine[18]*11)+16+sizegraph)*size);
   O('bar4').style.top = parseInt(((sizeLine[18]*11)+16+sizegraph)*size);
   O('top4').style.top = parseInt(((sizeLine[18]*11)+16+sizegraph)*size);
   O('peak').style.top = parseInt(((sizeLine[18]*11)+12+sizegraph)*size);
   O('offPeak').style.top = parseInt(((sizeLine[19]*11)+12+sizegraph)*size);
   O('query').style.top = parseInt(((sizeLine[20]*11)+13+sizegraph)*size);
   O('go').style.top = parseInt(((sizeLine[20]*11)+13+sizegraph)*size);
   O('newUpdate').style.top = parseInt(((sizeLine[21]*11)+12+sizegraph+addSearch)*size);
   for (var i=0; i<22; i++)
   {
     if(sizeLine[21-i]>=1)
     {
       totalLine=sizeLine[21-i];
       break;
     }
   }
   totalHeight=parseInt((totalLine*11)+28+sizegraph+addSearch);
   document.getElementById("query").style.left = parseInt(4 * size);
   document.getElementById("query").style.width = parseInt(109 * size);
   document.getElementById("query").style.height = parseInt(15 * size);
   document.getElementById("query").style.fontSize = parseInt(10 * size);
   document.getElementById("go").style.left = parseInt(111 * size);
   document.getElementById("go").style.width = parseInt(15 * size);
   document.getElementById("go").style.height = parseInt(15 * size);
   if(totalHeight>=217)
   {
     background.src = "bg_239.png";
   }
   else if(totalHeight>=173)
   {
     background.src = "bg_195.png";
   }
   else if(totalHeight>=129)
   {
     background.src = "bg_151.png";
   }
   else if(totalHeight>=85)
   {
     background.src = "bg_107.png";
   }
   else
   {
     background.src = "bg_63.png";
   }
   document.body.style.height = Math.round(totalHeight*size);
   O('background').style.height = Math.round(totalHeight*size);
   document.body.style.width = Math.round( 130 * size );
   O('background').style.width = Math.round( 130 * size );
   O('backgc').style.background = sBackg;
   O('backgc').style.top = Math.round( 3 * size );
   O('backgc').style.left = Math.round( 3 * size );
   O('backgc').style.width = Math.round( 124 * size );
   O('backgc').style.height = Math.round( ( totalHeight - 6 ) * size );
   O('blackwhite').style.top = Math.round( 3 * size );
   O('blackwhite').style.left = Math.round( 3 * size );
   O('blackwhite').style.width = Math.round( 124 * size );
   O('blackwhite').style.height = Math.round( ( totalHeight - 6 ) * size );
   O('hr1').style.width = parseInt( 118 * size );
   O('hr1').style.left = parseInt( 6 * size );
   O('remaining').style.left = parseInt( 6 * size );
   O('remainingPre').style.left = parseInt( 53 * size );
   O('remainingDate').style.left = parseInt( 90 * size );
   O('days').style.left = parseInt( 12 * size );
   O('quota').style.left = parseInt( 50 * size );
   O('perDay').style.left = parseInt( 88 * size );
   O('rDays').style.left = parseInt( 17 * size );
   O('rQuota').style.left = parseInt( 44 * size );
   O('rPreDay').style.left = parseInt( 87 * size );
   O('used').style.left = parseInt( 6 * size );
   O('usedTotal').style.left = parseInt( 31 * size );
   O('back4').style.left = parseInt( 72 * size );
   O('bar4').style.left = parseInt( 72 * size );
   O('top4').style.left = parseInt( 72 * size );
   O('peak').style.left = parseInt( 3 * size );
   O('offPeak').style.left = parseInt( 3 * size );
   O('peak').style.width = parseInt( 124 * size );
   O('offPeak').style.width = parseInt( 124 * size );
   O('network').style.color = sTitle;
   O('networkAdaName').style.color = sNetIN;
   O('networkid').style.color = sSSID;
   O('intIP').style.color = sINTIP;
   O('ip').style.color = sEXTIP;
   O('blIP').style.color = sBLIP;
   O('iplog').style.color = sBut;
   O('firewall').style.color = sFWPF;
   O('profile').style.color = sFWPF;
   O('secure').style.color = sConn;
   O('signalper').style.color = sSign;
   O('bar3').style.color = sSign;
   O('txtUpBits').style.color = sUplo;
   O('txtUpBytes').style.color = sUplo;
   O('bar1').style.color = sUplo;
   O('upgraph').strokecolor = sUplo;
   O('upgraph').fillcolor = sUplo;
   O('txtDownBits').style.color = sDownlo;
   O('txtDownBytes').style.color = sDownlo;
   O('bar2').style.color = sDownlo;
   O('downgraph').strokecolor = sDownlo;
   O('downgraph').fillcolor = sDownlo;
   O('current').style.color = sCurr;
   O('uptot').style.color = sCurrUp;
   O('downtot').style.color = sCurrDown;
   O('total').style.color = sTota;
   O('UpTotal').style.color = sTotaUp;
   O('DownTotal').style.color = sTotaDown;
   O('remaining').style.color = sRemPercent;
   O('remainingPre').style.color = sRemPercent;
   O('remainingDate').style.color = sRemPercent;
   O('days').style.color = sRemDays;
   O('rDays').style.color = sRemDays;
   O('quota').style.color = sRemQuota;
   O('rQuota').style.color = sRemQuota;
   O('perDay').style.color = sRemPerDay;
   O('rPreDay').style.color = sRemPerDay;
   O('used').style.color = sRemUsed;
   O('usedTotal').style.color = sRemUsed;
   O('bar4').style.color = sRemUsed;
   O('peak').style.color = sPeakUsed;
   O('offPeak').style.color = sOffpeakUsed;
   document.linkColor = sBut;
   document.vlinkColor = sBut;
   document.alinkColor = sBut;
   var gbleft= parseInt( 7 * size );
   O('linesx').style.left = gbleft;
   O('linesy').style.left = gbleft;
   O('border').style.left = gbleft;
   O('graphImg').style.left = gbleft;
   O('downgraph').style.left = gbleft;
   O('upgraph').style.left = gbleft;
   O('wpng').style.left = parseInt( 4 * size );
   O('network').style.left = parseInt( 23 * size );
   O('con').style.left = parseInt( 112 * size );
   O('networkAdaName').style.left = parseInt( 6 * size );
   O('networkid').style.left = parseInt( 6 * size );
   O('intIP').style.left = parseInt( 6 * size );
   O('ip').style.left = parseInt( 6 * size );
   O('blIP').style.left = parseInt( 6 * size );
   O('refreshe').style.left = parseInt( 110 * size );
   O('speed').style.left = parseInt( 6 * size );
   O('lookup').style.left = parseInt( 86 * size );
   O('iplog').style.left = parseInt( 61 * size );
   O('firewall').style.left = parseInt( 6 * size );
   O('profile').style.left = parseInt( 64 * size );
   O('secure').style.left = parseInt( 6 * size );
   O('signalper').style.left = parseInt( 6 * size );
   O('bar3').style.left = parseInt( 72 * size );
   O('back3').style.left = parseInt( 72 * size );
   O('top3').style.left = parseInt( 72 * size );
   O('txtUpBits').style.left = parseInt( 19 * size );
   O('txtUpBytes').style.left = parseInt( 75 * size );
   O('txtDownBits').style.left = parseInt( 19 * size );
   O('txtDownBytes').style.left = parseInt( 75 * size );
   O('back1').style.left = parseInt( 72 * size );
   O('bar1').style.left = parseInt( 72 * size );
   O('top1').style.left = parseInt( 72 * size );
   O('back2').style.left = parseInt( 72 * size );
   O('bar2').style.left = parseInt( 72 * size );
   O('top2').style.left = parseInt( 72 * size );
   O('arrowup').style.left = parseInt( 5 * size );
   O('arrowdown').style.left = parseInt( 5 * size );
   O('arrowup2').style.left = parseInt( 5 * size );
   O('arrowdown2').style.left = parseInt( 5 * size );
   O('arrowup3').style.left = parseInt( 61 * size );
   O('arrowdown3').style.left = parseInt( 61 * size );
   O('resetCurrent').style.left = parseInt( 39 * size );
   O('resetTotal').style.left = parseInt( 98 * size );
   O('total').style.left = parseInt( 75 * size );
   O('current').style.left = parseInt( 8 * size );
   O('uptot').style.left = parseInt( 19 * size );
   O('downtot').style.left = parseInt( 19 * size );
   O('UpTotal').style.left = parseInt( 75 * size );
   O('DownTotal').style.left = parseInt( 75 * size );
   O('newUpdate').style.left = parseInt( 15 * size );
   O('newUpdate').style.color = "#FF0000";
   document.body.style.fontSize = parseInt( 9 * size );
   O('network').style.fontSize = parseInt( 12 * size );
   con.src = ("/wrong.png");
   var bwidth = parseInt( 116 * size );
   O('linesx').style.width = bwidth;
   O('linesy').style.width = bwidth;
   O('border').style.width = bwidth+1;
   O('graphImg').style.width = bwidth;
   O('downgraph').style.width = bwidth;
   O('upgraph').style.width = bwidth;
   O('wpng').style.width = parseInt( 18 * size );
   O('con').style.width = parseInt( 13 * size );
   O('networkAdaName').style.width = parseInt( 109 * size );
   O('networkid').style.width = parseInt( 109 * size );
   O('back3').style.width = parseInt( 50 * size );
   O('top3').style.width = parseInt( 50 * size );
   O('back1').style.width = parseInt( 50 * size );
   O('top1').style.width = parseInt( 50 * size );
   O('bar1').style.width = parseInt( 1 * size );
   O('back2').style.width = parseInt( 50 * size );
   O('top2').style.width = parseInt( 50 * size );
   O('bar2').style.width = parseInt( 1 * size );
   O('arrowup').style.width = parseInt( 16 * size );
   O('arrowdown').style.width = parseInt( 16 * size );
   O('arrowup2').style.width = parseInt( 16 * size );
   O('arrowdown2').style.width = parseInt( 16 * size );
   O('arrowup3').style.width = parseInt( 16 * size );
   O('arrowdown3').style.width = parseInt( 16 * size );
   O('refreshe').style.width = parseInt( 16 * size );
   O('resetCurrent').style.width = parseInt( 16 * size );
   O('resetTotal').style.width = parseInt( 16 * size );
   O('linesx').style.height = parseInt( 30 * size );
   O('linesy').style.height = parseInt( 30 * size );
   O('border').style.height = parseInt( 30 * size )+1;
   O('graphImg').style.height = parseInt( 30 * size );
   O('downgraph').style.height = parseInt( 30 * size );
   O('upgraph').style.height = parseInt( 30 * size );
   O('wpng').style.height = parseInt( 13 * size );
   O('con').style.height = parseInt( 13 * size );
   O('networkAdaName').style.height = parseInt( 12 * size );
   O('networkid').style.height = parseInt( 12 * size );
   O('bar3').style.height = parseInt( 6 * size );
   O('back3').style.height = parseInt( 6 * size );
   O('top3').style.height = parseInt( 6 * size );
   O('back1').style.height = parseInt( 6 * size );
   O('bar1').style.height = parseInt( 6 * size );
   O('top1').style.height = parseInt( 6 * size );
   O('back2').style.height = parseInt( 6 * size );
   O('bar2').style.height = parseInt( 6 * size );
   O('top2').style.height = parseInt( 6 * size );
   O('bar4').style.height = parseInt( 6 * size );
   O('back4').style.height = parseInt( 6 * size );
   O('top4').style.height = parseInt( 6 * size );
   O('arrowup').style.height = parseInt( 16 * size );
   O('arrowdown').style.height = parseInt( 16 * size );
   O('arrowup2').style.height = parseInt( 16 * size );
   O('arrowdown2').style.height = parseInt( 16 * size );
   O('arrowup3').style.height = parseInt( 16 * size );
   O('arrowdown3').style.height = parseInt( 16 * size );
   O('refreshe').style.height = parseInt( 16 * size );
   O('resetCurrent').style.height = parseInt( 16 * size );
   O('resetTotal').style.height = parseInt( 16 * size );
   if(alertLostCon==1)
   {
     O('iconAlert').style.color = sAlertIcon;
   }
   else if(alertLostCon==2)
   {
     O('iconAlert').style.color = "#808080";
   }
   O('iconAlert').innerHTML = "&#9834;";
   O('iconAlert').style.top = parseInt( 17 * size );
   O('iconAlert').style.right = parseInt( 6 * size );
   O('iconAlert').style.fontSize = parseInt( 16 * size );
   if(saveChartData==3)
   {
     O('graphImg').style.visibility = "hidden";
   }
   else
   {
     O('graphImg').style.visibility = "visible";
   }
 }
 function refresheeip()
 {
   if (autoeip == 1)
   {
     getIP();
     clearTimeout(clearShowEIP);
     clearShowEIP = setTimeout("refresheeip()",parseInt( seteiptimer * 60000 ));
   }
 }
 function runNetworkAndSharingCenter()
 {
   System.Shell.execute("control", "/name Microsoft.NetworkAndSharingCenter");
 }
 function show_flyout()
 {
   System.Gadget.Flyout.file = "flyout.html";
   System.Gadget.Flyout.show = true;
 }
 function showBLIP()
 {
   System.Gadget.Flyout.file = "BLIP.html";
   System.Gadget.Flyout.show = true;
 }
 function showIPLog()
 {
   shell.Run("file:///"+System.Gadget.path.replace(" ","%20")+"/iplog.html");
 }
 function show_chart()
 {
   shell.Run("file:///"+System.Gadget.path.replace(" ","%20")+"/linechart.html");
 }
 function hideFlyout()
 {
   reloadBL=System.Gadget.Settings.read("reloadBL");
   if (reloadBL=="")
   {
     reloadBL=2;
   }
   if(reloadBL==1)
   {
     O('blIP').innerHTML = "Blacklisted IP Ratio: Loading";
     getDNSBL();
     System.Gadget.Settings.write("reloadBL", 2);
   }
 }
 function onUnload()
 {
   if(saveWriteUsage==1)
   {
     writeUsage();
   }
   saveTotal();
   NetLib='';
   UnregisterLibrary();
 }
 function showFO(x)
 {
   var SearchTerm = encodeURIComponent(x);
   if(x == "")
   {
     shell.Run("http://addgadgets.com/search/?q=");
     return false;
   }
   else if(x == "-Yahoo-")
   {
     shell.Run("http://addgadgets.com/search/?q=");
     return false;
   }
   else
   {
     shell.Run("http://addgadgets.com/search/?q="+SearchTerm);
     return false;
   }
 }
 function resetBar()
 {
   if(query.value=="")
   {
     query.value="Yahoo! Search";
     O("query").style.color = "#666666";
   }
 }
 function activateBar()
 {
   O("query").style.color = "#000000";
   if(query.value=="Yahoo! Search")
   {
     query.value="";
   }
 }
 