import ApplicationDispatcher from  '../Base/Dispatcher';
import { ActionTypes } from './Constants';
import reqwest from "reqwest";

module.exports = {
	updateAllEnviroments: function() {
		reqwest({
			url: "/api/enviroments/",
			type: 'json',
			contentType: 'application/json',
			method: "get"
		}).then((x => {
			ApplicationDispatcher.handleServerAction({
				type: ActionTypes.UPDATE_ENVIROMENTS,
				apps: x
			});
		}))
	},

	updateGroupedEnviroments: function() {
		reqwest({
			url: "/api/enviroments/grouped/",
			type: 'json',
			contentType: 'application/json',
			method: "get"
		}).then((x => {
			ApplicationDispatcher.handleServerAction({
				type: ActionTypes.UPDATE_GROUPENVIROMENTS,
				apps: x
			});
		}))
	},


	updateEnviroments: function(applicationId) {
		reqwest({
			url: "/api/enviroments/" + applicationId,
			type: 'json',
			contentType: 'application/json',
			method: "get"
		}).then((x => {
			ApplicationDispatcher.handleServerAction({
				type: ActionTypes.UPDATE_ENVIROMENTS,
				apps: x
			});
		}))
	},

	createEnviroment: function(applicationId, name, description) {
		var promise = reqwest({
			url: "/api/enviroments",
			type: 'json',
			contentType: 'application/json',
			method: "post",
			data: JSON.stringify({
				ApplicationId: applicationId,
				Configuration: name,
				Description: description
			})
		});

		promise.then(x => {
			module.exports.updateEnviroments(applicationId);
		});

		return promise;
	}
};
