// copyright 2007 Dean Laforet, Simon Paterson
// do not use or modify code without permission.

var specialLinksArray = ["My Computer","Network","Control Panel Category","Control Panel Classic","Internet Explorer","Recycle Bin","IE Favorites","Firefox Bookmarks","Opera Bookmarks"];

var wh = "?width=18&height=18";

var bDebug = oFSO.FileExists(gadgetPath+"\\debugset.txt");
try{
	if (bDebug)
		var debugLogFile = oFSO.OpenTextFile(gadgetPath+"\\debugset.txt", 2);
} catch(err) {bDebug = false;}

document.onreadystatechange = function(){    
  if(document.readyState=="complete"){
	try{
    loadSettings();
	} catch(err) {debugLog("onLoad: "+err.name+" - "+err.message)}
	}
}

function loadSettings(){
try{
  getLinks();

	if (linksList.length == 0){
		DisplayNoLinks(true);
	}
	else{
    checkDefaults();
		bldShortCuts();
		DisplayNoLinks(false);
	}
	setRbIcons();
  $("rbEmptyIcon").src = "gimage:///" + rbEmpty + "?width=32&height=32";
  $("rbFullIcon").src = "gimage:///" + rbFull + "?width=32&height=32";
  countBackgrounds();
	setBgColor();
  createColorChooser();

  if (settingsTipsSetting == 1){
    removeToolTips();
  }

  linkSelect.options.selectedIndex = targetLink;
  perRowSelect.options.selectedIndex = perRowSetting;
  dragSelect.options.selectedIndex = dragSetting;
  infoSelect.options.selectedIndex = infoSetting;
  folderSelect.options.selectedIndex = folderSetting;
  centreSelect.options.selectedIndex = centreSetting;
  settingsTipsSelect.options.selectedIndex = settingsTipsSetting;
  folderOpenSelect.options.selectedIndex = folderOpenSetting;
  filterExtSelect.options.selectedIndex = filterExtSetting;
  LdeleteSelect.options.selectedIndex = LdeleteSetting;
  FdeleteSelect.options.selectedIndex = FdeleteSetting;
  cornerSelect.options.selectedIndex = cornerSetting;
  tabsSelect.options.selectedIndex = tabsSetting;
} catch(err) {debugLog("LoadSettings: "+err.name+" - "+err.message)}
}

System.Gadget.onSettingsClosing = function(event){
	if (event.closeAction == event.Action.commit){
    saveShortcuts();
    write("BgSetting", bgSetting);
		write("FilterExtSetting", filterExtSelect.options.selectedIndex);
		write("FDeleteSetting", FdeleteSelect.options.selectedIndex);
    write("TextColorSetting", textColorSetting);
	}
	event.cancel = false;
}

function saveShortcuts(){
try{
  writeFilePathsFile();
	writeIconPathsFile();
  writeSwitchFile();

	if (oFSO.FileExists(settingsFile)){
		oFSO.DeleteFile(settingsFile, true);
	}
	var oFile = oFSO.CreateTextFile(settingsFile, true);
	oFile.WriteLine(linkSelect.options.selectedIndex);
	oFile.WriteLine(perRowSelect.options.selectedIndex);
	oFile.WriteLine(dragSelect.options.selectedIndex);
	oFile.WriteLine(bgSetting);
	oFile.WriteLine(infoSelect.options.selectedIndex);
	oFile.WriteLine(folderSelect.options.selectedIndex);
	oFile.WriteLine(centreSelect.options.selectedIndex);
	oFile.WriteLine(settingsTipsSelect.options.selectedIndex);
	oFile.WriteLine(folderOpenSelect.options.selectedIndex);
	oFile.WriteLine(filterExtSelect.options.selectedIndex);
	oFile.WriteLine(LdeleteSelect.options.selectedIndex);
	oFile.WriteLine(FdeleteSelect.options.selectedIndex);
	oFile.WriteLine(textColorSetting);
	oFile.WriteLine(cornerSelect.options.selectedIndex);
	oFile.WriteLine(tabsSelect.options.selectedIndex);
	oFile.Close();
} catch(err) {debugLog("saveShortcuts: "+err.name+" - "+err.message)}
}

function getFocus() { self.focus(); }

