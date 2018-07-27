$('body').on('click', '#btn-next', function (e) {
	$("form").each(function () {
		console.log($(this))
		$(this).submit();
	})
	//$("form").each(function () {
	//	var $this = $(this)
	//	console.log($this);
	//	$.ajax({
	//		url: Path_SubmitIssue,
	//		data: $this,
	//		cache: false,
	//		type: 'POST',
	//		contentType: 'multipart/form-data',
	//		success: function (data) {
	//			console.log(data);
	//		}
	//	})
	//})
})