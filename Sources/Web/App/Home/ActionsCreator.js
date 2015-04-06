var ApplicationDispatcher = require('../Base/Dispatcher');
var ActionTypes = require('./Constants').ActionTypes;
var reqwest = require("reqwest");

module.exports = {
  updateEnviroments : function(){
    reqwest({
      url:"/api/status/",
      type:'json',
      contentType: 'application/json',
      method:"get"
    }).then((x => {
      ApplicationDispatcher.handleServerAction({
        type: ActionTypes.UPDATE_STATUS,
        apps: x
      });
    }))
  }
};
