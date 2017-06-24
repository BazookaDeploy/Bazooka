import React from "react";
import Actions from "./Actions";
import Modal from "../../../Shared/Modal";
import Button from "../../../Shared/Button";
import Input from "../../../Shared/Input";
import Grid from "../../../Shared/Grid";
import Select from "../../../Shared/Select";
import Textarea from "../../../Shared/Textarea";
import Notification from "../../../Shared/Notifications";

var TemplatedTaskCreateDialog = React.createClass({
  getInitialState:function(){
    return {
      Name:"",
      Agent:null,
      Tasks: [],
      TaskId: null,
      TaskVersionId: null,
      Parameters: [],
      RequiredParameters: [],
      Agents: [],
      key: "",
      value:""
    };
  },


  componentDidMount:function(){
    Actions.getAgents(this.props.EnviromentId).then(x => {
      this.setState({Agents:x})
      })

    Actions.loadTemplatedTasks().then(x => {
        this.setState({ Tasks: x })
    })
  },

  create:function(){
      if (this.state.Name != "" && this.state.Agent != "" && this.state.Task != "") {
          Actions.createTemplatedTask(this.state.Name, this.props.EnviromentId, this.props.ApplicationId, this.state.Agent, this.state.TaskId, this.state.TaskVersionId ,this.state.Parameters).then(x => {
              Notification.Notify(x);
              if(x.Success){
                  this.props.onCreate();
                  this.props.onClose();
              }
      })
    }
  },

  addParameter: function () {
      if (this.state.key.length != 0 &&
          this.state.value.length != 0 &&
          !this.state.Parameters.some(x => x.Key == this.state.key)) {

          this.setState({
              Parameters: this.state.Parameters.concat({
                  Name: this.state.key,
                  Value: this.state.value
              }),
              key: "",
              value: "",
              Encrypted: false
          })
      }
  },

  remove: function (key) {
      var index = -1;
      var i;
      for (i = 0; i < this.state.Parameters.length; i++) {
          if (this.state.Parameters[i].Name == key) {
              index = i;
              break;
          }
      }
      this.state.Parameters.splice(index, 1);
      this.setState({
          Parameters: this.state.Parameters
      });
  },


  changeTask: function (id) {
      this.setState({
          TaskId: id

      })
      Actions.lastVersion(id).then(x => this.setState({ RequiredParameters: x.Parameters, TaskVersionId: x.TaskTemplateVersionId}))
  },

  render:function(){
    return(
     <Modal {...this.props}>
     <Modal.Header>Add new Templated task</Modal.Header>
     <Modal.Body>
                <Input title="Name" placeholder="Name" autoFocus onChange={(e) => this.setState({ Name: e.target.value })} />

           <Select title="Machine" onChange={(e)=> this.setState({Machine: e.target.value})}>
              <option />
             {this.state.Agents.map(x => (<option value={x.Id}>{x.Name}- {x.Address}</option>))}
           </Select>

           <Select title="Task" onChange={(e) => this.changeTask( e.target.value)}>
               <option />
               {this.state.Tasks.map(x => (<option value={x.Id}>{x.Name}</option>))}
           </Select>

           <h3>Parameters </h3>
           <b>Required:</b> {this.state.RequiredParameters.filter(x => !x.Optional).map(x => <span>{x.Name},</span>)}
           <br /><b>Optional</b>: {this.state.RequiredParameters.filter(x => x.Optional).map(x => <span>{x.Name},</span>)}
           <ul>
                    {this.state.Parameters.map(x => <li>{x.Name}: {x.Value}</li>)}
           </ul>

           <Grid fluid>
               <Grid.Row>

                   <Grid.Col md={3}>
                       <Input title="Key" placeholder="Key" value={this.state.key} onChange={(e) => this.setState({ key: e.target.value })} />
                   </Grid.Col>
                   <Grid.Col md={3}>
                       <Input title="Value" placeholder="Value" value={this.state.value} onChange={(e) => this.setState({ value: e.target.value })} />
                   </Grid.Col>
                   <Grid.Col md={4}>
                       <br />
                       <Button primary onClick={this.addParameter} >Add Parameter</Button>
                   </Grid.Col>
               </Grid.Row>
           </Grid>


     </Modal.Body>
     <Modal.Footer>
       <Button onClick={this.props.onClose}>Close</Button>
       <Button primary onClick={this.create}>Create</Button>
     </Modal.Footer>
     </Modal>);
  }
});

module.exports = TemplatedTaskCreateDialog;
