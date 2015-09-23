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

  removeUser: function (enviromentId,applicationId) {
      return reqwest({
        url: "/users/allowed/remove?enviromentId=" + enviromentId + "&applicationId="+applicationId,
        type: 'json',
        contentType: 'application/json',
        method: "post"
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

  removeGroups: function (id) {
      return reqwest({
        url: "/group/allowed/remove?id=" + id,
        type: 'json',
        contentType: 'application/json',
        method: "post"
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

  getTasks: function (enviromentId) {
    return reqwest({
      url: "/api/Tasks/?enviromentId=" + enviromentId,
      type: 'json',
      contentType: 'application/json',
      method: "get"
    })
  },

}
