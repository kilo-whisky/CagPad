$('#input-guardians').selectize({
	framework: 'bootstrap',
	delimiter: ',',
	persist: false,
	selectOnTab: true,
	plugins: ['remove_button'],
	onItemAdd: function (value) { AddRemoveRole(value, "a"); },
	onItemRemove: function (value) { AddRemoveRole(value, "r"); }
});

function AddRemoveRole(value, addremove) {
	$.ajax({
		url: Path_GuardianUpsert,
		data: {
			PadId: Data_PadId,
			AddRemove: addremove,
			UserId: value
		},
		success: function (data) {
			console.log(data);
		},
		error: function (error) {
			console.log(error);
		}
	})
}