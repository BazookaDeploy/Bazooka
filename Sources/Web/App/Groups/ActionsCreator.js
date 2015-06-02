var ApplicationDispatcher = require('../Base/Dispatcher');
var ActionTypes = require('./Constants').ActionTypes;
var reqwest = require("reqwest");

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
