// copyright 2007 Dean Laforet
// do not use or modify code without permission.

var gadgetPath = System.Gadget.path;
var isEmpty = "";
var rbEmpty = gadgetPath+"\\images\\rbEmpty.ico";
var rbFull = gadgetPath+"\\images\\rbFull.ico";
var oFSO = new ActiveXObject("Scripting.FileSystemObject");

window.onload = function(){
  textColorSetting = System.Gadget.Settings.read("TextColorSetting");
  document.body.style.color = textColorSetting;
  bgSetting = System.Gadget.Settings.read("BgSetting");
  bgPath = "url(/images/backgrounds/bg" + bgSetting + ".jpg)";
  document.getElementById("flyoutContents").style.backgroundImage = bgPath;
  setRbIcons();
	binCount = System.Shell.RecycleBin.fileCount + System.Shell.RecycleBin.folderCount;
	if (binCount == 0){
    rbIcon = rbEmpty;
    rbText = "It's Empty";
	}
	else if (binCount == 1){
    rbIcon = rbFull;
    rbText = binCount + " item";
	}
	else{
    rbIcon = rbFull;
    rbText = binCount + " items";
	}
  document.getElementById("binIcon").src = "gimage:///" + rbIcon + "?width=40&height=40";
  document.getElementById("binContents").innerText = rbText;
}

function emptyIT(){
	isEmpty = (System.Shell.RecycleBin.fileCount + System.Shell.RecycleBin.folderCount);
	if (isEmpty > 0){
		System.Shell.RecycleBin.emptyAll();
	}
}

function openRB(){
	System.Shell.execute("Explorer", "/N,::{645FF040-5081-101B-9F08-00AA002F954E}");
}

function openProp(){
	System.Shell.RecycleBin.showRecycleSettings();
}

function setRbIcons(){
  var tExists = oFSO.FileExists(gadgetPath+"\\BackMeUp\\rbSettings.txt");
	if (tExists){	
    defaultFile = oFSO.OpenTextFile(gadgetPath+"\\BackMeUp\\rbSettings.txt", 1, false);
		rbEmpty = defaultFile.ReadLine();
		rbFull = defaultFile.ReadLine();
		defaultFile.Close();
	}
}
