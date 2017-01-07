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
        

        var arr=[];
        this.props.enviroments.map(x => {
            arr.push(<div  className="configurationLinks__link--section">Enviroment: {x.Name}</div>);
            arr.push(<Link className="configurationLinks__link configurationLinks__link--subsection" activeClassName="active" to={"/Applications/"+this.props.params.id+"/Enviroment/"+x.Id+"/Tasks"}>Tasks</Link>);
            arr.push(<Link className="configurationLinks__link configurationLinks__link--subsection" activeClassName="active" to={"/Applications/"+this.props.params.id+"/Enviroment/"+x.Id+ "/Users"}>Users</Link>);          
        });

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
                            {arr}
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