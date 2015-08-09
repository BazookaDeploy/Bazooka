import reqwest from "reqwest";

module.exports = {

	getVersions: function(enviromentId) {
		var promise = reqwest({
			url: "/api/deploy/search?enviromentId=" + enviromentId,
			type: 'json',
			contentType: 'application/json',
			method: "get"
		});
		return promise;
	},

	startDeploy: function(enviromentId, version) {
		var promise = reqwest({
			url: "/api/deploy/deploy?enviromentId=" + enviromentId + "&version=" +
				version,
			type: 'json',
			contentType: 'application/json',
			method: "get"
		});
		
		return promise;
	},

	scheduleDeploy: function(enviromentId, version, date) {
		var promise = reqwest({
			url: "/api/deploy/deploy?enviromentId=" + enviromentId + "&version=" +
				version + "&start=" + date.toISOString(),
			type: 'json',
			contentType: 'application/json',
			method: "get"
		});
		return promise;
	},

  updateEnviroments : function(){
    return reqwest({
      url:"/api/status/",
      type:'json',
      contentType: 'application/json',
      method:"get"
    })
  }
};
