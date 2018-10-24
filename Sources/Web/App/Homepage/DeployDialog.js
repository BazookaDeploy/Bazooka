import React from "react";
import Actions from "./Actions";
import Button from "../Shared/Button";
import DateTimePicker from "../Shared/DateTimePicker";
import Modal from "../Shared/Modal";
import Select from "../Shared/Select";

var DeployDialog = React.createClass({
  getInitialState: function () {
    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    return {
      loading: true,
      scheduled: false,
      chooseTasks:false,
      versions: [],
      version:null,
      tasks: [],
      chosenTasks: [],
      date: tomorrow
    };
  },

  componentWillReceiveProps: function(nextProps) {
    if((this.props.show==false || this.props.show==undefined) && nextProps.show == true){

      Actions.getVersions(this.props.Enviroment.Id, this.props.ApplicationId).then(x => {
        this.setState({
          loading: false,
          versions: x
        });
      });

      Actions.getTasks(this.props.Enviroment.Id, this.props.ApplicationId).then(x => {
        this.setState({
          tasks: x
        });
      });
    }
  },

  rollback: function () {
      var res = window.confirm("Are you really sure you want to rollback the last deploy?")

      if (res) {
          Actions.rollback(this.props.Enviroment.Id, this.props.ApplicationId);
          this.props.onClose();
      }
  },

  create: function () {
    var version = this.refs.version.value();
    if (version != null) {
      if (!this.state.scheduled) {
          Actions.startDeploy(this.props.Enviroment.Id, this.props.ApplicationId, version, this.state.chosenTasks);
        this.props.onClose();
      } else {
          Actions.scheduleDeploy(this.props.Enviroment.Id, this.props.ApplicationId, version, this.state.date, this.state.chosenTasks);
        this.props.onClose();
      }
    }
  },

  setScheduled: function () {
    this.setState({
      scheduled: !this.state.scheduled
    });
  },

  addTask: function (e, task) {
      if (e.target.checked) {
          this.setState({ chosenTasks: this.state.chosenTasks.concat(task)})
      } else {
          this.setState({ chosenTasks: this.state.chosenTasks.filter(z => z.DeployTaskId != task.DeployTaskId) })
      }
  },

  render: function () {
    var title = "Start deploy " + this.props.Enviroment.Name + " - " + this.props.Enviroment.Configuration;

    var versions = this.state.versions.map(x => (<option>{x}</option>));

    return (
      <Modal {...this.props}>
        <Modal.Header>
            {title}
        </Modal.Header>
        <Modal.Body>
              {
                this.state.loading ?
                  <span><br />Loading available versions ... <br /> <br /></span> :
                  <Select title="Choose the version to deploy:" ref="version" onChange={(e) => this.setState({version: e.target.value})}>
                    {versions}
                  </Select>
              }

              <label htmlFor="Schedule" className="input__title">Do you want to schedule the deploy? <input type="checkbox" checked={this.state.scheduled} onChange={this.setScheduled}/></label><br /><br />
            {
              this.state.scheduled && <DateTimePicker value={this.state.date} onChange={(e) => this.setState({date:e})} />
            }

            <label htmlFor="singleTasks" className="input__title">Do you want execute only specific tasks? <input type="checkbox" checked={this.state.chooseTasks} onChange={() => this.setState({ chooseTasks: !this.state.chooseTasks })} /></label><br />
            {this.state.chooseTasks && <ul className="deploy__taskList">
                    {this.state.tasks.map(x => <li><input type="checkbox" onClick={(e) => this.addTask(e,x)} /> {x.Name}</li>)}
                </ul>
            }

            </Modal.Body>
        <Modal.Footer>

          <Button onClick={this.rollback}>Rollback last deploy</Button>
          <Button onClick={this.props.onClose}>Close</Button>
          <Button primary onClick={this.create}>Deploy</Button>
        </Modal.Footer>
      </Modal>);
  }
});


export default DeployDialog;
