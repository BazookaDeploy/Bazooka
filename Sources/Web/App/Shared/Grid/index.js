import React from "react";
import classname from "classnames";

var Col = React.createClass({

    render() {
        var obj = {};
        if (this.props.xs) { obj["col-xs-" + this.props.xs] = true }
        if (this.props.xsOffset) { obj["offset-xs-" + this.props.xsOffset] = true }
        if (this.props.xsPush) { obj["push-xs-" + this.props.xsPush] = true }
        if (this.props.xsPull) { obj["pull-xs-" + this.props.xsPull] = true }
        if (this.props.md) { obj["col-md-" + this.props.md] = true }
        if (this.props.mdOffset) { obj["offset-md-" + this.props.mdOffset] = true }
        if (this.props.mdPush) { obj["push-md-" + this.props.mdPush] = true }
        if (this.props.mdPull) { obj["pull-md-" + this.props.mdPull] = true }
        if (this.props.lg) { obj["col-lg-" + this.props.lg] = true }
        if (this.props.lgOffset) { obj["offset-lg-" + this.props.lgOffset] = true }
        if (this.props.lgPush) { obj["push-lg-" + this.props.lgPush] = true }
        if (this.props.lgPull) { obj["pull-lg-" + this.props.lgPull] = true }
        if (this.props.xl) { obj["col-xl-" + this.props.xl] = true }
        if (this.props.xlOffset) { obj["offset-xl-" + this.props.xlOffset] = true }
        if (this.props.xlPush) { obj["push-xl-" + this.props.xlPush] = true }
        if (this.props.xlPull) { obj["pull-xl-" + this.props.xlPull] = true }

        var classes = classname(obj, this.props.className);
        return <div className={classes}>{this.props.children}</div>
    }
});

var Row = React.createClass({
    propTypes: {
        className: React.PropTypes.string
    },

    render() {
        var classes = classname("row", this.props.className);
        return <div className={classes}>{this.props.children}</div>
    }
});

var Grid = React.createClass({
    propTypes: {
        className: React.PropTypes.string,
        fluid: React.PropTypes.any
    },

    render() {
        var classes = classname("container" + (this.props.fluid ? "-fluid" : ""), this.props.className);
        return <div className={classes}>{this.props.children}</div>
    }
});

Grid.Col = Col;
Grid.Row = Row;

export default Grid;