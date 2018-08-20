//$('#frm-check').on('submit', function(e){
//	e.preventDefault();
//	console.log($(this).serializeArray())
//})

var questions = []

$('.toggle').bootstrapToggle({
	on: "Yes",
	off: "No",
	onstyle: "success",
	offstyle: "danger",
	size: "small"
});

$('#btn-next').on('click', function () {
	$('input').each(function () {
		var q = {
			QuestionId: $(this).data('question'),
			Answer: $(this).prop('checked')
		};
		questions.push(q)
	});
	console.log(Data_PadId);
	console.log(questions);
	var params = JSON.stringify({
		"PadId": Data_PadId,
		"Questions": questions
	})
	console.log(params);
	$.ajax({
		type: 'POST',
		contentType: 'application/json; charset=utf-8',
		url: Path_Stage1,
		data: params,
		success: function (data) {
			console.log(data);
			window.location.href = data
		}
	});
})