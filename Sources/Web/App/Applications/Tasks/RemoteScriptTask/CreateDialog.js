import React from "react";
import Actions from "./Actions";
import Modal from "../../../Shared/Modal";
import Button from "../../../Shared/Button";
import Input from "../../../Shared/Input";
import Select from "../../../Shared/Select";
import Textarea from "../../../Shared/Textarea";
import Notification from "../../../Shared/Notifications";

var RemoteScriptTaskCreateDialog = React.createClass({
  getInitialState:function(){
    return {
      Name:"",
      Script:"",
      Machine:"",
      Folder:"",
      Agents:[]
    };
  },

  componentDidMount:function(){
    Actions.getAgents(this.props.EnviromentId).then(x => {
      this.setState({Agents:x});
    })
  },

  create:function(){
    if(this.state.Name!="" && this.state.Script!=""&& this.state.Machine!=""&& this.state.Folder!=""){
      Actions.createRemoteScriptTask(this.state.Name, this.state.Script,this.state.Machine,this.state.Folder, this.props.EnviromentId, this.props.ApplicationId).then(x => {
        Notification.Notify(x);
        this.props.onCreate();
        this.props.onClose();
      })
    }
  },

  render:function(){
    return(
     <Modal {...this.props}>
        <Modal.Header>Add new task</Modal.Header>
     <Modal.Body>
           <Input title="Name" placeholder="Name" autoFocus onChange={(e) => this.setState({Name:e.target.value})} />
             <Select  title="Machine"  onChange={(e) => this.setState({Machine:e.target.value})}>
               <option></option>
               {this.state.Agents.map(x => (<option value={x.Id}>{x.Name}- {x.Address}</option>))}
            </Select>
           <Input title="Folder" placeholder="Folder" onChange={(e) => this.setState({Folder:e.target.value})} />
           <Textarea title="Script"  placeholder="Script" onChange={(e) => this.setState({Script:e.target.value})} />
     </Modal.Body>
     <Modal.Footer>
       <Button onClick={this.props.onClose}>Close</Button>
       <Button primary onClick={this.create}>Create</Button>
     </Modal.Footer>
     </Modal>);
  }
});

module.exports = RemoteScriptTaskCreateDialog;
