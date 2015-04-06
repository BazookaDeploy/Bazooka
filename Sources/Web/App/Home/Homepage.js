var React = require("react");
var Actions = require("./ActionsCreator");
var Store = require("./Store");

var Application = React.createClass({
  render: function(){
    var envs = this.props.Application.Enviroments.map(x => (
      <Enviroment Enviroment={x} />
    ));

    return (<li>
      <h4>{this.props.Application.Application}</h4>
      {envs}
    </li>);
  }
})

var Enviroment = React.createClass({
  render: function(){
      var oneVersion = this.props.Enviroment.Versions.map(x => x.CurrentlyDeployedVersion).reduce(function(a, b){return (a === b)?a:false;});
      oneVersion = oneVersion === this.props.Enviroment.Versions[0].CurrentlyDeployedVersion;

      if(oneVersion){
        var version = this.props.Enviroment.Versions[0].CurrentlyDeployedVersion || "No version deployed";
        return (<div><h5>{this.props.Enviroment.Enviroment} Version: {version}</h5></div>);
      }

      var units = this.props.Enviroment.Versions.map(x => (
        <li><b>{x.Name}</b> Version: {x.CurrentlyDeployedVersion || "No version deployed"}</li>
      ))

      return  (<div><h5>{this.props.Enviroment.Enviroment}</h5>
        <ul>
          {units}
        </ul>
      </div>)
  }
});

var HomePage = React.createClass({
    getInitialState: function() {
      return {
        envs : Store.getAll()
      };
    },

    componentDidMount: function() {
      Store.addChangeListener(this._onChange);
      Actions.updateEnviroments();
    },

    componentWillUnmount: function() {
      Store.removeChangeListener(this._onChange);
    },

    _onChange: function(){
      this.setState({
        envs : Store.getAll()
      })
    },

    render: function () {
        return (
          <div><div>Welcome to Bazooka</div>
            <h4>Current system status</h4>
            <ul>{this.state.envs.map(x => (<Application Application={x} />))}</ul>
          </div>);
    }
});


module.exports = HomePage;