function DisplayNoLinks(tf){
try{
	if (tf){
		iViewList.style.display = "none";
		idNoShortcutList.style.display = "";
		$("L_REMOVE").disabled=true;
		$("L_CHANGE").disabled=true;
		$("L_RESET").disabled=true;
		$("L_ARG").disabled=true;
		setTimeout("getFocus()", 300);
	}
	else{
		iViewList.style.display  = "";
		idNoShortcutList.style.display = "none";
		$("L_REMOVE").disabled=false;       
		$("L_CHANGE").disabled=false;       
		$("L_RESET").disabled=false;       
		$("L_ARG").disabled=false;       
		setTimeout("getFocus()", 300);
	}
} catch(err) {debugLog("DisplayNoLinks: "+err.name+" - "+err.message)}
}

function bldShortCuts(){
try{
	img = null;
	thisPath = null;
	arFileName = null;
	while (iItemTbl.rows.length > 0) { iItemTbl.deleteRow(0); }
	for (var i = 0;i < linksList.length ; i++){
    if (linksList[i] != null){
      getFilePaths(i);

      showArg = switchList[i];
      if (showArg == "none"){
        showArg = "";
      }
      argLabel = "Commands to pass to " + label;
      if (allowSwitch == "no"){
        argLabel = "You can not set arguments for this object.";
      }

			row = iItemTbl.insertRow(i);  

			cell = row.insertCell();
			cell.style.whiteSpace = "nowrap";
			id = "r"+ i + "c0";
      if (i == (linksList.length-1)) showDown = '<img src="images/blank.png" />';
      else showDown = '<img src="images/down.png" onclick="moveDown('+i+');return false;" onmouseover="overIt();" onmouseout="notOverIt();" />';
      if (i == 0) showUp = '<img src="images/blank.png" />';
      else showUp = '<img src="images/up.png" onclick="moveUp('+i+');return false;" onmouseover="overIt();" onmouseout="notOverIt();" />';

			cell.innerHTML = showUp + "&nbsp;" + showDown + "&nbsp;<input type=\"checkbox\" id=\"" + id + "\" onfocus=\"blur();\" />";

			cell = row.insertCell();
			id = "r"+i + "c1";
			var oImg = document.createElement("img");
			oImg.src = img;
			oImg.title = label;
			oImg.width = 18;
			oImg.height = 18;
			cell.appendChild(oImg);
			cell.width = 25;

			cell = row.insertCell();
			cell.width = 150;
			id = "r"+ i + "c3";
			var oSpan = document.createElement("span");
			oSpan.id = id;
			oSpan.className = "spanFld";
			oSpan.setAttribute("title",thisPath);
			oSpan.setAttribute("link",allowSwitch);
			oSpan.innerText = label;
			cell.appendChild(oSpan);

			cell = row.insertCell();
			cell.width = 110;
			id = "r"+ i + "c4";
			var oSpan = document.createElement("span");
			oSpan.id = id;
			oSpan.className = "argFld";
			oSpan.setAttribute("title",argLabel);
			oSpan.innerText = showArg;
			cell.appendChild(oSpan);
		}
	}
	setTimeout("getFocus()", 300);
} catch(err) {debugLog("bldShortCuts: "+err.name+" - "+err.message)}
}

