// favorites code by Matt Palermo. modified by Dean Laforet for Opera, folder view, validation and XML DOM
// copyright 2007 Dean Laforet, Matt Palermo
// do not use or modify code without permission.

var popularFileTypes = ["bat","lnk","url","exe","txt","html","htm","pdf","mp3","aac","ape","wav","flac","shn","wma","m3u","pls","asx","doc","ppt","xls","mov","avi","mpg","mpeg","mp4","ogg","jpg","jpeg","png","bmp","tif","gif","psd"];
var mediaFileTypes = ["mp3","wav","flac","wma","ogg","shn","aac","ape"];
var notepadExclude = ["lnk","url","exe","pdf","mp3","aac","ape","wav","flac","shn","wma","mov","avi","mpg","mpeg","mp4","ogg","jpg","jpeg","png","bmp","tif","gif","psd","dll","rar","cab","msi","sys","wim"];

var favType = "ff";
var oFSO = new ActiveXObject("Scripting.FileSystemObject");
var oShell = new ActiveXObject("WScript.Shell");
var ie_favDir = oShell.SpecialFolders("Favorites");
var ff_favDir = oShell.SpecialFolders("Appdata")+"\\Mozilla\\Firefox";
var opera_favDir = oShell.SpecialFolders("Appdata")+"\\Opera\\Opera\\profile";
var desktop = oShell.SpecialFolders("Desktop")+"\\";

var winPath = System.Environment.getEnvironmentVariable("windir");
var pfPath = System.Environment.getEnvironmentVariable("PROGRAMFILES");
if (oFSO.FolderExists(pfPath+" (x86)")) {pfPath = pfPath+" (x86)";}
var ffPath = pfPath + "\\Mozilla Firefox\\firefox.exe";
var iePath = pfPath + "\\Internet Explorer\\iexplore.exe";
var operaPath = pfPath + "\\Opera\\Opera.exe";
var gadgetPath = System.Gadget.path;
var validateURL = /^(ht|f)tp(s?):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?$/;

var ff_ProfileDir;
var ff_bookmarkFile;
var ffItemsArray;
var favListArea;
var dirCount = 0;
var idCount = 0;
var delConfirmOpen = "no";
var targetLink = null;
var copyLink = null;

var showOnlyPopular = 1; // 1 = yes 0 = no. overwritten from settings page.

var bDebug = oFSO.FileExists(gadgetPath+"\\debugfav.txt");
try{
	if (bDebug)
		var debugLogFile = oFSO.OpenTextFile(gadgetPath+"\\debugfav.txt", 2);
} catch(err) {bDebug = false;}

function debugLog(outStr, errName, errMessage){
try{
		System.Debug.outputString(outStr+" - "+errName+" - "+errMessage);
		if (bDebug)
			debugLogFile.WriteLine(outStr+" - "+errName+" - "+errMessage);
} catch(err) {}
}

onerror = function(msg, url, l){
   //debugLog("Javascript error: " + msg + " - on line: " + l);
   favListArea.innerHTML += "ERROR: " + msg + " - on line: " + l;
}

function $(v) { return(document.getElementById(v)); }

function read(name){
  value = System.Gadget.Settings.read(name);
  return value;
}

function write(name,value){
  System.Gadget.Settings.write(name, value);
}

// Get the favorites type
var favMeth = read("FavMeth");
if(favMeth != "none" && favMeth != "" && favMeth != null) favType = favMeth;
var folderLocation = read("folderLocation");
var showOnlyPopular = read("FilterExtSetting");
var FdeleteSetting = read("FDeleteSetting");

if (favMeth == "folder"){
  ie_favDir = folderLocation;
}

// Setup trim
String.prototype.trim = function() { return this.replace(/^\s+|\s+$/g, ''); };

