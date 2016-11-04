import React from "react";
import { connect } from 'react-redux'
import Requests from "./Requests";
import Actions from "./Actions";
import { Link, IndexLink } from 'react-router';

var App = React.createClass({
    componentDidMount: function(){
        this.props.loadApplications();
    },

    render:function(){
        return (<div className="application">
            <div className="application__header">
                <div className="application__header__logo">Bazooka</div>
                <div className="application__header__links">
                     <IndexLink className="application__header__link" activeClassName="active" to="/">Dashboard</IndexLink>
                     <Link className="application__header__link" activeClassName="active" to="/Applications">Applications</Link>
                     <Link className="application__header__link" activeClassName="active" to="/Configurations">Configurations</Link>
                     <Link className="application__header__link" activeClassName="active" to="/Deployments">Deployments</Link>
                     <Link className="application__header__link" activeClassName="active" to="/Statistics">Statistics</Link>
                </div> 
            </div>

            <div className="application__body">
               {this.props.children}
            </div>
        </div>);
    }
});

var mapStoreToProps = function(store){
    return {
        applications: store.applications || []
    }
};

var mapDispatchToProps = function(dispatch){
    return {
        loadApplications:function(){
            Requests.updateApplications().then(x => {
                dispatch({type: Actions.AddApplications, applications: x});
            });
        }
    };
};

App = connect(mapStoreToProps,mapDispatchToProps)(App);

export default App;