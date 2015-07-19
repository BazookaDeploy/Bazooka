import reqwest from "reqwest";

module.exports = {
  getAgents : function(){
    return reqwest({
        url:"/api/update/agents",
        type:'json',
        contentType: 'application/json',
        method:"get"
    })
  }
};
