import React from "react";
import Header from "../Shared/Header";
import Grid from "../Shared/Grid";
import {Link, IndexLink} from "react-router";
import {connect} from "react-redux";

var ApplicationPage = React.createClass({
    render:function(){
        var application = this.props.applications.filter(x => x.Id == this.props.params.id);
        if(application.length>0){
            application = application[0].Name;
        }else{
            application=[];
        }
        

        return (<div>
            <Header>
                Application: {application}
            </Header>
            <Grid fluid>
                <Grid.Row>
                    <Grid.Col md={2}>
                        <div className="configurationLinks">
                            <IndexLink className="configurationLinks__link" activeClassName="active" to={"/Applications/"+this.props.params.id}>Overview</IndexLink>                   
                            <Link className="configurationLinks__link" activeClassName="active" to={"/Applications/"+this.props.params.id+"/Permissions"}>Permissions</Link>                       
                            {this.props.enviroments.map(x => <Link className="configurationLinks__link" activeClassName="active" to={"/Applications/"+this.props.params.id+"/Enviroment/"+ x.Name + "/"+x.Id}>Enviroment: {x.Name}</Link>)}
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
});


var mapStoreToProps = function(store){
    return {
        applications: store.applications || [],
        enviroments: store.enviroments || []
    };
};

ApplicationPage = connect(mapStoreToProps,null)(ApplicationPage);


export default ApplicationPage;