function getFilePaths(i){
try{
  allowSwitch = "yes";
	thisPath = linksList[i];
	filePath = thisPath;
	label = oFSO.GetFileName(thisPath);

	if (label == ""){
		label = "Drive - "+thisPath;
	}

	if (thisPath == "My Computer"){
		filePath = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
		label = thisPath;
		img = myComp;
    allowSwitch = "no";
	}
	else if (thisPath == "Network"){
		filePath = "::{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}";
		label = thisPath;
		img = network;
    allowSwitch = "no";
	}
	else if (thisPath == "Network VAIO"){
		filePath = "::{7007ACC7-3202-11D1-AAD2-00805FC1270E}";
		label = thisPath;
		img = network;
    allowSwitch = "no";
	}
	else if (thisPath == "Control Panel Category"){
		filePath = "::{26EE0668-A00A-44D7-9371-BEB064C98683}";
		label = thisPath;
		img = control;
    allowSwitch = "no";
	}
	else if (thisPath == "Control Panel Classic"){
		filePath = "::{21EC2020-3AEA-1069-A2DD-08002B30309D}";
		label = thisPath;
		img = control;
    allowSwitch = "no";
	}
	else if (thisPath == "Control Panel VAIO"){
		filePath = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}\\::{21EC2020-3AEA-1069-A2DD-08002B30309D}";
		label = thisPath;
		img = control;
    allowSwitch = "no";
	}
	else if (thisPath == "Internet Explorer"){
		filePath = "::{871C5380-42A0-1069-A2EA-08002B30309D}";
		label = thisPath;
		img = ie;
    allowSwitch = "no";
	}
	else if (thisPath == "Recycle Bin"){
		filePath = "::{645FF040-5081-101B-9F08-00AA002F954E}";
    label = thisPath;
    img = rbEmpty;
    allowSwitch = "no";
	}
	else if (thisPath == "IE Favorites"){
		label = thisPath;
		img = gadgetPath + "\\images\\FavsIE.png";
    allowSwitch = "no";
	}
	else if (thisPath == "Firefox Bookmarks"){
		label = thisPath;
		img = gadgetPath + "\\images\\FavsFF.png";
    allowSwitch = "no";
	}
	else if (thisPath == "Opera Bookmarks"){
		label = thisPath;
		img = gadgetPath + "\\images\\FavsOP.png";
    allowSwitch = "no";
	}
	else if (thisPath == "LauncherDivider"){
		label = thisPath;
		img = gadgetPath + "\\images\\divSmall.png";
    allowSwitch = "no";
	}
	else{
    img = thisPath;
		if (!oFSO.FileExists(thisPath)){
      if (!oFSO.FolderExists(thisPath)){
        img = errorImage;
        allowSwitch = "no";
        label = "file deleted or moved";
      }
		}
	}
			
	if (iconList[i] != "none"){
		img = iconList[i];
    if (!oFSO.FileExists(img)){
      if (!oFSO.FolderExists(img)){
        img = errorImage;
        label = "icon missing";
      }
    }
 	}
	img = "gimage:///" + img + wh;

} catch(err) {debugLog("getFilePaths: "+err.name+" - "+err.message)}
}

function delLinks(){
try{
	var index = 0;
	var startLength = linksList.length;
	linksList.splice(0,linksList.length);
	for (var i = 0; i < iItemTbl.rows.length ; i++){
		id = "r"+i+"c0";
		if (!$(id).checked){
			linksList[index] = $("r"+i+"c3").title;
			iconList[index] = iconList[i];
			switchList[index] = switchList[i];
			index++;
		}
	}
	if (linksList.length == 0){
		DisplayNoLinks(true);
	}
	else{
		bldShortCuts();
		checkDefaults();
		DisplayNoLinks(false);
	}    
	var endLength = linksList.length;
	if (startLength == endLength){
    $("warning").innerText="You must select an item to remove it.";
  }
  else{
    $("warning").innerText="Add any object by dragging it to the list.";
  }
} catch(err) {debugLog("delLinks: "+err.name+" - "+err.message)}
}

function addToArray(addThis){
try{
  if (linksList.length == 0){
		iconList = new Array();
		switchList = new Array();
	}
  var n = linksList.length;
	linksList[n] = addThis;
	iconList[n] = "none";
	switchList[n] = "none";
} catch(err) {debugLog("addToArray: "+err.name+" - "+err.message)}
}

function addDivide(){
try{
  addToArray("LauncherDivider");
	bldShortCuts(); 
 	DisplayNoLinks(false);
} catch(err) {debugLog("addDivide: "+err.name+" - "+err.message)}
}

function changeIco(){
try{
	var index = 0;
	for (var i = 0; i < iItemTbl.rows.length ; i++){
		id = "r"+i+"c0";
		if ($(id).checked){
			index++;
			var ChangeThisFilesIcon = linksList[i];
			var location = i;
		}
	}

	if (index == 1){
		if (ChangeThisFilesIcon != "Recycle Bin"){
			var oItem = System.Shell.chooseFile(true, "Icon Files:*.ico;*.png;*.exe::", "", "");
			if (oItem){
				iconList[location] = oItem.path;
				bldShortCuts(); 
				DisplayNoLinks(false);
        $("warning").innerText="Add any object by dragging it to the list.";
			}
		}
		else{
      $("warning").innerText="Click the Layout tab to change this.";
		}
	}
	
	if (index == 0){
    $("warning").innerText="First select an item to change it's icon.";
	}
	if (index > 1){
    $("warning").innerText="Select only one item at a time.";
	}
} catch(err) {debugLog("changeIco: "+err.name+" - "+err.message)}
}

