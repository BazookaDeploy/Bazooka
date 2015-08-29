import reqwest from "reqwest";

module.exports = {
  getDatabaseTask:function(id){
    return reqwest({
      url: "/api/DatabaseTasks/"+id,
      type: 'json',
      contentType: 'application/json',
      method: "get",
    });
  },

  createDatabaseTask: function(name, connectionString, pack, databaseName, enviromentId,repository){
    return  reqwest({
      url: "/api/DatabaseTasks",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        EnviromentId:enviromentId,
        Name:name,
        ConnectionString:connectionString,
        Package:pack,
        DatabaseName:databaseName,
        Repository:repository
      })
    });
  },

  updateDatabaseTask: function(id, name, connectionString, pack, databaseName, enviromentId,repository){
    return  reqwest({
      url: "/api/DatabaseTasks",
      type: 'json',
      contentType: 'application/json',
      method: "put",
      data: JSON.stringify({
        Id:id,
        EnviromentId:enviromentId,
        Name:name,
        ConnectionString:connectionString,
        Package:pack,
        DatabaseName:databaseName,
        Repository:repository
      })
    });
  },
}
