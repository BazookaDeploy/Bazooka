import React from "react";
import Card from "../Shared/Card";
import Grid from "../Shared/Grid";
import Actions from "./Actions";
import Select from "../Shared/Select";
import Button from "../Shared/Button";
import {connect} from "react-redux";

var AllowedUsersPage = React.createClass({
    getInitialState(){
        return {users : [], groups:[], currentUser : null, currentGroup: null}
    },

    componentDidMount(){
        this.updateUsers(this.props.params.id,this.props.params.enviromentId);
        this.updateGroups(this.props.params.id,this.props.params.enviromentId);
    },

    componentWillReceiveProps(nextProps){
        if(this.props.params.id != nextProps.params.id || this.props.params.enviromentId!=nextProps.params.enviromentId){
            this.updateUsers( nextProps.params.id, nextProps.params.enviromentId);
            this.updateGroups(nextProps.params.id, nextProps.params.enviromentId);          
        }
    },

    updateUsers(id,envId){
        Actions.getUsers(envId,id).then((x)=>this.setState({users:x}))
    },

    updateGroups(id,envId){
        Actions.getGroups(envId,id).then((x)=>this.setState({groups:x}))
    },  

    addUser(){
        if(this.state.currentUser!=null){
            Actions.addUser(this.props.params.enviromentId,this.props.params.id,this.state.currentUser).then(() => this.updateUsers());
        }
    },

    removeUser(id){
            Actions.removeUser(this.props.params.enviromentId,this.props.params.id,id).then(() => this.updateUsers());
    },


    selectUser(e){
        this.setState({currentUser:e.target.value})
    },


    addGroup(){
        if(this.state.currentGroup!=null){
            Actions.addGroup(this.props.params.enviromentId,this.props.params.id,this.state.currentGroup).then(() => this.updateGroups());
        }
    },

    removeGroup(id){
            Actions.removeGroups(this.props.params.enviromentId,this.props.params.id,id).then(() => this.updateGroups());
    },


    selectGroup(e){
        this.setState({currentGroup:e.target.value})
    },

    render:function(){

        return (<div>
        <Grid fluid>
            <Grid.Row>
                <Grid.Col md={6}>
                    <Card title="Allowed Users">
                        <ul className="group">
                            {this.state.users.map(x=><li>{x.UserName} <Button onClick={() => this.removeUser(x.USerId)}>Remove</Button></li>)}
                        </ul>
                        <Select onChange={this.selectUser} title="Add another user">
                            <option value={null}></option>
                            {this.props.users.map(x => <option value={x.Id}>{x.UserName}</option>)}
                        </Select>
                        <Button block primary onClick={this.addUser}>Add User</Button>
                    </Card>
                </Grid.Col>

                <Grid.Col md={6}>
                    <Card title="Allowed groups">
                        <ul className="group">
                            {this.state.groups.map(x=><li>{x.Name} <Button onClick={() => this.removeGroup(x.GroupId)}>Remove</Button></li>)}
                        </ul>
                        <Select onChange={this.selectGroup} title="Add another group">
                            <option value={null}></option>
                            {this.props.groups.map(x => <option value={x.Id}>{x.Name}</option>)}
                        </Select>
                        <Button block primary onClick={this.addGroup}>Add group</Button>
                    </Card>
                </Grid.Col>
            </Grid.Row>
        </Grid>

        </div>)
        }
});

var mapStoreToProps = function(store){
    return {
        users: store.users || [],
        groups: store.groups || []
    };
};

AllowedUsersPage = connect(mapStoreToProps,null)(AllowedUsersPage);


export default AllowedUsersPage;