import React from "react";
import Router from 'react-router';
import Actions from "./ActionsCreator";
import Modal from "react-bootstrap/lib/Modal";
import ModalTrigger from "react-bootstrap/lib/ModalTrigger";
var { Route, DefaultRoute, RouteHandler, Link } = Router;


var Enviroments = React.createClass({
  render:function(){
    return (<tr>
      <td><Link to="tasks" params={{
        applicationName: this.props.application,
        applicationId: this.props.applicationId,
        enviroment: this.props.Enviroment.Name,
        enviromentId: this.props.Enviroment.Id}}>{this.props.Enviroment.Name}</Link></td>
      </tr>)
  }
});

var CreateDialog = React.createClass({
  create:function(){
    var name = this.refs.name.getDOMNode().value;
    if(name.length!==0){
      Actions.createApplicationGroup(name).then(x => {
        this.props.onRequestHide();
        this.props.onCreate();
      });
    }
    return false;
  },

  render:function(){
    return(
      <Modal {...this.props} title="Create new application group">
        <div className="modal-body">
          <form role="form" onSubmit={this.create}>
            <div className="form-group">
              <label htmlFor="name">Name</label>
              <input type="text" className="form-control" id="name" placeholder="Name" autoFocus ref="name" />
            </div>
          </form>
        </div>
        <div className="modal-footer">
          <button className="btn btn-primary" onClick={this.create}>Create</button>
          <button className="btn" onClick={this.props.onRequestHide}>Close</button>
        </div>
      </Modal>);
    }
  })



var EnviromentsPage = React.createClass({
  mixins: [Router.State],
  getInitialState: function() {
    return {
      envs : [],     
      groups:[],
      users : [],
      admins : [],
      applicationGroup: null
    };
  },

  componentDidMount: function() {
    this.update();
    this.updateGroups();
    this.updateAdmins();
    this.updateUsers();
  },

  updateUsers(){
    Actions.getUsers().then(x => this.setState({users:x}));
  },

  updateAdmins(){
    Actions.getAdmins(this.getParams().applicationId).then(x => this.setState({admins:x}));
  },  

  updateGroups: function(){
    Actions.getApplicationGroups().then(x => this.setState({groups:x}));
    Actions.getApplicationInfo(this.getParams().applicationId).then(x => this.setState({applicationGroup:x.GroupName}));

  },

  update:function(){
    Actions.updateAllEnviroments().then(x =>{
      this.setState({
        envs:x
      })
    });
  },

  setGroup:function(){
    Actions.setApplicationGroup(this.getParams().applicationId, this.refs.group.getDOMNode().value).then(x =>
      this.updateGroups()
    )
  },

  addAdmin(){
    Actions.addAdmin(this.refs.admin.getDOMNode().value,this.getParams().applicationId).then(x =>this.updateAdmins());
  },

  removeAdmin(userId){
    Actions.removeAdmin(userId,this.getParams().applicationId).then(x =>this.updateAdmins());
  },

  render: function () {
    var envs = this.state.envs.map(a => (<Enviroments Enviroment={a} application={this.getParams().applicationName} applicationId={this.getParams().applicationId}/>));

    return(<div>
      <h3>Application {this.getParams().applicationName}</h3>

      <br />
      Application group : {this.state.applicationGroup} &nbsp;&nbsp;&nbsp;&nbsp;

      {window.Administator == "True" && <ModalTrigger modal={<CreateDialog onCreate={this.updateGroups}/>}>
        <button className='btn btn-primary btn-xs '>Create new</button>
      </ModalTrigger>}

      <br />
      <br />
      <div className="input-group">
      <select ref="group" className="form-control">
        <option value=""  />
        {this.state.groups.map(x => <option value={x.Id}>{x.Name}</option>)}
      </select>
      <span className="input-group-btn">
      <button className="btn btn-primary" onClick={this.setGroup}>Set group</button>
      </span>
    </div>


             <h3>Administrators</h3>

                <table className="table table-bordered table-hovered">
                  <thead><tr><th>Administrators</th></tr></thead>
                  <tbody>
                    {this.state.admins.map(x => <tr><td>{x.UserName} <button className="btn btn-danger btn-xs pull-right" onClick={z => {this.removeAdmin(x.UserId)}}>Remove</button></td></tr>)}
                    <tr>
                      <td>
                        <div className="input-group">
                          <select ref="admin" className="form-control">
                            {this.state.users.map(x => <option value={x.Id}>{x.UserName}</option>)}
                          </select>
                          <span className="input-group-btn">
                            <button  className="btn btn-default" onClick={this.addAdmin}>Add</button>
                          </span>
                        </div>
                      </td></tr>
                  </tbody>
                </table> 

      <br />

      <table className="table table-striped table-bordered">
      <thead><tr><th>Enviroments</th></tr></thead>
      <tbody>
        {envs}
      </tbody>
      </table>
      </div>)
  }
});

module.exports = EnviromentsPage;
