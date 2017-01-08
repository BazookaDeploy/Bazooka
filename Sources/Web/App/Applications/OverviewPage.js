import React from "react";
import Button from "../Shared/Button";
import Select from "../Shared/Select";
import Grid from "../Shared/Grid";
import Actions from "./Actions";


var OverviewPage = React.createClass({
    getInitialState(){
        return {
            url : null,
            groups:[],
            applicationGroup: null
        };
    },

  componentDidMount: function(){
      this.update()
  },

  update(){
          Actions.getApplicationGroups().then(x => this.setState({groups:x}));
    Actions.getApplicationInfo(this.props.params.id).then(x => this.setState({originalApplicationGroup:x.GroupName}));

  },

  setGroup:function(){
    Actions.setApplicationGroup(this.props.params.id, this.state.applicationGroup).then(x => this.update());
  },

    render:function(){

        return (<div>
            <h3>Overview</h3>
            From this page you can configure the application by setting permissions for users and groups in the <b>Permissions</b> tab or configure the deploy process for every enviroment
            
            <br /> <br />
            <h4>Application Group</h4>
            Current Group: <b>{this.state.originalApplicationGroup}</b><br /><br />
            <Grid fluid>
                <Grid.Row>
                <Grid.Col md={3}>
            <Select title="Change the group"  onChange={(e) => this.setState({applicationGroup: e.target.value})}>
                <option value={null}  />
                {this.state.groups.map(x => <option value={x.Id} >{x.Name}</option>)}
            </Select>
            </Grid.Col>
            <Grid.Col md={3}>
            <br />
            <Button primary onClick={this.setGroup}>Set group</Button>
</Grid.Col>
</Grid.Row>
</Grid>
        </div>)
        }
});


export default OverviewPage;