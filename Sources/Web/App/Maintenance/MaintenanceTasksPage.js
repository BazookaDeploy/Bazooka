import React from "react";
import Header from "../Shared/Header";
import Select from "../Shared/Select";
import Grid from "../Shared/Grid";
import Table from "../Shared/Table";
import Button from "../Shared/Button";
import Actions from "./Actions";
import ListIcon from "../Shared/Icon/ListIcon";
import PlayIcon from "../Shared/Icon/PlayIcon";
import CircleOkIcon from "../Shared/Icon/CircleOkIcon";
import CircleRemoveIcon from "../Shared/Icon/CircleRemoveIcon";
import WatchIcon from "../Shared/Icon/WatchIcon";
import EraseIcon from "../Shared/Icon/EraseIcon";
import {withRouter} from "react-router";
import FormattedDate from "../Shared/Utils/FormattedDate";
import FormattedTime from "../Shared/Utils/FormattedTime";
import prettyDate from "../Shared/Utils/PrettyDate";


var MaintenanceTasks = React.createClass({
    navigate: function () {
        this.props.router.push("/Maintenance/Task/" + this.props.item.Id );
    },

    getStatus() {

        if (this.props.item.Status == 0) {
            return <ListIcon />
        } else if (this.props.item.Status == 1) {
                return <PlayIcon style={{ fill: "#15CAFF" }} />
        } else if (this.props.item.Status == 2) {
            return <CircleOkIcon style={{ fill: "#17EF43" }} />
        } else if (this.props.item.Status == 3) {
                return <CircleRemoveIcon style={{ fill: "#FF3B3B" }} />
        } else if (this.props.item.Status == 4) {
            return <WatchIcon />
        } else {
            return <EraseIcon />
        }
    },

    render() {
        var date =new Date( this.props.item.StartDate);
        return <tr onClick={this.navigate}>
            <td>{this.getStatus() }</td>
            <td>{this.props.item.TaskName}</td>
            <td>{this.props.item.Agent}</td>
            <td>{this.props.item.UserName}</td>
            <td> <span title={('00' + date.getHours()).slice(-2) +":"+('00' + date.getMinutes()).slice(-2)+":"+('00' + date.getSeconds()).slice(-2) + " - "+ ('00' + date.getDate()).slice(-2) + "/" + ('00' + (date.getMonth() + 1)).slice(-2) + "/" + date.getFullYear()}>{prettyDate(this.props.item.StartDate)}</span></td>
        </tr>
    }
});

MaintenanceTasks = withRouter(MaintenanceTasks);

var MaintenanceTasksPage = React.createClass({
    getInitialState: function () {
        return {
            deployments: [],
            skip:0,
            currentFilter: "Today"
        };
    },

    componentDidMount: function () {
        this.update();
    },

    update: function () {
        Actions.getTaskList(this.state.skip,20).then(x => {
            this.setState({
                deployments: x
            });
        });
        
    },

    loadMore: function () {
        this.setState({ skip: this.state.skip + 20 }, () => Actions.getTaskList(this.state.skip, 20).then(x => {
            this.setState({ deployments: this.state.deployments.concat(x) })
        }))
    },

    vaiANuovo: function () {
        this.props.router.push("/Maintenance/NewTask/");
    },

    render: function () {
        return <div>
            <Header actions={<Button onClick={this.vaiANuovo}>New Task</Button>}>
                Maintenance Tasks
            </Header>

            <Grid fluid>
                <Grid.Row>
                    <Grid.Col md={12}>
                        <Table hover>
                            <Table.Head>
                                <tr>
                                    <td>Status</td>
                                    <td>Task</td>
                                    <td>Agent</td>
                                    <td>Started by</td>
                                    <td>Start time</td>
                                </tr>
                            </Table.Head>
                            <Table.Body>
                                {this.state.deployments.map(x => <MaintenanceTasks item={x} />) }
                            </Table.Body>
                        </Table>
                        <Button block primary onClick={() => this.loadMore()}>Load more</Button>
                    </Grid.Col>
                </Grid.Row>
            </Grid>
        </div>
    }
});

MaintenanceTasksPage = withRouter(MaintenanceTasksPage);
export default MaintenanceTasksPage;
