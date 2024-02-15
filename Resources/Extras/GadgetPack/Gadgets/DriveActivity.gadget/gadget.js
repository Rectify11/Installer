var version = "1.3.0.0";

var maxHistory	= 105;
var refreshRate	= 1000;

var queryType;
var historyType;
var driveMask;

var values = new Array();

function query() {
	var locator = new ActiveXObject("WbemScripting.SWbemLocator");
	var service = locator.ConnectServer(null, "root\\cimv2");
	return service.ExecQuery((queryType == 0) ?
		"SELECT * FROM Win32_PerfFormattedData_PerfDisk_LogicalDisk" :
		"SELECT * FROM Win32_PerfFormattedData_PerfDisk_PhysicalDisk");
}

function compareDriveLetters(a, b) {
	return a.drive.charCodeAt(0) - b.drive.charCodeAt(0);
}

function refresh() {
	var resultSet = query();

    var drives = 0;
	var pile = new Array();
	for (var i = 0; i < resultSet.Count; i++) {
		var name = resultSet.ItemIndex(i).Name;
		var start = name.indexOf(":") - 1;
		if ((start >= 0) && ((driveMask & (1 << (name.charCodeAt(start) - 65))) != 0)) {
			var temp = new Object();
			temp.drive = name.slice(start);
            temp.value = Number(resultSet.ItemIndex(i).PercentDiskTime);
            if (temp.value > 100)
                temp.value = 100;
			pile.push(temp);
			drives++;
        }
    }
    if (drives == 0) {
    	for (var i = 0; i < resultSet.Count; i++) {
		    var name = resultSet.ItemIndex(i).Name;
		    var start = name.indexOf(":") - 1;
		    if (start >= 0) {
			    var temp = new Object();
			    temp.drive = name.slice(start);
                temp.value = Number(resultSet.ItemIndex(i).PercentDiskTime);
                if (temp.value > 100)
                    temp.value = 100;
			    pile.push(temp);
			    drives++;
            }
        }
    }

	pile.sort(compareDriveLetters);
	if (drives > 12) drives = 12;

	if (drives != values.length) {
		values = new Array();
		for (var i=0; i<drives; i++) {
			values.push(new Array());
		}
        /*for (var i = 0; i < maxHistory; i++) {
            x = Math.floor((maxHistory - i) * 100 / maxHistory);
            if (i > maxHistory / 2 && i % 2 == 0)
                x = 0;
            for (var j = 0; j < drives; j++) {
                values[j].push(x);
            }
        }*/
		var html = "<g:background src=\"images/background" + drives + ".png\">";
        html += "<img id=\"win10back\" src=\"images/background" + drives + "_.png\" style=\"position:absolute;top:0px; left:0px;-ms-interpolation-mode: nearest-neighbor;\" />";
		for (var i=0; i<drives; i++) {
			html += "<g:text style=\"top:" + (i * 17 + 6) + "px;left:7px;\">";
			html += pile[i].drive;
			html += "</g:text>";
		}
        // v:shape is replaced with g:shape in the following. This fixes display issues when high-dpi is selected.
        // This works only with 8GadgetPack Version 30 and higher.
        html += "<g:shape id=\"graph\" style=\"position:absolute;top:5px; left:20px; width:" + maxHistory + "px; height:" + (drives * 17 - 1) +
				"px\" coordsize=\"" + maxHistory + " " + (drives * 17 - 1) + "\" strokecolor=\"#058\" fillcolor=\"#058\"/>";
		document.body.style.height = (drives * 17 + 9) + "px";
        document.body.innerHTML = html + "</g:background>";
	}

	var path = "";
	for (var i=0; i<drives; i++) {
		if (values[i].push(pile[i].value) > maxHistory) {
			values[i].shift();
		}

		var bottom = ((i + 1) * 17) - 2;
		switch (historyType) {
		case 0: // polygon
			path += "m 0," + bottom + " l";
			for (var x=0; x<values[i].length; x++) {
				var y = bottom - Math.floor((15 * values[i][x]) / 100);
				path += " " + x + "," + y;
			}
			path += " " + (values[i].length - 1) + "," + bottom + " x ";
			break;
		default: // line
			for (var x=0; x<values[i].length; x++) {
				var y = bottom - Math.floor((15 * values[i][x]) / 100);
				path += "m " + x + "," + bottom + " l " + x + "," + y + " ";
			}
		}
	}
	graph.path = path + "e";
}

function onLoad() {
	queryType = System.Gadget.Settings.read("queryType");
	if (queryType == "") queryType = 0;

	historyType = System.Gadget.Settings.read("historyType");
	if (historyType == "") historyType = 0;

	driveMask = System.Gadget.Settings.read("driveMask");
	if (driveMask == "") driveMask = (1 << 26) - 1;

	System.Gadget.settingsUI = "settings.html";
	window.setInterval("refresh();", refreshRate);
}

// --------------------------------------------------------------------------

function onSettingsClosed(event) {
	if (event.closeAction == event.Action.commit) {
		System.Gadget.Settings.writeString("queryType", queryTypeSelect.selectedIndex);
		System.Gadget.document.parentWindow.queryType = queryTypeSelect.selectedIndex;

		System.Gadget.Settings.writeString("historyType", historyTypeSelect.selectedIndex);
		System.Gadget.document.parentWindow.historyType = historyTypeSelect.selectedIndex;

		var driveMask = 0;
		for (var i=0; i<26; i++) {
			if (driveSelect.options[i].selected) {
				driveMask |= (1 << i);
			}
		}
		System.Gadget.Settings.writeString("driveMask", driveMask);
		System.Gadget.document.parentWindow.driveMask = driveMask;
	}
}

function loadSettings() {
	var queryType = System.Gadget.Settings.read("queryType");
	if (queryType == "") queryType = 0;
	queryTypeSelect.selectedIndex = queryType;

	var historyType = System.Gadget.Settings.read("historyType");
	if (historyType == "") historyType = 0;
	historyTypeSelect.selectedIndex = historyType;

	var driveMask = System.Gadget.Settings.read("driveMask");
	if (driveMask == "") driveMask = (1 << 26) - 1;
	for (var i=0; i<26; i++) {
		var c = String.fromCharCode(65 + i);
		var o = document.createElement("option");
		o.text = c + ":";
		o.selected = ((driveMask & (1 << i)) != 0);
		driveSelect.options.add(o);
	}

	System.Gadget.onSettingsClosed = onSettingsClosed;
}
