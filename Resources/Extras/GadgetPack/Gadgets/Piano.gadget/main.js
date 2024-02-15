var KeysNumber=-1;
var Keys=new Array();

KeysNumber+=1;Keys[KeysNumber]={KeyName:"F3",KeyCode:"F3",WriteKey:true};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"F3#",KeyCode:"F3S",WriteKey:false};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"G3",KeyCode:"G3",WriteKey:true};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"G3#",KeyCode:"G3S",WriteKey:false};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"A3",KeyCode:"A3",WriteKey:true};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"A3#",KeyCode:"A3S",WriteKey:false};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"B3",KeyCode:"B3",WriteKey:true};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"C4",KeyCode:"C4",WriteKey:true};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"C4#",KeyCode:"C4S",WriteKey:false};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"D4",KeyCode:"D4",WriteKey:true};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"D4#",KeyCode:"D4S",WriteKey:false};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"E4",KeyCode:"E4",WriteKey:true};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"F4",KeyCode:"F4",WriteKey:true};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"F4#",KeyCode:"F4S",WriteKey:false};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"G4",KeyCode:"G4",WriteKey:true};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"G4#",KeyCode:"G4S",WriteKey:false};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"A4",KeyCode:"A4",WriteKey:true};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"A4#",KeyCode:"A4S",WriteKey:false};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"B4",KeyCode:"B4",WriteKey:true};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"C5",KeyCode:"C5",WriteKey:true};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"C5#",KeyCode:"C5S",WriteKey:false};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"D5",KeyCode:"D5",WriteKey:true};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"D5#",KeyCode:"D5S",WriteKey:false};
KeysNumber+=1;Keys[KeysNumber]={KeyName:"E5",KeyCode:"E5",WriteKey:true};

//var DivKeys==new Array();

