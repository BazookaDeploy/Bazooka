import reqwest from "reqwest";

module.exports = {
  getAgents : function(){
    return reqwest({
        url:"/api/update/agents",
        type:'json',
        contentType: 'application/json',
        method:"get"
    })
  },

  testAgent: function (url) {
    return reqwest({
      url: "/api/deployUnits/test?url=" + url,
      type: 'json',
      contentType: 'application/json',
      method: "get"
    })
  },

  uploadFiles: function(files, agent){
    var data = new FormData();
    data.append(0,files[0]);

    return reqwest({
        processData:false,
        url:"/api/update/update?agent="+agent,
        method:"post",
        data:data
    })
  }
};
