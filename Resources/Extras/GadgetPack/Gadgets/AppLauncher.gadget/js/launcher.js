// copyright 2007 Dean Laforet, Simon Paterson
// do not use or modify code without permission.

var makeIconBigger = "yes";
var showHighlight = "no";
var cancelBodyOver = 0;

var settingsDir = oShell.SpecialFolders("Appdata")+"\\App Launcher Gadget";
write("settingsDir", settingsDir);

var bDebug = oFSO.FileExists(gadgetPath+"\\debug.txt");
try{
	if (bDebug)
		var debugLogFile = oFSO.OpenTextFile(gadgetPath+"\\debug.txt", 2);
} catch(err) {bDebug = false;}

document.onreadystatechange = function(){
try{
  if(document.readyState=="complete"){
    System.Gadget.settingsUI = "settings.html";
    System.Gadget.onSettingsClosed = settingsClosed;
    getLinks();
    setRbIcons();
    createLayout();
    createBorder();
    System.Shell.RecycleBin.onRecycleBinChanged = updateRB;
  }        
} catch(err) {debugLog("onreadystatechange: "+err.name+" - "+err.message)}
}

function settingsClosed(event){
try{
	cancelBodyOver = 0;
  getLinks();
  setRbIcons();
	createLayout();
  createBorder();
} catch(err) {debugLog("SettingsClosed: "+err.name+" - "+err.message)}
}

