import React from "react";
import Actions from "./Actions";
import Modal from "../../../Shared/Modal";
import Button from "../../../Shared/Button";
import Input from "../../../Shared/Input";
import Select from "../../../Shared/Select";
import Textarea from "../../../Shared/Textarea";
import Notification from "../../../Shared/Notifications";

var DatabaseTaskCreateDialog = React.createClass({
  getInitialState:function(){
    return {
      Name:"",
      ConnectionString:"",
      Pack:"",
      DatabaseName:"",
      "Repository":"",
      Machine:"",
      Agents:[]
    };
  },

  componentDidMount:function(){
    Actions.getAgents(this.props.EnviromentId).then(x => {
      this.setState({Agents:x})
    })
  },

  create:function(){
    if(this.state.Name!="" && this.state.ConnectionString!=""&& this.state.Pack!=""&& this.state.DatabaseName!=""&& this.state.Repository!=""&& this.state.Machine!=""){
      Actions.createDatabaseTask(this.state.Name, this.state.ConnectionString,this.state.Pack,this.state.DatabaseName, this.props.EnviromentId,this.state.Repository,this.state.Machine, this.props.ApplicationId).then(x => {
        Notification.Notify(x);
        this.props.onCreate();
        this.props.onClose();
      })
    }
  },

  render:function(){
    return(
     <Modal {...this.props}>
     <Modal.Header>Add new Database task</Modal.Header>
     <Modal.Body>
           <Input title="Name" placeholder="Name" autoFocus onChange={(e)=> this.setState({Name: e.target.value})} />
           <Input title="Connection string"  placeholder="ConnectionString" onChange={(e)=> this.setState({ConnectionString: e.target.value})} />
           <Input title="Package" placeholder="Package" onChange={(e)=> this.setState({Pack: e.target.value})} />
           <Select title="Machine" onChange={(e)=> this.setState({Machine: e.target.value})}>
              <option />
             {this.state.Agents.map(x => (<option value={x.Id}>{x.Name}- {x.Address}</option>))}
          </Select>
           <Input title="Repository" placeholder="Repository" onChange={(e)=> this.setState({Repository: e.target.value})} />
         <Input title="Database name" placeholder="Database Name" onChange={(e)=> this.setState({DatabaseName: e.target.value})} />
     </Modal.Body>
     <Modal.Footer>
       <Button onClick={this.props.onClose}>Close</Button>
       <Button primary onClick={this.create}>Create</Button>
     </Modal.Footer>
     </Modal>);
  }
});

module.exports = DatabaseTaskCreateDialog;
