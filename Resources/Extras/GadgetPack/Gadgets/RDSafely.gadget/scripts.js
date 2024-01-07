var shell, drives, interval;
var userDir = System.Environment.getEnvironmentVariable('USERPROFILE');

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

function initDrives(gd)
{
	if(interval){
		clearTimeout(interval);	
	}
	
	drives 	 	= new Array();
	var Obj 	= new ActiveXObject("Scripting.FileSystemObject");
	var oDrive 	= new Enumerator(Obj.Drives);
	
	for (; !oDrive.atEnd(); oDrive.moveNext())
	{
		var dinfo = System.Shell.drive(oDrive.item().DriveLetter);
		
		if(dinfo.driveType == 2 || dinfo.driveType == 3) // Removable or  Fixed Drives
		{
			var drive 		= new Object();
			drive.type 		= dinfo.driveType;
			drive.letter 	= oDrive.item().DriveLetter;
			drive.volume	= (dinfo.isReady ? (dinfo.volumeLabel ? dinfo.volumeLabel : (drive.type == 2 ? 'USB' : 'HDD')) : 'Not Ready');
			drive.ready 	= dinfo.isReady;
			drive.total		= (dinfo.isReady ? dinfo.totalSize : 0);
			drive.free		= (dinfo.isReady ? dinfo.freeSpace : 0);			
			drives.push(drive);
		}
	}
	
	if(!gd){
		showDrives();
	}
}

function inArray(str, arr)
{
	for(var i=0; i<arr.length; i++){
		if(str == arr[i]){
			return true;	
		}
	}
	return false;
}

function variable(varName, def)
{
	var value = System.Gadget.Settings.read(varName);
	if (typeof(value) == "undefined" || value == ''){
		value = def;
	}
	return value;
}

function SettingsClosing(event)
{
	if (event.closeAction == event.Action.commit){
		initDrives();
		shell.popup('ok');
	}
	event.cancel = false;
}

function showDrives()
{
	var hideNotReady 	= variable("rds_hideNotReady", "yes");
	var hideDrives 		= variable("rds_hideDrives", "").split(",");

	var y = 0;

	canvas.removeObjects();

	//Edit by Helmut Buhler (author of 8GadgetPack) on 2015-12-03
	//Added high-dpi support and changed mouse click handling. The gadget
	//was consuming a lot of cpu time over time because it added lots of
    //div elements.
	//targets.innerHtml = '';
	
	// Default
	var count = 0;
	
	for(var i = 0; i < drives.length; i++)
	{
		var d = drives[i];
		
		// Populate
		if(!inArray(d.letter, hideDrives))
		{
			if(hideNotReady == 'yes')
			{
				if(d.ready)
				{
				    fixDpiObject(canvas.addImageObject('images/bg.png', 0, y));  					
					fixDpiObject(canvas.addImageObject('images/drive'+d.type+'.png', 0, y));
					fixDpiObject(canvas.addImageObject('images/eject.png', 110, y+5));
					fixDpiObject(canvas.addTextObject(d.volume+' ('+d.letter+':)', 'Segoe UI', 11, 'white', 32, y+6));

					// Bar
					var f = Math.round(d.free / d.total * 100);
					var u 	= (100 - f);
					if(u > 0){
						var m = fixDpiObject(canvas.addImageObject('images/m' + (u < 90 ? 'b': (u < 95 ? 'o': 'r')) + '.png', 0, y + 23));
						m.width = Math.floor((u * 116) / 100) * dpiScaling;
						m.left = 8 * dpiScaling + (116 / 2) * (dpiScaling - 1) - Math.floor(((116 * dpiScaling - m.width) / 2));
	            	}
					
					// Explore
					/*var o = document.createElement('DIV');
					o.className = 'target';
					o.style.posTop = y;
					o.style.width	= '30px';
					o.style.height	= '20px';
					o.setAttribute('drive', d.letter);
					o.ondblclick = (d.ready ? openDrive : null);
					targets.appendChild(o);
					
					// Eject
					var e = document.createElement('DIV');
					e.className = 'target';
					e.style.posLeft = 109;
					e.style.posTop 	= y+5;
					e.style.width	= '15px';
					e.style.height	= '15px';
					e.setAttribute('drive', d.letter);
					e.onclick = removeDrive;
					targets.appendChild(e);*/
					
					
					y += 28;
					count++;
				}
			}
			else{
				fixDpiObject(canvas.addImageObject('images/bg.png', 0, y));  
				if(d.ready){
					fixDpiObject(canvas.addImageObject('images/drive'+d.type+'.png', 0, y));
									
					// Bar
					var f 	= Math.round(d.free / d.total * 100);
					var u 	= (100 - f);
					if(u > 0){
						var m = fixDpiObject(canvas.addImageObject('images/m' + (u < 90 ? 'b': (u < 95 ? 'o': 'r')) + '.png', 0, y + 23));
						m.width = Math.floor((u * 116) / 100) * dpiScaling;
						m.left = 8 * dpiScaling + (116 / 2) * (dpiScaling - 1) - Math.floor(((116 * dpiScaling - m.width) / 2));
		}
					
					// Explore
					/*var o = document.createElement('DIV');
					o.className = 'target';
					o.style.posTop = y;
					o.style.width	= '30px';
					o.style.height	= '20px';
					o.setAttribute('drive', d.letter);
					o.ondblclick = (d.ready ? openDrive : null);
					targets.appendChild(o);*/
				}
				else{
					fixDpiObject(canvas.addImageObject('images/notready.png', 0, y));	
				}
				fixDpiObject(canvas.addImageObject('images/eject.png', 110, y+5));
				fixDpiObject(canvas.addTextObject(d.volume+' ('+d.letter+':)', 'Segoe UI', 11, 'white', 32, y+6));
				
				// Eject
				/*var e = document.createElement('DIV');
				e.className = 'target';
				e.style.posLeft = 109;
				e.style.posTop 	= y+5;
				e.style.width	= '15px';
				e.style.height	= '15px';
				e.setAttribute('drive', d.letter);
				e.onclick = removeDrive;
				targets.appendChild(e);*/

				y += 28;
				count++;
			}
		}		
	}
	
	if(count > 0){
		if (y < 57) y = 57;
	}
	else{
		fixDpiObject(canvas.addImageObject('drag.png', 20, 0)); 
		y = 96;
	}
	canvas.style.height = y;
	document.body.style.posHeight = y;			
	
	//var autoDiscovery = variable("rds_autoDiscovery", 1);
	
	//if(autoDiscovery == 1){
		interval = setTimeout(initDrives, 5000);
	/*}
	else{
		if(interval){
			clearInterval(interval);
		}
	}*/
}

