import React from "react";

var FormattedDate = React.createClass({
    render: function(){
        var date = new Date(this.props.value);
        return <span>{('00' + date.getDate()).slice(-2) + "/" + ('00' + (date.getMonth() + 1)).slice(-2) + "/" + date.getFullYear()}</span>
}
})


export default FormattedDate;