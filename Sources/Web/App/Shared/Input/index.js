import React from "react";
import classname from "classnames";

var Input = React.createClass({
    render(){
        return (<div className="input">
            {this.props.title && <div className="input__title">{this.props.title}</div>}
            <input type="text" className="input__input" {...this.props} />
        </div>);
    }
});

export default Input;