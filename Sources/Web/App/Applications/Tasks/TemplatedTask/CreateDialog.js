import React from "react";
import Actions from "./Actions";
import Modal from "../../../Shared/Modal";
import Button from "../../../Shared/Button";
import Input from "../../../Shared/Input";
import Table from "../../../Shared/Table";
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



  changeTask: function (id) {
      this.setState({
          TaskId: id

      })
      Actions.lastVersion(id).then(x => {
          this.setState({
              RequiredParameters: x.Parameters,
              Parameters: x.Parameters.map(z => { return { TaskTemplateParameterId : z.Id, Name: z.Name, Optional : z.Optional } }),
              TaskVersionId: x.TaskTemplateVersionId
          })
      })
  },

  setParameter: function (value, id) {
      this.state.Parameters.filter(x => x.TaskTemplateParameterId == id)[0].Value = value;
      this.setState({ Parameters: this.state.Parameters})
  },

  render:function(){
    return(
     <Modal {...this.props}>
     <Modal.Header>Add new Templated task</Modal.Header>
     <Modal.Body>
                <Input title="Name" placeholder="Name" autoFocus onChange={(e) => this.setState({ Name: e.target.value })} />

           <Select title="Machine" onChange={(e)=> this.setState({Agent: e.target.value})}>
              <option />
             {this.state.Agents.map(x => (<option value={x.Id}>{x.Name}- {x.Address}</option>))}
           </Select>

           <Select title="Task" onChange={(e) => this.changeTask( e.target.value)}>
               <option />
               {this.state.Tasks.map(x => (<option value={x.Id}>{x.Name}</option>))}
           </Select>

           <h3>Parameters </h3>

                <Table>
                    <Table.Head>
                        <tr>
                            <th>Parameter</th>
                            <th>Value</th>
                            <th>Required</th>
                        </tr>
                    </Table.Head>
                    <Table.Body>
                        {this.state.Parameters.map(x => <tr>
                            <td>{x.Name}</td>
                            <td><Input placeholder="Value" onChange={(e) => this.setParameter(e.target.value, x.TaskTemplateParameterId)} /></td>
                            <td>{!x.Optional && <span>Required</span>}</td>
                        </tr>)}
                    </Table.Body>
                </Table>


     </Modal.Body>
     <Modal.Footer>
       <Button onClick={this.props.onClose}>Close</Button>
       <Button primary onClick={this.create}>Create</Button>
     </Modal.Footer>
     </Modal>);
  }
});

module.exports = TemplatedTaskCreateDialog;
