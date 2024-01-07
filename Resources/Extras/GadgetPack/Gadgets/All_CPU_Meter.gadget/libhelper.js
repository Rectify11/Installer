var dllCLSID  = "{083f5ae0-2b0a-11dd-bd0b-0800200c9a66}";
var Classname = "CoreTempReader.Reader";
var LibPath   = "file:///" + System.Gadget.path.replace(new RegExp("\\\\", "g"), "/") + "/CoreTempReader.dll";
var LibName   = "CoreTempReader";
var oShell=new ActiveXObject("WScript.Shell");
var regRoot;

function RegisterLibrary(){
 var classRoot = regRoot + "\\Software\\Classes\\" + Classname + "\\";
 var clsidRoot = regRoot + "\\Software\\Classes\\CLSID\\" + dllCLSID + "\\";
 try{
  oShell.RegWrite(classRoot,Classname,"REG_SZ");
  oShell.RegWrite(classRoot + "CLSID\\",dllCLSID,"REG_SZ");
  oShell.RegWrite(clsidRoot,Classname,"REG_SZ");
  oShell.RegWrite(clsidRoot + "InprocServer32\\","mscoree.dll","REG_SZ");
  oShell.RegWrite(clsidRoot + "InprocServer32\\ThreadingModel","Both","REG_SZ");
  oShell.RegWrite(clsidRoot + "InprocServer32\\Class",Classname,"REG_SZ");
  oShell.RegWrite(clsidRoot + "InprocServer32\\Assembly",LibName+", Version=1.0.2588.9125, Culture=neutral, PublicKeyToken=null","REG_SZ");
  oShell.RegWrite(clsidRoot + "InprocServer32\\RuntimeVersion","v2.0.50727","REG_SZ");
  oShell.RegWrite(clsidRoot + "InprocServer32\\CodeBase",LibPath,"REG_SZ");
  oShell.RegWrite(clsidRoot + "InprocServer32\\1.0.2588.9125\\Class",Classname,"REG_SZ");
  oShell.RegWrite(clsidRoot + "InprocServer32\\1.0.2588.9125\\Assembly",LibName+", Version=1.0.2588.9125, Culture=neutral, PublicKeyToken=null","REG_SZ");
  oShell.RegWrite(clsidRoot + "InprocServer32\\1.0.2588.9125\\RuntimeVersion","v2.0.50727","REG_SZ");
  oShell.RegWrite(clsidRoot + "InprocServer32\\1.0.2588.9125\\CodeBase",LibPath,"REG_SZ");
  oShell.RegWrite(clsidRoot + "ProgId\\",Classname,"REG_SZ");
  oShell.RegWrite(clsidRoot + "ProgId\\Implemented Categories\\{df21d370-2b10-11dd-bd0b-0800200c9a66}\\", "", "REG_SZ");
 }
 catch(err){return null;}
}

function UnregisterLibrary(){
 var classRoot = regRoot + "\\Software\\Classes\\" + Classname + "\\";
 var clsidRoot = regRoot + "\\Software\\Classes\\CLSID\\" + dllCLSID + "\\";
 try{
  oShell.RegDelete(clsidRoot+"ProgId\\Implemented Categories\\{df21d370-2b10-11dd-bd0b-0800200c9a66}\\");
  oShell.RegDelete(clsidRoot+"ProgId\\Implemented Categories\\");
  oShell.RegDelete(clsidRoot+"ProgId\\");
  oShell.RegDelete(clsidRoot+"InprocServer32\\1.0.2588.9125\\");
  oShell.RegDelete(clsidRoot+"InprocServer32\\");
  oShell.RegDelete(clsidRoot);
  oShell.RegDelete(classRoot+"CLSID\\");
  oShell.RegDelete(classRoot);
 }
 catch(err){}
}

function ActivateLibrary(root){
 regRoot=root;
 try{
  RegisterLibrary();
  return new ActiveXObject(Classname);
 }
 catch(err){
  UnregisterLibrary();
  return null;
 }
}

function CreateObjectFromDLL(){
 var Object;
 Object = ActivateLibrary("HKCU");
 if (Object == null) Object = ActivateLibrary("HKLM");
 return Object;
}