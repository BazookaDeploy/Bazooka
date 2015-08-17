import reqwest from "reqwest";

module.exports = {
	updateAllEnviroments: function() {
		return reqwest({
			url: "/api/enviroments/",
			type: 'json',
			contentType: 'application/json',
			method: "get"
		})
	},

	updateEnviroments: function(applicationId) {
	    return reqwest({
			url: "/api/enviroments/" + applicationId,
			type: 'json',
			contentType: 'application/json',
			method: "get"
		})
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

		return promise;
	}
};
