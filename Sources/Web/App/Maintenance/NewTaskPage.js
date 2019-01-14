import React from "react";
import Header from "../Shared/Header";
import Grid from "../Shared/Grid";
import Table from "../Shared/Table";
import Actions from "./Actions";
import Input from "../Shared/Input";
import Button from "../Shared/Button";
import Panel from "../Shared/Panel/Panel";
import FormattedDate from "../Shared/Utils/FormattedDate";
import FormattedTime from "../Shared/Utils/FormattedTime";
import { connect } from 'react-redux';
import Select from "../Shared/Select";
import { withRouter } from "react-router";
import Notification from "../Shared/Notifications";

var NewTaskPage = React.createClass({
    getInitialState: function () {
        return {
            enviromentId: null,
            agentId: null,
            agents:[],
            tasktemplateId: null,
            taskTemplates:[],
            parameters: []
        };
    },

    componentDidMount() {
        Actions.loadTemplatedTasks().then(x => this.setState({ taskTemplates: x }))
    },

    load(id) {
        Actions.loadTemplatedTask(id).then(x => this.setState({ parameters: x.Parameters }))
    },

    setParameter: function (value, id) {
        this.state.parameters.filter(x => x.Id == id)[0].Value = value;
        this.setState({ parameters: this.state.parameters })
    },

    startTask: function () {
        if (this.state.enviromentId != null && this.state.agentId != null && this.state.taskTemplateId != null) {
            var params = {};
            this.state.parameters.map(x => params[x.Name] = x.Value);
            Actions.run(this.state.agentId, this.state.taskTemplateId, params).then(x => {
                if (x.Success) {

                    this.props.router.push("/Maintenance/");
                } else {
                    Notification.Notify(x);
                }
            })
        }
    },


    render: function () {
        return <div>
            <Header>
                New Maintenance Task
            </Header>

            <Grid fluid>
                <Grid.Row>
                    <Grid.Col md={2}>
                        <br />
                        <h4>Enviroment/Agent</h4>

                        <Select title="Enviroment" onChange={(e) => this.setState({ enviromentId: e.target.value })}>
                            {this.props.enviroments.map(x => <option value={x.Id}>{x.Name}</option>)}
                        </Select>
                        <br />

                        {this.state.enviromentId != null && <Select title="Agent" onChange={(e) => this.setState({ agentId: e.target.value })}>
                            <option value={null}></option>
                            {this.props.enviroments.filter(x => x.Id == this.state.enviromentId)[0].Agents.map(x => <option value={x.Id}>{x.Name}</option>)}
                        </Select>}
                    </Grid.Col>

                    <Grid.Col md={2}>
                        <br />
                        <h4>Task</h4>
                        <Select title="Task to execute" onChange={(e) => { this.setState({ taskTemplateId: e.target.value }); this.load(e.target.value); }}>
                            <option value={null}></option>
                            {this.state.taskTemplates.map(x => <option value={x.Id}>{x.Name}</option>)}
                        </Select>
                    </Grid.Col>

                    <Grid.Col md={8}>
                        <br />
                        <h4>Parameters</h4>

                        <Table>
                            <Table.Head>
                                <tr>
                                    <th>Parameter</th>
                                    <th>Value</th>
                                    <th>Required</th>
                                </tr>
                            </Table.Head>
                            <Table.Body>
                                {this.state.parameters.map(x => <tr>
                                    <td>{x.Name} {x.Description && <span title={x.Description}>&#9432;</span>}</td>
                                    <td><Input type="text" placeholder="Value" value={x.Value} onChange={(e) => this.setParameter(e.target.value, x.Id)} /></td>
                                    <td>{!x.Optional && <span>Required</span>}</td>
                                </tr>)}
                            </Table.Body>
                        </Table>
                        {this.state.taskTemplateId != null && <Button primary block onClick={() => this.startTask()}>Start</Button>}
                    </Grid.Col>
                </Grid.Row>
            </Grid>
        </div>
    }
});


var mapStoreToProps = function (store) {
    return {
        enviroments: store.enviroments || []
    }
};

NewTaskPage = withRouter(NewTaskPage);
NewTaskPage = connect(mapStoreToProps, null)(NewTaskPage);


export default NewTaskPage;