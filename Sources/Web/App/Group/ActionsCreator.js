import reqwest from "reqwest";

module.exports = {
	updateUsers: function(id) {
		return reqwest({
			url: "api/users/group?groupName="+id,
			type: 'json',
			contentType: 'application/json',
			method: "get"
		})
	},

	getUsers: function() {
		return reqwest({
			url: "api/users/all",
			type: 'json',
			contentType: 'application/json',
			method: "get"
		});
	},

	addUser: function(group, userId) {
		var promise = reqwest({
			url: "api/users/addUserToGroup",
			type: 'json',
			contentType: 'application/json',
			method: "post",
			data:JSON.stringify({
				Group:group,
				UserId:userId
			})
		});


		return promise;
	},

	removeUser: function(group, userId) {
		var promise = reqwest({
			url: "api/users/removeUserFromGroup",
			type: 'json',
			contentType: 'application/json',
			method: "post",
			data:JSON.stringify({
				Group:group,
				UserId:userId
			})
		});

		return promise;
	},
};
