import React from "react";
import Header from "../Shared/Header";
import Grid from "../Shared/Grid";
import {Link} from "react-router";


var ConfigPage = React.createClass({
    render:function(){
        return (<div>
            {window.ActiveDirectory == "true" ?
            <span>The system is currently using Acctive Directory authentication</span>:
            <span>The system is currently using Form Authentication</span>
            }
        </div>)
        }
})

export default ConfigPage;