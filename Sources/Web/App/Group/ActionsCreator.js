import ApplicationDispatcher from '../Base/Dispatcher';
import { ActionTypes } from './Constants';
import reqwest from "reqwest";

module.exports = {
	updateUsers: function(id) {
		reqwest({
			url: "/users/group?groupName="+id,
			type: 'json',
			contentType: 'application/json',
			method: "get"
		}).then((x => {
			ApplicationDispatcher.handleServerAction({
				type: ActionTypes.UPDATE_USERS,
				apps: x
			});
		}))
	},

	getUsers: function() {
		return reqwest({
			url: "/users/all",
			type: 'json',
			contentType: 'application/json',
			method: "get"
		});
	},

	addUser: function(group, userId) {
		var promise = reqwest({
			url: "/users/add?group="+group+"&userId="+userId,
			type: 'json',
			contentType: 'application/json',
			method: "post"
		});


		return promise;
	},

	removeUser: function(group, userId) {
		var promise = reqwest({
			url: "/users/remove?group="+group+"&userId="+userId,
			type: 'json',
			contentType: 'application/json',
			method: "post"
		});

		return promise;
	},
};
