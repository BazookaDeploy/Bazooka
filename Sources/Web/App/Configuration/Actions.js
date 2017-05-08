import reqwest from "reqwest";
import Net from "../Shared/Net";

module.exports = {
	updateUsers: function () {
		return Net.get("/api/users/all");
	},

	updateGroups: function () {
		return Net.get("/api/users/groups");
	},

	getUsersInGroup: function (id) {
		return Net.get("api/users/group?groupName=" + id);
	},


	createGroup: function (name) {
		return Net.post("api/users/CreateGroup", {
			Name: name
		});
	},

	addUser: function (group, userId) {
		return Net.post("api/users/addUserToGroup", {
			Group: group,
			UserId: userId
		});
	},

	removeUser: function (group, userId) {
		return Net.post("api/users/removeUserFromGroup", {
			Group: group,
			UserId: userId
		});
	},

	getApplicationGroups: function () {
		return Net.get("/api/applications/applicationGroups/");
	},

	createApplicationGroup: function (name) {
		return Net.post("/api/applications/CreateApplicationGroup", {
			Name: name
		});
    },

    createTemplatedtask: function (name, description) {
        return Net.post("/api/Templatedtask/CreateTemplatedTask", {
            Name: name,
            Description: description
        });
    }, 

    loadTemplatedTasks: function () {
        return Net.get("/api/Templatedtask/");
    }
};