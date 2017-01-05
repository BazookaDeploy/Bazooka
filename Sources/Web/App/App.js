import React from "react";
import { connect } from 'react-redux'
import Requests from "./Requests";
import Actions from "./Actions";
import { Link, IndexLink } from 'react-router';

var App = React.createClass({
    componentDidMount: function(){
        this.props.loadApplications();
        this.props.loadUsers();
        this.props.loadGroups();
        this.props.loadEnviroments();

    },

    render:function(){
        return (<div className="application">
            <div className="application__header">
                <div className="application__header__logo">Bazooka</div>
                <div className="application__header__links"> 
                     <IndexLink className="application__header__link" activeClassName="active" to="/">Dashboard</IndexLink>                   
                     <Link className="application__header__link" activeClassName="active" to="/Deployments">Deployments</Link>
                     <Link className="application__header__link" activeClassName="active" to="/Enviroments">Enviroments</Link>
                     <Link className="application__header__link" activeClassName="active" to="/Applications">Applications</Link> 
                     <Link className="application__header__link" activeClassName="active" to="/Statistics">Statistics</Link>
                     <Link className="application__header__link" activeClassName="active" to="/Configuration">Configuration</Link>
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
        },

        loadUsers:function(){
            Requests.updateUsers().then(x => {
                dispatch({type: Actions.AddUsers, users: x});
            });
        },

        loadGroups:function(){
            Requests.updateGroups().then(x => {
                dispatch({type: Actions.AddGroups, groups: x});
            });
        },

        loadEnviroments:function(){
            Requests.updateEnviroments().then(x => {
                dispatch({type: Actions.AddEnviroments, enviroments: x});
            });
        }
    };
};

App = connect(mapStoreToProps,mapDispatchToProps)(App);

export default App;