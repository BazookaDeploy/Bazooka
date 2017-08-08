import React from "react";
import Button from "../../../Shared/Button";
import Input from "../../../Shared/Input";
import Select from "../../../Shared/Select";
import Textarea from "../../../Shared/Textarea";
import Table from "../../../Shared/Table";
import Actions from "./Actions";
import Notification from "../../../Shared/Notifications";

var EditPage = React.createClass({
  getInitialState:function(){
    return {
      Id:0,
      EnviromentId:0,
	  Name:"",
	  AgentId:"",
      Agents: [],
      Parameters: []
    };
  },

	componentDidMount() {
		this.update(this.props.params.taskId, this.props.params.enviromentId);
	},

    componentWillReceiveProps(nextProps) {
        debugger;
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
          Actions.modifyTemplatedTask(this.state.Id, this.state.EnviromentId, this.state.ApplicationId, this.state.AgentId, this.state.PackageName, this.state.Repository, this.state.Parameters).then(x => {
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
              this.props.onChange();
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

            <Input title="Package" placeholder="PackageName" value={this.state.PackageName || ""} onChange={(e) => this.setState({ PackageName: e.target.value })} />

            <Input title="Repository" placeholder="Repository" value={this.state.Repository || ""} onChange={(e) => this.setState({ Repository: e.target.value })} />


											<Select title="Agent"  value={this.state.AgentId} onChange={(e)=>this.setState({AgentId:e.target.value})}>
													{this.state.Agents.map(x => (<option value={x.Id}>{x.Name}- {x.Address}</option>))}
											</Select>

                                            <h3>Parameters </h3>

                                            <Table>
                                                <Table.Head>
                                                    <tr>
                                                        <th>Parameter</th>
                                                        <th>Value</th>
                                                        <th>Required</th>
                                                    </tr>
                                                </Table.Head>
                                                <Table.Body>
                                                    {this.state.Parameters.map(x => <tr>
                        <td>{x.Name} <span title={x.Description}>&#9432;</span></td>
                                                        <td><Input type={x.Encrypted ? "password" : "text" } placeholder="Value" value={x.Value} onChange={(e) => this.setParameter(e.target.value, x.TaskTemplateParameterId)} /></td>
                                                        <td>{!x.Optional && <span>Required</span>}</td>
                                                    </tr>)}
                                                </Table.Body>
                                            </Table>


                                        <Button primary block onClick={this.save}>Save</Button>
                                        <Button primary block onClick={this.updateToLatest}>Update to latest version</Button>

       </div>
);
   }
});


module.exports=EditPage;
