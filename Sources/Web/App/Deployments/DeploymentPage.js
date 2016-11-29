import React from "react";
import Header from "../Shared/Header";
import Grid from "../Shared/Grid";

var DeploymentPage = React.createClass({
    render: function () {
        return <div>
            <Header>

                Deployment
            </Header>

            <Grid fluid>
                <Grid.Row>
                    <Grid.Col md={12}>
                    </Grid.Col>
                </Grid.Row>
            </Grid>
        </div>
    }
});


export default DeploymentPage;