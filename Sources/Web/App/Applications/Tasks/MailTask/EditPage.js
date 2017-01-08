import React from "react";
import Button from "../../../Shared/Button";
import Input from "../../../Shared/Input";
import Select from "../../../Shared/Select";
import Textarea from "../../../Shared/Textarea";
import Actions from "./Actions";


var EditPage = React.createClass({
  getInitialState:function(){
    return {
      Id:0,
      EnviromentId:0,
      Name:"",
      Text:"",
      Recipients:"",
      Sender:""
    };
  },

  componentDidMount:function(){
    this.update(this.props.params.taskId);
  },

  componentWillReceiveProps(nextProps){
    if(this.props.params.taskId!=nextProps.params.taskId){
      this.update(nextProps.params.taskId);
    }
  },

  update(taskId){
    Actions.getMailTask(taskId).then(x => {
      this.setState(x);
    })
  },

  save:function(){
    if(this.state.Name!="" && this.state.Recipients!="" && this.state.Sender!=""){
      Actions.updateMailTask(this.state.Id,this.state.Name, this.state.Text,this.state.Recipients,this.state.Sender, this.state.EnviromentId, this.state.ApplicationId).then(x => {
      })
    }
  },

  render:function(){
    return(
      <div>
           <Input title="Name" placeholder="Name" autoFocus value={this.state.Name} onChange={(e)=> this.setState({Name: e.target.value})} />
           <Input title="Recipients" placeholder="Recipients" value={this.state.Recipients} onChange={(e)=> this.setState({Recipients: e.target.value})} />
           <Input title="Sender" placeholder="Sender" value={this.state.Sender} onChange={(e)=> this.setState({Sender: e.target.value})}  />
           <Textarea title="Text" placeholder="Text" value={this.state.Text} onChange={(e)=> this.setState({Text: e.target.value})}  />
       <Button primary onClick={this.save}>Save</Button>
       </div>
);
   }
});


module.exports=EditPage;
