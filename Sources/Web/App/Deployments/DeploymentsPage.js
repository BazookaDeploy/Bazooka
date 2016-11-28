import React from "react";
import Header from "../Shared/Header"
import Select from "../Shared/Select"
import Actions from "./Actions";

var DeploymentsPage = React.createClass({
    getInitialState: function() {
        return {
            deployments : [],
            currentFilter: "Today"
        };
    },

    update:function(){
        Actions.updateDeployments(this.state.currentFilter).then(x => {
            this.setState({
                deployments:x
            })
        });
    },

    componentDidMount: function() {
        this.update();
    },


    updateFilters:function(){
        this.update();
    },

    render:function(){
        return <div>
            <Header actions={<Select ref="filter" onChange={(e) => this.setState({currentFilter: e.target.value}, this.updateFilters)}>
                  <Select.Option>Today</Select.Option>
                  <Select.Option>Yesterday</Select.Option>
                  <Select.Option>Last week</Select.Option>
                  <Select.Option>Last month</Select.Option>
                </Select>}>Deployments</Header>

            </div>
        }
})


export default DeploymentsPage;