import React from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";

import Spinner from "../../common/spinner";
import Back from "../../common/back";
import Submit from "../../common/submit";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";

import DatetimePicker from "./datetimepicker";
import moment from "moment";

class ScheduleItems extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.submit = this.submit.bind(this);
        this.changeDate = this.changeDate.bind(this);
    }

    componentDidMount() {
        this.props.getScheduleItem(this.props.match.params.id);
        this.props.getSchedule();
        this.props.getLocations();
        this.props.getPersonalTrainers();
        this.props.getCourses();
        this.props.getCourseTypes();
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading) {
            const data = nextProps.scheduleitem;
            data.date = moment(data.date);
            if (data.personalTrainerId === null) {
                data.personalTrainerId = "";
            }
            this.setState(data);
        }
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState({ [name]: value });
    }

    changeDate(datetime) {
        this.setState({
            date: datetime.date,
            start: datetime.start,
            end: datetime.end
        });
    }

    submit() {
        this.props.submit({ ...this.state });
    }

    render() {
        if (this.props.submitted) {
            return <Redirect exact to={`/admin/scheduleitems/details/${this.state.id}`} />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }

        const schedule = this.props.schedule.find(i => i.id === this.state.scheduleId);
        const location = this.props.locations.find(l => l.id === schedule.locationId);

        return (
            <div>
                <Back to={`/admin/scheduleitems/details/${this.state.id}`} header="Planning item bewerken" />

                <table className="table">
                    <tbody>
                        <tr>
                            <th>Naam</th>
                            <td>{schedule.name}</td>
                        </tr>
                        <tr>
                            <th>Locatie</th>
                            <td>{location.name}</td>
                        </tr>
                    </tbody>
                </table>

                <form>
                    <DatetimePicker onChange={this.changeDate} data={this.state} />

                    <div className="form-group">
                        <label>Personal trainer</label>
                        <select name="personalTrainerId" className="custom-select" value={this.state.personalTrainerId} onChange={this.handleInputChange}>
                            <option value="">Niemand</option>
                            {
                                this.props.personaltrainers.map(trainer => {
                                    return (
                                        <option key={trainer.id} value={trainer.id}>{trainer.name}</option>
                                    );
                                })
                            }
                        </select>
                    </div>

                    <div className="form-group">
                        <label>Les</label>
                        <select name="courseId" className="custom-select" value={this.state.courseId} onChange={this.handleInputChange}>
                            {
                                this.props.coursetypes.map(courseType => {
                                    const courses = this.props.courses.filter(c => c.courseTypeId === courseType.id);
                                    return (
                                        <optgroup key={courseType.id} label={courseType.name}>
                                            {
                                                courses.map(course => {
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
                        <small className="form-text text-muted">
                            Een wijziging naar een andere lesvorm is alleen mogelijk als niemand zich voor deze les heeft aangemeld.
                        </small>
                    </div>
                    
                    <Submit onSubmit={this.submit}>Opslaan</Submit>
                </form>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const scheduleitem = getResult(state, api.scheduleitems);
    const schedule = getResult(state, api.schedule);
    const locations = getResult(state, api.locations);
    const personaltrainers = getResult(state, api.personaltrainers);
    const courses = getResult(state, api.courses);
    const coursetypes = getResult(state, api.coursetypes);

    const loading = isLoading(state, [api.scheduleitems, api.schedule, api.locations, api.personaltrainers, api.courses, api.coursetypes])
        || isArray(state, [api.scheduleitems]) || !isArray(state, [api.schedule, api.locations, api.personaltrainers, api.courses, api.coursetypes]);

    return {
        scheduleitem,
        schedule,
        locations,
        personaltrainers,
        courses,
        coursetypes,
        loading,
        submitted: state.api.hasOwnProperty(api.scheduleitems) && state.api[api.scheduleitems].submitted
    }
}

function mapDispatchToProps(dispatch) {
    return {
        getScheduleItem: id => dispatch(apiActions.get(api.scheduleitems, id)),
        getSchedule: () => dispatch(apiActions.get(api.schedule)),
        getLocations: () => dispatch(apiActions.get(api.locations)),
        getPersonalTrainers: () => dispatch(apiActions.get(api.personaltrainers)),
        getCourses: () => dispatch(apiActions.get(api.courses)),
        getCourseTypes: () => dispatch(apiActions.get(api.coursetypes)),
        submit: payload => dispatch(apiActions.put(api.scheduleitems, payload))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ScheduleItems);