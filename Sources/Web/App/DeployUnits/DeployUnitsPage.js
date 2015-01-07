var React = require("react");
var Router = require('react-router');
var Actions = require("./ActionsCreator");
var Store = require("./Store");
var Modal = require("react-bootstrap/Modal");
var ModalTrigger = require("react-bootstrap/ModalTrigger");
var { Route, DefaultRoute, RouteHandler, Link } = Router;

var CreateDialog = React.createClass({
  create:function(){

  },

  render:function(){
    return(
      <Modal {...this.props} title="Create new enviroment">
      <div className="modal-body">
      <form role="form">
        <div>DeployUnitForm</div>
      </form>
      </div>
      <div className="modal-footer">
        <button className="btn btn-primary" onClick={this.create}>Create</button>
        <button className="btn" onClick={this.props.onRequestHide}>Close</button>
      </div>
      </Modal>);
    }
  });

  var DeployUnitsPage = React.createClass({
    mixins: [Router.State],
    getInitialState: function() {
      return {
        envs : Store.getAll()
      };
    },

    componentDidMount: function() {
      Store.addChangeListener(this._onChange);
      var id = this.getParams().enviromentId;
      Actions.updateDeployUnits(id);
    },

    componentWillUnmount: function() {
      Store.removeChangeListener(this._onChange);
    },

    render: function () {

      return(<div>
        <h2>Deploy Units</h2>
        <ModalTrigger modal={<CreateDialog Enviroment={this.getParams().enviromentId} />}>
          <button className='btn'>Create</button>
        </ModalTrigger>
        </div>)
      },

      _onChange: function(){
        this.setState({
          envs : Store.getAll()
        })
      }
    });

    module.exports = DeployUnitsPage;
