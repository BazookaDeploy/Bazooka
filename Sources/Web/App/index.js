import React from "react";
import Router from "react-router";
var { Route, DefaultRoute, RouteHandler, Link } = Router;

import App                from "./App";
import HomePage           from "./Home/Homepage";
import AppPage            from "./Applications/AppPage";
import EnviromentPage     from "./Enviroments/EnviromentPage";
import AgentPage          from "./Agents/Agentpage";

import EnvsPage           from "./Envs/EnviromentPage";
import DeployUnitEditPage from "./Tasks/DeployTasks/DeployUnitEditPage";
import DeploymentsPage    from "./Deployments/DeploymentsPage";
import DeploymentPage     from "./Deployment/DeploymentsPage";
import GroupsPage         from "./Groups/GroupsPage";
import GroupPage          from "./Group/GroupsPage";
import StatsPage          from "./Stats/StatsPage";
import TasksPage          from "./Tasks/TasksPage";
import MailTasksEditPAge  from "./Tasks/MailTask/EditPage";
import LocalScriptTasksEditPAge  from "./Tasks/LocalScriptTask/EditPage";
import RemoteScriptTasksEditPage  from "./Tasks/RemoteScriptTask/EditPage";
import DatabaseTasksEditPage  from "./Tasks/DatabaseTasks/EditPage";


var routes = (
  <Route handler={App}>
    <DefaultRoute name="home" handler={HomePage} />
    <Route name="apps" path="apps" handler={AppPage}/>
    <Route name="envs" path="envs/" handler={EnvsPage} />
    <Route name="enviroments" path="enviroments/:applicationName/:applicationId" handler={EnviromentPage} />
    <Route name="agent"       path="agent/:id" handler={AgentPage} />

    <Route name="tasks" path="tasks/:applicationName/:enviroment/:enviromentId" handler={TasksPage} />
    <Route name="deployunitedit" path="deployunits/:applicationName/:enviroment/:deployUnitName/edit/:deployUnitId" handler={DeployUnitEditPage} />
    <Route name="mailtaskedit" path="mailtask/:applicationName/:enviroment/:mailTaskName/edit/:taskId" handler={MailTasksEditPAge} />
    <Route name="localscripttaskedit" path="localscripttask/:applicationName/:enviroment/:taskName/edit/:taskId" handler={LocalScriptTasksEditPAge} />
    <Route name="remotescripttaskedit" path="remotescripttask/:applicationName/:enviroment/:taskName/edit/:taskId" handler={RemoteScriptTasksEditPage} />
  <Route name="databasetaskedit" path="databasetask/:applicationName/:enviroment/:taskName/edit/:taskId" handler={DatabaseTasksEditPage} />


    <Route name="deployments" path="deployments" handler={DeploymentsPage} />
    <Route name="deployment" path="deployment/:Id" handler={DeploymentPage} />
    <Route name="groups" path="groups" handler={GroupsPage} />
    <Route name="group" path="group/:name" handler={GroupPage} />
    <Route name="stats" path="statistics" handler={StatsPage} />

  </Route>
);

Router.run(routes, function (Handler) {
    React.render(<Handler/>, document.getElementById('base'));
});
