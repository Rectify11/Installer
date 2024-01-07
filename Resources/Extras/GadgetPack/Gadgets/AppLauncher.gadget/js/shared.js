// copyright 2007 Dean Laforet
// do not use or modify code without permission.

var oShell = new ActiveXObject("WScript.Shell");
var oFSO = new ActiveXObject("Scripting.FileSystemObject");

var winPath = System.Environment.getEnvironmentVariable("WINDIR");
var pfPath = System.Environment.getEnvironmentVariable("PROGRAMFILES");
if (oFSO.FolderExists(pfPath+" (x86)")) pfPath += " (x86)";
var	myComp = winPath+"\\explorer.exe";
var	control = winPath+"\\system32\\control.exe";
var	ie = pfPath+"\\Internet Explorer\\iexplore.exe";
var gadgetPath = System.Gadget.path;
var network = gadgetPath+"\\images\\network.ico";
var rbEmpty = gadgetPath+"\\images\\rbEmpty.ico";
var rbFull = gadgetPath+"\\images\\rbFull.ico";
var errorImage = gadgetPath+"\\images\\Error.png";

onerror = function(msg,url,l){
	debugLog("Javascript error: "+msg+" - on line: "+l);
}

function debugLog(outStr){
try{
		System.Debug.outputString(outStr);
		if (bDebug)
			debugLogFile.WriteLine(outStr);
} catch(err) {}
}

function $(v) { return(document.getElementById(v)); }

function read(name){
  value = System.Gadget.Settings.read(name);
  return value;
}

function write(name,value){
  System.Gadget.Settings.write(name, value);
}

function getLinks(){
try{
  linksList = new Array(); iconList = new Array(); switchList = new Array;

  settingsDir = read("settingsDir");

  filePathsFile = settingsDir+"\\filepaths.txt";
  iconPathsFile = settingsDir+"\\iconpaths.txt";
  switchPathsFile = settingsDir+"\\switchpaths.txt";
  settingsFile = settingsDir+"\\settings.txt";
  rbSettingsFile = settingsDir+"\\rbSettings.txt";

  if (!oFSO.FolderExists(settingsDir)){
    oFSO.CreateFolder(settingsDir);
		linksList[0] = "My Computer";
		linksList[1] = "Control Panel Classic";
		linksList[2] = "Internet Explorer";
		linksList[3] = "Recycle Bin";
    writeFilePathsFile();
  }
	else if (oFSO.FileExists(filePathsFile)){
    var defaultFile = oFSO.OpenTextFile(filePathsFile, 1, false);
		var index = 0;
		for (var i = 0; !defaultFile.AtEndOfStream; i++){
			testLink = defaultFile.ReadLine();

			if (testLink == "My Computer" || testLink == "Control Panel Category" || testLink == "Control Panel Classic" || testLink == "Control Panel VAIO" || testLink == "Internet Explorer" || testLink == "Recycle Bin" || testLink == "Network" || testLink == "Network VAIO" || testLink == "IE Favorites" || testLink == "Firefox Bookmarks" || testLink == "Opera Bookmarks" || testLink == "LauncherDivider"){
				isGood = true;
			}
			else{
        if(isValidPath(testLink)){
          if (oFSO.FileExists(testLink)) isGood = true;
          else if (oFSO.FolderExists(testLink)) isGood = true;
          else isGood = false;
				}
				else{
          isGood = false;
				}
			}

			if (isGood){
         linksList[index] = testLink;
				index++;
			}
		}
		defaultFile.Close();
	}

	if (oFSO.FileExists(iconPathsFile)){
    var defaultFile = oFSO.OpenTextFile(iconPathsFile, 1, false);
    for (var i = 0; !defaultFile.AtEndOfStream; i++){
      iconList[i] = defaultFile.ReadLine();
    }
    defaultFile.Close();
  }

  if (iconList.length < linksList.length){
    for (var i=iconList.length; i<=linksList.length; i++){
      iconList[i] = "none";
    }
    writeIconPathsFile();
  }

	if (oFSO.FileExists(switchPathsFile)){
    var defaultFile = oFSO.OpenTextFile(switchPathsFile, 1, false);
    for (var i = 0; !defaultFile.AtEndOfStream; i++){
      switchList[i] = defaultFile.ReadLine();
    }
    defaultFile.Close();
  }

  if (switchList.length < linksList.length){
    for (var i=switchList.length; i<=linksList.length; i++){
      switchList[i] = "none";
    }
    writeSwitchFile();
	}

  targetLink = 1;
  perRowSetting = 1;
  dragSetting = 0;
  bgSetting = 19;
  infoSetting = 1;
  folderSetting = 0;
  centreSetting = 0;
  settingsTipsSetting = 0;
  folderOpenSetting = 1;
  filterExtSetting = 1;
  LdeleteSetting = 0;
  FdeleteSetting = 0;
  textColorSetting = "#ffffff";
  cornerSetting = 1;
  tabsSetting = 0;

  write("BgSetting", bgSetting);
  write("FilterExtSetting", filterExtSetting);
  write("FDeleteSetting", FdeleteSetting);

  if (oFSO.FileExists(settingsFile)){	
    defaultFile = oFSO.OpenTextFile(settingsFile, 1, false);
    targetLink = defaultFile.ReadLine();
    if (defaultFile.AtEndOfStream) return;
    perRowSetting = defaultFile.ReadLine();
    if (defaultFile.AtEndOfStream) return;
    dragSetting = defaultFile.ReadLine();
    if (defaultFile.AtEndOfStream) return;
    bgSetting = defaultFile.ReadLine();
    write("BgSetting", bgSetting);
    if (defaultFile.AtEndOfStream) return;
    infoSetting = defaultFile.ReadLine();
    if (defaultFile.AtEndOfStream) return;
    folderSetting = defaultFile.ReadLine();
    if (defaultFile.AtEndOfStream) return;
    centreSetting = defaultFile.ReadLine();
    if (defaultFile.AtEndOfStream) return;
    settingsTipsSetting = defaultFile.ReadLine();
    if (defaultFile.AtEndOfStream) return;
    folderOpenSetting = defaultFile.ReadLine();
    if (defaultFile.AtEndOfStream) return;
    filterExtSetting = defaultFile.ReadLine();
    write("FilterExtSetting", filterExtSetting);
    if (defaultFile.AtEndOfStream) return;
    LdeleteSetting = defaultFile.ReadLine();
    if (defaultFile.AtEndOfStream) return;
    FdeleteSetting = defaultFile.ReadLine();
    write("FDeleteSetting", FdeleteSetting);
    if (defaultFile.AtEndOfStream) return;
    textColorSetting = defaultFile.ReadLine();
    write("TextColorSetting", textColorSetting);
    if (defaultFile.AtEndOfStream) return;
    cornerSetting = defaultFile.ReadLine();
    if (defaultFile.AtEndOfStream) return;
    tabsSetting = defaultFile.ReadLine();
    defaultFile.Close();
  }
}	catch(err) {debugLog("getLinks"+err.name+" - "+err.message)}
}

