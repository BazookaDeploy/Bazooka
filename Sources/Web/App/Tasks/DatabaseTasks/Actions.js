import reqwest from "reqwest";

module.exports = {
  getAgents:function(id){
    return reqwest({
      url: "/api/Agents/AgentsByEnviroment/"+id,
      type: 'json',
      contentType: 'application/json',
      method: "get",
    });
  },

  getDatabaseTask:function(id){
    return reqwest({
      url: "/api/DatabaseTasks/"+id,
      type: 'json',
      contentType: 'application/json',
      method: "get",
    });
  },




  createDatabaseTask: function(name, connectionString, pack, databaseName, enviromentId,repository,agentId, applicationId){
    return  reqwest({
      url: "/api/DatabaseTasks/CreateDatabaseTask",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        EnviromentId:enviromentId,
        Name:name,
        ConnectionString:connectionString,
        Package:pack,
        DatabaseName:databaseName,
        Repository:repository,
        AgentId:agentId,
        ApplicationId:applicationId
      })
    });
  },

  updateDatabaseTask: function(id, name, connectionString, pack, databaseName, enviromentId,repository, agentId,applicationId){
    return  reqwest({
      url: "/api/DatabaseTasks/ModifyDatabaseTask",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        DatabaseTaskId:id,
        EnviromentId:enviromentId,
        Name:name,
        ConnectionString:connectionString,
        Package:pack,
        DatabaseName:databaseName,
        Repository:repository,
        AgentId: agentId,
        ApplicationId:applicationId
      })
    });
  },
}
