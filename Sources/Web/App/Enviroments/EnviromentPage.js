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
  },

  render:function(){
    return(
      <Modal {...this.props} title="Create new enviroment">
        <div className="modal-body">
          <form role="form">
            <div className="form-group">
              <label htmlFor="configuration">Configuration</label>
              <input type="text" className="form-control" id="configuration" placeholder="configuration" ref="configuration" />
            </div>
            <div className="form-group">
              <label htmlFor="description">Description (Optional)</label>
              <textarea className="form-control" id="description" placeholder="description" ref="description" />
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
    return (<div>{this.props.Enviroment.Configuration}<Link to="deployunits" params={{enviromentId: this.props.Enviroment.Id}}>Go to</Link></div>)
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
    var envs = this.state.envs.map(function(a){return(<Enviroments  Enviroment={a}></Enviroments>)});

    return(<div>
      <h2>Enviroments</h2>
      <ModalTrigger modal={<CreateDialog Application={this.getParams().applicationId} />}>
        <button className='btn'>Create</button>
      </ModalTrigger>
      <br />
      {envs}
      </div>)
  },

  _onChange: function(){
    this.setState({
      envs : Store.getAll()
    })
  }
});

module.exports = EnviromentsPage;
