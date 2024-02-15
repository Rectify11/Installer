
function flyout_load() {
	var html = '<table>';
	html += '<tr>';
	for (var i = 0; i < 8; ++i) {
		html += '<td><a name=\'clrn\'></a></td>';
	}
	html += '</tr>';
//	html += '<tr><td id=\'clrval\' colspan=\'8\'>123</td></tr>';
	html += '<tr>';
	for (var i = 0; i < 8; ++i) {
		html += '<td><a name=\'clrn\'></a></td>';
	}
	html += '</tr>';
	html += '</tr></table>';

	var con = document.getElementById('con');
	con.innerHTML = html;
}
