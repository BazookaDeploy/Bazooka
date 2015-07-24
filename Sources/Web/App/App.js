import React  from "react";
import Router  from 'react-router';
import DropdownButton  from "react-bootstrap/lib/DropdownButton";
import NavItemLink  from "react-router-bootstrap/lib/NavItemLink";
import MenuItemLink from "react-router-bootstrap/lib/MenuItemLink";

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
              <NavItemLink to="home">Home</NavItemLink>
              <NavItemLink to="deployments">Deployments</NavItemLink>
              <NavItemLink to="stats">Statistics</NavItemLink>
            </ul>
            <ul className="nav navbar-nav navbar-right">
              <li><a>Hello {window.Profile}</a></li>
              {window.Administator == "True" ?
                <DropdownButton title='Admin'  navItem={true}>
                  <MenuItemLink to="apps">Applications</MenuItemLink>
                  <MenuItemLink to="groups">Groups</MenuItemLink>
                  <MenuItemLink to="agents">Agents</MenuItemLink>
                </DropdownButton>
                : ""
                }
            </ul>
          </div>
        </div>
      </nav>
      <div className="container">
        <RouteHandler />
      </div></div>);
  }
});

export default App;
