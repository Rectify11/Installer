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
 var size = 1;
 var objWMIService = GetObject("winmgmts:\\\\" + "localhost" + "\\root\\CIMV2");
 var wbemFlagReturnImmediately = 0x10;
 var wbemFlagForwardOnly = 0x20;
 function onLoad()
 {
   foNetworkInt = System.Gadget.Settings.read("foNetworkInt");
   if ( foNetworkInt != "") ;
   else foNetworkInt = "";
   size = System.Gadget.Settings.read("size");
   if ( size != "") ;
   else size = "1";
   if (size <= "4") ;
   else size = "4";
   FlyoutBac = System.Gadget.Settings.read("FlyoutBac");
   if ( FlyoutBac != "") ;
   else FlyoutBac = "#080808";
   fixFlyoutBac = System.Gadget.Settings.read("fixFlyoutBac");
   if (fixFlyoutBac != "") sFlyoutBac = fixFlyoutBac ;
   else sFlyoutBac = FlyoutBac;
   FlyoutTit = System.Gadget.Settings.read("FlyoutTit");
   if ( FlyoutTit != "") ;
   else FlyoutTit = "#87cefa";
   fixFlyoutTit = System.Gadget.Settings.read("fixFlyoutTit");
   if (fixFlyoutTit != "") sFlyoutTit = fixFlyoutTit ;
   else sFlyoutTit = FlyoutTit;
   FlyoutDet = System.Gadget.Settings.read("FlyoutDet");
   if ( FlyoutDet != "") ;
   else FlyoutDet = "#ffcc00";
   fixFlyoutDet = System.Gadget.Settings.read("fixFlyoutDet");
   if (fixFlyoutDet != "") sFlyoutDet = fixFlyoutDet ;
   else sFlyoutDet = FlyoutDet;
   getInfo1();
   getInfo2();
   sizeMode();
 }
 function getInfo1()
 {
   var netAda = objWMIService.ExecQuery("SELECT * FROM Win32_NetworkAdapterConfiguration Where Description="+"'"+foNetworkInt+"'"+"", "WQL", wbemFlagReturnImmediately | wbemFlagForwardOnly);
   var adaNet = new Enumerator(netAda);
   objItem = adaNet.item();
   try
   {
     DefaultIPGateway.innerHTML = "Default Gateway: " + "<a href='javascript:cDefaultIPGateway()' style='text-decoration:none'><font color="+sFlyoutDet+">" + objItem.DefaultIPGateway.toArray()[0] + "</a>"
   }
   catch (err)
   {
     DefaultIPGateway.innerHTML = "Default Gateway:"
   }
   try
   {
     Description.innerHTML = "<a href='javascript:cDescription()' style='text-decoration:none'><font color="+sFlyoutDet+">" + objItem.Description + "</a>"
   }
   catch (err)
   {
     Description.innerHTML = "Description: Unknown"
   }
   try
   {
     DHCPLeaseExpires.innerHTML = "Lease Expires: <span style='color:"+sFlyoutDet+"'>" + objItem.DHCPLeaseExpires.replace(/^(\d{4})(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})(.*)$/,"$1-$2-$3 $4:$5:$6") + "</span>"
   }
   catch (err)
   {
     DHCPLeaseExpires.innerHTML = "Lease Expires:"
   }
   try
   {
     DHCPLeaseObtained.innerHTML = "Lease Obtained: <span style='color:"+sFlyoutDet+"'>" + objItem.DHCPLeaseObtained.replace(/^(\d{4})(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})(.*)$/,"$1-$2-$3 $4:$5:$6") + "</span>"
   }
   catch (err)
   {
     DHCPLeaseObtained.innerHTML = "Lease Obtained:"
   }
   try
   {
     DHCPServer.innerHTML = "DHCP Server: " + "<a href='javascript:cDHCPServer()' style='text-decoration:none'><font color="+sFlyoutDet+">" + objItem.DHCPServer + "</a>"
   }
   catch (err)
   {
     DHCPServer.innerHTML = "DHCP Server:"
   }
   try
   {
     DNSServerSearchOrder.innerHTML = "DNS Server: " + "<a href='javascript:cDNSServerSearchOrder()' style='text-decoration:none'><font color="+sFlyoutDet+">" + objItem.DNSServerSearchOrder.toArray()[0] + "</font></a>"
   }
   catch (err)
   {
     DNSServerSearchOrder.innerHTML = "DNS Server:"
   }
   try
   {
     IPSubnet.innerHTML = "Subnet Mask: " + "<a href='javascript:cIPSubnet()' style='text-decoration:none'><font color="+sFlyoutDet+">" + objItem.IPSubnet.toArray()[0] + "</a>"
   }
   catch (err)
   {
     IPSubnet.innerHTML = "Subnet Mask:"
   }
   try
   {
     MACAddress.innerHTML = "Physical Address: " + "<a href='javascript:cMACAddress()' style='text-decoration:none'><font color="+sFlyoutDet+">" + objItem.MACAddress + "</a>"
   }
   catch (err)
   {
     MACAddress.innerHTML = "Physical Address:"
   }
   try
   {
     WINSPrimaryServer.innerHTML = "WINS Server: " + "<a href='javascript:cWINSPrimaryServer()' style='text-decoration:none'><font color="+sFlyoutDet+">" + objItem.WINSPrimaryServer + "</a>"
   }
   catch (err)
   {
     WINSPrimaryServer.innerHTML = "WINS Server:"
   }
   try
   {
     DHCPEnabled.innerHTML = "DHCP Enabled: <span style='color:"+sFlyoutDet+"'>" + objItem.DHCPEnabled + "</span>"
   }
   catch (err)
   {
     DHCPEnabled.innerHTML = "DHCP Enabled:"
   }
   try
   {
     IPAddress.innerHTML = "IP Address: " + "<a href='javascript:cIPAddress()' style='text-decoration:none'><font color="+sFlyoutDet+">" + objItem.IPAddress.toArray()[0] + "</a>"
   }
   catch (err)
   {
     IPAddress.innerHTML = "IP Address:"
   }
   try
   {
     getTcpipNetbiosOptions = objItem.TcpipNetbiosOptions;
     if (getTcpipNetbiosOptions==0)
     {
       TcpipNetbiosOptions.innerHTML = "NetBIOS over Tcpip Enabled: <span style='color:"+sFlyoutDet+"'>Enable Netbios Via DHCP</span>";
     }
     if (getTcpipNetbiosOptions==1)
     {
       TcpipNetbiosOptions.innerHTML = "NetBIOS over Tcpip Enabled: <span style='color:"+sFlyoutDet+"'>Enable Netbios</span>";
     }
     if (getTcpipNetbiosOptions==2)
     {
       TcpipNetbiosOptions.innerHTML = "NetBIOS over Tcpip Enabled: <span style='color:"+sFlyoutDet+"'>Disable Netbios</span>";
     }
   }
   catch (err)
   {
     TcpipNetbiosOptions.innerHTML = "NetBIOS over Tcpip Enabled:"
   }
 }
 function getInfo2()
 {
   var netIndex = objItem.Index;
   var netAda2 = objWMIService.ExecQuery("SELECT * FROM Win32_NetworkAdapter Where Index= "+ netIndex +"", "WQL", wbemFlagReturnImmediately | wbemFlagForwardOnly);
   var adaNet2 = new Enumerator(netAda2);
   objItem2 = adaNet2.item();
   try
   {
     getSpeed = objItem2.Speed;
     if (getSpeed==1000000000)
     {
       Speed.innerHTML = "Speed: <span style='color:"+sFlyoutDet+"'>1Gbps</span>"
     }
     else
     {
       var speedNetAda = Math.round(objItem2.Speed/1000000);
       Speed.innerHTML = "Speed: <span style='color:"+sFlyoutDet+"'>"+speedNetAda+"Mbps</span>"
     }
   }
   catch (err)
   {
     Speed.innerHTML = "Speed:"
   }
   try
   {
     NetConnectionID.innerHTML = "<u><b>" + objItem2.NetConnectionID + "</b></u>"
   }
   catch (err)
   {
     NetConnectionID.innerHTML = "Net Connection ID: Unknown"
   }
   try
   {
     getNetConnectionStatus = objItem2.NetConnectionStatus;
     if (getNetConnectionStatus==0)
     {
       NetConnectionStatus.innerHTML = "Status: <span style='color:"+sFlyoutDet+"'>Disconnected</span>";
     }
     if (getNetConnectionStatus==1)
     {
       NetConnectionStatus.innerHTML = "Status: <span style='color:"+sFlyoutDet+"'>Connecting</span>";
     }
     if (getNetConnectionStatus==2)
     {
       NetConnectionStatus.innerHTML = "Status: <span style='color:"+sFlyoutDet+"'>Connected</span>";
     }
     if (getNetConnectionStatus==3)
     {
       NetConnectionStatus.innerHTML = "Status: <span style='color:"+sFlyoutDet+"'>Disconnecting</span>";
     }
     if (getNetConnectionStatus==4)
     {
       NetConnectionStatus.innerHTML = "Status: <span style='color:"+sFlyoutDet+"'>Hardware not present</span>";
     }
     if (getNetConnectionStatus==5)
     {
       NetConnectionStatus.innerHTML = "Status: <span style='color:"+sFlyoutDet+"'>Hardware disabled</span>";
     }
     if (getNetConnectionStatus==6)
     {
       NetConnectionStatus.innerHTML = "Status: <span style='color:"+sFlyoutDet+"'>Hardware malfunction</span>";
     }
     if (getNetConnectionStatus==7)
     {
       NetConnectionStatus.innerHTML = "Status: <span style='color:"+sFlyoutDet+"'>Media disconnected</span>";
     }
     if (getNetConnectionStatus==8)
     {
       NetConnectionStatus.innerHTML = "Status: <span style='color:"+sFlyoutDet+"'>Authenticating</span>";
     }
     if (getNetConnectionStatus==9)
     {
       NetConnectionStatus.innerHTML = "Status: <span style='color:"+sFlyoutDet+"'>Authentication succeeded</span>";
     }
     if (getNetConnectionStatus==10)
     {
       NetConnectionStatus.innerHTML = "Status: <span style='color:"+sFlyoutDet+"'>Authentication failed</span>";
     }
     if (getNetConnectionStatus==11)
     {
       NetConnectionStatus.innerHTML = "Status: <span style='color:"+sFlyoutDet+"'>Invalid address</span>";
     }
     if (getNetConnectionStatus==12)
     {
       NetConnectionStatus.innerHTML = "Status: <span style='color:"+sFlyoutDet+"'>Credentials required</span>";
     }
   }
   catch (err)
   {
     NetConnectionStatus.innerHTML = "Status:"
   }
 }
 function cDefaultIPGateway()
 {
   window.clipboardData.setData('Text',objItem.DefaultIPGateway.toArray()[0]);
 }
 function cDescription()
 {
   window.clipboardData.setData('Text',objItem.Description);
 }
 function cDHCPServer()
 {
   window.clipboardData.setData('Text',objItem.DHCPServer);
 }
 function cDNSServerSearchOrder()
 {
   window.clipboardData.setData('Text',objItem.DNSServerSearchOrder.toArray()[0]);
 }
 function cIPSubnet()
 {
   window.clipboardData.setData('Text',objItem.IPSubnet.toArray()[0]);
 }
 function cMACAddress()
 {
   window.clipboardData.setData('Text',objItem.MACAddress);
 }
 function cWINSPrimaryServer()
 {
   window.clipboardData.setData('Text',objItem.WINSPrimaryServer);
 }
 function cIPAddress()
 {
   window.clipboardData.setData('Text',objItem.IPAddress.toArray()[0]);
 }
 function sizeMode()
 {
   document.body.style.width = parseInt( 320 * size );
   document.body.style.height = parseInt( 210 * size );
   document.body.style.backgroundColor = sFlyoutBac;
   document.body.style.fontFamily = "Segoe UI";
   document.body.style.fontSize = parseInt( 11 * size );
   document.body.style.color = sFlyoutTit;
   document.getElementById('NetConnectionID').style.fontSize = parseInt( 13 * size );
   document.getElementById('Description').style.fontSize = parseInt( 12 * size );
   document.body.alinkColor = sFlyoutDet;
   document.vlinkColor = sFlyoutDet;
 }
 