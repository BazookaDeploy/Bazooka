import React from "react";
import Actions from "./ActionsCreator";
import Button from "../../../Shared/Button";
import Input from "../../../Shared/Input";
import Select from "../../../Shared/Select";
import Textarea from "../../../Shared/Textarea";
import Grid from "../../../Shared/Grid";
import Tabs from "../../../Shared/Tabs";


var EditPage = React.createClass({
  addParameter : function(){
    if(this.state.key.length!=0 &&
      this.state.value.length != 0 &&
       !this.state.Parameters.some(x => x.Key == this.state.key)){

      this.setState({
				Parameters: this.state.Parameters.concat({
          Name: this.state.key,
          Value: this.state.value,
					Encrypted:this.state.Encrypted
        }),
        key:"",
        value:"",
				Encrypted:false
      })
    }
  },

  remove: function(key){
    var index = -1;
    var i;
    for(i = 0; i < this.state.Parameters.length; i++){
      if(this.state.Parameters[i].Name==key){
        index=i;
        break;
      }
    }
    this.state.Parameters.splice(index,1);
    this.setState({
			Parameters: this.state.Parameters
    });
  },

	componentDidMount() {
		this.update(this.props.params.taskId, this.props.params.enviromentId);
	},

  componentWillReceiveProps(nextProps){
    if(this.props.params.taskId!=nextProps.params.taskId || this.props.params.enviromentId != nextProps.params.enviromentId){
        this.update(nextProps.params.taskId,nextProps.params.enviromentId);
    }
  },	

	update(taskId, enviromentId){
		Actions.updateDeployUnit(taskId).then(env => {
			this.setState({
			Id: env.Id,
			Enviroment: env.EnviromentId,
			Name:env.Name,
			AgentId:env.AgentId,
			PackageName: env.PackageName,
			Directory:env.Directory,
			Repository:env.Repository,
			key:"",
			value:"",
			Encrypted:false,
			Parameters : env.Parameters,
			UninstallationScript : env.UninstallScript,
			InstallationScript:env.InstallScript,
			ConfigurationFile:env.ConfigurationFile,
			ConfigTransform:env.ConfigurationTransform,
			Configuration : env.Configuration
		})
		});

		Actions.getAgents(enviromentId).then(x => {
			this.setState({Agents:x})
		})
	},


	getInitialState: function() {

		return {
			Name:null,
			AgentId:null,
			Agents:[],
			PackageName: null,
			Directory:null,
			Repository:null,
			key:"",
			value:"",
			Encrypted:false,
			Parameters : [],
			UninstallationScript : null,
			InstallationScript:null,
			ConfigurationFile:null,
			ConfigTransform:null,
			Configuration:null
		};

	},

	save:function(){
		Actions.modifyDeployUnit(
			this.state.Id,
			this.state.Enviroment,
			this.state.Name,
			this.state.AgentId,
			this.state.PackageName,
			this.state.Directory,
			this.state.Repository,
			this.state.Parameters,
			this.state.UninstallationScript,
			this.state.InstallationScript,
			this.state.ConfigurationFile,
			this.state.ConfigTransform,
			this.state.Configuration,
			this.props.params.id
			);

    return false;
	},

 	render: function() {

		return (
			<div>
				<Tabs>
		    	<Tabs.Tab title='Settings'>
								<Input title="Name" placeholder="Name" value={this.state.Name} onChange={(e)=> this.setState({Name: e.target.value})}/>
									<Select title="Agent" value={this.state.AgentId} onChange={(e)=> this.setState({AgentId: e.target.value})}>
											{this.state.Agents.map(x => (<option value={x.Id}>{x.Name}- {x.Address}</option>))}
									</Select>
									<h5>Additional Params</h5>
										<ul>
											{this.state.Parameters.map(a => (<li>{a.Name} = {a.Encrypted? "********" : a.Value} <Button onClick={() => this.remove(a.Key)}>Remove</Button></li>))}
										</ul>
									<Grid fluid>
										<Grid.Row>

										<Grid.Col md={3}>
					            <Input title="Key" placeholder="Key"  value={this.state.key} onChange={(e)=> this.setState({key: e.target.value})} />
					          </Grid.Col>
										<Grid.Col md={3}>
					            <Input title="Value" placeholder="Value"  value={this.state.value} onChange={(e)=> this.setState({value: e.target.value})} />
					          </Grid.Col>
										<Grid.Col md={2}>
					            <label htmlFor="Encrypted">Encrypted</label><br/>
					            <input type="checkbox" id="Encrypted" value={this.state.Encrypted} onChange={(e)=> this.setState({Encrypted: e.target.checked})} />
					          </Grid.Col>
										<Grid.Col md={4}>
					            <br />
					            <Button primary onClick={this.addParameter} >Add Parameter</Button>
					          </Grid.Col>
					        </Grid.Row>
									</Grid>

								<Input title="PackageName" placeholder="PackageName" value={this.state.PackageName} onChange={(e)=> this.setState({PackageName: e.target.value})} />
								<Input title="Directory" placeholder="Directory" value={this.state.Directory} onChange={(e)=> this.setState({Directory: e.target.value})} />
								<Input title="Repository" placeholder="Repository" value={this.state.Repository} onChange={(e)=> this.setState({Repository: e.target.value})} />
					</Tabs.Tab>
		    	<Tabs.Tab title='Scripts'>
								<Textarea rows="10" title="Installation script" placeholder="Installation Script" value={this.state.InstallationScript} onChange={(e)=> this.setState({InstallationScript: e.target.value})} />
								<Textarea rows="10" title="Uninstallation script" placeholder="Uninstallation Script" value={this.state.UninstallationScript} onChange={(e)=> this.setState({UninstallationScript: e.target.value})} />
					</Tabs.Tab>
					<Tabs.Tab title='Configurations'>
							<Input title="Configuration" placeholder="Specific configuration (optional)" value={this.state.Configuration} onChange={(e)=> this.setState({Configuration: e.target.value})}  />
							<Input title="Configuration File"  placeholder="Configuration File" value={this.state.ConfigurationFile} onChange={(e)=> this.setState({ConfigurationFile: e.target.value})}  />
							<Textarea rows="10" title="Config transform" placeholder="Config Transform" value={this.state.ConfigTransform} onChange={(e)=> this.setState({ConfigTransform: e.target.value})} />
					</Tabs.Tab>
		  	</Tabs>

				<Button primary onClick={this.save}>Save</Button>
			</div>
		);
	}
});

module.exports = EditPage;
