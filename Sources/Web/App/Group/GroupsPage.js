import React from  "react";
import Router from  'react-router';
import Actions from  "./ActionsCreator";
import Modal from  "react-bootstrap/lib/Modal";
import ModalTrigger from  "react-bootstrap/lib/ModalTrigger";
var { Route, DefaultRoute, RouteHandler, Link } = Router;

var User = React.createClass({
    removeUser:function(){
        Actions.removeUser(this.props.Group,this.props.User.UserId).then(x => {
            this.props.onRemove();
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
      envs : [],
      loading:true,
      users:[]
    };
  },

  componentDidMount: function() {
    this.update();
    Actions.getUsers().then(x => {
      this.setState({
        loading:false,
        users:x
      })
    })
  },

  update:function(){
    var id = this.getParams().name;
    Actions.updateUsers(id).then(x => {
      this.setState({
        envs:x
      })
    });
  },

  addUser:function(){
    Actions.addUser(this.getParams().name,this.refs.user.getDOMNode().value).then(x => {
      this.update;
    })
  },


render: function () {
    var that=this;
    var name = this.getParams().name;
    var envs = this.state.envs.map(a => (<User User={a} Group={name} onRemove={this.update}/>));
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
  }
});

module.exports = GroupsPage;
