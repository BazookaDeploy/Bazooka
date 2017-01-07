import React from "react";
import Button from "../Shared/Button";
import Grid from "../Shared/Grid";
import Actions from "./Actions";
import Modal from "../Shared/Modal";
import Select from "../Shared/Select";
import {Link} from "react-router";
import {connect} from "react-redux";


import RemoteScriptTaskCreationDialog from "./Tasks/RemoteScriptTask/CreateDialog";
import MailTaskCreationDialog from "./Tasks/MailTask/CreateDialog";
import LocalScriptTaskCreationDialog from "./Tasks/LocalScriptTask/CreateDialog";
import DatabaseTaskCreationDialog from "./Tasks/DatabaseTasks/CreateDialog";
import DeployTaskCreationDialog from "./Tasks/DeployTasks/DeployUnitsDialog";



var TaskSelectDialog = React.createClass({
    getInitialState(){
        return {
            show0:false,
            show1:false,
            show2:false,
            show3:false,
            show4:false
        }
    },

    onClose(){
        this.setState({
              show0:false,
            show1:false,
            show2:false,
            show3:false,
            show4:false          
        }, this.props.onClose)
    },

  render:function(){
    return(
     <Modal {...this.props}>
        <Modal.Header>Add new task</Modal.Header>
     <Modal.Body>
        <Button block onClick={() => this.setState({show0:true})}>Deploy task</Button>
        {this.state.show0 && <DeployTaskCreationDialog onCreate={this.props.onCreate} Enviroment={this.props.EnviromentId} ApplicationId={this.props.ApplicationId} show={this.state.show0} onClose={this.onClose} />}

        <Button block onClick={() => this.setState({show1:true})}>Mail task</Button>
        {this.state.show1 && <MailTaskCreationDialog onCreate={this.props.onCreate} Enviroment={this.props.EnviromentId} ApplicationId={this.props.ApplicationId} show={this.state.show1} onClose={this.onClose} />}

        <Button block onClick={() => this.setState({show2:true})}>Local script task</Button>
        {this.state.show2 && <LocalScriptTaskCreationDialog onCreate={this.props.onCreate} Enviroment={this.props.EnviromentId} ApplicationId={this.props.ApplicationId} show={this.state.show2} onClose={this.onClose} />}

        <Button block onClick={() => this.setState({show3:true})}>Remote script task</Button>
        {this.state.show3 && <RemoteScriptTaskCreationDialog onCreate={this.props.onCreate} Enviroment={this.props.EnviromentId} ApplicationId={this.props.ApplicationId} show={this.state.show3} onClose={this.onClose} /> }

        <Button block onClick={() => this.setState({show4:true})}>Database task</Button>
        {this.state.show4 && <DatabaseTaskCreationDialog onCreate={this.props.onCreate} Enviroment={this.props.EnviromentId} ApplicationId={this.props.ApplicationId} show={this.state.show4} onClose={this.onClose} /> }
     </Modal.Body>
     <Modal.Footer>
       <Button onClick={this.props.onClose}>Close</Button>
     </Modal.Footer>
     </Modal>);
  }
});


var CloneDialog = React.createClass({
  getInitialState:function(){
    return {
      agents : []
    }
  },

  componentDidMount: function(){
    Actions.getAgents(this.props.EnviromentId).then(x => this.setState({agents:x}));
  },

  clone:function(){
    Actions.cloneEnviroment(this.refs.enviroment.value(), this.refs.machine.value(), this.props.EnviromentId, this.props.ApplicationId).then(x =>
    {
      this.props.onCreate();
      this.props.onClose();
    })
  },

  render:function(){
    return(
     <Modal {...this.props}>
     <Modal.Header>Clone from enviroment</Modal.Header>
     <Modal.Body>

            <Select title="Select enviroment to clone"  ref="enviroment" >
                {this.props.enviroments.map(x => <option value={x.Id}>{x.Name}</option>)}
              </Select>

              <Select title="Select machine" ref="machine" >
                {this.state.agents.map(x => <option value={x.Id}>{x.Name}</option>)}
              </Select>
     </Modal.Body>
      <Modal.Footer>
        <Button  onClick={this.props.onClose}>Close</Button>
        <Button primary onClick={this.clone}>Clone</Button>
      </Modal.Footer>
     </Modal>);
  }
});



