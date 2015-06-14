var Dispatcher = require('../Base/Dispatcher');
var ActionTypes = require('./Constants').ActionTypes;
var reqwest = require("reqwest");

module.exports = {
  updateDeployments : function(filter){
    debugger;
    
    var query = "";
    var date = new Date();
    date.setHours(0,0,0,0);
    
    if(filter === "Today"){
      query = "?$filter=StartDate gt DateTime'"+ date.toISOString()+ "' ";
    }
    
    if(filter === "Yesterday"){
      date.setDate(date.getDate()-1);
      query = "?$filter=StartDate gt DateTime'"+ date.toISOString()+ "' ";
    } 
    
    if(filter === "Last week"){
      date.setDate(date.getDate()-7);
      query = "?$filter=StartDate gt DateTime'"+ date.toISOString()+ "' ";
    } 
 
     if(filter === "Last month"){
       date.setDate(date.getDate()-30);
      query = "?$filter=StartDate gt DateTime'"+ date.toISOString()+ "' ";
    } 
    
    reqwest({
      url:"/api/deployment/" + query,
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
