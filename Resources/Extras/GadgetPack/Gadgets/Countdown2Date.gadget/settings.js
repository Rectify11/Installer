
///////////////////////////////////////////////////////////////////////////////////////////////////
//
// Global Settings Members
//
///////////////////////////////////////////////////////////////////////////////////////////////////
var gSettingsDate = "";
var gSettingsTitle = "";
var gSettingsLink = "";
var imageArray = new Array("thmb_calendar_blue.png", "thmb_calendar_green.png", "thmb_calendar_grey.png", "thmb_calendar_orange.png",
"thmb_calendar_red.png", "thmb_calendar_yellow.png", "thmb_calendar_black.png", "thmb_calendar_brown.png");
var imagePath = "images/thumbs/";
var imageIndex = 0;

///////////////////////////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////////////////////////
//
// loadSettings - first method called when Settings page loads
//
///////////////////////////////////////////////////////////////////////////////////////////////////
function loadSettings() {   
    // main Setting page onload method (first method called every time the Settings page is loaded)
    
    // init event methods
    System.Gadget.onSettingsClosing = procSettingsClosingEvent;                 // set settings page closing event
    
    // get settings data
    gSettingsDate  = System.Gadget.Settings.readString("Date");             
    gSettingsTitle = System.Gadget.Settings.readString("Title");
    gSettingsLink = System.Gadget.Settings.readString("Link");
    imageIndex = System.Gadget.Settings.read("Index");
       
    // Check the imageIndex in case this is the first time through   
    if (imageIndex == "")
    {
        imageIndex = 0;
    }
       
    
    // add data to Settings page
    txtDate.value = gSettingsDate;
    txtTitle.value   = gSettingsTitle;
    txtLink.value = gSettingsLink;
    imgSkinPreview.src = imagePath + imageArray[imageIndex];
    
    // Set up the current index and the max index. Always add one as arrays start at 0.
    document.getElementById("currentIndex").innerText = imageIndex + 1;
    document.getElementById("of").innerText = L_txtOf + " "; 
    document.getElementById("maxIndex").innerText = imageArray.length; 
    
    
    document.getElementById("eventDate").innerText = L_txtEventDate;
    document.getElementById("eventTitle").innerText = L_txtEventTitle;
    document.getElementById("eventLink").innerText = L_txtEventLinkOptional;
    document.getElementById("moreInfo").innerText = L_txtMoreInfo;
    document.getElementById("help").innerText = L_txtHelp; 
    
    // Call out and check for a new version
    checkForUpdate();
}
///////////////////////////////////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////////////////////////////////////////
//
// procSettingsClosingEvent - process settings page closing event
//
///////////////////////////////////////////////////////////////////////////////////////////////////
function procSettingsClosingEvent(event){
    // process settings page closing event
    if (event.closeAction == event.Action.commit) {
        // save settings
        gSettingsDate = txtDate.value; 
        gSettingsTitle = txtTitle.value;
        gSettingsLink = txtLink.value;
        
        // Check the format of the date entered:
        if (isDateValid(gSettingsDate) == false)
        {
            document.getElementById("dateError").innerHTML = L_txtErrorIncorrectDateEntered;  
            event.cancel = true;   
        
        }
        else
        {
            // Check settings
            if (eventDateOK() == true)
            {
                // Write settings
                System.Gadget.Settings.writeString("Date", gSettingsDate);
                System.Gadget.Settings.writeString("Title", gSettingsTitle);
                System.Gadget.Settings.writeString("Link", gSettingsLink);
                System.Gadget.Settings.write("Index", imageIndex);
                event.cancel = false;            
            }
            else
            {
                document.getElementById("dateError").innerHTML = L_txtErrorDateIsInThePast;  
                event.cancel = true;    
            }
        }
          
    }
   
}
///////////////////////////////////////////////////////////////////////////////////////////////////

// skinLeft() - function that will decrement the imageArray index by 1 and then update the source 
// in the gui.
function skinLeft()
{
    if (event.keyCode == 32 || event.button == 1)
    {
        imageIndex--;
        
        if (imageIndex < 0)
        {
            imageIndex = imageArray.length - 1;
        }   
        imgSkinPreview.src = imagePath + imageArray[imageIndex]; 
        document.getElementById("currentIndex").innerText = imageIndex + 1; 
    }
}


// skinRight() - function that will increment the imageArray index by 1 and then update the source 
// in the gui.
function skinRight()
{
    if (event.keyCode == 32 || event.button == 1)
    {
        imageIndex++;
        
        if (imageIndex == imageArray.length)
        {
            imageIndex = 0;
        }   
        imgSkinPreview.src = imagePath + imageArray[imageIndex];
        document.getElementById("currentIndex").innerText = imageIndex + 1;   
    }
}







