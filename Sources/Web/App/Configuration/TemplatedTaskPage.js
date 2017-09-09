import React from "react";
import Header from "../Shared/Header";
import Actions from "./Actions";
import Button from "../Shared/Button";
import Grid from "../Shared/Grid";
import Select from "../Shared/Select";
import Modal from "../Shared/Modal";
import Input from "../Shared/Input";
import Table from "../Shared/Table";
import Card from "../Shared/Card";
import { connect } from 'react-redux';
import Notification from "../Shared/Notifications";
import Textarea from "../Shared/Textarea";
import { withRouter } from "react-router";
import Tabs from "../Shared/Tabs";

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

    removeParameter: function (index) {
        this.state.Parameters.splice(index, 1);
        this.setState({ Parameters: this.state.Parameters });
    },

    addParameter: function () {
        if (this.state.temporaryName) {
            this.setState({
                Parameters: this.state.Parameters.concat({
                    Name: this.state.temporaryName,
                    Optional: this.state.temporaryOptional,
                    Encrypted: this.state.temporaryEncrypted,
                    Description: this.state.temporaryDescription
                }),
                temporaryName: "",
                temporaryOptional: false,
                temporaryEncrypted: false,
                temporaryDescription:""
            });
        }
    },

    save: function () {
        Actions.createNewVersion(this.props.params.id, this.state.Script, this.state.Parameters).then(x => this.update());
    },

    render(){
        return (<div>
            <h3>Templated Task {this.state.Name}</h3>
            
            <Grid fluid>
                <Tabs>
                    <Tabs.Tab title="Description">

                        <Grid.Row>
                            <Input title="Name" value={this.state.Name} onChange={(e) => this.setState({ Name: e.target.value })} />
                            <Button primary block onClick={this.rename}>Rename</Button>
                            <Textarea title="Description" rows={5} value={this.state.Description} onChange={(e) => this.setState({ Description: e.target.value })} />
                            <Button primary onClick={this.changeDescription} block >Change description</Button>                         
                        </Grid.Row>
                    </Tabs.Tab>
                    <Tabs.Tab title="Parameters">
                        <Grid.Row>
                            <Table>
                                <Table.Head>
                                    <tr>
                                        <td>Name</td>
                                        <td>Description</td>
                                        <td>Optional</td>
                                        <td>Encrypted</td>
                                        <td></td>
                                    </tr>
                                </Table.Head>
                                <Table.Body>
                                    {this.state.Parameters.map((x, i) =>
                                        <tr>
                                            <td>{x.Name}</td>
                                            <td>{x.Description}</td>
                                            <td>{x.Optional && <span>&#x2714;</span>}</td>
                                            <td>{x.Encrypted && <span>&#x2714;</span>}</td>
                                            <td><Button onClick={() => this.removeParameter(i)}>Delete</Button></td>
                                        </tr>
                                    )}
                                    <tr>
                                        <td><Input value={this.state.temporaryName} onChange={(e) => this.setState({ temporaryName: e.target.value })} /></td>
                                        <td><Textarea rows={1} value={this.state.temporaryDescription} onChange={(e) => this.setState({ temporaryDescription: e.target.value })} /></td>
                                        <td><input type="checkbox" checked={this.state.temporaryOptional} onClick={(e) => this.setState({ temporaryOptional: !this.state.temporaryOptional })} /></td>
                                        <td><input type="checkbox" checked={this.state.temporaryEncrypted} onClick={(e) => this.setState({ temporaryEncrypted: !this.state.temporaryEncrypted })} /></td>
                                        <td><Button onClick={this.addParameter}>Add</Button></td>
                                    </tr>
                                </Table.Body>
                            </Table>
                        </Grid.Row>
                    </Tabs.Tab>
                    <Tabs.Tab title="Script">

                        <Grid.Row>
                            <div>
                                <b>Note:</b> Some parameters are pre-defined and avilable to your sccript like <b>$version</b>, <b>$config</b>, <b>$repository</b> and <b>$packageName</b>.
                            </div>
                            <br />
                            <br />
                            <Textarea title="Script" rows={20} value={this.state.Script} onChange={(e) => this.setState({ Script: e.target.value })} />
                        </Grid.Row>
                    </Tabs.Tab>
                </Tabs>

                <Grid.Row>
                    <br />
                    &nbsp;
                    <br />
                    <br />
                    <Button block primary onClick={this.save}>Save</Button><br />&nbsp;<br />&nbsp;<br />
                    </Grid.Row>
            </Grid>
            </div>
        )
    }
});



export default TemplatedTaskPage;