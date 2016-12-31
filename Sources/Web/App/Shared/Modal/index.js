import React from "react";
import ReactDOM from "react-dom";
import classname from "classnames";

var Header = React.createClass({
    render() {
        return <div className="modal--header" onMouseDown={this.props.onMouseDown}>
            {this.props.children}
        </div>;
    }
});

var Body = React.createClass({
    render() {
        return <div className="modal--body">
            {this.props.children}
        </div>;
    }
});

var Footer = React.createClass({
    render() {
        return <div className="modal--footer">
            {this.props.children}
        </div>;
    }
});

var Portal = React.createClass({
    propTypes: {
        onClose: React.PropTypes.func.isRequired
    },

    componentDidMount() {
        this.portalElement = document.createElement('div');
        document.body.appendChild(this.portalElement);
        this.componentDidUpdate();
        if (window.innerHeight < document.documentElement.scrollHeight) {
            document.body.classList.add("modal-open");
        }
    },

    componentDidUpdate() {

        var classes = classname("modal", this.props.className);

        var portal = <div className={classes}>
            <div className="modal--overlay" />
            <div className="modal--dialog">
                <div {...this.props} className="modal--content">
                    <div className="modal__close" onClick={this.props.onClose}>x</div>
                    {this.props.children}
                </div>
            </div>
        </div>;
        ReactDOM.render(portal, this.portalElement);
    },

    componentWillUnmount() {
        document.body.removeChild(this.portalElement);
        document.body.classList.remove("modal-open");
    },

    render() {
        return null;
    }
});

var Modal = React.createClass({
    propTypes: {
        show: React.PropTypes.bool.isRequired
    },

    render() {
        return this.props.show === true ? <Portal { ...this.props }> { this.props.children } </Portal> : null;
    }
});

Modal.Header = Header;
Modal.Body = Body;
Modal.Footer = Footer;

export default Modal;