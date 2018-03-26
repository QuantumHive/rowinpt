import React from "react";
import { connect } from "react-redux";

import Header from "../../common/header";
import Spinner from "../../common/spinner";
import Submit from "../../common/submit";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, getResult } from "../../utils/apiHelpers";
import groupBy from "../../utils/groupBy";

import moment from "moment";
import { Calendar } from 'react-widgets';

class Schedule extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            locationId: "",
            courseId: "",
            date: null,
            scheduleItemId: "",
            timeOutId: ""
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.changeDate = this.changeDate.bind(this);
        this.renderCalendarDayComponent = this.renderCalendarDayComponent.bind(this);
        this.submit = this.submit.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.submitted && this.state.timeOutId === "") {
            const plan = JSON.parse(JSON.stringify(this.props.plan));
            const location = plan.locations.find(l => l.id === this.state.locationId);
            const course = location.courses.find(c => c.id === this.state.courseId);
            const date = course.dates.find(d => moment(d.date).isSame(this.state.date, "day"));
            const time = date.times.find(t => t.id === this.state.scheduleItemId);
            time.registrations += 1;
            this.props.update(plan);
            this.props.stop();

            const timeOutId = window.setTimeout(() => {
                this.setState({ timeOutId: "" });
            }, 3000);

            this.setState({ timeOutId });
            this.props.resetAgenda();
        }
    }

    componentDidMount() {
        this.props.getPlanOverview();
    }

    componentWillUnmount() {
        if (this.state.timeOutId !== "") {
            window.clearTimeout(this.state.timeOutId);
        }
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        if (name === "locationId" && this.state.locationId !== "") {
            this.setState({ courseId: "", date: null, scheduleItemId: "", locationId: value });
        } else if (name === "courseId" && this.state.courseId !== ""){
            this.setState({ date: null, scheduleItemId: "", courseId: value });
        } else {
            this.setState({ [name]: value });
        }
    }

    changeDate(value) {
        if (!this.isValidDate(value)) {
            this.setState({ date: moment(value), scheduleItemId: "" });    
        }
    }

    parseTime(time) {
        return moment(time, "HH:mm:ss");
    }

    submit() {
        this.props.submit(this.state.scheduleItemId);
    }

    render() {
        if (this.props.loading) {
            return <Spinner />;
        }

        let renderTimes = null;
        if (this.state.date !== null) {
            const location = this.props.plan.locations.find(l => l.id === this.state.locationId);
            const course = location.courses.find(c => c.id === this.state.courseId);
            const date = course.dates.find(d => moment(d.date).isSame(this.state.date, "day"));
            const times = date.times.sort((a, b) => {
                if (moment(a.startTime, "HH:mm:ss").isBefore(moment(b.startTime, "HH:mm:ss"))) return -1;
                if (moment(a.startTime, "HH:mm:ss").isAfter(moment(b.startTime, "HH:mm:ss"))) return 1;
                return 0;
            });
            renderTimes = times.map(time => {
                let isFullWithOverlap = false;
                if (time.trainerId !== "") {
                    const timesWithTrainer = times.filter(t => t.trainerId === time.trainedId && t.id !== time.id);
                    const overlappingTimes = timesWithTrainer.filter(t => {
                        return parseTime(time.startTime).isBetween(parseTime(t.startTime), parseTime(t.endTime), null, "[)")
                            || parseTime(time.endTime).isBetween(parseTime(t.startTime), parseTime(t.endTime), null, "(]")
                    });

                    const sum = overlappingTimes.reduce((acc, curr) => acc.registrations + curr.registrations, 0);
                    isFullWithOverlap = sum + time.registrations >= time.capacity;
                }
                
                return (
                    <option key={time.id} value={time.id} disabled={time.registrations >= time.capacity || isFullWithOverlap}>
                        {moment(time.startTime, "HH:mm:ss").format("HH:mm")} - {moment(time.endTime, "HH:mm:ss").format("HH:mm")}, ({time.registrations}/{time.capacity}) - {time.trainer === "" ? "Niemand" : time.trainer}
                    </option>
                );
            });
        }

        return (
            <div>
                <Header>Inplannen</Header>

                <form>
                    <div className="form-group">
                        <label>Locatie</label>
                        <select name="locationId" className="custom-select" value={this.state.locationId} onChange={this.handleInputChange}>
                            <option value="" disabled={true}>Kies een locatie</option>
                            {
                                this.props.plan.locations.map(location => {
                                    return (
                                        <option key={location.id} value={location.id}>{location.name}</option>
                                    );
                                })
                            }
                        </select>
                    </div>

                    <div className="form-group">
                        <label>Les</label>
                        <select name="courseId" className="custom-select" disabled={this.state.locationId === ""} value={this.state.courseId} onChange={this.handleInputChange}>
                            <option value="" disabled={true}>Kies een les</option>
                            {
                                this.state.locationId === "" ? null :
                                groupBy(this.props.plan.locations.find(l => l.id === this.state.locationId).courses, "subscription").map(group => {
                                    return (
                                        <optgroup key={group.key} label={group.key}>
                                            {
                                                group.values.map(course => {
                                                    return (
                                                        <option key={course.id} value={course.id}>{course.name}</option>
                                                    );
                                                })
                                            }
                                        </optgroup>
                                    );
                                })
                            }
                        </select>
                    </div>

                    <div className="form-group">
                        <label>Datum</label>
                        <Calendar
                            footer={false}
                            disabled={this.state.courseId === ""}
                            min={moment().startOf("day").toDate()}
                            max={moment().startOf("day").add(4, "weeks").endOf("week").toDate()}
                            value={this.state.date === null ? null : this.state.date.toDate()}
                            onChange={this.changeDate}
                            dayComponent={this.renderCalendarDayComponent} />
                    </div>

                    <div className="form-group">
                        <label>Tijd</label>
                        <select name="scheduleItemId" className="custom-select" disabled={this.state.date === null} value={this.state.scheduleItemId} onChange={this.handleInputChange}>
                            <option value="" disabled={true}>Kies een beschikbare tijd</option>
                            {renderTimes}
                        </select>
                    </div>

                    <button type="button" className="btn btn-primary btn-lg btn-block" onClick={this.submit} disabled={this.state.scheduleItemId === "" || this.state.timeOutId !== ""}>
                        Aanmelden
                    </button>
                </form>

                <div className={"alert alert-primary toast " + (this.state.timeOutId === "" ? "invisible" : "visible")} role="alert">
                    Je hebt je aangemeld!
                </div>
            </div>
        );
    }

    renderCalendarDayComponent({ date, label }) {
        if (!this.isValidDate(date)) return label;

        return (
            <span className="text-danger">
                {label}
            </span>
        );
    }

    isValidDate(value) {
        const locationId = this.state.locationId;
        const courseId = this.state.courseId;
        if (locationId === "" || courseId === "") return false;

        const date = moment(value);
        if (date.isBefore(moment(), "day") || date.isAfter(moment().startOf("day").add(4, "weeks").endOf("week"), "day")) return false;

        const location = this.props.plan.locations.find(l => l.id === locationId);
        const course = location.courses.find(c => c.id === courseId);
        const dates = course.dates.map(date => moment(date.date));

        const isInvalid = dates.find(d => d.isSame(date, "day")) === undefined;
        return isInvalid;
    }
}

function mapStateToProps(state) {
    const plan = getResult(state, api.plan);
    const loading = isLoading(state, [api.plan]);

    return {
        plan,
        loading,
        submitted: state.api.hasOwnProperty(api.plan) && state.api[api.plan].submitted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getPlanOverview: () => dispatch(apiActions.get(api.plan)),
        submit: id => dispatch(apiActions.post(api.plan, null, id)),
        update: plan => dispatch(apiActions.setResult(api.plan, plan)),
        stop: () => dispatch(apiActions.stopCall(api.plan)),
        resetAgenda: () => dispatch(apiActions.reset(api.agendacustomer))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Schedule);