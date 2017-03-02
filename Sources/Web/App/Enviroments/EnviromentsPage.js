import React from "react";
import Header from "../Shared/Header";
import Grid from "../Shared/Grid";
import Actions from "./Actions";
import Modal from "../Shared/Modal";
import Button from "../Shared/Button";
import Notification from "../Shared/Notifications";
import Input from "../Shared/Input";
import ServerIcon from "../Shared/Icon/ServerIcon";
import {withRouter} from "react-router";
import { connect } from 'react-redux';
import classname from "classnames";


var Agent = React.createClass({
    navigate: function () {
        this.props.router.push("/Enviroments/Agent/" + this.props.agent.Id );
    },

    render(){
        var classes = classname("enviroment__agent", {"enviroment__agent--unreachable":  this.props.agent.LastStatusCheck!= null && !this.props.agent.LastCheck});
        return (<div className={classes} onClick={this.navigate}>
            <div className="enviroment__agent__icon"><ServerIcon /></div>
            <div className="enviroment__agent__description">{this.props.agent.Name}</div>
         </div>);
    }
})

Agent=withRouter(Agent);

var AgentCreationDialog = React.createClass({
    getInitialState(){
        return { name: "", address : ""}
    },

    close(){
        this.setState({name:"", address: ""});
        this.props.onClose();
    },

    create(){
        if(this.state.name=="" || this.state.address == ""){
            return;
        }

        Actions.createAgent(this.props.enviromentId, this.state.name,this.state.address).then((x) => {
            Notification.Notify(x);
            if(x.Success){
                this.props.onClose();
                this.props.onCreate();
            }
        });
    },

    render(){
        return (<Modal onClose={this.onClose} {...this.props}>
                    <Modal.Header>Create new Agent</Modal.Header>
                    <Modal.Body>
                        <Input title="Name" value={this.state.name} onChange={(e) => this.setState({name: e.target.value})} />
                        <Input title="Address" value={this.state.address} onChange={(e) => this.setState({address: e.target.value})} />
                    </Modal.Body>
                    <Modal.Footer>
                        <Button onClick={this.close}>Cancel</Button>
                        <Button primary onClick={this.create}>Create</Button>
                    </Modal.Footer>
                </Modal>)
    }
});

var Enviroment = React.createClass({
    getInitialState(){
        return {
            shownewAgent: false
        };
    },

    render(){
        return (<div className="enviroment">
            <div className="enviroment__title">{this.props.Enviroment.Name}</div>
            <div className="enviroment__actions"><Button onClick={() => this.setState({shownewAgent:true})}>Add agent</Button></div>
            <div className="enviroment__agents">
                {this.props.Enviroment.Agents.map(x => <Agent agent={x} />)}
            </div>
            <AgentCreationDialog enviromentId={this.props.Enviroment.Id} show={this.state.shownewAgent} onClose={() => this.setState({shownewAgent:false})} onCreate={this.props.onUpdate} onUpdate={this.props.onUpdate} />
        </div>);
    }
});


var EnviromentCreationDialog = React.createClass({
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

        Actions.createEnviroment(this.state.name).then((x) => {
            Notification.Notify(x);

            if(x.Success){
                this.props.onClose();
                this.props.onCreate();
            }
        });
    },

    render(){
        return (<Modal onClose={this.onClose} {...this.props}>
                    <Modal.Header>Create new Enviroment</Modal.Header>
                    <Modal.Body>
                        <Input title="Name" value={this.state.name} onChange={(e) => this.setState({name: e.target.value})} />
                    </Modal.Body>
                    <Modal.Footer>
                        <Button onClick={this.onClose}>Cancel</Button>
                        <Button primary onClick={this.create}>Create</Button>
                    </Modal.Footer>
                </Modal>)
    }
});

var EnviromentsPage = React.createClass({
  getInitialState(){
    return {
      showNewEnviroment:false
    };
  },

  update(){
      this.props.loadEnviroments();
  },

  render: function () {
    var envs = this.props.enviroments.map(a => (<Enviroment Enviroment={a} onUpdate={this.update}/>));

    return(<div>
        <Header actions={<Button onClick={() => this.setState({showNewEnviroment:true})}>New Enviroment</Button>}>
            Enviroments
        </Header>
            <EnviromentCreationDialog onClose={() => this.setState({showNewEnviroment:false})} onCreate={this.update} show={this.state.showNewEnviroment}/>
            <Grid fluid>
                <Grid.Row>
                    <Grid.Col md={12}>
                        {envs}
                    </Grid.Col>
                </Grid.Row>
            </Grid>

      </div>)
  }
});


var mapStoreToProps = function(store){
    return {
        enviroments: store.enviroments || []
    }
};

var mapDispatchToProps = function(dispatch){
    return {
        loadEnviroments:function(){
            Actions.updateAllEnviroments().then(x => {
                dispatch({type: "ADD_ENVIROMENTS", enviroments: x});
            });
        }
    };
};

EnviromentsPage = connect(mapStoreToProps,mapDispatchToProps)(EnviromentsPage);

export default EnviromentsPage;