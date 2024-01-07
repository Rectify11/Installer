//	Javascript file for the WeatherCenter gadget
//	(c) 2009
//	WeatherCenter Gadget Team
//	Development: hadj 
//	Graphics: Tex
//	Testing: Digital	
////////////////////////////////////////////////////////////////////////


function parseForecastWeatherEye(xmlData)
{
	var parametrsArray = [{"name":"nothing", "capt":"", "span":""}]



var obsMappingArray=new Array();
obsMappingArray[0]=["A+","11","Heavy Hail"];
obsMappingArray[1]=["A+N","11","Heavy Hail"];
obsMappingArray[2]=["A-","11","Light Hail"];
obsMappingArray[3]=["A--","11","Light Hail"];
obsMappingArray[4]=["A","11","Hail"];
obsMappingArray[5]=["A-N","11","Light Hail"];
obsMappingArray[6]=["A--N","11","Light Hail"];
obsMappingArray[7]=["AN","11","Hail"];
obsMappingArray[8]=["BD","2","Blowing Dust"];
obsMappingArray[9]=["BDN","19","Blowing Dust"];
obsMappingArray[10]=["-BKN","2","Partly Cloudy"];
obsMappingArray[11]=["BKN","3","Partly Cloudy"];
obsMappingArray[12]=["-BKNN","19","Partly Cloudy"];
obsMappingArray[13]=["BKNN","20","Partly Cloudy"];
obsMappingArray[14]=["BN","2","Blowing Sand"];
obsMappingArray[15]=["BNN","19","Blowing Sand"];
obsMappingArray[16]=["BS","22","Blowing Snow"];
obsMappingArray[17]=["BSN","22","Blowing Snow"];
obsMappingArray[18]=["CLR","1","Clear"];
obsMappingArray[19]=["CLRN","18","Clear"];
obsMappingArray[20]=["D","2","Dust"];
obsMappingArray[21]=["DN","19","Dust"];
obsMappingArray[22]=["F","17","Fog"];
obsMappingArray[23]=["FN","17","Fog"];
obsMappingArray[24]=["H","2","Haze"];
obsMappingArray[25]=["HN","19","Haze"];
obsMappingArray[26]=["IC","2","Ice Crystals"];
obsMappingArray[27]=["ICN","19","Ice Crystals"];
obsMappingArray[28]=["IF","17","Ice Fog"];
obsMappingArray[29]=["IFN","17","Ice Fog"];
obsMappingArray[30]=["IP+","5","Heavy Ice Pellets"];
obsMappingArray[31]=["IP+N","5","Heavy Ice Pellets"];
obsMappingArray[32]=["IP-","5","Light Ice Pellets"];
obsMappingArray[33]=["IP--","5","Light Ice Pellets"];
obsMappingArray[34]=["IP","5","Ice Pellets"];
obsMappingArray[35]=["IP-N","5","Light Ice Pellets"];
obsMappingArray[36]=["IP--N","5","Light Ice Pellets"];
obsMappingArray[37]=["IPN","5","Ice Pellets"];
obsMappingArray[38]=["IPW+","5","Heavy Ice Pellet Showers"];
obsMappingArray[39]=["IPW+N","5","Heavy Ice Pellet Showers"];
obsMappingArray[40]=["IPW","5","Ice Pellet Showers"];
obsMappingArray[41]=["IPW-","5","Light Ice Pellet Showers"];
obsMappingArray[42]=["IPW--","5","Light Ice Pellet Showers"];
obsMappingArray[43]=["IPWN","5","Ice Pellet Showers"];
obsMappingArray[44]=["IPW-N","5","Light Ice Pellet Showers"];
obsMappingArray[45]=["IPW--N","5","Light Ice Pellet Showers"];
obsMappingArray[46]=["K","2","Smoke"];
obsMappingArray[47]=["KN","19","Smoke"];
obsMappingArray[48]=["L+","9","Heavy Drizzle"];
obsMappingArray[49]=["L+N","9","Heavy Drizzle"];
obsMappingArray[50]=["L","9","Drizzle"];
obsMappingArray[51]=["L-","9","Light Drizzle"];
obsMappingArray[52]=["L--","9","Light Drizzle"];
obsMappingArray[53]=["LN","9","Drizzle"];
obsMappingArray[54]=["L-N","9","Light Drizzle"];
obsMappingArray[55]=["L--N","9","Light Drizzle"];
obsMappingArray[56]=["OVC","8","Overcast"];
obsMappingArray[57]=["-OVC","3","Cloudy"];
obsMappingArray[58]=["OVCN","8","Overcast"];
obsMappingArray[59]=["-OVCN","20","Cloudy"];
obsMappingArray[60]=["R+","10","Heavy Rain"];
obsMappingArray[61]=["R+N","10","Heavy Rain"];
obsMappingArray[62]=["R-","10","Light Rain"];
obsMappingArray[63]=["R--","10","Light Rain"];
obsMappingArray[64]=["R","10","Rain"];
obsMappingArray[65]=["R-N","10","Light Rain"];
obsMappingArray[66]=["R--N","10","Light Rain"];
obsMappingArray[67]=["RN","10","Rain"];
obsMappingArray[68]=["RS","13","Rain and Snow mixed"];
obsMappingArray[69]=["R-S","13","Rain and Snow mixed"];
obsMappingArray[70]=["RS-","13","Rain and Snow mixed"];
obsMappingArray[71]=["R-S-","13","Rain and Snow mixed"];
obsMappingArray[72]=["RSN","13","Rain and Snow mixed"];
obsMappingArray[73]=["R-SN","13","Rain and Snow mixed"];
obsMappingArray[74]=["RS-N","13","Rain and Snow mixed"];
obsMappingArray[75]=["R-S-N","13","Rain and Snow mixed"];
obsMappingArray[76]=["RW+","10","Heavy Rain Showers"];
obsMappingArray[77]=["RW+N","10","Heavy Rain Showers"];
obsMappingArray[78]=["RW","10","Rain Showers"];
obsMappingArray[79]=["RW-","10","Light Rain Showers"];
obsMappingArray[80]=["RW--","10","Light Rain Showers"];
obsMappingArray[81]=["RWN","10","Rain Showers"];
obsMappingArray[82]=["RW-N","10","Light Rain Showers"];
obsMappingArray[83]=["RW--N","10","Light Rain Showers"];
obsMappingArray[84]=["S+","15","Heavy Snow"];
obsMappingArray[85]=["S+N","15","Heavy Snow"];
obsMappingArray[86]=["S-","15","Light Snow"];
obsMappingArray[87]=["S--","15","Light Snow"];
obsMappingArray[88]=["S","15","Snow"];
obsMappingArray[89]=["-SCT","1","Fair"];
obsMappingArray[90]=["SCT","2","A few clouds"];
obsMappingArray[91]=["-SCTN","18","Fair"];
obsMappingArray[92]=["SCTN","19","A few clouds"];
obsMappingArray[93]=["SG+","15","Heavy Snow Grains"];
obsMappingArray[94]=["SG+N","15","Heavy Snow Grains"];
obsMappingArray[95]=["SG-","15","Light Snow Grains"];
obsMappingArray[96]=["SG--","15","Light Snow Grains"];
obsMappingArray[97]=["SG","15","Snow Grains"];
obsMappingArray[98]=["SG-N","15","Light Snow Grains"];
obsMappingArray[99]=["SG--N","15","Light Snow Grains"];
obsMappingArray[100]=["SGN","15","Snow Grains"];
obsMappingArray[101]=["S-N","15","Light Snow"];
obsMappingArray[102]=["S--N","15","Light Snow"];
obsMappingArray[103]=["SN","15","Snow"];
obsMappingArray[104]=["SP-","15","Light Snow Pellets"];
obsMappingArray[105]=["SP--","15","Light Snow Pellets"];
obsMappingArray[106]=["SP","15","Snow Pellets"];
obsMappingArray[107]=["SP-N","15","Light Snow Pellets"];
obsMappingArray[108]=["SP--N","15","Light Snow Pellets"];
obsMappingArray[109]=["SPN","15","Snow Pellets"];
obsMappingArray[110]=["SW+","16","Heavy Flurries"];
obsMappingArray[111]=["SW+N","16","Heavy Flurries"];
obsMappingArray[112]=["SW","16","Flurries"];
obsMappingArray[113]=["SW-","16","Light Flurries"];
obsMappingArray[114]=["SW--","16","Light Flurries"];
obsMappingArray[115]=["SWN","16","Flurries"];
obsMappingArray[116]=["SW-N","16","Light Flurries"];
obsMappingArray[117]=["SW--N","16","Light Flurries"];
obsMappingArray[118]=["T+","11","Severe Thunder"];
obsMappingArray[119]=["T+N","11","Severe Thunder"];
obsMappingArray[120]=["T","11","Thunder"];
obsMappingArray[121]=["TF","11","Foggy with Thunder"];
obsMappingArray[122]=["TH","11","Hazy with Thunder"];
obsMappingArray[123]=["TN","11","Thunder"];
obsMappingArray[124]=["TR+","11","Heavy Thunderstorm"];
obsMappingArray[125]=["TR+N","11","Heavy Thunderstorm"];
obsMappingArray[126]=["TR-","11","Light Thunderstorm"];
obsMappingArray[127]=["TR--","11","Light Thunderstorm"];
obsMappingArray[128]=["TR","11","Thunderstorm"];
obsMappingArray[129]=["TR-N","11","Light Thunderstorm"];
obsMappingArray[130]=["TR--N","11","Light Thunderstorm"];
obsMappingArray[131]=["TRN","11","Thunderstorm"];
obsMappingArray[132]=["TRW+","11","Heavy Thundershower"];
obsMappingArray[133]=["TRW+N","11","Heavy Thundershower"];
obsMappingArray[134]=["TRW-","7","Light Thundershower"];
obsMappingArray[135]=["TRW--","7","Light Thundershower"];
obsMappingArray[136]=["TRW","7","Thundershower"];
obsMappingArray[137]=["TRW-N","24","Light Thundershower"];
obsMappingArray[138]=["TRW--N","24","Light Thundershower"];
obsMappingArray[139]=["TRWN","7","Thundershower"];
obsMappingArray[140]=["X","8","Sky Obscured"];
obsMappingArray[141]=["XN","8","Sky Obscured"];
obsMappingArray[142]=["ZF","17","Ice Fog"];
obsMappingArray[143]=["ZFN","17","Ice Fog"];
obsMappingArray[144]=["ZL+","5","Heavy Freezing Drizzle"];
obsMappingArray[145]=["ZL+N","5","Heavy Freezing Drizzle"];
obsMappingArray[146]=["ZL","5","Freezing Drizzle"];
obsMappingArray[147]=["ZL-","5","Light Freezing Drizzle"];
obsMappingArray[148]=["ZL--","5","Light Freezing Drizzle"];
obsMappingArray[149]=["ZLN","5","Freezing Drizzle"];
obsMappingArray[150]=["ZL-N","5","Light Freezing Drizzle"];
obsMappingArray[151]=["ZL--N","5","Light Freezing Drizzle"];
obsMappingArray[152]=["ZR+","5","Heavy Freezing Rain"];
obsMappingArray[153]=["ZR-","5","Light Freezing Rain"];
obsMappingArray[154]=["ZR--","5","Light Freezing Rain"];
obsMappingArray[155]=["ZR","5","Freezing Rain"];
obsMappingArray[156]=["ZR-N","5","Light Freezing Rain"];
obsMappingArray[157]=["ZR--N","5","Light Freezing Rain"];
obsMappingArray[158]=["","27",""];

	
	var locName = System.Gadget.Settings.read("WElastSearch");
	
	setLocation(locName);

	var cc = xmlData.getElementsByTagName("./Observation").item(0);


	var current = cc.getAttribute('wxIcon');
	for(i=0; i < obsMappingArray.length; i++) {
		if(obsMappingArray[i][0]==current) {
			current = obsMappingArray[i][2];
			currenticon = obsMappingArray[i][1];
			if (currenticon==27 || currenticon==undefined) currenticon = 0;
		}
	}

	
	var timeupdate = cc.getAttribute('dtobs');						//time last update
	lasttimeupdate24full = timeupdate.slice(timeupdate.lastIndexOf(" ") + 1, timeupdate.length);
	lasttimeupdate24full = TimeTo24Convert(lasttimeupdate24full);
	if ((System.Gadget.Settings.read("showLastTimeUpdate")) != 1 || timeupdate == "N/A" || timeupdate == "") TimeLastUpdate.innerText = "";
	else TimeLastUpdate.innerText = lasttimeupdate24full;
	

	var temp = cc.getAttribute('temp');							//actual temperature
	if (System.Gadget.Settings.read("tunits") == "m") {temp = temp; var TemperatureUnits = "C";}
	if (System.Gadget.Settings.read("tunits") == "f") {temp = (temp*(9/5) + 32).toFixed(0); var TemperatureUnits = "F";}
	if (temp == 'N/A' || temp == '') TempSpan.innerText = lng_nodata;
	else TempSpan.innerHTML = temp + "&deg;" + lng_Units[TemperatureUnits];


	var flik = cc.getAttribute('feelsLike');						//feels like temperature
	if (System.Gadget.Settings.read("tunits") == "m") {flik = flik;}
	if (System.Gadget.Settings.read("tunits") == "f") {flik = (flik*(9/5) + 32).toFixed(0);}
	FlikCapt = lng_Stats["flik"];
	FlikSpan = flik + "&deg;" + lng_Units[TemperatureUnits];
	if (flik == 'N/A' || flik == '') FlikSpan = lng_nodata;
	parametrsArray.push({"name":"flik", "capt":FlikCapt, "span":FlikSpan});


	var wind = cc.getAttribute('wSpeed');
	if (wind == 'calm' || wind == '') wind = 0;
	if (System.Gadget.Settings.read("sunits") == "ms") {windSpeed = (wind*0.277777778).toFixed(0); var SpeedUnits = "m/s";}
	if (System.Gadget.Settings.read("sunits") == "km") {windSpeed = wind; var SpeedUnits = "km/h";}
	if (System.Gadget.Settings.read("sunits") == "mp") {windSpeed = (wind*0.621371192).toFixed(0); var SpeedUnits = "mph";}

	var windDirection = cc.getAttribute('wDir');
	var WindDirectionSpan = winddirection_Stats[windDirection];
	if (WindDirectionSpan == undefined) WindDirectionSpan = lng_nodata;

	WindCapt = lng_Stats["wind"] + "[" + WindDirectionSpan + "]";
	WindSpan = windSpeed + lng_Units[SpeedUnits];
	if (wind == 'N/A' || wind == 'calm' || wind == 'CALM' || wind == '') WindSpan = lng_nodata;
	parametrsArray.push({"name":"wind", "capt":WindCapt, "span":WindSpan});


	var humidity = cc.getAttribute('humidity');
	HumidityCapt = lng_Stats["humidity"];
	HumiditySpan = humidity + "%";
	if (humidity == 'N/A') HumiditySpan = lng_nodata;
	parametrsArray.push({"name":"humidity", "capt":HumidityCapt, "span":HumiditySpan});



	var PressureTrendArray = {
		0: "N/A",
		1: "rising",
		2: "falling",
		3: "steady"
	}

	var pressure_str = cc.getAttribute('press');
	var pressuretrend = cc.getAttribute('pressTend');
	if (System.Gadget.Settings.read("punits") == "mm") {pressure = (pressure_str*10 * 0.750062).toFixed(0); var PressureUnits = "mm";}
	if (System.Gadget.Settings.read("punits") == "mb") {pressure = (pressure_str*10).toFixed(1); var PressureUnits = "mb";}
	if (System.Gadget.Settings.read("punits") == "in") {pressure = (pressure_str*10 * 0.02952998).toFixed(2); var PressureUnits = "in";}
	if (System.Gadget.Settings.read("punits") == "kpa") {pressure = pressure_str; var PressureUnits = "kPa";}
	PressureCapt = lng_Stats["pressure"];
	PressureSpan = pressure + lng_Units[PressureUnits];
	if (pressure_str == 'N/A') PressureSpan = lng_nodata;
	parametrsArray.push({"name":"pressure", "capt":PressureCapt, "span":PressureSpan});

	PressuretrendCapt = lng_Stats["pressuretrend"];
	PressuretrendSpan = pressure_Stats[PressureTrendArray[pressuretrend]];
	if (PressuretrendSpan == undefined) PressuretrendSpan = lng_nodata;
	parametrsArray.push({"name":"pressuretrend", "capt":PressuretrendCapt, "span":PressuretrendSpan});
	
	
	setOptionsSettings(parametrsArray);
	


	//Check day or night
	if (currenticon <= 17 && DateToMinutesConvert(lasttimeupdate24full) >= 300 && DateToMinutesConvert(lasttimeupdate24full) <= 1110)
		daytime = "Day";
	else
		{daytime = "Night"; daytime_back = ""; currentImgMoon.style.display = "block"; currentImgMoon.src = "images/" + System.Gadget.Settings.read('Skin') + "/Night/moon/waning_gibbous.png";}

	if (daytime == "Day") 
	{
		
		if (current == "Clear" || current == "Sunny" || current == "Mostly Sunny" || current == "Fair" || current == "Fair and Windy" || current == "Dust" || current == "Ice Crystals" || current == "Partly Cloudy" || current == "Partly Sunny" || current == "Mostly Cloudy" || current == "Partly Cloudy and Windy" || current == "Scattered Clouds" || current == "A few clouds")
			{
				daytime_back = "blue";
			}
		else
			{
				daytime_back = "grey";
			}
		currentImg.style.display = "block";
		currentImgMoon.style.display = "none";
	}
	




	
	




	currentImg.src = "images/" + System.Gadget.Settings.read('Skin') + "/" + daytime + "/" + WEGetCondImage(current);

	if (daytime == "Night" && img == "undefined.png") img = "clear.png";

	if (daytime == "Night" && (img == "partcloudy.png" || img == "cloudy.png" || img == "mostcloudy.png" || img == "clear.png")) {

	var moon_img = {
		New: "new.png",
		"Waxing Crescent": "waxing_crescent.png",
		"First Quarter": "first_quater.png",
		"Waxing Gibbous": "waxing_gibbous.png",
		Full: "full.png",
		"Waning Gibbous": "waning_gibbous.png",
		"Last Quarter": "last_quater.png",
		"Waning Crescent": "waning_crescent.png",
		Darkened: "new.png"
		};

		var moonphase = computePhaseOfMoon(new Date().getFullYear(), new Date().getMonth()+1, new Date().getDate());
		currentImgMoon.src = "images/" + System.Gadget.Settings.read('Skin') + "/Night/moon/" + moon_img[moonphase];
		if (img != "clear.png") {currentImg.style.display = "block";}
		else currentImg.style.display = "none";
	}
	else currentImgMoon.style.display = "none";

	

	

	if (lng_WeatherEyeStatus[current] != undefined) current = lng_WeatherEyeStatus[current];
		while (current.length > 19) {
	 		current = current.slice(0, current.lastIndexOf(" "));
			lastsymbol = current.substring(current.lastIndexOf(" ") + 1, current.length);
			if (lastsymbol.length == 1 || lastsymbol == 'and') current = current.slice(0, current.lastIndexOf(" "));
		}
	CondSpan.innerText = current;



	//alert module
	if (xmlData.getElementsByTagName("./Warning").item(0)) {
		var WarningArr = xmlData.getElementsByTagName("./Warning").item(0);
		var alert1 = WarningArr.getElementsByTagName("./Event").item(0);
		CondSpan.innerHTML = "<MARQUEE WIDTH='115' SCROLLDELAY='70' SCROLLAMOUNT='2'><font color='red'><b>" + alert1.getAttribute('name') + "</b></font></MARQUEE>";
	}

	
	
	if (xmlData.getElementsByTagName("./ShortTerm").item(0))
	var ShortTermForecast = xmlData.getElementsByTagName("./ShortTerm").item(0);
	else var ShortTermForecast = "";
	var LongTermForecast = xmlData.getElementsByTagName("./LongTerm").item(0);

	DayOfLastCurrentUpdate = timeupdate.slice(timeupdate.lastIndexOf("/") + 1, timeupdate.lastIndexOf(" "));
	if (LongTermForecast) {
		DayOfLastForecastUpdate = LongTermForecast.getAttribute('dtfx');
		DayOfLastForecastUpdate = DayOfLastForecastUpdate.slice(DayOfLastForecastUpdate.lastIndexOf("/") + 1, DayOfLastForecastUpdate.lastIndexOf(" "));
		WEFillForecast(LongTermForecast,ShortTermForecast);
	}

	
	

		
	redrawGadget();
		
}




