$('#tbl-expiry > tbody  > tr').each(function () {
	var expiry = $(this).data('expiry');
	if (parseInt(expiry) <= 0) {
		$(this).addClass("danger");
	}
	if (parseInt(expiry) > 0 && parseInt(expiry) < 90) {
		$(this).addClass("warning");
	}
});

