var dllCLSID="{5b55a44a-d008-49aa-9234-86fb7709bc0a}";
var Classname="GPUStatusReader.GPUMonitor";
var LibPath="file:///"+System.Gadget.path.replace(new RegExp("\\\\","g"),"/")+"/GPUStatusReader.dll";
var LibName="GPUStatusReader";
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
  oShell.RegWrite(clsidRoot + "ProgId\\Implemented Categories\\{62C8FE65-4EBB-45E7-B440-6E39B2CDBF29}\\","","REG_SZ");
 }
 catch(err){}
}

function UnregisterLibrary(){
 var classRoot = regRoot + "\\Software\\Classes\\" + Classname + "\\";
 var clsidRoot = regRoot + "\\Software\\Classes\\CLSID\\" + dllCLSID + "\\";
 try{
  oShell.RegDelete(clsidRoot+"ProgId\\Implemented Categories\\{62C8FE65-4EBB-45E7-B440-6E39B2CDBF29}\\");
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