var Dispatcher = require('../Base/Dispatcher');
var ActionTypes = require('./Constants').ActionTypes;
var reqwest = require("reqwest");

module.exports = {
  updateDeployments : function(){
    reqwest({
      url:"/api/deployment/",
      type:'json',
      contentType: 'application/json',
      method:"get"
    }).then((x => {
      Dispatcher.handleServerAction({
        type: ActionTypes.UPDATE_DEPLOYMENTS,
        apps: x
      });
    }))
  }
};
