var React = require("react");
var Actions = require("./ActionsCreator");
var Store = require("./Store");
var Modal = require("react-bootstrap/Modal");
var ModalTrigger = require("react-bootstrap/ModalTrigger");

var DeployDialog = React.createClass({
  getInitialState: function() {
    return {
      loading : true,
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
      Actions.startDeploy(this.props.Enviroment.Id, version);
      this.props.onRequestHide();
    }
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

    return (<div className="col-md-4 col-lg-3">
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
        envs : Store.getAll()
      };
    },

    componentDidMount: function() {
      Store.addChangeListener(this._onChange);
      Actions.updateEnviroments();
    },

    componentWillUnmount: function() {
      Store.removeChangeListener(this._onChange);
    },

    _onChange: function(){
      this.setState({
        envs : Store.getAll()
      })
    },

    render: function () {
        return (
          <div>
            <h2>Current system status</h2>
            <br />
            <div className='container'>{this.state.envs.map(x => (<Application Application={x} />))}</div>
          </div>);
    }
});


module.exports = HomePage;
