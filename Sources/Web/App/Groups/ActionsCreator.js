import reqwest from "reqwest";

module.exports = {
	updateGroups: function() {
	return	reqwest({
			url: "/users/groups/",
			type: 'json',
			contentType: 'application/json',
			method: "get"
		})
	},

	createGroup: function( name) {
		var promise = reqwest({
			url: "/users/group?groupName="+name,
			type: 'json',
			contentType: 'application/json',
			method: "post"
		});

		return promise;
	}
};
