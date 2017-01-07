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


import ConfigurationPage from "./Configuration/ConfigurationPage";
import ConfigPage from "./Configuration/ConfigPage";
import GroupsPage from "./Configuration/GroupsPage";
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
              <Route path="DeployTask/:taskId" />
              <Route path="DatabaseTask/:taskId" />
              <Route path="MailTask/:taskId" />
              <Route path="LocalScriptTask/:taskId" />
              <Route path="RemoteScriptTask/:taskId" />
            </Route>
            <Route path="Enviroment/:enviromentId/Users" component={AllowedUsersPage} />
        </Route> 
      </Route>
      <Route path="Configuration" component={ConfigurationPage}>
        <IndexRoute component={ConfigPage} />
        <Route path="Groups" component={GroupsPage} />
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