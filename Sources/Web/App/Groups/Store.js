var Dispatcher = require("../Base/Dispatcher");
var Constants = require("./Constants");
var EventEmitter = require("events").EventEmitter;
var assign = require("object-assign");

var ActionTypes = Constants.ActionTypes;
var CHANGE_EVENT = 'change';

var _groups = [];

function _addGroups(raw) {
	_groups = raw;
}


var GroupsStore = assign({}, EventEmitter.prototype, {
	emitChange: function() {
		this.emit(CHANGE_EVENT);
	},
	addChangeListener: function(callback) {
		this.on(CHANGE_EVENT, callback);
	},
	removeChangeListener: function(callback) {
		this.removeListener(CHANGE_EVENT, callback);
	},
	getAll: function() {
		return _groups;
	}
});

GroupsStore.dispatchToken = Dispatcher.register(function(payload) {
	var action = payload.action;

	switch (action.type) {
		case ActionTypes.UPDATE_GROUPS:
			_addGroups(action.apps);
			GroupsStore.emitChange();
			break;
		default:
	}
});

module.exports = GroupsStore;
