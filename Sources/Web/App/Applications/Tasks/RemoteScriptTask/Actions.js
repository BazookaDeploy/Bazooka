import reqwest from "reqwest";
import Net from "../../../Shared/Net";

module.exports = {
  getRemoteScriptTask: function (id) {
    return Net.get("/api/RemoteScriptTasks/" + id);
  },

  getAgents: function (id) {
    return Net.get("/api/Agents/AgentsByEnviroment/" + id);
  },

  createRemoteScriptTask: function (name, script, machine, folder, enviromentId, applicationId) {
    return Net.post("/api/RemoteScriptTasks/CreateRemoteScriptTask", {
      EnviromentId: enviromentId,
      Name: name,
      Script: script,
      AgentId: machine,
      Folder: folder,
      ApplicationId: applicationId
    });
  },

  updateRemoteScriptTask: function (id, name, script, machine, folder, enviromentId, applicationId) {
    return Net.post("/api/RemoteScriptTasks/ModifyRemoteScriptTask", {
      RemoteScriptTaskId: id,
      EnviromentId: enviromentId,
      Name: name,
      Script: script,
      AgentId: machine,
      Folder: folder,
      ApplicationId: applicationId
    });
  }
};