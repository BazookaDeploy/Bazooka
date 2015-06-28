import Dispatcher  from  "../Base/Dispatcher";
import Constants from  "./Constants";
import  {EventEmitter} from  "events";
import assign from  "object-assign";

var ActionTypes = Constants.ActionTypes;
var CHANGE_EVENT = 'change';

var _users = [];

function _addUsers(raw) {
	_users = raw;
}


var GroupStore = assign({}, EventEmitter.prototype, {
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
		return _users;
	}
});

GroupStore.dispatchToken = Dispatcher.register(function(payload) {
	var action = payload.action;

	switch (action.type) {
		case ActionTypes.UPDATE_USERS:
			_addUsers(action.apps);
			GroupStore.emitChange();
			break;
		default:
	}
});

module.exports = GroupStore;
