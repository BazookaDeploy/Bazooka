import React from "react";
import Header from "../Shared/Header";
import Grid from "../Shared/Grid";
import {Link, IndexLink} from "react-router";


var ConfigurationPage = React.createClass({
    render:function(){
        return (<div>
            <Header>
                Configuration
            </Header>
            <Grid fluid>
                <Grid.Row>
                    <Grid.Col md={2}>
                        <div className="configurationLinks">
                            <IndexLink className="configurationLinks__link" activeClassName="active" to="/Configuration">Overview</IndexLink>                   
                            <Link className="configurationLinks__link" activeClassName="active" to="/Configuration/Groups">Groups</Link>   
                            <Link className="configurationLinks__link" activeClassName="active" to="/Configuration/ApplicationGroups">Application Groups</Link>             
                            <Link className="configurationLinks__link" activeClassName="active" to="/Configuration/TemplatedTasks">Task templates</Link>
                        </div>
                    </Grid.Col>
                    <Grid.Col md={10}>
                        <div className="configurationContent">
                            {this.props.children}
                        </div>
                    </Grid.Col>
                </Grid.Row>
            </Grid>
        </div>)
        }
})

export default ConfigurationPage;