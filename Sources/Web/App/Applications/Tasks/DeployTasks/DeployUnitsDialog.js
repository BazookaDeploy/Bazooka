import React from "react";
import Router from 'react-router';
import Actions from "./ActionsCreator";
import Modal from "../../../Shared/Modal";
import Button from "../../../Shared/Button";
import Input from "../../../Shared/Input";
import Select from "../../../Shared/Select";
import Textarea from "../../../Shared/Textarea";

var CreateDialog = React.createClass({

  getInitialState: function() {
    return {
      name:"",
      machine:"",
      packageName: "",
      directory:"",
      repository:"",
      key:"",
      value:"",
      Encrypted:false,
      params : [],
      Agents:[]
    };
  },

  componentDidMount:function(){
    Actions.getAgents(this.props.Enviroment).then(x => {
      this.setState({Agents:x})
    })
  },

  create:function(){
    if(this.state.name.length != 0 &&
      this.state.packageName.length != 0 &&
      this.state.repository.length != 0 &&
      this.state.directory.length != 0){

      Actions.createDeployUnit(
        this.props.Enviroment,
        this.state.name,
        this.state.machine,
        this.state.packageName,
        this.state.directory,
        this.state.repository,
        this.state.params,this.props.ApplicationId)
             .then(x => {this.props.onCreate();this.props.onClose();});
    }
    return false;
  },

  render:function(){
    return(
      <Modal {...this.props}>
      <Modal.Header>Create new deploy unit</Modal.Header>
      <Modal.Body>
            <Input title="Name" placeholder="Name" autoFocus  onChange={(e) => this.setState({name:e.target.value})}/>
              <Select  title="Machine"  onChange={(e) => this.setState({machine:e.target.value})}>
                <option></option>
                {this.state.Agents.map(x => (<option value={x.Id}>{x.Name}- {x.Address}</option>))}
             </Select>
            <Input title="Package name" placeholder="PackageName"  onChange={(e) => this.setState({packageName:e.target.value})} />
            <Input title="Directory" placeholder="Directory"  onChange={(e) => this.setState({directory:e.target.value})} />
          <Input title="Repository" placeholder="Repository" onChange={(e) => this.setState({repository:e.target.value})}/>
      </Modal.Body>
      <Modal.Footer>
        <Button primary onClick={this.create}>Create</Button>
        <Button onClick={this.props.onClose}>Close</Button>
      </Modal.Footer>
      </Modal>);
    }
  });


    module.exports = CreateDialog;
