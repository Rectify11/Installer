/////////////////////////////////////////////////////////////////////////////////
//
// LOCALIZABLE VARIABLES
//
////////////////////////////////////////////////////////////////////////////////
var L_SHOWMORE_TEXT             = "Más días de previsión";
var L_SHOWLESS_TEXT             = "Menos días de previsión";
var L_FULLMODE_TEXT             = "Mostrar más información";
var L_MINIMODE_TEXT             = "No mostrar información adicional";
var L_REFRESH_TEXT             	= "Actualizar los datos";
var lng_Updating = "Actualizando";
var lng_NoData = "No hay conexión";
var lng_Today = "Hoy";
var lng_Tomorrow = "Mañana";
var lng_Updating_Time_Text = "en la próxima actualización";

Wlng_defLastSearch = "Madrid, Spain";
Wlng_defLocationCode = "SPXX0050";

Alng_defLastSearch = "Madrid, Spain";
Alng_defLocationCode = "EUR|ES|SP013|Madrid|";

WUlng_defLastSearch = "Madrid, Spain";
WUlng_defLocationCode = "Madrid,Spain";

MSNlng_defLastSearch = "Madrid, Spain";
MSNlng_defLocationCode = "wc:SPXX0050";

WElng_defLastSearch = "Madrid, Spain";
WElng_defLocationCode = "ESMX0001";

WBlng_defLastSearch = "Madrid, Spain";
WBlng_defLocationCode = "61692|LEVS";

YAlng_defLastSearch = "Berlin, Germany";
YAlng_defLocationCode = "10385";

GISlng_defLastSearch = "Berlin, Germany";
GISlng_defLocationCode = "10381";

var periodsArray = new Array("","Noche","Amanecer","Mañana","Tarde","Día");

var lng_WeatherStatus = {
	"Light Freezing Rain": "Lluvia Ligera",
	"Light Rain Shower and Windy": "Lluvia Ligera y Viento",
	"Partly Cloudy and Windy": "Parcialmente Nublado y Viento",
	"Showers in the Vicinity": "Lluvia",
	"Thunder in the Vicinity": "Viento",
	"Light Rain with Thunder": "Lluvia Ligera y Viento",
	"Light Snow Grains": "Granizo",
	"Hvy.snowshower": "Nieve"
};

var lng_AccuWeatherStatus = {
	"Lgt.rainshower": "Light Rain"
};

var lng_WundergroundStatus = {
	"light snow blowing snow": "light blowing snow"
};

var lng_MSNStatus = {
	"Fair": "Despejado",
	"Rain": "Lluvioso",
	"Showers / Clear": "Nubes y Claros",
	"Partly Cloudy": "Parcialmente Nublado"
};

var lng_WeatherEyeStatus = {
};

var lng_WeatherBugStatus = {
};

var lng_nodata = "N/A";


var lng_DayOfWeek = {
	Sunday: "Domingo",
	Monday: "Lunes",
	Tuesday: "Martes",
	Wednesday: "Miercoles",
	Thursday: "Jueves",
	Friday: "Viernes",
	Saturday: "Sabado"
};

var lng_Stats = {
	pressure: "Presión",
	pressuretrend: "Presión↑↓",
	wind: "Viento",
	gust: "Velocidad del Viento",
	visibility: "Visibilidad",
	humidity: "Humedad",
	flik: "Feels Like",
	nothing: "Vacio",
	sunrise: "Sol↑",
	sunset: "Sol↓",
	dewpoint: "Rocío",
	uvindex: "Índice UV",
	uvlevel: "Nivel UV",
	moonterminator: "Luna",
	latitude: "Latitud",
	longitude: "Longitud",
	moonrise: "Salida Lunar↑",
	moonset: "Puesta Lunar↓",
	precipitation: "Precipitación",
	thunderstorm: "Tormenta Eléctrica",
	airquality: "Calidad del Aire",
	localtime: "Hora"
};

var pressure_Stats = {
	falling: "<FONT COLOR='#4169E1'>Descendente</FONT>",
	decreasing: "<FONT COLOR='#4169E1'>Descendente</FONT>",
	rising: "<FONT COLOR='#FA8072'>Ascendente</FONT>",
	increasing: "<FONT COLOR='#FA8072'>Ascendente</FONT>",
	steady: "Estable"
};