function onBodyClick(event)
{
    var hideNotReady = variable("rds_hideNotReady", "yes");
    var hideDrives = variable("rds_hideDrives", "").split(",");

    var y = 0;

    // Default
    var count = 0;

    for (var i = 0; i < drives.length; i++) {
        var d = drives[i];

        // Populate
        if (!inArray(d.letter, hideDrives)) {
            if (hideNotReady == 'yes') {
                if (d.ready) {

                    // Explore
                    if (event.clientX < 30 && event.clientY >= y && event.clientY < y + 20 &&
                            d.ready)
                        openDrive(d.letter);

                    // Eject
                    if (event.clientX >= 109 && event.clientX < 109 + 15 && event.clientY >= y + 5 && event.clientY < y + 5 + 15)
                        removeDrive(d.letter);

                    y += 28;
                    count++;
                }
            }
            else {
                // Explore
                if (event.clientX < 30 && event.clientY >= y && event.clientY < y + 20 &&
                        d.ready)
                    openDrive(d.letter);

                // Eject
                if (event.clientX >= 109 && event.clientX < 109 + 15 && event.clientY >= y + 5 && event.clientY < y + 5 + 15)
                    removeDrive(d.letter);

                y += 28;
                count++;
            }
        }
    }
}

function openDrive(d)
{
	//var d = window.event.srcElement.getAttribute('drive');
	System.Shell.execute(d + ':\\');
}

function removeDrive(d)
{
	var askConfirm = variable("rds_askConfirm", "yes");

	//var d = window.event.srcElement.getAttribute('drive');
	
	if(askConfirm == 'yes'){
		var conf = shell.Popup('Please confirm removal of drive ('+d+':)', 0, 'Remove Drive ('+d+':)', 4+32);
		if(conf == 6){
			var exec = shell.Run('cmd /C "'+System.Gadget.path+'\\rd.exe" '+d+': -b',0 , true);
		}
		else{
			return;	
		}
	}
	else{
		var exec = shell.Run('cmd /C "'+System.Gadget.path+'\\rd.exe" '+d+': -b',0 , true);
	}
	
	// Result
	switch(exec)
	{
		case 0:
			initDrives();
			break;
		
		case 1:
			shell.Popup("Operation failed.\nDevice is in use.");
			break;
		
		case 2:
			shell.Popup("Operation failed.\nDevice not found.");
			break;

		default:
			shell.Popup("Operation failed.\nUnknown Error.");
			break;
	}
}