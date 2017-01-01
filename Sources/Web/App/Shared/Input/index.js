import React from "react";
import classname from "classnames";

var Input = React.createClass({
    render(){
        return (<div className="input">
            {this.props.title && <div className="input__title">{this.props.title}</div>}
            <div className="input__wrapper">
                <input type="text" className="input__input" {...this.props} />
                <div className="input__buttons">
                    {this.props.buttons}
                </div>
            </div>
        </div>);
    }
});

export default Input;