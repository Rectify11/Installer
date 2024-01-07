
function loadMain()
{
    var gSettingsDate = "";
    var gSettingsTitle = "";   
    var gSettingsLink = "";
    // init settings event methods and data
    //debugger;
    System.Gadget.settingsUI = "settings.htm";                              
    System.Gadget.onSettingsClosed = procSettingsClosedEvent;               

    genDocked();

}
///////////////////////////////////////////////////////////////////////////////////////////////////
//
// procDockEvent - process dock event
//
///////////////////////////////////////////////////////////////////////////////////////////////////
function procDockEvent()
{   // process dock event
    genDocked();
}
///////////////////////////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////////////////////////
//
// procUndockEvent - process undock event
//
///////////////////////////////////////////////////////////////////////////////////////////////////
function procUndockEvent()
{   // process undock event
    genDocked();
}

///////////////////////////////////////////////////////////////////////////////////////////////////
//
// procSettingsClosedEvent - process settings closed event
//
///////////////////////////////////////////////////////////////////////////////////////////////////
function procSettingsClosedEvent(event)
{
    // process settings closed event
    if (event.closeAction == event.Action.commit)
     {
       loadMain();
    }
}

///////////////////////////////////////////////////////////////////////////////////////////////////
//
// getDocked() - Processing when the gadget it docked.
//
///////////////////////////////////////////////////////////////////////////////////////////////////

function genDocked()
{     
    // set gadget document styles
    document.body.style.width = "130px";                                            
    document.body.style.height = "150px";                                                        

    gadgetMainFrame.style.width = "130px";                                        
    gadgetMainFrame.style.height = "150px";                              
    
    // set content frame styles (main gadget content area)
    gadgetContentFrame.style.width = "130px";                                       
    gadgetContentFrame.style.height = "150px"; 

   
    // set gadget content visibility
    gadgetContentFrame.style.display = "block";                               // display docked content
       
    // Get the title and date from the settings.
    gSettingsDate = System.Gadget.Settings.readString("Date");             // read settings data
    gSettingsTitle = System.Gadget.Settings.readString("Title");
    gSettingsLink = System.Gadget.Settings.readString("Link");
   
   
    // Call the getBackgroundImage function to determine what skin colour has
    // been chosen by the user and set it.
    getBackgroundImage();
     
    
    // If there is a link defined in the settings, then dynamically change
    // the cursor when it hovers over the gadget
    if (gSettingsLink != "")
    {
        document.all.gadgetMainFrame.style.cursor = "pointer";
    }
    else
    {
        document.all.gadgetMainFrame.style.cursor = "default";
    }
    
    if (gSettingsDate == "")
    {
        // No date is configured, tell the user to set up.
        // If the localisation is too big, the no Settings text will be split into three variables
        if (L_txtNoSettings3 == "")
        {  
            document.getElementById("noSettings").innerHTML = L_txtNoSettings1 + "<br />" + L_txtNoSettings2;
        }
        else
        {
            document.getElementById("noSettings").innerHTML = L_txtNoSettings1 + "<br />" + L_txtNoSettings2 + "<br />" + L_txtNoSettings3;
        }
    }
    else
    {
        // Clear the no settings message
        document.getElementById("noSettings").innerHTML = "";
       
        
        
        // Get the number of days to the event
        var daysToEvent = getDaysToEvent();
        
        // Hide the DayPrefix until we actually need to display it (ie when the gadget has flipped 
        // past the event date
        daysToGoPrefix.style.display = "none";
        
        // Setup the title and number of days
        document.getElementById("eventTitle").innerText = gSettingsTitle;
        document.getElementById("daysToGo").innerText = daysToEvent;    
        
        document.all.daysToGo.style.color = "Black";
        
        // Determine the suffix for the days to go.
        if (daysToEvent >= 1000)
        {
            document.all.daysToGo.style.fontSize = "20pt";
        }
        else
        {
            // If the event is today
            if (daysToEvent == 0)
            {
                // Need to check the size of the text for the Localised version
                // of today. If the text size is > 7 chars, then we need to decrease
                // the font size even more.
                
                if (L_txtToday.length > 7)
                {
                    document.all.daysToGo.style.fontSize = "16pt";
                }
                else
                {
                    document.all.daysToGo.style.fontSize = "20pt";
                }
                
                // The font size has been changed, now set up the values.
                document.getElementById("daysToGo").innerText = L_txtToday;
                document.getElementById("daysToGoSuffix").innerText = "";
            }
            else
            {      
                document.all.daysToGo.style.fontSize = "40pt";
            }
        }

        
        if (daysToEvent > 1)
        {    
            document.getElementById("daysToGoSuffix").innerText = L_txtDaysToGo; 
        }
        
        if (daysToEvent == 1)
        {
            document.getElementById("daysToGoSuffix").innerText = L_txtDayToGo; 
        }
        
        if (daysToEvent < 0)
        {
            // If the date has been missed, then convert the date to a positive number by * -1
            document.all.daysToGo.style.color = "Red";
            document.getElementById("daysToGo").innerText = daysToEvent * -1;
            
            if (daysToEvent == -1)
            {
              document.getElementById("daysToGoSuffix").innerText = L_txtDayAgo;  
              
              // With the introduction of international support, some of the languages don't fit
              // into the straight xx days ago and require a DaysAgo Prefix (French & Italian mainly).
              // However the prefix stuffs up the formatting and requires the CSS to be changed dynamically
              // to move the daysToGo and daysToGoSuffix around.

              if (L_txtDayPrefix != "")
              {
                daysToGoPrefix.style.display = "";
                document.getElementById("daysToGoPrefix").innerText = L_txtDayPrefix;
                document.all.daysToGo.style.top = "-10px";
                document.all.daysToGoSuffix.style.top = "-20px";
              }

            }
            else
            {
              System.Gadget.close();
            }
        }
        

     }
     // Calculate how long it is until midnight, then set the timer to rerun 
     // causing the data and calculation to change.
     
     // get current date
     var currentDate = new Date();
     
     // work out tomorrows date by adding one day to the time in MS
     var tomorrowDateinMS = currentDate.getTime() + 86400000; 
     
     // convert the time to a date
     var tomorrowDate = new Date(tomorrowDateinMS);
     
     // Set the time to midnight
     tomorrowDate.setHours(0,0,0);
     
     // Difference will give you the time to midnight
     var difference = tomorrowDate - currentDate;
     
     setTimeout("genDocked()",difference);
     
}

