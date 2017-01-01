import React from "react";
import ReactDOM from "react-dom";

var FileUpload = React.createClass({

    render() {

        return (
            <div className="upload">
                <div className="upload__button" onClick={() => this.refs.input.click()}>
                    {this.props.children}
                </div>
                <input
                    type="file"
                    className="upload__input"
                    onChange={this.props.onChange}
                    ref="input" />
            </div>);
}
});

export default FileUpload;