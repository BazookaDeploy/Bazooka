import React from "react";
import classname from "classnames";

var Panel = React.createClass({
    render(){
        var classes = classname("panel", {"panel--danger" : this.props.danger, "panel--success" : this.props.success})

        return <div className={classes} >
            {this.props.title && <div className="panel__title" onClick={this.props.onClick}>{this.props.title}</div>}
            {this.props.open && <div className="panel__content">
                    {this.props.children}
                </div>
            }
        </div>;
    }
});

export default Panel;