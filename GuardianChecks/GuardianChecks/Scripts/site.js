//UK date format datatables sorting
jQuery.extend(jQuery.fn.dataTableExt.oSort, {
	"date-uk-pre": function (a) {
		if (a === null || a === "") {
			return 0;
		}
		var ukDatea = a.substring(0, 10).split('/');
		return (ukDatea[2] + ukDatea[1] + ukDatea[0]) * 1;
	},

	"date-uk-asc": function (a, b) {
		return a < b ? -1 : a > b ? 1 : 0;
	},

	"date-uk-desc": function (a, b) {
		return a < b ? 1 : a > b ? -1 : 0;
	}
});

//datepicker defaults - UK format
$.datepicker.setDefaults({
	dateFormat: 'dd/mm/yy',
	duration: 0,
	changeMonth: true,
	changeYear: true,
	showOtherMonths: true,
	selectOtherMonths: true
});

$.validator.methods.date = function (value, element) {
	return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
}