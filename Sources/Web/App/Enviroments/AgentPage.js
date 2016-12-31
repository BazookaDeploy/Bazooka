import React from "react";
import Header from "../Shared/Header";
import Grid from "../Shared/Grid";
import Actions from "./Actions";
import Button from "../Shared/Button";

var AgentPage = React.createClass({
    getInitialState: function () {
        return {};
    },

    componentDidMount: function () {
        Actions.getAgent(this.props.params.id).then(x => this.setState({
            Id: x.Id,
            OriginalAddress: x.Address,
            OriginalName: x.Name,
            Address: x.Address,
            Name: x.Name,
            EnviromentId: x.EnviromentId
        }));
    },

    onDrop: function (files) {
        Actions.uploadFiles(files, this.state.OriginalName);
    },

    testConnection: function (event) {
        event.preventDefault()
        Actions.testAgent(this.state.Address)
            .then(x => alert("Agent responding"))
            .fail(x => alert("Agent not responding"))
    },

    rename: function () {
        Actions.rename(this.state.Id, this.state.EnviromentId, this.state.Name);
    },


    render() {
        return <div> </div>
    }
});

export default AgentPage;