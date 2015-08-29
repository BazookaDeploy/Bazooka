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
      ConnectionString:"",
      Pack:"",
      DatabaseName:"",
			Repository:""
    };
  },

  componentDidMount:function(){
    Actions.getDatabaseTask(this.getParams().taskId).then(x => {
			x.Pack=x.Package;
      this.setState(x);
    })
  },

  save:function(){
    if(this.state.Name!="" && this.state.ConnectionString!=""&& this.state.Pack!=""&& this.state.DatabaseName!=""&& this.state.Repository!=""){
      Actions.updateDatabaseTask(this.state.Id,this.state.Name, this.state.ConnectionString,this.state.Pack,this.state.DatabaseName, this.state.EnviromentId, this.state.Repository)
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
											<label htmlFor="ConnectionString">Connection String</label>
											<input type="text" className="form-control" id="ConnectionString" placeholder="ConnectionString" valueLink={this.linkState('ConnectionString')} />
										</div>
										<div className="form-group">
											<label htmlFor="Pack">Package</label>
											<input type="text" className="form-control" id="Pack" placeholder="Package" valueLink={this.linkState('Pack')} />
										</div>
										<div className="form-group">
											<label htmlFor="Repository">Repository</label>
											<input type="text" className="form-control" id="Repository" placeholder="Repository" valueLink={this.linkState('Repository')} />
										</div>

										<div className="form-group">
											<label htmlFor="DatabaseName">Database Name</label>
										<input type="text" className="form-control" id="DatabaseName" placeholder="Database Name" valueLink={this.linkState('DatabaseName')} />
										</div>
									</form>

       <button className="btn btn-primary pull-right" onClick={this.save}>Save</button>
       </div>
);
   }
});


module.exports=EditPage;
