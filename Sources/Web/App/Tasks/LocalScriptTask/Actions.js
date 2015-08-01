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

  createLocalScriptTask: function(name, script, enviromentId){
    return  reqwest({
      url: "/api/LocalScriptTasks",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        EnviromentId:enviromentId,
        Name:name,
        Script:script
      })
    });
  },

  updateLocalScriptTask: function(id, name, script, enviromentId){
    return  reqwest({
      url: "/api/LocalScriptTasks",
      type: 'json',
      contentType: 'application/json',
      method: "put",
      data: JSON.stringify({
        Id:id,
        EnviromentId:enviromentId,
        Name:name,
        Script:script
      })
    });
  },
}
