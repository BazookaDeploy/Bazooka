import React from "react";
import Header from "../Shared/Header";
import Grid from "../Shared/Grid";
import Actions from "./Actions";
import Modal from "../Shared/Modal";
import Button from "../Shared/Button";
import Input from "../Shared/Input";
import ServerIcon from "../Shared/Icon/ServerIcon";
import {withRouter} from "react-router";
import { connect } from 'react-redux';



var Agent = React.createClass({
    navigate: function () {
        this.props.router.push("/Enviroments/Agent/" + this.props.agent.Id );
    },

    render(){
        return <div className="enviroment__agent" onClick={this.navigate}>
            <div className="enviroment__agent__icon"><ServerIcon /></div>
            <div className="enviroment__agent__description">{this.props.agent.Name}</div>
         </div>
    }
})

Agent=withRouter(Agent);

var Enviroment = React.createClass({
    render(){
        return (<div className="enviroment">
            <div className="enviroment__title">{this.props.Enviroment.Name}</div>
            <div className="enviroment__actions"><Button>Add agent</Button></div>
            <div className="enviroment__agents">
                {this.props.Enviroment.Agents.map(x => <Agent agent={x} />)}
            </div>
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

        Actions.createEnviroment(this.state.name).then(() => {
            this.props.onClose();
            this.props.onCreate()
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