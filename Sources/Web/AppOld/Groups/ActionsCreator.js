import reqwest from "reqwest";

module.exports = {
	updateGroups: function() {
	return	reqwest({
			url: "api/users/groups/",
			type: 'json',
			contentType: 'application/json',
			method: "get"
		})
	},

	createGroup: function(name) {
		var promise = reqwest({
			url: "api/users/CreateGroup",
			type: 'json',
			contentType: 'application/json',
			method: "post",
			data:JSON.stringify({
				Name:name
			})
		});

		return promise;
	}
};
