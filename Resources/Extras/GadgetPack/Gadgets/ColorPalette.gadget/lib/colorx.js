
var X_PATH32 = '.\\lib\\ColorX32.dll';
var X_PATH64 = '.\\lib\\ColorX64.dll';

var g_picker = null;

function x_register() {
	if (_GADGET) {
		System.Shell.execute('regsvr32.exe', '/s /n /i:user ' + X_PATH32, System.Gadget.path);
		System.Shell.execute('regsvr32.exe', '/s /n /i:user ' + X_PATH64, System.Gadget.path);
	}
}

function x_unregister() {
	if (_GADGET) {
		System.Shell.execute('regsvr32.exe', '/u /s /n /i:user ' + X_PATH32, System.Gadget.path);
		System.Shell.execute('regsvr32.exe', '/u /s /n /i:user ' + X_PATH64, System.Gadget.path);
	}
}

function x_ensure() {
	if (g_picker == null) {
		try {
			g_picker = new ActiveXObject('ColorX.ColorPicker');
		} catch(xc) {
			//_excep(xc);
		}
	}
	if (g_picker == null) {
		window.setTimeout('x_ensure()', 200);
	}
}

function x_count() {
	if (g_picker) {
		return g_picker.Instances;
	}
	return 0;
}

function x_set_cursor(path) {
	if (g_picker) {
		g_picker.SetCursor(path);
	}
}

function x_unset_cursor() {
	if (g_picker) {
		g_picker.SetCursor('');
	}
}

function x_pick() {
	if (g_picker) {
		g_picker.AtCursor();
	}
}

function x_get_color() {
	if (g_picker) {
		return new Array(g_picker.Red, g_picker.Green, g_picker.Blue);
	}
	return null;
}
