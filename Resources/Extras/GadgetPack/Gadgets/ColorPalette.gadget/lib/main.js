
var _GADGET = true;

var FMT_HEX = 'Hex';
var FMT_RGB = 'RGB';
var DEF_COLOR = '#ffffff';
var MAX_TIMER = 10;
var PICK_INTERVAL = 100;

var gtext;
var g_format = FMT_HEX;
var g_last_color = null;
var g_time_count = 0;
var g_picking = false;

function _debug(msg) {
	var elt = document.getElementById('bg');
	if (elt) document.body.removeChild(bg);
	elt = document.getElementById('fg');
	if (elt) document.body.removeChild(elt);
	var elt = document.getElementsByName('clrn');
	for (var i = 0; i < elt.length; ++i) {
		document.body.removeChild(elt[i]);
	}
	document.body.style.backgroundColor = '#ffffff';
	document.body.innerHTML = msg;
}

function _excep(xc) {
	_debug(xc.name + ' ' + xc.number + ', ' + xc.description + '; ' + xc.message);
}

function palette_load() {
	try {
		System.Gadget;
		_GADGET = true;
	} catch(xc) {
		_GADGET = false;
	}

	x_register();
	x_ensure();

	if (_GADGET) {
		//gadget_setup_flyout();
		gadget_settings();
		gadget_visual();
	}
}

function palette_unload() {
	g_time_count = 0;
	x_unset_cursor();
	if (x_count() <= 1) {
		x_unregister();
	}
}

function gadget_visual() {
	if (g_last_color == null || g_last_color.length == 0)
		g_last_color = DEF_COLOR;
	gtext = bg.addTextObject(g_last_color, 'Segoe UI', 10, 'Color(255, 255, 255, 255)', 66, 24);
	gtext.rotation = -15;
	//gtext.addGlow('Color(255, 127, 127, 127)', 1, 50);
	gtext.addShadow('Color(255, 48, 48, 48)', 2, 100, 1, 1);
	gadget_settings_refresh()
}

function gadget_visual_set(array) {
	if (g_format == FMT_HEX) {
		gtext.value = hex_from_array(array);
		gtext.left = 80;
		gtext.top = 110;
		gtext.width = 48;
		gtext.height = 15;
	} else if (g_format == FMT_RGB) {
		gtext.value = rgb2_from_array(array);
		gtext.left = 80;
		gtext.top = 110;
		gtext.width = 29 + (array[2] + '').length;
		gtext.height = 30;
	}
}

function palette_over(event) {
	var target = event.srcElement;
	if (g_picking == false) {
		var x = event.clientX, y = event.clientY;
		var clrns = document.getElementsByName('clrn');
		for (var i = 0; i < clrns.length; ++i) {
			if (palette_within(x, y, clrns[i])) {
				target.style.cursor = 'pointer';
				return;
			}
		}
	}
	if (target.style.cursor == 'pointer') {
		target.style.cursor = 'default';
	}
}

function palette_within(x, y, elt) {
	var MARGIN = 1;
	if (elt.offsetLeft + MARGIN < x && x + MARGIN < elt.offsetLeft + elt.offsetWidth) {
		if (elt.offsetTop + MARGIN < y && y + MARGIN < elt.offsetTop + elt.offsetHeight) {
			return true;
		}
	}
	return false;
}

function palette_click(event) {
	var x = event.clientX, y = event.clientY;
	var clrns = document.getElementsByName('clrn');
	for (var i = 0; i < clrns.length; ++i) {
		if (palette_within(x, y, clrns[i])) {
			if (event.button == 1) {
				color_to_pick(i);
			} else if (event.button == 2) {
				color_to_pick(i);
			} else if (event.button == 4) {
				color_copy(i);
			}
			return;
		}
	}
	if (_GADGET) { gadget_flyout(); }
}

function color_to_pick(index) {
	if (g_picking == false) {
		g_picking = true;
		g_time_count = MAX_TIMER;
		window.setTimeout('color_pick_timed(\'' + index + '\')', PICK_INTERVAL);
		document.getElementById('fg').style.cursor = 'default';
		if (_GADGET) { x_set_cursor(gadget_get_path('\\res\\dropper.cur')); }
	}
}

