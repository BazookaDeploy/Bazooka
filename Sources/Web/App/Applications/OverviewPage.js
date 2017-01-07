import React from "react";
import Button from "../Shared/Button";

var OverviewPage = React.createClass({
    getInitialState(){
        return {url : null}
    },

    render:function(){

        return (<div>
            <h3>Overview</h3>
            From this page you can configure the application by setting permissions for users and groups in the <b>Permissions</b> tab or configure the deploy process for every enviroment
            
        </div>)
        }
});


export default OverviewPage;