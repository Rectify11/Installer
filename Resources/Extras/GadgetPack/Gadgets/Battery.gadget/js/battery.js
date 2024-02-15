var _currentBattIcon;
var _currentOnPower;
var _currentCharging;
var _currentPercentString;
var _currentTimeString;
var _wmiInterface;
var _txtPercentage;
var _txtTime;
var _txtPercentageLarge;
//Will hold instance of the GadgetBuilder to load/unload .NET assemblies. See GadgetInterop.js
var _interop;
//Will hold .NET instance of the BatteryGauge.TimeRemaining object.
var _timeRemaining;

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

function Init()
{
  _currentBattIcon = -1;
  _currentOnPower = false;
  _currentCharging = false;
  _currentPercentString = " ";
  _currentTimeString = " ";
  _wmiInterface = GetObject("winmgmts:{impersonationLevel=impersonate}!//./root/cimv2");
  // Initialize the _interop object and acquire the _timeRemaining .NET assembly reference.
  _interop = new GadgetBuilder();
  _interop.Initialize();
  _timeRemaining = _interop.LoadType(System.Gadget.path + "\\bin\\BatteryGauge.dll", "BatteryGauge.TimeRemaining");
  Update();
}

function Dispose()
{
  //Clean up by gracefully disposing the COM object holding the TimeRemaining assembly.
  _interop.UnloadType(_timeRemaining);
  _timeRemaining = null;
  _interop = null;
}

function FixedPercent(percent)
{
  if (percent === null)
  {
    return "0"; //0
  }
  if (percent <= 0)
  {
    return "0"; //0
  }
  if (percent > 100)
  {
    return "n/a"; //(100) sem bateria
  }
  return percent;
}

function Update()
{
  var percent = FixedPercent(System.Machine.PowerStatus.batteryPercentRemaining);
  var percentString;
  var timeString;
  var battIcon;
  var onPower = false;
  var charging = false;
  var update = false;

  if (percent >= 90)
  {
    battIcon = 8;
  }
  else if (percent >= 80)
  {
    battIcon = 7;
  }
  else if (percent >= 70)
  {
    battIcon = 6;
  }
  else if (percent >= 60)
  {
    battIcon = 5;
  }
  else if (percent >= 50)
  {
    battIcon = 4;
  }
  else if (percent >= 30)
  {
    battIcon = 3;
  }
  else if (percent > 10)
  {
    battIcon = 2;
  }
  else if (percent > 7)
  {
    battIcon = 1;
  }
  else
  {
    battIcon = 0;
  }

  if (percent == "n/a"){battIcon = "na";} //no battery


  percentString = percent + "%";

  if(percent=="n/a"){percentString=" "} //no battery, remove percent.


  if (System.Machine.PowerStatus.isBatteryCharging)
  {
    timeString = " ";
    charging = true;
  }
  else if (System.Machine.PowerStatus.isPowerLineConnected)
  {
    timeString = " ";
    onPower = true;
  }
  else
  {
    timeString = _timeRemaining.TimeRemainingString;
    if (timeString == "")
    {
      timeString = " ";
    }
  }
  if (_currentBattIcon != battIcon)
  {
    update = true;
    _currentBattIcon = battIcon;
  }
  if (_currentTimeString != timeString)
  {
    if (_currentTimeString == " ")
    {
      //If transitioning to having a time string present, clear the large percent text and force the percent text to update below.
      _txtPercentageLarge = " ";
      _currentPercentString = " ";
    }
    if (timeString == " ")
    {
      //If transitioning from having a time string present, clear the small percent text and force the percent text to update below.
      _txtPercentage = " ";
      _currentPercentString = " ";
    }
    update = true;
    _txtTime = timeString;
    _currentTimeString = timeString;
  }
  if (_currentPercentString != percentString)
  {
    if (_currentTimeString == " ")
    {
      _txtPercentageLarge = percentString;
    }
    else
    {
      _txtPercentage = percentString;
    }
    update = true;
    _currentPercentString = percentString;
  }
  if ((_currentOnPower != onPower) || (_currentCharging != charging))
  {
    _currentOnPower = onPower;
    _currentCharging = charging;
    update = true;
  }

  if (update)
  {
    background.removeObjects();
    fixDpiObject(background.addImageObject("icons/battery_" + battIcon + ".png", 0, 0));
    if (charging) {
      fixDpiObject(background.addImageObject("icons/power_charging.png", 80, 20));
    }
    else if (onPower) {
      fixDpiObject(background.addImageObject("icons/power_ac.png", 80, 20));
    }
    else {
      fixDpiObject(background.addImageObject("icons/power_blank.png", 80, 20));
    }
    var txt = fixDpiObject(background.addTextObject(_txtPercentage, "Segoe UI", 18, "Black", 45, 16));
    //txt.width = 60;
    txt.align = 1;
    txt = fixDpiObject(background.addTextObject(_txtTime, "Segoe UI", 15, "Black", 45, 36));
    //txt.width = 60;
    txt.align = 1;
    txt = fixDpiObject(background.addTextObject(_txtPercentageLarge, "Segoe UI", 20, "Black", 45, 23));
    //txt.width = 60;
    txt.align = 1;
  }
  setTimeout(Update, 5000);
}