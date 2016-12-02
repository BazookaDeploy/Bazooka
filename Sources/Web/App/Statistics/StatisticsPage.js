import React from "react"
import Header from "../Shared/Header";
import Select from "../Shared/Select";
import Grid from "../Shared/Grid";
import Tabs from "../Shared/Tabs";
import Table from "../Shared/Table";
import Actions from "./Actions";
import { connect } from 'react-redux';

var randomColor = (function(){
  var golden_ratio_conjugate = 0.618033988749895;
  var h = Math.random();

  var hslToRgb = function (h, s, l){
      var r, g, b;

      if(s == 0){
          r = g = b = l; // achromatic
      }else{
          function hue2rgb(p, q, t){
              if(t < 0) t += 1;
              if(t > 1) t -= 1;
              if(t < 1/6) return p + (q - p) * 6 * t;
              if(t < 1/2) return q;
              if(t < 2/3) return p + (q - p) * (2/3 - t) * 6;
              return p;
          }

          var q = l < 0.5 ? l * (1 + s) : l + s - l * s;
          var p = 2 * l - q;
          r = hue2rgb(p, q, h + 1/3);
          g = hue2rgb(p, q, h);
          b = hue2rgb(p, q, h - 1/3);
      }

      return '#'+Math.round(r * 255).toString(16)+Math.round(g * 255).toString(16)+Math.round(b * 255).toString(16);
  };
  
  return function(){
    h += golden_ratio_conjugate;
    h %= 1;
    return hslToRgb(h, 0.5, 0.60);
  };
})();

var colors =new Array(100);
var i =0;
for(i=0;i<100;i++){
    colors[i] = randomColor();
}


var ApplicationStatistics = React.createClass({
    render(){
        var totalDeploys = this.props.apps.map((x,i) => x.envs.map(z => z.Count).reduce((a,b) => a+b,0)).reduce((a,b) => a+b,0);
        var maxDeploys = 100;
        if(this.props.apps.length > 0){
            maxDeploys = this.props.apps[0].envs.map(z => z.Count).reduce((a,b) => a+b,0)
        }

        return (<div>

        <h4>Legend: {this.props.enviroments.map(x => <span className="statsLegend" style={{backgroundColor: colors[x.Id]}}>{x.Name}</span>)}</h4>

        <Table>
            <Table.Head>
                <tr>
                    <th className="table__cell--small">Application</th>
                    <th>Deploys</th>
                    <th className="table__cell--small">Total ({totalDeploys})</th>
                </tr>
            </Table.Head>

            <Table.Body>
                {
                    this.props.apps.map((x,i) => <tr>
                        <td className="table__cell--small text--align--right">{x.app}</td>
                        <td><div style={{width: ((x.envs.map(z => z.Count).reduce((a,b) => a+b,0) / maxDeploys)*100)+"%" }}>{
                            x.envs.map(z => <div className="deploys" style={{ backgroundColor: colors[z.EnviromentId], width: ((z.Count/ x.envs.map(zz => zz.Count).reduce((a,b) => a+b,0)) * 100) + "%"}}>{z.Count}</div>)
                        }</div></td>
                        <td  className="table__cell--small">{x.envs.map(z => z.Count).reduce((a,b) => a+b,0)}</td>
                    </tr>) 
                }
            </Table.Body>

        </Table></div>);
    }
});


var mapStoreToProps = function(store){
    return {
        enviroments: store.enviroments || []
    }
};

ApplicationStatistics = connect(mapStoreToProps,null)(ApplicationStatistics);


var UserStatistics = React.createClass({
    render(){
        return <h2>User stats </h2>;
    }
});

var StatisticsPage = React.createClass({
    getInitialState: function () {
        return {
            deploys: [],
            users: [],
            currentFilter: "Today"
        };
    },

    componentDidMount: function () {
        this.update();
    },

    update: function () {
        Actions.getStatistics(this.state.currentFilter).then(x => {
            this.setState({
                deploys: x.Deploys,
                users: x.Users
            });
        });
    },


    render: function () {
        return (<div>
            <Header actions={<Select onChange={(e) => this.setState({ currentFilter: e.target.value }, this.update) }>
                <Select.Option>Today</Select.Option>
                <Select.Option>Yesterday</Select.Option>
                <Select.Option>Last week</Select.Option>
                <Select.Option>Last month</Select.Option>
                <Select.Option>Ever</Select.Option>
            </Select>}>

                Statistics
            </Header>

            <Grid fluid>
                <Grid.Row>
                    <Grid.Col md="12">
                        <Tabs>
                            <Tabs.Tab title="Applications">
                                <ApplicationStatistics apps={this.state.deploys} />
                            </Tabs.Tab>

                            <Tabs.Tab title="Users">
                                <UserStatistics users={this.state.users} />
                            </Tabs.Tab>
                        </Tabs>
                    </Grid.Col>
                </Grid.Row>

            </Grid>


        </div>);
    }
});

export default StatisticsPage;