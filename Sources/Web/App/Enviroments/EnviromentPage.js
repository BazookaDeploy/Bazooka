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

var EnviromentsPage = React.createClass({
  mixins: [Router.State],
  getInitialState: function() {
    return {
      envs : []
    };
  },

  componentDidMount: function() {
    this.update();
  },

  update:function(){
    Actions.updateAllEnviroments().then(x =>{
      this.setState({
        envs:x
      })
    });
  },

  render: function () {
    var envs = this.state.envs.map(a => (<Enviroments Enviroment={a} application={this.getParams().applicationName} applicationId={this.getParams().applicationId}/>));

    return(<div>
      <h3>Application {this.getParams().applicationName}</h3>
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
