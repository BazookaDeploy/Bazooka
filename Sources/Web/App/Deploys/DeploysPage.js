var React = require("react");
var Actions = require("./ActionsCreator");
var EnviromentActions = require("../Enviroments/ActionsCreator");
var Store = require("../Enviroments/Store");
var Modal = require("react-bootstrap/Modal");
var ModalTrigger = require("react-bootstrap/ModalTrigger");


var DeployDialog = React.createClass({
  create:function(){
    var version = this.refs.Version.getDOMNode().value;
    if(version!=null){
      Actions.startDeploy(this.props.Enviroment.Id, version);
      this.props.onRequestHide();
    }
  },

  render:function(){
    return(
      <Modal {...this.props} title="Start deploy">
      <div className="modal-body">
        <form role="form">
          <div className="form-group">
            <label htmlFor="Version">Version</label>
            <input type="text" className="form-control" id="Version" ref="Version" placeholder="Version"  />
          </div>
        </form>
      </div>
      <div className="modal-footer">
        <button className="btn btn-primary" onClick={this.create}>Deploy</button>
        <button className="btn" onClick={this.props.onRequestHide}>Close</button>
      </div>
      </Modal>);
    }
});

  var DeploysPage = React.createClass({
    getInitialState: function() {
      return {
        deploys : Store.getAll()
      };
    },

    componentDidMount: function() {
      Store.addChangeListener(this._onChange);
      EnviromentActions.updateAllEnviroments();
    },

    componentWillUnmount: function() {
      Store.removeChangeListener(this._onChange);
    },

    render: function () {

      return(<div>
        <h2>Available deploys</h2>

        <ul>
          {this.state.deploys.map(x => (
            <li>{x.Name} - {x.Configuration}
            <ModalTrigger modal={<DeployDialog Enviroment={x}/>}>
              <button className='btn'>Deploy</button>
            </ModalTrigger></li>
            ))}
        </ul>
        </div>)
      },

      _onChange: function(){
        this.setState({
          deploys : Store.getAll()
        })
      }
    });

    module.exports = DeploysPage;
