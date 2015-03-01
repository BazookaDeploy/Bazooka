var React = require("react");
var Router = require('react-router');
var Actions = require("./ActionsCreator");
var Store = require("./Store");
var { Route, DefaultRoute, RouteHandler, Link } = Router;

  var DeploymentRow = React.createClass({
    render:function(){
      if(this.props.Deployment.Status==0){
        return(<li><Link to="deployment" params={{Id : this.props.Deployment.Id}}>{this.props.Deployment.Name} - {this.props.Deployment.Configuration} - Status: Queued </Link></li>);
      } else if(this.props.Deployment.Status==1){
        return(<li><Link to="deployment" params={{Id : this.props.Deployment.Id}}>{this.props.Deployment.Name} - {this.props.Deployment.Configuration} - Status: Running </Link></li>);
      } else if(this.props.Deployment.Status==2){
        return(<li><Link to="deployment" params={{Id : this.props.Deployment.Id}}>{this.props.Deployment.Name} - {this.props.Deployment.Configuration} - Status: Ended</Link></li>);
      } else{
        return(<li><Link to="deployment" params={{Id : this.props.Deployment.Id}}>{this.props.Deployment.Name} - {this.props.Deployment.Configuration} - Status: Failed</Link></li>);
      }
    }
  });

  var DeploymentsPage = React.createClass({

    getInitialState: function() {
      return {
        deployments : Store.getAll()
      };
    },

    componentDidMount: function() {
      Store.addChangeListener(this._onChange);
      Actions.updateDeployments();
    },

    componentWillUnmount: function() {
      Store.removeChangeListener(this._onChange);
    },



    render: function () {



      return(<div>
        <h2>Deployments</h2>
        <ul>
          {this.state.deployments.map(x => (
              <DeploymentRow Deployment={x} />
            ))}
        </ul>
        </div>)
      },

      _onChange: function(){
        this.setState({
          deployments : Store.getAll()
        })
      }
    });

    module.exports = DeploymentsPage;
