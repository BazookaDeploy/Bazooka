import reqwest from "reqwest";

module.exports = {
  updateDeployments : function(filter){
    debugger;

    var query = "";
    var date = new Date();
    date.setHours(0,0,0,0);

    if(filter === "Yesterday"){
      date.setDate(date.getDate()-1);
    }

    if(filter === "Last week"){
      date.setDate(date.getDate()-7);
    }

     if(filter === "Last month"){
       date.setDate(date.getDate()-30);
     }

     query = "?startDate="+ date.toISOString()+ "";

    return reqwest({
      url:"/api/deployment/Filter" + query,
      type:'json',
      contentType: 'application/json',
      method:"get"
    })
  }
};
