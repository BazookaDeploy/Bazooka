import React from "react";
import ReactDOM from "react-dom";

var portalElement = document.createElement('div');
document.body.appendChild(portalElement);

var notificationType = {
    success: 1,
    error: 2
};

var Notification = React.createClass({
    getInitialState() {
        return {
            notifications: [],
            currentIndex: 0
        };
    },

    add(note, timeout) {
        if (Array.isArray(note.text)) {
            note.text = note.text.join("<br/>");
        }

        var index = this.state.currentIndex;
        var notification = this.state.notifications;
        notification[index] = note;

        this.setState({ notifications: notification, currentIndex: index + 1 }, () => this.remove(index, timeout));
    },

    remove(index, timeout) {
        setTimeout(() => {
            var n = this.state.notifications;
            n[index] = null;
            this.setState({ notifications: n });
        }, timeout);
    },

    render: function () {
        return (
            <div className="notification">
                {this.state.notifications.map((x, i) => {
                    if (x != null) {
                        return (
                            <div key={i} className={x.tipo == notificationType.success ? "notification__wrapper notification--success" : "notification__wrapper notification--fail"}>
                                <div className="notification__title">{x.title}</div>
                                <div className="notification__text" dangerouslySetInnerHTML={{ __html: x.text }}></div>
                                <p className="notification__closeBtn" onClick={() => this.remove(i, 0) }>x</p>
                            </div>);
                    }
                }
                )}
            </div>);
    }
});

var notificator = ReactDOM.render(
    React.createElement(Notification),
    portalElement
);

var Notificator = {
    Success: function (params) {
        notificator.add({ type: notificationType.success, text: params.text, title: params.title}, params.Timeout || 3000);
    },

    Error: function (params) {
        notificator.add({ type: notificationType.error, text: params.text, title: params.title}, 3000);
    },

    Notify: function(params){
        if(params.Success){
            notificator.add({ type: notificationType.success, text: "", title: "Success"}, params.Timeout || 3000);
        }else{
            notificator.add({ type: notificationType.error, text: params.Errors, title: "Error"}, 3000);
        }
    }
};

export default  Notificator;
