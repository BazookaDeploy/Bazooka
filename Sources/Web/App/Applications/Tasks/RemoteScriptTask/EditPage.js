import React from "react";
import Button from "../../../Shared/Button";
import Input from "../../../Shared/Input";
import Select from "../../../Shared/Select";
import Textarea from "../../../Shared/Textarea";
import Actions from "./Actions";


var EditPage = React.createClass({
  getInitialState:function(){
    return {
      Id:0,
      EnviromentId:0,
      Name:"",
      Script:"",
			AgentId:"",
			Folder:"",
      Agents:[]
    };
  },

  componentDidMount:function(){
    this.update(this.props.params.taskId,this.props.params.enviromentId);
  },

  componentWillReceiveProps(nextProps){
    if(this.props.params.taskId!=nextProps.params.taskId || this.props.params.enviromentId != nextProps.params.enviromentId){
        this.update(nextProps.params.taskId,nextProps.params.enviromentId);
    }
  },

  update(taskId,enviromentId){
    Actions.getRemoteScriptTask(taskId).then(x => {
      this.setState(x);
    })
		Actions.getAgents(enviromentId).then(x => {
			this.setState({Agents:x})
		})
  },

  save:function(){
    if(this.state.Name!="" && this.state.Script!=""&& this.state.Machine!=""&& this.state.Folder!=""){
      Actions.updateRemoteScriptTask(this.state.Id,this.state.Name, this.state.Script,this.state.AgentId,this.state.Folder, this.state.EnviromentId, this.state.ApplicationId)
    }
  },

  render:function(){
    return(
      <div>
           <Input title="Name"placeholder="Name" autoFocus value={this.state.Name} onChange={(e)=> this.setState({Name: e.target.value})}  />
						<Select title="Agent" className="form-control" value={this.state.AgentId} onChange={(e)=> this.setState({AgentId: e.target.value})} >
								{this.state.Agents.map(x => (<option value={x.Id}>{x.Name}- {x.Address}</option>))}
						</Select>
						<Input title="Folder" placeholder="Folder" value={this.state.Folder} onChange={(e)=> this.setState({Folder: e.target.value})}  />
           <Textarea title="Script" placeholder="Script" value={this.state.Script} onChange={(e)=> this.setState({Script: e.target.value})}  />
       <Button primary onClick={this.save}>Save</Button>
       </div>
);
   }
});


module.exports=EditPage;
