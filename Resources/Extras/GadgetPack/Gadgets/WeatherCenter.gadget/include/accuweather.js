//	Javascript file for the WeatherCenter gadget
//	(c) 2009
//	WeatherCenter Gadget Team
//	Development: hadj 
//	Graphics: Tex
//	Testing: Digital	
////////////////////////////////////////////////////////////////////////



function parseForecastAccuWeather(Xml2)
{
var parametrsArray = [{"name":"nothing", "capt":"", "span":""}]

		
	var city = Xml2.substring(Xml2.indexOf("<city>") + 6, Xml2.indexOf("</city>"));
	var state = Xml2.substring(Xml2.indexOf("<state>") + 7, Xml2.indexOf("</state>"));
	country = state.substring(state.lastIndexOf(" ") + 1, state.lenght);
	locName = city + ", " + country;
	setLocation(locName);

	
	var current = Xml2.substring(Xml2.indexOf("<weathertext>") + 13, Xml2.indexOf("</weathertext>"));
	var icon = Xml2.substring(Xml2.indexOf("<weathericon>") + 13, Xml2.indexOf("</weathericon>"));
	
	
	time = Xml2.substring(Xml2.indexOf("<time>") + 6, Xml2.indexOf("</time>"));			//time last update
	var sunriseTm = Xml2.substring(Xml2.indexOf("<sun rise=") + 11,  Xml2.indexOf(" ", Xml2.indexOf("<sun rise=") + 11) - 1);
	var sunsetTm = Xml2.substring(Xml2.indexOf("set=") + 5,  Xml2.indexOf("/>", Xml2.indexOf("set=") + 5) - 1);
	checkDayorNight(time, sunriseTm, sunsetTm, current);

	SunriseCapt = lng_Stats["sunrise"];
	SunriseSpan = TimeTo24Convert(sunriseTm);
	if (sunriseTm == 'N/A') SunriseSpan = lng_nodata;
	parametrsArray.push({"name":"sunrise", "capt":SunriseCapt, "span":SunriseSpan});
	
	SunsetCapt = lng_Stats["sunset"];
	SunsetSpan = TimeTo24Convert(sunsetTm);
	if (sunsetTm == 'N/A') SunsetSpan = lng_nodata;
	parametrsArray.push({"name":"sunset", "capt":SunsetCapt, "span":SunsetSpan});
	

	var lasttimeupdate24full = time;
	if (lasttimeupdate24full.length < 5) lasttimeupdate24full = "0" + lasttimeupdate24full;			//time last update
	if ((System.Gadget.Settings.read("showLastTimeUpdate")) != 1) TimeLastUpdate.innerText = "";
	else TimeLastUpdate.innerText = lasttimeupdate24full;
	if ((System.Gadget.Settings.read("showLastTimeUpdate")) == 1 ) {
		var NextTimeUpdate = MinutesToTime24Convert(Date24ToMinutesConvert(lasttimeupdate24full) + System.Gadget.Settings.read("updateInterval"));
		document.getElementById("TimeLastUpdate").title = lng_Updating_Time_Text + " " + NextTimeUpdate;
	}
	
				
	var temp = Xml2.substring(Xml2.indexOf("<temperature>") + 13, Xml2.indexOf("</temperature>"));		//actual temperature
	if (System.Gadget.Settings.read("tunits") == "m") {temp = temp; var TemperatureUnits = "C";}
	if (System.Gadget.Settings.read("tunits") == "f") {temp = (temp*(9/5) + 32).toFixed(0); var TemperatureUnits = "F";}
	if (temp == 'N/A') TempSpan.innerText = lng_nodata;
	else TempSpan.innerHTML = temp + "&deg;" + lng_Units[TemperatureUnits];
	
				
	var flik = Xml2.substring(Xml2.indexOf("<realfeel>") + 10, Xml2.indexOf("</realfeel>"));		//feels like temperature
	if (System.Gadget.Settings.read("tunits") == "m") {flik = flik;}
	if (System.Gadget.Settings.read("tunits") == "f") {flik = (flik*(9/5) + 32).toFixed(0);}
	FlikCapt = lng_Stats["flik"];
	FlikSpan = flik + "&deg;" + lng_Units[TemperatureUnits];
	if (flik == 'N/A' || flik > 150 || flik < -150) FlikSpan = lng_nodata;
	parametrsArray.push({"name":"flik", "capt":FlikCapt, "span":FlikSpan});



	var pressure_str = Xml2.substring(Xml2.indexOf("<pressure state=") + 16, Xml2.indexOf("</pressure>"));
	var pressuretrend = pressure_str.substring(1, pressure_str.indexOf(">") - 1);
	var pressure_str = pressure_str.substring(pressure_str.indexOf(">") + 1, pressure_str.lenght);
	pressure_str = pressure_str * 10;
	if (System.Gadget.Settings.read("punits") == "mm") {pressure = (pressure_str * 0.750062).toFixed(0); var PressureUnits = "mm";}
	if (System.Gadget.Settings.read("punits") == "mb") {pressure = pressure_str; var PressureUnits = "mb";}
	if (System.Gadget.Settings.read("punits") == "in") {pressure = (pressure_str * 0.02952998).toFixed(2); var PressureUnits = "in";}
	if (System.Gadget.Settings.read("punits") == "kpa") {pressure = (pressure_str/10).toFixed(2); var PressureUnits = "kPa";}
	PressureCapt = lng_Stats["pressure"];
	PressureSpan = pressure + lng_Units[PressureUnits];
	if (pressure_str == 'N/A') PressureSpan = lng_nodata;
	parametrsArray.push({"name":"pressure", "capt":PressureCapt, "span":PressureSpan});

	PressuretrendCapt = lng_Stats["pressuretrend"];
	PressuretrendSpan = pressure_Stats[pressuretrend.toLowerCase()];
	if (PressuretrendSpan == undefined) PressuretrendSpan = lng_nodata;
	parametrsArray.push({"name":"pressuretrend", "capt":PressuretrendCapt, "span":PressuretrendSpan});


	var windSpeed = Xml2.substring(Xml2.indexOf("<windspeed>") + 11, Xml2.indexOf("</windspeed>"));
	if (System.Gadget.Settings.read("sunits") == "ms") {windSpeed = windSpeed; var SpeedUnits = "m/s";}
		if (System.Gadget.Settings.read("sunits") == "km") {windSpeed = (windSpeed*3.6).toFixed(0); var SpeedUnits = "km/h";}
		if (System.Gadget.Settings.read("sunits") == "mp") {windSpeed = (windSpeed*2.23693629).toFixed(0); var SpeedUnits = "mph";}
	var windDirection = Xml2.substring(Xml2.indexOf("<winddirection>") + 15, Xml2.indexOf("</winddirection>"));
	var WindDirectionSpan = winddirection_Stats[windDirection];
	if (WindDirectionSpan == undefined) var WindCapt = lng_Stats["wind"];
	else var WindCapt = lng_Stats["wind"] + "[" + WindDirectionSpan + "]";
	WindSpan = windSpeed + lng_Units[SpeedUnits];
	parametrsArray.push({"name":"wind", "capt":WindCapt, "span":WindSpan});


	var windGust = Xml2.substring(Xml2.indexOf("<windgusts>") + 11, Xml2.indexOf("</windgusts>"));
	if (System.Gadget.Settings.read("sunits") == "ms") {windGust = windGust;}
	if (System.Gadget.Settings.read("sunits") == "km") {windGust = (windGust*3.6).toFixed(0);}
	if (System.Gadget.Settings.read("sunits") == "mp") {windGust = (windGust*2.23693629).toFixed(0);}
	GustCapt = lng_Stats["gust"];
	GustSpan = windGust + lng_Units[SpeedUnits];
	parametrsArray.push({"name":"gust", "capt":GustCapt, "span":GustSpan});

	
	var visibility = Xml2.substring(Xml2.indexOf("<visibility>") + 12, Xml2.indexOf("</visibility>"));
	if (System.Gadget.Settings.read("dunits") == "km") {visibility = visibility; var DistanceUnits = "km";}
	if (System.Gadget.Settings.read("dunits") == "mi") {visibility = (visibility*0.621371192).toFixed(1); var DistanceUnits = "mi";}
	VisibilityCapt = lng_Stats["visibility"];
	VisibilitySpan = visibility + lng_Units[DistanceUnits];
	parametrsArray.push({"name":"visibility", "capt":VisibilityCapt, "span":VisibilitySpan});
        
	
	var humidity = Xml2.substring(Xml2.indexOf("<humidity>") + 10, Xml2.indexOf("</humidity>"));
	HumidityCapt = lng_Stats["humidity"];
	HumiditySpan = humidity.slice(0, humidity.indexOf(".")) + "%";
	parametrsArray.push({"name":"humidity", "capt":HumidityCapt, "span":HumiditySpan});
	

	var UVIndex = Xml2.substring(Xml2.indexOf('uvindex index="') + 15, Xml2.indexOf((">"), Xml2.indexOf('uvindex index="') + 15) - 1);
	UVIndexCapt = lng_Stats["uvindex"];
	UVIndexSpan = UVIndex;
	parametrsArray.push({"name":"uvindex", "capt":UVIndexCapt, "span":UVIndexSpan});

	
	var UVLevel = Xml2.substring(Xml2.indexOf((">"), Xml2.indexOf('uvindex index="') + 15) + 1, Xml2.indexOf("</uvindex>"));
	UVLevelCapt = lng_Stats["uvlevel"];
	UVLevelSpan = uv_Stats[UVLevel];
	parametrsArray.push({"name":"uvlevel", "capt":UVLevelCapt, "span":UVLevelSpan});

	
	var moonterminator = Xml2.substring(Xml2.indexOf('text', Xml2.indexOf('<phase date="')) + 6, Xml2.indexOf((">"), Xml2.indexOf('<phase date="')) - 2);
	if (moonterminator == "First" || moonterminator == "Last") moonterminator = moonterminator + " Quarter";
	MoonCapt = lng_Stats["moonterminator"];
	MoonSpan = moon_Stats[moonterminator];
	if (MoonSpan == undefined) MoonSpan = lng_nodata;
	parametrsArray.push({"name":"moonterminator", "capt":MoonCapt, "span":MoonSpan});

	
			
	var latitude = Xml2.substring(Xml2.indexOf("<lat>") + 5, Xml2.indexOf("</lat>"));		//latitude
	LatitudeCapt = lng_Stats["latitude"];
	LatitudeSpan = latitude;
	if (latitude == 'N/A') LatitudeSpan = lng_nodata;
	parametrsArray.push({"name":"latitude", "capt":LatitudeCapt, "span":LatitudeSpan});


		
	var longitude = Xml2.substring(Xml2.indexOf("<lon>") + 5, Xml2.indexOf("</lon>"));		//longitude
	LongitudeCapt = lng_Stats["longitude"];
	LongitudeSpan = longitude;
	if (longitude == 'N/A') LongitudeSpan = lng_nodata;
	parametrsArray.push({"name":"longitude", "capt":LongitudeCapt, "span":LongitudeSpan});

	
	var moonriseTm = Xml2.substring(Xml2.indexOf("<moon rise=") + 12,  Xml2.indexOf(" ", Xml2.indexOf("<moon rise=") + 12) - 1);
	MoonriseCapt = lng_Stats["moonrise"];
	MoonriseSpan = TimeTo24Convert(moonriseTm);
	parametrsArray.push({"name":"moonrise", "capt":MoonriseCapt, "span":MoonriseSpan});

	var moonsetTm = Xml2.substring(Xml2.indexOf("set=", Xml2.indexOf("<moon rise=")) + 5,  Xml2.indexOf("/>", Xml2.indexOf("<moon rise=") + 5) - 1);	
	MoonsetCapt = lng_Stats["moonset"];
	MoonsetSpan = TimeTo24Convert(moonsetTm);
	parametrsArray.push({"name":"moonset", "capt":MoonsetCapt, "span":MoonsetSpan});

	
	var precipitation = Xml2.substring(Xml2.indexOf("<precip>") + 8, Xml2.indexOf("</precip>"));
	precipitation = precipitation*10;
	//var PrecUnits = Xml2.substring(Xml2.indexOf("<prec>") + 6, Xml2.indexOf("</prec>"));
	PrecipitationCapt = lng_Stats["precipitation"];
	PrecipitationSpan = precipitation + lng_Units["mm"];
	if (precipitation == NaN) PrecipitationSpan = lng_nodata;
	parametrsArray.push({"name":"precipitation", "capt":PrecipitationCapt, "span":PrecipitationSpan});


	var thunderstorm  = Xml2.substring(Xml2.indexOf("<tstormprob>") + 12, Xml2.indexOf("</tstormprob>"));
	ThunderCapt = lng_Stats["thunderstorm"];
	ThunderSpan = thunderstorm + "%";
	parametrsArray.push({"name":"thunderstorm", "capt":ThunderCapt, "span":ThunderSpan});


	var airquality  = Xml2.substring(Xml2.indexOf("<airquality>") + 12, Xml2.indexOf("</airquality>"));
	AirqualityCapt = lng_Stats["airquality"];
	AirqualitySpan = airquality;
	parametrsArray.push({"name":"airquality", "capt":AirqualityCapt, "span":AirqualitySpan});

	
	setOptionsSettings(parametrsArray);



	currentImg.src = "images/" + System.Gadget.Settings.read('Skin') + "/" + daytime + "/" + AccuGetCondImage(current);

	if (daytime == "Night" && (img == "partcloudy.png" || img == "cloudy.png" || img == "mostcloudy.png" || img == "clear.png")) {

		var moon_img = {
			New: "new.png",
			"Waxing Crescent": "waxing_crescent.png",
			"First": "first_quater.png",
			"Waxing Gibbous": "waxing_gibbous.png",
			Full: "full.png",
			"Waning Gibbous": "waning_gibbous.png",
			"Last": "last_quater.png",
			"Waning Crescent": "waning_crescent.png",
			Darkened: "new.png"
		};

		var moonphase_str = Xml2.substring(Xml2.indexOf("<moon>") + 6, Xml2.indexOf("</moon>"));
		var moonphase = moonphase_str.substring(moonphase_str.indexOf("<phase date=") + 12, moonphase_str.indexOf("</phase>"));
		moonphase = moonphase.substring(moonphase.indexOf('text=') + 6, moonphase.indexOf('>') - 2);
		currentImgMoon.src = "images/" + System.Gadget.Settings.read('Skin') + "/Night/moon/" + moon_img[moonphase];
		if (System.Gadget.Settings.read('showFlyoutForecast') == "1") {
			if (moonterminator == "First" || moonterminator == "Last") moonterminator = moonterminator + " Quarter";
			currentImg.alt = moon_Stats_full[moonterminator];
			currentImgMoon.alt = moon_Stats_full[moonterminator];
		}
		if (img != "clear.png") {currentImg.style.display = "block";}
		else currentImg.style.display = "none";
	}
	else {
		currentImgMoon.style.display = "none";
		currentImg.alt = "";
	}


	

	

	
	if (lng_AccuWeatherStatus[current] != undefined) current = lng_AccuWeatherStatus[current];
		while (current.length > 19) {
	 		current = current.slice(0, current.lastIndexOf(" "));
			lastsymbol = current.substring(current.lastIndexOf(" ") + 1, current.length);
			if (lastsymbol.length == 1 || lastsymbol == 'and') current = current.slice(0, current.lastIndexOf(" "));
		}
	



	//alert module
	var isactive = Xml2.substring(Xml2.indexOf('isactive="') + 10, Xml2.indexOf('isactive="') + 11);
	if (isactive == 1 && (CondSpan.innerHTML).search(/Weather Warning/i) == -1)
		{
			var watchwarnareas = Xml2.substring(Xml2.indexOf("<watchwarnareas") + 15, Xml2.indexOf("</watchwarnareas>"));
			var watchwarnareasUrl = watchwarnareas.substring(watchwarnareas.indexOf("<url>") + 5, watchwarnareas.indexOf("</url>"));			
			CondSpan.innerHTML = "<a href='" + watchwarnareasUrl + "' style='text-decoration: none;'><font color='red'><b>" + "Weather Warning" + "</b></font></a>";
		}
	else CondSpan.innerText = current;


	
	AccuFillForecast(Xml2.substring(Xml2.indexOf("<forecast>") + 10, Xml2.indexOf("</forecast>")));

		
	redrawGadget();
		
}




