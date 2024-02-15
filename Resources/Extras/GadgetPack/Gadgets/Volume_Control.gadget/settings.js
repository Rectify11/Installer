/////////////////////////////////////////////////////////////////////////////////////
//                                                                                 //
//  Volume Control Gadget 1.2 by Orbmu2k © 2007                                    //
//                                                                                 //
//  Copyright © 2007 Orbmu2k.  All rights reserved.                                //
//                                                                                 //
//  http://blog.orbmu2k.de                                                         //
//                                                                                 //
//  Email: sidebargadget@orbmu2k.de                                                //
//                                                                                 //
/////////////////////////////////////////////////////////////////////////////////////

System.Gadget.onSettingsClosing = SettingsClosing;

function onLoad()
{
    initSettings();
}

function onUnload()
{

}

function SettingsClosing(event)
{
	if (event.closeAction == event.Action.commit) 
		saveSettings();
	event.cancel = false;
}

function initSettings()
{
    loadSettings();
}

function loadSettings()
{
    background.value = System.Gadget.Settings.read("background");        
}

function saveSettings()
{
    System.Gadget.Settings.write("background", background.value);
}
