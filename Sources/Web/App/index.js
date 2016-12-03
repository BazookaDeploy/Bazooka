import React from 'react'
import { render } from 'react-dom'
import { Router, Route,IndexRoute, Link, hashHistory } from 'react-router'
import App from "./App"
import Homepage from "./Homepage/Homepage";
import ApplicationsPage from "./Applications/ApplicationsPage";
import ConfigurationPage from "./Configuration/ConfigurationPage";
import DeploymentsPage from "./Deployments/DeploymentsPage";
import DeploymentPage from "./Deployments/DeploymentPage";
import EnviromentsPage from "./Enviroments/EnviromentsPage";
import StatisticsPage from "./Statistics/StatisticsPage";
import { Provider } from 'react-redux';
import store from "./Store";


var route = <Provider store={store} >
    <Router history={hashHistory}>
    <Route path="/" component={App}>
      <IndexRoute component={Homepage}/>
      <Route path="Applications" component={ApplicationsPage} />
      <Route path="Configurations" component={ConfigurationPage} />
      <Route path="Enviroments" component={EnviromentsPage} />
      <Route path="Deployments">
        <IndexRoute component={DeploymentsPage}/>
        <Route path=":id" component={DeploymentPage} />
      </Route>     
      <Route path="Statistics" component={StatisticsPage} />
    </Route>
  </Router>
      </Provider>;

      render(route, document.getElementById('root'));