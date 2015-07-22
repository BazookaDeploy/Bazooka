import React from "react";
import Actions from "./ActionsCreator";
import Dropzone from 'react-dropzone';

var AgentLine = React.createClass({
  onDrop:function(files){
    Actions.uploadFiles(files,this.props.name);
  },

  testConnection:function(){
    Actions.testAgent(this.props.name)
      .then(x => alert("Agent responding"))
      .fail(x => alert("Agent not responding"))
  },

  render: function(){
    return(
      <tr>
        <td>
          {this.props.name}
          <Dropzone style={{display:"inline", height:"20px", borderStyle:"none"}}
                    ref="drop"
                    onDrop={this.onDrop}>
                    <button className="btn btn-primary btn-xs pull-right">Update</button>
          </Dropzone>
          <button className="btn btn-primary btn-xs pull-right" onClick={this.testConnection} style={{margin: "0 5px"}}>Test</button>
        </td>
      </tr>)
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
