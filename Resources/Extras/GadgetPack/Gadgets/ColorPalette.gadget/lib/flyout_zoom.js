
function flyout_load() {
	var html = '';
	for (var j = 0; j < 6; ++j) {
		for (var i = 0; i < 6; ++i) {
			html += '<a name=\'clrn\'>&nbsp;</a>';
		}
		html += '<br />';
	}

	var con = document.getElementById('con');
	con.innerHTML = html;
}
