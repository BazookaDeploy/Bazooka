import reqwest from "reqwest";
import Net from "../Shared/Net";

module.exports = {
  getApplications: function () {
    return Net.get("/api/Applications/");
  },

  getAllApplications: function () {
    return Net.get("/api/Applications/All");
  },

  createApplication: function (name) {
    return Net.post("/api/applications/Create", {
      Name: name
    });
  },

  renmeApplication: function (id, name) {
    return Net.post("/api/applications/Rename", {
      Name: name,
      ApplicationId: id
    });
  },

  deleteApplication: function (id) {
    return Net.post("/api/applications/delete", {
      ApplicationId: id
    });
  },

  cloneApplication: function (name, app) {
    return Net.post("/api/applications/CreateApplicationFromExisting", {
      Name: name,
      OriginalApplicationId: app
    });
  },

  getDeployUrl: function (id) {
    return Net.get("/api/deployTasks/deployUrl?id=" + id);
  },

  getUsers: function (enviromentId, applicationId) {
    return Net.get("/api/applications/AllowedUsers?enviromentId=" + enviromentId + "&applicationId=" + applicationId);
  },

  addUser: function (enviromentId, applicationid, userId) {
    return Net.post("/api/applications/AddAllowedUser", {
      ApplicationId: applicationid,
      EnviromentId: enviromentId,
      UserId: userId
    });
  },

  removeUser: function (enviromentId, applicationid, userId) {
    return Net.post("/api/applications/RemoveAllowedUser", {
      ApplicationId: applicationid,
      EnviromentId: enviromentId,
      UserId: userId
    });
  },

  getGroups: function (enviromentId, applicationId) {
    return Net.get("api/applications/AllowedGroups?enviromentId=" + enviromentId + "&applicationId=" + applicationId);
  },

  addGroup: function (enviromentId, applicationid, groupId) {
    return Net.post("/api/applications/AddAllowedGroup", {
      ApplicationId: applicationid,
      EnviromentId: enviromentId,
      GroupId: groupId
    });
  },

  removeGroups: function (enviromentId, applicationid, groupId) {
    return Net.post("/api/applications/RemoveAllowedGroup", {
      ApplicationId: applicationid,
      EnviromentId: enviromentId,
      GroupId: groupId
    });
  },

  getTasks: function (enviromentId, applicationId) {
    return Net.get("/api/Tasks/?enviromentId=" + enviromentId + "&applicationId=" + applicationId);
  },

  deleteTask: function (applicationId, id, type) {
    return Net.post("/api/tasks/deleteTask", {
      ApplicationId: applicationId,
      TaskId: id,
      Type: type
    });
  },

  cloneEnviroment: function (enviromentIdToClone, agentId, enviromentId, applicationId) {
    return Net.post("/api/applications/CopyConfigurationFromEnviroment", {
      ApplicationId: applicationId,
      MachineId: agentId,
      EnviromentId: enviromentId,
      OriginalEnviromentId: enviromentIdToClone
    });
  },


  getAgents: function (id) {
    return Net.get("/api/Agents/AgentsByEnviroment/" + id);
  },

  getAdmins: function (applicationId) {
    return Net.get("api/Applications/Administrators?applicationId=" + applicationId);
  },

  addAdmin: function (userId, applicationId) {
    return Net.post("/api/applications/AddApplicationAdministrator", {
      ApplicationId: applicationId,
      UserId: userId
    });
  },

  removeAdmin: function (userId, applicationId) {
    return Net.post("/api/applications/RemoveApplicationAdministrator", {
      ApplicationId: applicationId,
      UserId: userId
    });
  },


  getApplicationInfo: function (id) {
    return Net.get("/api/applications/" + id);
  },

  getApplicationGroups: function () {
    return Net.get("/api/applications/applicationGroups/");
  },

  createApplicationGroup: function (name) {
    return Net.post("/api/applications/CreateApplicationGroup", {
      Name: name
    });
  },

  setApplicationGroup: function (applicationId, groupId) {
    return Net.post("/api/applications/SetApplicationGroup", {
      ApplicationId: applicationId,
      ApplicationGroupId: groupId
    });
  },

  moveTaskDown: function (applicationId, enviromentId, position) {
      return Net.post("/api/applications/MoveTaskDown", {
          ApplicationId: applicationId,
          EnviromentId: enviromentId,
          Position:position
      });
  },

  moveTaskUp: function (applicationId, enviromentId, position) {
      return Net.post("/api/applications/MoveTaskUp", {
          ApplicationId: applicationId,
          EnviromentId: enviromentId,
          Position: position
      });
  },
};