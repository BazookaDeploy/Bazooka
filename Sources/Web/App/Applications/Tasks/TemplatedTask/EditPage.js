import React from "react";
import Button from "../../../Shared/Button";
import Input from "../../../Shared/Input";
import Select from "../../../Shared/Select";
import Textarea from "../../../Shared/Textarea";
import Actions from "./Actions";
import Notification from "../../../Shared/Notifications";

var EditPage = React.createClass({
  getInitialState:function(){
    return {
      Id:0,
      EnviromentId:0,
	  Name:"",
	  AgentId:"",
      Agents:[]
    };
  },

	componentDidMount() {
		this.update(this.props.params.taskId, this.props.params.enviromentId);
	},

  componentWillReceiveProps(nextProps){
    if(this.props.params.taskId!=nextProps.params.taskId || this.props.params.enviromentId != nextProps.params.enviromentId){
        this.update(nextProps.params.taskId,nextProps.params.enviromentId);
    }
  },	

  update:function(taskId, enviromentId){
        Actions.getTemplatedTask(taskId).then(x => {
          this.setState(x);
        })
		Actions.getAgents(enviromentId).then(x => {
			this.setState({Agents:x})
		})
  },

  save:function(){
      if (this.state.Name != "") {
          Actions.modifyTemplatedTask(this.state.Id, this.state.EnviromentId, this.state.ApplicationId, this.state.AgentId, this.state.Parameters).then(x => {
        Notification.Notify(x);
      })
    }
  },

  updateToLatest: function () {
      if (this.state.Name != "") {
          Actions.updateTemplatedTasks(this.state.Id, this.state.ApplicationId, this.state.LastKnownVersion).then(x => {
              Notification.Notify(x);
          })
      }
  },

  rename: function () {
      if (this.state.Name != "") {
          Actions.renameTemplatedTask(this.state.Id, this.state.EnviromentId, this.state.ApplicationId, this.state.Name).then(x => {
              Notification.Notify(x);
          })
      }
  },

  setParameter: function (value, id) {
      this.state.Parameters.filter(x => x.TaskTemplateParameterId == id)[0].Value = value;
      this.setState({ Parameters: this.state.Parameters })
  },


  render:function(){
    return(
      <div>

            <Input title="Name" placeholder="Name" autoFocus value={this.state.Name} onChange={(e) => this.setState({ Name: e.target.value })} />

            <Button primary block onClick={this.rename}>Rename</Button>


											<Select title="Agent"  value={this.state.AgentId} onChange={(e)=>this.setState({AgentId:e.target.value})}>
													{this.state.Agents.map(x => (<option value={x.Id}>{x.Name}- {x.Address}</option>))}
											</Select>

                                        <Button primary block onClick={this.save}>Save</Button>
                                        <Button primary block onClick={this.updateToLatest}>Update to latest version</Button>

       </div>
);
   }
});


module.exports=EditPage;
