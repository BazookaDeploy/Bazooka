import reqwest from "reqwest";
import Net from "../../../Shared/Net";

module.exports = {
    getAgents: function (id) {
        return Net.get("/api/Agents/AgentsByEnviroment/" + id);
    },

    getTemplatedTask: function (id) {
        return Net.get("/api/TemplatedTasks/" + id);
    },

    createTempaltedTask: function () {
        return Net.post("/api/TemplatedTasks/CreateTemplatedTask", {
        });
    },

    updateTemplatedTasks: function () {
        return Net.post("/api/TemplatedTasks/ModifyTemplatedTask", {
        });
    }
}