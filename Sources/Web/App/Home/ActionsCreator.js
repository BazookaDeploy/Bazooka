import Net from "../Base/Net";

module.exports = {
	getVersions: function (enviromentId, applicationId) {
		return Net.get("/api/deploy/search?enviromentId=" + enviromentId + "&applicationId=" + applicationId);
	},

	getTasks: function (enviromentId, applicationId) {
		return Net.get("/api/deploy/tasks?enviromentId=" + enviromentId + "&applicationId=" + applicationId);
	},

	startDeploy: function (enviromentId, applicationId, version, tasks) {
		return Net.post("/api/deploy/deploy?enviromentId=" + enviromentId + "&applicationId=" + applicationId + "&version=" + version, { tasks: tasks });
	},

	scheduleDeploy: function (enviromentId, applicationId, version, date, tasks) {
		return Net.post("/api/deploy/schedule?enviromentId=" + enviromentId + "&applicationId=" + applicationId + "&version=" +	version + "&start=" + date.toISOString(), { tasks: tasks });
	},

  updateEnviroments: function () {
    return Net.get("/api/status/");
  }
};
