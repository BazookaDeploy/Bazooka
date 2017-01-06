import React from "react";
import Header from "../Shared/Header";
import Select from "../Shared/Select";
import Modal from "../Shared/Modal";
import Button from "../Shared/Button";
import Input from "../Shared/Input";
import Grid from "../Shared/Grid";
import ApplicationIcon from "../Shared/Icon/ApplicationIcon";
import Action from "./Actions";
import {withRouter} from "react-router";

var groupByGroup = function(array){
    var a = {};

    var i=0;
    for(i=0; i<array.length; i++){
        var prop = array[i].GroupName || "";

        if(a[prop]==undefined){
            a[prop]=[];
        }
        
        a[prop].push(array[i]);  
    }

    return a;
};

var CreateDialog = React.createClass({
  getInitialState: function () {
    return { clone: false, name: "", applicationId :null};
  },

  create: function () {
    var name = this.state.name;

    if (name.length !== 0) {
      if (!this.state.clone) {
        Action.createApplication(name).then(x => {
          this.props.onCreate();
          this.props.onClose();
        });
      } else {
        Action.cloneApplication(name,this.state.applicationId).then(x => {
          this.props.onCreate();
          this.props.onClose();
        });
      }
    }
      return false;
    },

  selectClone: function () {
    this.setState({ clone: !this.state.clone });
  },

  render: function () {
    return (
      <Modal {...this.props}>
        <Modal.Header>Create new Application</Modal.Header>
        <Modal.Body>
              <Input title="Name" autoFocus onChange={(e)=>this.setState({name:e.target.value})} />
            <div className="form-group">
              <label htmlFor="clone">Clone Application</label>
              <br />
              <input type="checkbox" ref="clone" onClick={this.selectClone} value={this.state.clone}/>
            </div>
            {this.state.clone &&
              <Select title="Select application" onChange={(e) => this.setState({applicationId: e.target.value})}>
                <option></option>
                {this.props.apps.map(x => <option value={x.Id}>{x.Name}</option>)}
              </Select>        

            }
        </Modal.Body>
        <Modal.Footer>
          <Button onClick={this.props.onClose}>Close</Button>
          <Button primary onClick={this.create}>Create</Button>
        </Modal.Footer>
      </Modal>);
  }
})


var ApplicationGroup = React.createClass({
    navigate: function (id) {
        this.props.router.push("/Applications/" + id );
    },

    render(){
        return (<div className="appsGroup">
            <div className="appsGroup__title">{this.props.groupName || "No Group"}</div>
            <div className="appsGroup__applications">
                {this.props.apps.map(x => <div className="appsGroup__app" onClick={() => this.navigate(x.Id)}>
                    <ApplicationIcon /> <br />{x.Name}
                    </div>)}
            </div>
        </div>);
    }
});

ApplicationGroup = withRouter(ApplicationGroup);

var ApplicationsPage = React.createClass({
    getInitialState(){
        return {showDialog:false, showOnlyMine: true, apps:[]};
    },

    componentDidMount(){
        this.load();
    },

    load(){
        debugger;
        if(this.state.showOnlyMine === true || this.state.showOnlyMine === "true"){
            Action.getApplications().then(x => this.setState({apps:x}));
        }else{
            Action.getAllApplications().then(x => this.setState({apps:x}));
        }
    },

    render:function(){
        var gr = groupByGroup(this.state.apps);


        return (<div><Header actions={<div>
            <Select onChange={(e) => this.setState({showOnlyMine: e.target.value}, this.load)}><option value={true}>Show only my application</option><option value={false}>Show all</option></Select>
                <Button onClick={() => this.setState({showDialog:true})}>Create new Application</Button>
            </div>}>
            Applications
        </Header>
        <CreateDialog show={this.state.showDialog} onClose={() => this.setState({showDialog:false})} apps={this.state.apps} onCreate={this.load}/>
            <Grid fluid>
                {Object.keys(gr).sort((a,b)=>a.localeCompare(b)).map(prop=> <ApplicationGroup groupName={prop} apps={gr[prop]} />)}
            </Grid>
        
        </div>)
        }
});

export default ApplicationsPage;