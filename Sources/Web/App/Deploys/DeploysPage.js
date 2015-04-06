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
    var title = "Start deploy " + this.props.Enviroment.Name + " - " + this.props.Enviroment.Configuration;

    return(
      <Modal {...this.props} title={title}>
      <div className="modal-body">
        <form role="form">
          <div className="form-group">
            <label htmlFor="Version">Version</label>
            <input type="text" className="form-control" id="Version" ref="Version" placeholder="Version"  />
          </div>
        </form>
      </div>
      <div className="modal-footer">
        <button className="btn" onClick={this.props.onRequestHide}>Close</button>
        <button className="btn btn-primary" onClick={this.create}>Deploy</button>
      </div>
      </Modal>);
    }
});

  var DeploysPage = React.createClass({
    getInitialState: function() {
      return {
        deploys : Store.getGrouped()
      };
    },

    componentDidMount: function() {
      Store.addChangeListener(this._onChange);
      EnviromentActions.updateGroupedEnviroments();
    },

    componentWillUnmount: function() {
      Store.removeChangeListener(this._onChange);
    },

    render: function () {

      return(<div>
        <h2>Available deploys</h2>

          {this.state.deploys.map(x => (
            <div>
              <h4>{x.Application}</h4>
              <ul>
                {x.Enviroments.map(z => (
                  <li>{z.Configuration} -
                    <ModalTrigger modal={<DeployDialog Enviroment={z}/>}>
                      <button className='btn btn-primary btn-xs'>Deploy</button>
                    </ModalTrigger>
                  </li>
                ))}
              </ul>
          </div>
            ))}
        </div>)
      },

      _onChange: function(){
        debugger;
        this.setState({
          deploys : Store.getGrouped()
        })
      }
    });

    module.exports = DeploysPage;
