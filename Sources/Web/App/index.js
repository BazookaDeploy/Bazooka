import React from 'react'
import { render } from 'react-dom'
import { Router, Route,IndexRoute, Link, hashHistory } from 'react-router'
import App from "./App"
import Homepage from "./Homepage/Homepage";
import ApplicationsPage from "./Applications/ApplicationsPage";
import ApplicationPage from "./Applications/ApplicationPage";
import PermissionsPage from "./Applications/PermissionsPage";
import OverviewPage from "./Applications/OverviewPage";
import TasksPage from "./Applications/TasksPage";
import AllowedUsersPage from "./Applications/AllowedUserPage";

import DatabaseTaskEditPage from "./Applications/Tasks/DatabaseTasks/EditPage";
import DeployTaskEditPage from "./Applications/Tasks/DeployTasks/DeployUnitEditPage";
import LocalScriptTaskEditPage from "./Applications/Tasks/LocalScriptTask/EditPage";
import MailTaskEditPage from "./Applications/Tasks/MailTask/EditPage";
import RemoteScriptTaskEditPage from "./Applications/Tasks/RemoteScriptTask/EditPage";


import ConfigurationPage from "./Configuration/ConfigurationPage";
import ConfigPage from "./Configuration/ConfigPage";
import TemplatedTasksPage from "./Configuration/TemplatedTasksPage";
import TemplatedTaskPage from "./Configuration/TemplatedTaskPage";
import GroupsPage from "./Configuration/GroupsPage";
import ApplicationGroupsPage from "./Configuration/ApplicationGroupsPage";
import DeploymentsPage from "./Deployments/DeploymentsPage";
import DeploymentPage from "./Deployments/DeploymentPage";
import EnviromentsPage from "./Enviroments/EnviromentsPage";
import AgentPage from "./Enviroments/AgentPage";
import StatisticsPage from "./Statistics/StatisticsPage";
import { Provider } from 'react-redux';
import store from "./Store";


var route = <Provider store={store} >
    <Router history={hashHistory}>
    <Route path="/" component={App}>
      <IndexRoute component={Homepage}/>
      <Route path="Applications">
        <IndexRoute component={ApplicationsPage} />
        <Route path=":id" component={ApplicationPage}>
            <IndexRoute component={OverviewPage} />
            <Route path="Permissions" component={PermissionsPage} />
            <Route path="Enviroment/:enviromentId/Tasks" component={TasksPage}>
              <Route path="DeployTask/:taskId" component={DeployTaskEditPage} />
              <Route path="DatabaseTask/:taskId" component={DatabaseTaskEditPage} />
              <Route path="MailTask/:taskId" component={MailTaskEditPage}/>
              <Route path="LocalScriptTask/:taskId" component={LocalScriptTaskEditPage}/>
              <Route path="RemoteScriptTask/:taskId" component={RemoteScriptTaskEditPage}/>
            </Route>
            <Route path="Enviroment/:enviromentId/Users" component={AllowedUsersPage} />
        </Route> 
      </Route>
      <Route path="Configuration" component={ConfigurationPage}>
        <IndexRoute component={ConfigPage} />
        <Route path="Groups" component={GroupsPage} />
        <Route path="ApplicationGroups" component={ApplicationGroupsPage} />
        <Route path="TemplatedTasks">
                    <IndexRoute component={TemplatedTasksPage} />
                    <Route path=":id" component={TemplatedTaskPage} />
        </Route>
      </Route>
      <Route path="Enviroments">
        <IndexRoute  component={EnviromentsPage} />
        <Route path="Agent/:id" component={AgentPage} />
      </Route>
      <Route path="Deployments">
        <IndexRoute component={DeploymentsPage}/>
        <Route path=":id" component={DeploymentPage} />
      </Route>     
      <Route path="Statistics" component={StatisticsPage} />
    </Route>
  </Router>
      </Provider>;

      render(route, document.getElementById('root'));