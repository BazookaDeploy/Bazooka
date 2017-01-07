import reqwest from "reqwest";

module.exports = {
  getApplications: function () {
    return reqwest({
      url: "/api/Applications/",
      type: 'json',
      contentType: 'application/json',
      method: "get"
    })
  },

  getAllApplications: function () {
    return reqwest({
      url: "/api/Applications/All",
      type: 'json',
      contentType: 'application/json',
      method: "get"
    })
  },

  createApplication: function (name) {
    var promise = reqwest({
      url: "/api/applications/Create",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        Name: name
      })
    });

    return promise;
  },

  cloneApplication: function (name, app) {
    var promise = reqwest({
      url: "/api/applications/CreateApplicationFromExisting",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        Name: name,
        OriginalApplicationId: app
      })
    });

    return promise;
  },

  getDeployUrl: function (id) {
    return reqwest({
      url: "/api/deployTasks/deployUrl?id=" + id,
      type: 'json',
      contentType: 'application/json',
      method: "get"
    })
  },

  getUsers: function (enviromentId, applicationId) {
    return reqwest({
      url: "/api/applications/AllowedUsers?enviromentId=" + enviromentId + "&applicationId=" + applicationId,
      type: 'json',
      contentType: 'application/json',
      method: "get"
    })
  },

  addUser: function (enviromentId, applicationid, userId) {
    return reqwest({
      url: "/api/applications/AddAllowedUser",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        ApplicationId: applicationid,
        EnviromentId: enviromentId,
        UserId: userId
      })
    })
  },

  removeUser: function (enviromentId, applicationid, userId) {
    return reqwest({
      url: "/api/applications/RemoveAllowedUser",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        ApplicationId: applicationid,
        EnviromentId: enviromentId,
        UserId: userId
      })
    })
  },

  getGroups: function (enviromentId, applicationId) {
    return reqwest({
      url: "api/applications/AllowedGroups?enviromentId=" + enviromentId + "&applicationId=" + applicationId,
      type: 'json',
      contentType: 'application/json',
      method: "get"
    })
  },

  addGroup: function (enviromentId, applicationid, groupId) {
    return reqwest({
      url: "/api/applications/AddAllowedGroup",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        ApplicationId: applicationid,
        EnviromentId: enviromentId,
        GroupId: groupId
      })
    })
  },

  removeGroups: function (enviromentId, applicationid, groupId) {
    return reqwest({
      url: "/api/applications/RemoveAllowedGroup",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        ApplicationId: applicationid,
        EnviromentId: enviromentId,
        GroupId: groupId
      })
    })
  },

  getTasks: function (enviromentId, applicationId) {
    return reqwest({
      url: "/api/Tasks/?enviromentId=" + enviromentId + "&applicationId=" + applicationId,
      type: 'json',
      contentType: 'application/json',
      method: "get"
    })
  },

  deleteTask: function (applicationId, id, type) {
    return reqwest({
      url: "/api/tasks/deleteTask",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        ApplicationId: applicationId,
        TaskId: id,
        Type: type
      })
    })
  },

  cloneEnviroment: function (enviromentIdToClone, agentId, enviromentId, applicationId) {
    return reqwest({
      url: "/api/applications/CopyConfigurationFromEnviroment",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        ApplicationId: applicationId,
        MachineId: agentId,
        EnviromentId: enviromentId,
        OriginalEnviromentId: enviromentIdToClone
      })
    });
  },


  getAgents: function (id) {
    return reqwest({
      url: "/api/Agents/AgentsByEnviroment/" + id,
      type: 'json',
      contentType: 'application/json',
      method: "get",
    });
  },

  	getAdmins: function(applicationId) {
		return reqwest({
			url: "api/Applications/Administrators?applicationId="+applicationId,
			type: 'json',
			contentType: 'application/json',
			method: "get"
		});
	},

	addAdmin: function(userId, applicationId){
		return reqwest({
			url:"/api/applications/AddApplicationAdministrator",
			type:'json',
			contentType: 'application/json',
			method:"post",
			data:JSON.stringify({
				ApplicationId:applicationId,
				UserId:userId
			})
		})
	},

	removeAdmin: function(userId, applicationId){
		return reqwest({
			url:"/api/applications/RemoveApplicationAdministrator",
			type:'json',
			contentType: 'application/json',
			method:"post",
			data:JSON.stringify({
				ApplicationId:applicationId,
				UserId:userId
			})
		})
	}
};