import React from "react";
import classname from "classnames";

var Option = React.createClass({
    render(){
        return <option {...this.props}>{this.props.children}</option>
    }
});

var Select = React.createClass({
    render(){
        return (<div className="select">
            {this.props.title && <div className="select__title">{this.props.title}</div>}
            <select className="select__input" {...this.props}>{this.props.children}</select>
        </div>);
    }
});

    Select.Option = Option;

export default Select;