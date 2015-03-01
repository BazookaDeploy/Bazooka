var Dispatcher = require('../Base/Dispatcher');
var ActionTypes = require('./Constants').ActionTypes;
var reqwest = require("reqwest");

module.exports = {
  updateDeployment : function(id){
    reqwest({
      url:"/api/deployment/"+id,
      type:'json',
      contentType: 'application/json',
      method:"get"
    }).then((x => {
      Dispatcher.handleServerAction({
        type: ActionTypes.UPDATE_DEPLOYMENT,
        apps: x
      });
    }))
  }
};
