import React from "react";
import Header from "../Shared/Header";
import Input from "../Shared/Input";
import Grid from "../Shared/Grid";
import Card from "../Shared/Card";
import Actions from "./Actions";
import Button from "../Shared/Button";
import FileUpload from "../Shared/FileUpload";
import Notification from "../Shared/Notifications";

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

    onDrop: function (e) {
        var files=e.target.files;
        Actions.uploadFiles(files, this.state.OriginalName);
    },

    testConnection: function (event) {
        event.preventDefault()
        Actions.testAgent(this.state.Address)
            .then(x => alert("Agent responding"))
            .fail(x => alert("Agent not responding"))
    },

  rename: function(){
    Actions.rename(this.state.Id,this.state.EnviromentId,this.state.Name).then((x) => Notification.Notify(x));
  },

  changeAddress: function(){
    Actions.changeAddress(this.state.Id,this.state.EnviromentId,this.state.Address).then((x) => Notification.Notify(x));
  },


    render() {
        return (<div> 
            <Header><a href="#/Enviroments">Enviroments</a> &raquo; Agent {this.state.OriginalName}</Header>
            <div>
                <Grid fluid>
                    <Grid.Row>
                    <Grid.Col md={6}>
                        <Card title="Agent Description">
                            <Input title="Name" value={this.state.Name} onChange={(e)=>this.setState({Name: e.target.value})} buttons={<Button onClick={this.rename}>Rename</Button>} />
                        </Card>
                    </Grid.Col>

                    <Grid.Col md={6}>
                        <Card title="Agent Address">
                            <Input title="Address" value={this.state.Address} onChange={(e)=>this.setState({Address: e.target.value})} buttons={<div><Button onClick={this.changeAddress}>Change</Button><Button onClick={this.testConnection}>Test connection</Button></div>} />
                        </Card>
                    </Grid.Col>

                    <Grid.Col md={12}>
                        <Card title="Agent Update">
                            <FileUpload onChange={this.onDrop}>
                                <Button primary>Upload new version</Button>
                            </FileUpload>
                        </Card>
                    </Grid.Col>

                    </Grid.Row>
                </Grid>
            </div>
        </div>);
    }
});

export default AgentPage;