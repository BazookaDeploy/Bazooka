import React from "react";
import classname from "classnames";

var Head = React.createClass({
    return <thead classname="table__head">{this.props.children}</thead>
});

var Body = React.createClass({
    return <tbody classname="table__body">{this.props.children}</tbody>
});

var Table = React.createClass({
    return <table classname="table">{this.props.children}</table>
});

Table.Head = Head;
Table.Body = body;

export default Table;