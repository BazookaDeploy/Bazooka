import React  from "react";
import Actions from "./ActionsCreator";

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
      var deploys = this.state.deploys.map(x => (<tr><td>{x.Name} - {x.Configuration}</td><td className="text-right">{x.Count}</td></tr>));
      var users = this.state.users.map(x => (<tr><td>{x.UserName}</td><td className="text-right">{x.Count}</td></tr>));
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
            <br />

            <table className="table table-bordered table-striped" >
              <thead>
                <tr><th>Application/Enviroment</th><th className="text-right">Deploys</th></tr>
              </thead>
              <tbody>
                {deploys}
              </tbody>
            </table>

            <br />

              <table className="table table-bordered table-striped">
                <thead>
                  <tr><th>User</th><th className="text-right">Deploys</th></tr>
                </thead>
                <tbody>
                  {users}
                </tbody>
              </table>
          </div>);
    }
});


module.exports = StatsPage;
