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
 var NetLib;
 var interfaceCount=0;
 var name = new Array();
 var name2 = new Array();
 var name3 = new Array();
 var addName = new Array();
 var addName2 = new Array();
 var addName3 = new Array();
 var allName = new Array();
 var allName2 = new Array();
 var allName3 = new Array();
 var objLocator = new ActiveXObject("WbemScripting.SWbemLocator");
 var objWMIService = objLocator.ConnectServer(null, "root\\cimv2");
 var wbemFlagReturnImmediately = 0x10;
 var wbemFlagForwardOnly = 0x20;
 var O=document.getElementById;
 function onLoad()
 {
   try
   {
     NetLib = GetLibrary();
   }
   catch (err)
   {
   }
   initSettings();
   System.Gadget.onSettingsClosing = SettingsClosing;
 }
 function initSettings()
 {
   loadSettings();
   disableInterface();
   fillInterfaceCombo5();
   fillInterfaceCombo4();
   fillInterfaceCombo3();
   setPeakRemaining();
   soundSettings();
   sizeSettings();
   graphSettings();
   colorSettings();
   showColor();
   cbStarts();
 }
 function SettingsClosing(event)
 {
   if (event.closeAction == event.Action.commit)
   {
     System.Gadget.Settings.write("netType", netType.value);
     System.Gadget.Settings.write("method", method.value);
     System.Gadget.Settings.write("interface", interface.value);
     System.Gadget.Settings.write("interface2", interface2.value);
     System.Gadget.Settings.write("interface3", interface3.value);
     System.Gadget.Settings.write("autoNetwork", autoNetwork.value);
     if (autoInterface.checked)
     {
       System.Gadget.Settings.write ("autoInterface", 1)
     }
     else
     {
       System.Gadget.Settings.write ("autoInterface", 2)
     }
     System.Gadget.Settings.write("scanNetwork", scanNetwork.value);
     System.Gadget.Settings.write("graph", graph.value);
     System.Gadget.Settings.write("drawstyle", drawstyle.value);
     System.Gadget.Settings.write("scaletype", ScaleType.value);
     System.Gadget.Settings.write("scalesize", ScaleSize.value);
     System.Gadget.Settings.write("vismode", VisMode.value);
     System.Gadget.Settings.write("barup", barup.value);
     System.Gadget.Settings.write("bardown", bardown.value);
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
     System.Gadget.Settings.write("update", update.value);
     System.Gadget.Settings.write("settimer", settimer.value);
     System.Gadget.Settings.write("showextip", showextip.value);
     System.Gadget.Settings.write("autoiip", autoiip.value);
     System.Gadget.Settings.write("setiiptimer", setiiptimer.value);
     System.Gadget.Settings.write("autoeip", autoeip.value);
     System.Gadget.Settings.write("seteiptimer", seteiptimer.value);
     System.Gadget.Settings.write("totalbandwidth", totalbandwidth.value);
     System.Gadget.Settings.write("bytesSentSize", bytesSentSize.value);
     System.Gadget.Settings.write("bytesReceivedSize", bytesReceivedSize.value);
     System.Gadget.Settings.write("fixUnit", fixUnit.value);
     System.Gadget.Settings.write("backg","#" + backg.value);
     System.Gadget.Settings.write("fixBackg",fixBackg.value);
     System.Gadget.Settings.write("title","#" + title.value);
     System.Gadget.Settings.write("fixTitle",fixTitle.value);
     System.Gadget.Settings.write("SSID","#" + SSID.value);
     System.Gadget.Settings.write("fixSSID",fixSSID.value);
     System.Gadget.Settings.write("INTIP","#" + INTIP.value);
     System.Gadget.Settings.write("fixINTIP",fixINTIP.value);
     System.Gadget.Settings.write("EXTIP","#" + EXTIP.value);
     System.Gadget.Settings.write("fixEXTIP",fixEXTIP.value);
     System.Gadget.Settings.write("But","#" + But.value);
     System.Gadget.Settings.write("fixBut",fixBut.value);
     System.Gadget.Settings.write("Conn","#" + Conn.value);
     System.Gadget.Settings.write("fixConn",fixConn.value);
     System.Gadget.Settings.write("Sign","#" + Sign.value);
     System.Gadget.Settings.write("fixSign",fixSign.value);
     System.Gadget.Settings.write("Uplo","#" + Uplo.value);
     System.Gadget.Settings.write("fixUplo",fixUplo.value);
     System.Gadget.Settings.write("Downlo","#" + Downlo.value);
     System.Gadget.Settings.write("fixDownlo",fixDownlo.value);
     System.Gadget.Settings.write("Curr","#" + Curr.value);
     System.Gadget.Settings.write("fixCurr",fixCurr.value);
     System.Gadget.Settings.write("CurrUp","#" + CurrUp.value);
     System.Gadget.Settings.write("fixCurrUp",fixCurrUp.value);
     System.Gadget.Settings.write("CurrDown","#" + CurrDown.value);
     System.Gadget.Settings.write("fixCurrDown",fixCurrDown.value);
     System.Gadget.Settings.write("Tota","#" + Tota.value);
     System.Gadget.Settings.write("fixTota",fixTota.value);
     System.Gadget.Settings.write("TotaUp","#" + TotaUp.value);
     System.Gadget.Settings.write("fixTotaUp",fixTotaUp.value);
     System.Gadget.Settings.write("TotaDown","#" + TotaDown.value);
     System.Gadget.Settings.write("fixTotaDown",fixTotaDown.value);
     System.Gadget.Settings.write("setRemaining",setRemaining.value);
     System.Gadget.Settings.write("billingStarts",billingStarts.value);
     System.Gadget.Settings.write("cycleQuota",cycleQuota.value);
     System.Gadget.Settings.write("billingWeek",billingWeek.value);
     System.Gadget.Settings.write("billingMonth",billingMonth.value);
     System.Gadget.Settings.write("cycleQuotaSize",cycleQuotaSize.value);
     System.Gadget.Settings.write("RemPercent","#" + RemPercent.value);
     System.Gadget.Settings.write("fixRemPercent",fixRemPercent.value);
     System.Gadget.Settings.write("RemDays","#" + RemDays.value);
     System.Gadget.Settings.write("fixRemDays",fixRemDays.value);
     System.Gadget.Settings.write("RemQuota","#" + RemQuota.value);
     System.Gadget.Settings.write("fixRemQuota",fixRemQuota.value);
     System.Gadget.Settings.write("RemPerDay","#" + RemPerDay.value);
     System.Gadget.Settings.write("fixRemPerDay",fixRemPerDay.value);
     System.Gadget.Settings.write("RemUsed","#" + RemUsed.value);
     System.Gadget.Settings.write("fixRemUsed",fixRemUsed.value);
     System.Gadget.Settings.write("interface5", allName[interface.value]);
     System.Gadget.Settings.write("interface6", allName2[interface2.value]);
     System.Gadget.Settings.write("interface7", allName3[interface3.value]);
     System.Gadget.Settings.write("allName",allName);
     System.Gadget.Settings.write("allName2",allName2);
     System.Gadget.Settings.write("allName3",allName3);
     System.Gadget.Settings.write("showInternalIP", showInternalIP.value);
     System.Gadget.Settings.write("showBlacklisted", showBlacklisted.value);
     System.Gadget.Settings.write("showSTIL", showSTIL.value);
     System.Gadget.Settings.write("showFirewall", showFirewall.value);
     System.Gadget.Settings.write("peakHour",peakHour.value);
     System.Gadget.Settings.write("peakMin",peakMin.value);
     System.Gadget.Settings.write("peakCycleQuota",peakCycleQuota.value);
     System.Gadget.Settings.write("peakCycleQuotaSize",peakCycleQuotaSize.value);
     System.Gadget.Settings.write("peakAdjustUsage",peakAdjustUsage.value);
     System.Gadget.Settings.write("peakAdjustUsageSize",peakAdjustUsageSize.value);
     System.Gadget.Settings.write("offpeakHour",offpeakHour.value);
     System.Gadget.Settings.write("offpeakMin",offpeakMin.value);
     System.Gadget.Settings.write("offpeakCycleQuota",offpeakCycleQuota.value);
     System.Gadget.Settings.write("offpeakCycleQuotaSize",offpeakCycleQuotaSize.value);
     System.Gadget.Settings.write("offpeakAdjustUsage",offpeakAdjustUsage.value);
     System.Gadget.Settings.write("offpeakAdjustUsageSize",offpeakAdjustUsageSize.value);
     System.Gadget.Settings.write("BLIP","#" + BLIP.value);
     System.Gadget.Settings.write("fixBLIP",fixBLIP.value);
     System.Gadget.Settings.write("FWPF","#" + FWPF.value);
     System.Gadget.Settings.write("fixFWPF",fixFWPF.value);
     System.Gadget.Settings.write("PeakUsed","#" + PeakUsed.value);
     System.Gadget.Settings.write("fixPeakUsed",fixPeakUsed.value);
     System.Gadget.Settings.write("OffpeakUsed","#" + OffpeakUsed.value);
     System.Gadget.Settings.write("fixOffpeakUsed",fixOffpeakUsed.value);
     System.Gadget.Settings.write("FlyoutBac","#" + FlyoutBac.value);
     System.Gadget.Settings.write("fixFlyoutBac",fixFlyoutBac.value);
     System.Gadget.Settings.write("FlyoutTit","#" + FlyoutTit.value);
     System.Gadget.Settings.write("fixFlyoutTit",fixFlyoutTit.value);
     System.Gadget.Settings.write("FlyoutDet","#" + FlyoutDet.value);
     System.Gadget.Settings.write("fixFlyoutDet",fixFlyoutDet.value);
     System.Gadget.Settings.write("saveChartData",saveChartData.value);
     System.Gadget.Settings.write("showNetworkAdaName",showNetworkAdaName.value);
     System.Gadget.Settings.write("NetIN","#" + NetIN.value);
     System.Gadget.Settings.write("fixNetIN",fixNetIN.value);
     System.Gadget.Settings.write("saveWriteUsage",saveWriteUsage.value);
     System.Gadget.Settings.write("alertLostCon", alertLostCon.value);
     System.Gadget.Settings.write("soundLostCon", soundLostCon.value);
     System.Gadget.Settings.write("soundLostConurl", soundLostConurl.innerText);
     System.Gadget.Settings.write("soundLostConVol", soundLostConVol.value);
     System.Gadget.Settings.write("soundLostConRepeats", soundLostConRepeats.value);
     System.Gadget.Settings.write("soundLostConCount", soundLostConCount.value);
     System.Gadget.Settings.write("AlertIcon","#" + AlertIcon.value);
     System.Gadget.Settings.write("fixAlertIcon",fixAlertIcon.value);
     System.Gadget.Settings.write("saveIPAddress", saveIPAddress.value);
     System.Gadget.Settings.write("showSearch", showSearch.value);
     savefilesettings();
     saveTotal();
   }
   event.cancel = false;
 }
 function loadSettings()
 {
   Int_Nr3 = System.Gadget.Settings.read("interface3");
   if (Int_Nr3 != '') interface3.selectedIndex = Int_Nr3;
   else interface3.selectedIndex = 0;
   gallName = System.Gadget.Settings.read("allName");
   if (gallName != '')
   {
     allName = gallName.split(",");
   }
   else
   {
     allName=[];
   }
   gallName2 = System.Gadget.Settings.read("allName2");
   if (gallName2 != '')
   {
     allName2 = gallName2.split(",");
   }
   else
   {
     allName2=[];
   }
   gallName3 = System.Gadget.Settings.read("allName3");
   if (gallName3 != '')
   {
     allName3 = gallName3.split(",");
   }
   else
   {
     allName3=[];
   }
   gnetType = System.Gadget.Settings.read("netType");
   if (gnetType != '') netType.value = gnetType;
   else netType.value='3';
   gmethod = System.Gadget.Settings.read("method");
   if (gmethod != '') method.value = gmethod;
   else method.value='2';
   gautoNetwork = System.Gadget.Settings.read("autoNetwork");
   if (gautoNetwork != '') autoNetwork.value = gautoNetwork;
   else autoNetwork.value='1';
   gautoInterface = System.Gadget.Settings.read("autoInterface");
   if (gautoInterface == 2) autoInterface.checked = false;
   else autoInterface.checked = true;
   gscanNetwork = System.Gadget.Settings.read("scanNetwork");
   if (gscanNetwork != '') scanNetwork.value = gscanNetwork;
   else scanNetwork.value='10';
   gshowInternalIP = System.Gadget.Settings.read("showInternalIP");
   if (gshowInternalIP != '') showInternalIP.value = gshowInternalIP;
   else showInternalIP.value='1';
   gshowBlacklisted = System.Gadget.Settings.read("showBlacklisted");
   if (gshowBlacklisted != '') showBlacklisted.value = gshowBlacklisted;
   else showBlacklisted.value='1';
   gshowSTIL = System.Gadget.Settings.read("showSTIL");
   if (gshowSTIL != '') showSTIL.value = gshowSTIL;
   else showSTIL.value='1';
   gshowFirewall = System.Gadget.Settings.read("showFirewall");
   if (gshowFirewall != '') showFirewall.value = gshowFirewall;
   else showFirewall.value='1';
   ggraph = System.Gadget.Settings.read("graph");
   if (ggraph != '') graph.value = ggraph;
   else graph.value='1';
   gdrawstyle = System.Gadget.Settings.read("drawstyle");
   if (gdrawstyle != '') drawstyle.value = gdrawstyle;
   else drawstyle.value='0';
   gScaleType = System.Gadget.Settings.read("scaletype");
   if (gScaleType != '') ScaleType.value = gScaleType;
   else ScaleType.value='0';
   gScaleSize = System.Gadget.Settings.read("scalesize");
   if (gScaleSize != '') ScaleSize.value = gScaleSize;
   else ScaleSize.value='0';
   gVisMode = System.Gadget.Settings.read("vismode");
   if (gVisMode != '') VisMode.value = gVisMode;
   else VisMode.value='0';
   gbarup = System.Gadget.Settings.read("barup");
   if (gbarup != '') barup.value = gbarup;
   else barup.value='0';
   gbardown = System.Gadget.Settings.read("bardown");
   if (gbardown != '') bardown.value = gbardown;
   else bardown.value='0';
   gfixsize = System.Gadget.Settings.read("fixsize");
   if (gfixsize != '') fixsize.value = gfixsize;
   else fixsize.value='100';
   gssize = System.Gadget.Settings.read("ssize");
   if (gssize != '') ssize.value = gssize;
   else ssize.value='100';
   gupdate = System.Gadget.Settings.read("update");
   if (gupdate != '') update.value = gupdate;
   else update.value='1';
   gsettimer = System.Gadget.Settings.read("settimer");
   if (gsettimer != '') settimer.value = gsettimer;
   else settimer.value='1';
   gshowextip = System.Gadget.Settings.read("showextip");
   if (gshowextip != '') showextip.value = gshowextip;
   else showextip.value='1';
   gautoiip = System.Gadget.Settings.read("autoiip");
   if (gautoiip != '') autoiip.value = gautoiip;
   else autoiip.value='1';
   gsetiiptimer = System.Gadget.Settings.read("setiiptimer");
   if (gsetiiptimer != '') setiiptimer.value = gsetiiptimer;
   else setiiptimer.value='10';
   gautoeip = System.Gadget.Settings.read("autoeip");
   if (gautoeip != '') autoeip.value = gautoeip;
   else autoeip.value='2';
   gseteiptimer = System.Gadget.Settings.read("seteiptimer");
   if (gseteiptimer != '') seteiptimer.value = gseteiptimer;
   else seteiptimer.value='10';
   gtotalbandwidth = System.Gadget.Settings.read("totalbandwidth");
   if (gtotalbandwidth != '') totalbandwidth.value = gtotalbandwidth;
   else totalbandwidth.value='1';
   gbytesSentSize = System.Gadget.Settings.read("bytesSentSize");
   if (gbytesSentSize != '') bytesSentSize.value = gbytesSentSize;
   else bytesSentSize.value='1';
   gbytesReceivedSize = System.Gadget.Settings.read("bytesReceivedSize");
   if (gbytesReceivedSize != '') bytesReceivedSize.value = gbytesReceivedSize;
   else bytesReceivedSize.value='1';
   gfixUnit = System.Gadget.Settings.read("fixUnit");
   if (gfixUnit != '') fixUnit.value = gfixUnit;
   else fixUnit.value='1';
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
   gSSID = System.Gadget.Settings.read("SSID");
   if (gSSID != '') SSID.value = gSSID;
   else SSID.value='87cefa';
   gfixSSID = System.Gadget.Settings.read("fixSSID");
   if (gfixSSID != '') fixSSID.value = gfixSSID;
   else fixSSID.value='';
   gINTIP = System.Gadget.Settings.read("INTIP");
   if (gINTIP != '') INTIP.value = gINTIP;
   else INTIP.value='87cefa';
   gfixINTIP = System.Gadget.Settings.read("fixINTIP");
   if (gfixINTIP != '') fixINTIP.value = gfixINTIP;
   else fixINTIP.value='';
   gEXTIP = System.Gadget.Settings.read("EXTIP");
   if (gEXTIP != '') EXTIP.value = gEXTIP;
   else EXTIP.value='87cefa';
   gfixEXTIP = System.Gadget.Settings.read("fixEXTIP");
   if (gfixEXTIP != '') fixEXTIP.value = gfixEXTIP;
   else fixEXTIP.value='';
   gBLIP= System.Gadget.Settings.read("BLIP");
   if (gBLIP != '') BLIP.value = gBLIP;
   else BLIP.value='fff62a';
   gfixBLIP = System.Gadget.Settings.read("fixBLIP");
   if (gfixBLIP != '') fixBLIP.value = gfixBLIP;
   else fixBLIP.value='';
   gBut= System.Gadget.Settings.read("But");
   if (gBut != '') But.value = gBut;
   else But.value='ffcc00';
   gfixBut = System.Gadget.Settings.read("fixBut");
   if (gfixBut != '') fixBut.value = gfixBut;
   else fixBut.value='';
   gFWPF= System.Gadget.Settings.read("FWPF");
   if (gFWPF != '') FWPF.value = gFWPF;
   else FWPF.value='ec7527';
   gfixFWPF = System.Gadget.Settings.read("fixFWPF");
   if (gfixFWPF != '') fixFWPF.value = gfixFWPF;
   else fixFWPF.value='';
   gConn = System.Gadget.Settings.read("Conn");
   if (gConn != '') Conn.value = gConn;
   else Conn.value='87cefa';
   gfixConn = System.Gadget.Settings.read("fixConn");
   if (gfixConn != '') fixConn.value = gfixConn;
   else fixConn.value='';
   gSign = System.Gadget.Settings.read("Sign");
   if (gSign != '') Sign.value = gSign;
   else Sign.value='87cefa';
   gfixSign = System.Gadget.Settings.read("fixSign");
   if (gfixSign != '') fixSign.value = gfixSign;
   else fixSign.value='';
   gUplo = System.Gadget.Settings.read("Uplo");
   if (gUplo != '') Uplo.value = gUplo;
   else Uplo.value='90ee90';
   gfixUplo = System.Gadget.Settings.read("fixUplo");
   if (gfixUplo != '') fixUplo.value = gfixUplo;
   else fixUplo.value='';
   gDownlo = System.Gadget.Settings.read("Downlo");
   if (gDownlo != '') Downlo.value = gDownlo;
   else Downlo.value='fff62a';
   gfixDownlo = System.Gadget.Settings.read("fixDownlo");
   if (gfixDownlo != '') fixDownlo.value = gfixDownlo;
   else fixDownlo.value='';
   gCurr = System.Gadget.Settings.read("Curr");
   if (gCurr != '') Curr.value = gCurr;
   else Curr.value='87cefa';
   gfixCurr = System.Gadget.Settings.read("fixCurr");
   if (gfixCurr != '') fixCurr.value = gfixCurr;
   else fixCurr.value='';
   gCurrUp = System.Gadget.Settings.read("CurrUp");
   if (gCurrUp != '') CurrUp.value = gCurrUp;
   else CurrUp.value='90ee90';
   gfixCurrUp = System.Gadget.Settings.read("fixCurrUp");
   if (gfixCurrUp != '') fixCurrUp.value = gfixCurrUp;
   else fixCurrUp.value='';
   gCurrDown = System.Gadget.Settings.read("CurrDown");
   if (gCurrDown != '') CurrDown.value = gCurrDown;
   else CurrDown.value='fff62a';
   gfixCurrDown = System.Gadget.Settings.read("fixCurrDown");
   if (gfixCurrDown != '') fixCurrDown.value = gfixCurrDown;
   else fixCurrDown.value='';
   gTota = System.Gadget.Settings.read("Tota");
   if (gTota != '') Tota.value = gTota;
   else Tota.value='87cefa';
   gfixTota = System.Gadget.Settings.read("fixTota");
   if (gfixTota != '') fixTota.value = gfixTota;
   else fixTota.value='';
   gTotaUp = System.Gadget.Settings.read("TotaUp");
   if (gTotaUp != '') TotaUp.value = gTotaUp;
   else TotaUp.value='90ee90';
   gfixTotaUp = System.Gadget.Settings.read("fixTotaUp");
   if (gfixTotaUp != '') fixTotaUp.value = gfixTotaUp;
   else fixTotaUp.value='';
   gTotaDown = System.Gadget.Settings.read("TotaDown");
   if (gTotaDown != '') TotaDown.value = gTotaDown;
   else TotaDown.value='fff62a';
   gfixTotaDown = System.Gadget.Settings.read("fixTotaDown");
   if (gfixTotaDown != '') fixTotaDown.value = gfixTotaDown;
   else fixTotaDown.value='';
   gsetRemaining = System.Gadget.Settings.read("setRemaining");
   if (gsetRemaining != '') setRemaining.value = gsetRemaining;
   else setRemaining.value='2';
   gbillingStarts = System.Gadget.Settings.read("billingStarts");
   if (gbillingStarts != '') billingStarts.value = gbillingStarts;
   else billingStarts.value='1';
   gcycleQuota = System.Gadget.Settings.read("cycleQuota");
   if (gcycleQuota != '') cycleQuota.value = gcycleQuota;
   else cycleQuota.value='100';
   gbillingWeek = System.Gadget.Settings.read("billingWeek");
   if (gbillingWeek != '') billingWeek.value = gbillingWeek;
   else billingWeek.value='1';
   gbillingMonth = System.Gadget.Settings.read("billingMonth");
   if (gbillingMonth != '') billingMonth.value = gbillingMonth;
   else billingMonth.value='1';
   gcycleQuotaSize = System.Gadget.Settings.read("cycleQuotaSize");
   if (gcycleQuotaSize != '') cycleQuotaSize.value = gcycleQuotaSize;
   else cycleQuotaSize.value='1';
   gpeakHour = System.Gadget.Settings.read("peakHour");
   if (gpeakHour != '') peakHour.value = gpeakHour;
   else peakHour.value='0';
   gpeakMin = System.Gadget.Settings.read("peakMin");
   if (gpeakMin != '') peakMin.value = gpeakMin;
   else peakMin.value='0';
   if (peakMin.value <=9)
   {
     peakMin.value ='0'+peakMin.value;
   }
   gpeakCycleQuota = System.Gadget.Settings.read("peakCycleQuota");
   if (gpeakCycleQuota != '') peakCycleQuota.value = gpeakCycleQuota;
   else peakCycleQuota.value='100';
   gpeakCycleQuotaSize = System.Gadget.Settings.read("peakCycleQuotaSize");
   if (gpeakCycleQuotaSize != '') peakCycleQuotaSize.value = gpeakCycleQuotaSize;
   else peakCycleQuotaSize.value='1';
   goffpeakHour = System.Gadget.Settings.read("offpeakHour");
   if (goffpeakHour != '') offpeakHour.value = goffpeakHour;
   else offpeakHour.value='0';
   goffpeakMin = System.Gadget.Settings.read("offpeakMin");
   if (goffpeakMin != '') offpeakMin.value = goffpeakMin;
   else offpeakMin.value='0';
   if (offpeakMin.value <=9)
   {
     offpeakMin.value ='0'+offpeakMin.value;
   }
   goffpeakCycleQuota = System.Gadget.Settings.read("offpeakCycleQuota");
   if (goffpeakCycleQuota != '') offpeakCycleQuota.value = goffpeakCycleQuota;
   else offpeakCycleQuota.value='';
   goffpeakCycleQuotaSize = System.Gadget.Settings.read("offpeakCycleQuotaSize");
   if (goffpeakCycleQuotaSize != '') offpeakCycleQuotaSize.value = goffpeakCycleQuotaSize;
   else offpeakCycleQuotaSize.value='4';
   gRemPercent = System.Gadget.Settings.read("RemPercent");
   if (gRemPercent != '') RemPercent.value = gRemPercent;
   else RemPercent.value='87cefa';
   gfixRemPercent = System.Gadget.Settings.read("fixRemPercent");
   if (gfixRemPercent != '') fixRemPercent.value = gfixRemPercent;
   else fixRemPercent.value='';
   gRemDays = System.Gadget.Settings.read("RemDays");
   if (gRemDays != '') RemDays.value = gRemDays;
   else RemDays.value='87cefa';
   gfixRemDays = System.Gadget.Settings.read("fixRemDays");
   if (gfixRemDays != '') fixRemDays.value = gfixRemDays;
   else fixRemDays.value='';
   gRemQuota = System.Gadget.Settings.read("RemQuota");
   if (gRemQuota != '') RemQuota.value = gRemQuota;
   else RemQuota.value='87cefa';
   gfixRemQuota = System.Gadget.Settings.read("fixRemQuota");
   if (gfixRemQuota != '') fixRemQuota.value = gfixRemQuota;
   else fixRemQuota.value='';
   gRemPerDay = System.Gadget.Settings.read("RemPerDay");
   if (gRemPerDay != '') RemPerDay.value = gRemPerDay;
   else RemPerDay.value='87cefa';
   gfixRemPerDay = System.Gadget.Settings.read("fixRemPerDay");
   if (gfixRemPerDay != '') fixRemPerDay.value = gfixRemPerDay;
   else fixRemPerDay.value='';
   gRemUsed = System.Gadget.Settings.read("RemUsed");
   if (gRemUsed != '') RemUsed.value = gRemUsed;
   else RemUsed.value='90ee90';
   gfixRemUsed = System.Gadget.Settings.read("fixRemUsed");
   if (gfixRemUsed != '') fixRemUsed.value = gfixRemUsed;
   else fixRemUsed.value='';
   gPeakUsed = System.Gadget.Settings.read("PeakUsed");
   if (gPeakUsed != '') PeakUsed.value = gPeakUsed;
   else PeakUsed.value='90ee90';
   gfixPeakUsed = System.Gadget.Settings.read("fixPeakUsed");
   if (gfixPeakUsed != '') fixPeakUsed.value = gfixPeakUsed;
   else fixPeakUsed.value='';
   gOffpeakUsed = System.Gadget.Settings.read("OffpeakUsed");
   if (gOffpeakUsed != '') OffpeakUsed.value = gOffpeakUsed;
   else OffpeakUsed.value='90ee90';
   gfixOffpeakUsed = System.Gadget.Settings.read("fixOffpeakUsed");
   if (gfixOffpeakUsed != '') fixOffpeakUsed.value = gfixOffpeakUsed;
   else fixOffpeakUsed.value='';
   gFlyoutBac = System.Gadget.Settings.read("FlyoutBac");
   if (gFlyoutBac != '') FlyoutBac.value = gFlyoutBac;
   else FlyoutBac.value='080808';
   gfixFlyoutBac = System.Gadget.Settings.read("fixFlyoutBac");
   if (gfixFlyoutBac != '') fixFlyoutBac.value = gfixFlyoutBac;
   else fixFlyoutBac.value='';
   gFlyoutTit = System.Gadget.Settings.read("FlyoutTit");
   if (gFlyoutTit != '') FlyoutTit.value = gFlyoutTit;
   else FlyoutTit.value='87cefa';
   gfixFlyoutTit = System.Gadget.Settings.read("fixFlyoutTit");
   if (gfixFlyoutTit != '') fixFlyoutTit.value = gfixFlyoutTit;
   else fixFlyoutTit.value='';
   gFlyoutDet = System.Gadget.Settings.read("FlyoutDet");
   if (gFlyoutDet != '') FlyoutDet.value = gFlyoutDet;
   else FlyoutDet.value='ffcc00';
   gfixFlyoutDet = System.Gadget.Settings.read("fixFlyoutDet");
   if (gfixFlyoutDet != '') fixFlyoutDet.value = gfixFlyoutDet;
   else fixFlyoutDet.value='';
   gsaveChartData = System.Gadget.Settings.read("saveChartData");
   if (gsaveChartData != '') saveChartData.value = gsaveChartData;
   else saveChartData.value='2';
   gshowNetworkAdaName = System.Gadget.Settings.read("showNetworkAdaName");
   if (gshowNetworkAdaName != '') showNetworkAdaName.value = gshowNetworkAdaName;
   else showNetworkAdaName.value='1';
   gNetIN = System.Gadget.Settings.read("NetIN");
   if (gNetIN != '') NetIN.value = gNetIN;
   else NetIN.value='90ee90';
   gfixNetIN = System.Gadget.Settings.read("fixNetIN");
   if (gfixNetIN != '') fixNetIN.value = gfixNetIN;
   else fixNetIN.value='';
   gsaveWriteUsage = System.Gadget.Settings.read("saveWriteUsage");
   if (gsaveWriteUsage != '') saveWriteUsage.value = gsaveWriteUsage;
   else saveWriteUsage.value='1';
   galertLostCon = System.Gadget.Settings.read("alertLostCon");
   if (galertLostCon != '') alertLostCon.value = galertLostCon;
   else alertLostCon.value='2';
   gsoundLostCon = System.Gadget.Settings.read("soundLostCon");
   if (gsoundLostCon != '') soundLostCon.value = gsoundLostCon;
   else soundLostCon.value='1';
   gsoundLostConurl = System.Gadget.Settings.read("soundLostConurl");
   if (gsoundLostConurl != '') soundLostConurl.innerText = gsoundLostConurl;
   else soundLostConurl.innerText='';
   gsoundLostConVol = System.Gadget.Settings.read("soundLostConVol");
   if (gsoundLostConVol != '') soundLostConVol.value = gsoundLostConVol;
   else soundLostConVol.value='100';
   gsoundLostConRepeats = System.Gadget.Settings.read("soundLostConRepeats");
   if (gsoundLostConRepeats != '') soundLostConRepeats.value = gsoundLostConRepeats;
   else soundLostConRepeats.value='3';
   gsoundLostConCount = System.Gadget.Settings.read("soundLostConCount");
   if (gsoundLostConCount != '') soundLostConCount.value = gsoundLostConCount;
   else soundLostConCount.value='1';
   gAlertIcon = System.Gadget.Settings.read("AlertIcon");
   if (gAlertIcon != '') AlertIcon.value = gAlertIcon;
   else AlertIcon.value='90EE90';
   gfixAlertIcon = System.Gadget.Settings.read("fixAlertIcon");
   if (gfixAlertIcon != '') fixAlertIcon.value = gfixAlertIcon;
   else fixAlertIcon.value='';
   gsaveIPAddress = System.Gadget.Settings.read("saveIPAddress");
   if (gsaveIPAddress != '') saveIPAddress.value = gsaveIPAddress;
   else saveIPAddress.value='1';
   gshowSearch = System.Gadget.Settings.read("showSearch");
   if (gshowSearch != '') showSearch.value = gshowSearch;
   else showSearch.value='1';
   changemethod();
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
       inifile.WriteLine(";Network Meter (c) 2007-2013 AddGadgets.com");
       inifile.WriteLine(";v3");
       inifile.WriteLine(netType.value);
       inifile.WriteLine(method.value);
       inifile.WriteLine(interface.value);
       inifile.WriteLine(interface2.value);
       inifile.WriteLine(interface3.value);
       inifile.WriteLine(autoNetwork.value);
       inifile.WriteLine(settimer.value);
       inifile.WriteLine(update.value);
       inifile.WriteLine(fixsize.value);
       inifile.WriteLine(ssize.value);
       inifile.WriteLine(showextip.value);
       inifile.WriteLine(VisMode.value);
       inifile.WriteLine(barup.value);
       inifile.WriteLine(bardown.value);
       inifile.WriteLine(fixUnit.value);
       inifile.WriteLine(drawstyle.value);
       inifile.WriteLine(ScaleType.value);
       inifile.WriteLine(ScaleSize.value);
       inifile.WriteLine(totalbandwidth.value);
       inifile.WriteLine(bytesSentSize.value);
       inifile.WriteLine(bytesReceivedSize.value);
       inifile.WriteLine("#" + backg.value);
       inifile.WriteLine(fixBackg.value);
       inifile.WriteLine("#" + title.value);
       inifile.WriteLine(fixTitle.value);
       inifile.WriteLine("#" + SSID.value);
       inifile.WriteLine(fixSSID.value);
       inifile.WriteLine("#" + INTIP.value);
       inifile.WriteLine(fixINTIP.value);
       inifile.WriteLine("#" + EXTIP.value);
       inifile.WriteLine(fixEXTIP.value);
       inifile.WriteLine("#" + But.value);
       inifile.WriteLine(fixBut.value);
       inifile.WriteLine("#" + Conn.value);
       inifile.WriteLine(fixConn.value);
       inifile.WriteLine("#" + Sign.value);
       inifile.WriteLine(fixSign.value);
       inifile.WriteLine("#" + Uplo.value);
       inifile.WriteLine(fixUplo.value);
       inifile.WriteLine("#" + Downlo.value);
       inifile.WriteLine(fixDownlo.value);
       inifile.WriteLine("#" + Curr.value);
       inifile.WriteLine(fixCurr.value);
       inifile.WriteLine("#" + CurrUp.value);
       inifile.WriteLine(fixCurrUp.value);
       inifile.WriteLine("#" + CurrDown.value);
       inifile.WriteLine(fixCurrDown.value);
       inifile.WriteLine("#" + Tota.value);
       inifile.WriteLine(fixTota.value);
       inifile.WriteLine("#" + TotaUp.value);
       inifile.WriteLine(fixTotaUp.value);
       inifile.WriteLine("#" + TotaDown.value);
       inifile.WriteLine(fixTotaDown.value);
       inifile.WriteLine(autoeip.value);
       inifile.WriteLine(seteiptimer.value);
       inifile.WriteLine(graph.value);
       inifile.WriteLine(setRemaining.value);
       inifile.WriteLine(billingStarts.value);
       inifile.WriteLine(cycleQuota.value);
       inifile.WriteLine(billingWeek.value);
       inifile.WriteLine(billingMonth.value);
       inifile.WriteLine(cycleQuotaSize.value);
       inifile.WriteLine("#" + RemPercent.value);
       inifile.WriteLine(fixRemPercent.value);
       inifile.WriteLine("#" + RemDays.value);
       inifile.WriteLine(fixRemDays.value);
       inifile.WriteLine("#" + RemQuota.value);
       inifile.WriteLine(fixRemQuota.value);
       inifile.WriteLine("#" + RemPerDay.value);
       inifile.WriteLine(fixRemPerDay.value);
       inifile.WriteLine("#" + RemUsed.value);
       inifile.WriteLine(fixRemUsed.value);
       if(fixsize.value == "Custom")
       {
         inifile.WriteLine(ssize.value / 100)
       }
       else
       {
         inifile.WriteLine(fixsize.value / 100)
       }
       inifile.WriteLine(allName[interface.value]);
       inifile.WriteLine(allName2[interface2.value]);
       inifile.WriteLine(allName3[interface3.value]);
       inifile.WriteLine(allName);
       inifile.WriteLine(allName2);
       inifile.WriteLine(allName3);
       inifile.WriteLine(showInternalIP.value);
       inifile.WriteLine(showBlacklisted.value);
       inifile.WriteLine(showSTIL.value);
       inifile.WriteLine(showFirewall.value);
       inifile.WriteLine(peakHour.value);
       inifile.WriteLine(peakMin.value);
       inifile.WriteLine(peakCycleQuota.value);
       inifile.WriteLine(peakCycleQuotaSize.value);
       inifile.WriteLine(offpeakHour.value);
       inifile.WriteLine(offpeakMin.value);
       inifile.WriteLine(offpeakCycleQuota.value);
       inifile.WriteLine(offpeakCycleQuotaSize.value);
       inifile.WriteLine("#" + BLIP.value);
       inifile.WriteLine(fixBLIP.value);
       inifile.WriteLine("#" + FWPF.value);
       inifile.WriteLine(fixFWPF.value);
       inifile.WriteLine("#" + PeakUsed.value);
       inifile.WriteLine(fixPeakUsed.value);
       inifile.WriteLine("#" + OffpeakUsed.value);
       inifile.WriteLine(fixOffpeakUsed.value);
       inifile.WriteLine("#" + FlyoutBac.value);
       inifile.WriteLine(fixFlyoutBac.value);
       inifile.WriteLine("#" + FlyoutTit.value);
       inifile.WriteLine(fixFlyoutTit.value);
       inifile.WriteLine("#" + FlyoutDet.value);
       inifile.WriteLine(fixFlyoutDet.value);
       inifile.WriteLine(saveChartData.value);
       if (autoInterface.checked)
       {
         inifile.WriteLine(1);
       }
       else
       {
         inifile.WriteLine(2);
       }
       inifile.WriteLine(scanNetwork.value);
       inifile.WriteLine(showNetworkAdaName.value);
       inifile.WriteLine("#" + NetIN.value);
       inifile.WriteLine(fixNetIN.value);
       inifile.WriteLine(autoiip.value);
       inifile.WriteLine(setiiptimer.value);
       inifile.WriteLine(saveWriteUsage.value);
       inifile.WriteLine(alertLostCon.value);
       inifile.WriteLine(soundLostCon.value);
       inifile.WriteLine(soundLostConurl.innerText);
       inifile.WriteLine(soundLostConVol.value);
       inifile.WriteLine(soundLostConRepeats.value);
       inifile.WriteLine(soundLostConCount.value);
       inifile.WriteLine("#" + AlertIcon.value);
       inifile.WriteLine(fixAlertIcon.value);
       inifile.WriteLine(saveIPAddress.value);
       inifile.WriteLine(showSearch.value);
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
 function disableInterface()
 {
   if (autoInterface.checked)
   {
     O("scanNetworkw").disabled = false;
     O("methodw").disabled = true;
     O("method").disabled = true;
     O("interfacew").disabled = true;
     O("interface").disabled = true;
     O("interface2w").disabled = true;
     O("interface2").disabled = true;
     O("interface3").disabled = true;
   }
   else
   {
     O("scanNetworkw").disabled = true;
     O("methodw").disabled = false;
     O("method").disabled = false;
     O("interfacew").disabled = false;
     O("interface").disabled = false;
     O("interface2w").disabled = false;
     O("interface2").disabled = false;
     O("interface3").disabled = false;
   }
 }
 function fillInterfaceCombo4()
 {
   try
   {
     var colItems2 = objWMIService.ExecQuery("SELECT Description FROM Win32_NetworkAdapterConfiguration Where IPenabled=True", "WQL", wbemFlagReturnImmediately | wbemFlagForwardOnly);
     var enumItems2 = new Enumerator(colItems2);
     for (var i=0; !enumItems2.atEnd();
     enumItems2.moveNext())
     {
       var objItem2 = enumItems2.item();
       name2[i]=objItem2.Description;
       i++;
     }
   }
   catch (err)
   {
   }
   for (var i=0;i<name2.length;i++)
   {
     addName2[i] = 0;
     for (var t=0;t<allName2.length;t++)
     {
       addName2[i] = addName2[i] + allName2[t].indexOf(name2[i]);
     }
     totalAllName2=allName2.length*(-1);
     if(addName2[i]==totalAllName2)
     {
       allName2 = allName2.concat(name2[i]);
     }
     else
     {
       allName2;
     }
   }
   for (var i=0;i<allName2.length;i++)
   {
     interface2.options[interface2.length] = new Option(i + ": " + allName2[i], i);
   }
   Int_Nr2 = System.Gadget.Settings.read("interface2");
   if (Int_Nr2 != '') interface2.selectedIndex = Int_Nr2;
   else interface2.selectedIndex = 0;
 }
 function fillInterfaceCombo5()
 {
   try
   {
     interfaceCount = NetLib.Initialize();
   }
   catch (err)
   {
   }
   try
   {
     for (var i=0;i<interfaceCount;i++)
     {
       name3[i]=NetLib.Description(i);
     }
   }
   catch (err)
   {
   }
   for (var i=0;i<name3.length;i++)
   {
     addName3[i] = 0;
     for (var t=0;t<allName3.length;t++)
     {
       addName3[i] = addName3[i] + allName3[t].indexOf(name3[i]);
     }
     totalAllName3=allName3.length*(-1);
     if(addName3[i]==totalAllName3)
     {
       allName3 = allName3.concat(name3[i]);
     }
     else
     {
       allName3;
     }
   }
   for (var i=0;i<allName3.length;i++)
   {
     interface3.options[interface3.length] = new Option(i + ": " + allName3[i], i);
   }
   Int_Nr3 = System.Gadget.Settings.read("interface3");
   if (Int_Nr3 != '') interface3.selectedIndex = Int_Nr3;
   else interface3.selectedIndex = 0;
 }
 function fillInterfaceCombo3()
 {
   try
   {
     var colItems = objWMIService.ExecQuery("SELECT Name FROM Win32_PerfRawData_Tcpip_NetworkInterface", "WQL", wbemFlagReturnImmediately | wbemFlagForwardOnly);
     var enumItems = new Enumerator(colItems);
     for (var i=0; !enumItems.atEnd();
     enumItems.moveNext())
     {
       var objItem = enumItems.item();
       name[i]=objItem.Name;
       i++;
     }
   }
   catch (err)
   {
   }
   for (var i=0;i<name.length;i++)
   {
     addName[i] = 0;
     for (var t=0;t<allName.length;t++)
     {
       addName[i] = addName[i] + allName[t].indexOf(name[i]);
     }
     totalAllName=allName.length*(-1);
     if(addName[i]==totalAllName)
     {
       allName = allName.concat(name[i]);
     }
     else
     {
       allName;
     }
   }
   for (var i=0;i<allName.length;i++)
   {
     interface.options[interface.length] = new Option(i + ": " + allName[i], i);
   }
   Int_Nr = System.Gadget.Settings.read("interface");
   if (Int_Nr != '') interface.selectedIndex = Int_Nr;
   else interface.selectedIndex = 0;
 }
 function changemethod()
 {
   if(method.value==2)
   {
     O('interface3').style.visibility = "visible";
     O('interface').style.visibility = "hidden";
   }
   else
   {
     O('interface').style.visibility = "visible";
     O('interface3').style.visibility = "hidden";
   }
 }
 function chooseSound()
 {
   var shellitem = System.Shell.chooseFile(true, "Music files:*.asx;*.wpl;*.mp3;*.wav;*.wma::", "", "");
   if(shellitem!=null)
   {
     soundLostConurl.innerText = shellitem.path;
     Player.settings.volume = soundLostConVol.value;
     Player.URL = soundLostConurl.innerText;
     Player.Controls.play();
   }
 }
 function playchoosesound()
 {
   if(soundLostCon.value==100)
   {
   }
   else if(soundLostCon.value==50)
   {
     Player.URL = soundLostConurl.innerText;
   }
   else
   {
     Player.URL = System.Gadget.path + "\\alarm"+soundLostCon.value+".mp3";
   }
   Player.settings.volume = soundLostConVol.value;
   Player.Controls.play();
 }
 function clickstop()
 {
   if (Player.controls.isAvailable('Stop'))
   {
     Player.controls.stop();
   }
 }
 function soundSettings()
 {
   if(soundLostCon.value == "50")
   {
     O('csButton').style.visibility = "visible";
     O('soundLostConurl').style.visibility = "visible";
   }
   else
   {
     O('csButton').style.visibility = "hidden";
     O('soundLostConurl').style.visibility = "hidden";
   }
 }
 function soundLostConRepeatsSettings()
 {
   if(soundLostConRepeats.value == "3")
   {
     O('soundLostConCount').style.visibility = "visible";
     O('timestext').style.visibility = "visible";
   }
   else
   {
     O('soundLostConCount').style.visibility = "hidden";
     O('timestext').style.visibility = "hidden";
   }
 }
 function cbStarts()
 {
   if (billingStarts.value== 1)
   {
     O('billingMonth').style.visibility = "visible";
     O('billingWeek').style.visibility = "hidden";
   }
   else
   {
     O('billingMonth').style.visibility = "hidden";
     O('billingWeek').style.visibility = "visible";
   }
   O('billingMonth').style.top = 399;
   O('billingMonth').style.left = 153;
   O('interface3').style.top = 128;
   O('interface3').style.left = 5;
 }
 function setCheckRemaining()
 {
   checkRemaining =1;
   System.Gadget.Settings.write("checkRemaining", checkRemaining);
 }
 function setPeakRemaining()
 {
   if(setRemaining.value==3)
   {
     O('remainingPeak').disabled = false;
     O('billingCycle').disabled = true;
   }
   else
   {
     O('remainingPeak').disabled = true;
     O('billingCycle').disabled = false;
   }
 }
 function saveTotal()
 {
   if (bytesSentSize.value == 1)
   {
     var setSentTotal1= changebytesSentTotal.value * 1;
   }
   else if (bytesSentSize.value == 2)
   {
     var setSentTotal1= changebytesSentTotal.value * 1000;
   }
   else if (bytesSentSize.value == 3)
   {
     var setSentTotal1= changebytesSentTotal.value * 1000000;
   }
   if ( setSentTotal1 >= 1000000000)
   {
     var setSentTotal2= (((((setSentTotal1 * 1024) * 1024) * 1.024) * 1.024) * 1.024);
   }
   else if ( setSentTotal1 >= 1000000)
   {
     var setSentTotal2= ((((setSentTotal1 * 1024) * 1024) * 1.024) * 1.024);
   }
   else if ( setSentTotal1 >= 1000)
   {
     var setSentTotal2= (((setSentTotal1 * 1024) * 1024) * 1.024);
   }
   else if ( setSentTotal1 >= 1)
   {
     var setSentTotal2= ((setSentTotal1 * 1024) * 1024);
   }
   System.Gadget.Settings.write ("saveTotalSend", setSentTotal2 );
   if (bytesReceivedSize.value == 1)
   {
     var setReceivedTotal1= changebytesReceivedTotal.value * 1;
   }
   else if (bytesReceivedSize.value == 2)
   {
     var setReceivedTotal1= changebytesReceivedTotal.value * 1000;
   }
   else if (bytesReceivedSize.value == 3)
   {
     var setReceivedTotal1= changebytesReceivedTotal.value * 1000000;
   }
   if ( setReceivedTotal1 >= 1000000000)
   {
     var setReceivedTotal2= (((((setReceivedTotal1 * 1024) * 1024) * 1.024) * 1.024) * 1.024);
   }
   else if ( setReceivedTotal1 >= 1000000)
   {
     var setReceivedTotal2= ((((setReceivedTotal1 * 1024) * 1024) * 1.024) * 1.024);
   }
   else if ( setReceivedTotal1 >= 1000)
   {
     var setReceivedTotal2= (((setReceivedTotal1 * 1024) * 1024) * 1.024);
   }
   else if ( setReceivedTotal1 >= 1)
   {
     var setReceivedTotal2= ((setReceivedTotal1 * 1024) * 1024);
   }
   System.Gadget.Settings.write ("saveTotalReceived", setReceivedTotal2 );
   if (cycleQuotaSize.value == 1)
   {
     var setTotalTotal1= cycleQuota.value * 1;
   }
   else if (cycleQuotaSize.value == 2)
   {
     var setTotalTotal1= cycleQuota.value * 1000;
   }
   else if (cycleQuotaSize.value == 3)
   {
     var setTotalTotal1= cycleQuota.value * 1000000;
   }
   if ( setTotalTotal1 >= 1000000000)
   {
     var setTotalTotal2= (((((setTotalTotal1 * 1024) * 1024) * 1.024) * 1.024) * 1.024);
   }
   else if ( setTotalTotal1 >= 1000000)
   {
     var setTotalTotal2= ((((setTotalTotal1 * 1024) * 1024) * 1.024) * 1.024);
   }
   else if ( setTotalTotal1 >= 1000)
   {
     var setTotalTotal2= (((setTotalTotal1 * 1024) * 1024) * 1.024);
   }
   else if ( setTotalTotal1 >= 1)
   {
     var setTotalTotal2= ((setTotalTotal1 * 1024) * 1024);
   }
   System.Gadget.Settings.write ("saveTotalTotal", setTotalTotal2 );
   if (adjustUsageSize.value == 1)
   {
     var adjustUsage1= adjustUsage.value * 1;
   }
   else if (adjustUsageSize.value == 2)
   {
     var adjustUsage1= adjustUsage.value * 1000;
   }
   else if (adjustUsageSize.value == 3)
   {
     var adjustUsage1= adjustUsage.value * 1000000;
   }
   if ( adjustUsage1 >= 1000000000)
   {
     var adjustUsage2= (((((adjustUsage1 * 1024) * 1024) * 1.024) * 1.024) * 1.024);
   }
   else if ( adjustUsage1 >= 1000000)
   {
     var adjustUsage2= ((((adjustUsage1 * 1024) * 1024) * 1.024) * 1.024);
   }
   else if ( adjustUsage1 >= 1000)
   {
     var adjustUsage2= (((adjustUsage1 * 1024) * 1024) * 1.024);
   }
   else if ( adjustUsage1 >= 1)
   {
     var adjustUsage2= ((adjustUsage1 * 1024) * 1024);
   }
   System.Gadget.Settings.write ("saveTotalTot", adjustUsage2 );
   if (peakCycleQuotaSize.value == 1)
   {
     var setPeakTotal1= peakCycleQuota.value * 1;
   }
   else if (peakCycleQuotaSize.value == 2)
   {
     var setPeakTotal1= peakCycleQuota.value * 1000;
   }
   else if (peakCycleQuotaSize.value == 3)
   {
     var setPeakTotal1= peakCycleQuota.value * 1000000;
   }
   if ( setPeakTotal1 >= 1000000000)
   {
     var setPeakTotal2= (((((setPeakTotal1 * 1024) * 1024) * 1.024) * 1.024) * 1.024);
   }
   else if ( setPeakTotal1 >= 1000000)
   {
     var setPeakTotal2= ((((setPeakTotal1 * 1024) * 1024) * 1.024) * 1.024);
   }
   else if ( setPeakTotal1 >= 1000)
   {
     var setPeakTotal2= (((setPeakTotal1 * 1024) * 1024) * 1.024);
   }
   else if ( setPeakTotal1 >= 1)
   {
     var setPeakTotal2= ((setPeakTotal1 * 1024) * 1024);
   }
   System.Gadget.Settings.write ("savePeakTotal", setPeakTotal2 );
   if (peakAdjustUsageSize.value == 1)
   {
     var peakAdjustUsage1= peakAdjustUsage.value * 1;
   }
   else if (peakAdjustUsageSize.value == 2)
   {
     var peakAdjustUsage1= peakAdjustUsage.value * 1000;
   }
   else if (peakAdjustUsageSize.value == 3)
   {
     var peakAdjustUsage1= peakAdjustUsage.value * 1000000;
   }
   if ( peakAdjustUsage1 >= 1000000000)
   {
     var peakAdjustUsage2= (((((peakAdjustUsage1 * 1024) * 1024) * 1.024) * 1.024) * 1.024);
   }
   else if ( peakAdjustUsage1 >= 1000000)
   {
     var peakAdjustUsage2= ((((peakAdjustUsage1 * 1024) * 1024) * 1.024) * 1.024);
   }
   else if ( peakAdjustUsage1 >= 1000)
   {
     var peakAdjustUsage2= (((peakAdjustUsage1 * 1024) * 1024) * 1.024);
   }
   else if ( peakAdjustUsage1 >= 1)
   {
     var peakAdjustUsage2= ((peakAdjustUsage1 * 1024) * 1024);
   }
   System.Gadget.Settings.write ("saveTotalPeak", peakAdjustUsage2 );
   if (offpeakCycleQuotaSize.value == 1)
   {
     var setOffpeakTotal1= offpeakCycleQuota.value * 1;
   }
   else if (offpeakCycleQuotaSize.value == 2)
   {
     var setOffpeakTotal1= offpeakCycleQuota.value * 1000;
   }
   else if (offpeakCycleQuotaSize.value == 3)
   {
     var setOffpeakTotal1= offpeakCycleQuota.value * 1000000;
   }
   if ( setOffpeakTotal1 >= 1000000000)
   {
     var setOffpeakTotal2= (((((setOffpeakTotal1 * 1024) * 1024) * 1.024) * 1.024) * 1.024);
   }
   else if ( setOffpeakTotal1 >= 1000000)
   {
     var setOffpeakTotal2= ((((setOffpeakTotal1 * 1024) * 1024) * 1.024) * 1.024);
   }
   else if ( setOffpeakTotal1 >= 1000)
   {
     var setOffpeakTotal2= (((setOffpeakTotal1 * 1024) * 1024) * 1.024);
   }
   else if ( setOffpeakTotal1 >= 1)
   {
     var setOffpeakTotal2= ((setOffpeakTotal1 * 1024) * 1024);
   }
   System.Gadget.Settings.write ("saveOffpeakTotal", setOffpeakTotal2 );
   if (offpeakAdjustUsageSize.value == 1)
   {
     var offpeakAdjustUsage1= offpeakAdjustUsage.value * 1;
   }
   else if (offpeakAdjustUsageSize.value == 2)
   {
     var offpeakAdjustUsage1= offpeakAdjustUsage.value * 1000;
   }
   else if (offpeakAdjustUsageSize.value == 3)
   {
     var offpeakAdjustUsage1= offpeakAdjustUsage.value * 1000000;
   }
   if ( offpeakAdjustUsage1 >= 1000000000)
   {
     var offpeakAdjustUsage2= (((((offpeakAdjustUsage1 * 1024) * 1024) * 1.024) * 1.024) * 1.024);
   }
   else if ( offpeakAdjustUsage1 >= 1000000)
   {
     var offpeakAdjustUsage2= ((((offpeakAdjustUsage1 * 1024) * 1024) * 1.024) * 1.024);
   }
   else if ( offpeakAdjustUsage1 >= 1000)
   {
     var offpeakAdjustUsage2= (((offpeakAdjustUsage1 * 1024) * 1024) * 1.024);
   }
   else if ( offpeakAdjustUsage1 >= 1)
   {
     var offpeakAdjustUsage2= ((offpeakAdjustUsage1 * 1024) * 1024);
   }
   System.Gadget.Settings.write ("saveTotalOffPeak", offpeakAdjustUsage2 );
 }
 function onChaAdjustUsage()
 {
   if(adjustUsage.value==0)
   {
     checkadjustUsage=1;
   }
   else
   {
     checkadjustUsage=2;
   }
   System.Gadget.Settings.write ("checkadjustUsage", checkadjustUsage);
 }
 function onChaPeakAdjustUsage()
 {
   if(peakAdjustUsage.value==0)
   {
     checkPeakAdjustUsage=1;
   }
   else
   {
     checkPeakAdjustUsage=2;
   }
   System.Gadget.Settings.write ("checkPeakAdjustUsage", checkPeakAdjustUsage);
 }
 function onChaOffpeakAdjustUsage()
 {
   if(offpeakAdjustUsage.value==0)
   {
     checkOffpeakAdjustUsage=1;
   }
   else
   {
     checkOffpeakAdjustUsage=2;
   }
   System.Gadget.Settings.write ("checkOffpeakAdjustUsage", checkOffpeakAdjustUsage);
 }
 function formatBytes(bytes)
 {
   if (bytes > 1125899906842624)  return sixdigits(((((bytes * 1024) * 1024) * 1024) * 1024) * 1024);
   if (bytes > 1099511627776) return sixdigits((((bytes * 1024) * 1024) * 1024) * 1024);
   if (bytes > 1073741824) return sixdigits(((bytes * 1024) * 1024) * 1024);
   if (bytes > 1048576) return sixdigits((bytes * 1024) * 1024);
   if (bytes > 1024) return sixdigits(bytes * 1024);
   return sixdigits(bytes);
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
 function sizeSettings()
 {
   if(fixsize.value == "Custom")
   {
     O('ssize').style.visibility = "visible";
     O('sizetext').style.visibility = "visible";
   }
   else
   {
     O('ssize').style.visibility = "hidden";
     O('sizetext').style.visibility = "hidden";
   }
 }
 function graphSettings()
 {
   if(graph.value == 1)
   {
     O('drawstyle').style.visibility = "visible";
     O('drawtext').style.visibility = "visible";
   }
   else
   {
     O('drawstyle').style.visibility = "hidden";
     O('drawtext').style.visibility = "hidden";
   }
 }
 function colorSettings()
 {
   if(fixBackg.value == '')
   {
     O('backg').style.visibility = "visible";
     O('showBackg').style.visibility = "hidden";
   }
   else
   {
     O('backg').style.visibility = "hidden";
     O('showBackg').style.visibility = "visible";
   }
   if(fixTitle.value == '')
   {
     O('title').style.visibility = "visible";
     O('showTitle').style.visibility = "hidden";
   }
   else
   {
     O('title').style.visibility = "hidden";
     O('showTitle').style.visibility = "visible";
   }
   if(fixNetIN.value == '')
   {
     O('NetIN').style.visibility = "visible";
     O('showNetIN').style.visibility = "hidden";
   }
   else
   {
     O('NetIN').style.visibility = "hidden";
     O('showNetIN').style.visibility = "visible";
   }
   if(fixAlertIcon.value == '')
   {
     O('AlertIcon').style.visibility = "visible";
     O('showAlertIcon').style.visibility = "hidden";
   }
   else
   {
     O('AlertIcon').style.visibility = "hidden";
     O('showAlertIcon').style.visibility = "visible";
   }
   if(fixSSID.value == '')
   {
     O('SSID').style.visibility = "visible";
     O('showSSID').style.visibility = "hidden";
   }
   else
   {
     O('SSID').style.visibility = "hidden";
     O('showSSID').style.visibility = "visible";
   }
   if(fixINTIP.value == '')
   {
     O('INTIP').style.visibility = "visible";
     O('showINTIP').style.visibility = "hidden";
   }
   else
   {
     O('INTIP').style.visibility = "hidden";
     O('showINTIP').style.visibility = "visible";
   }
   if(fixEXTIP.value == '')
   {
     O('EXTIP').style.visibility = "visible";
     O('showEXTIPA').style.visibility = "hidden";
   }
   else
   {
     O('EXTIP').style.visibility = "hidden";
     O('showEXTIPA').style.visibility = "visible";
   }
   if(fixBLIP.value == '')
   {
     O('BLIP').style.visibility = "visible";
     O('showBLIP').style.visibility = "hidden";
   }
   else
   {
     O('BLIP').style.visibility = "hidden";
     O('showBLIP').style.visibility = "visible";
   }
   if(fixBut.value == '')
   {
     O('But').style.visibility = "visible";
     O('showBut').style.visibility = "hidden";
   }
   else
   {
     O('But').style.visibility = "hidden";
     O('showBut').style.visibility = "visible";
   }
   if(fixFWPF.value == '')
   {
     O('FWPF').style.visibility = "visible";
     O('showFWPF').style.visibility = "hidden";
   }
   else
   {
     O('FWPF').style.visibility = "hidden";
     O('showFWPF').style.visibility = "visible";
   }
   if(fixConn.value == '')
   {
     O('Conn').style.visibility = "visible";
     O('showConn').style.visibility = "hidden";
   }
   else
   {
     O('Conn').style.visibility = "hidden";
     O('showConn').style.visibility = "visible";
   }
   if(fixSign.value == '')
   {
     O('Sign').style.visibility = "visible";
     O('showSign').style.visibility = "hidden";
   }
   else
   {
     O('Sign').style.visibility = "hidden";
     O('showSign').style.visibility = "visible";
   }
   if(fixUplo.value == '')
   {
     O('Uplo').style.visibility = "visible";
     O('showUplo').style.visibility = "hidden";
   }
   else
   {
     O('Uplo').style.visibility = "hidden";
     O('showUplo').style.visibility = "visible";
   }
   if(fixDownlo.value == '')
   {
     O('Downlo').style.visibility = "visible";
     O('showDownlo').style.visibility = "hidden";
   }
   else
   {
     O('Downlo').style.visibility = "hidden";
     O('showDownlo').style.visibility = "visible";
   }
   if(fixCurr.value == '')
   {
     O('Curr').style.visibility = "visible";
     O('showCurr').style.visibility = "hidden";
   }
   else
   {
     O('Curr').style.visibility = "hidden";
     O('showCurr').style.visibility = "visible";
   }
   if(fixCurrUp.value == '')
   {
     O('CurrUp').style.visibility = "visible";
     O('showCurrUp').style.visibility = "hidden";
   }
   else
   {
     O('CurrUp').style.visibility = "hidden";
     O('showCurrUp').style.visibility = "visible";
   }
   if(fixCurrDown.value == '')
   {
     O('CurrDown').style.visibility = "visible";
     O('showCurrDown').style.visibility = "hidden";
   }
   else
   {
     O('CurrDown').style.visibility = "hidden";
     O('showCurrDown').style.visibility = "visible";
   }
   if(fixTota.value == '')
   {
     O('Tota').style.visibility = "visible";
     O('showTota').style.visibility = "hidden";
   }
   else
   {
     O('Tota').style.visibility = "hidden";
     O('showTota').style.visibility = "visible";
   }
   if(fixTotaUp.value == '')
   {
     O('TotaUp').style.visibility = "visible";
     O('showTotaUp').style.visibility = "hidden";
   }
   else
   {
     O('TotaUp').style.visibility = "hidden";
     O('showTotaUp').style.visibility = "visible";
   }
   if(fixTotaDown.value == '')
   {
     O('TotaDown').style.visibility = "visible";
     O('showTotaDown').style.visibility = "hidden";
   }
   else
   {
     O('TotaDown').style.visibility = "hidden";
     O('showTotaDown').style.visibility = "visible";
   }
   if(fixRemPercent.value == '')
   {
     O('RemPercent').style.visibility = "visible";
     O('showRemPercent').style.visibility = "hidden";
   }
   else
   {
     O('RemPercent').style.visibility = "hidden";
     O('showRemPercent').style.visibility = "visible";
   }
   if(fixRemDays.value == '')
   {
     O('RemDays').style.visibility = "visible";
     O('showRemDays').style.visibility = "hidden";
   }
   else
   {
     O('RemDays').style.visibility = "hidden";
     O('showRemDays').style.visibility = "visible";
   }
   if(fixRemQuota.value == '')
   {
     O('RemQuota').style.visibility = "visible";
     O('showRemQuota').style.visibility = "hidden";
   }
   else
   {
     O('RemQuota').style.visibility = "hidden";
     O('showRemQuota').style.visibility = "visible";
   }
   if(fixRemPerDay.value == '')
   {
     O('RemPerDay').style.visibility = "visible";
     O('showRemPerDay').style.visibility = "hidden";
   }
   else
   {
     O('RemPerDay').style.visibility = "hidden";
     O('showRemPerDay').style.visibility = "visible";
   }
   if(fixRemUsed.value == '')
   {
     O('RemUsed').style.visibility = "visible";
     O('showRemUsed').style.visibility = "hidden";
   }
   else
   {
     O('RemUsed').style.visibility = "hidden";
     O('showRemUsed').style.visibility = "visible";
   }
   if(fixPeakUsed.value == '')
   {
     O('PeakUsed').style.visibility = "visible";
     O('showPeakUsed').style.visibility = "hidden";
   }
   else
   {
     O('PeakUsed').style.visibility = "hidden";
     O('showPeakUsed').style.visibility = "visible";
   }
   if(fixOffpeakUsed.value == '')
   {
     O('OffpeakUsed').style.visibility = "visible";
     O('showOffpeakUsed').style.visibility = "hidden";
   }
   else
   {
     O('OffpeakUsed').style.visibility = "hidden";
     O('showOffpeakUsed').style.visibility = "visible";
   }
   if(fixFlyoutBac.value == '')
   {
     O('FlyoutBac').style.visibility = "visible";
     O('showFlyoutBac').style.visibility = "hidden";
   }
   else
   {
     O('FlyoutBac').style.visibility = "hidden";
     O('showFlyoutBac').style.visibility = "visible";
   }
   if(fixFlyoutTit.value == '')
   {
     O('FlyoutTit').style.visibility = "visible";
     O('showFlyoutTit').style.visibility = "hidden";
   }
   else
   {
     O('FlyoutTit').style.visibility = "hidden";
     O('showFlyoutTit').style.visibility = "visible";
   }
   if(fixFlyoutDet.value == '')
   {
     O('FlyoutDet').style.visibility = "visible";
     O('showFlyoutDet').style.visibility = "hidden";
   }
   else
   {
     O('FlyoutDet').style.visibility = "hidden";
     O('showFlyoutDet').style.visibility = "visible";
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
   if (fixNetIN.value != "") sNetIN = fixNetIN.value ;
   else sNetIN = NetIN.value;
   O('showNetIN').style.color = sNetIN;
   O('showNetIN').style.width = 50;
   O('showNetIN').style.height = 21;
   O('showNetIN').style.top = 52;
   O('showNetIN').style.left = 206;
   if (fixAlertIcon.value != "") sAlertIcon = fixAlertIcon.value ;
   else sAlertIcon = AlertIcon.value;
   O('showAlertIcon').style.color = sAlertIcon;
   O('showAlertIcon').style.width = 50;
   O('showAlertIcon').style.height = 21;
   O('showAlertIcon').style.top = 75;
   O('showAlertIcon').style.left = 206;
   if (fixSSID.value != "") sSSID = fixSSID.value ;
   else sSSID = SSID.value;
   O('showSSID').style.color = sSSID;
   O('showSSID').style.width = 50;
   O('showSSID').style.height = 21;
   O('showSSID').style.top = 98;
   O('showSSID').style.left = 206;
   if (fixINTIP.value != "") sINTIP = fixINTIP.value ;
   else sINTIP = INTIP.value;
   O('showINTIP').style.color = sINTIP;
   O('showINTIP').style.width = 50;
   O('showINTIP').style.height = 21;
   O('showINTIP').style.top = 121;
   O('showINTIP').style.left = 206;
   if (fixEXTIP.value != "") sEXTIP = fixEXTIP.value ;
   else sEXTIP = EXTIP.value;
   O('showEXTIPA').style.color = sEXTIP;
   O('showEXTIPA').style.width = 50;
   O('showEXTIPA').style.height = 21;
   O('showEXTIPA').style.top = 144;
   O('showEXTIPA').style.left = 206;
   if (fixBLIP.value != "") sBLIP = fixBLIP.value ;
   else sBLIP = BLIP.value;
   O('showBLIP').style.color = sBLIP;
   O('showBLIP').style.width = 50;
   O('showBLIP').style.height = 21;
   O('showBLIP').style.top = 167;
   O('showBLIP').style.left = 206;
   if (fixBut.value != "") sBut = fixBut.value ;
   else sBut = But.value;
   O('showBut').style.color = sBut;
   O('showBut').style.width = 50;
   O('showBut').style.height = 21;
   O('showBut').style.top = 190;
   O('showBut').style.left = 206;
   if (fixFWPF.value != "") sFWPF = fixFWPF.value ;
   else sFWPF = FWPF.value;
   O('showFWPF').style.color = sFWPF;
   O('showFWPF').style.width = 50;
   O('showFWPF').style.height = 21;
   O('showFWPF').style.top = 213;
   O('showFWPF').style.left = 206;
   if (fixConn.value != "") sConn = fixConn.value ;
   else sConn = Conn.value;
   O('showConn').style.color = sConn;
   O('showConn').style.width = 50;
   O('showConn').style.height = 21;
   O('showConn').style.top = 236;
   O('showConn').style.left = 206;
   if (fixSign.value != "") sSign = fixSign.value ;
   else sSign = Sign.value;
   O('showSign').style.color = sSign;
   O('showSign').style.width = 50;
   O('showSign').style.height = 21;
   O('showSign').style.top = 259;
   O('showSign').style.left = 206;
   if (fixUplo.value != "") sUplo = fixUplo.value ;
   else sUplo = Uplo.value;
   O('showUplo').style.color = sUplo;
   O('showUplo').style.width = 50;
   O('showUplo').style.height = 21;
   O('showUplo').style.top = 312;
   O('showUplo').style.left = 206;
   if (fixDownlo.value != "") sDownlo = fixDownlo.value ;
   else sDownlo = Downlo.value;
   O('showDownlo').style.color = sDownlo;
   O('showDownlo').style.width = 50;
   O('showDownlo').style.height = 21;
   O('showDownlo').style.top = 335;
   O('showDownlo').style.left = 206;
   if (fixCurr.value != "") sCurr = fixCurr.value ;
   else sCurr = Curr.value;
   O('showCurr').style.color = sCurr;
   O('showCurr').style.width = 50;
   O('showCurr').style.height = 21;
   O('showCurr').style.top = 388;
   O('showCurr').style.left = 206;
   if (fixCurrUp.value != "") sCurrUp = fixCurrUp.value ;
   else sCurrUp = CurrUp.value;
   O('showCurrUp').style.color = sCurrUp;
   O('showCurrUp').style.width = 50;
   O('showCurrUp').style.height = 21;
   O('showCurrUp').style.top = 411;
   O('showCurrUp').style.left = 206;
   if (fixCurrDown.value != "") sCurrDown = fixCurrDown.value ;
   else sCurrDown = CurrDown.value;
   O('showCurrDown').style.color = sCurrDown;
   O('showCurrDown').style.width = 50;
   O('showCurrDown').style.height = 21;
   O('showCurrDown').style.top = 434;
   O('showCurrDown').style.left = 206;
   if (fixTota.value != "") sTota = fixTota.value ;
   else sTota = Tota.value;
   O('showTota').style.color = sTota;
   O('showTota').style.width = 50;
   O('showTota').style.height = 21;
   O('showTota').style.top = 487;
   O('showTota').style.left = 206;
   if (fixTotaUp.value != "") sTotaUp = fixTotaUp.value ;
   else sTotaUp = TotaUp.value;
   O('showTotaUp').style.color = sTotaUp;
   O('showTotaUp').style.width = 50;
   O('showTotaUp').style.height = 21;
   O('showTotaUp').style.top = 510;
   O('showTotaUp').style.left = 206;
   if (fixTotaDown.value != "") sTotaDown = fixTotaDown.value ;
   else sTotaDown = TotaDown.value;
   O('showTotaDown').style.color = sTotaDown;
   O('showTotaDown').style.width = 50;
   O('showTotaDown').style.height = 21;
   O('showTotaDown').style.top = 533;
   O('showTotaDown').style.left = 206;
   if (fixRemPercent.value != "") sRemPercent = fixRemPercent.value ;
   else sRemPercent = RemPercent.value;
   O('showRemPercent').style.color = sRemPercent;
   O('showRemPercent').style.width = 50;
   O('showRemPercent').style.height = 21;
   O('showRemPercent').style.top = 586;
   O('showRemPercent').style.left = 206;
   if (fixRemDays.value != "") sRemDays = fixRemDays.value ;
   else sRemDays = RemDays.value;
   O('showRemDays').style.color = sRemDays;
   O('showRemDays').style.width = 50;
   O('showRemDays').style.height = 21;
   O('showRemDays').style.top = 609;
   O('showRemDays').style.left = 206;
   if (fixRemQuota.value != "") sRemQuota = fixRemQuota.value ;
   else sRemQuota = RemQuota.value;
   O('showRemQuota').style.color = sRemQuota;
   O('showRemQuota').style.width = 50;
   O('showRemQuota').style.height = 21;
   O('showRemQuota').style.top = 632;
   O('showRemQuota').style.left = 206;
   if (fixRemPerDay.value != "") sRemPerDay = fixRemPerDay.value ;
   else sRemPerDay = RemPerDay.value;
   O('showRemPerDay').style.color = sRemPerDay;
   O('showRemPerDay').style.width = 50;
   O('showRemPerDay').style.height = 21;
   O('showRemPerDay').style.top = 655;
   O('showRemPerDay').style.left = 206;
   if (fixRemUsed.value != "") sRemUsed = fixRemUsed.value ;
   else sRemUsed = RemUsed.value;
   O('showRemUsed').style.color = sRemUsed;
   O('showRemUsed').style.width = 50;
   O('showRemUsed').style.height = 21;
   O('showRemUsed').style.top = 678;
   O('showRemUsed').style.left = 206;
   if (fixPeakUsed.value != "") sPeakUsed = fixPeakUsed.value ;
   else sPeakUsed = PeakUsed.value;
   O('showPeakUsed').style.color = sPeakUsed;
   O('showPeakUsed').style.width = 50;
   O('showPeakUsed').style.height = 21;
   O('showPeakUsed').style.top = 701;
   O('showPeakUsed').style.left = 206;
   if (fixOffpeakUsed.value != "") sOffpeakUsed = fixOffpeakUsed.value ;
   else sOffpeakUsed = OffpeakUsed.value;
   O('showOffpeakUsed').style.color = sOffpeakUsed;
   O('showOffpeakUsed').style.width = 50;
   O('showOffpeakUsed').style.height = 21;
   O('showOffpeakUsed').style.top = 724;
   O('showOffpeakUsed').style.left = 206;
   if (fixFlyoutBac.value != "") sFlyoutBac = fixFlyoutBac.value ;
   else sFlyoutBac = FlyoutBac.value;
   O('showFlyoutBac').style.color = sFlyoutBac;
   O('showFlyoutBac').style.width = 50;
   O('showFlyoutBac').style.height = 21;
   O('showFlyoutBac').style.top = 777;
   O('showFlyoutBac').style.left = 206;
   if (fixFlyoutTit.value != "") sFlyoutTit = fixFlyoutTit.value ;
   else sFlyoutTit = FlyoutTit.value;
   O('showFlyoutTit').style.color = sFlyoutTit;
   O('showFlyoutTit').style.width = 50;
   O('showFlyoutTit').style.height = 21;
   O('showFlyoutTit').style.top = 800;
   O('showFlyoutTit').style.left = 206;
   if (fixFlyoutDet.value != "") sFlyoutDet = fixFlyoutDet.value ;
   else sFlyoutDet = FlyoutDet.value;
   O('showFlyoutDet').style.color = sFlyoutDet;
   O('showFlyoutDet').style.width = 50;
   O('showFlyoutDet').style.height = 21;
   O('showFlyoutDet').style.top = 823;
   O('showFlyoutDet').style.left = 206;
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
   var urlData=getURL("http://addgadgets.com/network_meter/version.htm");
   if(urlData===false)
   {
     return false
   }
   var version="9.6";
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
   netType.value= 3;
   method.value= 2;
   autoNetwork.value= 1;
   autoInterface.checked = 1;
   scanNetwork.value= 10;
   settimer.value= 1;
   saveChartData.value= 2;
   saveIPAddress.value=1;
   alertLostCon.value=2;
   soundLostCon.value=1;
   soundLostConurl.innerText='';
   soundLostConVol.value=100;
   soundLostConRepeats.value=3;
   soundLostConCount.value=1;
   setRemaining.value=2;
   billingStarts.value=1;
   cycleQuota.value=100;
   billingWeek.value=1;
   billingMonth.value=1;
   cycleQuotaSize.value=1;
   peakHour.value=8;
   peakMin.value=30;
   peakCycleQuota.value=100;
   peakCycleQuotaSize.value=1;
   offpeakHour.value=20;
   offpeakMin.value=30;
   offpeakCycleQuota.value='';
   offpeakCycleQuotaSize.value=4;
   update.value= 1;
   changemethod();
   setPeakRemaining();
   cbStarts();
   setCheckRemaining();
 }
 function DefDisplaySetting()
 {
   ssize.value= 100;
   fixsize.value= 100;
   showNetworkAdaName.value=1;
   showInternalIP.value=1;
   autoiip.value= 1;
   setiiptimer.value= 10;
   showextip.value= 1;
   autoeip.value= 2;
   seteiptimer.value= 10;
   showBlacklisted.value=1;
   showSTIL.value=1;
   showFirewall.value=1;
   VisMode.value= 0;
   barup.value= 0;
   bardown.value= 0;
   fixUnit.value= 1;
   graph.value= 1;
   drawstyle.value= 0;
   ScaleType.value= 0;
   ScaleSize.value= 0;
   totalbandwidth.value= 1;
   saveWriteUsage.value= 1;
   bytesSentSize.value= 1;
   bytesReceivedSize.value= 1;
   showSearch.value=1;
   sizeSettings();
   graphSettings();
 }
 function DefColorSetting()
 {
   backg.value= "080808";
   fixBackg.value= "";
   title.value="FFFFFF";
   fixTitle.value="";
   NetIN.value="90EE90";
   fixNetIN.value="";
   AlertIcon.value="90EE90";
   fixAlertIcon.value="";
   SSID.value="87CEFA";
   fixSSID.value="";
   INTIP.value="87CEFA";
   fixINTIP.value="";
   EXTIP.value="87CEFA";
   fixEXTIP.value="";
   BLIP.value="FFF62A";
   fixBLIP.value="";
   But.value="FFCC00";
   fixBut.value="";
   FWPF.value="EC7527";
   fixFWPF.value="";
   Conn.value="87CEFA";
   fixConn.value="";
   Sign.value="87CEFA";
   fixSign.value="";
   Uplo.value="90EE90";
   fixUplo.value="";
   Downlo.value="FFF62A";
   fixDownlo.value="";
   Curr.value="87CEFA";
   fixCurr.value="";
   CurrUp.value="90EE90";
   fixCurrUp.value="";
   CurrDown.value="FFF62A";
   fixCurrDown.value="";
   Tota.value="87CEFA";
   fixTota.value="";
   TotaUp.value="90EE90";
   fixTotaUp.value="";
   TotaDown.value="FFF62A";
   fixTotaDown.value="";
   RemPercent.value="87CEFA";
   fixRemPercent.value="";
   RemDays.value="87CEFA";
   fixRemDays.value="";
   RemQuota.value="87CEFA";
   fixRemQuota.value="";
   RemPerDay.value="87CEFA";
   fixRemPerDay.value="";
   RemUsed.value="90EE90";
   fixRemUsed.value="";
   PeakUsed.value="90EE90";
   fixPeakUsed.value="";
   OffpeakUsed.value="90EE90";
   fixOffpeakUsed.value="";
   FlyoutBac.value="080808";
   fixFlyoutBac.value="";
   FlyoutTit.value="87CEFA";
   fixFlyoutTit.value="";
   FlyoutDet.value="FFCC00";
   fixFlyoutDet.value="";
   colorSettings();
   showColor();
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
 