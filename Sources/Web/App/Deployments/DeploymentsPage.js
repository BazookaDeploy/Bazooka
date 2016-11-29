import React from "react";
import Header from "../Shared/Header";
import Select from "../Shared/Select";
import Grid from "../Shared/Grid";
import Table from "../Shared/Table";
import Actions from "./Actions";

var Deployment = React.createClass({
    navigate:function(){
      this.transitionTo("deployment",{Id:this.props.Deployment.Id});
    },

    getStatus(){
        return <span>{this.props.item.Status}</span>
    },

    render(){
        return <tr onClick={this.navigate}>
            <td>{this.getStatus()}</td>
            <td>{this.props.item.Name} - {this.props.item.Configuration}</td>
            <td>{this.props.item.Version}</td>
            <td>{this.props.item.UserName}</td>
            <td>{this.props.item.StartDate}</td>
        </tr>
    }
});

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
                                {this.state.deployments.map(x => <Deployment item={x} />)}
                            </Table.Body>
                        </Table>
                    </Grid.Col>
                </Grid.Row>
            </Grid>
        </div>
    }
});


export default DeploymentsPage;