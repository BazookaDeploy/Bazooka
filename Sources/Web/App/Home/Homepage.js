import React  from "react";
import Actions from "./ActionsCreator";
import Modal  from "react-bootstrap/lib/Modal";
import ModalTrigger from "react-bootstrap/lib/ModalTrigger" ;

var DeployDialog = React.createClass({
  getInitialState: function() {
    return {
      loading : true,
      scheduled:false,
      versions:[]
    };
  },

  componentDidMount: function() {
    Actions.getVersions(this.props.Enviroment.Id).then(x => {
      this.setState({
        loading : false,
        versions : x
      })
    });
  },

  create:function(){
    var version = this.refs.Version.getDOMNode().value;
    if(version!=null){
      if(!this.state.scheduled){
        Actions.startDeploy(this.props.Enviroment.Id, version);
        this.props.onRequestHide();
      }else{
        var data = this.refs.deployDate.getDOMNode().value;
        var hour = this.refs.hour.getDOMNode().value;
        var minutes = this.refs.minutes.getDOMNode().value;

        Actions.scheduleDeploy(this.props.Enviroment.Id, version,new Date(data + " " + hour + ":" + minutes));
        this.props.onRequestHide();
      }
    }
  },

  setScheduled:function(){
    this.setState({
      scheduled:!this.state.scheduled
    })
  },

  render:function(){
    var title = "Start deploy " + this.props.Enviroment.Name + " - " + this.props.Enviroment.Configuration;

    var versions = this.state.versions.map(x => (<option>{x}</option>));

    return(
      <Modal {...this.props} title={title}>
      <div className="modal-body">
        <form role="form">
          <div className="form-group">
            <label htmlFor="Version">Version</label>
            {
              this.state.loading ?
                <span><br />Loading available versions ... </span> :
                <select className="form-control" id="Version" ref="Version" placeholder="Version">
                  {versions}
                </select>
            }
          </div>

          <div className="form-group">
            <label htmlFor="Schedule">Schedule the deploy: <input type="checkbox" checked={this.state.scheduled} onChange={this.setScheduled}/></label>
          </div>
          {
              this.state.scheduled ?
                <div className="form-group container-fluid row">
                  <input type="date" ref="deployDate" className="form-control col-lg-6" min={(new Date()).toISOString().substring(0,10)} />
                  <br />
                  <br />
                <select ref="hour" className="form-control" style={{width:"48%", float:"left"}}>
                  <option>00</option>
                  <option>01</option>
                  <option>02</option>
                  <option>03</option>
                  <option>04</option>
                  <option>05</option>
                  <option>06</option>
                  <option>07</option>
                  <option>08</option>
                  <option>09</option>
                  <option>12</option>
                  <option>13</option>
                  <option>14</option>
                  <option>15</option>
                  <option>16</option>
                  <option>17</option>
                  <option>18</option>
                  <option>19</option>
                  <option>20</option>
                  <option>21</option>
                  <option>22</option>
                  <option>23</option>
                </select>
                <select ref="minutes"  className="form-control  col-lg-2" style={{width:"50%", float:"left"}}>
                {Array.apply(null, {length: 59}).map(Number.call, Number).map( x => (<option>{"00".substring(x.toString().length) +  x.toString()}</option>))}
                </select>
                </div> :
                <span></span>
            }

        </form>
      </div>
      <div className="modal-footer">
        <button className="btn" onClick={this.props.onRequestHide}>Close</button>
        <button className="btn btn-primary" onClick={this.create}>Deploy</button>
      </div>
      </Modal>);
    }
});


var Application = React.createClass({
  render: function(){
    var envs = this.props.Application.Enviroments.map(x => (
      <Enviroment Enviroment={x}/>
    ));

    return (<div className="application">
      <div className=' panel panel-primary'>
        <div className='panel-heading'>{this.props.Application.Application}</div>
        <div className='panel-body'>
          {envs}
        </div>
      </div>
    </div>);
  }
})

var Enviroment = React.createClass({
  render: function(){
      var oneVersion = this.props
                           .Enviroment
                           .Versions
                           .map(x => x.CurrentlyDeployedVersion)
                           .reduce(function(a, b){return (a === b)?a:false;});

      oneVersion = oneVersion === this.props.Enviroment.Versions[0].CurrentlyDeployedVersion;

      if(oneVersion){
        var version = this.props.Enviroment.Versions[0].CurrentlyDeployedVersion || "None";
        return (<div style={{"marginBottom":"10px"}}>
            Enviroment: <b>{this.props.Enviroment.Enviroment}</b> <ModalTrigger modal={<DeployDialog Enviroment={this.props.Enviroment}/>}>
                      <button className='btn btn-primary btn-xs pull-right'>Deploy</button>
                    </ModalTrigger>
                    <br /> Version: <b>{version}</b><br /></div>);
      }

      var units = this.props.Enviroment.Versions.map(x => (
        <li><b>{x.Name}</b> Version: {x.CurrentlyDeployedVersion || "None"}</li>
      ))

      return  (<div style={{"marginBottom":"10px"}}>Enviroment: <b>{this.props.Enviroment.Enviroment}</b> <ModalTrigger modal={<DeployDialog Enviroment={this.props.Enviroment}/>}>
                      <button className='btn btn-primary btn-xs pull-right'>Deploy</button>
                    </ModalTrigger>
        <ul>
        {units}
        </ul>
        <br />
      </div>)
  }
});

var HomePage = React.createClass({
    getInitialState: function() {
      return {
        envs : []
      };
    },

    componentDidMount: function() {
      Actions.updateEnviroments().then(x => {
        this.setState({envs:x})
      });
    },

    render: function () {
        return (
          <div>
            <h2>Current system status</h2>
            <br />
            <div className='container applicationGrid'>{this.state.envs.map(x => (<Application Application={x} />))}</div>
          </div>);
    }
});


module.exports = HomePage;
