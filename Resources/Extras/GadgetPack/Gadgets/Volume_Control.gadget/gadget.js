/////////////////////////////////////////////////////////////////////////////////////
//                                                                                 //
//  Volume Control Gadget 1.2 by Orbmu2k © 2007                                   //
//                                                                                 //
//  Copyright © 2007 Orbmu2k.  All rights reserved.                                //
//                                                                                 //
//  http://blog.orbmu2k.de                                                         //
//                                                                                 //
//  Email: sidebargadget@orbmu2k.de                                                //
//                                                                                 //
/////////////////////////////////////////////////////////////////////////////////////

System.Gadget.settingsUI = "settings.html";
System.Gadget.onSettingsClosed = onSettingsClosed;

var sound;
var t;
var vol;
var mute;
//var imgSpeaker;
//var imgCross;
var updating = false;

function init()
{
    sound = GetLibrary();
    loadSettings();
    //initObjects();
	t = setInterval("refreshdisplay()",75);
}

function loadSettings()
{
    var b = System.Gadget.Settings.readString("background");
	if (b != "") background.src = b;    
    if (b == "background.png" || b == "")
        win10back.style.display = "block";
    else
        win10back.style.display = "none";
}

function onSettingsClosed()
{
    loadSettings();
}

function terminate()
{
    UnregisterLibrary(); 
}


function changeVolume()
{
    var v = sound.MasterVolume;

    if (event.wheelDelta >= 20)
		v += 3;
	else 
		v -= 3;
	
	if (v >= 100) v = 100;
	if (v <= 0) v = 0;

	sound.MasterVolume = v;
}

function muteVolume()
{
    if (event.button == 4)
    {                
        sound.MasterMute = !sound.MasterMute;
    }
    onMouseMove();
}

function onMouseMove()
{
    if (event.button == 1)
    {
        var v = event.x-12;
	    if (v >= 100) v = 100;
	    if (v <= 0) v = 0;
        sound.MasterVolume = v;
    }
}

function runMixer()
{
    System.Shell.execute("sndvol.exe");
}

function refreshdisplay()
{
    try
    {
        if (!System.Gadget.visible || updating) return;
        updating = true;
        
        vol = sound.MasterVolume;
        mute = sound.MasterMute;
        
        if (mute)
        {
            imgCross.style.display = "block";
            txtVolume.innerText = "Volume: Mute";
        }
        else
        {
            imgCross.style.display = "none";
            txtVolume.innerText = "Volume: " + vol + "%";
        }
        
        divvol.style.width = (Math.round((vol / 8)) * 8);
        divpeakleft.style.width = (Math.round((sound.LeftPeak / 4)) * 4);
        divpeakright.style.width = (Math.round((sound.RightPeak / 4)) * 4);
        
        updating = false;
    }
    catch(err) 
    {
    }
}

