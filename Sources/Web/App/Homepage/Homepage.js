import React from "react";
import Table from "../Shared/Table";
import Button from "../Shared/Button";
import Header from "../Shared/Header";
import Grid from "../Shared/Grid";
import ArrowUpIcon from "../Shared/Icon/ArrowUpIcon";
import ArrowDownIcon from "../Shared/Icon/ArrowDownIcon";
import DeployDialog from "./DeployDialog";
import Actions from "./Actions";
import prettyDate from "../Shared/Utils/PrettyDate";

var Enviroment = React.createClass({
    getInitialState(){
        return {
            showDialog:false
        }
    },

  render: function () {
    if (this.props.Enviroment == undefined) {
      return <td></td>;
    }

    var oneVersion = this.props
      .Enviroment
      .Versions
      .map(x => x.CurrentlyDeployedVersion)
      .reduce(function (a, b) { return (a === b) ? a : false; }) === this.props.Enviroment.Versions[0].CurrentlyDeployedVersion

    if (oneVersion) {
      var version = this.props.Enviroment.Versions[0].CurrentlyDeployedVersion || "None";
      return (<td>
          <b>{version}</b> &nbsp; {this.props.Enviroment.LastDeploymentDate != null && <span title={this.props.Enviroment.LastDeploymentDate}><br /><small>({prettyDate(this.props.Enviroment.LastDeploymentDate)})</small></span>} <br /><br /> <DeployDialog onClose={() => this.setState({ show: false })} show={this.state.show} Enviroment={this.props.Enviroment} ApplicationId={this.props.ApplicationId} />
          <Button primary onClick={() => this.setState({show:true})}>Deploy</Button>
      </td>);
    }

    var units = this.props.Enviroment.Versions.map(x => (
      <li>{x.Name}: <b>{x.CurrentlyDeployedVersion || "None"}</b></li>
    ))

    return (<td>
      <ul className="application__versionList">
        {units}
      </ul>
      {this.props.Enviroment.LastDeploymentDate != null && <span title={this.props.Enviroment.LastDeploymentDate}><br /><small>({prettyDate(this.props.Enviroment.LastDeploymentDate)})</small></span>}
      <br />
      <DeployDialog show={this.state.show} onClose={() => this.setState({show:false})} Enviroment={this.props.Enviroment} ApplicationId={this.props.ApplicationId} />
          <Button primary onClick={() => this.setState({show:true})}>Deploy</Button>
    </td>)
  }
});

var Application = React.createClass({
  render: function () {
    return (
      <tr>
        <td>{this.props.Application.Name}</td>
        {this.props.Enviroments.map(x => <Enviroment EnviromentId={x.Id} ApplicationId={this.props.Application.Id} Enviroment={this.props.Application.Enviroments.filter(z => z.Id == x.Id)[0]}/>) }
      </tr>);
  }
});

var ApplicationGroup = React.createClass({
  render: function () {
    return (
      <div className="applicationGroup">
        <h3 className="applicationGroup__title">{this.props.Group.GroupName} 
        <div className="applicationGroup__actions">
        {this.props.editMode ? <Button onClick={() => this.props.onMoveUp(this.props.Group)}><ArrowUpIcon /></Button> : null}
        {this.props.editMode ? <Button onClick={() => this.props.onMoveDown(this.props.Group)}><ArrowDownIcon /></Button> : null}
        </div>
        </h3>
        <Table bordered className="applicationGroup__table">
          <Table.Head>
            <tr>
              <th></th>
              {this.props.Enviroments.map(x => (<th>{x.Name}</th>)) }
            </tr>
          </Table.Head>
          <Table.Body>
            {this.props.Group.Applications.map(x => <Application Application={x} Enviroments={this.props.Enviroments} />) }
          </Table.Body>
        </Table>
      </div>
    )
  }
});

var swapArray = function(A,x,y){
  A[x] = A.splice(y, 1, A[x])[0];
};

var HomePage = React.createClass({
  getInitialState: function () {
    return {
      editMode: false,
      envs: { Applications: [], Enviroments: [] }
    };
  },

  componentDidMount: function () {
    Actions.updateEnviroments().then(x => {
      this.setState({ envs: x }, this.ordina);
    });
  },

  ordina(){
    if(window.localStorage){
      var groups = window.localStorage.getItem("homeGroups");

      if(groups == null || groups == ""){
        groups="[]";
        window.localStorage.setItem("homeGroups",groups);
      }

      var gruppi = JSON.parse(groups);

      var i=0;
      for(i=0; i<this.state.envs.Applications.length; i++){
        var pos = gruppi.findIndex(x => x.GroupName == this.state.envs.Applications[i].GroupName);

        if(pos!=-1){
          swapArray(this.state.envs.Applications,i,pos);     
        }
      }

      window.localStorage.setItem("homeGroups",JSON.stringify(this.state.envs.Applications));   

      this.setState({envs: this.state.envs});
    }
  },

  moveUp(group){
    if(window.localStorage){
      var groups = window.localStorage.getItem("homeGroups");
      var gruppi = JSON.parse(groups);
      var pos = gruppi.findIndex(x => x.GroupName == group.GroupName);
      if(pos > 0){
        swapArray(gruppi,pos,pos-1);
      }

      window.localStorage.setItem("homeGroups",JSON.stringify(gruppi));    
      this.ordina();
    }
  },

  moveDown(group){
    if(window.localStorage){
      var groups = window.localStorage.getItem("homeGroups");
      var gruppi = JSON.parse(groups);
      var pos = gruppi.findIndex(x => x.GroupName == group.GroupName);
      if(pos < gruppi.length-1 ){
        swapArray(gruppi,pos,pos+1);
      }

      window.localStorage.setItem("homeGroups",JSON.stringify(gruppi));    
      this.ordina();
    }
  },

  modify(){
    this.setState({editMode : !this.state.editMode});
  },

  render: function () {
    return (
      <div>
        <Header actions={<Button onClick={this.modify}>{this.state.editMode ? "Save": "Modify"}</Button>}>
            Current system status
        </Header>
        <Grid fluid>
            <Grid.Row>
                <Grid.Col md={12}>
                  {this.state.envs.Applications.map(x => (<ApplicationGroup Group={x} Enviroments={this.state.envs.Enviroments} editMode={this.state.editMode} onMoveUp={() => this.moveUp(x)} onMoveDown={() => this.moveDown(x)} />)) }
                </Grid.Col>
            </Grid.Row>
        </Grid>
      </div>);
  }
});


export default HomePage;