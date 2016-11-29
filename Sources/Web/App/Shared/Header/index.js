import React from "react";

var Header = React.createClass({
    render(){
        return <div className="header">
            {this.props.children}
            <div className="header__actions">{this.props.actions}</div>
        </div>;
    }
});

export default Header;