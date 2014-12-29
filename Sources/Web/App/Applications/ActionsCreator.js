var ApplicationDispatcher = require('../Base/Dispatcher');
var ActionTypes = require('./Constants').ActionTypes;
var reqwest = require("reqwest");

module.exports = {
  updateApplications : function(){
    reqwest({
        url:"/api/applications",
        type:'json',
        contentType: 'application/json',
        method:"get"
    }).then((x => {
      ApplicationDispatcher.handleServerAction({
        type: ActionTypes.UPDATE_APPS,
        apps: x
      });
    }))
  },

  createApplication:function(name){
    var promise = reqwest({
      url:"/api/applications?name="+name,
      type:'json',
      contentType: 'application/json',
      method:"post"
    });

    promise.then(x => {
      module.exports.updateApplications();
    });

    return promise;
  }
};
