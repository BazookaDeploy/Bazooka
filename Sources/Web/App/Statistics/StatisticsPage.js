import React from "react"
import Header from "../Shared/Header";
import Select from "../Shared/Select";
import Grid from "../Shared/Grid";
import Table from "../Shared/Table";
import Actions from "./Actions";

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
        </div>);
    }
});

export default StatisticsPage;