function createLayout(){
try{
  System.Gadget.background = "/images/backgrounds/bg" + bgSetting + ".jpg";
  appInfo.style.color = textColorSetting;
  tabDiv.style.color = textColorSetting;

	if (infoSetting == 1 || tabsSetting == 1){
    $("appInfo").style.display="";
    bottomPadding = 18;
	}
	else{
    $("appInfo").style.display="none";
    bottomPadding = 4;
	}

	if (perRowSetting == 0){
		perRow = 3;
		imgSmall = 32;
		imgLarge = 38;
    wh = "?width=32&height=32";
    wh2 = "?width=38&height=38";
		cellSize = 40;
    tabMarginTop = "4px";
    if (linksList.length < 4 && infoSetting == 0){
      tabMarginTop = "6px";
    }
	}
	else{
		perRow = 4;
		imgSmall = 24;
		imgLarge = 28;
    wh = "?width=24&height=24";
    wh2 = "?width=28&height=28";
		cellSize = 30;
    tabMarginTop = "2px";
		if (linksList.length < 5){
      tabMarginTop = "9px";
		}
	}

	var imgCounter = 1;
	var rowCounter = 1;
  var tabCounter = 1;
  
	if (tabsSetting == 1){
    tmpLinks = new Array(); tmpIcons = new Array(); tmpSwitch = new Array();
    var index = 0;
    for (var i=0; i< linksList.length; i++){
      if (linksList[i] == "LauncherDivider") continue;  // strip dividers if using tabs
      tmpLinks[index] = linksList[i];
      tmpIcons[index] = iconList[i];
      tmpSwitch[index] = switchList[i];
      index++;
    }
    linksList = tmpLinks; iconList = tmpIcons; switchList = tmpSwitch;
    tabRows = Math.ceil(linksList.length/4/perRow);
    if (tabRows == 1 && perRow == 4) tabMarginTop = "9px";
  }
	else tabRows = 99;
	
	launcherDiv.innerHTML = "";
	oDiv = document.createElement("div");
	oDiv.id = "tab1";
	oDiv.style.paddingTop = tabMarginTop;

  var oTable = createTable();
  row = oTable.insertRow();
  row.className = "launcherRow";

	for (var i=0; i < linksList.length; i++){
		setFilePaths(i);

		if (thisPath != "LauncherDivider"){
      src = "gimage:///" + src + wh;

      cell = row.insertCell();
      cell.id = "cell"+i;
      cell.className = "cell";
      cell.style.width = cellSize;
      cell.style.height = cellSize;
      var oImg = document.createElement("img");
      oImg.style.width = imgSmall;
      oImg.style.height = imgSmall;
      oImg.setAttribute("link",thisPath);
      oImg.id = imageID;
      oImg.src = src;
      oImg.onclick = new Function(""+execType+"");
      oImg.onmouseover = function(){hiliteLink(0)};
      oImg.onmouseout = function(){unHiliteLink(0)};
      oImg.ondragenter = new Function(""+dragEnter+"");
      oImg.ondragover = new Function(""+dragOver+"");
      oImg.ondragleave = new Function(""+dragLeave+"");
      oImg.ondrop = new Function(""+dragDrop+"");
      cell.appendChild(oImg);

      if (imgCounter == perRow && i != (linksList.length-1)){
        imgCounter = 0;
        oDiv.appendChild(oTable);

        if (tabRows == rowCounter){
          tabCounter++;
          launcherDiv.appendChild(oDiv);
          oDiv = document.createElement("div");
          oDiv.id = "tab"+tabCounter;
          oDiv.style.paddingTop = tabMarginTop;
          
          rowCounter = 0;
        }

        if (linksList[(i+1)] != "LauncherDivider"){
          var oTable = createTable();
          row = oTable.insertRow();
          row.className = "launcherRow";

          rowCounter++;
        }
      }
      imgCounter++;
		}
		else{
      if (imgCounter != 1){
        if (centreSetting == 1){
          for (var j=imgCounter ; j <= perRow ; j++){
            cell = row.insertCell();
            cell.className = "emptyCell";
            cell.style.width = cellSize;
            cell.style.height = cellSize;
            cell.innerHTML = "&nbsp;";
          }
        }
        oDiv.appendChild(oTable);
      }

      var oTable = createTable();
      row = oTable.insertRow();
      row.className = "divider";
      cell = row.insertCell();
      var oImg = document.createElement("img");
      oImg.src = src;
      cell.appendChild(oImg);
      oDiv.appendChild(oTable);

      bottomPadding += 2;
      imgCounter = 1;
      
      if (linksList[i-1] == "LauncherDivider") rowCounter--;
      
      if (i != (linksList.length-1)){
        var oTable = createTable();
        row = oTable.insertRow();
        row.className = "launcherRow";

        rowCounter++;
      }
		}
  }

  if (imgCounter != 1 && centreSetting == 1){
    for (var i=imgCounter ; i <= perRow ; i++){
      cell = row.insertCell();
      cell.className = "emptyCell";
      cell.style.width = cellSize;
      cell.style.height = cellSize;
      cell.innerHTML = "&nbsp;";
    }
  }

  oDiv.appendChild(oTable);
  launcherDiv.appendChild(oDiv);
  
  for (var j = 2; j < 5; j++){
    if (!$("tab"+j)){
      oDiv = document.createElement("div");
      oDiv.id = "tab"+j;
      oDiv.style.display = "none";
      launcherDiv.appendChild(oDiv);
    }
    else{
      $("tab"+j).style.display = "none";
    }
  }

	if (tabsSetting == 0) { document.body.style.height = parseInt(rowCounter*cellSize+bottomPadding)+"px"; }
	else{ document.body.style.height = parseInt(tabRows*cellSize+bottomPadding)+"px";	}

	updateRB();
} catch(err) {debugLog("CreateLayout: "+err.name+" - "+err.message);}
}

