import React from "react"
import Header from "../Shared/Header";
import Select from "../Shared/Select";
import Grid from "../Shared/Grid";
import Tabs from "../Shared/Tabs";
import Actions from "./Actions";


var ApplicationStatistics = React.createClass({
    render(){
        return <h2>Application stats </h2>;
    }
});


var UserStatistics = React.createClass({
    render(){
        return <h2>User stats </h2>;
    }
});

var StatisticsPage = React.createClass({
    getInitialState: function () {
        return {
            deploys: [],
            users: [],
            currentFilter: "Today"
        };
    },

    componentDidMount: function () {
        this.update();
    },

    update: function () {
        Actions.getStatistics(this.state.currentFilter).then(x => {
            this.setState({
                deploys: x.Deploys,
                users: x.Users
            });
        });
    },


    render: function () {
        return (<div>
            <Header actions={<Select onChange={(e) => this.setState({ currentFilter: e.target.value }, this.update) }>
                <Select.Option>Today</Select.Option>
                <Select.Option>Yesterday</Select.Option>
                <Select.Option>Last week</Select.Option>
                <Select.Option>Last month</Select.Option>
                <Select.Option>Ever</Select.Option>
            </Select>}>

                Statistics
            </Header>

            <Grid fluid>
                <Grid.Row>
                    <Grid.Col md="12">
                        <Tabs>
                            <Tabs.Tab title="Applications">
                                <ApplicationStatistics apps={this.state.deploys} />
                            </Tabs.Tab>

                            <Tabs.Tab title="Users">
                                <UserStatistics users={this.state.users} />
                            </Tabs.Tab>
                        </Tabs>
                    </Grid.Col>
                </Grid.Row>

            </Grid>


        </div>);
    }
});

export default StatisticsPage;