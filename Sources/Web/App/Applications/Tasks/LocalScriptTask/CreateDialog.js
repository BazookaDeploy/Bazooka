import React from "react";
import Actions from "./Actions";
import Modal from "../../../Shared/Modal";
import Button from "../../../Shared/Button";
import Input from "../../../Shared/Input";
import Select from "../../../Shared/Select";
import Textarea from "../../../Shared/Textarea";
import Notification from "../../../Shared/Notifications";

var LocalScriptTaskCreateDialog = React.createClass({
  getInitialState:function(){
    return {
      Name:"",
      Script:""
    };
  },

  create:function(){
    if(this.state.Name!="" && this.state.Script!=""){
      Actions.createLocalScriptTask(this.state.Name, this.state.Script, this.props.EnviromentId, this.props.ApplicationId).then(x => {
        Notification.Notify(x);
        this.props.onCreate();
        this.props.onClose();
      })
    }
  },

  render:function(){
    return(
     <Modal {...this.props}>
     <Modal.Header>
      Add new task
     </Modal.Header>
     <Modal.Body>
           <Input title="Name" placeholder="Name" autoFocus onChange={(e) => this.setState({Name: e.target.value})} />
           <Textarea title="Script" placeholder="Script" onChange={(e) => this.setState({Script: e.target.value})} />
     </Modal.Body>
     <Modal.Footer>
       <Button onClick={this.props.onClose}>Close</Button>
       <Button primary onClick={this.create}>Create</Button>

     </Modal.Footer>
     </Modal>);
  }
});

module.exports = LocalScriptTaskCreateDialog;
