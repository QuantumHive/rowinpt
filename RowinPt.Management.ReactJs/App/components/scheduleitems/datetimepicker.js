import React from "react";
import { Calendar, DateTimePicker } from 'react-widgets';
import moment from "moment";

class DatetimePicker extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            date: props.data.date,
            start: props.data.start,
            end: props.data.end
        };

        this.changeDate = this.changeDate.bind(this);
        this.changeStart = this.changeStart.bind(this);
        this.changeEnd = this.changeEnd.bind(this);
    }

    changeDate(value) {
        this.setState(prevState => {
            const newState = { ...prevState, date: moment(value) };
            this.props.onChange(newState);
            return newState;
        });
    }

    changeStart(value) {
        this.setState(prevState => {
            const newState = { ...prevState, start: moment(value).format("HH:mm:ss"), end: moment(value).add(1, "hours").format("HH:mm:ss") };
            this.props.onChange(newState);
            return newState;
        });
    }

    changeEnd(value) {
        this.setState(prevState => {
            const newState = { ...prevState, end: moment(value).format("HH:mm:ss") };
            this.props.onChange(newState);
            return newState;
        });
    }

    render() {
        return (
            <div>
                <div className="form-group">
                    <label>Datum</label>
                    <Calendar
                        value={this.state.date.toDate()}
                        onChange={this.changeDate}
                        min={moment().startOf("day").toDate()} />
                </div>

                <div className="form-group">
                    <label>Start tijd</label>
                    <DateTimePicker
                        value={moment(this.state.start, "HH:mm:ss").toDate()}
                        onChange={this.changeStart}
                        step={15}
                        date={false}/>
                </div>

                <div className="form-group">
                    <label>Eind tijd</label>
                    <DateTimePicker
                        value={moment(this.state.end, "HH:mm:ss").toDate()}
                        onChange={this.changeEnd}
                        step={15}
                        date={false}/>
                </div>
            </div>
        );
    }
}

export default DatetimePicker;