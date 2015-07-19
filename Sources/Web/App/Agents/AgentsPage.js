import React from "react";
import Actions from "./ActionsCreator";
import Dropzone from 'react-dropzone';

var AgentLine = React.createClass({
  onDrop:function(){
    debugger;
  },
  render: function(){
    return(<tr><td><Dropzone style={{width:"100%", height:"20px", borderStyle:"none"}} ref="drop" onDrop={this.onDrop}>{this.props.name}</Dropzone></td></tr>)
  }
});

var AgentsPage = React.createClass({
  getInitialState: function() {
    return {
      Agents :[]
    };
  },

  componentDidMount: function() {
    Actions.getAgents().then(x => this.setState({Agents:x}))
  },

  render: function () {
    var apps = this.state.Agents.map(function(a){return(<AgentLine name={a}></AgentLine>)});

    return (<div>
      <h3>Known Agents </h3>
      <table className="table table-hovered table-bordered table-striped">
        <thead><tr><th>Available agents </th></tr></thead>
        <tbody>
          {apps}
        </tbody>
      </table>
    </div>);
  }
});

module.exports = AgentsPage;
