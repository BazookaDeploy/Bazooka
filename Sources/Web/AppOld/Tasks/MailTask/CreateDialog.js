import React from "react";
import Actions from "./Actions";
import Modal from "react-bootstrap/lib/Modal";
import LinkedState from "react/lib/LinkedStateMixin";

var MailTaskCreateDialog = React.createClass({
  mixins:[LinkedState],
  getInitialState:function(){
    return {
      Name:"",
      Text:"",
      Recipients:"",
      Sender:""
    };
  },

  create:function(){
    if(this.state.Name!="" && this.state.Recipients!="" && this.state.Sender!=""){
      Actions.createMailTask(this.state.Name, this.state.Text,this.state.Recipients,this.state.Sender, this.props.EnviromentId, this.props.ApplicationId).then(x => {
        this.props.onRequestHide()
        this.props.onCreate();
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
     </div>
     <div className="modal-footer">
       <button className="btn" onClick={this.props.onRequestHide}>Close</button>
       <button className="btn btn-primary" onClick={this.create}>Create</button>

     </div>
     </Modal>);
  }
});

module.exports = MailTaskCreateDialog;
