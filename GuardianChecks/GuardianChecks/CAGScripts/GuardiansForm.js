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
	var params = JSON.stringify({
		"PadId": Data_PadId,
		"Questions": questions
	})
	$.ajax({
		url: Path_Stage1,
		data: questions,
		success: function (data) {
			window.location.href = data
		}
	});
})