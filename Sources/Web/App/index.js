import React from "react";
import Router from "react-router";
var { Route, DefaultRoute, RouteHandler, Link } = Router;

import App                from "./App";
import HomePage           from "./Home/Homepage";
import AppPage            from "./Applications/AppPage";
import EnviromentPage     from "./Enviroments/EnviromentPage";
import DeployUnitsPage    from "./DeployUnits/DeployUnitsPage";
import DeployUnitEditPage from "./DeployUnits/DeployUnitEditPage";
import DeploymentsPage    from "./Deployments/DeploymentsPage";
import DeploymentPage     from "./Deployment/DeploymentsPage";
import GroupsPage         from "./Groups/GroupsPage";
import GroupPage          from "./Group/GroupsPage";

var routes = (
  <Route handler={App}>
    <DefaultRoute name="home" handler={HomePage} />
    <Route name="apps" path="apps" handler={AppPage}/>
    <Route name="enviroments" path="enviroments/:applicationName/:applicationId" handler={EnviromentPage} />
    <Route name="deployunits" path="deployunits/:applicationName/:enviroment/:enviromentId" handler={DeployUnitsPage} />
    <Route name="deployunitedit" path="deployunits/:applicationName/:enviroment/:deployUnitName/edit/:deployUnitId" handler={DeployUnitEditPage} />
    <Route name="deployments" path="deployments" handler={DeploymentsPage} />
    <Route name="deployment" path="deployment/:Id" handler={DeploymentPage} />
    <Route name="groups" path="groups" handler={GroupsPage} />
    <Route name="group" path="group/:name" handler={GroupPage} />
  </Route>
);

Router.run(routes, function (Handler) {
    React.render(<Handler/>, document.getElementById('base'));
});
