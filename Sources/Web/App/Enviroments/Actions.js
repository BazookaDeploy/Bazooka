import reqwest from "reqwest";
import Net from "../Shared/Net";

module.exports = {
	updateAllEnviroments: function () {
		return Net.get("/api/enviroments/")
	},

	createEnviroment: function (name) {
		return Net.post("/api/enviroments/create", {
			Name: name
		});
	},

	createAgent: function (enviromentId, name, address) {
		return Net.post("/api/enviroments/addAgent", {
			EnviromentId: enviromentId,
			Address: address,
			Name: name
		});
	},

	getAgent: function (id) {
		return Net.get("/api/agents/" + id);
	},

	rename: function (id, enviromentId, name) {
		return Net.post("/api/agents/Rename", {
			EnviromentId: enviromentId,
			AgentId: id,
			Name: name
		});
	},

	changeAddress: function (id, enviromentId, address) {
		return Net.post("/api/agents/ChangeAddress", {
			EnviromentId: enviromentId,
			AgentId: id,
			Address: address
		});
	},

	testAgent: function (url) {
		return Net.get("/api/agents/test?url=" + url)
	},

	uploadFiles: function (files, agent) {
		var data = new FormData();
		data.append(0, files[0]);

		return reqwest({
			processData: false,
			url: "/api/agents/update?agent=" + agent,
			method: "post",
			data: data
		})
	}
};