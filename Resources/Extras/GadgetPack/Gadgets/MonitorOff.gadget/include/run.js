// JavaScript Document


function runMonOff(){
System.Shell.execute(System.Gadget.path + "\\core\\nircmd.exe" , 'monitor off');
 }

function loadGadget(){

	System.Gadget.onDock = dockStateChanged;
	System.Gadget.onUndock = dockStateChanged;


	System.Gadget.settingsUI = "settings.html";
	 System.Gadget.onSettingsClosed = settingsClosed;
}

function dockStateChanged(){
//change size depending on state
if (!System.Gadget.docked){ 
 
  with (document.body.style)
 {
  width = "256px";
  height = "256px";

 }
 background.style.width = "256px";
 background.style.height = "256px";
 background.src = "url(../images/dell2.png)";
 }
 
 else if(System.Gadget.docked){
  with (document.body.style)
 {
  width = "135px";
  height = "135px";
 }
 background.style.width = "130px";
 background.style.height = "130px";
 background.src = "url(../images/dell2.png)";
 }

}