////////////////////



function AccuGetCondImage(condition)
{
	img="undefined.png";

	if (condition == 1)
		img="mostsunny.png";

	if (condition >= 2 && condition <= 4)
		img="partcloudy.png";
	
	if (condition == 5 || condition == 30)
		img="clear.png";

	if (condition == 6)
		img="mostcloudy.png";
	
	if (condition >= 7 && condition <= 11 || condition == 31)
		img="cloudy.png";

	if (condition >= 12 && condition <= 14)
		img="lightrain.png";
	
	if (condition >= 15 && condition <= 17)
		img="thunderstorm.png";

	if (condition == 18 || condition == 25)
		img="rain.png";
	
	if (condition >= 19 && condition <= 21)
		img="lightsnow.png";

	if (condition >= 22 && condition <= 23)
		img="snow.png";

	if (condition == 24 || condition == 26 || condition == 29)
		img="rainandsnow.png";

	if ((condition.search(/Cloudy/i) > -1) || (condition.search(/Clouds/i) > -1))
		img="cloudy.png";

	if (condition.search(/Rain/i) > -1)
		img="rain.png";

	if (condition.search(/Hail/i) > -1)
		img="hail.png";

	if ((condition.search(/Sunny/i) > -1) || (condition.search(/Clear/i) > -1))
		img="clear.png";

	if (condition.search(/Mostly Sunny/i) > -1)
		img="mostsunny.png";

	if (condition.search(/Dust/i) > -1)
		img="dusthaze.png";

	if ((condition.search(/Fog/i) > -1) || (condition.search(/Mist/i) > -1) || (condition.search(/Haze/i) > -1)) 
		img="fog.png";

	if (condition.search(/Smoke/i) > -1)
		img="smoke.png";

	if ((condition.search(/Snow/i) > -1) || (condition.search(/Snowshower/i) > -1) || (condition.search(/Snow Shower/i) > -1))
		img="snow.png";

	if ((condition.search(/Thunder/i) > -1) || (condition.search(/T-Storm/i) > -1))
		img="thunderstorm.png";

	if ((condition.search(/Fair/i) > -1) || (condition.search(/Partly Cloudy/i) > -1) || (condition.search(/Partly Sunny/i) > -1) || (condition.search(/Partly Clear/i) > -1) || (condition.search(/Some Clouds/i) > -1))
		img="partcloudy.png";

	if (condition.search(/Mostly Cloudy/i) > -1)
		img="mostcloudy.png";

	if ((condition.search(/Light Rain/) > -1) || (condition.search(/Shower/) > -1) || (condition.search(/Drizzle/) > -1) || (condition.search(/rainshower/) > -1))
		img="lightrain.png";

	if ((condition.search(/Snow/i) > -1)  || (condition.search(/Sleet/i) > -1))
		img="rainandsnow.png";

	if ((condition.search(/Snow/i) > -1) && (condition.search(/Light/i) > -1))
		img="lightsnow.png";

	if (condition.search(/Ice/i) > -1)
		img="ice.png";

	return img;
}


