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

	getApplicationInfo: function(id){
		return reqwest({
			url: "/api/applications/" + id,
			method: "get",
			type: 'json'
		})
	},

	getApplicationGroups: function(){
		return reqwest({
			url: "/api/applications/applicationGroups/",
			method: "get",
			type: 'json'
		})
	},

	createApplicationGroup: function(name){
		return reqwest({
			url:"/api/applications/CreateApplicationGroup",
      type:'json',
      contentType: 'application/json',
      method:"post",
      data:JSON.stringify({
        Name:name
      })
		})
	},

	setApplicationGroup: function(applicationId,groupId){
		return reqwest({
			url:"/api/applications/SetApplicationGroup",
			type:'json',
			contentType: 'application/json',
			method:"post",
			data:JSON.stringify({
				ApplicationId:applicationId,
				ApplicationGroupId:groupId
			})
		})
	},
};
