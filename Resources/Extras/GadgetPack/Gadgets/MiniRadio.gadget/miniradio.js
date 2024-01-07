    function doNothing(){return true}
    window.onerror =doNothing
	
	
	var vol=50;
	var completeRegel = '';
	var huidigeIndex;
	var huidigeUrl;
	var huidigeZendernaam;
	var populateKeuze = '2';
	var schermRegel = '';
	var mapNaam = '';
	var frontje = 0;
	var rps = 0;
	var logosetting = 'leeg';
	var huidigeRPtitel = '';
	var tempRPtitel = '';
	var runnedOnce = '0';
	var sorteerEx = '0';
	var externeZendersAlGetoond = false;
	var autoSort = false;
	var wmps = '';
	var inladenGadget = '0';
	var oPopup = window.createPopup();


    System.Gadget.settingsUI = "instellingen.html";
    System.Gadget.onSettingsClosed = settingsClosed;
	System.Gadget.onUndock = unDocked;
	System.Gadget.onDock = docked;
	

    pakMapNaam();
	
	  function inlezenInstellingen(){
	    huidigeIndex = System.Gadget.Settings.read("huidigeIndex");
		 document.getElementById('selector').selectedIndex = huidigeIndex;
		 var huidigeVol = System.Gadget.Settings.read("huidigeVol");
		 if (huidigeVol!=''){
		   vol = huidigeVol;
		   document.getElementById('mediaPlayer').settings.volume=huidigeVol;
		 } else {document.getElementById('mediaPlayer').settings.volume=vol;}
		 huidigeUrl = System.Gadget.Settings.read("huidigeUrl");
	     if (huidigeUrl!=''){
		   document.getElementById('mediaPlayer').URL = huidigeUrl;
		 }
		 bepaalFrontje();
		 soortAfbeelding(); // Moet dit hier staan ??? ingevoegd 29 maart 2008
		 document.getElementById('mediaPlayer').controls.stop();
		 inlezenExtraInst();
		 checkRealPlayer();
		 document.getElementById('RealPlayer').DoStop();
		 //if (rps==1 && logoSetting=='radioGroot'){document.getElementById('RealPlayer').style.visibility='hidden';} 
      }

	   
	  

	  function pakMapNaam(){
	    mapNaam = System.Gadget.path;
		var nr = mapNaam.lastIndexOf("\\");
		mapNaam = mapNaam.substr(0,nr);
	  }
	  
	  
	  
	  
	function indexeer(){
	  document.getElementById('selector').focus();
      var x=document.getElementById("selector");
      document.getElementById('mediaPlayer').URL=x.options[x.selectedIndex].value;
	  huidigeIndex = document.getElementById('selector').selectedIndex;
	  huidigeUrl = x.options[x.selectedIndex].value; //??
	  huidigeZendernaam = x.options[x.selectedIndex].text;
	  System.Gadget.Settings.write("huidigeIndex", huidigeIndex);
	  System.Gadget.Settings.write("huidigeUrl", huidigeUrl);
    }	
	  

	  function bepaalZender(){
	    indexeer(); //bij RP nog speler naar voren toveren bij inladen Sidebar
		if (huidigeUrl=='999'){
		  var rand_no = Math.ceil((document.getElementById('selector').length-1)*Math.random());
          document.getElementById('selector').selectedIndex = rand_no;
		  indexeer();
		}
	    checkRealPlayer();
	    showSelector();
		soortAfbeelding();
		document.getElementById('selector').blur();
	  }
	  
	  
	  function checkRealPlayer(){
	    document.getElementById('RealPlayer').DoStop();
	    //document.getElementById('RealPlayer').style.visibility='hidden';
	    var x=document.getElementById("selector");
		var urlstream = x.options[x.selectedIndex].value;
		if (urlstream.substr(0,10)=='webhttp://'){ // user made url
		  System.Shell.execute(urlstream.substr(3));
		  return;
		}
		rps = 0;
		var geldigeExtensies = " .ra .rm .ram  .rpm .rv .pls ";
		var extensie = " " + urlstream.substring(urlstream.lastIndexOf(".")).toLowerCase() + " ";
		if (urlstream.indexOf(".pls?")>0){rps=1;}
        if (geldigeExtensies.indexOf(extensie) > -1) {
          rps = 1;
        }
   		if (rps==1){  //RealPlayer stream
		  stoppen();
		  document.getElementById('frontje').style.visibility = 'hidden';
		  //document.getElementById('RealPlayer').style.visibility='visible';
		  document.getElementById('realLogo').style.visibility='visible';
		  document.getElementById('RealPlayer').SetSource(x.options[x.selectedIndex].value);
		  document.getElementById('RealPlayer').DoPlay();
		  checkTitle();
		}
		if (rps==0){  //WMP stream
		  //afspelen();  //autoplay=true, dus hoeft niet ...?
		  document.getElementById('realLogo').style.visibility='hidden';
		}
	  }
	  
	  
	  
	  function afspelen(){
	    if (huidigeUrl==''){bepaalZender();}
		if(rps==1){document.getElementById('RealPlayer').DoPlay(); return}
	    document.getElementById('mediaPlayer').controls.play();
	  }  
	  
	  function stoppen(){
	    if(rps==1){document.getElementById('RealPlayer').DoStop(); return}
	    document.getElementById('mediaPlayer').controls.stop();
	  }  
	  
	  
	  
	  
	  
		
	  function soortAfbeelding(){
	    bepaalFrontje();
        logoSetting = System.Gadget.Settings.read("logo");
        if (logoSetting=='radioKlein'){
		  document.getElementById('smooth').style.visibility = 'visible';   // omhulsel tonen !
		  document.getElementById('mediaPlayer').style.visibility='hidden';
		  document.getElementById('evt').innerHTML='<img src="mrlogo.png" style="position:absolute;top:0px;left:0px;z-index:33">';
		  document.getElementById('totaal').style.visibility='visible';
		  document.getElementById('frontje').style.visibility = 'hidden';
		}
		
		if (logoSetting=='leeg'){
		  document.getElementById('smooth').style.visibility = 'visible';   // omhulsel tonen !
  	      document.getElementById('totaal').style.visibility='visible';
	      document.getElementById('zwart').style.visibility='visible';
		  document.getElementById('mediaPlayer').style.visibility='visible';
		  //if (rps==1){document.getElementById('RealPlayer').style.visibility='visible';}
		  document.getElementById('evt').innerHTML='';
		  
		}	
						
		if (logoSetting=='radioGroot'){
		  document.getElementById('smooth').style.visibility = 'hidden';   // geen omhulsel tonen !
		  document.getElementById('mediaPlayer').style.visibility='hidden';
          document.getElementById('totaal').style.visibility='hidden';
		  document.getElementById('zwart').style.visibility='hidden';
		  document.getElementById('frontje').style.visibility = 'hidden';
		  document.getElementById('realLogo').style.visibility = 'hidden';
		  document.getElementById('evt').innerHTML='<img src="radio.png" style="position:absolute;top:10px;left:0px;z-index:3" onclick="javascript:radioPicKlik()">';
		}
		//if (rps==1 && logoSetting=='leeg'){document.getElementById('RealPlayer').style.visibility='visible';}
		//if (rps==1 && logoSetting=='radioGroot'){document.getElementById('RealPlayer').style.visibility='hidden';}
      }

	 
		
		
      function settingsClosed(){
	    soortAfbeelding();
	    inlezenZenders();
		inlezenExtraInst();
		
	  }
	  
	  
	  
	  
	  function bepaalFrontje(){
	    frontje = System.Gadget.Settings.read("frontje");
		frontBestandsnaam = System.Gadget.Settings.read("frontBestandsnaam");
		if (frontje==0){
		  document.getElementById('frontje').style.visibility = 'hidden';
		}
	    if (frontje > 0){
		  document.getElementById('frontje').style.visibility = 'visible';
		  document.getElementById('frontje').style.height = 50;
		  if (frontje>14){
			document.getElementById('frontje').src = mapNaam + '\\FrontjesMiniRadio\\' + frontBestandsnaam;
		  }
		  else {document.getElementById('frontje').src = 'frontje' + frontje + '.png';}
		}
	  }

	  
	  
	  function showSelector(){ //met een frontje kun je niet rechtstreeks een andere zender kiezen
	    if (document.getElementById('frontje').style.height=='29px'){
		  document.getElementById('frontje').style.height = 50;
		  return false;
		} else
        document.getElementById('frontje').style.height=29;
	  }


      function kijkGrootte(){
	    if (document.getElementById('frontje').style.height=='29px'){
		  document.getElementById('frontje').style.height = 50;
		}
	  }
	  
	  
	  function harder(){
	    if (rps==1){document.getElementById('RealPlayer').SetVolume(vol);}
	    if (vol < 20){
		  vol = vol + 2;
		} else {vol = vol+5;}
		if (vol>100){vol=100;}
		if(rps==1){document.getElementById('RealPlayer').SetVolume(vol)}
		else { document.getElementById('mediaPlayer').settings.volume=vol; }
		toonVolumeStand();
		setTimeout("document.getElementById('VolStandTekst').innerText='';",3000);
		System.Gadget.Settings.write("huidigeVol", vol);
	  }
	  
	  
	  function zachter(){
	    if (rps==1){document.getElementById('RealPlayer').SetVolume(vol);}
	    if (vol < 21){
		 vol = vol - 2;
		} else { vol = vol-5; }
		if (vol<0){vol=0;}
		if(rps==1){document.getElementById('RealPlayer').SetVolume(vol)}
		else { document.getElementById('mediaPlayer').settings.volume=vol; }
		toonVolumeStand();
		setTimeout("document.getElementById('VolStandTekst').innerText='';",3000);
	    System.Gadget.Settings.write("huidigeVol", vol);
	  }
	
		
		
		
	  function toonVolumeStand(){
	    document.getElementById('VolStandTekst').innerText=vol;
	  }
	

	  function muisVolume(){
		if (event.wheelDelta >= 20){ harder();}
		else{if (event.wheelDelta <= -20){
		       zachter();
			}
		}
	  }
	
	
      function plusOver(){
        document.getElementById('plus').style.visibility='visible';
      }	  
	  
	  function plusOut(){
        document.getElementById('plus').style.visibility='hidden';
      }	  
	  
	  function minOver(){
        document.getElementById('min').style.visibility='visible';
      }	  
	  
	  function minOut(){
        document.getElementById('min').style.visibility='hidden';
      }	  
	  
	  
	  
	  function fileDragDropped() {
        var  sFile;
        for (var i=0; System.Shell.itemFromFileDrop(event.dataTransfer, i) != null; i++){
          sFile = System.Shell.itemFromFileDrop(event.dataTransfer, i).path;
		  //document.getElementById('rs').options[document.getElementById('rs').options.length]=new Option(sFile,false,false);
		  document.getElementById('selector').focus();
          document.getElementById('selector').options[document.getElementById('selector').options.length]=new Option(sFile,sFile);
		  document.getElementById('selector').selectedIndex = document.getElementById('selector').options.length-1;
		  huidigeIndex = document.getElementById('selector').selectedIndex;
		  document.getElementById('mediaPlayer').URL = sFile;
        }
      }
	  
	  	  
  
      function radioPicKlik(){
	    document.getElementById('smooth').style.visibility = 'visible';   // omhulsel tonen !
	    document.getElementById('totaal').style.visibility='visible';
	    document.getElementById('zwart').style.visibility='visible';
		if (rps==0){document.getElementById('mediaPlayer').style.visibility='visible';}
		//if (rps==1){document.getElementById('RealPlayer').style.visibility='visible';}
		if (rps==1){document.getElementById('realLogo').style.visibility='visible';}
		document.getElementById('evt').innerHTML='';
	  }
	  
  
	  
  
	  function inlezenZenders(){
	    var zenderRegel;
	    var zenderNaam;
	    var zenderAdres;
		var alleZenders = '';
	  
	    mapNaam = System.Gadget.path;
		var nr = mapNaam.lastIndexOf("\\");
		mapNaam = mapNaam.substr(0,nr);
		
		bijwerken();

	    var zendersBestand = mapNaam + '\\ZenderData2.txt';
        var fso, f, r;
        var ForReading = 1, ForWriting = 2;
        fso = new ActiveXObject("Scripting.FileSystemObject");
		if (fso.FileExists(zendersBestand)){
          f = fso.OpenTextFile(zendersBestand, ForReading);
		  try{
            r =  f.ReadLine();
          }
          catch(err)
          {
            r="dummy";
          }
	      alleZenders = r;
		  f.Close();
		}
		
		
	    if (alleZenders == ""){
		  if (externeZendersAlGetoond == false){  //Als dit er niet staat, dan worden elke keer opnieuw de externe zenders aan de lijst toegevoegd. Geldt alleen indien Meegeleverde zenders nog niet zijn opgeslagen.
		    eigenZendersNaarSystem();  
		  }
		  externeZendersAlGetoond = true;
		  return false;
		}
	   
        document.getElementById('selector').options.length=0;  // wis de complete lijst

	    if (alleZenders != ""){
          var zenderLijst = alleZenders.split("#", alleZenders.length);
	      for (var i=1 ; i < zenderLijst.length; i++){   //???
		    zenderRegel = zenderLijst[i].split("*", zenderLijst[i].length);
		    zenderNaam = zenderRegel[0];
		    zenderAdres = zenderRegel[1];
            document.getElementById('selector').options[document.getElementById('selector').options.length]=new Option(zenderNaam, zenderAdres,false,false);
	      }
        }
        eigenZendersNaarSystem();
		document.getElementById('selector').selectedIndex = huidigeIndex;
		
		
		//Bovenstaande regel vervangen op 24-08-08 door onderstaande 2 omdat na sorteren zenders de naam op een verkeerde naam springt omdat de index veranderd is.
		// OOK NIET GOED!  Bovenste nog beter
		//document.getElementById('selector').options[document.getElementById('selector').selectedIndex].text = huidigeZendernaam;
		//document.getElementById('selector').options[document.getElementById('selector').selectedIndex].value = huidigeUrl;
		
	  }
	  
	  

      function startSpelen(){
	    document.getElementById('mediaPlayer').controls.play();
	  }
	  
	  function stopSpelen(){
	    document.getElementById('mediaPlayer').controls.stop();
	  }
	  
	  function inlezenExtraInst(){		
	    var startNa = System.Gadget.Settings.read("startNa");
	    if (startNa !=''){
		  if (startNa =='0'){return false;}
		  System.Gadget.Settings.write("startNa","0");
		  var t = setTimeout("startSpelen()",startNa);
		}
		var stopNa = System.Gadget.Settings.read("stopNa");
	    if (stopNa !=''){
		  if (stopNa =='0'){return false;}
		  System.Gadget.Settings.write("stopNa","0");
		  var t2 = setTimeout("stopSpelen()",stopNa);
		}
		
	    var extraInstellingenAan = System.Gadget.Settings.read("extraInstellingenAan");
		if (extraInstellingenAan==''){return false;}
		var akzendernamen = System.Gadget.Settings.read("akZendernamen");
		  document.getElementById('selector').style.backgroundColor = akzendernamen;
		var vkzendernamen = System.Gadget.Settings.read("vkZendernamen");
		  document.getElementById('selector').style.color = vkzendernamen;
		var ltzendernamen = System.Gadget.Settings.read("ltZendernamen");
		  document.getElementById('selector').style.fontFamily = ltzendernamen;
		var lgzendernamen = System.Gadget.Settings.read("lgZendernamen");
		  document.getElementById('selector').style.fontSize = lgzendernamen
		var vetteLetters = System.Gadget.Settings.read("vetteLetters");
		  if (vetteLetters == true){document.getElementById('selector').style.fontWeight = 'bold';}else{document.getElementById('selector').style.fontWeight = 'normal';}
		var omhulsel = System.Gadget.Settings.read("omhulsel");
		  if (omhulsel == false){document.getElementById('smooth').style.visibility = 'hidden';} else {document.getElementById('smooth').style.visibility = 'visible';}
		var autoplay = System.Gadget.Settings.read("autoplay");
    	  if (autoplay==true){
		    if (mediaPlayer.playState==10 &&  inladenGadget=='0'){ //player ready
		      bepaalZender(); // Dit werkt goed, alleen elke keer nadat het config. scherm gesloten wordt, hapert de boel omdat de huidige zender opnieuw ingelezen wordt.
			  afspelen();
			  inladenGadget = '1';
			}
		  }
	  }

	  
	  
	  
