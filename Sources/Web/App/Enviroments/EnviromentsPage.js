import React from "react";
import Header from "../Shared/Header";
import Grid from "../Shared/Grid";
import Actions from "./Actions";
import Button from "../Shared/Button";
import ServerIcon from "../Shared/Icon/ServerIcon";

var Agent = React.createClass({
    render(){
        return <div className="enviroment__agent">
            <div className="enviroment__agent__icon"><ServerIcon /></div>
            <div className="enviroment__agent__description">{this.props.agent.Name}</div>
         </div>
    }
})

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


var EnviromentsPage = React.createClass({
    getInitialState: function() {
    return {
      envs : []
    };
  },

  componentDidMount: function() {
    this.update();
  },

  update:function(){
    Actions.updateAllEnviroments().then(x =>{
      this.setState({
        envs:x
      });
    });
  },

  render: function () {
    var envs = this.state.envs.map(a => (<Enviroment Enviroment={a} onUpdate={this.update}/>));

    return(<div>
        <Header actions={<Button>New Enviroment</Button>}>
            Enviroments
        </Header>
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

export default EnviromentsPage;