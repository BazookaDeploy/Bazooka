var Dispatcher = require("../Base/Dispatcher");
var Constants = require("./Constants");
var EventEmitter = require("events").EventEmitter;
var assign = require("object-assign");

var ActionTypes = Constants.ActionTypes;
var CHANGE_EVENT = 'change';

var _deployUnits = [];

function _addDeployments(raw) {
  _deployUnits=raw;
}

var DeploymentsStore = assign({}, EventEmitter.prototype, {
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
    return _deployUnits;
  }
});

DeploymentsStore.dispatchToken = Dispatcher.register(function(payload) {
  var action = payload.action;

  switch(action.type) {
    case ActionTypes.UPDATE_DEPLOYMENTS:
      _addDeployments(action.apps);
      DeploymentsStore.emitChange();
      break;

      default:
      }
    });

module.exports = DeploymentsStore;