function eigenZendersNaarSystem(){
	  	var aanvullendeZendersBestand = mapNaam + '\\ZenderData.txt';
        var fso, f, r;
        var ForReading = 1, ForWriting = 2;
        fso = new ActiveXObject("Scripting.FileSystemObject");
		var alleEigenZenders = '';
		if (fso.FileExists(aanvullendeZendersBestand)){
          f = fso.OpenTextFile(aanvullendeZendersBestand, ForReading);
          r =  f.ReadLine();
	      alleEigenZenders = r;
		  f.Close();
		} else return;
		if (alleEigenZenders != ""){
          var zenderLijst = alleEigenZenders.split("#", alleEigenZenders.length);
	      for (var i=1 ; i < zenderLijst.length; i++){   //???
		    zenderRegel = zenderLijst[i].split("*", zenderLijst[i].length);
		    zenderNaam = zenderRegel[0];
		    zenderAdres = zenderRegel[1];
            document.getElementById('selector').options[document.getElementById('selector').options.length]=new Option(zenderNaam, zenderAdres,false,false);
	      }
		  
        }
		sorteerZendersEnExterneZenders();
	  }


function checkTitle(){
  if (rps==0){
    clearInterval(rpint);
    return;
  }
  var rpint=setInterval("showTitle()",5000);
}


