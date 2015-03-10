var React = require("react");
var Router = require('react-router');
var Actions = require("./ActionsCreator");
var Store = require("./Store");
var { Route, DefaultRoute, RouteHandler, Link } = Router;

  var DeploymentPage = React.createClass({
    mixins: [Router.State],
    getInitialState: function() {
      return {
        deployments : Store.getAll()
      };
    },

    componentDidMount: function() {
      Store.addChangeListener(this._onChange);
      var id = this.getParams().Id;
      Actions.updateDeployment(id);
    },

    componentWillUnmount: function() {
      Store.removeChangeListener(this._onChange);
    },

    reload:function(){
      var id = this.getParams().Id;
      Actions.updateDeployment(id);
    },

    getStatus:function(status){
      if(status==0){
        return "Queued";
      }else if(status==1){
        return "Running";
      }else if(status==2){
        return "Ended";
      }else{
        return "Failed";
      }
    },

    render: function () {

      return(<div>
        <h2>{this.state.deployments.Name} - {this.state.deployments.Configuration}</h2>
        <button className='btn btn-default' onClick={this.reload}>Reload</button>
        <ul>
          <h4>Current status: {this.getStatus(this.state.deployments.Status)}</h4>
          <h5>Deployed Version : {this.state.deployments.Version}</h5>
          <span>Deployment started at : {this.state.deployments.StartDate} and ended at {this.state.deployments.EndDate}</span>
          <h4>Logs:</h4>
          <span dangerouslySetInnerHTML={{
            __html: (this.state.deployments.Log||"").replace(/(?:\r\n|\r|\n)/g, '<br />') 
          }} ></span>
        </ul>
        </div>)
      },

      _onChange: function(){
        this.setState({
          deployments : Store.getAll()
        })
      }
    });

    module.exports = DeploymentPage;
