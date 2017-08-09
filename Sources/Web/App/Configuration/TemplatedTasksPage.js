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

var TemplatedTaskCreationDialog = React.createClass({
    getInitialState(){
        return { name: "", description: ""}
    },

    close(){
        this.setState({ name: "", description: ""});
        this.props.onClose();
    },

    create(){
        if(this.state.name==""){
            return;
        }

        Actions.createTemplatedtask(this.state.name, this.state.description).then((x) => {
            Notification.Notify(x);
            if(x.Success){
                this.props.onClose();
                this.props.onCreate();
            }
        });
    },

    render(){
        return (<Modal onClose={this.onClose} {...this.props}>
                    <Modal.Header>Create new Templated task</Modal.Header>
                    <Modal.Body>
                        <Input title="Name" value={this.state.name} onChange={(e) => this.setState({name: e.target.value})} />
                        <Textarea rows={6} title="Description" value={this.state.description} onChange={(e) => this.setState({ description: e.target.value })} />

                    </Modal.Body>
                    <Modal.Footer>
                        <Button onClick={this.close}>Cancel</Button>
                        <Button primary onClick={this.create}>Create</Button>
                    </Modal.Footer>
                </Modal>)
    }
});


var TemplatedTasksPage= React.createClass({
    getInitialState(){
        return {show:false, tasks:[]};
    },

    componentDidMount() {
        this.update();
    },

    update(){
        Actions.loadTemplatedTasks().then(x => this.setState({ tasks: x }));
    },

    open(id) {
        this.props.router.push("/Configuration/TemplatedTasks/" + id);
    },

    deleteTask: function (evt, id) {
        evt.stopPropagation();
        var confirm = window.confirm("Are you sure you want to delete this task template ?");

        if (confirm) {
            Actions.deleteTaskTemplate(id).then(x => {
                Notification.Notify(x);
                this.update();
            });
        }
    },

    render(){
        return (<div>

            <h3>TemplatedTasks <Button onClick={() => this.setState({ show: true })} >Create new template</Button></h3>
            <TemplatedTaskCreationDialog show={this.state.show} onClose={() => this.setState({show:false})} onCreate={this.update} />

            <Grid fluid>
                <Grid.Row>
                    {this.state.tasks.map(x => (<Grid.Col className="taskTemplate" md={3} > <div onClick={() => this.open(x.Id)}><Card title={x.Name}>{x.Description} <br /> <br /><Button onClick={(evt)=>this.deleteTask(evt,x.Id)}>Delete</Button></Card></div></Grid.Col>))}
                </Grid.Row>
            </Grid>
            </div>
        )
    }
});

TemplatedTasksPage = withRouter(TemplatedTasksPage);

export default TemplatedTasksPage;