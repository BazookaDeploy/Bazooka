import React from "react";
import classname from "classnames";

var Header = React.createClass({
    render(){
        return <div classname="header">
            {this.props.children}
            <div classname="header__actions">{this.props.actions}</div>
        </div>;
    }
});

export default Header;