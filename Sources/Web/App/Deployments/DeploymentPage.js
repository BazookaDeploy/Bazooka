import React from "react";
import Header from "../Shared/Header";
import Grid from "../Shared/Grid";
import Actions from "./Actions";
import Button from "../Shared/Button";
import Panel from "../Shared/Panel/Panel";
import FormattedDate from "../Shared/Utils/FormattedDate";
import FormattedTime from "../Shared/Utils/FormattedTime";

var groupBy = function (array) {
    var a = [];
    var last = array[0].TaskName;
    var current = [];
    var i = 0;
    for (i = 0; i < array.length; i++) {
        if (array[i].TaskName != last) {
            a.push(current);
            current = [];
            last = array[i].TaskName;
        } else {
            current.push(array[i]);
        }
    }

    if (current.length == 0) {
        current.push(array[array.length - 1]);
    }

    a.push(current);

    return a;
};


function SameDate(a, b) {
    if (a == null || b == null) {
        return false;
    }

    a = new Date(a);
    b = new Date(b);

    return a.getHours() == b.getHours() && a.getMinutes() == b.getMinutes() && a.getSeconds() == b.getSeconds();
}

var LogLine = React.createClass({
    render: function () {
        return (
            <span>
                <dt className={SameDate(this.props.PrevTimeStamp, this.props.TimeStamp) ? "log__logTime log__logTime--empty" : "log__logTime"}>{SameDate(this.props.PrevTimeStamp, this.props.TimeStamp) ? null : <FormattedTime  value={this.props.TimeStamp} />}</dt>
                <dd className={SameDate(this.props.PrevTimeStamp, this.props.TimeStamp) ? "log__logData" : "log__logData log__logData--first"}>
                    <span className={this.props.Error ? "text-danger" : ""}
                        dangerouslySetInnerHTML={{ __html: (this.props.Text || "").replace(/(?:\r\n|\r|\n)/g, '<br />') }}>
                    </span>
                </dd>
            </span>)
    }
});


var Container = React.createClass({
    getInitialState: function () {
        return { open: this.props.open || this.props.Logs.some(z => z.Error) };
    },

    render: function () {

        if (this.props.Logs.length == 1) {
            return (<LogLine Error={this.props.Logs[0].Error} Text={this.props.Logs[0].Text} TimeStamp={this.props.Logs[0].TimeStamp} PrevTimeStamp={null} />);
        } else {

            return (<Panel  danger={this.props.Logs.some(z => z.Error) } success={!this.props.Logs.some(z => z.Error) } title={this.props.TaskName || "Logs"} open={this.state.open} onClick={ () => this.setState({ open: !this.state.open }) }>
                {this.props.Logs.map((x, index) => (<LogLine Error={x.Error} Text={x.Text} TimeStamp={x.TimeStamp} PrevTimeStamp={index > 0 ? this.props.Logs[index - 1].TimeStamp : null} />)) }
            </Panel>
            );
        }
    }
});


var DeploymentPage = React.createClass({
    getInitialState: function () {
        return {
            refreshing: false,
            deployments: {}
        };
    },

    componentDidMount: function () {
        this.reload();
    },

    reload: function () {
        var id = this.props.routeParams.id;
        Actions.updateDeployment(id).then(x => {
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
        } else {
            return "Canceled";
        }
    },

    cancelDeployment() {
        var res = window.confirm("Are you sure you want to cancel this deployment?");

        if (res) {
            Actions.cancelDeployment(this.props.Id).then(x => {
                Actions.updateDeployment(this.props.Id);
                this.props.onRequestHide();
            });
        }
    },

    render: function () {

        var groups = groupBy(this.state.deployments.Logs || [{ TaskName: "" }]);

        var logs = this.state.deployments.Logs == null ? (<span />) :
            groups.map((x, index) =>
                x.length == 0 ?
                    <span /> :
                    <Container TaskName={x[0].TaskName} Logs={x} open={(this.state.deployments.Status == 1 && index == groups.length - 1) || groups.length == 1 }/>
            );

        return <div>
            <Header actions={<div><Button onClick={this.reload}>{this.state.refreshing ? "Reloading ..." : "Reload"}</Button>{this.state.deployments.Status == 4 && <Button onClick={this.cancelDeployment}>Cancel deployment</Button>}</div>  }>
                Deployment
            </Header>

            <Grid fluid>
                <Grid.Row>
                    <Grid.Col md={12}>

                        <h2>{this.state.deployments.Name} - {this.state.deployments.Configuration}         </h2>

                        <h4>Current deployment status: {this.getStatus(this.state.deployments.Status) }  {this.state.deployments.Status == 4 ? <ModalTrigger modal={<CancelDialog Id={this.getParams().Id}/>}><button className='btn btn-warning btn-xs'>Cancel scheduled deploy</button></ModalTrigger> : <span />}</h4>

                        <h5>Deploying version: {this.state.deployments.Version}</h5>
                        <span>
                            {this.state.deployments.StartDate != null ?
                                (<span>Deployment {this.state.deployments.Status == 4 ? "scheduled" : "started" } on <FormattedDate value={this.state.deployments.StartDate} /> at <FormattedTime  value={this.state.deployments.StartDate} />  </span>) : (<span />) }

                            {this.state.deployments.EndDate != null ? (<span>and ended at <FormattedTime value={this.state.deployments.EndDate} /></span>) : (<span />) }


                        </span>
                        <br />
                        <br />
                        <h4>Logs: </h4>
                        <dl className="logs">
                            {logs}
                        </dl>


                    </Grid.Col>
                </Grid.Row>
            </Grid>
        </div>
    }
});


export default DeploymentPage;