function setFilePaths(i){
try{
	imageID = "img"+i;
	thisPath = linksList[i];
		
  thisTitle = oFSO.GetFileName(thisPath);

  execType = "execLink()";

	dragEnter = "event.returnValue = false";
	dragOver = "event.returnValue = false";
	dragLeave = "event.returnValue = false";
	dragDrop = "event.returnValue = false";

	tmpPath = thisPath;

	fileExtension = oFSO.GetExtensionName(thisPath).toLowerCase();
	if (fileExtension == "lnk"){
		oLink = oShell.CreateShortcut(thisPath);
		tmpPath = oLink.TargetPath;
		if (targetLink == 0){
			if (oFSO.GetFileName(tmpPath).toLowerCase() != "hlsw.exe"){
				thisPath = tmpPath;
				linksList[i] = thisPath;
				writeFilePathsFile();
			}
		}
		else if (targetLink == 2){ 
      tmpPath = thisPath;
      theIconLocation = oLink.iconLocation;
      if ((theIconLocation.length >= 2) && (theIconLocation.substr(theIconLocation.length - 2).toLowerCase() == ",0")){
        tmpPath = theIconLocation.substr(0, (theIconLocation.length - 2));
        if (tmpPath== ""){
           tmpPath = oLink.targetPath;
        }
      }
		}
		else{}
	}
		
	if (thisTitle=="iTunesIco.exe" || thisTitle=="iTunes.lnk"){
    iTunesPath = pfPath + "\\iTunes\\iTunes.exe";
    if (oFSO.FileExists(iTunesPath)){
      linksList[i] = iTunesPath;
      writeFilePathsFile();
		}
	}

	if (thisPath == "My Computer"){
		src = myComp;
	}
	else if (thisPath == "Network"){
		src = network;
	}
	else if (thisPath == "Network VAIO"){
		src = network;
	}
	else if (thisPath == "Control Panel Category"){
		src = control;
	}
	else if (thisPath == "Control Panel Classic"){
		src = control;
	}
	else if (thisPath == "Control Panel VAIO"){
		src = control;
	}
	else if (thisPath == "Internet Explorer"){
		src = ie;
	}
	else if (thisPath == "Recycle Bin"){
		src = rbEmpty;
		imageID = "rbHere";    
		dragOver = "hiliteLink(1);event.returnValue = false";
		dragLeave = "unHiliteLink(1);event.returnValue = false";
		dragDrop = "recycleBinDragDropped()";
		execType = "recycleBin()";
	}
	else if (thisPath == "IE Favorites"){
		src = gadgetPath + "\\images\\FavsIE.png";
		execType = "showFavs('ie')";
	}
	else if (thisPath == "Firefox Bookmarks"){
		src = gadgetPath + "\\images\\FavsFF.png";
		execType = "showFavs('ff')";
	}
	else if (thisPath == "Opera Bookmarks"){
		src = gadgetPath + "\\images\\FavsOP.png";
		execType = "showFavs('op')";
	}
	else if (thisPath == "LauncherDivider"){
		src = "/images/divLong.png";
	}
	else if (oFSO.FolderExists(thisPath)){
    src = thisPath;
		if (folderSetting != 2){
      dragOver = "hiliteLink(3);event.returnValue = false";
      dragLeave = "unHiliteLink(3);event.returnValue = false";
      dragDrop = "moveToFolder(linksList["+i+"])";
      execType = "execFolder()";
    }
	}
	else{
    filePath = thisPath;
		if (!oFSO.FileExists(filePath)){
			src = gadgetPath+"\\images\\Error.png";
			execType = "javascript:void(0)";
		}
		else{
			src = tmpPath;
			if (dragSetting != 0){
				dragOver = "hiliteLink(2);event.returnValue = false";
				dragLeave = "unHiliteLink(2);event.returnValue = false";
				dragDrop = "fileDropExec()";
			}
			if (switchList[i] != "none"){
        execType = "execWithSwitches("+i+")";
			}
		}
	}

	if (iconList[i] != "none"){
		src = iconList[i];
    if (!oFSO.FileExists(src)){
      if (!oFSO.FolderExists(src)) src = gadgetPath+"\\images\\Error.png";
    }
 	}
} catch(err) {debugLog("setFilePaths: "+err.name+" - "+err.message);}
}

