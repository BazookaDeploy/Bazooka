import Dispatcher from "../../Base/Dispatcher";
import Constants from "./Constants";
import { EventEmitter } from "events";
import assign from "object-assign";

var ActionTypes = Constants.ActionTypes;
var CHANGE_EVENT = 'change';

var _deployUnits = [];

function _addDeployUnits(raw) {
  _deployUnits=raw;
}

function _addDeployUnit(raw) {
  _deployUnits=_deployUnits.concat(raw);
}

var DeployUnitsStore = assign({}, EventEmitter.prototype, {
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
  },
  getSingle:function(id){
    if(_deployUnits==null){return null;}

    return _deployUnits.filter(x=> x.Id == id)[0];
  }
});

DeployUnitsStore.dispatchToken = Dispatcher.register(function(payload) {
  var action = payload.action;

  switch(action.type) {
    case ActionTypes.UPDATE_DEPLOYUNITS:
      _addDeployUnits(action.apps);
      DeployUnitsStore.emitChange();
      break;

      case ActionTypes.UPDATE_DEPLOYUNIT:
        _addDeployUnit(action.apps);
        DeployUnitsStore.emitChange();
        break;

      default:
      }
    });

module.exports = DeployUnitsStore;
