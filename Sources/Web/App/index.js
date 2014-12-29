var React = require("react");
var Router = require("react-router");
var { Route, DefaultRoute, RouteHandler, Link } = Router;

var App = require("./App");
var HomePage = require("./Home/Homepage");
var AppPage = require("./Applications/AppPage");
var EnviromentPage = require("./Enviroments/EnviromentPage");

var routes = (
  <Route handler={App}>
    <DefaultRoute name="home" handler={HomePage} />
    <Route name="apps" path="apps" handler={AppPage}/>
    <Route name="enviroments" path="enviroments/:applicationId" handler={EnviromentPage} />
  </Route>
);

Router.run(routes, function (Handler) {
    React.render(<Handler/>, document.getElementById('base'));
});
