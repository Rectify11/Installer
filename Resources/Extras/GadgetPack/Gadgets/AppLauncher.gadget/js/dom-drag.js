/**************************************************
 * dom-drag.js
 * 09.25.2001
 * www.youngpup.net
 **************************************************
 * 10.28.2001 - fixed minor bug where events
 * sometimes fired off the handle, not the root.
 **************************************************/
 
/**************************************************
* modified extensively by Dean
* I removed all x axis and unneeded code
***************************************************/

var Drag = {

	obj : null,

	init : function(obj){
		obj.onmousedown	= Drag.start;
		obj.style.pixelTop = 0;
		obj.onDragStart = new Function();
		obj.onDragEnd = new Function();
		obj.onDrag = new Function();
	},

	start : function(e){
		Drag.obj = this;
		e = Drag.fixE(e);
		Drag.obj.lastMouseY = e.clientY;
		document.onmousemove = Drag.drag;
		document.onmouseup = Drag.end;
		return false;
	},

	drag : function(e){
		e = Drag.fixE(e);
		var ey	= e.clientY;
		var y = parseInt(Drag.obj.style.pixelTop);
		var ny = parseInt(y + ey - Drag.obj.lastMouseY);
		Drag.obj.style.pixelTop = ny;
		Drag.obj.style.pixelLeft = 15;
		Drag.obj.lastMouseY	= ey;
		Drag.obj.onDrag(ny, Drag.obj);
		return false;
	},

	end : function(){
		document.onmousemove = null;
		document.onmouseup = null;
		Drag.obj.onDragEnd(Drag.obj);
		Drag.obj = null;
	},

	fixE : function(e){
		if (typeof e == 'undefined') e = window.event;
		return e;
	}
};

var offsets = new Array();

function createDrag(){
try{
  drag.style.display = "";
  dragContainer.innerHTML = "";

  oUl = document.createElement("ul");
  oUl.id = "dragBox";
  for (var i=0; i < linksList.length; i++){
    getFilePaths(i);
    oLi = document.createElement("li");
    oLi.id = "item"+i;
    oLi.className = "draggable";
    oLi.innerText = label;
    oLi.style.backgroundImage = "url(\""+img+"\")";
    oUl.appendChild(oLi);
  }
  $("dragContainer").appendChild(oUl);
  
  var dragBox = $("dragBox");
  var elems = dragBox.getElementsByTagName("li");

  for (var i = 0; i < elems.length; i++){
    Drag.init(elems[i]);
    elems[i].onDrag = function(y,myElem){
      y = myElem.offsetTop;
      calcOffsets();
      myElem.style.zIndex = 99;
      var pos = whereAmI(myElem);
      var elems = dragBox.getElementsByTagName("li");
      if (pos != elems.length-1 && y > offsets[pos+1]){ 
        dragBox.removeChild(myElem);
        if (elems[pos+1]) dragBox.insertBefore(myElem, elems[pos+1]);
        else dragBox.appendChild(myElem);
        myElem.style.pixelTop = 0;                
      }
      if (pos != 0 && y < offsets[pos-1]){ 
        dragBox.removeChild(myElem);
        dragBox.insertBefore(myElem, elems[pos-1]);
        myElem.style.pixelTop = 0;
      }
    };
    elems[i].onDragEnd = function(myElem){
      myElem.style.pixelTop = 0;
      myElem.style.pixelLeft = 0;                
      myElem.style.zIndex = 1;
    }
  }
  calcOffsets();
} catch(err) {debugLog("createDrag: "+err.name+" - "+err.message)}
}

function calcOffsets(){
  var elems = dragBox.getElementsByTagName("li");
  for (var i = 0; i < elems.length; i++){
    offsets[i] = elems[i].offsetTop;
  }
}

function whereAmI(elem){ 
  var elems = dragBox.getElementsByTagName("li");
  for (var i = 0; i < elems.length; i++){
    if (elems[i] == elem) { return i }
  }
}

function saveDragged(){
try{
  tmpLinks = new Array(); tmpIcons = new Array(); tmpSwitch = new Array();
  var elems = dragBox.getElementsByTagName("li");
  for (var i = 0; i < elems.length; i++){
    var currItem = parseInt(elems[i].id.substr(4));
    tmpLinks[i] = linksList[currItem];
    tmpIcons[i] = iconList[currItem];
    tmpSwitch[i] = switchList[currItem];
  }
  linksList = tmpLinks;
  iconList = tmpIcons;
  switchList = tmpSwitch;
  bldShortCuts();
  drag.style.display = "none";
} catch(err) {debugLog("saveDragged: "+err.name+" - "+err.message)}
}

