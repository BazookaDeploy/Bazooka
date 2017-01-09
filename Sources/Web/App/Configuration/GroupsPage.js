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

        Actions.createGroup(this.state.name).then((x) => {
            Notification.Notify(x);
            if(x.Success){
                this.props.onClose();
                this.props.onCreate();
            }
        });
    },

    render(){
        return (<Modal onClose={this.onClose} {...this.props}>
                    <Modal.Header>Create new Group</Modal.Header>
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
    getInitialState(){
        return {users:[], user: null}
    },

    componentDidMount(){
        this.update();
    },
    
    update(){
        Actions.getUsersInGroup(this.props.group.Name).then(x => {
            this.setState({users:x});
        })
    },

    addUser(){
        if(this.state.user!=null){
            Actions.addUser(this.props.group.Name,this.state.user).then((x) => { Notification.Notify(x); this.update();});
        }
    },

    removeUser(id){
        Actions.removeUser(this.props.group.Name,id).then((x) => { Notification.Notify(x); this.update();});
    },

    selectUser(e){
        this.setState({user:e.target.value});
    },

    render(){
        return (<Grid.Col md={4}>
            <Card title={this.props.group.Name}>
                <ul className="group">
                    {this.state.users.map(x=><li>{x.UserName} <Button onClick={() => this.removeUser(x.UserId)}>Remove</Button></li>)}
                </ul>
                <Select onChange={this.selectUser} title="Add another user">
                    <option value={null}></option>
                    {this.props.users.map(x => <option value={x.Id}>{x.UserName}</option>)}
                </Select>
                <Button block onClick={this.addUser}>Add User</Button>
            </Card>
        </Grid.Col>)
    }
});

var GroupsPage= React.createClass({
    getInitialState(){
        return {show:false};
    },

    update(){
        this.props.loadGroups();
    },
    
    render(){
        return (<div>

            <h3>Groups <Button onClick={() => this.setState({show:true})} >Create new group</Button></h3>
            <GroupCreationDialog show={this.state.show} onClose={() => this.setState({show:false})} onCreate={this.update} />

            <Grid fluid>
                <Grid.Row>
                    {this.props.groups.map(x =>(<GroupPage group={x} key={x.Name} users={this.props.users} onUpdate={this.update}/>))}
                </Grid.Row>
            </Grid>
            </div>
        )
    }
});


var mapStoreToProps = function(store){
    return {
        users: store.users || [],
        groups: store.groups || []
    };
};

var mapDispatchToProps = function(dispatch){
    return {
        loadGroups:function(){
            Actions.updateGroups().then(x => {
                dispatch({type:"ADD_GROUPS", groups: x});
            });
        }
    };
};

GroupsPage = connect(mapStoreToProps,mapDispatchToProps)(GroupsPage);


export default GroupsPage;