var React = require("react");
var Router = require("react-router");
var { Route, RouteHandler, Link } = Router;

var HomePage = require("./Home/Homepage");

var routes = (
  <Route handler={HomePage} path="/" />

);

Router.run(routes, function (Handler) {
    React.render(<Handler/>, document.getElementById('base'));
});

