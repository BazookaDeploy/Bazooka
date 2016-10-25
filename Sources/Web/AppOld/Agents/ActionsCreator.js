import reqwest from "reqwest";

module.exports = {
  getAgent : function(id){
    return reqwest({
        url:"/api/agents/"+id,
        type:'json',
        contentType: 'application/json',
        method:"get"
    })
  },

  rename : function(id, enviromentId, name){
    return reqwest({
        url:"/api/agents/Rename",
        type:'json',
        contentType: 'application/json',
        method:"post",
        data: JSON.stringify({
          EnviromentId:enviromentId,
          AgentId : id,
          Name : name
        })
    })
  },

  changeAddress : function(id, enviromentId, address){
    return reqwest({
        url:"/api/agents/ChangeAddress",
        type:'json',
        contentType: 'application/json',
        method:"post",
        data: JSON.stringify({
          EnviromentId:enviromentId,
          AgentId : id,
          Address : address
        })
    })
  },

  testAgent: function (url) {
    return reqwest({
      url: "/api/agents/test?url=" + url,
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
        url:"/api/agents/update?agent="+agent,
        method:"post",
        data:data
    })
  }
};
