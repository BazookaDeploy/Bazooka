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

  createRemoteScriptTask: function(name, script, machine, folder, enviromentId){
    return  reqwest({
      url: "/api/RemoteScriptTasks",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        EnviromentId:enviromentId,
        Name:name,
        Script:script,
        Machine:machine,
        Folder:folder
      })
    });
  },

  updateRemoteScriptTask: function(id, name, script,machine,folder, enviromentId){
    return  reqwest({
      url: "/api/RemoteScriptTasks",
      type: 'json',
      contentType: 'application/json',
      method: "put",
      data: JSON.stringify({
        Id:id,
        EnviromentId:enviromentId,
        Name:name,
        Script:script,
        Machine:machine,
        Folder:folder
      })
    });
  },
}
