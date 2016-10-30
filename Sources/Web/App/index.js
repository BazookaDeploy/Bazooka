import React from 'react'
import { render } from 'react-dom'
import { Router, Route,IndexRoute, Link, browserHistory } from 'react-router'
import App from "./App"
import Homepage from "./Homepage/Homepage"
import { Provider } from 'react-redux';
import store from "./Store";


var route = <Provider store={store} >
    <Router history={browserHistory}>
    <Route path="/" component={App}>
      <IndexRoute component={Homepage}/>
    </Route>
  </Router>
      </Provider>;

      render(route, document.getElementById('root'));