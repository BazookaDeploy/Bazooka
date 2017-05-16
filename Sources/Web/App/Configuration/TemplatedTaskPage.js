import React from "react";
import Header from "../Shared/Header";
import Actions from "./Actions";
import Button from "../Shared/Button";
import Grid from "../Shared/Grid";
import Select from "../Shared/Select";
import Modal from "../Shared/Modal";
import Input from "../Shared/Input";
import Card from "../Shared/Card";
import { connect } from 'react-redux';
import Notification from "../Shared/Notifications";
import Textarea from "../Shared/Textarea";
import { withRouter } from "react-router";


var TemplatedTaskPage= React.createClass({
    getInitialState(){
        return {Parameters:[]};
    },

    componentDidMount() {
        this.update();
    },

    update() {
        Actions.lastVersion(this.props.params.id).then(x => this.setState(x));
    },

    rename: function () {
        if (this.state.Name.length > 0) {
            Actions.rename(this.props.params.id,this.state.Name).then(() => this.update())
        }
    },

    changeDescription: function () {
        if (this.state.Description.length > 0) {
            Actions.changeDescription(this.props.params.id, this.state.Description).then(() => this.update())
        }
    },

    render(){
        return (<div>
            <h3>Templated Task {this.state.Name}</h3>
            <Grid fluid>
                <Grid.Row>
                        <Input title="Name" value={this.state.Name} onChange={(e) => this.setState({ Name: e.target.value })} />
                        <Button block onClick={this.rename}>Rename</Button>
                    <Textarea title="Description" rows={5} value={this.state.Description} onChange={(e) => this.setState({ Description: e.target.value })} />
                    <Button onClick={this.changeDescription} block >Change description</Button>
                    <Textarea title="Script" rows={20} value={this.state.Script} onChange={(e) => this.setState({ Script: e.target.value })} />

                </Grid.Row> 
            </Grid>
            </div>
        )
    }
});



export default TemplatedTaskPage;