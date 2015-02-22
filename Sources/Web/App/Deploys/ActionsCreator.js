var Dispatcher = require('../Base/Dispatcher');
var reqwest = require("reqwest");

module.exports = {

  startDeploy:function(enviromentId, version){
    var promise = reqwest({
      url:"/api/deploy/deploy?enviromentId="+enviromentId+"&version="+version,
      type:'json',
      contentType: 'application/json',
      method:"get"
    });
    return promise;
  }
};
