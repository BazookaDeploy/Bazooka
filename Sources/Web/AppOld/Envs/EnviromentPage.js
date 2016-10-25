import React from "react";
import Router from 'react-router';
import Actions from "./ActionsCreator";
import Modal from "react-bootstrap/lib/Modal";
import ModalTrigger from "react-bootstrap/lib/ModalTrigger";
var { Route, DefaultRoute, RouteHandler, Link } = Router;

var CreateDialog = React.createClass({
  create:function(){
    var name = this.refs.name.getDOMNode().value;

    if(name.length!==0){
      Actions.createEnviroment(name).then(x => {this.props.onRequestHide();this.props.onCreate()});
    }

    return false;
  },

  render:function(){
    return(
      <Modal {...this.props} title="Create new enviroment">
        <div className="modal-body">
          <form role="form" onSubmit={this.create}>
            <div className="form-group">
              <label htmlFor="name">Name</label>
              <input type="text" className="form-control" id="name" placeholder="Name of your enviroment" ref="name" />
            </div>
          </form>
        </div>
        <div className="modal-footer">
          <button className="btn" onClick={this.props.onRequestHide}>Close</button>
          <button className="btn btn-primary" onClick={this.create}>Create</button>
        </div>
      </Modal>);
    }
  })

  var AddAgentDialog = React.createClass({
    create:function(){
      var name = this.refs.name.getDOMNode().value;
      var address = this.refs.address.getDOMNode().value;

      if(name.length!==0 && address.length!==0 ){
        Actions.createAgent(this.props.EnviromentId,name,address).then(x => {this.props.onRequestHide();this.props.onCreate()});
      }

      return false;
    },

    render:function(){
      return(
        <Modal {...this.props} title="Add an Agent">
          <div className="modal-body">
            <form role="form" onSubmit={this.create}>
              <div className="form-group">
                <label htmlFor="name">Name</label>
                <input type="text" className="form-control" id="name" placeholder="Name of the agent" ref="name" />
              </div>
              <div className="form-group">
                <label htmlFor="address">Address</label>
                <input type="text" className="form-control" id="address" placeholder="Address of the agent" ref="address" />
              </div>
            </form>
          </div>
          <div className="modal-footer">
            <button className="btn" onClick={this.props.onRequestHide}>Close</button>
            <button className="btn btn-primary" onClick={this.create}>Create</button>
          </div>
        </Modal>);
      }
    })

var Enviroments = React.createClass({
  render:function(){
    var agents = this.props.Enviroment.Agents.map(x => (
      <Link to="agent" params={{id:x.Id}} className="AgentLogo">
        <i className="glyphicon glyphicon-hdd"></i>
          {x.Name}
      </Link>))

    return (
      <div className="panel panel-default">
        <div className="panel-heading">{this.props.Enviroment.Name}
            {window.Administator == "True" && <ModalTrigger  modal={<AddAgentDialog EnviromentId={this.props.Enviroment.Id} onCreate={this.props.onUpdate}/>}>
            <button className='btn btn-primary btn-xs pull-right'>Add an agent</button>
          </ModalTrigger>}
        </div>
        <div className="panel-body">
          {this.props.Enviroment.Agents.length==0 && <span>No agents for this enviroment</span>}
          {this.props.Enviroment.Agents.length!=0 && agents}
        </div>
      </div>);
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
    var envs = this.state.envs.map(a => (<Enviroments Enviroment={a} onUpdate={this.update}/>));

    return(<div>
      <h3>Enviroments   {window.Administator == "True" && <ModalTrigger modal={<CreateDialog onCreate={this.update}/>}>
        <button className='btn btn-primary btn-xs'>Create new</button>
      </ModalTrigger>}</h3>

        {envs}
      </div>)
  }
});

module.exports = EnviromentsPage;
