import reqwest from "reqwest";

module.exports = {
  getMailTask:function(id){
    return reqwest({
      url: "/api/mailTasks/"+id,
      type: 'json',
      contentType: 'application/json',
      method: "get",      
    });
  },

  createMailTask: function(name, text,recipients,sender, enviromentId){
    return  reqwest({
      url: "/api/mailTasks",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        EnviromentId:enviromentId,
        Name:name,
        Text:text,
        Recipients:recipients,
        Sender:sender
      })
    });
  },

  updateMailTask: function(id, name, text,recipients,sender, enviromentId){
    return  reqwest({
      url: "/api/mailTasks",
      type: 'json',
      contentType: 'application/json',
      method: "put",
      data: JSON.stringify({
        Id:id,
        EnviromentId:enviromentId,
        Name:name,
        Text:text,
        Recipients:recipients,
        Sender:sender
      })
    });
  },
}
