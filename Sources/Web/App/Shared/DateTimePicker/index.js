import React from "react";
import Button from "../Button";

var monthsDescriptions = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

function isLeapYear(year) { 
    return (((year % 4 === 0) && (year % 100 !== 0)) || (year % 400 === 0)); 
}

function getDaysInMonth(year, month) {
    return [31, (isLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month];
}


var DateTimePicker = React.createClass({

    getInitialState(){
        return {
            open: false,
            currentDay: this.props.value.getDate(),
            currentMonth: this.props.value.getMonth(),
            currentYear: this.props.value.getFullYear(),
            currentHour: this.props.value.getHours() % 13,
            currentMinute:0,
            currentSelector: this.props.value.getHours() < 12 ? 0 : 1
        };
    },

    toggle(){
        this.setState({open:!this.state.open});
    },

    notify(){
        var d = new Date(this.state.currentYear, this.state.currentMonth, this.state.currentDay, this.state.currentHour + this.state.currentSelector * 12, this.state.currentMinute);

        var date = d;
        this.props.onChange && this.props.onChange(date);
    },

    setHour(x){
        this.setState({currentHour:x}, this.notify)
    },

    setMinutes(x){
        this.setState({currentMinute:x}, this.notify)
    },

    setSelector(x){
        this.setState({currentSelector:x}, this.notify);
    },

    previousMonth(){
        if(this.state.currentMonth==0){
            this.setState({currentMonth: 11, currentYear: this.state.currentYear-1}, this.notify)
        }else{
            this.setState({currentMonth: this.state.currentMonth-1}, this.notify)
        }
    },

    nextMonth(){
        if(this.state.currentMonth==11){
            this.setState({currentMonth: 0, currentYear: this.state.currentYear+1}, this.notify)
        }else{
            this.setState({currentMonth: this.state.currentMonth+1}, this.notify)
        }
    },

    setDay(x){
        if(x!=null){
            this.setState({currentDay:x}, this.notify)
        }
    },

    renderDays(){
        var d = new Date(this.state.currentYear,this.state.currentMonth,0);
        var first = d.getDay();
        var a = [];
        var i=0;
        if(d.getDay()!=6){
            for(i=0;i<d.getDay();i++){
                a.push(null);
            }
        }
        var m = getDaysInMonth(this.state.currentYear, this.state.currentMonth);

        for(i=1;i<=m;i++){
             a.push(i);
        }

        return a.map(x => <li className={x==this.state.currentDay ? "selected" : ""} onClick={() => this.setDay(x)}>{x}&nbsp;</li>)
    },

    render(){
        var date = ('00' + this.props.value.getDate()).slice(-2) + "/" + ('00' + (this.props.value.getMonth() + 1)).slice(-2) + "/" + this.props.value.getFullYear();
        var time = ('00' + this.props.value.getHours()).slice(-2) + ":" + ('00' + this.props.value.getMinutes()).slice(-2);

        return (<div className="datetimepicker">
           <div className="datetimepicker__menu">
                <div className="datetimepicker__calendar">
                    <div className="datetimepicker__calendar__header">
                        <span onClick={this.previousMonth} className="previousMonth">&lt;</span>

                        {monthsDescriptions[this.state.currentMonth]} {this.state.currentYear}
                        <span onClick={this.nextMonth} className="nextMonth">&gt;</span>
                    </div>
                    <div className="datetimepicker__calendar__days">
                        <li className="datetimepicker__days">Su</li>	<li className="datetimepicker__days">Mo</li>	<li className="datetimepicker__days">Tu</li>	<li className="datetimepicker__days">We</li>	<li className="datetimepicker__days">Th</li>	<li className="datetimepicker__days">Fr</li>	<li className="datetimepicker__days">Sa</li>
                        {this.renderDays()}
                    </div>
                </div>
                <div className="datetimepicker__clock">
                    <div className="datetimepicker__selector">
                        <span className={this.state.currentSelector == 0 ? "selected" : ""} onClick={() => this.setSelector(0)} >AM</span> 
                        <span className={this.state.currentSelector == 1 ? "selected" : ""} onClick={() => this.setSelector(1)}>PM</span>
                    </div>
                    <div className="datetimepicker__hours">
                         <div className="datetimepicker__hours__bakcground"></div>
                        {[12,1,2,3,4,5,6,7,8,9,10,11].map(x => <li style={{transform:"rotate("+((x%12) * 30)+"deg)"}} className={this.state.currentHour == x ? "selected" : ""} onClick={() => this.setHour(x)}>{x}</li>)}
                    </div>
                    <div className="datetimepicker__minutes">
                         <div className="datetimepicker__minutes__bakcground"></div>
                        {[0,5,10,15,20,25,30,35,40,45,50,55].map((x,i) => <li style={{transform:"rotate("+((i%12) * 30)+"deg)"}}  className={this.state.currentMinute == x ? "selected" : ""} onClick={() => this.setMinutes(x)}>{x}</li>)}
                    </div>                  
                </div>
                <div className="datetimepicker__buttons">
                </div>
            </div>
        </div>);
    }

});

export default DateTimePicker;