
var SET_FMT_HEX = 'Hex';
var SET_FMT_RGB = 'RGB';

function settings_load() {
	var id;
	var val = System.Gadget.Settings.readString('ColorX_Format');
	if (val == SET_FMT_HEX) {
		id = SET_FMT_HEX;
	} else if (val == SET_FMT_RGB) {
		id = SET_FMT_RGB;
	} else {
		id = SET_FMT_HEX;
	}
	var radio = document.getElementById('radio' + id);
	if (radio) {
		radio.checked = true;
	}

	System.Gadget.onSettingsClosing = function(event) {
		if (event.closeAction == event.Action.commit) {
			settings_commit();
		}
	};
}

function settings_commit() {
	var clrfmt = null;
	var radios = document.getElementsByName('radioFmt');
	if (radios && radios.length >= 2) {
		if (radios[0].checked) {
			clrfmt = SET_FMT_HEX;
		} else if (radios[1].checked) {
			clrfmt = SET_FMT_RGB;
		}
		System.Gadget.Settings.writeString('ColorX_Format', clrfmt);
	}
}

function settings_write(key, value) {
	System.Gadget.Settings.writeString(key, value);
}