////////////////////



function WEGetCondImage(condition)
{
	img="undefined.png";

	if ((condition.search(/Cloudy/) > -1) || (condition.search(/Clouds/) > -1) || (condition.search(/Variable/) > -1) || (condition.search(/Overcast/) > -1))
		img="cloudy.png";

	if (condition.search(/Rain/) > -1)
		img="rain.png";

	if (condition.search(/Hail/) > -1)
		img="hail.png";

	if ((condition.search(/Sunny/) > -1) || (condition.search(/Clear/) > -1))
		img="clear.png";

	if (condition.search(/Mostly Sunny/) > -1)
		img="mostsunny.png";

	if (condition.search(/Dust/) > -1)
		img="dusthaze.png";

	if ((condition.search(/Fog/) > -1) || (condition.search(/Mist/) > -1) || (condition.search(/Haze/) > -1)) 
		img="fog.png";

	if (condition.search(/Smoke/) > -1)
		img="smoke.png";

	if ((condition.search(/Snow/i) > -1) || (condition.search(/snowshower/i) > -1) || (condition.search(/Snow Shower/i) > -1))
		img="snow.png";

	if ((condition.search(/Thunder/) > -1) || (condition.search(/T-Storm/) > -1))
		img="thunderstorm.png";

	if ((condition.search(/Fair/) > -1) || (condition.search(/Partly Cloudy/) > -1) || (condition.search(/Partly Sunny/) > -1) || (condition.search(/Partly Clear/) > -1) || (condition.search(/A few clouds/) > -1))
		img="partcloudy.png";

	if (condition.search(/Mostly Cloudy/) > -1)
		img="mostcloudy.png";

	if ((condition.search(/Light Rain/) > -1) || (condition.search(/Shower/) > -1) || (condition.search(/Drizzle/) > -1) || (condition.search(/rainshower/) > -1))
		img="lightrain.png";

	if (((condition.search(/Rain/i) > -1) && (condition.search(/Snow/) > -1)) || (condition.search(/Sleet/) > -1))
		img="rainandsnow.png";

	if ((condition.search(/Snow/) > -1) && (condition.search(/Light/) > -1) || (condition.search(/Flurries/) > -1))
		img="lightsnow.png";

	if (condition.search(/Ice/) > -1)
		img="ice.png";

	return img;
}


