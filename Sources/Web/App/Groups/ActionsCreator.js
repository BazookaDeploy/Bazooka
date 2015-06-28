import ApplicationDispatcher from '../Base/Dispatcher';
import { ActionTypes } from './Constants';
import reqwest from "reqwest";

module.exports = {
	updateGroups: function() {
		reqwest({
			url: "/users/groups/",
			type: 'json',
			contentType: 'application/json',
			method: "get"
		}).then((x => {
			ApplicationDispatcher.handleServerAction({
				type: ActionTypes.UPDATE_GROUPS,
				apps: x
			});
		}))
	},

	createGroup: function( name) {
		var promise = reqwest({
			url: "/users/group?groupName="+name,
			type: 'json',
			contentType: 'application/json',
			method: "post"
		});

		promise.then(x => {
			module.exports.updateGroups();
		});

		return promise;
	}
};
