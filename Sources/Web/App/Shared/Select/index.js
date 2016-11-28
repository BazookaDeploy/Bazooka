import React from "react";
import classname from "classnames";

var Option = React.createClass({
    render(){
        return <option {...this.props}>{this.props.children}</option>
    }
});

var Select = React.createClass({
    render(){
        return <div classname="select"><select {...this.props}>{this.props.children}</select></div>
    }
});

    Select.Option = Option;

export default Select;