///////////////////////



function WEFillForecast(LongTermForecast,ShortTermForecast)
{
	
var lng_Month_Number = {
	"1": "January",
	"2": "February",
	"3": "March",
	"4": "April",
	"5": "May",
	"6": "June",
	"7": "July",
	"8": "August",
	"9": "September",
	"10": "October",
	"11": "November",
	"12": "December"
};

var lng_DayOfWeek_Number = {
	0: "Sunday",
	1: "Monday",
	2: "Tuesday",
	3: "Wednesday",
	4: "Thursday",
	5: "Friday",
	6: "Saturday",
	7: "Sunday"
};


var fxMappingArray = {
	"A": "Showers",
	"A-": "Showers",
	"A+": "Showers",
	"A+N": "Showers",
	"AN": "Showers",
	"A-N": "Showers",
	"B": "Variably Cloudy",
	"B-": "Partly Clear",
	"B+": "Partly Sunny",
	"B+N": "Partly Clear",
	"BN": "Variable",
	"B-N": "Partly Clear",
	"C": "Sunny",
	"C-": "Sunny",
	"CN": "Clear",
	"C-N": "Clear",
	"F": "Fog",
	"F-": "Fog",
	"F+": "Fog",
	"F+N": "Fog",
	"FN": "Fog",
	"F-N": "Fog",
	"G": "Snow and Rain",
	"G-": "Snow to Rain",
	"G+": "Wet Snow",
	"G+N": "Wet Snow",
	"GN": "Snow and Rain",
	"G-N": "Snow to Rain",
	"H": "Hazy",
	"H+": "Humid",
	"H+N": "Humid",
	"HN": "Hazy",
	"I": "Rain and Snow",
	"I-": "Rain or Snow",
	"I+": "Rain to Snow",
	"I+N": "Rain to Snow",
	"IN": "Rain and Snow",
	"I-N": "Rain or Snow",
	"J": "Windy",
	"J+": "Windy",
	"J+N": "Windy",
	"JN": "Windy",
	"K": "Smoke",
	"K-": "Ice Fog",
	"K+": "Blowing Dust",
	"K+N": "Blowing Dust",
	"KN": "Smoke",
	"K-N": "Ice Fog",
	"L": "Showers",
	"L-": "Drizzle",
	"L+": "Rain",
	"L+N": "Rain",
	"LN": "Showers",
	"L-N": "Drizzle",
	"M": "Rain",
	"M+": "Wet Flurries",
	"M+N": "Wet Flurries",
	"MN": "Rain",
	"N": "Flurries",
	"N-": "Flurries",
	"N+": "Snow-Squalls",
	"N+N": "Snow-Squalls",
	"NN": "Flurries",
	"N-N": "Flurries",
	"O": "Cloudy",
	"O-": "Cloudy",
	"ON": "Cloudy",
	"O-N": "Cloudy",
	"P": "Blowing Snow",
	"P-": "Drifting Snow",
	"P+": "Flurries",
	"P+N": "Flurries",
	"PN": "Blowing Snow",
	"P-N": "Drifting Snow",
	"Q": "Thunder-Storms",
	"Q-": "Thunder-Storms",
	"Q+": "Thunder-Storms",
	"Q+N": "Thunder-Storms",
	"QN": "Thunder-Storms",
	"Q-N": "Thunder-Storms",
	"R": "Rain",
	"R-": "Rain",
	"R+": "Rain",
	"R+N": "Rain",
	"RN": "Rain",
	"R-N": "Rain",
	"S": "Mainly Sunny",
	"S-": "Mainly Clear",
	"S+": "Partly Cloudy",
	"S+N": "Partly Cloudy",
	"SN": "Clear",
	"S-N": "Clear",
	"T": "Thunder-Showers",
	"T-": "Thunder-Showers",
	"T+": "Thunder-Showers",
	"T+N": "Thunder-Showers",
	"TN": "Thunder-Showers",
	"T-N": "Thunder-Showers",
	"U": "Partly Cloudy",
	"U-": "Partly Cloudy",
	"U+": "Partly Cloudy",
	"U+N": "Partly Cloudy",
	"UN": "Partly Cloudy",
	"U-N": "Partly Cloudy",
	"V": "Flurries",
	"V-": "Flurries",
	"V+": "1 to 3cm snow",
	"V+N": "1 to 3cm snow",
	"VN": "Flurries",
	"V-N": "Flurries",
	"W": "Light Snow",
	"W-": "3 to 5cm snow",
	"W+": "5 to 10cm snow",
	"W+N": "5 to 10cm snow",
	"WN": "Light Snow",
	"W-N": "3 to 5cm snow",
	"Y": "Snow",
	"Y-": "10+cm snow",
	"Y+": "Blizzard",
	"Y+N": "Blizzard",
	"YN": "Snow",
	"Y-N": "10+cm snow",
	"Z": "Freezing Rain",
	"Z-": "Freezing Drizzle",
	"Z+": "Ice Pellets",
	"Z+N": "Ice Pellets",
	"ZN": "Freezing Rain",
	"Z-N": "Freezing Drizzle",
	"": "N/A"
};


var a = 1;
totalFCDays = 0;

if (ShortTermForecast) {
	
	for (var i = 0; i <= 3; i++) {
		
		ForecastDay = ShortTermForecast.getElementsByTagName('Period').item(i);
		var high = ForecastDay.getAttribute('temp');
		var pop = ForecastDay.getAttribute("pop");

		if (System.Gadget.Settings.read("tunits") == "f") {high = (high*(9/5) + 32).toFixed(0);}
	
		if (high != "N/A")
			high+="&deg;";
		else	high = "??&deg;";


		var day = ForecastDay.getAttribute("dow");
		daylng = lng_DayOfWeek[lng_DayOfWeek_Number[day]];

		
		var perno = ForecastDay.getAttribute("perno");
		if (perno == 2) daylng = lng_DayOfWeek[lng_DayOfWeek_Number[day-1]];
		var date = periodsArray[perno];
				
				
		var condition = ForecastDay.getAttribute("wxIcon");
		if (condition == 'N/A' && document.getElementById("dayImg1").src) return;
		condition = fxMappingArray[condition];

		var conditionIMG = WEGetCondImage(condition)
		if (perno == 2 && (conditionIMG == 'clear.png' || conditionIMG == 'mostcloudy.png' || conditionIMG == 'partcloudy.png')) conditionIMG = conditionIMG.replace(".png", "_night.png");

		
			
		document.getElementById("dayName" + a).innerText = daylng; 
		document.getElementById("date" + a).innerText = date; 
		document.getElementById("dayHi" + a).innerHTML = high;
		document.getElementById("separator"  + a).innerText = "/";
		document.getElementById("dayLow" + a).innerHTML = pop + "%"; 
		document.getElementById("dayImg" + a).src = "images/" + System.Gadget.Settings.read('Skin') + "/Forecast/" + conditionIMG;
		if (lng_WeatherStatus[condition] != undefined) condition = lng_WeatherStatus[condition];
		if (System.Gadget.Settings.read('showFlyoutForecast') == "1") document.getElementById("dayImg" + a).alt = condition;
		a = a + 1;
		totalFCDays++;
	}

}



	ForecastDays = LongTermForecast.getElementsByTagName('Period');

		
	for (var i = 0; i < ForecastDays.length; i++) {

	if ((System.Gadget.Settings.read("showForecastToday")) != 1 || DateToMinutesConvert(lasttimeupdate24full) >= 900)
			{
				if (!ShortTermForecast && System.Gadget.Settings.read("showDayNameForecast") == 0) dayName1.innerText = lng_Tomorrow;
				
			}
 		else
			{
				if (!ShortTermForecast && System.Gadget.Settings.read("showDayNameForecast") == 0) {
					dayName1.innerText = lng_Today;
					dayName2.innerText = lng_Tomorrow;
				}
			}
		
		ForecastDay = ForecastDays[i];
		var high = ForecastDay.getAttribute('tempMax');
		var low = ForecastDay.getAttribute("tempMin");

		if (high == "" && low == "") break;

		if (System.Gadget.Settings.read("tunits") == "f") {high = (high*(9/5) + 32).toFixed(0); low = (low*(9/5) + 32).toFixed(0);}
	
		if (high != "N/A")
			high+="&deg;";
		else	high = "??&deg;";


		if (low != "N/A")
			low +="&deg;";
		else	low = "??&deg;";

		var day = ForecastDay.getAttribute("dow");
		day = lng_DayOfWeek[lng_DayOfWeek_Number[day]];

		
		var date = ForecastDay.getAttribute("day");
		var month = ForecastDay.getAttribute("month");
		date = date + " " + lng_Month_full[lng_Month_Number[month]];

				
		var condition = ForecastDay.getAttribute("wxIcon");
		if (condition == 'N/A' && document.getElementById("dayImg1").src) return;
		condition = fxMappingArray[condition];
			
		document.getElementById("dayName" + a).innerText = day; 
		document.getElementById("date" + a).innerText = date; 
		document.getElementById("dayHi" + a).innerHTML = high;
		document.getElementById("separator"  + a).innerText = "/";
		document.getElementById("dayLow" + a).innerHTML = low; 
		document.getElementById("dayImg" + a).src = "images/" + System.Gadget.Settings.read('Skin') + "/Forecast/" + WEGetCondImage(condition);
		if (lng_WeatherEyeStatus[condition] != undefined) condition = lng_WeatherEyeStatus[condition];
		if (System.Gadget.Settings.read('showFlyoutForecast') == "1") document.getElementById("dayImg" + a).alt = condition;
		a++;
		totalFCDays++;
	}



}



