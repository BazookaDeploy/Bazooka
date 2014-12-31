var ApplicationDispatcher = require('../Base/Dispatcher');
var ActionTypes = require('./Constants').ActionTypes;
var reqwest = require("reqwest");

module.exports = {
  updateEnviroments : function(applicationId){
    reqwest({
      url:"/api/enviroments/"+applicationId,
      type:'json',
      contentType: 'application/json',
      method:"get"
    }).then((x => {
      ApplicationDispatcher.handleServerAction({
        type: ActionTypes.UPDATE_ENVIROMENTS,
        apps: x
      });
    }))
  },

  createEnviroment:function(applicationId, name, description){
    var promise = reqwest({
      url:"/api/enviroments",
      type:'json',
      contentType: 'application/json',
      method:"post",
      data: JSON.stringify({
        ApplicationId: applicationId,
        Configuration:name,
        Description:description
      })
    });

    promise.then(x => {
      module.exports.updateEnviroments(applicationId);
    });

    return promise;
  }
};
