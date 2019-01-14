import React from "react";
import Header from "../Shared/Header";
import Grid from "../Shared/Grid";
import Actions from "./Actions";
import Button from "../Shared/Button";
import Panel from "../Shared/Panel/Panel";
import FormattedDate from "../Shared/Utils/FormattedDate";
import FormattedTime from "../Shared/Utils/FormattedTime";


var LogLine = React.createClass({
    render: function () {
        return (
            <span>

                    <span 
                        dangerouslySetInnerHTML={{ __html: (this.props.Text || "").replace(/(?:\r\n|\r|\n)/g, '<br />') }}>
                </span>
                <br />
            </span>)
    }
});



var MaintenanceTaskPage = React.createClass({
    getInitialState: function () {
        return {
            refreshing: false,
            deployments: {},
            canceling: false
        };
    },

    componentDidMount: function () {
        this.reload();
    },

    reload: function () {
        var id = this.props.routeParams.id;
        Actions.getTask(id).then(x => {
            this.setState({
                refreshing: false,
                deployments: x
            });

            if (this.state.deployments.Status == 1 && this.isMounted()) {
                setTimeout(this.reload, 10000);
            }
        });
        this.setState({ refreshing: true })
    },

    getStatus: function (status) {
        if (status == 0) {
            return "Queued";
        } else if (status == 1) {
            return "Running";
        } else if (status == 2) {
            return "Ended";
        } else if (status == 3) {
            return "Failed";
        } else if (status == 4) {
            return "Scheduled";
        } else if (status == 5) {
            return "Canceled";
        } else {
            return "unknown";
        }
    },

    render: function () {
        return <div>
            <Header actions={<div><Button onClick={this.reload}>{this.state.refreshing ? "Reloading ..." : "Reload"}</Button>{this.state.deployments.Status == 4 && <Button onClick={this.cancelDeployment}>Cancel deployment</Button>}</div>}>
                Maintenance Task
            </Header>

            <Grid fluid>
                <Grid.Row>
                    <Grid.Col md={12}>
                        <h2>{this.state.deployments.TaskName} - {this.state.deployments.Agent}         </h2>

                        <h4>Current task status: {this.getStatus(this.state.deployments.Status)}</h4>

                        <span>
                            {this.state.deployments.StartDate != null ?
                                (<span>Task {this.state.deployments.Status == 4 ? "scheduled" : "started"} on <FormattedDate value={this.state.deployments.StartDate} /> at <FormattedTime value={this.state.deployments.StartDate} />  </span>) : (<span />)}

                            {this.state.deployments.EndDate != null ? (<span>and ended at <FormattedTime value={this.state.deployments.EndDate} /></span>) : (<span />)}


                        </span>
                        <br />
                        <br />
                        <h4>Logs: </h4>
                        <dl className="logs">
                            {this.state.deployments.Logs != null && this.state.deployments.Logs.map(x => <LogLine Text={x.Text} />)}
                        </dl>


                    </Grid.Col>
                </Grid.Row>
            </Grid>
        </div>
    }
});


export default MaintenanceTaskPage;