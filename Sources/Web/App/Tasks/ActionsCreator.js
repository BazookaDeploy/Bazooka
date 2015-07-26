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

  getUsers: function (id) {
      return reqwest({
        url: "/users/allowed?id=" + id,
        type: 'json',
        contentType: 'application/json',
        method: "get"
      })
  },

  addUser: function (id, userId) {
      return reqwest({
        url: "/users/allowed/add?enviromentId=" + id + "&userId=" + userId,
        type: 'json',
        contentType: 'application/json',
        method: "post"
      })
  },

  removeUser: function (id) {
      return reqwest({
        url: "/users/allowed/remove?id=" + id,
        type: 'json',
        contentType: 'application/json',
        method: "post"
      })
  },

  getGroups: function (id) {
      return reqwest({
        url: "/groups/allowed?id=" + id,
        type: 'json',
        contentType: 'application/json',
        method: "get"
      })
  },

  addGroup: function (id, groupId) {
      return reqwest({
        url: "/group/allowed/add?enviromentId=" + id + "&groupId=" + groupId,
        type: 'json',
        contentType: 'application/json',
        method: "post"
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
      url: "/users/all",
      type: 'json',
      contentType: 'application/json',
      method: "get"
    });
  },

  getAllGroups: function () {
    return reqwest({
      url: "/users/groups/",
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
