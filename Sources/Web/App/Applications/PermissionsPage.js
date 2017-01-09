import React from "react";
import Button from "../Shared/Button";
import Select from "../Shared/Select";
import Grid from "../Shared/Grid";
import Card from "../Shared/Card";
import Actions from "./Actions";
import {connect} from "react-redux";
import Notification from "../Shared/Notifications";

var PermissionsPage = React.createClass({
    getInitialState(){
        return {users: [], currentUser:null}
    },

    componentDidMount(){
        this.updateUsers();
    },

    selectUser(e){
        this.setState({currentUser:e.target.value})
    },

    addUser(){
        if(this.state.currentUser!=null){
            Actions.addAdmin(this.state.currentUser,this.props.params.id).then((x) => {Notification.Notify(x); this.updateUsers();});
        }
    },

    removeUser(id){
            Actions.removeAdmin(id, this.props.params.id).then((x) => {Notification.Notify(x); this.updateUsers();});
    },

    updateUsers(){
        Actions.getAdmins(this.props.params.id).then(x => this.setState({users: x}))
    },

    render:function(){

        return (<div>

            <h3>Application administrators</h3>

            <Grid fluid>
                <Grid.Row>
                    <Grid.Col md={4}>
                        <ul className="group">
                            {this.state.users.map(x=><li>{x.UserName} <Button onClick={() => this.removeUser(x.UserId)}>Remove</Button></li>)}
                        </ul>
                        <Select onChange={this.selectUser} title="Add another user">
                            <option value={null}></option>
                            {this.props.users.map(x => <option value={x.Id}>{x.UserName}</option>)}
                        </Select>
                        <Button block primary onClick={this.addUser}>Add User</Button>
                    </Grid.Col>
                </Grid.Row>
            </Grid>
        </div>)
        }
});



var mapStoreToProps = function(store){
    return {
        users: store.users || [],
    };
};

PermissionsPage = connect(mapStoreToProps,null)(PermissionsPage);



export default PermissionsPage;