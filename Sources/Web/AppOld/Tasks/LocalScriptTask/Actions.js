import reqwest from "reqwest";

module.exports = {
  getLocalScriptTask:function(id){
    return reqwest({
      url: "/api/LocalScriptTasks/"+id,
      type: 'json',
      contentType: 'application/json',
      method: "get",
    });
  },

  createLocalScriptTask: function(name, script, enviromentId, applicationId){
    return  reqwest({
      url: "/api/LocalScriptTasks/CreateLocalScriptTask",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        EnviromentId:enviromentId,
        Name:name,
        Script:script,
        ApplicationId:applicationId
      })
    });
  },

  updateLocalScriptTask: function(id, name, script, enviromentId, applicationId){
    return  reqwest({
      url: "/api/LocalScriptTasks/ModifyLocalScriptTask",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        LocalScriptTaskId:id,
        EnviromentId:enviromentId,
        Name:name,
        Script:script,
        ApplicationId:applicationId
      })
    });
  },
}