function resetIco(){
try{
	var index = 0;
	for (var i = 0; i < iItemTbl.rows.length ; i++){
		id = "r"+i+"c0";
		if ($(id).checked){
			index++;
      iconList[i] = "none";
		}
	}
	if (index > 0){
		bldShortCuts(); 
		DisplayNoLinks(false);
    $("warning").innerText="Icons reset to original state.";
	}
	if (index == 0){
    $("warning").innerText="First select the items you want to reset.";
	}
} catch(err) {debugLog("resetIco: "+err.name+" - "+err.message)}
}

function moveDown(thisOne){
try{
	if (thisOne < linksList.length){
    switchThem(thisOne);
	}
	bldShortCuts();
	DisplayNoLinks(false);
} catch(err) {debugLog("moveDown: "+err.name+" - "+err.message)}
}

function moveUp(thisOne){
try{
	if (thisOne > 0){
    thisOne = thisOne-1;
    switchThem(thisOne);
	}
	bldShortCuts();
	DisplayNoLinks(false);
} catch(err) {debugLog("moveUp: "+err.name+" - "+err.message)}
}

function switchThem(thisOne){
try{
	var index = 0;
	for (var i = 0; i < iItemTbl.rows.length ; i++){
		if (i == thisOne){
      var linkTemp = linksList[index];
			var iconTemp = iconList[index];
			var switchTemp = switchList[index];
			linksList[index] = linksList[i+1]; 
			iconList[index] = iconList[i+1];
			switchList[index] = switchList[i+1];
			index++;
			linksList[index] = linkTemp; 
			iconList[index] = iconTemp;
			switchList[index] = switchTemp;
			i++;     
		}
		index++;
	}
} catch(err) {debugLog("switchThem: "+err.name+" - "+err.message)}
}

function overIt(){
	var img = event.srcElement;
	img.style.cursor = "hand";
}

function notOverIt(){
	var img = event.srcElement;
	img.style.cursor = "default";
}

function clearMenu(){
	defaults.style.display = "none";
	style.style.display = "none";
	save.style.display = "none";
	misc.style.display = "none";
	reorder.style.display = "none";
	menu1.style.borderBottomColor = "#505050";
	menu2.style.borderBottomColor = "#505050";
	menu3.style.borderBottomColor = "#505050";
	menu4.style.borderBottomColor = "#505050";
	menu5.style.borderBottomColor = "#505050";
}

function menuA(){
  clearMenu();
	reorder.style.display = "";
	menu1.style.borderBottomColor = "#f0f0f0";
}

function menuB(){
  clearMenu();
	style.style.display = "";
	menu2.style.borderBottomColor = "#f0f0f0";
	checkDefaults();
}

function menuC(){
  clearMenu();
	defaults.style.display = "";
	menu3.style.borderBottomColor = "#f0f0f0";
}

function menuD(){
  clearMenu();
	misc.style.display = "";
	menu4.style.borderBottomColor = "#f0f0f0";
	checkDefaults();
}

function menuE(){
  clearMenu();
	save.style.display = "";
	menu5.style.borderBottomColor = "#f0f0f0";
}

function fileDragDropped(){
try{
	var sFile;
	for (var i=0 ; System.Shell.itemFromFileDrop(event.dataTransfer, i) != null ; i++){
		sFile = System.Shell.itemFromFileDrop(event.dataTransfer, i).path;
    addToArray(sFile);
	}
	bldShortCuts();  
	DisplayNoLinks(false);
}	catch(err)	{debugLog("fileDragDropped: "+err.name+" - "+err.message)}
}

function openGadgetFolder(){
try{
	System.Shell.execute(settingsDir);
} catch(err) {debugLog("openGadgetFolder: "+err.name+" - "+err.message)}
}

function openBgFolder(){
try{
	System.Shell.execute(gadgetPath+"\\images\\backgrounds");
} catch(err) {debugLog("openBgFolder: "+err.name+" - "+err.message)}
}

function changeRbIcon(whichOne){
try{
	var oItem = System.Shell.chooseFile(true, "Icon Files:*.ico;*.png;*.exe::", "", "");
	if (oItem){
    if (whichOne == "empty"){
      rbEmpty = oItem.path;
      $("rbEmptyIcon").src = "gimage:///" + rbEmpty + "?width=32&height=32";
    }
    else{
      rbFull = oItem.path;
      $("rbFullIcon").src = "gimage:///" + rbFull + "?width=32&height=32";
    }
  }
  saveRbSettings();
} catch(err) {debugLog("changeRbEmptyIcon: "+err.name+" - "+err.message)}
}

