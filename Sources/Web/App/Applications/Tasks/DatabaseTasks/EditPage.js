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
      ConnectionString:"",
      Pack:"",
      DatabaseName:"",
			Repository:"",
        AgentId: "",
    Partial: false,
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
    Actions.getDatabaseTask(taskId).then(x => {
			x.Pack=x.Package;
      this.setState(x);
    })
		Actions.getAgents(enviromentId).then(x => {
			this.setState({Agents:x})
		})
  },

  save:function(){
    if(this.state.Name!="" && this.state.ConnectionString!=""&& this.state.Pack!=""&& this.state.DatabaseName!=""&& this.state.Repository!=""&& this.state.Machine!=""){
      Actions.updateDatabaseTask(this.state.Id,this.state.Name, this.state.ConnectionString,this.state.Pack,this.state.DatabaseName, this.state.EnviromentId, this.state.Repository,this.state.AgentId, this.state.ApplicationId, this.state.Partial).then(x => {
          Notification.Notify(x);
          this.props.onChange();
      })
    }
  },

  render:function(){
    return(
      <div>

											<Input title="Name" placeholder="Name" autoFocus  value={this.state.Name}  onChange={(e)=>this.setState({Name:e.target.value})} />
		<Input title="Connection string" placeholder="ConnectionString"  value={this.state.ConnectionString}   onChange={(e)=>this.setState({ConnectionString:e.target.value}) }/>
											<Input title="Package" placeholder="Package"   value={this.state.Pack}  onChange={(e)=>this.setState({Pack:e.target.value})} />
											<Select title="Agent"  value={this.state.AgentId} onChange={(e)=>this.setState({AgentId:e.target.value})}>
													{this.state.Agents.map(x => (<option value={x.Id}>{x.Name}- {x.Address}</option>))}
											</Select>
											<Input title="Repository" placeholder="Repository" value={this.state.Repository}  onChange={(e)=>this.setState({Repository:e.target.value})}  />
										<Input title="DatabaseName" placeholder="Database Name"value={this.state.DatabaseName}  onChange={(e)=>this.setState({DatabaseName:e.target.value})}  />
            <Select title="Partial deploy" value={this.state.Partial} onChange={(e) => this.setState({ Partial: e.target.value })}>
                <option value={"false"}>Full deploy</option>
                <option value={"true"}>Partial deploy</option>
            </Select>
       <Button primary block onClick={this.save}>Save</Button>
       </div>
);
   }
});


module.exports=EditPage;
