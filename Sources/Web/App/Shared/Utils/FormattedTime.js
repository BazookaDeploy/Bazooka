import React from "react";

var FormattedTime = React.createClass({
    render: function(){
        var date = new Date(this.props.value);
        return <span>{('00' + date.getHours()).slice(-2)}:{('00' + date.getMinutes()).slice(-2)}:{('00' + date.getSeconds()).slice(-2)}</span>
}
})


export default FormattedTime;