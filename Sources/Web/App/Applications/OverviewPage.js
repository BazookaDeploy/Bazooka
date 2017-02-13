import React from "react";
import Button from "../Shared/Button";
import Select from "../Shared/Select";
import Input from "../Shared/Input";
import Card from "../Shared/Card";
import Grid from "../Shared/Grid";
import Actions from "./Actions";
import Notification from "../Shared/Notifications";
import {connect} from "react-redux";


var OverviewPage = React.createClass({
    getInitialState() {
        return {
            url: null,
            groups: [],
            applicationGroup: null
        };
    },

    componentDidMount: function () {
        this.update()
    },

    update() {
        Actions.getApplicationGroups().then(x => this.setState({ groups: x }));
        Actions.getApplicationInfo(this.props.params.id).then(x => this.setState({ originalApplicationGroup: x.GroupName, originalName: x.Name, Name:x.Name }));
    },

    setGroup: function () {
        Actions.setApplicationGroup(this.props.params.id, this.state.applicationGroup).then(x => { Notification.Notify(x); this.update(); });
    },

    rename(){
        Actions.renmeApplication(this.props.params.id,this.state.Name).then(x => {this.update(); this.props.loadApplications();});
    },
 
    render: function () {

        return (<div>
            <h3>Overview</h3>
            From this page you can configure the application by setting permissions for users and groups in the <b>Permissions</b> tab or configure the deploy process for every enviroment

            <br /> <br />
            <h4>Application Group</h4>
            Current Group: <b>{this.state.originalApplicationGroup}</b><br /><br />
            <Grid fluid>
                <Grid.Row>
                    <Grid.Col md={3}>
                        <Card>
                            <Select title="Change the group" onChange={(e) => this.setState({ applicationGroup: e.target.value })}>
                                <option value={null} />
                                {this.state.groups.map(x => <option value={x.Id} >{x.Name}</option>)}
                            </Select>
                            <br />
                            <Button primary block onClick={this.setGroup}>Set group</Button>
                        </Card>
                    </Grid.Col>
                    <Grid.Col md={3}>
                        <Card>
                            <Input title="Change application name" value={this.state.Name}onChange={(e) => this.setState({Name:e.target.value})}/>
                            <br />
                            <Button primary block onClick={this.rename}>Change name</Button>
                        </Card>
                    </Grid.Col>
                </Grid.Row>
            </Grid>
        </div>)
    }
});

var mapDispatchToProps = function(dispatch){
    return {
        loadApplications:function(){
            Actions.getAllApplications().then(x => {
                dispatch({type: "ADD_APPLICATIONS", applications: x});
            });
        }
    };
};

OverviewPage = connect(null,mapDispatchToProps)(OverviewPage);



export default OverviewPage;