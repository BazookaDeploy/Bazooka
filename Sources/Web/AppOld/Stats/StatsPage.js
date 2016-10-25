import React  from "react";
import Actions from "./ActionsCreator";
import TabbedArea from "react-bootstrap/lib/TabbedArea";
import TabPane from "react-bootstrap/lib/TabPane";
import PieChart from "react-d3-components/lib/PieChart";
import BarChart from "react-d3-components/lib/BarChart";


var StatsPage = React.createClass({
    getInitialState: function() {
      return {
        deploys : [],
        users: []
      };
    },

    componentDidMount: function() {
      this.updateFilters();
    },

    updateFilters:function(){
         Actions.getStatistics(this.refs.filter.getDOMNode().value).then(x => {
           this.setState({
             deploys:x.Deploys,
             users:x.Users
           });
         });
    },


    render: function () {
      var deploys = this.state.deploys.map(x => { return {label : x.app, values: x.envs.map(z => {return {x:z.env, y:z.count}})}});
      var envs = this.state.deploys.map(x => (<span>{x.app}, </span>));
      var vals = this.state.users.map(z => { return {x: z.UserName + "/" + z.Count, y: z.Count}});
        return (
          <div>
            <h2 style={{display:"inline"}}>Deployment statistics for: </h2>
              <select ref="filter" onChange={this.updateFilters}>
                              <option>Today</option>
                              <option>Yesterday</option>
                              <option>Last week</option>
                              <option>Last month</option>
                              <option>Ever</option>
              </select>

            <br />
            <br />


              <TabbedArea defaultActiveKey={1}>
      		    	<TabPane eventKey={1} tab='Applications'>
  <br />
        Legend: {envs}
  <br />
        {deploys.length==0 ? <span>No data to render</span>:
          <BarChart
                    data={deploys}
                    width={800}
                    height={800}
                    margin={{top: 10, bottom: 50, left: 50, right: 10}}/>
                }
          </TabPane>
            	<TabPane eventKey={2} tab='Users'>
            <br />

              <PieChart
                data={{ label: "user", values: vals} }
                width={800}
                height={800}
                margin={{top: 10, bottom: 10, left: 100, right: 100}}
                />

            </TabPane>
          </TabbedArea>
          </div>);
    }
});


module.exports = StatsPage;