function createTable(){
  var oTable = document.createElement("table");
  oTable.className = "launcherTable";
  oTable.setAttribute("cellPadding",0);
  oTable.setAttribute("cellSpacing",0);
  oTable.setAttribute("border",0);
  return oTable;
}

function execLink(passLink){
try{
  var getLink = event.srcElement;
  var thisPath = getLink.link;
  if (passLink){
    thisPath = passLink;
  }

	if (thisPath == "My Computer"){
		System.Shell.execute("::{20D04FE0-3AEA-1069-A2D8-08002B30309D}");
	}
	else if (thisPath == "Network"){
		System.Shell.execute("::{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}");
	}
	else if (thisPath == "Network VAIO"){
		System.Shell.execute("::{7007ACC7-3202-11D1-AAD2-00805FC1270E}");
	}
	else if (thisPath == "Control Panel Category"){
		System.Shell.execute("::{26EE0668-A00A-44D7-9371-BEB064C98683}");
	}
	else if (thisPath == "Control Panel Classic"){
		System.Shell.execute("::{21EC2020-3AEA-1069-A2DD-08002B30309D}");
	}
	else if (thisPath == "Control Panel VAIO"){
		System.Shell.execute("::{20D04FE0-3AEA-1069-A2D8-08002B30309D}\\::{21EC2020-3AEA-1069-A2DD-08002B30309D}");
	}
	else if (thisPath == "Internet Explorer"){
    System.Shell.execute("::{871C5380-42A0-1069-A2EA-08002B30309D}");
	}
	else{
    if (oFSO.FileExists(thisPath) || oFSO.FolderExists(thisPath)){
      System.Shell.execute(thisPath);
    }
	}
  $("appInfo").innerText="";
  getLink.blur();
  return false;
} catch(err) {debugLog("execLink: "+err.name+" - "+err.message)}
}

function moveToFolder(thisFolder){
try{
  var sFile;
  cancelBodyOver = 0;
  for (var i=0 ; System.Shell.itemFromFileDrop(event.dataTransfer, i) != null ; i++){
    sFile = System.Shell.itemFromFileDrop(event.dataTransfer, i).path;
    if (oFSO.FolderExists(sFile)){
      if (folderSetting == 0){
        oFSO.MoveFolder(sFile, thisFolder+"\\");
      }
      else{
        oFSO.CopyFolder(sFile, thisFolder+"\\", false);
      }
    }
    else{
      if (folderSetting == 0){
        oFSO.MoveFile(sFile, thisFolder+"\\");
      }
      else{
        oFSO.CopyFile(sFile, thisFolder+"\\", false);
      }
    }
  }
  $("appInfo").innerText="";
  return false;
}	catch(err)	{debugLog("moveToFolder: "+err.name+" - "+err.message);}
}

function recycleBin(){
try{
	System.Gadget.Flyout.file = "flyoutRB.html";
	System.Gadget.Flyout.show = !System.Gadget.Flyout.show;
} catch(err) {debugLog("recycleBin: "+err.name+" - "+err.message)}
}

function updateRB(){
try{
	var link = $("rbHere");
	if(link){
		binCount = System.Shell.RecycleBin.fileCount + System.Shell.RecycleBin.folderCount;
		if (binCount == 0){
			link.src = "gimage:///" + rbEmpty + wh;
		}
		else{
			link.src = "gimage:///" + rbFull + wh;
		}
	}
} catch(err) {debugLog("updateRB: "+err.name+" - "+err.message)}
}

function recycleBinDragDropped(){
try{
  var sFile;
  cancelBodyOver = 0;
  for (var i=0 ; System.Shell.itemFromFileDrop(event.dataTransfer, i) != null ; i++){
    sFile = System.Shell.itemFromFileDrop(event.dataTransfer, i).path;
    if (LdeleteSetting == 0){
      System.Shell.RecycleBin.deleteItem(sFile);
    }
    else{
      if (oFSO.FileExists(sFile)) oFSO.DeleteFile(sFile);
      else if (oFSO.FolderExists(sFile)) oFSO.DeleteFolder(sFile);
    }
  }
  $("appInfo").innerText="";
  return false;
}	catch(err)	{	debugLog("recycleBinDragDropped: "+err.name+" - "+err.message);	}
}

