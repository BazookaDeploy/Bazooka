import React from "react";
import classname from "classnames";

var Textarea = React.createClass({
    render(){
        return (<div className="textarea">
                {this.props.title && <div className="textarea__title">{this.props.title}</div>}
                <textarea type="text" className="textarea__input" {...this.props} />
        </div>);
    }
});

export default Textarea;