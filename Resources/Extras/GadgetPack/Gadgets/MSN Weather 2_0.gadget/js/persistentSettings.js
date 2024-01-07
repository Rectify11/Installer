// PersistentSettings.js - A Persistent Setting API For Sidebar Gadgets
// by Bruce Williams (e-mail: bwill@microsoft.com, IM: domanite@hotmail.com)

var persistentSettingLockFile = System.Gadget.path + "\\WeatherSettingsLockFile.txt";
var persistentSettingFile = System.Gadget.path + "\\WeatherSettings.txt";
var persistentSettingTempFile = System.Gadget.path + "\\WeatherSettings.tmp.txt";
//
// we'll use five tildes as our magic separator - if you actually put that
// in your setting name or value, we're hosed.  someday maybe we can get more
// clever here and do some encoding.
//
var persistentSettingDelimiter = "=";
var persistentSettingRegExpFormatString = "(..*)" + persistentSettingDelimiter + "(.*)";
var persistentSettingQueue = new Array();
var persistentSettingPendingTimeouts = 0;
var persistentSettingFailedRetries = 0;

function getPersistentSetting(name, callback)
{
	if ((callback == undefined) || (callback == null))
	{
		var errorMsg = "getPersistentSetting: you must specify a callback function";
		persistentSettingLog(errorMsg);
		throw errorMsg;
	}
	persistentSettingQueue.push({operation:"get", name:name, callback:callback});
	window.setTimeout(processPersistentSettingQueue, 0);
	persistentSettingPendingTimeouts++;
}

function setPersistentSetting(name, value, callback)
{
	persistentSettingQueue.push({operation:"set", name:name, value:value, callback:callback});
	window.setTimeout(processPersistentSettingQueue, 0);
	persistentSettingPendingTimeouts++;
}

function processPersistentSettingQueue()
{
	try
	{
		persistentSettingPendingTimeouts--;
		if (persistentSettingQueue.length == 0)
		{
			return;
		}
		var fs = new ActiveXObject("Scripting.FileSystemObject");
		try
		{
			var lockFile = null;
			try
			{
				lockFile = fs.CreateTextFile(persistentSettingLockFile, false);
			}
			catch (e)
			{
				if (persistentSettingPendingTimeouts == 0)
				{
					if (persistentSettingFailedRetries > 600)
					{
						throw "maximum lock retries exceeded";
					}
					persistentSettingFailedRetries++;
					persistentSettingPendingTimeouts++;
					window.setTimeout(processPersistentSettingQueue, 100);
				}
				return;
			}
			try
			{
				persistentSettingFailedRetries = 0;
				while (persistentSettingQueue.length > 0)
				{
					var returnValue = null;
					var workItem = persistentSettingQueue.shift();
					if (workItem.operation == "set")
					{
						setPersistentSettingInternal(workItem.name, workItem.value);
					}
					else if (workItem.operation == "get")
					{
						returnValue = getPersistentSettingInternal(workItem.name);
					}
					if ((workItem.callback != undefined) && (workItem.callback != null) && (workItem.callback != false))
					{
						persistentSettingScheduleCallback(workItem.callback, returnValue);
					}
				}
			}
			finally
			{
				lockFile.Close();
				fs.DeleteFile(persistentSettingLockFile, true);
			}
		}
		finally
		{
			fs = null;
		}
	}
	catch (e)
	{
		persistentSettingFailFast(e.name + ": " + e.message);
	}    
}

function persistentSettingScheduleCallback(callback, returnValue)
{
	window.setTimeout(function () 
	{
		try
		{
			callback(returnValue);
		}
		catch (e)
		{
			persistentSettingLog("exception from callback: " + e.name + ": " + e.message);
		}
	}, 0);
}

function setPersistentSettingInternal(name, value)
{
	var fs = new ActiveXObject("Scripting.FileSystemObject");
	try
	{
		var currentFile = null;
		try
		{
			currentFile = fs.GetFile(persistentSettingFile);
		}
		catch (e) // assume file not found - don't know a language-neutral way to detect other exceptions
		{
			var newFile = fs.CreateTextFile(persistentSettingFile)
			try
			{
				newFile.WriteLine(name + persistentSettingDelimiter + value);
			}
			finally
			{
				newFile.Close();
			}
			return;
		}
		oldSettings = currentFile.OpenAsTextStream(1); // read-only
		try
		{
			var settingReplaced = false;
			var template = new RegExp(persistentSettingRegExpFormatString);
			var newSettings = fs.CreateTextFile(persistentSettingTempFile, true);
			try
			{
				while (!oldSettings.AtEndOfStream)
				{
					var line = oldSettings.ReadLine();
					var matches = line.match(template);
					var nameFromFile = matches[1];
					var valueFromFile = matches[2];
					if (name == nameFromFile)
					{
						newSettings.WriteLine(name + persistentSettingDelimiter + value);
						settingReplaced = true;
					}
					else
					{
						newSettings.WriteLine(nameFromFile + persistentSettingDelimiter + valueFromFile);
					}
				}
				if (!settingReplaced)
				{
					newSettings.WriteLine(name + persistentSettingDelimiter + value);
				}
			}
			finally
			{
				newSettings.Close();
			}
		}
		finally
		{
			oldSettings.Close();
		}
		fs.DeleteFile(persistentSettingFile, true); // force it, even if it is read-only
		fs.MoveFile(persistentSettingTempFile, persistentSettingFile);
	}
	finally
	{
		fs = null;
	}
}

function getPersistentSettingInternal(name)
{
	var fs = new ActiveXObject("Scripting.FileSystemObject");
	try
	{
		var settingsFile = null;
		try
		{
			settingsFile = fs.GetFile(persistentSettingFile);
		}
		catch (e)
		{
			// assume file not found - don't know a language-neutral way to detect other exceptions
			return false;
		}
		settings = settingsFile.OpenAsTextStream(1); // read-only
		try
		{
			var template = new RegExp(persistentSettingRegExpFormatString);
			while (!settings.AtEndOfStream)
			{
				var line = settings.ReadLine();
				var matches = line.match(template);
				var nameFromFile = matches[1];
				var valueFromFile = matches[2];
				if (name == nameFromFile)
				{
					return valueFromFile;
				}
			}		
		}
		finally
		{
			settings.Close();
		}
		return false;
	}
	finally
	{
		fs = null;
	}
}

function valueExists(name)
{
	var fs = new ActiveXObject("Scripting.FileSystemObject");
	try
	{
		var settingsFile = null;
		try
		{
			settingsFile = fs.GetFile(persistentSettingFile);
		}
		catch (e)
		{
			// assume file not found - don't know a language-neutral way to detect other exceptions
			return false;
		}
		settings = settingsFile.OpenAsTextStream(1); // read-only
		try
		{
			var template = new RegExp(persistentSettingRegExpFormatString);
			while (!settings.AtEndOfStream)
			{
				var line = settings.ReadLine();
				var matches = line.match(template);
				var nameFromFile = matches[1];
				var valueFromFile = matches[2];
				if (name == nameFromFile)
				{
					return true;
				}
			}		
		}
		finally
		{
			settings.Close();
		}
		return false;
	}
	finally
	{
		fs = null;
	}
}

function persistentSettingLog(message)
{
	System.Debug.outputString("PersistentSettings.js: " + message + "\n");
}

function persistentSettingFailFast(errorMessage)
{
	persistentSettingLog("FAILFAST: " + errorMessage);
	System.Gadget.close();
}
