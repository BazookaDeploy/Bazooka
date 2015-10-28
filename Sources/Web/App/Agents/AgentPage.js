import React from "react";
import Actions from "./ActionsCreator";
import Dropzone from 'react-dropzone';
import Router from 'react-router';
import LinkedState from "react/lib/LinkedStateMixin";

var AgentsPage = React.createClass({
  mixins: [Router.State,LinkedState],

  getInitialState: function(){
      return {};
  },

  componentDidMount: function() {
    Actions.getAgent(this.getParams().id).then(x => this.setState({
        Id:x.Id,
        OriginalAddress: x.Addres,
        OriginalName:x.Name,
        Address:x.Address,
        Name:x.Name,
        EnviromentId:x.EnviromentId
    }))
  },

  onDrop:function(files){
    Actions.uploadFiles(files,this.state.OriginalName);
  },

  testConnection:function(){
    Actions.testAgent(this.state.Address)
      .then(x => alert("Agent responding"))
      .fail(x => alert("Agent not responding"))
  },

  rename: function(){
    Actions.rename(this.state.Id,this.state.EnviromentId,this.state.Name);
  },

  changeAddress: function(){
    Actions.changeAddress(this.state.Id,this.state.EnviromentId,this.state.Address);
  },

  render: function () {
    return (<div>
      <h3>Agent {this.state.OriginalName}</h3>
<br />

        <form >
          <fieldset>
            <legend>Description</legend>
          <div className="form-group">
            <label >Agent name</label>


                {window.Administator ?
                  <div className="input-group">
              <input type="text" ref="name" className="form-control"  valueLink={this.linkState('Name')} />
                <span className="input-group-btn">
                  <button className="btn btn-primary" type="button" onClick={this.rename}>Rename</button>
                </span> </div>: <div className="input-group"><span>{this.state.Name}</span></div>}

            <p className="help-block">A descriptive unique name for this agent</p>
          </div>
        </fieldset>
        </form>

        <br />

          <form >
            <fieldset>
              <legend>Agent URL</legend>
            <div className="form-group">
              <label >Address</label>


                  {window.Administator ?
                <div className="input-group"><input type="text" ref="name" className="form-control"  valueLink={this.linkState('Address')}/>
                  <span className="input-group-btn">
                    <button className="btn btn-primary" type="button"  onClick={this.changeAddress}>Change</button>
                  </span> <span className="input-group-btn">
                            <button className="btn btn-primary " onClick={this.testConnection} style={{margin: "0 5px"}}>Test connection</button>
                  </span>
              </div>: <div className="input-group"><span>{this.state.Address}</span><span className="input-group-btn">
                        <button className="btn btn-primary " onClick={this.testConnection} style={{margin: "0 5px"}}>Test connection</button>
              </span>
          </div>}

              <p className="help-block">The URL that can be used to reach the agent</p>
            </div>
          </fieldset>
          </form>

  {window.Administator &&
          <form >
            <fieldset>
              <legend>Agent Update</legend>
            <div className="form-group">
              <label >Upload an update to the agent</label>

              <div className="input-group">
                <Dropzone style={{display:"inline", height:"20px", borderStyle:"none"}}
                          ref="drop"
                          onDrop={this.onDrop}>
                          <button className="btn btn-primary">Update</button>
                          </Dropzone>
              </div>
            </div>
          </fieldset>
          </form>
}



    </div>);
  }
});

module.exports = AgentsPage;