function getBackgroundImage()
{
    var imageArray = new Array("calendar_blue.png", "calendar_green.png", "calendar_grey.png", "calendar_orange.png",
"calendar_red.png", "calendar_yellow.png", "calendar_black.png", "calendar_brown.png");
    var imagePath = "images/";
    
    var imageIndex = System.Gadget.Settings.read("Index");
    
    // Check to see what the background colour is, if yellow, then change the font to be black   
    if (imageIndex == 5 || imageIndex == 4 || imageIndex == 3)
    {
        document.all.eventTitle.style.color = "Black";
    }
    else
    {
        document.all.eventTitle.style.color = "White";
    }
    
    /*if (imageIndex == "")
    {
        System.Gadget.background="url(" + imagePath + "calendar_blue.png)";
    }
    else
    {
        System.Gadget.background="url(" + imagePath + imageArray[imageIndex] + ")";
    }*/
    if (imageIndex == "")
    {
		background.src = imagePath + "calendar_blue.png";
    }
    else
    {
		background.src = imagePath + imageArray[imageIndex];
    }
	//background.addImageObject(imagePath + "calendar_blue.png", 0, 0)
}


function getDaysToEvent()
{
    // Get the currnet system date
    var currentDate = new Date();
    
    // As we are only interested in the date, clear the time component
    currentDate.setHours(0,0,0,0);

    // Take the date from the settings and change that to a date object
    var eventDate = new Date(convertDateString(gSettingsDate));

    // Now calculate how many days between the currnet date and the eventDate

    var difference = eventDate - currentDate;
    
    // Divide the difference to get the number of days
    var daysToEvent = Math.floor(difference/86400000);

    return daysToEvent;
}


function convertDateString(dateToConvert)
{
    var strEventDate = new String;
    strEventDate = dateToConvert;
    var dateDays = strEventDate.substring(0,2);
    var dateMonth = strEventDate.substring(3,5);
    var dateYear = strEventDate.substring(6,10);
    
    var returnValue = dateMonth + "-" + dateDays + "-" + dateYear + " 00:00:00";

    return returnValue;
}

function executeLink()
{

        var shell = new ActiveXObject("WScript.Shell"); 
        
        if (gSettingsLink != "")
        {
            shell.Run(gSettingsLink); 
        }
}


