import React from "react";

var Tab = React.createClass({
    propTypes: {
        title: React.PropTypes.string.isRequired
    },

    render(){
        return (<div className="tabs__tab">
            {this.props.children}
        </div>);
    }
});

var Tabs = React.createClass({
    getInitialState() {
        return { index: 0 }
    },

    seleziona(indice) {
        this.setState({ index: indice });
    },

    render() {
        var tabTexts = this.props.children.map((x) => x.props.title);

        return (<div className="tabs">
            <div className="tabs__indexes">
                {tabTexts.map((x, i) => <div className={i == this.state.index ? "tabs__index tabs__index--selected" : "tabs__index"} onClick={() => this.seleziona(i) } key={i}>{x}</div>) }
            </div>
            {React.Children.toArray(this.props.children)[this.state.index]}
        </div>);
}
});

Tabs.Tab = Tab;
export default Tabs;