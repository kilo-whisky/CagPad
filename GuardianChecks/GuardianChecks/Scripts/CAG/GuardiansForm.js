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