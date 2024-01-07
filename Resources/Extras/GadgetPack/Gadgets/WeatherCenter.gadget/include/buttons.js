////////////////////////////////////////////////////////////////////////////////
//
// set's alt tabs for navigation;
//
////////////////////////////////////////////////////////////////////////////////
function setAltLabels()
{
    buttonDownImage.setAttribute("alt", L_SHOWMORE_TEXT);
    buttonUpImage.setAttribute("alt", L_SHOWLESS_TEXT);
    buttonLeftImage.setAttribute("alt", L_MINIMODE_TEXT);
    buttonRightImage.setAttribute("alt", L_FULLMODE_TEXT);
    buttonRefreshImage.setAttribute("alt", L_REFRESH_TEXT);
}
////////////////////////////////////////////////////////////////////////////////
//
// show navigation bar with fade-in
//
////////////////////////////////////////////////////////////////////////////////
function showBar()
{
	if (System.Gadget.Settings.read('showFlyoutForecast') == "1") self.focus();
	if (System.Gadget.Settings.read('hideBarSettings') == "1") return;
    	//if(event.fromElement) return;
    	clearTimeout(bar.timer);

	buttonUp.style.display = "block";
	buttonDown.style.display = "block";
	if (System.Gadget.Settings.read("fcDays") == 0) buttonUp.style.display = "none";
	if (System.Gadget.Settings.read("fcDays") == totalFCDays) buttonDown.style.display = "none";
	
    	up();

    function up()
    {
        with(bar.filters("alpha"))
        {
            if((opacity+=15)<75)
            {
                bar.timer = setTimeout(up, 50);
            }
            else
            {
                opacity=75;
            }
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
//
// hide navigation bar with fade-out
//
////////////////////////////////////////////////////////////////////////////////
function hideBar()
{
    if (System.Gadget.Settings.read('hideBarSettings') == "1") return;
    //if(event.toElement) return;
    clearTimeout(bar.timer);

    down();

    function down()
    {
        with(bar.filters("alpha"))
        {
            if((opacity-=15)>0)
            {
                bar.timer = setTimeout(down, 50);
            }
            else
            {
                opacity=0;
            }
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
//
//
////////////////////////////////////////////////////////////////////////////////
function toggleButton(bttn, srcName)
{
    eval(bttn + ".src = 'images/" + srcName + ".png'"); 
}