function saveRbSettings(){
try{
	if (oFSO.FileExists(rbSettingsFile)){
		oFSO.DeleteFile(rbSettingsFile, true);
	}
	if (rbEmpty == gadgetPath+"\\images\\rbEmpty.ico" && rbFull == gadgetPath+"\\images\\rbFull.ico") return;
	var oFile = oFSO.CreateTextFile(rbSettingsFile, true);
	oFile.WriteLine(rbEmpty);
	oFile.WriteLine(rbFull);
	oFile.Close();
} catch(err) {debugLog("saveRbSettings: "+err.name+" - "+err.message)}
}

function setRbIcons(){
try{
	if (oFSO.FileExists(rbSettingsFile)){	
    defaultFile = oFSO.OpenTextFile(rbSettingsFile, 1, false);
		rbEmpty = defaultFile.ReadLine();
		rbFull = defaultFile.ReadLine();
		defaultFile.Close();
	}
} catch(err) {debugLog("setRbIcons: "+err.name+" - "+err.message)}
}

function resetRbIco(){
try{
  rbEmpty = gadgetPath+"\\images\\rbEmpty.ico";
  rbFull = gadgetPath+"\\images\\rbFull.ico";
  $("rbEmptyIcon").src = "gimage:///" + rbEmpty + "?width=32&height=32";
	$("rbFullIcon").src = "gimage:///" + rbFull + "?width=32&height=32";
  saveRbSettings();
} catch(err) {debugLog("resetRbIco: "+err.name+" - "+err.message)}
}

function nextBG(){
  bgSetting++;
  if (bgSetting > numBGs) {bgSetting = 1;}
  setBgColor();
}

function prevBG(){
  bgSetting--;
  if (bgSetting < 1) {bgSetting = numBGs;}
  setBgColor();
}

function setBgColor(){
  bgPath = "url(/images/backgrounds/bg" + bgSetting + ".jpg)";
  $("bgPreview").style.backgroundImage = bgPath;
}

function countBackgrounds(){
try{
  numBGs = 31;
  while (oFSO.FileExists(gadgetPath + "\\images\\backgrounds\\bg" + numBGs + ".jpg")){
    numBGs++;
  }
  numBGs--;
} catch(err) {debugLog("countBackgrounds: "+err.name+" - "+err.message)}
}

function addArguments(){
try{
	var index = 0;
	argLocation = null;
	for (var i = 0; i < iItemTbl.rows.length ; i++){
		id = "r"+i+"c0";
		if ($(id).checked){
			index++;
			var allowSwitch = $("r"+i+"c3").link;
			argLocation = i;
		}
	}

	if (index == 1 && allowSwitch == "yes"){
    thisName = linksList[argLocation];
    label = oFSO.GetFileName(thisName);
    appName.innerText = label;
    commands.style.display = "";
    commandInput.focus();
    if (switchList[argLocation] != "none"){
      commandInput.value = switchList[argLocation];
    }
    else{
      commandInput.value = "";
    }
	}

	if (index == 1 && allowSwitch == "no"){
    $("warning").innerText="You can not set arguments for this object.";
  }
	if (index == 0){
    $("warning").innerText="First select an item to add arguments.";
	}
	if (index > 1){
    $("warning").innerText="Select only one item at a time.";
	}
} catch(err) {debugLog("addArguments: "+err.name+" - "+err.message)}
}

function setArg(){
try{
  var arg = commandInput.value.replace(/^\s+|\s+$/g,""); // remove blank spaces from the begining and end
  if (arg != ""){
    switchList[argLocation] = arg;
  }
  else{
    switchList[argLocation] = "none";
  }
  
  commandInput.value = "";
	commands.style.display = "none";
	bldShortCuts();
	DisplayNoLinks(false);
} catch(err) {debugLog("setArg: "+err.name+" - "+err.message)}
}

function cancelSetArg(){
  commandInput.value = "";
	commands.style.display = "none";
	uncheckAll();
}

function uncheckAll(){
	for (var i = 0; i < iItemTbl.rows.length ; i++){
		id = "r"+i+"c0";
		$(id).checked = false;
  }
}

