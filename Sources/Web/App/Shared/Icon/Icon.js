import React from "react";
import classname from "classnames";

var Icon = function (path) {
    return React.createClass({
        render() {
            var classes = classname("icon", {"icon--small": this.props.small}, this.props.className);
            return <svg viewBox="0 0 50 50" className={classes} {...this.props}>{path}</svg>
        }
    })
};

export default Icon;