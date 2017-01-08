import reqwest from "reqwest";
import Net from "../../../Shared/Net";

module.exports = {
  getLocalScriptTask: function (id) {
    return Net.get("/api/LocalScriptTasks/" + id);
  },

  createLocalScriptTask: function (name, script, enviromentId, applicationId) {
    return Net.post("/api/LocalScriptTasks/CreateLocalScriptTask", {
      EnviromentId: enviromentId,
      Name: name,
      Script: script,
      ApplicationId: applicationId
    });
  },

  updateLocalScriptTask: function (id, name, script, enviromentId, applicationId) {
    return Net.post("/api/LocalScriptTasks/ModifyLocalScriptTask", {
      LocalScriptTaskId: id,
      EnviromentId: enviromentId,
      Name: name,
      Script: script,
      ApplicationId: applicationId
    });
  }
}