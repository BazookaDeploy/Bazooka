import reqwest from "reqwest";

module.exports = {
	updateAllEnviroments: function() {
		return reqwest({
			url: "/api/enviroments/",
			type: 'json',
			contentType: 'application/json',
			method: "get"
		})
	}
};
