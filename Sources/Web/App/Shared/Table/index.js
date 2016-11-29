import React from "react";
import classname from "classnames";

var Head = React.createClass({
    render() {
        return <thead className="table__head">{this.props.children}</thead>
    }
});

var Body = React.createClass({
    render() {
        return <tbody className="table__body">{this.props.children}</tbody>
    }
});

var Table = React.createClass({
    render() {
        var classes = classname("table",{"table--hover": this.props.hover});

        return <table className={classes}>{this.props.children}</table>
    }
});

Table.Head = Head;
Table.Body = Body;

export default Table;