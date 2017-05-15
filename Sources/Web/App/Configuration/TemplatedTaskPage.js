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
        return {parameters:[]};
    },

    componentDidMount() {
        this.update();
    },

    update(){
    },


    
    render(){
        return (<div>
            <h3>Templated Task</h3>
            <Grid fluid>
                <Grid.Row>
                    Edit task
                </Grid.Row>
            </Grid>
            </div>
        )
    }
});



export default TemplatedTaskPage;