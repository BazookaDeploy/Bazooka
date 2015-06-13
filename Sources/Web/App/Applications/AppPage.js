var React = require("react");
var Actions = require("./ActionsCreator");
var Store = require("./Store");
var Modal = require("react-bootstrap/Modal");
var ModalTrigger = require("react-bootstrap/ModalTrigger");
var Router = require('react-router');
var { Route, DefaultRoute, RouteHandler, Link } = Router;

var CreateDialog = React.createClass({
  create:function(){
    var name = this.refs.name.getDOMNode().value;

    if(name.length!==0){
      Actions.createApplication(name).then(x => {
        this.props.onRequestHide();
      });
    }
    
    return false;
  },

  render:function(){
    return(
      <Modal {...this.props} title="Create new application">
      <div className="modal-body">
        <form role="form" onSubmit={this.create}>
          <div className="form-group">
            <label htmlFor="name">Application name</label>
            <input type="text" className="form-control" id="name" placeholder="Name" autoFocus ref="name" />
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

var AppLine = React.createClass({
  render: function(){
    return(<tr><td><Link to="enviroments" params={{
       applicationName : this.props.Application.Name,
       applicationId: this.props.Application.Id}}><b>{this.props.Application.Name}</b></Link></td></tr>)
  }
});

var AppPage = React.createClass({
  getInitialState: function() {
    return {
      apps : Store.getAll()
    };
  },

  componentDidMount: function() {
    Store.addChangeListener(this._onChange);
    Actions.updateApplications();
  },

  componentWillUnmount: function() {
    Store.removeChangeListener(this._onChange);
  },

  render: function () {
    var apps = this.state.apps.map(function(a){return(<AppLine Application={a}></AppLine>)});

    return (<div>
      <h3>Existing applications       </h3>
      <table className="table table-hovered table-bordered table-striped">
        <thead><tr><th>Application <ModalTrigger modal={<CreateDialog />}>
                <button className='btn btn-primary btn-xs pull-right'>Create new Application</button>
              </ModalTrigger></th></tr></thead>
        <tbody>
          {apps}
        </tbody>
      </table>
    </div>);
  },

  _onChange: function(){
    this.setState({
      apps : Store.getAll()
    });
  }
});

module.exports = AppPage;
