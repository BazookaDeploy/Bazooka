var React = require("react");
var Router = require('react-router');
var Actions = require("./ActionsCreator");
var Store = require("./Store");
var { Route, DefaultRoute, RouteHandler, Link } = Router;

  var DeploymentRow = React.createClass({
    mixins:[Router.Navigation],

    navigate:function(){
      this.transitionTo("deployment",{Id:this.props.Deployment.Id});
    },

    render:function(){
      if(this.props.Deployment.Status==0){
        return(
          <tr onClick={this.navigate} style={{cursor:"pointer"}}>
            <td><i className="glyphicon glyphicon-list-alt"></i> Queued</td>
            <td>{this.props.Deployment.Name} - {this.props.Deployment.Configuration}</td>
            <td>{this.props.Deployment.UserName}</td>
          </tr>);
      } else if(this.props.Deployment.Status==1){
        return(
          <tr onClick={this.navigate} style={{cursor:"pointer"}}>
            <td><i className="glyphicon glyphicon glyphicon-play" style="color:#15CAFF"></i> Running</td>
            <td>{this.props.Deployment.Name} - {this.props.Deployment.Configuration}</td>
            <td>{this.props.Deployment.UserName}</td>
          </tr>);
      } else if(this.props.Deployment.Status==2){
        return(
          <tr onClick={this.navigate} style={{cursor:"pointer"}}>
            <td> <i className="glyphicon glyphicon-ok-circle" style="color:#17EF43"></i> Succeeded</td>
            <td>{this.props.Deployment.Name} - {this.props.Deployment.Configuration}</td>
            <td>{this.props.Deployment.UserName}</td>
          </tr>);
      } else{
        return(
          <tr onClick={this.navigate} style={{cursor:"pointer"}}>
            <td><i className="glyphicon glyphicon-remove-circle" style="color:#FF3B3B"></i> Failed</td>
            <td>{this.props.Deployment.Name} - {this.props.Deployment.Configuration}</td>
            <td>{this.props.Deployment.UserName}</td>
          </tr>);
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
