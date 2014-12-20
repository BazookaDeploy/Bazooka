var ApplicationDispatcher = require('../Dispatchers/ApplicationDispatcher');
var ActionTypes = require('../Constants/ApplicationsConstants').ActionTypes;

module.exports = {
  updateApplications : function(raw){
    ApplicationDispatcher.handleServerAction({
      type: ActionTypes.UPDATE_APPS,
      apps: raw
    });
  }
};