function showTitle(){
  tempRPtitel = document.getElementById('RealPlayer').getEntryAuthor(0);
  if (huidigeRPtitel != tempRPtitel){
	huidigeRPtitel = tempRPtitel;
	toonPopup(huidigeRPtitel);
  }
}




function opnieuwRPtitel(){
  if (rps==0){return;}
  huidigeRPtitel = '';
  checkTitle();
}



function bijwerken(){
  runnedOnce = System.Gadget.Settings.read("runnedOnce");
  if (runnedOnce==''){runnedOnce = '0';}
  if (runnedOnce == '0'){
	  wisTxts();
  }
}



function sorteerZendersEnExterneZenders(){
  sorteerEx = System.Gadget.Settings.read("sorteer");
  if (sorteerEx==''){sorteerEx = '0';}
  if (sorteerEx == '0'){
    return false;
  }
  var sorteerArray = new Array();
  var huidigeSorteerRegel;
  for (var i=0;i<document.getElementById('selector').options.length;i++){
	 sorteerArray[i] = document.getElementById('selector').options[i].text + '***' + document.getElementById('selector').options[i].value;
  }
  sorteerArray = sorteerArray.sort();
  document.getElementById('selector').options.length = 0;
  for (var j=0;j<sorteerArray.length;j++){
    huidigeSorteerRegel = sorteerArray[j].split("***", sorteerArray[j].length);
	document.getElementById('selector').options[j] = new Option(huidigeSorteerRegel[0],false,false);
	document.getElementById('selector').options[j].value = huidigeSorteerRegel[1];
  }
}



