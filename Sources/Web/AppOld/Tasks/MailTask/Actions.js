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

  createMailTask: function(name, text,recipients,sender, enviromentId, applicationId){
    return  reqwest({
      url: "/api/mailTasks/CreateMailtask",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        EnviromentId:enviromentId,
        Name:name,
        Text:text,
        Recipients:recipients,
        Sender:sender,
        ApplicationId:applicationId
      })
    });
  },

  updateMailTask: function(id, name, text,recipients,sender, enviromentId, applicationId){
    return  reqwest({
      url: "/api/mailTasks/ModifyMailTask",
      type: 'json',
      contentType: 'application/json',
      method: "post",
      data: JSON.stringify({
        MailTaskId:id,
        EnviromentId:enviromentId,
        Name:name,
        Text:text,
        Recipients:recipients,
        Sender:sender,
        ApplicationId:applicationId
      })
    });
  },
}
