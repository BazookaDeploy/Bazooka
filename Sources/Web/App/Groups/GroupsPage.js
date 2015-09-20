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
      Actions.createGroup(name).then(x => {
        this.props.onRequestHide();
        this.props.onCreate();
      });
    }
    return false;
  },

  render:function(){
    return(
      <Modal {...this.props} title="Create new group">
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


var GroupsPage = React.createClass({
  mixins: [Router.State],
  getInitialState: function() {
    return {
      envs : []
    };
  },

  update:function(){
        Actions.updateGroups().then(x =>{
          this.setState({
            envs:x
          })
        })
  },

  componentDidMount: function() {
    this.update();
  },


  render: function () {
    var envs = this.state.envs.map(a => (<tr><td><Link to="group" params={{name: a.Name}}>{a.Name}</Link></td></tr>));

    return(<div>
      <table className="table table-striped table-bordered">
      <thead><tr><th>Groups {window.Administator == "True" && <ModalTrigger modal={<CreateDialog onCreate={this.update}/>}>
        <button className='btn btn-primary btn-xs pull-right'>Create</button>
      </ModalTrigger>} </th></tr></thead>
      <tbody>
        {envs}
      </tbody>
      </table>
      </div>)
  }
});

module.exports = GroupsPage;
