import React from "react";
import LinkedState from "react/lib/LinkedStateMixin";
import Router from 'react-router';
import Actions from "./Actions";

var {
	Route, DefaultRoute, RouteHandler, Link, State
} = Router;


var EditPage = React.createClass({
	mixins: [LinkedState,State],
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
    Actions.getRemoteScriptTask(this.getParams().taskId).then(x => {
      this.setState(x);
    })
		Actions.getAgents(this.getParams().enviromentId).then(x => {
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
							      <h3>Application {this.getParams().applicationName} <i className='glyphicon glyphicon-menu-right' /> {this.getParams().enviroment} <i className='glyphicon glyphicon-menu-right' /> {this.getParams().taskName}</h3>

        <form role="form" onSubmit={this.create}>
         <div className="form-group">
           <label htmlFor="Name">Name</label>
           <input type="text" className="form-control" id="Name" placeholder="Name" autoFocus valueLink={this.linkState('Name')} />
         </div>
				<div className="form-group">
           <label htmlFor="AgentId">Machine</label>
						<select  className="form-control" id="AgentId" valueLink={this.linkState('AgentId')}>
								{this.state.Agents.map(x => (<option value={x.Id}>{x.Name}- {x.Address}</option>))}
						</select>
				</div>
				<div className="form-group">
						<label htmlFor="Folder">Folder</label>
						<input type="text" className="form-control" id="Folder" placeholder="Folder" valueLink={this.linkState('Folder')} />
					</div>
         <div className="form-group">
           <label htmlFor="Script">Script</label>
           <textarea type="text" className="form-control" id="Script" placeholder="Script" valueLink={this.linkState('Script')} />
         </div>
       </form>
       <button className="btn btn-primary pull-right" onClick={this.save}>Save</button>
       </div>
);
   }
});


module.exports=EditPage;
