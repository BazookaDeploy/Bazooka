var React = require("react");
var LinkedState = require("react/lib/LinkedStateMixin");
var Router = require('react-router');
var Actions = require("./ActionsCreator");
var Store = require("./Store");
var TabbedArea = require("react-bootstrap/TabbedArea");
var TabPane = require("react-bootstrap/TabPane");

var {
	Route, DefaultRoute, RouteHandler, Link, State
} = Router;


var EditPage = React.createClass({
	mixins: [LinkedState,State],

	/**
   * Adds a new parameter to the list if key and value are set
   * an there isn't already another parameter with the same key
   */
  addParameter : function(){
    if(this.state.key.length!=0 &&
      this.state.value.length != 0 &&
       !this.state.Parameters.some(x => x.Key == this.state.key)){

				debugger;
      this.setState({
				Parameters: this.state.Parameters.concat({
          Name: this.state.key,
          Value: this.state.value,
					Encrypted:this.state.Encrypted
        }),
        key:"",
        value:"",
				Encrypted:false
      })
    }
  },

  remove: function(key){
    var index = -1;
    var i;
    for(i = 0; i < this.state.Parameters.length; i++){
      if(this.state.Parameters[i].Name==key){
        index=i;
        break;
      }
    }
    this.state.Parameters.splice(index,1);
    this.setState({
			Parameters: this.state.Parameters
    });
  },


  testAgent : function(){
    Actions.testAgent(this.state.machine).then(x => {
      alert("Agent responding");
    }).fail(x =>{
        alert("Agent not responding");
    })
	return false;
  },

	componentDidMount: function() {
		Store.addChangeListener(this._onChange);
		var id = this.getParams().deployUnitId;
		Actions.updateDeployUnit(id);
	},

	componentWillUnmount: function() {
		Store.removeChangeListener(this._onChange);
	},

	_onChange:function(){
		var env = Store.getSingle(this.getParams().deployUnitId);

		this.setState({
			Id: env.Id,
			Enviroment: env.EnviromentId,
			Name:env.Name,
			Machine:env.Machine,
			PackageName: env.PackageName,
			Directory:env.Directory,
			Repository:env.Repository,
			key:"",
			value:"",
			Encrypted:false,
			Parameters : env.Parameters,
			UninstallationScript : env.UninstallScript,
			InstallationScript:env.InstallScript,
			ConfigurationFile:env.ConfigurationFile,
			ConfigTransform:env.ConfigurationTransform
		})
	},

	getInitialState: function() {

		return {
			Name:null,
			Machine:null,
			PackageName: null,
			Directory:null,
			Repository:null,
			key:"",
			value:"",
			Encrypted:false,
			Parameters : [],
			UninstallationScript : null,
			InstallationScript:null,
			ConfigurationFile:null,
			ConfigTransform:null
		};

	},

	save:function(){
		Actions.modifyDeployUnit(
			this.state.Id,
			this.state.Enviroment,
			this.state.Name,
			this.state.Machine,
			this.state.PackageName,
			this.state.Directory,
			this.state.Repository,
			this.state.Parameters,
			this.state.UninstallationScript,
			this.state.InstallationScript,
			this.state.ConfigurationFile,
			this.state.ConfigTransform
			)
					return;
	},

 	render: function() {

		return (
			<div>
				<TabbedArea defaultActiveKey={1}>
		    	<TabPane eventKey={1} tab='Settings'>
						<form role="form">
							<div className="form-group">
								<label htmlFor="Name">Name</label>
								<input type="text" className="form-control" id="Name" placeholder="Name" valueLink={this.linkState('Name')} />
							</div>
							<div className="form-group">
								<label htmlFor="Machine">Machine</label>
									<div className="input-group">
										<input type="text" className="form-control" id="Machine" placeholder="Machine" valueLink={this.linkState('Machine')} />
										<span className="input-group-btn">
											<button className="btn btn-primary" onClick={this.testAgent} >Test</button>
										</span>
									</div>
									<h5>Additional Params</h5>
										<ul>
											{this.state.Parameters.map(a => (<li>{a.Name} = {a.Encrypted? "********" : a.Value} <button className="btn btn-xs btn-danger" onClick={this.remove.bind(this,a.Key)}><i className="glyphicon glyphicon-trash"></i></button></li>))}
										</ul>
									<div className="form-group row">
					          <div className="col-md-3">
					            <label htmlFor="Key">Key</label>
					            <input type="text" className="form-control" id="Key" placeholder="Key" valueLink={this.linkState('key')} />
					          </div>
					          <div className="col-md-3">
					            <label htmlFor="Value">Value</label>
					            <input type="text" className="form-control" id="Value" placeholder="Value" valueLink={this.linkState('value')} />
					          </div>
										<div className="col-md-2">
					            <label htmlFor="Encrypted">Encrypted</label><br/>
					            <input type="checkbox" className="" id="Encrypted" checkedLink={this.linkState('Encrypted')} />
					          </div>
					          <div className="col-md-4">
					            <br />
					            <button className="btn btn-primary" onClick={this.addParameter} >Add Parameter</button>
					          </div>
					        </div>

							</div>
							<div className="form-group">
								<label htmlFor="PackageName">PackageName</label>
								<input type="text" className="form-control" id="PackageName" placeholder="PackageName" valueLink={this.linkState('PackageName')} />
							</div>
							<div className="form-group">
								<label htmlFor="Directory">Directory</label>
								<input type="text" className="form-control" id="Directory" placeholder="Directory" valueLink={this.linkState('Directory')} />
							</div>
							<div className="form-group">
								<label htmlFor="Repository">Repository</label>
								<input type="text" className="form-control" id="Repository" placeholder="Repository" valueLink={this.linkState('Repository')} />
							</div>
						</form>
					</TabPane>
		    	<TabPane eventKey={2} tab='Scripts'>
						<form>
							<div className="form-group">
								<label htmlFor="InstallationScript">Installation Script</label>
								<textarea rows="10" className="form-control" id="InstallationScript" placeholder="Installation Script" valueLink={this.linkState('InstallationScript')} />
							</div>
							<div className="form-group">
								<label htmlFor="UninstallationScript">Uninstallation Script</label>
								<textarea rows="10" className="form-control" id="UninstallationScript" placeholder="Uninstallation Script" valueLink={this.linkState('UninstallationScript')} />
							</div>
						</form>
					</TabPane>
					<TabPane eventKey={3} tab='Configurations'>
						<div className="form-group">
							<label htmlFor="ConfigurationFile">Configuration File</label>
							<input type="text" className="form-control" id="ConfigurationFile" placeholder="Configuration File" valueLink={this.linkState('ConfigurationFile')} />
						</div>
						<div className="form-group">
							<label htmlFor="ConfigTransform">Configuration Trasformation</label>
							<textarea rows="10" className="form-control" id="ConfigTransform" placeholder="Config Transform" valueLink={this.linkState('ConfigTransform')} />
						</div>
					</TabPane>
		  	</TabbedArea>

				<button className="btn btn-primary pull-right" onClick={this.save}>Save</button>
			</div>
		);
	}
});

module.exports = EditPage;
