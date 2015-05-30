var React = require("react");
var Router = require('react-router');
var Actions = require("./ActionsCreator");
var Store = require("./Store");
var { Route, DefaultRoute, RouteHandler, Link } = Router;

  var DeploymentRow = React.createClass({
    render:function(){

      var status ="";
      if(this.props.Deployment.Status==0){
        status = "Queued";
      } else if(this.props.Deployment.Status==1){
        status = "Running";
      } else if(this.props.Deployment.Status==2){
        status = "Ended";
      } else{
        status = "Failed";;
      }

      return(<tr><td>{status}</td><td><Link to="deployment" params={{Id : this.props.Deployment.Id}}>{this.props.Deployment.Name} - {this.props.Deployment.Configuration}</Link></td><td>{this.props.Deployment.UserName}</td></tr>);
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
        <h2>Deployments:</h2>
        <table className="table table-border table-hover">
          <thead><tr><th>Status</th><th>Application</th><th>Started by</th></tr></thead>
          <tbody>
          {this.state.deployments.map(x => (
              <DeploymentRow Deployment={x} />
            ))}
          </tbody>
        </table>
        </div>)
      },

      _onChange: function(){
        this.setState({
          deployments : Store.getAll()
        })
      }
    });

    module.exports = DeploymentsPage;
