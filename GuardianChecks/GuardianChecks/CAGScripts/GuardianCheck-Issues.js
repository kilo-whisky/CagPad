var Issues = [];

$('body').on('click', '#btn-next', function (e) {

	$('.issue').each(function () {
		var issueid = $(this).attr('id');
		var fileinput = $(this).find('.image').prop('files');
		var file;
		if (fileinput.length > 0) {
			file = fileinput[0]
		}
		var issue = {
			IssueId: issueid,
			Description: ,
			
		};
		var formData = new FormData();
		var params = JSON.Stringify({
			"IssueId": issueid,
			"Description": $(this).find('.description').val(),
			"Severity": $(this).find('input[type="radio"]:checked').val()
		})
		formData.append()
	})

})