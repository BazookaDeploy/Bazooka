import reqwest from "reqwest";

module.exports = {
  cancelDeployment:function(id){
    return reqwest({
      url:"/api/deploy/cancel?deploymentId="+id,
      type:'json',
      contentType: 'application/json',
      method:"get"
    })
  },

  updateDeployment : function(id){
    return reqwest({
      url:"/api/deployment/"+id,
      type:'json',
      contentType: 'application/json',
      method:"get"
    })
  }
};
