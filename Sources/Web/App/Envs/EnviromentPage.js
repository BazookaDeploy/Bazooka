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
          <button className="btn btn-primary" onClick={this.create}>Create</button>
          <button className="btn" onClick={this.props.onRequestHide}>Close</button>
        </div>
      </Modal>);
    }
  })


var Enviroments = React.createClass({
  render:function(){
    return (<div className="panel panel-default">
  <div className="panel-heading">{this.props.Enviroment.Name}</div>
  <div className="panel-body">
    Panel content
  </div>
</div>)
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
    var envs = this.state.envs.map(a => (<Enviroments Enviroment={a}/>));

    return(<div>
      <h3>Enviroments <ModalTrigger modal={<CreateDialog onCreate={this.update}/>}>
        <button className='btn btn-primary btn-xs pull-right'>Create</button>
      </ModalTrigger></h3>

        {envs}
      </div>)
  }
});

module.exports = EnviromentsPage;