function setRbIcons(){
try{
	if (oFSO.FileExists(rbSettingsFile)){	
    defaultFile = oFSO.OpenTextFile(rbSettingsFile, 1, false);
		rbEmpty = defaultFile.ReadLine();
		rbFull = defaultFile.ReadLine();
		defaultFile.Close();
	}
	else{
    rbEmpty = gadgetPath+"\\images\\rbEmpty.ico";
    rbFull = gadgetPath+"\\images\\rbFull.ico";
	}
} catch(err) {debugLog("SetRbIcons: "+err.name+" - "+err.message)}
}

function fileDropAdd(){
try{
	if (dragSetting != 1 && System.Shell.itemFromFileDrop(event.dataTransfer, 0) != null){
    var sFile;
		for (var i=0; System.Shell.itemFromFileDrop(event.dataTransfer, i) != null; i++){
			sFile = System.Shell.itemFromFileDrop(event.dataTransfer, i).path;
			addOneToList(sFile);
		}

		createLayout();

    writeFilePathsFile();
    writeIconPathsFile();
    writeSwitchFile();
  }
  $("appInfo").innerText="";
  return false;
} catch(err) {debugLog("fileDropAdd: "+err.name+" - "+err.message);}
}

function fileDropExec(){
try{
  var sFile;
  cancelBodyOver = 0;
	for (var i=0 ; System.Shell.itemFromFileDrop(event.dataTransfer, i) != null ; i++){
		sFile = System.Shell.itemFromFileDrop(event.dataTransfer, i).path;

		tmpLink = event.srcElement.link;

		if (oFSO.FileExists(tmpLink)){
      System.Shell.execute(tmpLink, "\""+sFile+"\"", null, "open");
		}
	}
  $("appInfo").innerText="";
  return false;
} catch(err) {debugLog("fileDropExec: "+err.name+" - "+err.message)}
}

function execWithSwitches(index){
try{
  var thisFile = linksList[index];
  var thisSwitch = switchList[index];

	if (oFSO.FileExists(thisFile)){
    System.Shell.execute(thisFile, "\""+thisSwitch+"\"");
  }
  return false;
} catch(err) {debugLog("execWithSwitches: "+err.name+" - "+err.message)}
}

function execFolder(){
try{
  var getLink = event.srcElement.link;
  if (folderOpenSetting == 1){
    write("folderLocation", getLink);
    showFavs('folder');
  }
  else{
    execLink(getLink);
  }
  return false;
} catch(err) {debugLog("execFolder: "+err.name+" - "+err.message)}
}

function addOneToList(newOne){
try{
	var n = linksList.length;
	linksList[n] = newOne;
	iconList[n] = "none";
	switchList[n] = "none";
} catch(err) {debugLog("addOneToList: "+err.name+" - "+err.message)}
}

function showFavs(type){
try{
  write("FavMeth", type);
	System.Gadget.Flyout.file = "flyoutFav.html";
	System.Gadget.Flyout.show = !System.Gadget.Flyout.show;
} catch(err) {debugLog("showFavs: "+err.name+" - "+err.message)}
}

