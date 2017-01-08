import reqwest from "reqwest";

module.exports = {
	updateUsers: function () {
		return reqwest({
			url: "/api/users/all",
			type: 'json',
			contentType: 'application/json',
			method: "get"
		})
	},

	updateGroups: function () {
		return reqwest({
			url: "/api/users/groups",
			type: 'json',
			contentType: 'application/json',
			method: "get"
		})
	},

	getUsersInGroup: function (id) {
		return reqwest({
			url: "api/users/group?groupName=" + id,
			type: 'json',
			contentType: 'application/json',
			method: "get"
		})
	},


	createGroup: function (name) {
		var promise = reqwest({
			url: "api/users/CreateGroup",
			type: 'json',
			contentType: 'application/json',
			method: "post",
			data: JSON.stringify({
				Name: name
			})
		});

		return promise;
	},

	addUser: function (group, userId) {
		var promise = reqwest({
			url: "api/users/addUserToGroup",
			type: 'json',
			contentType: 'application/json',
			method: "post",
			data: JSON.stringify({
				Group: group,
				UserId: userId
			})
		});


		return promise;
	},

	removeUser: function (group, userId) {
		var promise = reqwest({
			url: "api/users/removeUserFromGroup",
			type: 'json',
			contentType: 'application/json',
			method: "post",
			data: JSON.stringify({
				Group: group,
				UserId: userId
			})
		});

		return promise;
	},

	getApplicationGroups: function () {
		return reqwest({
			url: "/api/applications/applicationGroups/",
			method: "get",
			type: 'json'
		})
	},

	createApplicationGroup: function (name) {
		return reqwest({
			url: "/api/applications/CreateApplicationGroup",
			type: 'json',
			contentType: 'application/json',
			method: "post",
			data: JSON.stringify({
				Name: name
			})
		})
	},
};