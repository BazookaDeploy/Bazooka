import React from "react";
import { connect } from 'react-redux'
import Requests from "./Requests";
import Actions from "./Actions";


var App = React.createClass({
    componentDidMount: function(){
        this.props.loadApplications();
    },

    render:function(){
        return (<div className="application">
            <div className="application__header">
                Bazooka
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
                dispatch({type: Actions.AddApplications, applications: x})
            })
        }
    }
}

var App = connect(mapStoreToProps,mapDispatchToProps)(App);

export default App;