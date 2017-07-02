import reqwest from "reqwest";
import Net from "../../../Shared/Net";

module.exports = {
    getAgents: function (id) {
        return Net.get("/api/Agents/AgentsByEnviroment/" + id);
    },

    getTemplatedTask: function (id) {
        return Net.get("/api/TemplatedTask/" + id);
    },


    loadTemplatedTasks: function () {
        return Net.get("/api/TaskTemplate/");
    },

    lastVersion: function (id) {
        return Net.get("/api/TaskTemplate/LastVersion/" + id);
    },

    createTemplatedTask: function (name, enviromentId, applicationId, agentId, taskId, taskVersionId, parameters) {
        return Net.post("/api/TemplatedTask/CreateTemplatedTask", {
            Name: name,
            EnviromentId: enviromentId,
            ApplicationId: applicationId,
            AgentId: agentId,
            TaskId: taskId,
            TaskVersionId: taskVersionId,
            Parameters: parameters
        });
    },

    modifyTemplatedTask: function (id, enviromentId, applicationId, agentId, parameters) {
        return Net.post("/api/TemplatedTask/Modify", {
            Id:id,
            EnviromentId: enviromentId,
            ApplicationId: applicationId,
            AgentId: agentId,
            Parameters: parameters
        });
    },

    renameTemplatedTask: function (id, enviromentId, applicationId, name) {
        return Net.post("/api/TemplatedTask/RenameTemplatedTask", {
            Name: name,
            Id:id,
            EnviromentId: enviromentId,
            ApplicationId: applicationId
        });
    },

    updateTemplatedTasks: function (id, applicationId, version) {
        return Net.post("/api/TemplatedTask/Update", {
            Id: id,
            ApplicationId: applicationId,
            Version:version
        });
    }
}