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
       <ModalTrigger modal={<DeployUnitDialog onCreate={this.props.onCreate} Enviroment={this.props.EnviromentId} />} >
         <button type="button" className="btn btn-default btn-lg btn-block">Deploy task</button>
       </ModalTrigger>
       <ModalTrigger modal={<MailTaskDialog onCreate={this.props.onCreate} EnviromentId={this.props.EnviromentId} />} >
         <button type="button" className="btn btn-default btn-lg btn-block">Mail task</button>
       </ModalTrigger>
       <ModalTrigger modal={<LocalScriptTaskDialog onCreate={this.props.onCreate} EnviromentId={this.props.EnviromentId} />} >
         <button type="button" className="btn btn-default btn-lg btn-block">Local script task</button>
       </ModalTrigger>
       <ModalTrigger modal={<RemoteScriptTaskDialog onCreate={this.props.onCreate} EnviromentId={this.props.EnviromentId} />} >
         <button type="button" className="btn btn-default btn-lg btn-block">Remote script task</button>
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
      Actions.getTasks(id).then(x => {
        this.setState({tasks:x})
      });
    },

    componentDidMount: function() {
      var id = this.getParams().enviromentId;
      Actions.getTasks(id).then(x => {
        this.setState({tasks:x})
      });

      Actions.getUsers(id).then(x => {
        this.setState({users:x})
      })

      Actions.getAllUsers(id).then(x => {
        this.setState({allUsers:x})
      })

      Actions.getGroups(id).then(x => {
        this.setState({groups:x})
      })

      Actions.getAllGroups(id).then(x => {
        this.setState({allGroups:x})
      })
    },

    removeUser:function(id){
      Actions.removeUser(id).then(x => {
        Actions.getUsers(this.getParams().enviromentId).then(z => {
          this.setState({users:z})
        })
      })
    },

    removeGroup:function(id){
      Actions.removeGroups(id).then(x => {
        Actions.getGroups(this.getParams().enviromentId).then(z => {
          this.setState({groups:z})
        })
      })
    },

    addUser:function(){
      Actions.addUser(this.getParams().enviromentId,this.refs.user.getDOMNode().value).then(x => {
        Actions.getUsers(this.getParams().enviromentId).then(z => {
          this.setState({users:z})
        })
      })
    },

    addGroup:function(){
      Actions.addGroup(this.getParams().enviromentId,this.refs.group.getDOMNode().value).then(x => {
        Actions.getGroups(this.getParams().enviromentId).then(z => {
          this.setState({groups:z})
        })
      })
    },

    render: function () {
      var users = this.state.users.map(x => {
        return (<tr><td>{x.UserName}  <button className="btn btn-danger btn-xs pull-right" onClick={z => this.removeUser(x.Id)}>Remove</button></td></tr>);
      })

      var allUsers = this.state.allUsers.map(x => {
        return (<option value={x.Id}>{x.UserName}</option>);
      })

      var groups = this.state.groups.map(x => {
        return (<tr><td>{x.Name}  <button className="btn btn-danger btn-xs pull-right" onClick={z => {this.removeGroup(x.Id)}}>Remove</button></td></tr>);
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
                  <ModalTrigger modal={<TaskSelectDialog onCreate={this.updateTasks} EnviromentId={this.getParams().enviromentId}/>}>
                    <button className='btn btn-xs btn-primary pull-right'>New</button></ModalTrigger></th></tr></thead>
                <tbody>
                  {this.state.tasks.map(x => x.Type == 0 ? (
                    <tr><td><Link to="deployunitedit" params={{
                        applicationName:this.getParams().applicationName,
                        enviroment:this.getParams().enviroment,
                        deployUnitName : x.Name,
                        deployUnitId: x.Id
                      }}>{x.Name}</Link></td></tr>
                  ) :
                  (x.Type == 1 ? (
                    <tr><td><Link to="mailtaskedit" params={{
                        applicationName:this.getParams().applicationName,
                        enviroment:this.getParams().enviroment,
                        mailTaskName : x.Name,
                        taskId: x.Id
                      }}>{x.Name}</Link></td></tr>
                  ) : (x.Type == 2 ?
                    (
                      <tr><td><Link to="localscripttaskedit" params={{
                          applicationName:this.getParams().applicationName,
                          enviroment:this.getParams().enviroment,
                          mailTaskName : x.Name,
                          taskId: x.Id
                        }}>{x.Name}</Link></td></tr>
                    )
                    :
                    (
                      <tr><td><Link to="remotescripttaskedit" params={{
                          applicationName:this.getParams().applicationName,
                          enviroment:this.getParams().enviroment,
                          mailTaskName : x.Name,
                          taskId: x.Id
                        }}>{x.Name}</Link></td></tr>
                    )
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
            <TabPane eventKey={3} tab='Hooks'>

                       <h4>Web hook</h4>

                       <p>Deploys can be automated by invoking a specific web hook for each enviroment of your application
                       <ModalTrigger modal={<HookDialog Enviroment={this.getParams().enviromentId} />}>
                            <button className='btn btn-xs btn-primary pull-right'>Get Url</button>
                          </ModalTrigger>
                        </p>
            </TabPane>
          </TabbedArea>





        </div>)
      }
    });

    module.exports = TasksPage;
