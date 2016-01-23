import React from "react";
import LinkedState from "react/lib/LinkedStateMixin";
import Router from 'react-router';
import Actions from "./ActionsCreator";
import Modal from "react-bootstrap/lib/Modal";
import ModalTrigger from  "react-bootstrap/lib/ModalTrigger";
import TabbedArea from "react-bootstrap/lib/TabbedArea";
import TabPane from "react-bootstrap/lib/TabPane";
import DeployUnitDialog from "./DeployTasks/DeployUnitsDialog";
import MailTaskDialog from "./MailTask/CreateDialog";
import LocalScriptTaskDialog from "./LocalScriptTask/CreateDialog";
import RemoteScriptTaskDialog from "./RemoteScriptTask/CreateDialog";
import DatabaseTaskDialog from "./DatabaseTasks/CreateDialog";
var { Route, DefaultRoute, RouteHandler, Link } = Router;

var HookDialog = React.createClass({
  getInitialState: function(){
    return {
      url: ""
    }
  },

  componentDidMount: function(){
    Actions.getDeployUrl(this.props.Enviroment).then(x => {
      this.setState({
        url:x
      })
    })
  },

  render: function(){

       return(
      <Modal {...this.props} backdrop="static" title="Get deploy url">
      <div className="modal-body">
        <p>The web hook to deploy this application in this enviroment is <br /><b> {this.state.url}</b><br /> </p>
      </div>
      <div className="modal-footer">
        <button className="btn" onClick={this.props.onRequestHide}>Close</button>
      </div>
      </Modal>);
  }
})

