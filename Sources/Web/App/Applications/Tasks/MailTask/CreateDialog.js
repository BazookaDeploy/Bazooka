import React from "react";
import Actions from "./Actions";
import Modal from "../../../Shared/Modal";
import Button from "../../../Shared/Button";
import Input from "../../../Shared/Input";
import Select from "../../../Shared/Select";
import Textarea from "../../../Shared/Textarea";

var MailTaskCreateDialog = React.createClass({
  getInitialState:function(){
    return {
      Name:"",
      Text:"",
      Recipients:"",
      Sender:""
    };
  },

  create:function(){
    if(this.state.Name!="" && this.state.Recipients!="" && this.state.Sender!=""){
      Actions.createMailTask(this.state.Name, this.state.Text,this.state.Recipients,this.state.Sender, this.props.EnviromentId, this.props.ApplicationId).then(x => {
        this.props.onClose()
        this.props.onCreate();
      })
    }
  },

  render:function(){
    return(
     <Modal {...this.props}>
      <Modal.Header>Add new task</Modal.Header>
     <Modal.Body>
           <Input title="Name" placeholder="Name" autoFocus onChange={(e) => this.setState({Name:e.target.value})}/>
           <Input title="Recipients"placeholder="Recipients" onChange={(e) => this.setState({Recipients:e.target.value})} />
           <Input title="Sender" placeholder="Sender" onChange={(e) => this.setState({Sender:e.target.value})} />
           <Textarea title="Text" placeholder="Text" onChange={(e) => this.setState({Text:e.target.value})} />
     </Modal.Body>
     <Modal.Footer>
       <Button onClick={this.props.onClose}>Close</Button>
       <Button primary onClick={this.create}>Create</Button>

     </Modal.Footer>
     </Modal>);
  }
});

module.exports = MailTaskCreateDialog;
