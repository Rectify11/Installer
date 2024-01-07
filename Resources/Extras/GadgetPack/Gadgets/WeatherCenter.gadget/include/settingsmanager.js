//	Javascript file for the WeatherCenter gadget
//	(c) 2009
//	WeatherCenter Gadget Team
//	Development: hadj 
//	Graphics: Tex
//	Testing: Digital	
////////////////////////////////////////////////////////////////////////



function ReadSettings()
{	

	var fs = new ActiveXObject("Scripting.FileSystemObject");
	var ts = fs.OpenTextFile(System.Gadget.path + "/settings.ini", 1);
	var ini = "";		
	ini = ts.ReadAll();
	ts.Close();
	
	var lines=ini.split('\n');
	var lineCount = lines.length;				
	
	var n = 0;
			
	while (n < lineCount-1) {

		var str=lines[n];
		var key = str.substring(0, str.indexOf("="));
		var value = str.substring(str.indexOf("=") + 1, str.length);
		value = value.replace("\r","");
		if (value == 'true') value = 1;
		if (value == 'false') value = 0;
		if ((value+"").indexOf("lng_defLocationCode") > -1 || (value+"").indexOf("lng_defLastSearch") > -1) System.Gadget.Settings.write(key, window[value]);
		else System.Gadget.Settings.write(key, value);
		n++;
	}
				
}




function WriteSettings()
{
	DataArr = [];

	DataArr.push({"Key":"fcDays", "Value":System.Gadget.Settings.read("fcDays")});
	DataArr.push({"Key":"SourceOfUpdates", "Value":GetSourceOfUpdates.value});
	DataArr.push({"Key":"showMiniMode", "Value":System.Gadget.Settings.read('showMiniMode')});
	DataArr.push({"Key":"Skin", "Value":System.Gadget.Settings.read('Skin')});
	DataArr.push({"Key":"changeSkin", "Value":System.Gadget.Settings.read('changeSkin')});
	DataArr.push({"Key":"noCountry", "Value":System.Gadget.Settings.read('noCountry')});
	DataArr.push({"Key":"unDockAlter", "Value":System.Gadget.Settings.read('unDockAlter')});
	DataArr.push({"Key":"showLastTimeUpdate", "Value":System.Gadget.Settings.read('showLastTimeUpdate')});
	DataArr.push({"Key":"showForecastToday", "Value":System.Gadget.Settings.read('showForecastToday')});
	DataArr.push({"Key":"showFlyoutForecast", "Value":System.Gadget.Settings.read('showFlyoutForecast')});
	DataArr.push({"Key":"showDayNameForecast", "Value":System.Gadget.Settings.read('showDayNameForecast')});
	DataArr.push({"Key":"hideBarSettings", "Value":System.Gadget.Settings.read('hideBarSettings')});
	DataArr.push({"Key":"showWeatherAfterRestart", "Value":System.Gadget.Settings.read('showWeatherAfterRestart')});

	DataArr.push({"Key":"tunits", "Value":System.Gadget.Settings.read('tunits')});
	DataArr.push({"Key":"sunits", "Value":System.Gadget.Settings.read('sunits')});
	DataArr.push({"Key":"punits", "Value":System.Gadget.Settings.read('punits')});
	DataArr.push({"Key":"dunits", "Value":System.Gadget.Settings.read('dunits')});

	getDataToWrite("W");
	getDataToWrite("A");
	getDataToWrite("WU");
	getDataToWrite("MSN");
	getDataToWrite("WE");
	getDataToWrite("WB");
	getDataToWrite("YA");
	getDataToWrite("GIS");
	getDataToWrite("NOAA");
	getDataToWrite("METEONOVA");

	var fs = new ActiveXObject("Scripting.FileSystemObject");
	var newFile = fs.CreateTextFile(System.Gadget.path + "/settings.ini", true);

	for (i=0; i<DataArr.length; i++ )
	newFile.WriteLine(DataArr[i].Key + "=" + DataArr[i].Value);
	
	newFile.Close();
}




function getDataToWrite(source)
{
		DataArr.push({"Key":source+"locationCode", "Value":System.Gadget.Settings.read(source+'locationCode')});
		if (source == 'GIS' || source == 'YA' || source == 'METEONOVA') DataArr.push({"Key":source+"lastSearch", "Value":System.Gadget.Settings.read(source+'locationCode')});
		else DataArr.push({"Key":source+"lastSearch", "Value":System.Gadget.Settings.read(source+'lastSearch')});
		DataArr.push({"Key":source+"updateInterval", "Value":System.Gadget.Settings.read(source+'updateInterval')});
		DataArr.push({"Key":source+"ShowParametersOption1", "Value":System.Gadget.Settings.read(source+'ShowParametersOption1')});
		DataArr.push({"Key":source+"ShowParametersOption2", "Value":System.Gadget.Settings.read(source+'ShowParametersOption2')});
		DataArr.push({"Key":source+"ShowParametersOption3", "Value":System.Gadget.Settings.read(source+'ShowParametersOption3')});
		DataArr.push({"Key":source+"ShowParametersOption4", "Value":System.Gadget.Settings.read(source+'ShowParametersOption4')});
	
}



function WritefcDays()
{

	var fs = new ActiveXObject("Scripting.FileSystemObject");
	var ts = fs.OpenTextFile(System.Gadget.path + "/settings.ini", 1);
	var ini = ts.ReadAll();
	ts.Close();


	var lines = ini.split('\n');
	
	var str = lines[0];
	var key = str.substring(0, str.indexOf("="));
	lines[0] = key + "=" + System.Gadget.Settings.read('fcDays');

	var str = lines[2];
	var key = str.substring(0, str.indexOf("="));
	lines[2] = key + "=" + System.Gadget.Settings.read('showMiniMode');


	ini = lines.join('\n');

	
	var fs = new ActiveXObject("Scripting.FileSystemObject");
	var newFile = fs.CreateTextFile(System.Gadget.path + "/settings.ini", true);
	newFile.Write(ini);
	newFile.Close();
	
}

