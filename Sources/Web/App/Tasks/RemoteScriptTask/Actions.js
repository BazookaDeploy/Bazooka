import reqwest from "reqwest";

module.exports = {
  getRemoteScriptTask:function(id){
    return reqwest({
      url: "/api/RemoteScriptTasks/"+id,
      type: 'json',
      contentType: 'application/json',
      method: "get",
    });
  },

  getAgents:function(id){
    return reqwest({
      url: "/api/Agents/AgentsByEnviroment/"+id,
      type: 'json',
      contentType: 'application/json',
      method: "get",
    });
  },


  createRemoteScriptTask: function(name, script, machine, folder, enviromentId,applicationId){
    return  reqwest({
      url: "/api/RemoteScriptTasks/CreateRemoteScriptTask",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        EnviromentId:enviromentId,
        Name:name,
        Script:script,
        AgentId:machine,
        Folder:folder,
        ApplicationId: applicationId
      })
    });
  },

  updateRemoteScriptTask: function(id, name, script,machine,folder, enviromentId,applicationId){
    return  reqwest({
      url: "/api/RemoteScriptTasks/ModifyRemoteScriptTask",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        RemoteScriptTaskId:id,
        EnviromentId:enviromentId,
        Name:name,
        Script:script,
        AgentId:machine,
        Folder:folder,
        ApplicationId: applicationId
      })
    });
  },
}
