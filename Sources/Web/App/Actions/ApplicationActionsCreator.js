var ApplicationDispatcher = require('../Dispatchers/ApplicationDispatcher');
var ActionTypes = require('../Constants/ApplicationsConstants').ActionTypes;
var reqwest = require("reqwest");

module.exports = {
  updateApplications : function(){
    reqwest({
      url:"/api/applications",
      method:"get"
    }).then((x => {
      ApplicationDispatcher.handleServerAction({
        type: ActionTypes.UPDATE_APPS,
        apps: x
      });
    }))
  }
};
