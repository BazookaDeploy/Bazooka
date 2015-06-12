var React = require("react");
var LinkedState = require("react/lib/LinkedStateMixin");
var Router = require('react-router');
var Actions = require("./ActionsCreator");
var Store = require("./Store");
var Modal = require("react-bootstrap/Modal");
var ModalTrigger = require("react-bootstrap/ModalTrigger");
var { Route, DefaultRoute, RouteHandler, Link } = Router;

var CreateDialog = React.createClass({
  mixins: [LinkedState],

  getInitialState: function() {
    if(this.props.Env!=null){
      return {
        name:this.props.Env.Name,
        machine:this.props.Env.Machine,
        packageName: this.props.Env.PackageName,
        directory:this.props.Env.Directory,
        repository:this.props.Env.Repository,
        key:"",
        value:"",
        Encrypted:false,
        params : this.props.Env.Parameters
      };
    }

    return {
      name:"",
      machine:"",
      packageName: "",
      directory:"",
      repository:"",
      key:"",
      value:"",
      Encrypted:false,
      params : []
    };
  },

  /**
   * Creates the new deployUnit and then closes the dialog if all
   * parameters are set
   */
  create:function(){
    if(this.state.name.length != 0 &&
      this.state.machine.length != 0 &&
      this.state.packageName.length != 0 &&
      this.state.repository.length != 0 &&
      this.state.directory.length != 0){

      if(this.props.Env!=null){
        Actions.modifyDeployUnit(
          this.props.Env.Id,
          this.props.Enviroment,
          this.state.name,
          this.state.machine,
          this.state.packageName,
          this.state.directory,
          this.state.repository,
          this.state.params)
               .then(x => this.props.onRequestHide());
               return;
      }

      Actions.createDeployUnit(
        this.props.Enviroment,
        this.state.name,
        this.state.machine,
        this.state.packageName,
        this.state.directory,
        this.state.repository,
        this.state.params)
             .then(x => this.props.onRequestHide());
    }
    return false;
  },

  /**
   * Adds a new parameter to the list if key and value are set
   * an there isn't already another parameter with the same key
   */
  addParameter : function(){
    if(this.state.key.length!=0 &&
      this.state.value.length != 0 &&
       !this.state.params.some(x => x.Key == this.state.key)){
      this.setState({
        params: this.state.params.concat({
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
    for(i = 0; i < this.state.params.length; i++){
      if(this.state.params[i].Name==key){
        index=i;
        break;
      }
    }
    this.state.params.splice(index,1);
    this.setState({
      params: this.state.params
    });
  },


  testAgent : function(){
    Actions.testAgent(this.state.machine).then(x => {
      alert("Agent responding");
    }).fail(x =>{
        alert("Agent not responding");
    });
    return false;
  },

  render:function(){
    return(
      <Modal {...this.props} backdrop="static" title="Create new deploy unit">
      <div className="modal-body">
        <form role="form" onSubmit={this.create}>
          <div className="form-group">
            <label htmlFor="Name">Name</label>
            <input type="text" className="form-control" id="Name" placeholder="Name" autoFocus valueLink={this.linkState('name')} />
          </div>
          <div className="form-group">
            <label htmlFor="Machine">Machine</label>
              <div className="input-group">
                <input type="text" className="form-control" id="Machine" placeholder="Machine" valueLink={this.linkState('machine')} />
                <span className="input-group-btn">
                  <button className="btn btn-primary" onClick={this.testAgent} >Test</button>
                </span>
              </div>


          </div>
          <div className="form-group">
            <label htmlFor="PackageName">PackageName</label>
            <input type="text" className="form-control" id="PackageName" placeholder="PackageName" valueLink={this.linkState('packageName')} />
          </div>
          <div className="form-group">
            <label htmlFor="Directory">Directory</label>
            <input type="text" className="form-control" id="Directory" placeholder="Directory" valueLink={this.linkState('directory')} />
          </div>
          <div className="form-group">
          <label htmlFor="Repository">Repository</label>
          <input type="text" className="form-control" id="Repository" placeholder="Repository" valueLink={this.linkState('repository')} />
          </div>
        </form>
        <h5>Additional Params</h5>
          <ul>
            {this.state.params.map(a => (<li>{a.Name} = {a.Encrypted ? "***********" : a.Value} <button className="btn btn-xs btn-danger" onClick={this.remove.bind(this,a.Key)}><i className="glyphicon glyphicon-trash"></i></button></li>))}
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
            <label htmlFor="Encrypted">Encrypted</label><br />
            <input type="checkbox" className="" id="Encrypted" checkedLink={this.linkState('Encrypted')} />
          </div>
          <div className="col-md-4">
            <br />
            <button className="btn btn-primary" onClick={this.addParameter} >Add Parameter</button>
          </div>
        </div>
      </div>
      <div className="modal-footer">
        <button className="btn btn-primary" onClick={this.create}>Create</button>
        <button className="btn" onClick={this.props.onRequestHide}>Close</button>
      </div>
      </Modal>);
    }
  });

  var DeployUnitsPage = React.createClass({
    mixins: [Router.State],
    getInitialState: function() {
      return {
        envs : Store.getAll(),
        users : [],
        groups:[],
        allUsers:[],
        allGroups:[]
      };
    },

    componentDidMount: function() {
      Store.addChangeListener(this._onChange);
      var id = this.getParams().enviromentId;
      Actions.updateDeployUnits(id);

      Actions.getUsers(id).then(x => {
        this.setState({users:x})
      })

      Actions.getAllUsers(id).then(x => {
        this.setState({allUsers:x})
      })

      Actions.getGroups(id).then(x => {
        this.setState({groups:x})
      })

      Actions.getAllGroups(id).then(x => {
        this.setState({allGroups:x})
      })
    },

    componentWillUnmount: function() {
      Store.removeChangeListener(this._onChange);
    },

    removeUser:function(id){
      debugger;
      Actions.removeUser(id).then(x => {
        Actions.getUsers(this.getParams().enviromentId).then(z => {
          this.setState({users:z})
        })
      })
    },

    removeGroup:function(id){
      debugger;
      Actions.removeGroups(id).then(x => {
        Actions.getGroups(this.getParams().enviromentId).then(z => {
          this.setState({groups:z})
        })
      })
    },

    addUser:function(){
      Actions.addUser(this.getParams().enviromentId,this.refs.user.getDOMNode().value).then(x => {
        Actions.getUsers(this.getParams().enviromentId).then(z => {
          this.setState({users:z})
        })
      })
    },

    addGroup:function(){
      Actions.addGroup(this.getParams().enviromentId,this.refs.group.getDOMNode().value).then(x => {
        Actions.getGroups(this.getParams().enviromentId).then(z => {
          this.setState({groups:z})
        })
      })
    },

    render: function () {
      var users = this.state.users.map(x => {
        return (<tr><td>{x.UserName}  <button className="btn btn-danger btn-xs pull-right" onClick={z => this.removeUser(x.Id)}>Remove</button></td></tr>);
      })

      var allUsers = this.state.allUsers.map(x => {
        return (<option value={x.Id}>{x.UserName}</option>);
      })

      var groups = this.state.groups.map(x => {
        return (<tr><td>{x.Name}  <button className="btn btn-danger btn-xs pull-right" onClick={z => {this.removeGroup(x.Id)}}>Remove</button></td></tr>);
      })

      var allGroups = this.state.allGroups.map(x => {
        return (<option value={x.Id}>{x.Name}</option>);
      })

      return(<div>
        <table className="table table-bordered table-striped">
          <thead><tr><th>Deploy Units
            <ModalTrigger modal={<CreateDialog Enviroment={this.getParams().enviromentId} />}>
              <button className='btn btn-xs btn-primary pull-right'>New</button>
            </ModalTrigger></th></tr></thead>
          <tbody>
            {this.state.envs.map(x => (
              <tr><td><Link to="deployunitedit" params={{deployUnitId: x.Id}}>{x.Name}</Link></td></tr>
              ))}
          </tbody>
        </table>

        <h3>Allowed Users</h3>

        <table className="table table-bordered table-hovered">
          <thead><tr><th>Users</th></tr></thead>
          <tbody>
            {users}
            <tr>
            <td>
              <div className="input-group">
                <select ref="user" className="form-control">
                  {allUsers}
                </select>
                <span className="input-group-btn">
                  <button  className="btn btn-default" onClick={this.addUser}>Add</button>
                </span>
              </div>
            </td>
            </tr>
          </tbody>
        </table>

        <h3>Allowed Groups</h3>

          <table className="table table-bordered table-hovered">
            <thead><tr><th>Groups</th></tr></thead>
            <tbody>
              {groups}
              <tr>
                <td>
                  <div className="input-group">
                    <select ref="group" className="form-control">
                      {allGroups}
                    </select>
                    <span className="input-group-btn">
                      <button  className="btn btn-default" onClick={this.addGroup}>Add</button>
                    </span>
                  </div>
                </td></tr>
            </tbody>
          </table>

        </div>)
      },

      _onChange: function(){
        this.setState({
          envs : Store.getAll()
        })
      }
    });

    module.exports = DeployUnitsPage;
