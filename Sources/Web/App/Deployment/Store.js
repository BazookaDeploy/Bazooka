import Dispatcher from "../Base/Dispatcher";
import Constants from "./Constants";
import EventEmitter from "events";
import assign from "object-assign";

var ActionTypes = Constants.ActionTypes;
var CHANGE_EVENT = 'change';

var _deployUnits = [];

function _addDeployment(raw) {
  _deployUnits=raw;
}

var DeploymentStore = assign({}, EventEmitter.prototype, {
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

DeploymentStore.dispatchToken = Dispatcher.register(function(payload) {
  var action = payload.action;

  switch(action.type) {
    case ActionTypes.UPDATE_DEPLOYMENT:
      _addDeployment(action.apps);
      DeploymentStore.emitChange();
      break;

      default:
      }
    });

module.exports = DeploymentStore;
