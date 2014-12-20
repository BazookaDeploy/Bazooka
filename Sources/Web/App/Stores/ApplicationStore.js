var ApplicationDispatcher = require("../Dispatchers/ApplicationDispatcher");
var ApplicationConstants = require("../Constants/ApplicationsConstants");
var EventEmitter = require("events").EventEmitter;
var assign = require("object-assign");

var ActionTypes = ApplicationConstants.ActionTypes;
var CHANGE_EVENT = 'change';

var _applications = [];

function _addApplications(raw) {
  _applications=raw;
}

var ApplicationsStore = assign({}, EventEmitter.prototype, {
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
    return _applications;
  }
});

ApplicationsStore.dispatchToken = ApplicationDispatcher.register(function(payload) {
  var action = payload.action;

  switch(action.type) {
    case ActionTypes.UPDATE_APPS:
      _addApplications(action.apps);
      ApplicationsStore.emitChange();
      break;

    default:
  }
});

module.exports = ApplicationsStore;
