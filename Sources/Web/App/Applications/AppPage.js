var React = require("react");
var Actions = require("../Actions/ApplicationActionsCreator");
var Store = require("../Stores/ApplicationStore");
var Modal = require("react-bootstrap/Modal");
var ModalTrigger = require("react-bootstrap/ModalTrigger");


var CreateDialog = React.createClass({
  create:function(){
    var name = this.refs.name.getDOMNode().value;

    if(name.length!==0){
      Actions.createApplication(name).then(x => {
        this.props.onRequestHide();
      });
    }
  },

  render:function(){
    return(
      <Modal {...this.props} title="Create new application">
      <div className="modal-body">
        <form role="form">
          <div className="form-group">
            <label htmlFor="name">Application name</label>
            <input type="text" className="form-control" id="name" placeholder="Name" ref="name" />
          </div>
        </form>
      </div>
      <div className="modal-footer">
        <button className="btn btn-primary" onClick={this.create}>Create</button>
        <button className="btn" onClick={this.props.onRequestHide}>Close</button>
      </div>
    </Modal>);
  }
})

var AppLine = React.createClass({
  render: function(){
    return(<div><b>{this.props.Application.Name}</b></div>)
  }
});

var AppPage = React.createClass({
  getInitialState: function() {
    return {
      apps : Store.getAll()
    };
  },

  componentDidMount: function() {
    Store.addChangeListener(this._onChange);
    Actions.updateApplications();
  },

  componentWillUnmount: function() {
    Store.removeChangeListener(this._onChange);
  },

  render: function () {
    var apps = this.state.apps.map(function(a){return(<AppLine Application={a}></AppLine>)});

    return (<div>Welcome to the App page
      <ModalTrigger modal={<CreateDialog />}>
        <button className='btn'>Crea</button>
      </ModalTrigger>


      <h3>Lista</h3>
      {apps}
    </div>);
  },

  _onChange: function(){
    this.setState({
      apps : Store.getAll()
    });
  }
});

module.exports = AppPage;