function color_pick_timed(index) {
	var clr = document.getElementById('clr' + index);
	if (g_time_count > 0.1) {
		g_time_count -= 0.1;
		clr.innerHTML = Math.floor(g_time_count);
		color_get(clr);
		window.setTimeout('color_pick_timed(\'' + index + '\')', PICK_INTERVAL);
	} else {
		g_time_count = 0;
		clr.innerHTML = '';
		x_unset_cursor();
		g_picking = false;
		if (_GADGET) { settings_write('ColorX_Color' + index, g_last_color); }
	}
}

function color_get(elt) {
	x_pick();
	var a = x_get_color();
	if (a) {
		g_last_color = hex_from_array(a);
		elt.style.backgroundColor = g_last_color;
		elt.style.color = contrast_hex_from_array(a);
		if (_GADGET) { gadget_visual_set(a); }
	}
}

function color_copy(index) {
	var clr = document.getElementById('clr' + index);
	g_last_color = clr.style.backgroundColor;
	if (g_last_color == null || g_last_color.length == 0) {
		g_last_color = DEF_COLOR;
	}
	if (_GADGET) { gadget_visual_set(array_from_hex(g_last_color)); }
	var field = document.getElementById('copy');
	field.value = string_from_hex(g_last_color);
	if (field.value.length > 0) {
		var tr = field.createTextRange();
		tr.execCommand('Copy');
	}
}

function string_from_hex(hex) {
	if (g_format == FMT_HEX) {
		return hex;
	} else if (g_format == FMT_RGB) {
		return rgb_from_array(array_from_hex(hex));
	}
}

function gadget_get_path(path) {
	return System.Gadget.path + path;
}

function gadget_setup_flyout() {
	System.Gadget.Flyout.file = 'flyout_zoom.html';
	System.Gadget.Flyout.onShow = function() {
		var doc = System.Gadget.Flyout.document;
		var html = '';
		for (var i = 0; i < 10; ++i) {
			html += '<td><a name=\'clrn\'></a></td>';
		}
//		doc.getElementById('upper').innerHTML = html;
//		doc.getElementById('lower').innerHTML = html;
	}
}

function gadget_flyout() {
	//System.Gadget.Flyout.show = true;
}

function gadget_settings() {
	System.Gadget.settingsUI = 'settings.html';
	System.Gadget.onSettingsClosed = function(event) {
		if (event.closeAction == event.Action.commit) {
			gadget_settings_refresh();
		}
	};
	gadget_settings_read();
}

function gadget_settings_refresh() {
	var val = System.Gadget.Settings.readString('ColorX_Format');
	if (val) g_format = val;
	gadget_visual_set(array_from_hex(g_last_color));
}

function gadget_settings_read() {
	var clrns = document.getElementsByName('clrn');
	for (var i = 0; i < clrns.length; ++i) {
		var val = System.Gadget.Settings.readString('ColorX_Color' + i);
		if (val && val.length > 0) {
			clrns[i].style.backgroundColor = val;
			if (i == 0) {
				g_last_color = val;
			}
		}
	}
}

function hex_from_array(array) {
	return '#' + h2(array[0]) + h2(array[1]) + h2(array[2]);
}

function contrast_hex_from_array(array) {
	return '#' + h2(contrast(array[0])) + h2(contrast(array[1])) + h2(contrast(array[2]));
}

function contrast(c) {
	var DELTA = 0xf;
	if ((0x7f - DELTA) < c && c < 0x7f) {
		return 0xff;
	} else if (0x7f < c && c < (0x7f + DELTA)) {
		return 0;
	} else {
		return 0xff - c;
	}
}

function h2(s) {
	s = s.toString(16);
	if (s.length == 1)
		return '0' + s;
	return s;
}

function array_from_hex(hex) {
	return new Array(parseInt(hex.substr(1, 2), 16), parseInt(hex.substr(3, 2), 16), parseInt(hex.substr(5, 2), 16));
}

function rgb_from_array(array) {
	return '(' + array[0] + ', ' + array[1] + ', ' + array[2] + ')';
}

function rgb2_from_array(array) {
	return array[0] + '\n ' + array[1] + '\n  ' + array[2];
}