var piano = new function(){
	//页面载入
	this.pageOnload= function(){
		var h="";
		for (var i = 0; i < Keys.length; i++) {
			if (Keys[i].WriteKey){
				h+='<div id="key_'+i+'" unselectable="on" class="key_'+Keys[i].KeyCode+' writekey" onfocus="piano.onfocus()" onblur="piano.lostfocus()"';
				h+='onmousedown="piano.KeyDownPlay('+i+')" onmouseup="piano.KeyUpPlay('+i+')" onmouseout="piano.KeyUpPlay('+i+')">';
				h+='<img alt="" class="keyupimg" src="images/WriteKey.png" />';
				h+='<img alt="" id="keydownimg_'+i+'" class="keydownimg" src="images/WriteKey_f2.png" style="display:none;"/>';
				if (Keys[i].KeyCode=="C4"){
					h+='<img alt="" id="C4Position" src="images/C4Position.png" />';
				}
				h+='</div>';
			}else{
				h+='<div id="key_'+i+'" unselectable="on" class="key_'+Keys[i].KeyCode+' blackkey" onfocus="piano.onfocus()" onblur="piano.lostfocus()"';
				h+='onmousedown="piano.KeyDownPlay('+i+')" onmouseup="piano.KeyUpPlay('+i+')" onmouseout="piano.KeyUpPlay('+i+')">';
				h+='<img alt="" class="keyupimg" src="images/BlackKey.png" />';
				h+='<img alt="" id="keydownimg_'+i+'" class="keydownimg" src="images/BlackKey_f2.png" style="display:none;"/>';
				h+='</div>';
			}
		}
		document.getElementById("keys").innerHTML=h;
		try{
			
			piano.checkState();
			System.Gadget.onUndock = piano.checkState; 
			System.Gadget.onDock = piano.checkState;
			System.Gadget.Flyout.file ="Flyout.html";
       		System.Gadget.settingsUI = "setting.html";
    		System.Gadget.onSettingsClosed = piano.SettingsClosed;
    		
    	}catch(err){}
    	piano.ResetKeyCode();
    	document.getElementById("check_update").src="http://gadget.photo-bon.com/programdata/PianoGadgetUpdate.js";
    }
    this.getDivKeys=function(){
    	for (var i = 0; i < Keys.length; i++) {
    		DivKeys[i]=document.getElementById('key_'+i.toString());
    	}
    }
    this.ResetKeyCode=function(){
    	var keycodetype=L_localizedStrings_keycodetype;
    	
    	try{			
			keycodetype=System.Gadget.Settings.read("keycodetype");
    		if (keycodetype) {SetKeyCodeArray(keycodetype);}
    		else{SetKeyCodeArray(L_localizedStrings_keycodetype);}
    	}catch(err){
    		SetKeyCodeArray(L_localizedStrings_keycodetype);
    	}
    }
    //设置关闭
    this.SettingsClosed=function(event){
	     if (event.closeAction == event.Action.commit)    {
    	 	piano.ResetKeyCode();		
	    }
    	// User hits Cancel on the settings page.
    	else if (event.closeAction == event.Action.cancel)    {
        	//SetContentText("Cancelled");
    	}
    }
	this.KeyDownPlay=function(i){
		if (document.getElementById("keydownimg_"+i).style.display!=""){
			System.Sound.playSound("sound\\"+Keys[i].KeyCode+".wav");
			document.getElementById("keydownimg_"+i).style.display="";
		}	
	}
	this.KeyUpPlay=function(i){
		document.getElementById("keydownimg_"+i).style.display="none";
	}
	this.keydown=function(){
		piano.onfocus();		
		for (var i = 0; i < PCKeys.length; i++) {
			if(PCKeys[i]==event.keyCode){
				this.KeyDownPlay(i);
				event.returnValue = false;event.cancel = true;
				return false;
			}
		}
		if(event.keyCode==9){piano.KeyDownPlay(0);event.returnValue = false;event.cancel = true;return false;}
		if(event.keyCode==20){piano.KeyDownPlay(0);event.returnValue = false;event.cancel = true;return false;}
		if(event.keyCode==37){piano.KeyDownPlay(18);event.returnValue = false;event.cancel = true;return false;}
		if(event.keyCode==40){piano.KeyDownPlay(19);event.returnValue = false;event.cancel = true;return false;}
		if(event.keyCode==38){piano.KeyDownPlay(20);event.returnValue = false;event.cancel = true;return false;}
		if(event.keyCode==39){piano.KeyDownPlay(22);event.returnValue = false;event.cancel = true;return false;}
		if(event.keyCode==86||event.keyCode==45){piano.KeyDownPlay(18);event.returnValue = false;event.cancel = true;return false;} 
		if(event.keyCode==66||event.keyCode==46||event.keyCode==188){piano.KeyDownPlay(19);event.returnValue = false;event.cancel = true;return false;} 
		if(event.keyCode==55||event.keyCode==36||event.keyCode==189||event.keyCode==57){piano.KeyDownPlay(20);event.returnValue = false;event.cancel = true;return false;} 
		if(event.keyCode==78||event.keyCode==35||event.keyCode==190){piano.KeyDownPlay(21);event.returnValue = false;event.cancel = true;return false;} 
		if(event.keyCode==56||event.keyCode==33||event.keyCode==187||event.keyCode==48){piano.KeyDownPlay(22);event.returnValue = false;event.cancel = true;return false;} 
		if(event.keyCode==77||event.keyCode==34||event.keyCode==191){piano.KeyDownPlay(23);event.returnValue = false;event.cancel = true;return false;}
	}
	this.keyup=function(){
		for (var i = 0; i < PCKeys.length; i++) {
			if(PCKeys[i]==event.keyCode){
				this.KeyUpPlay(i);
				event.returnValue = false;event.cancel = true;
				return false;
			}
		}
		if(event.keyCode==9){piano.KeyUpPlay(0);event.returnValue = false;event.cancel = true;return false;}
		if(event.keyCode==20){piano.KeyUpPlay(0);event.returnValue = false;event.cancel = true;return false;}
		if(event.keyCode==37){piano.KeyUpPlay(18);event.returnValue = false;event.cancel = true;return false;}
		if(event.keyCode==40){piano.KeyUpPlay(19);event.returnValue = false;event.cancel = true;return false;}
		if(event.keyCode==38){piano.KeyUpPlay(20);event.returnValue = false;event.cancel = true;return false;}
		if(event.keyCode==39){piano.KeyUpPlay(22);event.returnValue = false;event.cancel = true;return false;}	
		if(event.keyCode==86||event.keyCode==45){piano.KeyUpPlay(18);event.returnValue = false;event.cancel = true;return false;} 
		if(event.keyCode==66||event.keyCode==46||event.keyCode==188){piano.KeyUpPlay(19);event.returnValue = false;event.cancel = true;return false;} 
		if(event.keyCode==55||event.keyCode==36||event.keyCode==189||event.keyCode==57){piano.KeyUpPlay(20);event.returnValue = false;event.cancel = true;return false;} 
		if(event.keyCode==78||event.keyCode==35||event.keyCode==190){piano.KeyUpPlay(21);event.returnValue = false;event.cancel = true;return false;} 
		if(event.keyCode==56||event.keyCode==33||event.keyCode==187||event.keyCode==48){piano.KeyUpPlay(22);event.returnValue = false;event.cancel = true;return false;} 
		if(event.keyCode==77||event.keyCode==34||event.keyCode==191){piano.KeyUpPlay(23);event.returnValue = false;event.cancel = true;return false;}
	}
	//电源灯开关
	this.onfocus=function(){
		document.getElementById("PowerLightON").style.display="";
	}
	this.lostfocus=function(){
		document.getElementById("PowerLightON").style.display="none";
	}

	this.Power_MouseOut= function(){
		document.getElementById("tooltip").style.display="none";
		document.getElementById("div_update").style.display="";		
	}
	this.Power_MouseOver= function(){
		document.getElementById("tooltip").innerText=L_localizedStrings_Demo;
		document.getElementById("tooltip").style.display="";
		document.getElementById("div_update").style.display="none";		
	}
	this.checkState=function(){
		var imgBackground = document.getElementById("imgBackground");
    	if (System.Gadget.docked){
    		//document.body.className="docked";
    		document.body.style.width="133px";
    		document.body.style.height="74px";
    		imgBackground.style.width="133px";
    		imgBackground.style.height="74px";
    		document.getElementById("pagebody").className="docked";
    		imgBackground.src = "url(images/background_dock.png)";
    	}else{
    		//document.body.className="undocked";
    		document.body.style.width="592px";
    		document.body.style.height="260px";
    		imgBackground.style.width="592px";
    		imgBackground.style.height="260px";
    		document.getElementById("pagebody").className="undocked";
    		imgBackground.src = "url(images/background_undock.png)";
    	}
	}
	//打开Flyout
	this.ShowFlyout= function(){
		try {
				System.Gadget.Flyout.show = true;
				setTimeout("document.getElementById('key_1').focus();",100);
				//setTimeout("piano.ShowContent();",100);
				piano.flyout_load();
		} catch (err){}
	}
	//显示内容在Flyout
	this.ShowContent= function(){
		try {
			if (System.Gadget.Flyout.show) {
				var h="";
				for (var i = 0; i < Keys.length; i++) {
					h+="<div>"+Keys[i].KeyName+" "+piano.getKeyName(PCKeys[i])+"</div>";
				}
				System.Gadget.Flyout.document.getElementById("keyslist").innerHTML=h;				
			}else{setTimeout("piano.ShowFlyout();",500);}
		}catch (err){}
		piano.flyout_loaded();
	}
	this.getKeyName= function(i){
		if (i==32) return "Space";
		if (i==13) return "Enter";
		if (i==8) return "Backspace";
		if (i==38) return "Up";
		if (i==40) return "Down";
		if (i==37) return "Left";
		if (i==39) return "Right";
		if (i==9) return "Tab";
		if (i==33) return "PageUp";
		if (i==34) return "PageDown";
		if (i==45) return "Insert";
		if (i==46) return "Delete";
		if (i==27) return "Esc";
		if (i==35) return "End";
		if (i==36) return "Home";

		return String.fromCharCode(i);
	}
		/////////////////事件
	this.flyout_load= function(){}
	this.flyout_loaded= function(){}
	//////////演示
	this.PlayDemo= function(){
		var t=500;	
		piano.KeyDownPlay(11);	
		
		piano.onfocus();
		piano.ShowFlyout();
		document.getElementById("PowerLight").onclick=piano.ShowFlyout;	
		
		setTimeout("piano.KeyUpPlay(11);",t*0.9);
		setTimeout("piano.KeyDownPlay(11);",t);
		setTimeout("piano.KeyUpPlay(11);",t+t*0.9);
		setTimeout("piano.KeyDownPlay(12);",t*2);
		setTimeout("piano.KeyUpPlay(12);",t*2+t*0.9);
		setTimeout("piano.KeyDownPlay(14);",t*3);
		setTimeout("piano.KeyUpPlay(14);",t*3+t*0.9);
		setTimeout("piano.KeyDownPlay(14);",t*4);
		setTimeout("piano.KeyUpPlay(14);",t*4+t*0.9);
		setTimeout("piano.KeyDownPlay(12);",t*5);
		setTimeout("piano.KeyUpPlay(12);",t*5+t*0.9);
		setTimeout("piano.KeyDownPlay(11);",t*6);
		setTimeout("piano.KeyUpPlay(11);",t*6+t*0.9);
		setTimeout("piano.KeyDownPlay(9);",t*7);
		setTimeout("piano.KeyUpPlay(9);",t*7+t*0.9);
		setTimeout("piano.KeyDownPlay(7);",t*8);
		setTimeout("piano.KeyUpPlay(7);",t*8+t*0.9);
		setTimeout("piano.KeyDownPlay(7);",t*9);
		setTimeout("piano.KeyUpPlay(7);",t*9+t*0.9);
		setTimeout("piano.KeyDownPlay(9);",t*10);
		setTimeout("piano.KeyUpPlay(9);",t*10+t*0.9);
		setTimeout("piano.KeyDownPlay(11);",t*11);
		setTimeout("piano.KeyUpPlay(11);",t*11+t*0.9);
		setTimeout("piano.KeyDownPlay(11);",t*12);
		setTimeout("piano.KeyUpPlay(11);",t*12.5+t*0.9);
		setTimeout("piano.KeyDownPlay(9);",t*13.5);
		setTimeout("piano.KeyUpPlay(9);",t*13.5+t*0.45);
		setTimeout("piano.KeyDownPlay(9);",t*14);
		setTimeout("piano.KeyUpPlay(9);",t*14+t*1.9);

		setTimeout("piano.KeyDownPlay(11);",t*16);
		setTimeout("piano.KeyUpPlay(11);",t*16+t*0.9);
		setTimeout("piano.KeyDownPlay(11);",t*17);
		setTimeout("piano.KeyUpPlay(11);",t*17+t*0.9);
		setTimeout("piano.KeyDownPlay(12);",t*18);
		setTimeout("piano.KeyUpPlay(12);",t*18+t*0.9);
		setTimeout("piano.KeyDownPlay(14);",t*19);
		setTimeout("piano.KeyUpPlay(14);",t*19+t*0.9);
		setTimeout("piano.KeyDownPlay(14);",t*20);
		setTimeout("piano.KeyUpPlay(14);",t*20+t*0.9);
		setTimeout("piano.KeyDownPlay(12);",t*21);
		setTimeout("piano.KeyUpPlay(12);",t*21+t*0.9);
		setTimeout("piano.KeyDownPlay(11);",t*22);
		setTimeout("piano.KeyUpPlay(11);",t*22+t*0.9);
		setTimeout("piano.KeyDownPlay(9);",t*23);
		setTimeout("piano.KeyUpPlay(9);",t*23+t*0.9);
		setTimeout("piano.KeyDownPlay(7);",t*24);
		setTimeout("piano.KeyUpPlay(7);",t*24+t*0.9);
		setTimeout("piano.KeyDownPlay(7);",t*25);
		setTimeout("piano.KeyUpPlay(7);",t*25+t*0.9);
		setTimeout("piano.KeyDownPlay(9);",t*26);
		setTimeout("piano.KeyUpPlay(9);",t*26+t*0.9);
		setTimeout("piano.KeyDownPlay(11);",t*27);
		setTimeout("piano.KeyUpPlay(11);",t*27+t*0.9);
		setTimeout("piano.KeyDownPlay(9);",t*28);
		setTimeout("piano.KeyUpPlay(9);",t*28.5+t*0.9);
		setTimeout("piano.KeyDownPlay(7);",t*29.5);
		setTimeout("piano.KeyUpPlay(7);",t*29.5+t*0.45);
		setTimeout("piano.KeyDownPlay(7);",t*30);
		setTimeout("piano.KeyUpPlay(7);",t*30+t*1.9);
		
		setTimeout("piano.lostfocus();document.getElementById('PowerLight').onclick=piano.PlayDemo;",t*32);
	}
}
try{
	document.getElementById("div_update").innerText=System.Gadget.name;
}catch(err){}

