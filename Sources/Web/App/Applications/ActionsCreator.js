import reqwest from "reqwest";

module.exports = {
  updateApplications : function(){
    return reqwest({
        url:"/api/applications",
        type:'json',
        contentType: 'application/json',
        method:"get"
    })
  },

  createApplication:function(name){
    var promise = reqwest({
      url:"/api/applications/Create",
      type:'json',
      contentType: 'application/json',
      method:"post",
      data:JSON.stringify({
        Name:name
      })
    });

    return promise;
  }
};