var mapStoreToProps = function(store){
    return {
        enviroments: store.enviroments || [],
    };
};

CloneDialog = connect(mapStoreToProps,null)(CloneDialog);




var TaskLine = React.createClass({
    render(){
        switch( this.props.task.Type){
            case 0:
                return <Link activeClassName="active" to={"/Applications/"+ this.props.params.id+ "/Enviroment/"+this.props.params.enviromentId+ "/Tasks/DeployTask/"+ this.props.task.Id}>{this.props.task.Name}</Link>;
            case 1:
                return <Link activeClassName="active" to={"/Applications/"+ this.props.params.id+ "/Enviroment/"+this.props.params.enviromentId+ "/Tasks/MailTask/"+ this.props.task.Id}>{this.props.task.Name}</Link>;
            case 2:
                return <Link activeClassName="active" to={"/Applications/"+ this.props.params.id+ "/Enviroment/"+this.props.params.enviromentId+ "/Tasks/LocalScriptTask/"+ this.props.task.Id}>{this.props.task.Name}</Link>;
            case 3:
                return <Link activeClassName="active" to={"/Applications/"+ this.props.params.id+ "/Enviroment/"+this.props.params.enviromentId+ "/Tasks/RemoteScriptTask/"+ this.props.task.Id}>{this.props.task.Name}</Link>;
            case 4:
                return <Link activeClassName="active" to={"/Applications/"+ this.props.params.id+ "/Enviroment/"+this.props.params.enviromentId+ "/Tasks/DatabaseTask/"+ this.props.task.Id}>{this.props.task.Name}</Link>;
        }
    }
});


var TasksPage = React.createClass({
    getInitialState(){
        return {show:false,showCreateDialog:false, tasks: []}
    },

    componentDidMount(){
        this.update(this.props.params.enviromentId);
    },

    componentWillReceiveProps(nextProps){
      if(this.props.params.id != nextProps.params.id || this.props.params.enviromentId!=nextProps.params.enviromentId){
          this.update(nextProps.params.enviromentId)
      }
    },

    update(id){
        Actions.getTasks(id, this.props.params.id).then((x) => this.setState({tasks:x}));
    },

    render:function(){

        return (<div>

        {this.state.show && <CloneDialog show={this.state.show}  onClose={() => this.setState({show:false})} onCreate={this.update} EnviromentId={this.props.params.enviromentId} ApplicationId={this.props.params.id}/>}
 
        {this.state.showCreateDialog && <TaskSelectDialog onClose={() => this.setState({showCreateDialog:false})} onCreate={this.update} show={this.state.showCreateDialog} EnviromentId={this.props.params.enviromentId} ApplicationId={this.props.params.id}/>}
 

        
        <Grid fluid>
            <Grid.Row>
                <Grid.Col md={4}>
                    <div className="applicationTasks">
                    {this.state.tasks.map(x => <TaskLine task={x} params={this.props.params}/>)}

                    {this.state.tasks.length==0 && <div className="cloneSection">
                        <Button onClick={() => this.setState({show:true})} primary block> Clone from another enviroment</Button>
                    </div>}

                    {this.state.tasks.length!=0 && <div className="addSection">
                        <Button onClick={() => this.setState({showCreateDialog:true})} primary block> Add new task</Button>
                    </div>}                    
                    </div>
                </Grid.Col>
                <Grid.Col md={8}>
                    {this.props.children || <div className="defaultTask">Choose a task on the left to edit it</div>}
                </Grid.Col>
            </Grid.Row>
        </Grid>
        </div>)
        }
});


export default TasksPage;