window.onload = function(){
try{
	favListArea = $('favListArea');
  if (favMeth == "ff") { titleArea.innerText = "Firefox Bookmarks"; browserPath = ffPath;}
  else if (favMeth == "op") { titleArea.innerText = "Opera Bookmarks"; browserPath = operaPath;}
  else if (favMeth == "ie") { titleArea.innerText = "IE Favorites"; }
  else{
    folderName = oFSO.GetFileName(folderLocation);
    if (folderName == ""){
      folderName = "Drive: "+folderLocation;
      titleArea.innerText = folderName;
    }
    else{
      titleArea.innerText = "Folder: "+folderName;
    } 
  }

	if(favType == "ie" || favType == "folder") displayLinksAndDirs(ie_favDir, "favListArea", 0);
	else if (favType == "ff"){
		// We need to get the profile to use for FF
		var profilesFile = ff_favDir+"\\profiles.ini";
		if(oFSO.FileExists(profilesFile)){
		    // Get the directory to the FireFox profile to use
			ts = oFSO.OpenTextFile(profilesFile, 1);
			line = ts.ReadLine();
			while(line != null)	{
				if(line.substr(0, 5) == "Path="){
					chunks = line.split("=");
					profileDir = chunks[chunks.length - 1];
					ff_ProfileDir = ff_favDir+"\\"+profileDir.replace(/\//g, '\\');
					ff_bookmarkFile = ff_ProfileDir+"\\bookmarks.html";
					break;
				}
				line = ts.ReadLine();
			}
			ts.close();

			if(oFSO.FileExists(ff_bookmarkFile)){
				// Go through the bookmarks file and get all files/folders
				bookFile = oFSO.OpenTextFile(ff_bookmarkFile, 1);
				ffItemsArray = new Array();
				var ff_dirCount = -1;
				while(!bookFile.AtendOfStream){
					bookLine = bookFile.ReadLine();
					lnTmp = bookLine.trim();

					if(lnTmp.substr(0, 5) == "</DL>")  ff_dirCount--;
					else if(lnTmp.substr(0, 4) == "<DL>")  ff_dirCount++;
					if(lnTmp.substr(0, 4) == "<DT>"){
						// This is a file/folder element
						lnTmp = lnTmp.substr(4);

						itemInfo = new Array();
						if(lnTmp.substr(0, 3) == "<H3"){
							// We got a "folder" here
							tmpTitle = lnTmp.split(/\>/)[1].split(/\</)[0];

							itemInfo['type'] = "dir";
							itemInfo['title'] = tmpTitle;
							itemInfo['dir_level'] = ff_dirCount;
							ffItemsArray[ffItemsArray.length] = itemInfo;
						}
						else if(lnTmp.substr(0, 2) == "<A"){
							// We got a "link" here
							testTheUrl = lnTmp.split(/\"/)[1];
							tmpTitle = lnTmp.split(/\>/)[1].split(/\</)[0];

              if (validateURL.exec(testTheUrl)){
                itemInfo['type'] = "file";
                itemInfo['title'] = tmpTitle;
                itemInfo['dir_level'] = ff_dirCount;
                itemInfo['url'] = testTheUrl;
                ffItemsArray[ffItemsArray.length] = itemInfo;
							}
						}
						else{}
					}
					else{}
				}
				bookFile.close();

				// Iterate through items and set their parent id's
				for(var i = 0; i < ffItemsArray.length; i++){
					ffItemsArray[i]['parent_id'] = getFFitemParentId(i);
				}
				// Display initial folder levels
				displayFFbookmarks("favListArea", -1);
			}
		}
		else{
      favListArea.innerText = "Firefox Bookmarks file not found.";
		}
	}
	else if (favType == "op"){
		var operaAdr6File = opera_favDir+"\\opera6.adr";
		if(oFSO.FileExists(operaAdr6File)){
				// Go through the bookmarks file and get all files/folders
				bookFile = oFSO.OpenTextFile(operaAdr6File, 1);
				ffItemsArray = new Array();
				var opera_dirCount = 0;
				while(!bookFile.AtendOfStream){
					lnTmp = bookFile.ReadLine();
					itemInfo = new Array();

					if(lnTmp.substr(0) == "#FOLDER"){
            bookFile.SkipLine();
            lnTmp = bookFile.ReadLine();
						lnTmp = lnTmp.substr(6);

						itemInfo['type'] = "dir";
						itemInfo['title'] = lnTmp;
						itemInfo['dir_level'] = opera_dirCount;
						ffItemsArray[ffItemsArray.length] = itemInfo;
						opera_dirCount++;
					}
					else if (lnTmp.substr(0) == "#URL"){
            bookFile.SkipLine();
            lnName = bookFile.ReadLine();
            lnName = lnName.substr(6);
            lnUrl = bookFile.ReadLine();
            lnUrl = lnUrl.substr(5);

            testTheUrl = lnUrl;

            if (validateURL.exec(testTheUrl)){
              itemInfo['type'] = "file";
              itemInfo['title'] = lnName;
              itemInfo['dir_level'] = opera_dirCount;
              itemInfo['url'] = testTheUrl;
              ffItemsArray[ffItemsArray.length] = itemInfo;
            }
							
					}
					else{
            if(lnTmp.substr(0) == "-")  opera_dirCount--;
					}
				}
        bookFile.close();

        for(var i = 0; i < ffItemsArray.length; i++){
          ffItemsArray[i]['parent_id'] = getFFitemParentId(i);
        }
        displayFFbookmarks("favListArea", -1);
     }
     else{
        favListArea.innerText = "Opera bookmarks file not found.";
     }
   }
   else{}
} catch(err) {debugLog("window onload: ", err.name, err.message);}
}

// Get the parent folder for an item
function getFFitemParentId(itemId){
	// The first folder with a lower id num is the parent folder
	var parentId = -1;
	for(var z = itemId - 1; z > -1; z--){
		if(ffItemsArray[z]['dir_level'] < ffItemsArray[itemId]['dir_level']){
			parentId = z;
			break;
		}
	}
	return parentId;
}

// Hide or show the sub links for a folder in FF and Opera
function toggleFFlinkDisplay(displayElementId, parentIdNum){
try{
	var displayObj = $(displayElementId);
	if(displayObj.style.display == "none"){
		displayFFbookmarks(displayElementId, parentIdNum);
	}
	else{
		displayObj.style.display = "none";
	}
} catch(err) {debugLog("toggleFFlinkDisplay: ", err.name, err.message);}
}

// Display FF ond Opera bookmarks
function displayFFbookmarks(displayElementId, parentIdNum){
try{
	var displayObj = $(displayElementId);
	displayObj.style.display = "";
	displayObj.innerText = "";
	// Go through the line array extract info
	for(var i = 1; i < ffItemsArray.length; i++){
		currItem = ffItemsArray[i];
		if(currItem['parent_id'] != parentIdNum) continue;

		if(currItem['type'] == "dir"){ 
      var oDiv = createFolder(i, currItem['dir_level'], "", currItem['title']);
      oDiv.firstChild.className = "favIcon";
      oDiv.lastChild.onclick = new Function("toggleFFlinkDisplay('dir_"+i+"', '"+i+"')");
      displayObj.appendChild(oDiv);

      var oDiv = document.createElement("div");
      oDiv.id = "dir_"+i;
      oDiv.style.display = "none";
      displayObj.appendChild(oDiv);
    }
	}
  for(var i = 1; i < ffItemsArray.length; i++){ // second pass add links that are not in folders. This is an Opera workaround.
    currItem = ffItemsArray[i];
    if(currItem['parent_id'] != parentIdNum) continue;

    if (currItem['type'] == "file"){
      var oDiv = createFile(i, currItem['dir_level'], "", currItem['title']);
      if (favMeth == "ff") oDiv.firstChild.src = "/images/urlFF.png";
      else oDiv.firstChild.src = "/images/urlOP.png";
      oDiv.firstChild.className = "favIcon";
      oDiv.lastChild.onclick = new Function("System.Shell.execute(browserPath, '"+currItem['url']+"')");
      displayObj.appendChild(oDiv);

      ffItemsArray[i]['id'] = i;
    }
  }
} catch(err) {debugLog("displayFFBookmarks: ", err.name, err.message);}
}

// Hide or show the sub links for a folder
function toggleLinkDisplay(dp, displayElement, level){
try{
	displayObj = $(displayElement);
	if(displayObj.style.display == "none"){
		displayLinksAndDirs(dp, displayElement, level);
	}
	else{
		displayObj.style.display = "none";
	}
} catch(err) {debugLog("toggleLinkDisplay: ", err.name, err.message);}
}

// Display links and folders inside a given folder
function displayLinksAndDirs(dp, displayElement, level){
try{
	displayObj = $(displayElement);
	displayObj.style.display = "";
	displayObj.innerText = "";

	if(level == 0 && favMeth == "folder"){
		oDiv = createFolder(idCount, level, "", "This folder ("+folderName+")");
    oDiv.className = "folderHeader";
    oDiv.firstChild.className = "folderIcon";
    oDiv.firstChild.setAttribute("link",folderLocation);
    oDiv.firstChild.onclick = function(){showMenu('parentFolder')};
    oDiv.lastChild.style.cursor = "default";
    displayObj.appendChild(oDiv);

    idCount++;
  }

	// Get folders
	favDirs = getSubDirs(dp);
	for(favItem in favDirs)	{ 
		dirCount++;
		dname = favDirs[favItem];

		d = oFSO.GetFolder(dname);
		if(d.attributes == 18 || d.attributes == 22 || d.attributes == 1046) continue;
		dirPath = d.Path; 

		arFileName = dirPath.split("\\");
		label = arFileName[arFileName.length - 1];
		if (favMeth == "ie") label = label.split(".")[0];

		dirPath = dirPath.replace(/\\/g, '\\\\');
		dirPath = dirPath.replace(/\'/g, '\\\'');
		levelUp = parseInt(level) + 1;

		var oDiv = createFolder(idCount, level, dirPath, label);
		if (favMeth == "ie"){
      oDiv.firstChild.className = "favIcon"
    }
    else{
      oDiv.firstChild.className = "folderIcon"
      oDiv.firstChild.onclick = function(){showMenu('folder')};
    }
    oDiv.lastChild.onclick = new Function("toggleLinkDisplay(\""+dirPath+"\", \"dir_"+dirCount+"\", \""+levelUp+"\")");
    displayObj.appendChild(oDiv);

    var oDiv = document.createElement("div");
    oDiv.id = "dir_"+dirCount;
    oDiv.style.display = "none";
    displayObj.appendChild(oDiv);

    idCount++;
	}

	// Get files
	favFiles = getSubFiles(dp);
	for(favItem in favFiles){ 
		fname = favFiles[favItem];

		f = oFSO.GetFile(fname);
		if (f.attributes == 37 || f.attributes == 38 || f.attributes == 39 || f.attributes == 4 || f.attributes == 5 || f.attributes == 6 || f.attributes == 7) continue;

    fext = oFSO.GetExtensionName(fname).toLowerCase();

    if (favMeth == "ie"){
    try{
      oLink = oShell.CreateShortcut(fname);
      tmpPath = oLink.TargetPath;
      if (!validateURL.exec(tmpPath)) continue;
    }catch(err){continue;}
    }
    else if (showOnlyPopular == 1){
      if (!checkExtension(fext)) continue;
    }
    else{}

		filePath = f.Path; 
		arFileName = filePath.split("\\");
		label = arFileName[arFileName.length - 1];
		if (favMeth == "ie") label = label.split(".")[0];

		filePath = filePath.replace(/\\/g, '\\\\');
		filePath = filePath.replace(/\'/g, '\\\'');

		if (favMeth == "ie" && fext == "url"){
      var oDiv = createFile(idCount, level, filePath, label);
      oDiv.firstChild.src = "/images/urlIE.png";
      oDiv.firstChild.className = "favIcon";
      oDiv.lastChild.onclick = new Function("System.Shell.execute(iePath, \""+filePath+"\")");
      displayObj.appendChild(oDiv);
    }
    else if (favMeth == "folder"){
      var oDiv = createFile(idCount, level, filePath, label);
      oDiv.firstChild.src = "gimage:///"+f.Path+"?width=16&height=16";
      oDiv.firstChild.className = "folderIcon";
      oDiv.firstChild.onclick = function(){showMenu('file')};
      oDiv.lastChild.onclick = function(){executeFile()};
      displayObj.appendChild(oDiv);
    }
    else{}
    idCount++;
	}
	if (favFiles == "" && favDirs == ""){
	  var oEmpty = document.createElement("div");
	  oEmpty.id = "empty"+idCount;
	  oEmpty.style.cssText = "color:teal;margin-left:"+(level*20)+"px;height:18px;";
	  oEmpty.innerText = "empty folder";
	  displayObj.appendChild(oEmpty);
	}

	if (favMeth == "folder"){
    openArea.style.display = "";
		openArea.innerHTML = "<span onclick=\"System.Shell.execute(folderLocation);\" style=\"cursor:pointer;width:100%;text-align:center;color:blue;\">Click here to open the folder.</span>"; 
  }
} catch(err) {debugLog("displayLinksandDirs: ", err.name, err.message);}
}

function createFolder(idCount, level, dirPath, label){
try{
  var oDiv = document.createElement("div");
  oDiv.id = "folder"+idCount;
  oDiv.style.cssText = "margin-left:"+(level*20)+"px;";
  var oImg = document.createElement("img");
  oImg.src = "/images/folder.png";
  oImg.setAttribute("align","absmiddle");
  oImg.setAttribute("link",dirPath);
  oDiv.appendChild(oImg);
  var oSpan = document.createElement("span");
  oSpan.className = "folderLink";
  oSpan.innerText = label;
  oSpan.id = "span"+idCount;
  oDiv.appendChild(oSpan);

  return oDiv;
} catch(err) {debugLog("createFolder: ", err.name, err.message);}
}

function createFile(idCount, level, filePath, label){
try{
  var oDiv = document.createElement("div");
  oDiv.id = "item"+idCount;
  oDiv.style.cssText = "margin-left:"+(level*20)+"px;";
  var oImg = document.createElement("img");
  oImg.src = "";
  oImg.setAttribute("link",filePath);
  oImg.setAttribute("align","absmiddle");
  oDiv.appendChild(oImg);
  var oSpan = document.createElement("span");
  oSpan.setAttribute("link",filePath);
  oSpan.innerText = label;
  oSpan.className = "folderLink";
  oDiv.appendChild(oSpan);

  return oDiv;
} catch(err) {debugLog("createFile: ", err.name, err.message);}
}

function getSubDirs(s){ 
	var e, f, i, r = [];
	if(oFSO.FolderExists(s)){
		f = oFSO.GetFolder(s);
		e = new Enumerator(f.SubFolders);
		for(; !e.atEnd(); e.moveNext()){
			if((i = e.item())) r.push(i); 
		}
	}
	return r;
}

function getSubFiles(s){ 
	var e, f, i, r = [];
	if(oFSO.FolderExists(s)){
		f = oFSO.GetFolder(s);
		e = new Enumerator(f.files);
		for(; !e.atEnd(); e.moveNext()){
			if((i = e.item())) r.push(i); 
		}
	}
	return r;
}

function checkExtension(fext){
  var foundEXT = false;
  for (var i=0;i<popularFileTypes.length;i++){
    if (fext == popularFileTypes[i]){
      foundEXT = true;
      break;
    }
  }
  return foundEXT;
}

function checkMediaExtension(fext){
  var foundEXT = false;
  for (var i=0;i<mediaFileTypes.length;i++){
    if (fext == mediaFileTypes[i]){
      foundEXT = true;
      break;
    }
  }
  return foundEXT;
}

function checkNotepadExtensions(fext){
  var foundEXT = false;
  for (var i=0;i<notepadExclude.length;i++){
    if (fext == notepadExclude[i]){
      foundEXT = true;
      break;
    }
  }
  return foundEXT;
}

// context menu
function showMenu(type){
try{
  if (delConfirmOpen == "no"){  // I don't want the menu accessible if rename or confirm delete is open
    targetFile = event.srcElement;
    targetLink = event.srcElement.link.replace(/\\\'/g, '\'');  // handle names with apostrophes
    targetLinkId = $(event.srcElement.parentNode.id);
    theLayout = "";
    menuDivider = "<img src=\"/images/divLong.png\" class=\"menuDiv\" />";
    menuItem = "<div class=\"menuitems\" onclick=\"";
    fext = oFSO.GetExtensionName(targetLink);

    if (type == "file"){
      if (!checkNotepadExtensions(fext)){
        theLayout += menuItem+"openWithNotepad();\">Open with notepad</div>";
        theLayout += menuDivider;
      }
      if (checkMediaExtension(fext)){
        theLayout += menuItem+"createPlaylist();\">Create a playlist</div>";
        theLayout += menuDivider;
      }
      theLayout += menuItem+"deleteIt('file');\">Delete this file</div>";
      theLayout += menuDivider;
      theLayout += menuItem+"moveToDesktop('file','move');\">Move to desktop</div>";
      theLayout += menuDivider;
      theLayout += menuItem+"moveToDesktop('file','copy');\">Copy to desktop</div>";
      theLayout += menuDivider;
      theLayout += menuItem+"moveThis('move');\">Move this file...</div>";
      theLayout += menuDivider;
      theLayout += menuItem+"moveThis('copy');\">Copy this file...</div>";
      theLayout += menuDivider;
      theLayout += menuItem+"renameIt();\">Rename this file</div>";
      theLayout += menuDivider;
      theLayout += menuItem+"hideMenu();\">Close Menu</div>";
    }
    else if (type == "folder"){
      theLayout += menuItem+"openFolder();\">Open this folder</div>";
      theLayout += menuDivider;
      theLayout += menuItem+"deleteIt('folder');\">Delete this folder</div>";
      theLayout += menuDivider;
      theLayout += menuItem+"moveToDesktop('folder','move');\">Move to desktop</div>";
      theLayout += menuDivider;
      theLayout += menuItem+"moveToDesktop('folder','copy');\">Copy to desktop</div>";
      theLayout += menuDivider;
      theLayout += menuItem+"moveThis('move');\">Move this folder...</div>";
      theLayout += menuDivider;
      theLayout += menuItem+"toThisFolder();\">...to this folder</div>";
      theLayout += menuDivider;
      theLayout += menuItem+"hideMenu();\">Close Menu</div>";
    }
    else if (type == "parentFolder"){
      theLayout += menuItem+"openFolder();\">Open this folder</div>";
      theLayout += menuDivider;
      theLayout += menuItem+"toThisFolder();\">...to this folder</div>";
      theLayout += menuDivider;
      theLayout += menuItem+"hideMenu();\">Close Menu</div>";
    }
    else {}

    contxtMenu.innerHTML = theLayout;

    // make sure it's not off screen
    var rightedge = document.body.clientWidth-event.clientX;
    var bottomedge = document.body.clientHeight-event.clientY;
    if (rightedge < contxtMenu.offsetWidth) contxtMenu.style.left = document.body.scrollLeft + event.clientX - contxtMenu.offsetWidth;
    else contxtMenu.style.left = document.body.scrollLeft + event.clientX;
    if (bottomedge < contxtMenu.offsetHeight) contxtMenu.style.top = document.body.scrollTop + event.clientY - contxtMenu.offsetHeight;
    else contxtMenu.style.top = document.body.scrollTop + event.clientY;
    contxtMenu.style.visibility = "visible";
  }
} catch(err) {debugLog("showMenu: ", err.name, err.message)}
}

function hideMenu(){
  contxtMenu.style.visibility = "hidden";
}

function highLight(){
  if (event.srcElement.className == "menuitems"){
    event.srcElement.style.backgroundColor = "highlight";
    event.srcElement.style.color = "white";
  }
}

function lowLight(){
  if (event.srcElement.className == "menuitems"){
    event.srcElement.style.backgroundColor = "";
    event.srcElement.style.color = "black";
  }
}

function moveToDesktop(type,method){
try{
  hideMenu();
  if (method == "copy"){
    if (type == "file") oFSO.CopyFile(targetLink, desktop, false);
    else if (type == "folder") oFSO.CopyFolder(targetLink, desktop, false);
    else {}
  }
  else{
    if (type == "file"){
      oFSO.MoveFile(targetLink, desktop);
      thisFile = targetFile;
      addEmptyDialog(1);
      thisFile.parentNode.parentNode.removeChild(thisFile.parentNode);
    }
    else if (type == "folder"){
      oFSO.MoveFolder(targetLink, desktop);
      thisFile = targetFile;
      addEmptyDialog(2);
      thisFile.parentNode.parentNode.removeChild(thisFile.parentNode.nextSibling); // remove the html for the moved folders sub files
      thisFile.parentNode.parentNode.removeChild(thisFile.parentNode); // remove the html for the moved folder
    }
    else {}
  }
  targetLink = null;
  copyLink = null;
} catch(err) {displayError("moveToDesktop: ", err.name, err.message); targetLink = null; copyLink = null;}
}

function openWithNotepad(){
try{
  System.Shell.execute("notepad.exe", "\""+targetLink+"\"", null, "open");
  targetLink = null;
  copyLink = null;
} catch(err) {displayError("openWithNotepad: ", err.name, err.message); targetLink = null; copyLink = null;}
}

function deleteIt(type){
  hideMenu();
  delConfirmOpen = "yes";
  delConfirm.style.display = "block";
  theOutput = "<h5 style=\"margin-bottom:5px;\">Are you sure?</h5>";
  theOutput += "<button class=\"btn\" onClick=\"delConfirmed('"+type+"');\" onFocus=\"blur();\" style=\"margin-right:10px;\">Delete</button>";
  theOutput += "<button class=\"btn\" onClick=\"cancelDel();\" onFocus=\"blur();\">Cancel</button>";
  delConfirm.innerHTML = theOutput;
}

function delConfirmed(type){
try{
  delConfirm.style.display = "none";

  if (type == "file"){
    if (FdeleteSetting == 0) System.Shell.RecycleBin.deleteItem(targetLink);
    else oFSO.DeleteFile(targetLink);

    thisFile = targetFile;
    addEmptyDialog(1);
    thisFile.parentNode.parentNode.removeChild(thisFile.parentNode);
  }
  else if (type == "folder"){
    if (FdeleteSetting == 0) System.Shell.RecycleBin.deleteItem(targetLink);
    else oFSO.DeleteFolder(targetLink);

    thisFile = targetFile;
    addEmptyDialog(2);
    thisFile.parentNode.parentNode.removeChild(thisFile.parentNode.nextSibling); // remove the html for the deleted folders sub files
    thisFile.parentNode.parentNode.removeChild(thisFile.parentNode); // remove the html for the deleted folder
  }
  else{}
  delConfirmOpen = "no";
  targetLink = null;
  copyLink = null;
} catch(err) {displayError("delConfirmed: ", err.name, err.message); targetLink = null; copyLink = null; delConfirmOpen = "no";}
}

function cancelDel(){
  delConfirm.style.display = "none";
  renameFile.style.display = "none";
  delConfirmOpen = "no";
}

function openFolder(){
try{
  System.Shell.execute(targetLink);
  targetLink = null;
  copyLink = null;
} catch(err) {displayError("openFolder: ", err.name, err.message); targetLink = null; copyLink = null;}
}

function moveThis(method){
try{
  copyLink = targetLink;
  copyTheFile = targetFile;
  toFolderMethod = method;
  targetLinkIdOld = targetLinkId;
  hideMenu();
} catch(err) {debugLog("moveThis: ", err.name, err.message);}
}

function toThisFolder(){
try{
  hideMenu();
  if (copyLink != null){
    if (oFSO.FileExists(copyLink)){
      if (toFolderMethod == "move"){
        oFSO.MoveFile(copyLink, targetLink+"\\");
      }
      else if (toFolderMethod == "copy"){
        oFSO.CopyFile(copyLink, targetLink+"\\", false);
      }
      else{}

      moveFileHtml(); // call function to move the Html. moved to separate function for better error management

      if (toFolderMethod == "move"){
        thisFile = copyTheFile;
        addEmptyDialog(1);
        thisFile.parentNode.parentNode.removeChild(thisFile.parentNode);
      }
    }
    else if (oFSO.FolderExists(copyLink)){
      oFSO.MoveFolder(copyLink, targetLink+"\\");
      displayLinksAndDirs(ie_favDir, "favListArea", 0);  // moving the folder html is too complex so just refresh the flyout :-(
    }
    else {}
    targetLink = null;
    copyLink = null;
  }
} catch(err) {displayError("toThisFolder: ", err.name, err.message); targetLink = null; copyLink = null;}
}

function renameThis(){
try{
  hideMenu();
  renameFile.style.display = "none";
  tempName = nameInput.value.replace(/\/|:|\*|\?|\"|<|>|\|/g, '');  // remove invalid characters to prevent error
  if (tempName != ""){
    var newName = targetFolder+"\\"+tempName+"."+targetExtension;
    oFSO.MoveFile(targetLink, newName);
    newName = newName.replace(/'/g, '\'');  // handle names with apostrophes
    targetFile.link = newName;
    targetFile.nextSibling.link = newName;
    targetFile.nextSibling.innerText = tempName+"."+targetExtension;
  }
  delConfirmOpen = "no";
  targetLink = null; targetName = null; targetExtension = null;
} catch(err) {displayError("renameThis: ", err.name, err.message); targetLink = null; targetName = null; targetExtension = null; delConfirmOpen = "no";}
}

function renameIt(){
try{
  hideMenu();
  delConfirmOpen = "yes";
  renameFile.style.display = "block";
  targetFolder = oFSO.GetParentFolderName(targetLink); 
  targetName = oFSO.GetBaseName(targetLink);
  targetExtension = oFSO.GetExtensionName(targetLink);
  nameInput.focus();
  nameInput.value = targetName;
} catch(err) {debugLog("renameIt: ", err.name, err.message);}
}

function executeFile(){
try{
  var execLink = event.srcElement.link.replace(/\\\'/g, '\'');
  System.Shell.execute(execLink);
} catch(err) {displayError("executeFile: ", err.name, err.message);}
}

function displayError(funct, errName, errMessage){
try{
  errDisplay.style.display = "block";
  hideMenu();
  errorHere.innerText = errMessage;
  debugLog(funct, errName, errMessage);
} catch(err) {debugLog("displayError: ", err.name, err.message);}
}

function createPlaylist(){
try{
  hideMenu();
  targetFolder = oFSO.GetParentFolderName(targetLink); 
  targetFolderName = oFSO.GetBaseName(targetFolder); 
  targetParent = targetFile.parentNode.parentNode;
  var m3uFileExists = "no";

	itemPlaylist = new Array();
	for (var i = 0; i < targetParent.childNodes.length ; i++){
    if (targetParent.childNodes[i].id.substr(0,6) == "folder"){
      i++;
      continue;
    }
    fext = targetParent.childNodes[i].lastChild.innerText.split(".");
    fext2 = fext[fext.length-1].toLowerCase();
    if(checkMediaExtension(fext2)){
      itemPlaylist[itemPlaylist.length] = targetParent.childNodes[i].lastChild.innerText;
    }
	}

	itemPlaylist.sort(sortList); // sort the playlist if not numbered properly

	var sFilename = targetFolder + "\\"+targetFolderName+".m3u";
	if (oFSO.FileExists(sFilename)){
		oFSO.DeleteFile(sFilename, true);
		m3uFileExists = "yes";
	}

	var oFile = oFSO.CreateTextFile(sFilename, true);
	for (var z = 0; z < itemPlaylist.length; z++){
    oFile.WriteLine(itemPlaylist[z]);
	}
	oFile.Close();

	if (m3uFileExists == "yes") return; // if the file exist and we are just replacing it. don't add new html

	// create the html to add it to the flyout list
  var oDiv = document.createElement("div");
  oDiv.id = "play"+idCount; idCount++;
  oDiv.style.marginLeft = targetParent.firstChild.style.marginLeft;
  var oImg = document.createElement("img");
  oImg.src = "/images/m3u.png";
  oImg.setAttribute("link",sFilename);
  oImg.className = "folderIcon";
  oImg.setAttribute("align","absmiddle");
  oImg.onclick = function(){showMenu('file')};
  oDiv.appendChild(oImg);
  var oSpan = document.createElement("span");
  oSpan.className = "folderLink";
  oSpan.setAttribute("link",sFilename);
  oSpan.onclick = function(){executeFile()};
  oSpan.innerText = targetFolderName+".m3u";
  oDiv.appendChild(oSpan);
  targetParent.appendChild(oDiv);

} catch(err) {displayError("createPlaylist: ", err.name, err.message);}
}

// sort the playlist for items that are numbered poorly
function sortList(a, b){
try{
  var ValidChars = "0123456789.";
  var Char; var num = 0;
  for (var i = 0; i < a.length; i++){
    Char = a.charAt(i); 
    if (ValidChars.indexOf(Char) == -1){
      break;
    }
    num++;
  }
  var Char; var num2 = 0;
  for (var i = 0; i < b.length; i++){
    Char = b.charAt(i); 
    if (ValidChars.indexOf(Char) == -1){
      break;
    }
    num2++;
  }
  var xa = a.substr(0, num)*1;
  var xb = b.substr(0, num2)*1;
  if (xa < xb) return -1;
  else if (xa > xb) return 1;
  else return 0;
} catch(err) {debugLog("sortList: ", err.name, err.message); return 0;}
}

function addEmptyDialog(num){
try{
    // num will be 1 for a file and 2 for a folder. 2 because of the sub div with the folder contents
  if (thisFile.parentNode.parentNode.childNodes.length == num){
    var oEmpty = document.createElement("div");
    oEmpty.id = "empty"+idCount; idCount++;
    oEmpty.style.cssText = "color:teal;height:18px;";
    oEmpty.style.marginLeft = thisFile.parentNode.style.marginLeft;
    oEmpty.innerText = "empty folder";
    thisFile.parentNode.parentNode.appendChild(oEmpty);
  }
} catch(err) {debugLog("addEmptyDialog: ", err.name, err.message);}
}

function moveFileHtml(){
try{
  // this code moves the html in the flyout so the folders can remain open :-)
  var newChild = copyTheFile.parentNode.cloneNode(true);
//  newChild.id = "newID"+idCount; idCount++;
  newChild.firstChild.link = targetLink+"\\"+oFSO.GetFileName(copyLink);
  newChild.firstChild.onclick = function(){showMenu('file')};
  newChild.lastChild.link = targetLink+"\\"+oFSO.GetFileName(copyLink);
  newChild.lastChild.onclick = function(){executeFile()};
  if (targetLink != folderLocation){
    var copyHere = targetFile.parentNode.nextSibling;
    newChild.style.marginLeft = copyHere.firstChild.style.marginLeft;
    if (copyHere.firstChild.innerText == "empty folder") copyHere.removeChild(copyHere.firstChild); // remove the empty folder text then moving to an empty folder
    copyHere.appendChild(newChild);
  }
  else{
    favListArea.appendChild(newChild);
    newChild.style.marginLeft = 0;
  }
} catch(err) {debugLog("moveFileHtml: ", err.name, err.message);}
}