function isValidPath(str){
try{
               //   WIN PATH                       |        NETWORK PATH    
  var regexp = /(^[A-Za-z]:(\\|\\[^\/:\*\?\"<>\|]+)|^\\\\([^\\\/:\*\?\"<>\|]+)\\([^\/:\*\?\"<>\|]+))$/;
  return regexp.exec(str);
} catch(err) {debugLog("isValidPath: "+err.name+" - "+err.message)}
}

function writeFilePathsFile(){
try{
  if (oFSO.FileExists(filePathsFile)){
    oFSO.DeleteFile(filePathsFile, true);
  }
  var oFile = oFSO.CreateTextFile(filePathsFile, true);

  for (var i = 0; i < linksList.length ; i++){
    oFile.WriteLine(linksList[i]);
  }
  oFile.Close();
} catch(err) {debugLog("writeFilePathsFile: "+err.name+" - "+err.message);}
}

function writeIconPathsFile(){
try{
	if (oFSO.FileExists(iconPathsFile)){
		oFSO.DeleteFile(iconPathsFile, true);
	}
	var noIcons = true;
	for (var i = 0; i < linksList.length ; i++){
    if (iconList[i] != "none"){
      noIcons = false;
      break;
    }
  }
  if (noIcons == false){
    var oFile = oFSO.CreateTextFile(iconPathsFile, true);
    for (var i = 0; i < linksList.length ; i++){
      oFile.WriteLine(iconList[i]);
    }
    oFile.Close();
  }
} catch(err) {debugLog("writeIconPathsFile: "+err.name+" - "+err.message);}
}

function writeSwitchFile(){
try{
	if (oFSO.FileExists(switchPathsFile)){
		oFSO.DeleteFile(switchPathsFile, true);
	}
	var noSwitch = true;
	for (var i = 0; i < linksList.length ; i++){
    if (switchList[i] != "none"){
      noSwitch = false;
      break;
    }
  }
	if (noSwitch == false){
    var oFile = oFSO.CreateTextFile(switchPathsFile, true);
    for (var i = 0; i < linksList.length ; i++){
      oFile.WriteLine(switchList[i]);
    }
    oFile.Close();
	}
} catch(err) {debugLog("writeSwitchFile: "+err.name+" - "+err.message);}
}

