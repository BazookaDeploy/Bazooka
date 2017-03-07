import React from "react";
import Button from "../Shared/Button";
import Select from "../Shared/Select";
import Input from "../Shared/Input";
import Card from "../Shared/Card";
import Grid from "../Shared/Grid";
import Actions from "./Actions";
import Notification from "../Shared/Notifications";
import {connect} from "react-redux";
import {withRouter} from "react-router";

var OverviewPage = React.createClass({
    getInitialState() {
        return {
            url: null,
            groups: [],
            applicationGroup: null,
            hiddenSecret:true
        };
    },

    componentDidMount: function () {
        this.update()
    },

    update() {
        Actions.getApplicationGroups().then(x => this.setState({ groups: x }));
        Actions.getApplicationInfo(this.props.params.id).then(x => this.setState({ originalApplicationGroup: x.GroupName, originalName: x.Name, Name:x.Name, Secret: x.Secret }));
    },

    setGroup: function () {
        Actions.setApplicationGroup(this.props.params.id, this.state.applicationGroup).then(x => { Notification.Notify(x); this.update(); });
    },

    delete(){
        var res = window.confirm("Are you sure you want to delete this applicaiton?");

        if(res){
            Actions.deleteApplication(this.props.params.id).then(x => {
                Notification.Notify(x);
	            this.props.router.push("/Applications/");
            });
        }
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
                    <Grid.Col md={4}>
                        <Card>
                            <Select title="Change the group" onChange={(e) => this.setState({ applicationGroup: e.target.value })}>
                                <option value={null} />
                                {this.state.groups.map(x => <option value={x.Id} >{x.Name}</option>)}
                            </Select>
                            <br />
                            <Button primary block onClick={this.setGroup}>Set group</Button>
                        </Card>
                    </Grid.Col>
                    <Grid.Col md={4}>
                        <Card>
                            <Input title="Change application name" value={this.state.Name}onChange={(e) => this.setState({Name:e.target.value})}/>
                            <br />
                            <Button primary block onClick={this.rename}>Change name</Button>
                        </Card>
                    </Grid.Col>

                      <Grid.Col md={4}>
                        <Card>
                            <h4>Delete this application</h4>
                            <br />
                            <Button primary block onClick={this.delete}>Delete</Button>
                        </Card>
                    </Grid.Col>  

 
                      <Grid.Col md={12}>
                        <Card>
                            <h4>Hook for automatic deploy</h4>
                            <br />
                            {this.state.hiddenSecret ?
                                    <Button block primary onClick={() => this.setState({hiddenSecret:false})}>Reveal secret</Button>
                            :
                                <span>Your url is <b>/api/Deploy/WebHook?applicationId={this.props.params.id}&amp;enviromentId=YOURENV&amp;version=VERSION&amp;secret={this.state.Secret}</b> or <br />
                                <b>/api/Deploy/WebHookLatest?applicationId={this.props.params.id}&amp;enviromentId=YOURENV&amp;secret={this.state.Secret}</b>
                                </span>
                            }
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
OverviewPage = withRouter(OverviewPage);


export default OverviewPage;