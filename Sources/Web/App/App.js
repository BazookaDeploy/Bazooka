var React = require("react");
var Router = require('react-router');
var { Route, DefaultRoute, RouteHandler, Link } = Router;

var App = React.createClass({
  render:function(){
    return(<div><nav className="navbar navbar-default navbar-static-top" role="navigation">
        <div className="container">
          <div className="navbar-header">
            <a className="navbar-brand">Bazooka</a>
          </div>
          <div id="navbar" class="navbar-collapse collapse">
            <ul className="nav navbar-nav">
              <li><Link to="home">Home</Link></li>
              <li><Link to="apps">Applications</Link></li>
            </ul>
            <ul className="nav navbar-nav navbar-right">
              <li><a>Profile</a></li>
            </ul>
          </div>
        </div>
      </nav>
      <div className="container">
        <RouteHandler />
      </div></div>);
  }
});

module.exports = App;
