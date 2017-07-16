import React from "react";
import Header from "../Shared/Header";
import Select from "../Shared/Select";
import Grid from "../Shared/Grid";
import Table from "../Shared/Table";
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

function prettyDate(time) {
    var date = new Date(time || ""),
        diff = (((new Date()).getTime() - date.getTime()) / 1000),
        day_diff = Math.floor(diff / 86400);

    if (!isNaN(day_diff) && day_diff < 0) 
        return <span><FormattedTime value={date} /> - <FormattedDate value={date} /> </span>;

    if (isNaN(day_diff) || day_diff < 0 || day_diff >= 31) return;

    return day_diff == 0 && (
    diff < 60 && "just now" || diff < 120 && "1 minute ago" || diff < 3600 && Math.floor(diff / 60) + " minutes ago" || diff < 7200 && "1 hour ago" || diff < 86400 && Math.floor(diff / 3600) + " hours ago") || day_diff == 1 && "Yesterday" || day_diff < 7 && day_diff + " days ago" || day_diff < 31 && Math.ceil(day_diff / 7) + " weeks ago";
}

var Deployment = React.createClass({
    navigate: function () {
        this.props.router.push("/Deployments/" + this.props.item.Id );
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
        } else if (this.props.item.Status == 3) {
            return <WatchIcon />
        } else {
            return <EraseIcon />
        }
    },

    render() {
        var date =new Date( this.props.item.StartDate);
        return <tr onClick={this.navigate}>
            <td>{this.getStatus() }</td>
            <td>{this.props.item.Name} - {this.props.item.Configuration}</td>
            <td>{this.props.item.Version}</td>
            <td>{this.props.item.UserName}</td>
            <td> <span title={('00' + date.getHours()).slice(-2) +":"+('00' + date.getMinutes()).slice(-2)+":"+('00' + date.getSeconds()).slice(-2) + " - "+ ('00' + date.getDate()).slice(-2) + "/" + ('00' + (date.getMonth() + 1)).slice(-2) + "/" + date.getFullYear()}>{prettyDate(this.props.item.StartDate)}</span></td>
        </tr>
    }
});

Deployment = withRouter(Deployment);

var DeploymentsPage = React.createClass({
    getInitialState: function () {
        return {
            deployments: [],
            currentFilter: "Today"
        };
    },

    componentDidMount: function () {
        this.update();
    },

    update: function () {
        Actions.updateDeployments(this.state.currentFilter).then(x => {
            this.setState({
                deployments: x
            });
        });
    },

    render: function () {
        return <div>
            <Header actions={<Select ref="filter" onChange={(e) => this.setState({ currentFilter: e.target.value }, this.update) }>
                <Select.Option>Today</Select.Option>
                <Select.Option>Yesterday</Select.Option>
                <Select.Option>Last week</Select.Option>
                <Select.Option>Last month</Select.Option>
            </Select>}>

                Deployments
            </Header>

            <Grid fluid>
                <Grid.Row>
                    <Grid.Col md={12}>
                        <Table hover>
                            <Table.Head>
                                <tr>
                                    <td>Status</td>
                                    <td>Application / Enviroment</td>
                                    <td>Version</td>
                                    <td>Started by</td>
                                    <td>Start time</td>
                                </tr>
                            </Table.Head>
                            <Table.Body>
                                {this.state.deployments.map(x => <Deployment item={x} />) }
                            </Table.Body>
                        </Table>
                    </Grid.Col>
                </Grid.Row>
            </Grid>
        </div>
    }
});


export default DeploymentsPage;