///////////////////////



function AccuFillForecast(XmlData)
{
	var a = 1;
	totalFCDays = 0;

	XmlData = XmlData.split('<day number="');

	for (var i = 1; i < XmlData.length; i++) {
		if ((System.Gadget.Settings.read("showForecastToday")) != 1 || Date24ToMinutesConvert(time) >= 900)
			{
				if (i == 1) i++;
				if (System.Gadget.Settings.read("showDayNameForecast") == 0) dayName1.innerText = lng_Tomorrow;
			}
 		else
			{
				if (System.Gadget.Settings.read("showDayNameForecast") == 0) {
					dayName1.innerText = lng_Today;
					dayName2.innerText = lng_Tomorrow;
				}
			}

		var dayData = XmlData[i];

		var high = dayData.substring(dayData.indexOf("<hightemperature>") + 17, dayData.indexOf("</hightemperature>"));
		var low = dayData.substring(dayData.indexOf("<lowtemperature>") + 16, dayData.indexOf("</lowtemperature>"));
		if (System.Gadget.Settings.read("tunits") == "f") {high = (high*(9/5) + 32).toFixed(0); low = (low*(9/5) + 32).toFixed(0);}
				
		if (high != "N/A")
			high+="&deg;";
		else	high = "??&deg;";
		if (low != "N/A")
			low +="&deg;";
		else	low = "??&deg;";

		var day = dayData.substring(dayData.indexOf('<daycode>') + 9, dayData.indexOf('</daycode>'));
		day = lng_DayOfWeek[day];


		var date = dayData.substring(dayData.indexOf('<obsdate>') + 9, dayData.indexOf('</obsdate>'));
		date = DateUSToEUAccu(date);


		var precip = dayData.substring(dayData.indexOf('<precipamount>') + 14, dayData.indexOf('</precipamount>'));
		precip = (precip*10).toFixed(0);

		var windSpeed = dayData.substring(dayData.indexOf('<windspeed>') + 11, dayData.indexOf('</windspeed>'));
		if (windSpeed == 'calm' || windSpeed == 'N/A' || !windSpeed) windSpeed = 0;
		windSpeed = (windSpeed*1).toFixed(0);
		if (System.Gadget.Settings.read("sunits") == "ms") {windSpeed = windSpeed; var SpeedUnits = "m/s";}
		if (System.Gadget.Settings.read("sunits") == "km") {windSpeed = (windSpeed*3.6).toFixed(0); var SpeedUnits = "km/h";}
		if (System.Gadget.Settings.read("sunits") == "mp") {windSpeed = (windSpeed*2.23693629).toFixed(0); var SpeedUnits = "mph";}
		

		var condition = dayData.substring(dayData.indexOf("<weathericon>") + 13, dayData.indexOf("</weathericon>"));
		var txtshort = dayData.substring(dayData.indexOf("<txtshort>") + 10, dayData.indexOf("</txtshort>"));
				
		document.getElementById("dayName" + a).innerText = day; 
		document.getElementById("date" + a).innerText = date; 
		document.getElementById("dayHi" + a).innerHTML = high;
		document.getElementById("separator"  + a).innerText = "/";
		document.getElementById("dayLow" + a).innerHTML = low; 
		document.getElementById("dayImg" + a).src = "images/" + System.Gadget.Settings.read('Skin') + "/Forecast/" + AccuGetCondImage(condition);
		if (lng_AccuWeatherStatus[txtshort] != undefined) txtshort = lng_AccuWeatherStatus[txtshort];
		if (System.Gadget.Settings.read('showFlyoutForecast') == "1") document.getElementById("dayImg" + a).alt = txtshort + ", " + lng_Stats['precipitation'] + ": " + precip + lng_Units["mm"] + ", " + lng_Stats["wind"] + ": " + windSpeed + lng_Units[SpeedUnits];
		a++;
		totalFCDays++;
	}


}



function DateUSToEUAccu(timeparametr)
{
	var month = timeparametr.slice(0, timeparametr.indexOf("/"));	
	var day = timeparametr.slice(timeparametr.indexOf("/") + 1, timeparametr.lastIndexOf("/"));

	
var lng_Number_Month = {
	'01': "Jan",
	'02': "Feb",
	'03': "Mar",
	'04': "Apr",
	'05': "May",
	'06': "Jun",
	'07': "Jul",
	'08': "Aug",
	'09': "Sep",
	'10': "Oct",
	'11': "Nov",
	'12': "Dec"};

	date = day + " " + lng_Month[lng_Number_Month[month]];
	return date;
}


