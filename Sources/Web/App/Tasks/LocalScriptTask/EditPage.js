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
      Script:""
    };
  },

  componentDidMount:function(){
    Actions.getLocalScriptTask(this.getParams().taskId).then(x => {
      this.setState(x);
    })
  },

  save:function(){
    if(this.state.Name!="" && this.state.Script!=""){
      Actions.updateLocalScriptTask(this.state.Id,this.state.Name, this.state.Script, this.state.EnviromentId)
    }
  },

  render:function(){
    return(
      <div>
        <form role="form" onSubmit={this.create}>
         <div className="form-group">
           <label htmlFor="Name">Name</label>
           <input type="text" className="form-control" id="Name" placeholder="Name" autoFocus valueLink={this.linkState('Name')} />
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
