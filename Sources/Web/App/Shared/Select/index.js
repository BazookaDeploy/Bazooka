import React from "react";
import classname from "classnames";

var Option = React.createClass({
    render(){
        return <option {...this.props}>{this.props.children}</option>
    }
});

var Select = React.createClass({
    value(){
        return this.refs.input.value;
    },

    render(){
        return (<div className="select">
            {this.props.title && <div className="select__title">{this.props.title}</div>}
            <select ref="input" className="select__input" {...this.props}>{this.props.children}</select>
        </div>);
    }
});

    Select.Option = Option;

export default Select;