function hiliteLink(hovType){
try{
  var img = event.srcElement;
  thisPath = img.link;
  thisTitle = oFSO.GetBaseName(thisPath);
  tabDiv.style.display = "none";

  if (dragSetting != 0 && hovType){
    document.body.ondrop = "event.returnValue = false";
  }

  if (oFSO.DriveExists(thisPath)){
    d = oFSO.GetDrive(oFSO.GetDriveName(thisPath));
		thisTitle = thisPath+" {"+parseInt(d.FreeSpace/1024/1024/1024)+" GB free}";
	}

  if (img.link != null){
    if (showHighlight=="yes"){
      img.style.backgroundImage = "url(/images/highlight.png)";  // setting off by default
    }
    if (makeIconBigger=="yes"){
      img.style.width = imgLarge;
      img.style.height = imgLarge;
      img.src = img.src.substr(0, img.src.length-19)+wh2;
//      img.style.msInterpolationMode = "bicubic";
    }
    img.style.cursor = "hand";
        
    if (hovType == 1){
      cancelBodyOver = 1;
      displayThis = "Delete this item.";
    }
    else if (hovType == 2){
      cancelBodyOver = 1;
      displayThis = "Open with " + thisTitle + ".";
    }
    else if (hovType == 3){
      cancelBodyOver = 1;
      if (folderSetting == 0){
        displayThis = "Move to " + thisTitle + ".";
      }
      else{
        displayThis = "Copy to " + thisTitle + ".";
      }
    }
    else{
      displayThis = thisTitle;
      if(thisPath == "Recycle Bin"){
        rbCount = System.Shell.RecycleBin.fileCount + " files, " + System.Shell.RecycleBin.folderCount + " folders";
        displayThis = rbCount;
      }
    }
    if (infoSetting == 1){
      $("appInfo").innerText=displayThis;
    }
  }
  else{
    event.returnValue = false;
  }
} catch(err) {debugLog("HiliteLinks: "+err.name+" - "+err.message)}
}

function unHiliteLink(hovType){
try{
  var img = event.srcElement;

  document.body.ondrop = fileDropAdd;
  tabDiv.style.display = "none";
  
  if (img.link != null){
    img.style.backgroundImage = "";
    if (makeIconBigger=="yes"){
      img.style.width = imgSmall;
      img.style.height = imgSmall;
      img.src = img.src.substr(0, img.src.length-19)+wh;
//      img.style.msInterpolationMode = "bicubic";
    }
    img.style.cursor = "default";
    $("appInfo").innerText = "";
    cancelBodyOver = 0;
  }
  else{
    event.returnValue = false;
  }
} catch(err) {debugLog("UnhiliteLinks: "+err.name+" - "+err.message)}
}

function addTipOver(){
try{
  if (dragSetting != 1){
    if ( cancelBodyOver == 0){
      $("appInfo").innerText="Add to launcher.";
    }
  }
  event.returnValue = false;
} catch(err) {debugLog("addTipOver: "+err.name+" - "+err.message)}
}

function addTipOut(){
try{
  $("appInfo").innerText="";
  event.returnValue = false;
} catch(err) {debugLog("addTipOut: "+err.name+" - "+err.message)}
}

function createBorder(){
  border.innerHTML = "<img src=\"images/borderTop.png\" style=\"position:absolute;top:0;left:0;\" />";
  border.innerHTML += "<img src=\"images/borderBtm.png\" style=\"position:absolute;bottom:0;left:0;\" />";
  border.innerHTML += "<img src=\"images/borderL.png\" style=\"position:absolute;top:0;left:0;\" />";
  border.innerHTML += "<img src=\"images/borderR.png\" style=\"position:absolute;top:0;right:0;\" />";
  if (cornerSetting == 1){
    border.innerHTML += "<img src=\"images/borderTL.png\" style=\"position:absolute;top:0;left:0;\" />";
    border.innerHTML += "<img src=\"images/borderTR.png\" style=\"position:absolute;top:0;right:0;\" />";
    border.innerHTML += "<img src=\"images/borderBL.png\" style=\"position:absolute;bottom:0;left:0;\" />";
    border.innerHTML += "<img src=\"images/borderBR.png\" style=\"position:absolute;bottom:0;right:0;\" />";
  }
}

function showTabs(){
  $("appInfo").innerText="";
  if (tabsSetting == 1) tabDiv.style.display = "";
  event.returnValue = false
}