var PCKeys=new Array();
function SetKeyCodeArray(lang){
if (lang=="en"){
	var KNi=-1;
	KNi+=1;PCKeys[KNi]=32; 
	KNi+=1;PCKeys[KNi]=81; 
	KNi+=1;PCKeys[KNi]=65; 
	KNi+=1;PCKeys[KNi]=87; 
	KNi+=1;PCKeys[KNi]=83; 
	KNi+=1;PCKeys[KNi]=69; 
	KNi+=1;PCKeys[KNi]=68; 
	KNi+=1;PCKeys[KNi]=70; 
	KNi+=1;PCKeys[KNi]=84; 
	KNi+=1;PCKeys[KNi]=71; 
	KNi+=1;PCKeys[KNi]=89; 
	KNi+=1;PCKeys[KNi]=72; 
	KNi+=1;PCKeys[KNi]=74; 
	KNi+=1;PCKeys[KNi]=73; 
	KNi+=1;PCKeys[KNi]=75; 
	KNi+=1;PCKeys[KNi]=79; 
	KNi+=1;PCKeys[KNi]=76; 
	KNi+=1;PCKeys[KNi]=80; 
	KNi+=1;PCKeys[KNi]=186; 
	KNi+=1;PCKeys[KNi]=222; 
	KNi+=1;PCKeys[KNi]=221; 
	KNi+=1;PCKeys[KNi]=13; 
	KNi+=1;PCKeys[KNi]=220; 
	KNi+=1;PCKeys[KNi]=8;
return 0;
}
if (lang=="cs"){
	var KNi=-1;
KNi+=1;PCKeys[KNi]=32; 
KNi+=1;PCKeys[KNi]=81; 
KNi+=1;PCKeys[KNi]=65; 
KNi+=1;PCKeys[KNi]=87; 
KNi+=1;PCKeys[KNi]=83; 
KNi+=1;PCKeys[KNi]=69; 
KNi+=1;PCKeys[KNi]=68; 
KNi+=1;PCKeys[KNi]=70; 
KNi+=1;PCKeys[KNi]=84; 
KNi+=1;PCKeys[KNi]=71; 
KNi+=1;PCKeys[KNi]=89; 
KNi+=1;PCKeys[KNi]=72; 
KNi+=1;PCKeys[KNi]=74; 
KNi+=1;PCKeys[KNi]=73; 
KNi+=1;PCKeys[KNi]=75; 
KNi+=1;PCKeys[KNi]=79; 
KNi+=1;PCKeys[KNi]=76; 
KNi+=1;PCKeys[KNi]=80; 
KNi+=1;PCKeys[KNi]=186; 
KNi+=1;PCKeys[KNi]=222; 
KNi+=1;PCKeys[KNi]=219; 
KNi+=1;PCKeys[KNi]=13; 
KNi+=1;PCKeys[KNi]=221; 
KNi+=1;PCKeys[KNi]=8; return 0;
}
if (lang=="da"){
	var KNi=-1;
KNi+=1;PCKeys[KNi]=32; 
KNi+=1;PCKeys[KNi]=81; 
KNi+=1;PCKeys[KNi]=65; 
KNi+=1;PCKeys[KNi]=87; 
KNi+=1;PCKeys[KNi]=83; 
KNi+=1;PCKeys[KNi]=69; 
KNi+=1;PCKeys[KNi]=68; 
KNi+=1;PCKeys[KNi]=70; 
KNi+=1;PCKeys[KNi]=84; 
KNi+=1;PCKeys[KNi]=71; 
KNi+=1;PCKeys[KNi]=89; 
KNi+=1;PCKeys[KNi]=72; 
KNi+=1;PCKeys[KNi]=74; 
KNi+=1;PCKeys[KNi]=73; 
KNi+=1;PCKeys[KNi]=75; 
KNi+=1;PCKeys[KNi]=79; 
KNi+=1;PCKeys[KNi]=76; 
KNi+=1;PCKeys[KNi]=80; 
KNi+=1;PCKeys[KNi]=192; 
KNi+=1;PCKeys[KNi]=222; 
KNi+=1;PCKeys[KNi]=221; 
KNi+=1;PCKeys[KNi]=191; 
KNi+=1;PCKeys[KNi]=8; 
KNi+=1;PCKeys[KNi]=13;return 0;
}
if (lang=="de"){
	var KNi=-1;
KNi+=1;PCKeys[KNi]=32; 
KNi+=1;PCKeys[KNi]=81; 
KNi+=1;PCKeys[KNi]=65; 
KNi+=1;PCKeys[KNi]=87; 
KNi+=1;PCKeys[KNi]=83; 
KNi+=1;PCKeys[KNi]=69; 
KNi+=1;PCKeys[KNi]=68; 
KNi+=1;PCKeys[KNi]=70; 
KNi+=1;PCKeys[KNi]=84; 
KNi+=1;PCKeys[KNi]=71; 
KNi+=1;PCKeys[KNi]=90; 
KNi+=1;PCKeys[KNi]=72; 
KNi+=1;PCKeys[KNi]=74; 
KNi+=1;PCKeys[KNi]=73; 
KNi+=1;PCKeys[KNi]=75; 
KNi+=1;PCKeys[KNi]=79; 
KNi+=1;PCKeys[KNi]=76; 
KNi+=1;PCKeys[KNi]=80; 
KNi+=1;PCKeys[KNi]=192; 
KNi+=1;PCKeys[KNi]=222; 
KNi+=1;PCKeys[KNi]=187; 
KNi+=1;PCKeys[KNi]=191; 
KNi+=1;PCKeys[KNi]=8; 
KNi+=1;PCKeys[KNi]=13;
return 0;
}
if (lang=="el"){
	var KNi=-1;
KNi+=1;PCKeys[KNi]=32; 
KNi+=1;PCKeys[KNi]=81; 
KNi+=1;PCKeys[KNi]=65; 
KNi+=1;PCKeys[KNi]=87; 
KNi+=1;PCKeys[KNi]=83; 
KNi+=1;PCKeys[KNi]=69; 
KNi+=1;PCKeys[KNi]=68; 
KNi+=1;PCKeys[KNi]=70; 
KNi+=1;PCKeys[KNi]=84; 
KNi+=1;PCKeys[KNi]=71; 
KNi+=1;PCKeys[KNi]=89; 
KNi+=1;PCKeys[KNi]=72; 
KNi+=1;PCKeys[KNi]=74; 
KNi+=1;PCKeys[KNi]=73; 
KNi+=1;PCKeys[KNi]=75; 
KNi+=1;PCKeys[KNi]=79; 
KNi+=1;PCKeys[KNi]=76; 
KNi+=1;PCKeys[KNi]=80; 
KNi+=1;PCKeys[KNi]=222; 
KNi+=1;PCKeys[KNi]=220; 
KNi+=1;PCKeys[KNi]=219; 
KNi+=1;PCKeys[KNi]=13; 
KNi+=1;PCKeys[KNi]=221; 
KNi+=1;PCKeys[KNi]=8;return 0;
}
if (lang=="es"){
	var KNi=-1;
KNi+=1;PCKeys[KNi]=32; 
KNi+=1;PCKeys[KNi]=81; 
KNi+=1;PCKeys[KNi]=65; 
KNi+=1;PCKeys[KNi]=87; 
KNi+=1;PCKeys[KNi]=83; 
KNi+=1;PCKeys[KNi]=69; 
KNi+=1;PCKeys[KNi]=68; 
KNi+=1;PCKeys[KNi]=70; 
KNi+=1;PCKeys[KNi]=84; 
KNi+=1;PCKeys[KNi]=71; 
KNi+=1;PCKeys[KNi]=89; 
KNi+=1;PCKeys[KNi]=72; 
KNi+=1;PCKeys[KNi]=74; 
KNi+=1;PCKeys[KNi]=73; 
KNi+=1;PCKeys[KNi]=75; 
KNi+=1;PCKeys[KNi]=79; 
KNi+=1;PCKeys[KNi]=76; 
KNi+=1;PCKeys[KNi]=80; 
KNi+=1;PCKeys[KNi]=192; 
KNi+=1;PCKeys[KNi]=191; 
KNi+=1;PCKeys[KNi]=187; 
KNi+=1;PCKeys[KNi]=13; 
KNi+=1;PCKeys[KNi]=221; 
KNi+=1;PCKeys[KNi]=8;return 0;
}
if (lang=="fr"){
	var KNi=-1;
KNi+=1;PCKeys[KNi]=32; 
KNi+=1;PCKeys[KNi]=65; 
KNi+=1;PCKeys[KNi]=81; 
KNi+=1;PCKeys[KNi]=90; 
KNi+=1;PCKeys[KNi]=83; 
KNi+=1;PCKeys[KNi]=69; 
KNi+=1;PCKeys[KNi]=68; 
KNi+=1;PCKeys[KNi]=70; 
KNi+=1;PCKeys[KNi]=84; 
KNi+=1;PCKeys[KNi]=71; 
KNi+=1;PCKeys[KNi]=89; 
KNi+=1;PCKeys[KNi]=72; 
KNi+=1;PCKeys[KNi]=74; 
KNi+=1;PCKeys[KNi]=73; 
KNi+=1;PCKeys[KNi]=75; 
KNi+=1;PCKeys[KNi]=79; 
KNi+=1;PCKeys[KNi]=76; 
KNi+=1;PCKeys[KNi]=80; 
KNi+=1;PCKeys[KNi]=77; 
KNi+=1;PCKeys[KNi]=192; 
KNi+=1;PCKeys[KNi]=186; 
KNi+=1;PCKeys[KNi]=220; 
KNi+=1;PCKeys[KNi]=8; 
KNi+=1;PCKeys[KNi]=13;
return 0;
}
if (lang=="hu"){
	var KNi=-1;
KNi+=1;PCKeys[KNi]=32; 
KNi+=1;PCKeys[KNi]=81; 
KNi+=1;PCKeys[KNi]=65; 
KNi+=1;PCKeys[KNi]=87; 
KNi+=1;PCKeys[KNi]=83; 
KNi+=1;PCKeys[KNi]=69; 
KNi+=1;PCKeys[KNi]=68; 
KNi+=1;PCKeys[KNi]=70; 
KNi+=1;PCKeys[KNi]=84; 
KNi+=1;PCKeys[KNi]=71; 
KNi+=1;PCKeys[KNi]=90; 
KNi+=1;PCKeys[KNi]=72; 
KNi+=1;PCKeys[KNi]=74; 
KNi+=1;PCKeys[KNi]=73; 
KNi+=1;PCKeys[KNi]=75; 
KNi+=1;PCKeys[KNi]=79; 
KNi+=1;PCKeys[KNi]=76; 
KNi+=1;PCKeys[KNi]=80; 
KNi+=1;PCKeys[KNi]=186; 
KNi+=1;PCKeys[KNi]=222; 
KNi+=1;PCKeys[KNi]=221; 
KNi+=1;PCKeys[KNi]=220; 
KNi+=1;PCKeys[KNi]=8; 
KNi+=1;PCKeys[KNi]=13;return 0;
}
if (lang=="it"){
	var KNi=-1;
KNi+=1;PCKeys[KNi]=32; 
KNi+=1;PCKeys[KNi]=81; 
KNi+=1;PCKeys[KNi]=65; 
KNi+=1;PCKeys[KNi]=87; 
KNi+=1;PCKeys[KNi]=83; 
KNi+=1;PCKeys[KNi]=69; 
KNi+=1;PCKeys[KNi]=68; 
KNi+=1;PCKeys[KNi]=70; 
KNi+=1;PCKeys[KNi]=84; 
KNi+=1;PCKeys[KNi]=71; 
KNi+=1;PCKeys[KNi]=89; 
KNi+=1;PCKeys[KNi]=72; 
KNi+=1;PCKeys[KNi]=74; 
KNi+=1;PCKeys[KNi]=73; 
KNi+=1;PCKeys[KNi]=75; 
KNi+=1;PCKeys[KNi]=79; 
KNi+=1;PCKeys[KNi]=76; 
KNi+=1;PCKeys[KNi]=80; 
KNi+=1;PCKeys[KNi]=192; 
KNi+=1;PCKeys[KNi]=222; 
KNi+=1;PCKeys[KNi]=187; 
KNi+=1;PCKeys[KNi]=191; 
KNi+=1;PCKeys[KNi]=8; 
KNi+=1;PCKeys[KNi]=13;return 0;
}
if (lang=="lv"){
	var KNi=-1;
KNi+=1;PCKeys[KNi]=32; 
KNi+=1;PCKeys[KNi]=81; 
KNi+=1;PCKeys[KNi]=186; 
KNi+=1;PCKeys[KNi]=71; 
KNi+=1;PCKeys[KNi]=85; 
KNi+=1;PCKeys[KNi]=74; 
KNi+=1;PCKeys[KNi]=83; 
KNi+=1;PCKeys[KNi]=73; 
KNi+=1;PCKeys[KNi]=77; 
KNi+=1;PCKeys[KNi]=76; 
KNi+=1;PCKeys[KNi]=86; 
KNi+=1;PCKeys[KNi]=68; 
KNi+=1;PCKeys[KNi]=65; 
KNi+=1;PCKeys[KNi]=90; 
KNi+=1;PCKeys[KNi]=84; 
KNi+=1;PCKeys[KNi]=87; 
KNi+=1;PCKeys[KNi]=69; 
KNi+=1;PCKeys[KNi]=88; 
KNi+=1;PCKeys[KNi]=67; 
KNi+=1;PCKeys[KNi]=220; 
KNi+=1;PCKeys[KNi]=72; 
KNi+=1;PCKeys[KNi]=13; 
KNi+=1;PCKeys[KNi]=70; 
KNi+=1;PCKeys[KNi]=8;return 0;
}
if (lang=="nl"){
	var KNi=-1;
KNi+=1;PCKeys[KNi]=32; 
KNi+=1;PCKeys[KNi]=81; 
KNi+=1;PCKeys[KNi]=65; 
KNi+=1;PCKeys[KNi]=87; 
KNi+=1;PCKeys[KNi]=83; 
KNi+=1;PCKeys[KNi]=69; 
KNi+=1;PCKeys[KNi]=68; 
KNi+=1;PCKeys[KNi]=70; 
KNi+=1;PCKeys[KNi]=84; 
KNi+=1;PCKeys[KNi]=71; 
KNi+=1;PCKeys[KNi]=89; 
KNi+=1;PCKeys[KNi]=72; 
KNi+=1;PCKeys[KNi]=74; 
KNi+=1;PCKeys[KNi]=73; 
KNi+=1;PCKeys[KNi]=75; 
KNi+=1;PCKeys[KNi]=79; 
KNi+=1;PCKeys[KNi]=76; 
KNi+=1;PCKeys[KNi]=80; 
KNi+=1;PCKeys[KNi]=187; 
KNi+=1;PCKeys[KNi]=220; 
KNi+=1;PCKeys[KNi]=186; 
KNi+=1;PCKeys[KNi]=13; 
KNi+=1;PCKeys[KNi]=191; 
KNi+=1;PCKeys[KNi]=8; 
 return 0;
}
if (lang=="pt-BR"){
	var KNi=-1;
KNi+=1;PCKeys[KNi]=32; 
KNi+=1;PCKeys[KNi]=81; 
KNi+=1;PCKeys[KNi]=65; 
KNi+=1;PCKeys[KNi]=87; 
KNi+=1;PCKeys[KNi]=83; 
KNi+=1;PCKeys[KNi]=69; 
KNi+=1;PCKeys[KNi]=68; 
KNi+=1;PCKeys[KNi]=70; 
KNi+=1;PCKeys[KNi]=84; 
KNi+=1;PCKeys[KNi]=71; 
KNi+=1;PCKeys[KNi]=89; 
KNi+=1;PCKeys[KNi]=72; 
KNi+=1;PCKeys[KNi]=74; 
KNi+=1;PCKeys[KNi]=73; 
KNi+=1;PCKeys[KNi]=75; 
KNi+=1;PCKeys[KNi]=79; 
KNi+=1;PCKeys[KNi]=76; 
KNi+=1;PCKeys[KNi]=80; 
KNi+=1;PCKeys[KNi]=186; 
KNi+=1;PCKeys[KNi]=220; 
KNi+=1;PCKeys[KNi]=221; 
KNi+=1;PCKeys[KNi]=13; 
KNi+=1;PCKeys[KNi]=187; 
KNi+=1;PCKeys[KNi]=8;return 0;
}

if (lang=="pt"){
	var KNi=-1;
KNi+=1;PCKeys[KNi]=32; 
KNi+=1;PCKeys[KNi]=81; 
KNi+=1;PCKeys[KNi]=65; 
KNi+=1;PCKeys[KNi]=87; 
KNi+=1;PCKeys[KNi]=83; 
KNi+=1;PCKeys[KNi]=69; 
KNi+=1;PCKeys[KNi]=68; 
KNi+=1;PCKeys[KNi]=70; 
KNi+=1;PCKeys[KNi]=84; 
KNi+=1;PCKeys[KNi]=71; 
KNi+=1;PCKeys[KNi]=89; 
KNi+=1;PCKeys[KNi]=72; 
KNi+=1;PCKeys[KNi]=74; 
KNi+=1;PCKeys[KNi]=73; 
KNi+=1;PCKeys[KNi]=75; 
KNi+=1;PCKeys[KNi]=79; 
KNi+=1;PCKeys[KNi]=76; 
KNi+=1;PCKeys[KNi]=80; 
KNi+=1;PCKeys[KNi]=192; 
KNi+=1;PCKeys[KNi]=222; 
KNi+=1;PCKeys[KNi]=187; 
KNi+=1;PCKeys[KNi]=13; 
KNi+=1;PCKeys[KNi]=221; 
KNi+=1;PCKeys[KNi]=8;return 0;
}
if (lang=="ro"){
	var KNi=-1;
KNi+=1;PCKeys[KNi]=32; 
KNi+=1;PCKeys[KNi]=81; 
KNi+=1;PCKeys[KNi]=65; 
KNi+=1;PCKeys[KNi]=87; 
KNi+=1;PCKeys[KNi]=83; 
KNi+=1;PCKeys[KNi]=69; 
KNi+=1;PCKeys[KNi]=68; 
KNi+=1;PCKeys[KNi]=70; 
KNi+=1;PCKeys[KNi]=84; 
KNi+=1;PCKeys[KNi]=71; 
KNi+=1;PCKeys[KNi]=89; 
KNi+=1;PCKeys[KNi]=72; 
KNi+=1;PCKeys[KNi]=74; 
KNi+=1;PCKeys[KNi]=73; 
KNi+=1;PCKeys[KNi]=75; 
KNi+=1;PCKeys[KNi]=79; 
KNi+=1;PCKeys[KNi]=76; 
KNi+=1;PCKeys[KNi]=80; 
KNi+=1;PCKeys[KNi]=186; 
KNi+=1;PCKeys[KNi]=222; 
KNi+=1;PCKeys[KNi]=221; 
KNi+=1;PCKeys[KNi]=220; 
KNi+=1;PCKeys[KNi]=8; 
KNi+=1;PCKeys[KNi]=13;return 0;
}
if (lang=="tr"){
	var KNi=-1;
KNi+=1;PCKeys[KNi]=32; 
KNi+=1;PCKeys[KNi]=81; 
KNi+=1;PCKeys[KNi]=65; 
KNi+=1;PCKeys[KNi]=87; 
KNi+=1;PCKeys[KNi]=83; 
KNi+=1;PCKeys[KNi]=69; 
KNi+=1;PCKeys[KNi]=68; 
KNi+=1;PCKeys[KNi]=70; 
KNi+=1;PCKeys[KNi]=84; 
KNi+=1;PCKeys[KNi]=71; 
KNi+=1;PCKeys[KNi]=89; 
KNi+=1;PCKeys[KNi]=72; 
KNi+=1;PCKeys[KNi]=74; 
KNi+=1;PCKeys[KNi]=73; 
KNi+=1;PCKeys[KNi]=75; 
KNi+=1;PCKeys[KNi]=79; 
KNi+=1;PCKeys[KNi]=76; 
KNi+=1;PCKeys[KNi]=80; 
KNi+=1;PCKeys[KNi]=186; 
KNi+=1;PCKeys[KNi]=222; 
KNi+=1;PCKeys[KNi]=221; 
KNi+=1;PCKeys[KNi]=188; 
KNi+=1;PCKeys[KNi]=8; 
KNi+=1;PCKeys[KNi]=13;
return 0;
}

if (lang=="tr-F"){
	var KNi=-1;
KNi+=1;PCKeys[KNi]=32; 
KNi+=1;PCKeys[KNi]=70; 
KNi+=1;PCKeys[KNi]=85; 
KNi+=1;PCKeys[KNi]=71; 
KNi+=1;PCKeys[KNi]=219; 
KNi+=1;PCKeys[KNi]=186; 
KNi+=1;PCKeys[KNi]=69; 
KNi+=1;PCKeys[KNi]=65; 
KNi+=1;PCKeys[KNi]=79; 
KNi+=1;PCKeys[KNi]=221; 
KNi+=1;PCKeys[KNi]=68; 
KNi+=1;PCKeys[KNi]=84; 
KNi+=1;PCKeys[KNi]=75; 
KNi+=1;PCKeys[KNi]=78; 
KNi+=1;PCKeys[KNi]=77; 
KNi+=1;PCKeys[KNi]=72; 
KNi+=1;PCKeys[KNi]=76; 
KNi+=1;PCKeys[KNi]=80; 
KNi+=1;PCKeys[KNi]=89; 
KNi+=1;PCKeys[KNi]=222; 
KNi+=1;PCKeys[KNi]=87; 
KNi+=1;PCKeys[KNi]=88; 
KNi+=1;PCKeys[KNi]=8; 
KNi+=1;PCKeys[KNi]=13;return 0;
}
	var KNi=-1;
	KNi+=1;PCKeys[KNi]=32; 
	KNi+=1;PCKeys[KNi]=81; 
	KNi+=1;PCKeys[KNi]=65; 
	KNi+=1;PCKeys[KNi]=87; 
	KNi+=1;PCKeys[KNi]=83; 
	KNi+=1;PCKeys[KNi]=69; 
	KNi+=1;PCKeys[KNi]=68; 
	KNi+=1;PCKeys[KNi]=70; 
	KNi+=1;PCKeys[KNi]=84; 
	KNi+=1;PCKeys[KNi]=71; 
	KNi+=1;PCKeys[KNi]=89; 
	KNi+=1;PCKeys[KNi]=72; 
	KNi+=1;PCKeys[KNi]=74; 
	KNi+=1;PCKeys[KNi]=73; 
	KNi+=1;PCKeys[KNi]=75; 
	KNi+=1;PCKeys[KNi]=79; 
	KNi+=1;PCKeys[KNi]=76; 
	KNi+=1;PCKeys[KNi]=80; 
	KNi+=1;PCKeys[KNi]=186; 
	KNi+=1;PCKeys[KNi]=222; 
	KNi+=1;PCKeys[KNi]=221; 
	KNi+=1;PCKeys[KNi]=13; 
	KNi+=1;PCKeys[KNi]=220; 
	KNi+=1;PCKeys[KNi]=8;
return 0;

}

