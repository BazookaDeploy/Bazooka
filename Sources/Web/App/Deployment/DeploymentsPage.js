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
      }else{
        return "Failed";
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

      var logs = this.state.deployments==null ? (<span />): this.state.deployments.Logs.map(x => (<span> <FormattedTime formats={formats} format="hhmm"  value={x.TimeStamp} /> : <span dangerouslySetInnerHTML={{ __html: (x.Text||"").replace(/(?:\r\n|\r|\n)/g, '<br />') }}></span> </span>))

      return(<div>
        <h2>{this.state.deployments.Name} - {this.state.deployments.Configuration}         <button className='btn btn-xs btn-default' onClick={this.reload}>{this.state.refreshing? "Reloading ..." : "Reload"}</button></h2>

          <h4>Current deployment status: {this.getStatus(this.state.deployments.Status)}</h4>
          <h5>Deploying version: {this.state.deployments.Version}</h5>
          <span>
          {this.state.deployments.StartDate!=null ?
          (<span>Deployment started on <FormattedDate value={this.state.deployments.StartDate} /> at <FormattedTime formats={formats} format="hhmm"  value={this.state.deployments.StartDate} />  </span>) : (<span />)}

          {this.state.deployments.EndDate!=null ? (<span>and ended at <FormattedTime formats={formats} format="hhmm"  value={this.state.deployments.EndDate} /></span> ): (<span />)}


          </span>
          <br />
          <h4>Logs:</h4>
          {logs}
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
