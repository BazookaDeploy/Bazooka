import reqwest from "reqwest";
import Net from "../../../Shared/Net";

module.exports = {
  getMailTask:function(id){
    return Net.get("/api/mailTasks/"+id);
  },

  createMailTask: function(name, text,recipients,sender, enviromentId, applicationId){
    return Net.post("/api/mailTasks/CreateMailtask",{
        EnviromentId:enviromentId,
        Name:name,
        Text:text,
        Recipients:recipients,
        Sender:sender,
        ApplicationId:applicationId
      });
  },

  updateMailTask: function(id, name, text,recipients,sender, enviromentId, applicationId){
    return Net.post("/api/mailTasks/ModifyMailTask",{
        MailTaskId:id,
        EnviromentId:enviromentId,
        Name:name,
        Text:text,
        Recipients:recipients,
        Sender:sender,
        ApplicationId:applicationId
      });
  }
};