import React from "react";
import Router from 'react-router';
import Actions from "./ActionsCreator";
import Store from  "./Store";
import ReactIntl from "react-intl";
var FormattedDate = ReactIntl.FormattedDate;
var FormattedTime = ReactIntl.FormattedTime;
var { Route, DefaultRoute, RouteHandler, Link } = Router;

  var DeploymentPage = React.createClass({
    mixins: [Router.State, ReactIntl.IntlMixin],
    getInitialState: function() {
      return {
        refreshing:false,
        deployments : Store.getAll()
      };
    },

    componentDidMount: function() {
      Store.addChangeListener(this._onChange);
      var id = this.getParams().Id;
      Actions.updateDeployment(id);
    },

    componentWillUnmount: function() {
      Store.removeChangeListener(this._onChange);
    },

    reload:function(){
      var id = this.getParams().Id;
      Actions.updateDeployment(id);
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
      }else{
        return "Scheduled";
      }
    },

    render: function () {

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

      var logs = this.state.deployments==null ? (<span />): this.state.deployments.Logs.map(x => (<span><dt style={{width:"80px"}}><FormattedTime formats={formats} format="hhmm"  value={x.TimeStamp} /></dt> <dd style={{marginLeft:"100px"}}> <span className={x.Error ? "text-danger" : ""} dangerouslySetInnerHTML={{ __html: (x.Text||"").replace(/(?:\r\n|\r|\n)/g, '<br />') }}></span></dd></span>))

      return(<div>
        <h2>{this.state.deployments.Name} - {this.state.deployments.Configuration}         <button className='btn btn-xs btn-default' onClick={this.reload}>{this.state.refreshing? "Reloading ..." : "Reload"}</button></h2>

          <h4>Current deployment status: {this.getStatus(this.state.deployments.Status)}</h4>
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
      },

      _onChange: function(){
        this.setState({
          deployments : Store.getAll(),
          refreshing:false,
        })

        if(this.state.deployments.Status == 1){
          setTimeout(this.reload,10000);
        }
      }
    });

    module.exports = DeploymentPage;
