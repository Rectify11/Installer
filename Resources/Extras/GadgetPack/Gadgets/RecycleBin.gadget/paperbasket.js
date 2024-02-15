var deac = null;
var autosync = null;
window.onload = BasicInit;

function BasicInit()
{
//	System.Gadget.settingsUI = "settings.html";
	System.Shell.RecycleBin.onRecycleBinChanged = ShowBasketInformations;
	settings.alt = BIN_SETTINGS;
	sync.alt = BIN_SYNC;
	basketImage.alt = BIN_OPEN;
	document.body.ondrop = MoveToBin;
	basketImage.ondragenter = cancelEvent;
	basketImage.ondragover = cancelEvent;
	ShowBasketInformations();
	autosync = setInterval("SyncBin()", 120000);

//	if ( System.Gadget.Settings.read("transparent") == "yes" )
//	{
//		background.src = "images/space.gif";
//	}
}

function cancelEvent()
{
	event.returnValue = false;
}

function MoveToBin()
{
	var item = null;
	var index = 0;

	item = System.Shell.itemFromFileDrop(event.dataTransfer, index);
	while (item != null)
	{ 
		if (item)
		{
			System.Shell.RecycleBin.deleteItem(item.path);
		}

		index++;
		item = System.Shell.itemFromFileDrop(event.dataTransfer, index);
	}

	SyncBin();
}

function ShowBasketInformations()
{
//	if ( System.Gadget.Settings.read("transparent") == "yes" )
//	{
//		background.src = "images/space.gif";
//	}
//	else
//	{
//		background.src = "images/docked.png";
//	}

	var thesize = System.Shell.RecycleBin.sizeUsed;
	var suffix = '';

	if ((System.Shell.RecycleBin.fileCount == 0) && (System.Shell.RecycleBin.folderCount == 0))
	{
		basketSize.innerHTML = EMPTY;

		emptyAll.src = "images/emptybin_disabled.png";
		emptyAll.style.cursor = "normal";
		emptyAll.alt = BIN_NO_DELETE;

		folderCountx.innerHTML = "0 " + FOLDER_MORE;
		fileCountx.innerHTML = "0 " + FILES_MORE;
	} 
	else
	{
		if (thesize < 1024)
		{
			suffix = "B";
		}
		else if ((thesize >= 1024) && (thesize < (1024*1024)))
		{
			thesize = thesize / 1024;
			suffix = "KB";
		}
		else if ((thesize >= 1024*1024) && (thesize < (1024*1024*1024)))
		{
			thesize = thesize / 1024 / 1024;
			suffix = "MB";
		}
		else if ((thesize >= 1024*1024*1024))
		{
			thesize = thesize / 1024 / 1024 / 1024;
			suffix = "GB";
		}
		
		emptyAll.src = "images/emptybin.png";
		emptyAll.style.cursor = "hand";
		emptyAll.alt = BIN_DELETE;
		
		thesize = thesize + "0"; // to cast integer to string
		thesize = thesize.substring(0, (thesize.length - 1));
		var CommaSep = thesize.indexOf(".");
		if (CommaSep != -1)
		{
			thesize = thesize.substring(0, CommaSep + 3);
		}
		var newsize = thesize.replace(/\./g, DECIMAL_COMMA_SEPERATOR);

		basketSize.innerHTML = newsize + " " + suffix;
		folderCountx.innerHTML = ((System.Shell.RecycleBin.fileCount + System.Shell.RecycleBin.folderCount) == 1) ? "1 " + FOLDER_ONE : (System.Shell.RecycleBin.fileCount + System.Shell.RecycleBin.folderCount) + " " + FOLDER_MORE; 
	}

	var assignimg = '000';
	if ((thesize < 512) && (thesize > 0) && (suffix != "GB"))
	{
		assignimg = "030";
	}
	else if ((thesize > 512) && (suffix != "KB" && suffix != "GB"))
	{
		assignimg = "070";
	}
	else if ((thesize > 1) && (suffix == "GB"))
	{
		assignimg = "100";
	}
	basketImage.src = "images/" + assignimg + ".png";
}

function EmptyPaperBasket()
{
	if (((System.Shell.RecycleBin.fileCount + System.Shell.RecycleBin.folderCount) > 0))
	{
		System.Shell.RecycleBin.emptyAll();
	}
}

function ShowBinSettings()
{
	System.Shell.RecycleBin.showRecycleSettings();
}

function SyncBin()
{
	sync.style.visibility = "hidden";
	deac = setInterval("sync.style.visibility = 'visible'; clearInterval(deac);", 2000);

	ShowBasketInformations();
	ShowBasketInformations();
}

function ShowBin()
{
	System.Shell.execute('::{645FF040-5081-101B-9F08-00AA002F954E}');//explorer.exe', "{645FF040-5081-101B-9F08-00AA002F954E}");
}