function removeToolTips(){
	$("L_REMOVE").title="";
	$("L_CHANGE").title="";
	$("L_RESET").title="";
	$("L_ARG").title="";
	$("L_RB").title="";
	$("L_DIV").title="";
	$("L_SPEC").title="";
	$("L_SETARG").title="";
	$("L_CARG").title="";
	$("L_BGFLD").title="";
	$("L_COUNT").title="";
	$("L_GFLD").title="";
	$("L_PBG").title="";
	$("L_NBG").title="";
	$("rbEmptyIcon").title="";
	$("rbFullIcon").title="";
	$("L_PRO1").title = "";
	$("L_PRO2").title = "";
	$("L_PRO3").title = "";
	$("L_PRO4").title = "";
	$("L_PROSELECT").title = "";
	$("L_PROCANCEL").title = "";
	$("L_SAVDRAG").title = "";
	$("L_CANDRAG").title = "";
	$("L_DRAGIT").title = "";
}

var cc = ["00","11","33","44","66","88","99","bb","cc","ee","ff"];

function createColorChooser(){
try{
  colorChooser.innerHTML = "";
  infoText.style.color = textColorSetting;
  var index = 0;
  var oTable = document.createElement("table");
  oTable.setAttribute("cellPadding",0);
  oTable.setAttribute("cellSpacing",0);
  oTable.setAttribute("border",0);
  row = oTable.insertRow();
  for (var i=0; i < 11; i++){  // sets grey scale
    cell = row.insertCell();
    cell.width = "10px";
    cell.height = "10px";
    cell.style.setAttribute("cssText", "background:#"+cc[i]+cc[i]+cc[i]);
    cell.onclick = function(){setInfoColor()};
    index++;
  }
  for (var i=0; i < 5; i++){  // sets random colors
    row = oTable.insertRow();
    for (var j=0; j < 11; j++){
      var x = Math.floor(Math.random()*11);
      var y = Math.floor(Math.random()*11);
      var z = Math.floor(Math.random()*11);
      cell = row.insertCell();
      cell.width = "10px";
      cell.height = "10px";
      cell.style.setAttribute("cssText", "background:#"+cc[x]+cc[y]+cc[z]);
      cell.onclick = function(){setInfoColor()};
      index++;
    }
  }
  colorChooser.appendChild(oTable);
} catch(err) {debugLog("createColorChooser: "+err.name+" - "+err.message)}
}

function setInfoColor(){
  var clickedColor = event.srcElement.style.backgroundColor;
  infoText.style.color = clickedColor;
  textColorSetting = clickedColor;
}

function checkDefaults(){
try{
	for (var i = 0;i < 9 ; i++){
		$('sp'+i).checked = false;
    $('sp'+i).style.display = "";
	}
	for (var i = 0; i < linksList.length ; i++){
		var testIt = linksList[i];
		
		for (var j = 0; j < 9; j++){
      if (testIt == specialLinksArray[j]){
        thisID = "sp"+j;
        $(thisID).style.display = "none";
      }
		}
	}
} catch(err) {debugLog("checkDefaults: "+err.name+" - "+err.message)}
}

function updateDef(){
try{
	for (var i = 0; i < 9 ; i++){
		id = "sp"+i;
		if ($(id).checked){
      addThis = specialLinksArray[i];
      addToArray(addThis);
		}
	}
	bldShortCuts();
	checkDefaults();
	DisplayNoLinks(false);
} catch(err) {debugLog("updateDef: "+err.name+" - "+err.message)}
}

function pro1(){
  settingsDir = oShell.SpecialFolders("Appdata")+"\\App Launcher Gadget";
  write("settingsDir", settingsDir);
  loadSettings();
}

function pro2(){
  settingsDir = oShell.SpecialFolders("Appdata")+"\\App Launcher Gadget\\profile2";
  write("settingsDir", settingsDir);
  loadSettings();
}

function pro3(){
  settingsDir = oShell.SpecialFolders("Appdata")+"\\App Launcher Gadget\\profile3";
  write("settingsDir", settingsDir);
  loadSettings();
}

function pro4(){
  settingsDir = oShell.SpecialFolders("Appdata")+"\\App Launcher Gadget\\profile4";
  write("settingsDir", settingsDir);
  loadSettings();
}

