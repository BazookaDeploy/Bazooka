import reqwest from "reqwest";

module.exports = {
  getDeployUrl:function (id) {
    return reqwest({
      url: "/api/deployTasks/deployUrl?id=" + id,
      type: 'json',
      contentType: 'application/json',
      method: "get"
    })
  },

  getUsers: function (enviromentId,applicationId) {
      return reqwest({
        url: "/api/applications/AllowedUsers?enviromentId=" + enviromentId+ "&applicationId="+applicationId,
        type: 'json',
        contentType: 'application/json',
        method: "get"
      })
  },

  addUser: function (enviromentId,applicationid, userId) {
      return reqwest({
        url: "/api/applications/AddAllowedUser",
        type: 'json',
        contentType: 'application/json',
        method: "post",
        data:JSON.stringify({
          ApplicationId : applicationid,
          EnviromentId:enviromentId,
          UserId:userId
        })
      })
  },

  removeUser: function (enviromentId,applicationid, userId) {
    return reqwest({
      url: "/api/applications/RemoveAllowedUser",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data:JSON.stringify({
        ApplicationId : applicationid,
        EnviromentId:enviromentId,
        UserId:userId
      })
    })
  },

  getGroups: function (enviromentId,applicationId) {
      return reqwest({
        url: "api/applications/AllowedGroups?enviromentId=" + enviromentId+ "&applicationId="+applicationId,
        type: 'json',
        contentType: 'application/json',
        method: "get"
      })
  },

  addGroup: function (enviromentId,applicationid, groupId) {
      return reqwest({
        url: "/api/applications/AddAllowedGroup",
        type: 'json',
        contentType: 'application/json',
        method: "post",
        data:JSON.stringify({
          ApplicationId : applicationid,
          EnviromentId:enviromentId,
          GroupId:groupId
        })
      })
  },

  removeGroups: function (enviromentId,applicationid, groupId) {
    return reqwest({
      url: "/api/applications/RemoveAllowedGroup",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data:JSON.stringify({
        ApplicationId : applicationid,
        EnviromentId:enviromentId,
        GroupId:groupId
      })
    })
  },

  getAllUsers: function () {
    return reqwest({
      url: "api/users/all",
      type: 'json',
      contentType: 'application/json',
      method: "get"
    });
  },

  getAllGroups: function () {
    return reqwest({
      url: "api/users/groups/",
      type: 'json',
      contentType: 'application/json',
      method: "get"
    })
  },

  getTasks: function (enviromentId,applicationId) {
    return reqwest({
      url: "/api/Tasks/?enviromentId=" + enviromentId+ "&applicationId="+applicationId,
      type: 'json',
      contentType: 'application/json',
      method: "get"
    })
  },

  deleteTask : function(applicationId, id, type){
      return reqwest({
          url: "/api/tasks/deleteTask",
          type: 'json',
          contentType: 'application/json',
          method: "post",
          data:JSON.stringify({
              ApplicationId : applicationId,
              TaskId:id,
              Type:type
          })
      })
  }

}
