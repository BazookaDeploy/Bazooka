var ApplicationsConstants = require('../Constants/ApplicationsConstants');
var Dispatcher = require('flux').Dispatcher;
var assign = require('object-assign');

var PayloadSources = ApplicationsConstants.PayloadSources;

var ApplicationsDispatcher = assign(new Dispatcher(), {

  handleServerAction: function(action) {
    var payload = {
      source: PayloadSources.SERVER_ACTION,
      action: action
    };
    this.dispatch(payload);
  },

  handleViewAction: function(action) {
    var payload = {
      source: PayloadSources.VIEW_ACTION,
      action: action
    };
    this.dispatch(payload);
  }

});

module.exports = ApplicationsDispatcher;
