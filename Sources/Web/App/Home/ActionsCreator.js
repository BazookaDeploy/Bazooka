import reqwest from "reqwest";

module.exports = {

	getVersions: function(enviromentId, applicationId) {
		var promise = reqwest({
			url: "/api/deploy/search?enviromentId=" + enviromentId + "&applicationId="+applicationId,
			type: 'json',
			contentType: 'application/json',
			method: "get"
		});
		return promise;
	},

	startDeploy: function(enviromentId,  applicationId, version) {
		var promise = reqwest({
		    url: "/api/deploy/deploy?enviromentId=" + enviromentId + "&applicationId=" +	applicationId + "&version=" +	version,
			type: 'json',
			contentType: 'application/json',
			method: "get"
		});
		
		return promise;
	},

	scheduleDeploy: function(enviromentId, applicationId, version, date) {
		var promise = reqwest({
		    url: "/api/deploy/schedule?enviromentId=" + enviromentId + "&applicationId=" +	applicationId  + "&version=" +
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