function wisTxts(){
  var fso,fso2;
  fso = new ActiveXObject("Scripting.FileSystemObject");
  fso2 = new ActiveXObject("Scripting.FileSystemObject");
  if (fso.FileExists(mapNaam + "\\ZenderData1.txt" )){fso.DeleteFile( mapNaam + "\\ZenderData1.txt");}
  if (fso2.FileExists(mapNaam + "\\ZenderData2.txt" )){fso.DeleteFile( mapNaam + "\\ZenderData2.txt");}
  System.Gadget.Settings.write("runnedOnce","1");
  runnedOnce = '1';
}



function unDocked(){
  document.body.style.width = '260px';
  document.body.style.height = '100px';
  document.body.style.zoom = '200%';
}


function docked(){
  document.body.style.width = '130px';
  document.body.style.height = '50px';
  document.body.style.zoom = '100%';
}


function toonPopup(boodschap){
  var oPopBody = oPopup.document.body;
  oPopBody.style.backgroundColor = "black";
  oPopBody.style.color = "lightyellow";
  oPopBody.style.padding= '2px';
  oPopBody.style.fontFamily = 'Tahoma';
  oPopBody.style.fontSize = '12px';
  oPopBody.style.fontWeight = 'bold';
  oPopBody.innerHTML = '<span style="text-align:center">' + boodschap + '</span><script type="javascript">setTimeout("document.body.style.top=-100px",3000);</script>';
  oPopup.show(0, 0, 350, 18);
}


function stopRadio(){
  if (rps==1){document.getElementById('RealPlayer').DoStop();}
}



function willekeurigStation(){
  var rand_no = Math.ceil((document.getElementById('selector').length-1)*Math.random());
  document.getElementById('selector').selectedIndex = rand_no;
  bepaalZender();
  afspelen();
}
	  
	
