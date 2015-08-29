import React from "react";
import Actions from "./Actions";
import Modal from "react-bootstrap/lib/Modal";
import LinkedState from "react/lib/LinkedStateMixin";

var DatabaseTaskCreateDialog = React.createClass({
  mixins:[LinkedState],
  getInitialState:function(){
    return {
      Name:"",
      ConnectionString:"",
      Pack:"",
      DatabaseName:"",
      "Repository":""
    };
  },

  create:function(){
    if(this.state.Name!="" && this.state.ConnectionString!=""&& this.state.Pack!=""&& this.state.DatabaseName!=""&& this.state.Repository!=""){
      Actions.createDatabaseTask(this.state.Name, this.state.ConnectionString,this.state.Pack,this.state.DatabaseName, this.props.EnviromentId,this.state.Repository).then(x => {
        this.props.onCreate();
        this.props.onRequestHide();
      })
    }
  },

  render:function(){
    return(
     <Modal {...this.props} backdrop="static" title="Add new database task">
     <div className="modal-body">
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
     </div>
     <div className="modal-footer">
       <button className="btn" onClick={this.props.onRequestHide}>Close</button>
       <button className="btn btn-primary" onClick={this.create}>Create</button>
     </div>
     </Modal>);
  }
});

module.exports = DatabaseTaskCreateDialog;
