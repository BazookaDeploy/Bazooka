var React = require("react");
var Actions = require("../Actions/ApplicationActionsCreator");
var Store = require("../Stores/ApplicationStore");

var AppLine = React.createClass({
  render: function(){
    return(<div>Applicazione</div>)
  }
});

var AppPage = React.createClass({
  getInitialState: function() {
    return {
      apps : Store.getAll()
    };
  },
  componentDidMount: function() {
    Store.addChangeListener(this._onChange);
  },
  componentWillUnmount: function() {
    Store.removeChangeListener(this._onChange);
  },
  load:function(){
    Actions.updateApplications([{test:2}]);
  },
  render: function () {
    var apps = this.state.apps.map(function(a){return(<AppLine></AppLine>)});

    return (<div>Welcome to the App page
      <button className='btn' onClick={this.load}>Carica</button>

      <h3>Lista</h3>
      {apps}
    </div>);
  },
  _onChange: function(){
    this.setState({
      apps : Store.getAll()
    });
  }
});

module.exports = AppPage;
