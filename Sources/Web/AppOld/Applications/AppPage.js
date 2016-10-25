import React from "react";
import Actions from "./ActionsCreator";
import Modal from "react-bootstrap/lib/Modal";
import ModalTrigger from "react-bootstrap/lib/ModalTrigger";
import Router from 'react-router';
var { Route, DefaultRoute, RouteHandler, Link } = Router;

var CreateDialog = React.createClass({
  getInitialState: function () {
    return { clone: false }
  },

  create: function () {
    var name = this.refs.name.getDOMNode().value;

    if (name.length !== 0) {
      if (!this.state.clone) {
        Actions.createApplication(name).then(x => {
          this.props.onCreate();
          this.props.onRequestHide();
        });
      } else {
        Actions.cloneApplication(name,this.refs.app.getDOMNode().value).then(x => {
          this.props.onCreate();
          this.props.onRequestHide();
        });
      }
    }
      return false;
    },

  selectClone: function () {
    this.setState({ clone: !this.state.clone });
  },

  render: function () {
    return (
      <Modal {...this.props} title="Create new application">
        <div className="modal-body">
          <form role="form" onSubmit={this.create}>
            <div className="form-group">
              <label htmlFor="name">Application name</label>
              <input type="text" className="form-control" id="name" placeholder="Name" autoFocus ref="name" />
            </div>
            <div className="form-group">
              <label htmlFor="clone">Clone Application</label>
              <br />
              <input type="checkbox" ref="clone" onClick={this.selectClone} value={this.state.clone}/>
            </div>
            {this.state.clone &&
             <div className="form-group">
              <label htmlFor="app">Select application</label>
              <select className="form-control" ref="app">
                {this.props.apps.map(x => <option value={x.Id}>{x.Name}</option>)}
              </select>
            </div>             

            }
          </form>
        </div>
        <div className="modal-footer">
          <button className="btn" onClick={this.props.onRequestHide}>Close</button>
          <button className="btn btn-primary" onClick={this.create}>Create</button>
        </div>
      </Modal>);
  }
})

var AppLine = React.createClass({
  render: function () {
    return (<tr><td><Link to="enviroments" params={{
      applicationName: this.props.Application.Name,
      applicationId: this.props.Application.Id
    }}><b>{this.props.Application.Name}</b></Link></td></tr>)
  }
});

var AppPage = React.createClass({
  getInitialState: function () {
    return {
      apps: []
    };
  },

  update: function () {
    Actions.updateApplications().then(x => this.setState({ apps: x }));
  },

  componentDidMount: function () {
    this.update();
  },

  render: function () {
    var apps = this.state.apps.map(function (a) { return (<AppLine Application={a}></AppLine>) });

    return (<div>
      <h3>Existing applications       </h3>
      <table className="table table-hovered table-bordered table-striped">
        <thead><tr><th>Application <ModalTrigger modal={<CreateDialog apps={this.state.apps} onCreate={this.update}/>}>
          <button className='btn btn-primary btn-xs pull-right'>Create new Application</button>
        </ModalTrigger></th></tr></thead>
        <tbody>
          {apps}
        </tbody>
      </table>
    </div>);
  }
});

module.exports = AppPage;
