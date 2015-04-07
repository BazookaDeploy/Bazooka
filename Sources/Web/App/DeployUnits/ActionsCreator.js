var Dispatcher = require('../Base/Dispatcher');
var ActionTypes = require('./Constants').ActionTypes;
var reqwest = require("reqwest");

module.exports = {
	updateDeployUnits: function(enviromentId) {
		reqwest({
			url: "/api/deployUnits/" + enviromentId,
			type: 'json',
			contentType: 'application/json',
			method: "get"
		}).then((x => {
			Dispatcher.handleServerAction({
				type: ActionTypes.UPDATE_DEPLOYUNITS,
				apps: x
			});
		}))
	},

	modifyDeployUnit: function(deployUnitId, enviromentId, name, machine,
		packageName, directory, repository, params) {
		var promise = reqwest({
			url: "/api/deployUnits",
			type: 'json',
			contentType: 'application/json',
			method: "put",
			data: JSON.stringify({
				Id: deployUnitId,
				EnviromentId: enviromentId,
				Name: name,
				Machine: machine,
				PackageName: packageName,
				Directory: directory,
				Repository: repository,
				AdditionalParameters: params.map(x => {
					x.Key = x.Name;
					return x;
				})
			})
		});

		promise.then(x => {
			module.exports.updateDeployUnits(enviromentId);
		});

		return promise;
	},

	createDeployUnit: function(enviromentId, name, machine, packageName,
		directory, repository, params) {
		var promise = reqwest({
			url: "/api/deployUnits",
			type: 'json',
			contentType: 'application/json',
			method: "post",
			data: JSON.stringify({
				EnviromentId: enviromentId,
				Name: name,
				Machine: machine,
				PackageName: packageName,
				Directory: directory,
				Repository: repository,
				AdditionalParameters: params.map(x => {
					x.Key = x.Name;
					return x;
				})
			})
		});

		promise.then(x => {
			module.exports.updateDeployUnits(enviromentId);
		});

		return promise;
	}
};
