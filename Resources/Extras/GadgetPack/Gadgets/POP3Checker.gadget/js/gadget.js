var MsgCount = "";
var user = "";
var pass = "";
var host = "";
var t;

////////////////////////////////////////////////////////////////////////////////////////////////////
//Two small functions to make gadgets high-dpi compatible by the author of 8GadgetPack  (Version 1)
//Include this if you use addTextObject or addImageObject.
//You also have to wrap fixDpiObject around every call to one of those functions.
//For example replace "g.addTextObject(...)" with "fixDpiObject(g.addTextObject(...))"
////////////////////////////////////////////////////////////////////////////////////////////////////
function getDpiScaling() {
    var wshShell = new ActiveXObject("WScript.Shell");
    var DPI = 96;
    try {
        try {
            //You can set custom DPI in 8GadgetPack
            DPI = wshShell.RegRead("HKCU\\Software\\8GadgetPack\\ForceDPI");
        }
        catch (e) {
            //In case no custom DPI is set or 8GadgetPack isn't installed
            DPI = wshShell.RegRead("HKCU\\Control Panel\\Desktop\\LogPixels");
        }
        wshShell.RegRead("HKCU\\Software\\8GadgetPack\\NoGadgetScalingFix"); //In case I'll be able to fix this in sidebar.exe I will set this registry entry
        DPI = 96;
    }
    catch (e) { }
    return parseInt((DPI / 96) * 100) / 100;
}
var dpiScaling = getDpiScaling();
function fixDpiObject(obj) {
    if ("fontsize" in obj) {
        obj.left = obj.left * dpiScaling;
        obj.top = obj.top * dpiScaling;
    }
    else {
        obj.left = obj.left * dpiScaling + (obj.width * dpiScaling - obj.width) / 2;
        obj.top = obj.top * dpiScaling + (obj.height * dpiScaling - obj.height) / 2;
    }
    obj.width = obj.width * dpiScaling;
    obj.height = obj.height * dpiScaling;
    return obj;
}
////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////

window.onload = function(){
    var gOpacitySetting = System.Gadget.Settings.read("gOpacity");
    if (gOpacitySetting != "")
        background.opacity = gOpacitySetting;

    var pUser = System.Gadget.Settings.read("pUser");
    var pPass = System.Gadget.Settings.read("pPass");
    var pServer = System.Gadget.Settings.read("pServer");

    if (pUser != "")
        user = pUser;
    if (pPass != "")
        pass = pPass;
    if (pServer != "")
        host = pServer;

    System.Gadget.settingsUI = "Settings.html";
    System.Gadget.onSettingsClosed = SettingsClosed;

    checkformail();
}

function SettingsClosed() {
    var gOpacitySetting = System.Gadget.Settings.read("gOpacity");
    var pUser = System.Gadget.Settings.read("pUser");
    var pPass = System.Gadget.Settings.read("pPass");
    var pServer = System.Gadget.Settings.read("pServer");
	
	
    if (gOpacitySetting != "")
        background.opacity = gOpacitySetting;
    
    if (pUser != "")
        user = pUser;
	if (pPass != "")
	    pass = pPass;
	if (pServer != "")
	    host = pServer;
	clearTimeout(t);
	checkformail();
}
function checkformail(){
    var NewMsg;
    var WshShell = new ActiveXObject("WScript.Shell");
    NewMsg = false;

    var args = "/U:"+user+" /P:"+pass+" /H:"+host;

    System.Gadget.beginTransition();	

    background.removeObjects();
    var mailImage = fixDpiObject(background.addImageObject("images/Email40.png", 7, 10));
    mailImage.addGlow( "gray", 1, 30 );

    var exec = WshShell.Exec(System.Gadget.path + "\\Client\\POP3Gadget.exe " + args);
    var outputData = new String(exec.StdOut.ReadAll());
    outputData = outputData.substring(0,outputData.length-1);

    var textObject = fixDpiObject(background.addTextObject("messages", "Segoe UI", 13, "White", 50, 30)); 
    if(outputData == "")
        outputData = 0;

    textObject = fixDpiObject(background.addTextObject(outputData + " new", "Segoe UI", 13, "White", 50, 15));

    if (MsgCount != "" && MsgCount != outputData)
    {
        System.Sound.playSound("Windows XP Notify.wav");
    }
    MsgCount = outputData;

    System.Gadget.endTransition( System.Gadget.TransitionType.morph, 0.5 );
    // 15mins
    t = setTimeout(checkformail, 900000);
    // Test
  //var t = setTimeout(checkformail, 900);

}
