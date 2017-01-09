import reqwest from "reqwest";
import Net from "../../../Shared/Net";

module.exports = {
  testAgent: function (url) {
    return Net.get("/api/deployTasks/test?url=" + url);
  },

  getAgents: function (id) {
    return Net.get("/api/Agents/AgentsByEnviroment/" + id);
  },

  updateDeployUnits: function (enviromentId) {
    return Net.get("/api/DeployTasks/?id=" + enviromentId);
  },

  updateDeployUnit: function (deployUnitId) {
    return Net.get("/api/DeployTasks/?id=" + deployUnitId);
  },

  modifyDeployUnit: function (deployUnitId, enviromentId, name, machine,
    packageName, directory, repository, params, uninstallationScript, installationScript, configFile, configTransform, configuration, applicationId) {
    return Net.post("/api/deployTasks/ModifyDeployTask", {
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
    });
  },

  createDeployUnit: function (enviromentId, name, machine, packageName,
    directory, repository, params, applicationId) {
    return Net.post("/api/deployTasks/CreateDeployTask", {
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
    });
  }
};