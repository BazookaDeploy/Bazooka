var React = require("react");
var Router = require('react-router');
var Actions = require("./ActionsCreator");
var Store = require("./Store");
var Modal = require("react-bootstrap/Modal");
var ModalTrigger = require("react-bootstrap/ModalTrigger");
var { Route, DefaultRoute, RouteHandler, Link } = Router;

var CreateDialog = React.createClass({
  create:function(){
    var name = this.refs.name.getDOMNode().value;
    if(name.length!==0){
      Actions.createGroup(name).then(x => this.props.onRequestHide());
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
      envs : Store.getAll()
    };
  },

  componentDidMount: function() {
    Store.addChangeListener(this._onChange);
    Actions.updateGroups();
  },

  componentWillUnmount: function() {
    Store.removeChangeListener(this._onChange);
  },

  render: function () {
    var envs = this.state.envs.map(a => (<tr><td><Link to="group" params={{name: a.Name}}>{a.Name}</Link></td></tr>));

    return(<div>
      <table className="table table-striped table-bordered">
      <thead><tr><th>Groups <ModalTrigger modal={<CreateDialog />}>
        <button className='btn btn-primary btn-xs pull-right'>Create</button>
      </ModalTrigger></th></tr></thead>
      <tbody>
        {envs}
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
