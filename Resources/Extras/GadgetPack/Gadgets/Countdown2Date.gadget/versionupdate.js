var link;
var linkname;

function checkForUpdate() 
{
    var xmlFile = new ActiveXObject("Microsoft.XMLDOM");
    xmlURL = "http://members.optusnet.com.au/~blurg64/version/Countdown.version";
    xmlFile.async = "false";
    xmlFile.load(xmlURL);
    xmlObj = xmlFile.documentElement;
    if(xmlFile.parseError != "0")
    {
        // Parse error, do nothing and return
    }
    else
    {
        if (updateAvailable() == true)
        {
            versionMessage.innerHTML = L_txtUpdateAvailable +  "<a href=\"" + link + "\">" + linkname + "</a>";
        }
        else
        {
            versionMessage.innerHTML = "<a href=\"https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=alexb%40blurg%2enet&item_name=Countdown%20Gadget&no_shipping=0&no_note=1&tax=0&currency_code=AUD&lc=AU&bn=PP%2dDonationsBF&charset=UTF%2d8\"><img src=\"images/PayPalDonate.gif\" style=\"border-right:0;border-top:0;border-left:0;border-bottom:0;\" alt=\"Support the continued development of this gadget\"/></a>"
        
        }
    }
}
 

function updateAvailable()
{
    // Pull out data from the XML.
    
    // Get the version data
    var remoteMajorVersion = xmlObj.childNodes(0).childNodes(0).childNodes(0).firstChild.text;
    var remoteMinorVersion = xmlObj.childNodes(0).childNodes(0).childNodes(1).firstChild.text;
    var remoteBuildVersion = xmlObj.childNodes(0).childNodes(0).childNodes(2).firstChild.text;
    
    // Get the link information
    link = xmlObj.childNodes(0).childNodes(1).firstChild.text;
    linkname = xmlObj.childNodes(0).childNodes(2).firstChild.text;
    
    // Now we need to split the gadget's version into major.minor.build nomenclature
    var localVersion = System.Gadget.version;
    
    var arrayVersion = localVersion.split(".");
    
    // Take the three components from the array and stick them in the Major/Minor/Build variables
    var localMajorVersion = arrayVersion[0];
    var localMinorVersion = arrayVersion[1];
    var localBuildVersion = arrayVersion[2];
    
    
    // Check for a build update
    if (remoteMajorVersion > localMajorVersion)
    {
        return true;
    }
    else
    {
        // remoteMajorVersion must be less than or equal to localMajorVersion, so now
        // we check the remoteMinorVersion.
        if (remoteMinorVersion > localMinorVersion)
        {
            return true;
        }
        else
        {
            // minor version hasn't incremented, next check the build
            if (remoteBuildVersion > localBuildVersion)
            {
                return true; 
            }
            else
            {
                // nothings incremented, therefore an update cannot be available.
                return false; 
            }
        }
    }
    
}