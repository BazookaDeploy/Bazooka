import React from "react";
import Actions from "./Actions";
import Modal from "react-bootstrap/lib/Modal";
import LinkedState from "react/lib/LinkedStateMixin";

var LocalScriptTaskCreateDialog = React.createClass({
  mixins:[LinkedState],
  getInitialState:function(){
    return {
      Name:"",
      Script:"",
    };
  },

  create:function(){
    if(this.state.Name!="" && this.state.Script!=""){
      Actions.createLocalScriptTask(this.state.Name, this.state.Script, this.props.EnviromentId, this.props.ApplicationId).then(x => {
        this.props.onCreate();
        this.props.onRequestHide();
      })
    }
  },

  render:function(){
    return(
     <Modal {...this.props} backdrop="static" title="Add new task">
     <div className="modal-body">
       <form role="form" onSubmit={this.create}>
         <div className="form-group">
           <label htmlFor="Name">Name</label>
           <input type="text" className="form-control" id="Name" placeholder="Name" autoFocus valueLink={this.linkState('Name')} />
         </div>
         <div className="form-group">
           <label htmlFor="Script">Script</label>
           <textarea type="Script" className="form-control" id="Script" placeholder="Script" valueLink={this.linkState('Script')} />
         </div>
       </form>
     </div>
     <div className="modal-footer">
       <button className="btn" onClick={this.props.onRequestHide}>Close</button>
       <button className="btn btn-primary" onClick={this.create}>Create</button>

     </div>
     </Modal>);
  }
});

module.exports = LocalScriptTaskCreateDialog;