var uv_Stats = {
	Low: "Bajo",
	Moderate: "Moderado",
	High: "Alto",
	Extreme: "Extremo"
};


var winddirection_Stats = {
	N: "<FONT COLOR='#FA8072'>N</FONT>",
	North: "<FONT COLOR='#FA8072'>N</FONT>",
	NNE: "<FONT COLOR='#FA8072'>NNE</FONT>",
	NE: "<FONT COLOR='#FFD700'>NE</FONT>",
	ENE: "<FONT COLOR='#FFD700'>ENE</FONT>",
	E: "<FONT COLOR='#00FF00'>E</FONT>",
	East: "<FONT COLOR='#00FF00'>E</FONT>",
	ESE: "<FONT COLOR='#00FF00'>ESE</FONT>",
	SE: "<FONT COLOR='#00FF00'>SE</FONT>",
	SSE: "<FONT COLOR='#00FF00'>SSE</FONT>",
	S: "<FONT COLOR='#FF3030'>S</FONT>",
	South: "<FONT COLOR='#FF3030'>S</FONT>",
	SSW: "<FONT COLOR='#FF3030'>SSO</FONT>",
	SW: "<FONT COLOR='#FFD700'>SO</FONT>",
	WSW: "<FONT COLOR='#FFD700'>OSO</FONT>",
	W: "<FONT COLOR='#FFFFFF'>W</FONT>",
	West: "<FONT COLOR='#FFFFFF'>O</FONT>",
	WNW: "<FONT COLOR='#FFFFFF'>ONO</FONT>",
	NW: "<FONT COLOR='#FFFFFF'>NO</FONT>",
	NNW: "<FONT COLOR='#FFFFFF'>NNO</FONT>"
};

var moon_Stats = {
	New: "Nueva",
	"Waxing Crescent": "Cuarto Creciente",
	"First Quarter": "Cuarto Creciente",
	"Waxing Gibbous": "Cuarto Creciente",
	Full: "Llena",
	"Waning Gibbous": "Cuarto Menguante",
	"Last Quarter": "Cuarto Menguante",
	"Waning Crescent": "Cuarto Menguante",
	Darkened: "Nueva"
};

var moon_Stats_full = {
	New: "New",
	"Waxing Crescent": "Waxing Crescent",
	"First Quarter": "First Quarter",
	"Waxing Gibbous": "Waxing Gibbous",
	Full: "Full",
	"Waning Gibbous": "Waning Gibbous",
	"Last Quarter": "Last Quarter",
	"Waning Crescent": "Waning Crescent",
	Darkened: "Darkened"
};


var lng_Units = {
	"C": "C",
	"F": "F",
	"km": "km",
	"km/h": "km/h",
	"m/s": "m/s",
	"mb": "mb",
	"mi": "mi",
	"mph": "mph",
	"in": "in",
	"cm": "cm",
	"mm": "mm",
	"kPa": "kPa"
};





var lng_Countries = {
	"United Kingdom": "United Kingdom",
	"United States": "United States"
};

var lng_Cities = {
	"Moscow": "Moscow"
};

var lng_Month = {
	Jan: "Enero",
	Feb: "Febrero",
	Mar: "Marzo",
	Apr: "Abril",
	May: "Mayo",
	Jun: "Junio",
	Jul: "Julio",
	Aug: "Agosto",
	Sep: "Septiembre",
	Oct: "Octubre",
	Nov: "Noviembre",
	Dec: "Deciembre"
};

var lng_Month_Short = {
	Jan: "Ene",
	Feb: "Feb",
	Mar: "Mar",
	Apr: "Abr",
	May: "May",
	Jun: "Jun",
	Jul: "Jul",
	Aug: "Ago",
	Sep: "Sep",
	Oct: "Oct",
	Nov: "Nov",
	Dec: "Dic"
};
var lng_Month_full = {
	January: "Enero",
	February: "Febrero",
	March: "Marzo",
	April: "Abril",
	May: "Mayo",
	June: "Junio",
	July: "Julio",
	August: "Agosto",
	September: "Septiembre",
	October: "Octubre",
	November: "Noviembre",
	December: "Diciembre"
};

