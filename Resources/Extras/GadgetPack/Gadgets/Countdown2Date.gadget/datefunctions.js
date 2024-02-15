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


function eventDateOK()
{
    // Function to check the date entered by the user and ensure it's not in the past
  
    
    // Get the currnet system date
    var currentDate = new Date();
    
    // As we are only interested in the date, clear the time component
    currentDate.setHours(0,0,0,0);

    // Take the date from the settings and change that to a date object
    var eventDate = new Date(convertDateString(gSettingsDate));

    // If the date is less than today, then we need to put up an error
    if (eventDate < currentDate)
    {
        // Error
        return false;
    }
    else
    {
        return true;
    }
}

function isDateValid(str)
{
    //Needs to be checked.
    var RegExPattern = /(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))/;
    // Return true if the context matches the pattern, false if not.
    return RegExPattern.test(str)
}