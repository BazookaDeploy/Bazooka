var React = require("react");
var Router = require('react-router');
var Actions = require("./ActionsCreator");
var Store = require("./Store");
var Modal = require("react-bootstrap/Modal");
var ModalTrigger = require("react-bootstrap/ModalTrigger");
var { Route, DefaultRoute, RouteHandler, Link } = Router;


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

  removeUser:function(){
    Actions.removeUser(this.getParams().name,this.refs.user.getDOMNode().value).then(x => {
      Actions.updateUsers(this.getParams().name);
    })
  },

  componentWillUnmount: function() {
    Store.removeChangeListener(this._onChange);
  },

  render: function () {
    var envs = this.state.envs.map(a => (<tr><td>{a.UserName}<button className="btn btn-danger btn-xs pull-right" onClick={this.removeUser.bind(a.Id)}>Remove</button></td></tr>));
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
