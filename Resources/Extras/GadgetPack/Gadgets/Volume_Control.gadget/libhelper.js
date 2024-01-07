/////////////////////////////////////////////////////////////////////////////////////
//                                                                                 //
//  Libhelper 1.0 for .Net ActiveX Objects by Orbmu2k © 2007                       //
//                                                                                 //
//  Based on the some code of "Network Utilization" Gadget by Jonathan Abbott      //
//  Many thanks to Jonathan Abbott this way :)                                     //
//                                                                                 //
//  Copyright © 2007 Orbmu2k.  All rights reserved.                                //
//                                                                                 //
//  http://blog.orbmu2k.de                                                         //
//                                                                                 //
//  Email: sidebargadget@orbmu2k.de                                                //
//                                                                                 //
/////////////////////////////////////////////////////////////////////////////////////

// ################## CONFIGURATION ###################
var dllCLSID = "{71B1723C-1EC2-4b4d-868E-FA58C7F95CD9}";
var Classname = "SoundControl.SoundControlGadget";
var LibPath = "file:///" + System.Gadget.path.replace(new RegExp("\\\\", "g"), "/") + "/SoundControl.dll"
var LibName = "SoundControl";
// ####################################################

// Gloabal Vars
var oShell = new ActiveXObject("WScript.Shell");
var regRoot;

// is debugging ?
var dbg = new ActiveXObject("Scripting.FileSystemObject");
var bDebug = dbg.FileExists(System.Gadget.path+"\\debug.txt");

// Debuglog
function debugLog(str) 
{
    try
    {
        if (bDebug)
        {
            var oFSO = new ActiveXObject("Scripting.FileSystemObject");
            var iFlag = 2;
            if (str != "")
                iFlag = 8; 
        
            var debugLogFile = oFSO.OpenTextFile(System.Gadget.path+"\\debug.txt", iFlag);
            debugLogFile.WriteLine(new Date().toLocaleString() + ": " + str);
            debugLogFile.Close();
            debugLogFile = null;
        }
    } 
    catch(err) 
    {
    }
}

// Register ActiveX component
function RegisterLibrary() 
{
	debugLog("RegisterLibrary:" + Classname);
	var classRoot = regRoot + "\\Software\\Classes\\"+Classname+"\\";
	var clsidRoot = regRoot + "\\Software\\Classes\\CLSID\\" + dllCLSID + "\\";

	try
	{
		oShell.RegWrite(classRoot,Classname, "REG_SZ");
		oShell.RegWrite(classRoot + "CLSID\\", dllCLSID, "REG_SZ");
		oShell.RegWrite(clsidRoot, Classname, "REG_SZ");
		oShell.RegWrite(clsidRoot + "InprocServer32\\", "mscoree.dll", "REG_SZ");
		oShell.RegWrite(clsidRoot + "InprocServer32\\ThreadingModel", "Both", "REG_SZ");
		oShell.RegWrite(clsidRoot + "InprocServer32\\Class", Classname, "REG_SZ");
		oShell.RegWrite(clsidRoot + "InprocServer32\\Assembly", LibName + ", Version=1.0.2588.9125, Culture=neutral, PublicKeyToken=null", "REG_SZ");
		oShell.RegWrite(clsidRoot + "InprocServer32\\RuntimeVersion", "v4.0.30319", "REG_SZ");
		oShell.RegWrite(clsidRoot + "InprocServer32\\CodeBase", LibPath , "REG_SZ");
		oShell.RegWrite(clsidRoot + "InprocServer32\\1.0.2588.9125\\Class", Classname, "REG_SZ");
		oShell.RegWrite(clsidRoot + "InprocServer32\\1.0.2588.9125\\Assembly", LibName + ", Version=1.0.2588.9125, Culture=neutral, PublicKeyToken=null", "REG_SZ");
		oShell.RegWrite(clsidRoot + "InprocServer32\\1.0.2588.9125\\RuntimeVersion", "v4.0.30319", "REG_SZ");
		oShell.RegWrite(clsidRoot + "InprocServer32\\1.0.2588.9125\\CodeBase", LibPath , "REG_SZ");
		oShell.RegWrite(clsidRoot + "ProgId\\", Classname , "REG_SZ");
		oShell.RegWrite(clsidRoot + "ProgId\\Implemented Categories\\{62C8FE65-4EBB-45E7-B440-6E39B2CDBF29}\\", "", "REG_SZ");
	}
	catch(err) 
	{
	    debugLog("RegisterLibrary: "+err.name+" - "+err.message)
	}
}

// Unregister ActiveX component
function UnregisterLibrary() 
{
    debugLog("UnregisterLibrary:" + Classname);
	var classRoot = regRoot + "\\Software\\Classes\\"+Classname+"\\";
	var clsidRoot = regRoot + "\\Software\\Classes\\CLSID\\" + dllCLSID + "\\";

	try
	{		
		oShell.RegDelete(clsidRoot + "ProgId\\Implemented Categories\\{62C8FE65-4EBB-45E7-B440-6E39B2CDBF29}\\");
		oShell.RegDelete(clsidRoot + "ProgId\\Implemented Categories\\");
		oShell.RegDelete(clsidRoot + "ProgId\\");
		oShell.RegDelete(clsidRoot + "InprocServer32\\1.0.2588.9125\\");
		oShell.RegDelete(clsidRoot + "InprocServer32\\");
		oShell.RegDelete(clsidRoot);
		oShell.RegDelete(classRoot + "CLSID\\");
		oShell.RegDelete(classRoot);
	}
	catch(err) 
	{
	    debugLog("UnregisterLibrary: "+err.name+" - "+err.message);
	}
}

// Try to Register the Library
function ActivateLibrary(root) 
{
	debugLog("ActivateLibrary:" + root);
	regRoot = root;
	try
	{
		
		RegisterLibrary();
		debugLog("ReturnLibrary");
		return new ActiveXObject(Classname);
	}
	catch(err)
	{
		debugLog("ActivateLibrary: "+err.name+" - "+err.message);
		UnregisterLibrary();
		return null;
	}
}

function GetLibrary()
{
    debugLog("Register ActiveX");
    var Lib;
    
    // If UAC enabled registering to HKCU
    Lib = ActivateLibrary("HKCU");
    
    // If UAC disabled registering to HKLM
    if (Lib == null)
  	  Lib = ActivateLibrary("HKLM");
    
    if (Lib == null)
		debugLog("Error creating ActiveX object");
	else
		return Lib;
}