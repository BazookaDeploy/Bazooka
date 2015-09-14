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

	createEnviroment: function(name) {
		var promise = reqwest({
			url: "/api/enviroments/createEnviroment",
			type: 'json',
			contentType: 'application/json',
			method: "post",
			data: JSON.stringify({
				Name: name
			})
		});

		return promise;
	}
};
