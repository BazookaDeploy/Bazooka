import reqwest from "reqwest";

module.exports = {
  testAgent: function (url) {
    return reqwest({
      url: "/api/deployTasks/test?url=" + url,
      type: 'json',
      contentType: 'application/json',
      method: "get"
    })
  },

  getAgents:function(id){
    return reqwest({
      url: "/api/Agents/AgentsByEnviroment/"+id,
      type: 'json',
      contentType: 'application/json',
      method: "get",
    });
  },


  updateDeployUnits: function (enviromentId) {
    return reqwest({
      url: "/api/DeployTasks/?id=" + enviromentId,
      type: 'json',
      contentType: 'application/json',
      method: "get"
    })
  },

  updateDeployUnit: function (deployUnitId) {
    return reqwest({
      url: "/api/DeployTasks/?id=" + deployUnitId,
      type: 'json',
      contentType: 'application/json',
      method: "get"
    })
  },

  modifyDeployUnit: function (deployUnitId, enviromentId, name, machine,
    packageName, directory, repository, params, uninstallationScript, installationScript, configFile, configTransform, configuration,applicationId) {
    var promise = reqwest({
      url: "/api/deployTasks/ModifyDeployTask",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        DeployTaskId: deployUnitId,
        EnviromentId: enviromentId,
        Name: name,
        AgentId: machine,
        PackageName: packageName,
        Directory: directory,
        Repository: repository,
        UninstallScript: uninstallationScript,
        InstallScript: installationScript,
        ConfigurationFile: configFile,
        ConfigurationTransform: configTransform,
        Configuration: configuration,
        ApplicationId: applicationId,
        AdditionalParameters: params.map(x => {
          x.Key = x.Name;
          return x;
        })
      })
    });

    return promise;
  },

  createDeployUnit: function (enviromentId, name, machine, packageName,
    directory, repository, params, applicationId) {
    var promise = reqwest({
      url: "/api/deployTasks/CreateDeployTask",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        EnviromentId: enviromentId,
        Name: name,
        AgentId: machine,
        PackageName: packageName,
        Directory: directory,
        Repository: repository,
        ApplicationId: applicationId,
        AdditionalParameters: params.map(x => {
          x.Key = x.Name;
          return x;
        })
      })
    });

    return promise;
  }
};
