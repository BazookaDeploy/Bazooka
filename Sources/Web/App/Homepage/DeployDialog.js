import React from "react";
import Actions from "./Actions";
import Button from "../Shared/Button";
import DateTimePicker from "../Shared/DateTimePicker";
import Modal from "../Shared/Modal";
import Select from "../Shared/Select";

var DeployDialog = React.createClass({
  getInitialState: function () {
    return {
      loading: true,
      scheduled: false,
      versions: [],
      version:null,
      tasks: [],
      date: new Date()
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

  create: function () {
    var version = this.state.version;
    if (version != null) {
      if (!this.state.scheduled) {
        Actions.startDeploy(this.props.Enviroment.Id, this.props.ApplicationId, version);
        this.props.onClose();
      } else {
        Actions.scheduleDeploy(this.props.Enviroment.Id, this.props.ApplicationId, version,this.state.date);
        this.props.onClose();
      }
    }
  },

  setScheduled: function () {
    this.setState({
      scheduled: !this.state.scheduled
    });
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
                  <Select title="Choose the version to deploy:" onChange={(e) => this.setState({version: e.target.value})}>
                    <option></option>
                    {versions}
                  </Select>
              }

              <label htmlFor="Schedule" className="input__title">Do you want to schedule the deploy: <input type="checkbox" checked={this.state.scheduled} onChange={this.setScheduled}/></label><br /><br />
            {
              this.state.scheduled && <DateTimePicker value={this.state.date} onChange={(e) => this.setState({date:e})} />
            }
        </Modal.Body>
        <Modal.Footer>
          <Button onClick={this.props.onClose}>Close</Button>
          <Button primary onClick={this.create}>Deploy</Button>
        </Modal.Footer>
      </Modal>);
  }
});


export default DeployDialog;