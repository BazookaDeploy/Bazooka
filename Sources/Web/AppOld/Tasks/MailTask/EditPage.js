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
      Text:"",
      Recipients:"",
      Sender:""
    };
  },

  componentDidMount:function(){
    Actions.getMailTask(this.getParams().taskId).then(x => {
      this.setState(x);
    })
  },

  save:function(){
    if(this.state.Name!="" && this.state.Recipients!="" && this.state.Sender!=""){
      Actions.updateMailTask(this.state.Id,this.state.Name, this.state.Text,this.state.Recipients,this.state.Sender, this.state.EnviromentId, this.state.ApplicationId).then(x => {
        this.props.onRequestHide()
        this.props.onTaskCreate();
      })
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
           <label htmlFor="Recipients">Recipients</label>
           <input type="text" className="form-control" id="Recipients" placeholder="Recipients" valueLink={this.linkState('Recipients')} />
         </div>
         <div className="form-group">
           <label htmlFor="Sender">Sender</label>
           <input type="text" className="form-control" id="Sender" placeholder="Sender" valueLink={this.linkState('Sender')} />
         </div>
         <div className="form-group">
           <label htmlFor="Text">Text</label>
           <textarea type="text" className="form-control" id="Text" placeholder="Text" valueLink={this.linkState('Text')} />
         </div>
       </form>
       <button className="btn btn-primary pull-right" onClick={this.save}>Save</button>
       </div>
);
   }
});


module.exports=EditPage;
