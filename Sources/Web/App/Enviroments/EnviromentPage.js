var React = require("react");
var Router = require('react-router');
var Actions = require("./ActionsCreator");
var Store = require("./Store");
var Modal = require("react-bootstrap/Modal");
var ModalTrigger = require("react-bootstrap/ModalTrigger");
var { Route, DefaultRoute, RouteHandler, Link } = Router;

var CreateDialog = React.createClass({
  create:function(){
    var configuration = this.refs.configuration.getDOMNode().value;
    var description = this.refs.description.getDOMNode().value;
    var id = this.props.Application;

    if(configuration.length!==0){
      Actions.createEnviroment(id, configuration,description).then(x => this.props.onRequestHide());
    }
      
    return false;
  },

  render:function(){
    return(
      <Modal {...this.props} title="Create new enviroment">
        <div className="modal-body">
          <form role="form" onSubmit={this.create}>
            <div className="form-group">
              <label htmlFor="configuration">Configuration</label>
              <input type="text" className="form-control" id="configuration" placeholder="Configuration" autoFocus ref="configuration" />
            </div>
            <div className="form-group">
              <label htmlFor="description">Description (Optional)</label>
              <textarea className="form-control" id="description" placeholder="Description of your enviroment ( optional)" ref="description" />
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
    return (<tr>
      <td><Link to="deployunits" params={{enviromentId: this.props.Enviroment.Id}}>{this.props.Enviroment.Configuration}</Link></td>
      </tr>)
  }
});

var EnviromentsPage = React.createClass({
  mixins: [Router.State],
  getInitialState: function() {
    return {
      envs : Store.getAll()
    };
  },

  componentDidMount: function() {
    Store.addChangeListener(this._onChange);
    var id = this.getParams().applicationId;
    Actions.updateEnviroments(id);
  },

  componentWillUnmount: function() {
    Store.removeChangeListener(this._onChange);
  },

  render: function () {
    var envs = this.state.envs.map(a => (<Enviroments Enviroment={a}/>));

    return(<div>
      <table className="table table-striped table-bordered">
      <thead><tr><th>Enviroments <ModalTrigger modal={<CreateDialog Application={this.getParams().applicationId} />}>
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

module.exports = EnviromentsPage;
