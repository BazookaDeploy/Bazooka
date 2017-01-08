import reqwest from "reqwest";
import Net from "../Shared/Net";

module.exports = {
    updateDeployments: function (filter) {
        var query = "";
        var date = new Date();
        date.setHours(0, 0, 0, 0);

        if (filter === "Today") {
            query = "?$filter=(StartDate gt DateTime'" + date.toISOString() + "' or StartDate eq null) ";
        }

        if (filter === "Yesterday") {
            date.setDate(date.getDate() - 1);
            query = "?$filter=(StartDate gt DateTime'" + date.toISOString() + "' or StartDate eq null)  ";
        }

        if (filter === "Last week") {
            date.setDate(date.getDate() - 7);
            query = "?$filter=(StartDate gt DateTime'" + date.toISOString() + "' or StartDate eq null)  ";
        }

        if (filter === "Last month") {
            date.setDate(date.getDate() - 30);
            query = "?$filter=(StartDate gt DateTime'" + date.toISOString() + "' or StartDate eq null)  ";
        }

        return Net.get("/api/deployment/" + query);
    },

    cancelDeployment: function (id) {
        return Net.get("/api/deploy/cancel?deploymentId=" + id);
    },

    updateDeployment: function (id) {
        return Net.get("/api/deployment/" + id);
    }
};