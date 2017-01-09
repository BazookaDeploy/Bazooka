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

var GroupCreationDialog = React.createClass({
    getInitialState(){
        return { name: ""}
    },

    close(){
        this.setState({name:""});
        this.props.onClose();
    },

    create(){
        if(this.state.name==""){
            return;
        }

        Actions.createApplicationGroup(this.state.name).then((x) => {
            Notification.Notify(x);
            if(x.Success){
                this.props.onClose();
                this.props.onCreate();
            }
        });
    },

    render(){
        return (<Modal onClose={this.onClose} {...this.props}>
                    <Modal.Header>Create new Application Group</Modal.Header>
                    <Modal.Body>
                        <Input title="Name" value={this.state.name} onChange={(e) => this.setState({name: e.target.value})} />
                    </Modal.Body>
                    <Modal.Footer>
                        <Button onClick={this.close}>Cancel</Button>
                        <Button primary onClick={this.create}>Create</Button>
                    </Modal.Footer>
                </Modal>)
    }
});


var GroupPage = React.createClass({
    render(){
        return (<Grid.Col md={4}>
            <Card title={this.props.group.Name}>
            </Card>
        </Grid.Col>)
    }
});

var ApplicationsGroupsPage= React.createClass({
    getInitialState(){
        return {show:false, groups : []};
    },

    componentDidMount(){
        this.update();
    },

    update(){
        Actions.getApplicationGroups().then(x => this.setState({groups:x}));
    },
    
    render(){
        return (<div>

            <h3>Applications Groups <Button onClick={() => this.setState({show:true})} >Create new group</Button></h3>
            <GroupCreationDialog show={this.state.show} onClose={() => this.setState({show:false})} onCreate={this.update} />

            <Grid fluid>
                <Grid.Row>
                    {this.state.groups.map(x =>(<GroupPage group={x} users={this.props.users} onUpdate={this.update}/>))}
                </Grid.Row>
            </Grid>
            </div>
        )
    }
});



export default ApplicationsGroupsPage;