var TaskSelectDialog = React.createClass({
  render:function(){
    return(
     <Modal {...this.props} enforceFocus={false} title="Add new task">
     <div className="modal-body">
       <ModalTrigger modal={<DeployUnitDialog onCreate={this.props.onCreate} Enviroment={this.props.EnviromentId} ApplicationId={this.props.ApplicationId} />} >
         <button type="button" className="btn btn-default btn-lg btn-block">Deploy task</button>
       </ModalTrigger>
       <ModalTrigger modal={<MailTaskDialog onCreate={this.props.onCreate} EnviromentId={this.props.EnviromentId} ApplicationId={this.props.ApplicationId} />} >
         <button type="button" className="btn btn-default btn-lg btn-block">Mail task</button>
       </ModalTrigger>
       <ModalTrigger modal={<LocalScriptTaskDialog onCreate={this.props.onCreate} EnviromentId={this.props.EnviromentId} ApplicationId={this.props.ApplicationId} />} >
         <button type="button" className="btn btn-default btn-lg btn-block">Local script task</button>
       </ModalTrigger>
       <ModalTrigger modal={<RemoteScriptTaskDialog onCreate={this.props.onCreate} EnviromentId={this.props.EnviromentId} ApplicationId={this.props.ApplicationId} />} >
         <button type="button" className="btn btn-default btn-lg btn-block">Remote script task</button>
       </ModalTrigger>
       <ModalTrigger modal={<DatabaseTaskDialog onCreate={this.props.onCreate} EnviromentId={this.props.EnviromentId} ApplicationId={this.props.ApplicationId} />} >
         <button type="button" className="btn btn-default btn-lg btn-block">Database task</button>
       </ModalTrigger>
     </div>
     <div className="modal-footer">
       <button className="btn" onClick={this.props.onRequestHide}>Close</button>
     </div>
     </Modal>);
  }
});

  var TasksPage = React.createClass({
    mixins: [Router.State],
    getInitialState: function() {
      return {
        tasks : [],
        users : [],
        groups:[],
        allUsers:[],
        allGroups:[]
      };
    },

    updateTasks:function(){
      var id = this.getParams().enviromentId;
      Actions.getTasks(id,this.getParams().applicationId).then(x => {
        this.setState({tasks:x})
      });
    },

    componentDidMount: function() {
      var id = this.getParams().enviromentId;
      var appId=  this.getParams().applicationId;
      Actions.getTasks(id,appId).then(x => {
        this.setState({tasks:x})
      });

      Actions.getUsers(id,appId).then(x => {
        this.setState({users:x})
      })

      Actions.getAllUsers(id).then(x => {
        this.setState({allUsers:x})
      })

      Actions.getGroups(id,appId).then(x => {
        this.setState({groups:x})
      })

      Actions.getAllGroups(id).then(x => {
        this.setState({allGroups:x})
      })
    },

    deleteTask: function(id,type){
        Actions.deleteTask(this.getParams().applicationId, id,type).then(x => this.updateTasks());
    },

    removeUser:function(id){
      Actions.removeUser(this.getParams().enviromentId,this.getParams().applicationId,id).then(x => {
        Actions.getUsers(this.getParams().enviromentId,this.getParams().applicationId).then(z => {
          this.setState({users:z})
        })
      })
    },

    removeGroup:function(id){
      Actions.removeGroups(this.getParams().enviromentId,this.getParams().applicationId,id).then(x => {
        Actions.getGroups(this.getParams().enviromentId,this.getParams().applicationId).then(z => {
          this.setState({groups:z})
        })
      })
    },

    addUser:function(){
      Actions.addUser(this.getParams().enviromentId,this.getParams().applicationId,this.refs.user.getDOMNode().value).then(x => {
        Actions.getUsers(this.getParams().enviromentId,this.getParams().applicationId).then(z => {
          this.setState({users:z})
        })
      })
    },

    addGroup:function(){
      Actions.addGroup(this.getParams().enviromentId,this.getParams().applicationId,this.refs.group.getDOMNode().value).then(x => {
        Actions.getGroups(this.getParams().enviromentId,this.getParams().applicationId).then(z => {
          this.setState({groups:z})
        })
      })
    },

    render: function () {
      var users = this.state.users.map(x => {
        return (<tr><td>{x.UserName}  <button className="btn btn-danger btn-xs pull-right" onClick={z => this.removeUser(x.USerId)}>Remove</button></td></tr>);
      })

      var allUsers = this.state.allUsers.map(x => {
        return (<option value={x.Id}>{x.UserName}</option>);
      })

      var groups = this.state.groups.map(x => {
        return (<tr><td>{x.Name}  <button className="btn btn-danger btn-xs pull-right" onClick={z => {this.removeGroup(x.GroupId)}}>Remove</button></td></tr>);
      })

      var allGroups = this.state.allGroups.map(x => {
        return (<option value={x.Id}>{x.Name}</option>);
      })

      return(<div>
        <h3>Application {this.getParams().applicationName} <i className='glyphicon glyphicon-menu-right' /> {this.getParams().enviroment}</h3>

          <TabbedArea defaultActiveKey={1}>
  		    	<TabPane eventKey={1} tab='Tasks'>
              <br />

              <table className="table table-bordered table-striped">
                <thead><tr><th>Tasks
                  <ModalTrigger modal={<TaskSelectDialog onCreate={this.updateTasks} EnviromentId={this.getParams().enviromentId} ApplicationId={this.getParams().applicationId}/>}>
                    <button className='btn btn-xs btn-primary pull-right'>New</button></ModalTrigger></th></tr></thead>
                <tbody>
                  {this.state.tasks.map(x => x.Type == 0 ? (
                    <tr><td><Link to="deployunitedit" params={{
                        applicationName:this.getParams().applicationName,
                        applicationId: this.getParams().applicationId,
                        enviroment:this.getParams().enviroment,
                        enviromentId:this.getParams().enviromentId,
                        deployUnitName : x.Name,
                        deployUnitId: x.Id
                        }}>{x.Name}</Link><button className="btn btn-danger btn-xs pull-right" onClick={() => this.deleteTask(x.Id,0)}><i className="glyphicon glyphicon-remove"></i></button></td></tr>
                  ) :
                  (x.Type == 1 ? (
                    <tr><td><Link to="mailtaskedit" params={{
                        applicationName:this.getParams().applicationName,
                        applicationId: this.getParams().applicationId,
                        enviroment:this.getParams().enviroment,
                        enviromentId:this.getParams().enviromentId,
                        mailTaskName : x.Name,
                        taskId: x.Id
                        }}>{x.Name}</Link><button className="btn btn-danger btn-xs  pull-right" onClick={() => this.deleteTask(x.Id,1)}><i className="glyphicon glyphicon-remove"></i></button></td></tr>
                  ) : (x.Type == 2 ?
                    (
                      <tr><td><Link to="localscripttaskedit" params={{
                          applicationName:this.getParams().applicationName,
                          applicationId: this.getParams().applicationId,
                          enviroment:this.getParams().enviroment,
                          enviromentId:this.getParams().enviromentId,
                          taskName : x.Name,
                          taskId: x.Id
                          }}>{x.Name}</Link><button className="btn btn-danger btn-xs pull-right" onClick={() => this.deleteTask(x.Id,2)}><i className="glyphicon glyphicon-remove"></i></button></td></tr>
                    )
                    :
                    (x.Type==3 ?(
                      <tr><td><Link to="remotescripttaskedit" params={{
                          applicationName:this.getParams().applicationName,
                          applicationId: this.getParams().applicationId,
                          enviroment:this.getParams().enviroment,
                          enviromentId:this.getParams().enviromentId,
                          taskName : x.Name,
                          taskId: x.Id
                          }}>{x.Name}</Link><button className="btn btn-danger btn-xs pull-right" onClick={() => this.deleteTask(x.Id,3)}><i className="glyphicon glyphicon-remove"></i></button></td></tr>
                    ):(
                      <tr><td><Link to="databasetaskedit" params={{
                          applicationName:this.getParams().applicationName,
                          applicationId: this.getParams().applicationId,
                          enviroment:this.getParams().enviroment,
                          enviromentId:this.getParams().enviromentId,
                          taskName : x.Name,
                          taskId: x.Id
                          }}>{x.Name}</Link><button className="btn btn-danger btn-xs pull-right" onClick={() => this.deleteTask(x.Id,4)}><i className="glyphicon glyphicon-remove"></i></button></td></tr>
                    ))
                    ))
                  )}
                </tbody>
              </table>
            </TabPane>
            <TabPane eventKey={2} tab='Permissions'>
              <h3>Allowed Users</h3>

              <table className="table table-bordered table-hovered">
                <thead><tr><th>Users</th></tr></thead>
                <tbody>
                  {users}
                  <tr>
                  <td>
                    <div className="input-group">
                      <select ref="user" className="form-control">
                        {allUsers}
                      </select>
                      <span className="input-group-btn">
                        <button  className="btn btn-default" onClick={this.addUser}>Add</button>
                      </span>
                    </div>
                  </td>
                  </tr>
                </tbody>
              </table>

              <h3>Allowed Groups</h3>

                <table className="table table-bordered table-hovered">
                  <thead><tr><th>Groups</th></tr></thead>
                  <tbody>
                    {groups}
                    <tr>
                      <td>
                        <div className="input-group">
                          <select ref="group" className="form-control">
                            {allGroups}
                          </select>
                          <span className="input-group-btn">
                            <button  className="btn btn-default" onClick={this.addGroup}>Add</button>
                          </span>
                        </div>
                      </td></tr>
                  </tbody>
                </table>
            </TabPane>
          </TabbedArea>





        </div>)
      }
    });

    module.exports = TasksPage;
