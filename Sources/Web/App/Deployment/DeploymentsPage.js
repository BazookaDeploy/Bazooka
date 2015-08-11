import React from "react";
import Router from 'react-router';
import Actions from "./ActionsCreator";
import ReactIntl from "react-intl";

import Modal  from "react-bootstrap/lib/Modal";
import ModalTrigger from "react-bootstrap/lib/ModalTrigger" ;

var FormattedDate = ReactIntl.FormattedDate;
var FormattedTime = ReactIntl.FormattedTime;
var { Route, DefaultRoute, RouteHandler, Link } = Router;

      var format = {
         "formats": {
            "time": {
                "hhmm": {
                    "hour": "numeric",
                    "minute": "numeric",
                    "second":"numeric"
                }
            }
         }
      }

      var formats =format.formats;

function SameDate(a,b){
  if(a==null || b==null){
    return false;
  }

  a= new Date(a);
  b=new Date(b);

  return a.getHours()==b.getHours() && a.getMinutes()==b.getMinutes() && a.getSeconds() == b.getSeconds();
}

  var LogLine = React.createClass({
    render:function(){
      return (
        <span>
          <dt style={{width:"80px"}}>{SameDate(this.props.PrevTimeStamp,this.props.TimeStamp) ? <span /> : <FormattedTime formats={formats} format="hhmm"  value={this.props.TimeStamp} />}</dt>
          <dd style={{marginLeft:"100px"}}>
            <span className={this.props.Error ? "text-danger" : ""}
              dangerouslySetInnerHTML={{ __html: (this.props.Text||"").replace(/(?:\r\n|\r|\n)/g, '<br />') }}>
            </span>
          </dd>
        </span>)
    }
  });

  var CancelDialog = React.createClass({
    create:function(){
      Actions.cancelDeployment(this.props.Id).then(x => {
        Actions.updateDeployment(this.props.Id);
        this.props.onRequestHide();
      });
    },

    render:function(){
      return(
        <Modal {...this.props} title="Cancel scheduled deploy">
        <div className="modal-body">
            <h4>Are you really sure that you want to cancel this scheduled deploy?</h4>
        </div>
        <div className="modal-footer">
          <button className="btn" onClick={this.props.onRequestHide}>Cancel</button>
          <button className="btn btn-primary" onClick={this.create}>Ok</button>
        </div>
        </Modal>);
      }
  });

  var DeploymentPage = React.createClass({
    mixins: [Router.State, ReactIntl.IntlMixin],
    getInitialState: function() {
      return {
        refreshing:false,
        deployments : {
        }
      };
    },

    componentDidMount: function() {
      this.reload();
    },

    reload:function(){
      var id = this.getParams().Id;
      Actions.updateDeployment(id).then(x => {
        this.setState({
          refreshing:false,
          deployments : x
        });

        if(this.state.deployments.Status == 1){
          setTimeout(this.reload,10000);
        }
      });
      this.setState({
        refreshing:true
      })
    },

    getStatus:function(status){
      if(status==0){
        return "Queued";
      }else if(status==1){
        return "Running";
      }else if(status==2){
        return "Ended";
      }else if(status==3){
          return "Failed";
      }else if(status==4){
          return "Scheduled";
      }else{
        return "Canceled";
      }
    },

    render: function () {
      debugger;
      var logs = this.state.deployments.Logs==null ? (<span />): this.state.deployments.Logs.map((x,index) => (<LogLine Error={x.Error} Text={x.Text} TimeStamp={x.TimeStamp} PrevTimeStamp={index>0?this.state.deployments.Logs[index-1].TimeStamp:null} />))

      return(<div>
        <h2>{this.state.deployments.Name} - {this.state.deployments.Configuration}         <button className='btn btn-xs btn-default' onClick={this.reload}>{this.state.refreshing? "Reloading ..." : "Reload"}</button></h2>

          <h4>Current deployment status: {this.getStatus(this.state.deployments.Status)}  {this.state.deployments.Status == 4 ?<ModalTrigger modal={<CancelDialog Id={this.getParams().Id}/>}><button className='btn btn-warning btn-xs'>Cancel scheduled deploy</button></ModalTrigger>: <span />}</h4>



          <h5>Deploying version: {this.state.deployments.Version}</h5>
          <span>
          {this.state.deployments.StartDate!=null ?
          (<span>Deployment {this.state.deployments.Status == 4 ? "scheduled" : "started" } on <FormattedDate value={this.state.deployments.StartDate} /> at <FormattedTime formats={formats} format="hhmm"  value={this.state.deployments.StartDate} />  </span>) : (<span />)}

          {this.state.deployments.EndDate!=null ? (<span>and ended at <FormattedTime formats={formats} format="hhmm"  value={this.state.deployments.EndDate} /></span> ): (<span />)}


          </span>
          <br />
          <h4>Logs:</h4>
          <dl className="dl-horizontal">
            {logs}
          </dl>
        </div>)
      }
    });

    module.exports = DeploymentPage;
