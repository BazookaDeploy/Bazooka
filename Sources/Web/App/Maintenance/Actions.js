import reqwest from "reqwest";
import Net from "../Shared/Net";

module.exports = {
    loadTemplatedTasks: function () {
        return Net.get("/api/TaskTemplate/");
    },

    loadTemplatedTask: function (id) {
        return Net.get("/api/Tasktemplate/LastVersion/" + id);
    },

    getTask: function (id) {
        return Net.get("/api/MaintenanceTask?id=" + id);
    },

    getTaskList: function (skip,take) {
        return Net.post("/api/MaintenanceTask/Lista?skip=" + skip + "&take="+take);
    },

    run: function (agentId, taskId, params) {
        return Net.post("/api/MaintenanceTask/Run?agentId=" + agentId + "&taskId=" + taskId, params );
    },
};