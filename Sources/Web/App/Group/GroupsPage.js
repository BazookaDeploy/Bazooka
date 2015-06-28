import React from  "react";
import Router from  'react-router';
import Actions from  "./ActionsCreator";
import Store from  "./Store";
import Modal from  "react-bootstrap/lib/Modal";
import ModalTrigger from  "react-bootstrap/lib/ModalTrigger";
var { Route, DefaultRoute, RouteHandler, Link } = Router;

var User = React.createClass({
    removeUser:function(){
        Actions.removeUser(this.props.Group,this.props.User.UserId).then(x => {
            Actions.updateUsers(this.props.Group);
        })
    },

    render:function(){
        return (<tr><td>{this.props.User.UserName}<button className="btn btn-danger btn-xs pull-right" onClick={this.removeUser}>Remove</button></td></tr>);
    }
})

var GroupsPage = React.createClass({
  mixins: [Router.State],
  getInitialState: function() {
    return {
      envs : Store.getAll(),
      loading:true,
      users:[]
    };
  },

  componentDidMount: function() {
    Store.addChangeListener(this._onChange);
    var id = this.getParams().name;
    Actions.updateUsers(id);
    Actions.getUsers().then(x => {
      this.setState({
        loading:false,
        users:x
      })
    })
  },

  addUser:function(){
    Actions.addUser(this.getParams().name,this.refs.user.getDOMNode().value).then(x => {
      Actions.updateUsers(this.getParams().name);
    })
  },


  componentWillUnmount: function() {
    Store.removeChangeListener(this._onChange);
  },

render: function () {
    var that=this;
    var name = this.getParams().name;
    var envs = this.state.envs.map(a => (<User User={a} Group={name} />));
    var users = this.state.users.map(a => (<option value={a.Id}>{a.UserName}</option>));

    return(<div>
      {this.state.loading? "Loading users ..." : ""}

      <br />

      <table className="table table-striped table-bordered">
      <thead><tr><th>Users </th></tr></thead>
      <tbody>
        {envs}

        <tr>
        <td>
          <div className="input-group">
            <select ref="user" className="form-control">
              {users}
            </select>
            <span className="input-group-btn">
              <button  className="btn btn-default" onClick={this.addUser}>Add</button>
            </span>
          </div>
        </td>
        </tr>
      </tbody>
      </table>
      </div>)
  },

  _onChange: function(){
    this.setState({
      envs : Store.getAll()
    })
  }
});

module.exports = GroupsPage;
