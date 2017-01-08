import reqwest from "reqwest";
import Net from "../Shared/Net";

module.exports = {
    getStatistics: function (filter) {
        var date = new Date();
        date.setHours(0, 0, 0, 0);

        if (filter === "Yesterday") {
            date.setDate(date.getDate() - 1);
        }

        if (filter === "Last week") {
            date.setDate(date.getDate() - 7);
        }

        if (filter === "Last month") {
            date.setDate(date.getDate() - 30);
        }

        if (filter === "Ever") {
            date.setDate(date.getDate() - 30000);
        }

        return Net.get("/api/stats/statistics?startDate=" + date.toISOString());
    }
};