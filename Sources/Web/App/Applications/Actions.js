import reqwest from "reqwest";

module.exports = {
    getApplications : function(){
        return reqwest({
            url:"/api/Applications/",
            type:'json',
            contentType: 'application/json',
            method:"get"
        })
    },

    getAllApplications : function(){
        return reqwest({
            url:"/api/Applications/All",
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
  },

  cloneApplication: function (name,app){
    var promise = reqwest({
      url:"/api/applications/CreateApplicationFromExisting",
      type:'json',
      contentType: 'application/json',
      method:"post",
      data:JSON.stringify({
        Name:name,
        OriginalApplicationId: app
      })
    });

    return promise;
  }
};
