var Issues = [];
var issueid;
var fileinput;
var file;
var issue;

$('body').on('click', '#btn-next', function (e) {

	$('.IssueForm').each(function () {

		issueid = $(this).attr('id');
		fileinput = $(this).find('.image').prop('files');
		file;
		if (fileinput.length > 0) {
			file = fileinput[0]
		}
		issue = {
			"IssueId": issueid,
			"Description": $(this).find('.description').val(),
			"Severity": $(this).find('input[type="radio"]:checked').val()
		}
		upsert(file, issue)

	});

	window.location.href = '/Guardians/Confirmation?CheckId=' + Data_CheckId
});

function upsert(file, issue) {
	console.log(file)
	console.log(issue)
	var formData = new FormData();
	formData.append("image", file);
	formData.append("issue", JSON.stringify(issue));
	console.log(formData)
	$.ajax({
		type: 'POST',
		url: Path_UpsertIssue,
		data: formData,
		cache: false,
		contentType: false,
		processData: false,
		success: function (data) {
			console.log(data);
		}
	})
}