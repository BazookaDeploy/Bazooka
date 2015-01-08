var React = require("react");
var Router = require('react-router');
var Actions = require("./ActionsCreator");
var Store = require("./Store");
var Modal = require("react-bootstrap/Modal");
var ModalTrigger = require("react-bootstrap/ModalTrigger");
var { Route, DefaultRoute, RouteHandler, Link } = Router;

var CreateDialog = React.createClass({
  getInitialState: function() {
    return {
      params : []
    };
  },

  create:function(){

  },

  addParameter : function(){
    debugger;
    var key = this.refs.Key.getDOMNode().value;
    var value = this.refs.Value.getDOMNode().value;

    if(key.length!=0 && value.length != 0){
      this.setState({
        params: this.state.params.concat({
          Key: key,
          Value: value
        })
      })
    }
  },

  render:function(){
    return(
      <Modal {...this.props} title="Create new deploy unit">
      <div className="modal-body">
        <form role="form">
          <div className="form-group">
            <label htmlFor="Name">Name</label>
            <input type="text" className="form-control" id="Name" placeholder="Name" ref="Name" />
          </div>
          <div className="form-group">
            <label htmlFor="Machine">Machine</label>
            <input type="text" className="form-control" id="Machine" placeholder="Machine" ref="Machine" />
          </div>
          <div className="form-group">
            <label htmlFor="PackageName">PackageName</label>
            <input type="text" className="form-control" id="PackageName" placeholder="PackageName" ref="PackageName" />
          </div>
          <div className="form-group">
            <label htmlFor="Directory">Directory</label>
            <input type="text" className="form-control" id="Directory" placeholder="Directory" ref="Directory" />
          </div>

        </form>
        <h5>Additional Params</h5>
        <table className="table table-bordered">
        <thead><tr><th>Key</th><th>Value</th></tr></thead>
          <tbody>
            {this.state.params.map(a => (<tr><td>{a.Key}</td><td>{a.Value}</td></tr>))}
          </tbody>
        </table>
        <div className="form-group">
          <label htmlFor="Key">Key</label>
          <input type="text" className="form-control" id="Key" placeholder="Key" ref="Key" />
          <label htmlFor="Value">Value</label>
          <input type="text" className="form-control" id="Value" placeholder="Value" ref="Value" />
          <button className="btn btn-primary" onClick={this.addParameter} >Add Parameter</button>
        </div>
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
