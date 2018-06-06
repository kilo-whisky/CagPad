$('#frm-check').on('submit', function(e){
	e.preventDefault();
	console.log($(this).serializeArray())
})

$('.toggle').bootstrapToggle({
	on: "Yes",
	off: "No",
	onstyle: "success",
	offstyle: "danger",
	size: "small"
});

$('.toggle').on('change', function (e) {
	var thing = this.id.slice(4)
	if ($(this).prop('checked') === true) {
		$('#btn-' + thing).addClass('disabled').prop("disabled", true);
	}
	else {
		$('#btn-' + thing).removeClass('disabled').prop("disabled", false);
	}
});

$('.btn-note').on("click", function () {

	$('#modal').modal('show');
});

$()