$('body').on('click', '#btn-next', function (e) {
	console.log("hello")
	$("form").each(function () {
		var $this = $(this);
		console.log($this);
	})
})