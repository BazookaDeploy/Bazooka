import React from "react";
import Header from "../Shared/Header";
import Grid from "../Shared/Grid";
import Actions from "./Actions";
import Button from "../Shared/Button";
import Panel from "../Shared/Panel/Panel";
import FormattedDate from "../Shared/Utils/FormattedDate";
import FormattedTime from "../Shared/Utils/FormattedTime";
import { connect } from 'react-redux';


var NewTaskPage = React.createClass({
    getInitialState: function () {
        return {
            enviromentId: null,
            agentId: null,
            agents:[],
            tasktemplateId: null,
            taskTemplates:[],
            parameters: []
        };
    },

    render: function () {
        return <div>
            <Header>
                New Maintenance Task
            </Header>

            <Grid fluid>
                <Grid.Row>
                    <Grid.Col md={4}>

                    </Grid.Col>

                    <Grid.Col md={4}>

                    </Grid.Col>

                    <Grid.Col md={4}>

                    </Grid.Col>
                </Grid.Row>
            </Grid>
        </div>
    }
});


var mapStoreToProps = function (store) {
    return {
        enviroments: store.enviroments || []
    }
};


NewTaskPage = connect(mapStoreToProps, null)(NewTaskPage);


export default NewTaskPage;