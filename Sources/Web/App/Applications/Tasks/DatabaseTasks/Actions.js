import reqwest from "reqwest";
import Net from "../../../Shared/Net";

module.exports = {
  getAgents: function (id) {
    return Net.get("/api/Agents/AgentsByEnviroment/" + id);
  },

  getDatabaseTask: function (id) {
    return Net.get("/api/DatabaseTasks/" + id);
  },

  createDatabaseTask: function (name, connectionString, pack, databaseName, enviromentId, repository, agentId, applicationId) {
    return Net.post("/api/DatabaseTasks/CreateDatabaseTask", {
      EnviromentId: enviromentId,
      Name: name,
      ConnectionString: connectionString,
      Package: pack,
      DatabaseName: databaseName,
      Repository: repository,
      AgentId: agentId,
      ApplicationId: applicationId
    });
  },

  updateDatabaseTask: function (id, name, connectionString, pack, databaseName, enviromentId, repository, agentId, applicationId, partial) {
    return Net.post("/api/DatabaseTasks/ModifyDatabaseTask", {
      DatabaseTaskId: id,
      EnviromentId: enviromentId,
      Name: name,
      ConnectionString: connectionString,
      Package: pack,
      DatabaseName: databaseName,
      Repository: repository,
      AgentId: agentId,
      ApplicationId: applicationId,
      Partial: partial
    });
  }
}