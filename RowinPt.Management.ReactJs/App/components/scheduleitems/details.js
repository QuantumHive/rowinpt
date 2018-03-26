import React from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";

import Spinner from "../../common/spinner";
import Back from "../../common/back";
import ComboButtons from "../../common/comboButtonsDetails";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";

import moment from "moment";

class ScheduleItems extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
        };

        this.submitDelete = this.submitDelete.bind(this);
    }

    componentDidMount() {
        this.props.getScheduleItem(this.props.match.params.id);
        this.props.getSchedule();
        this.props.getLocations();
        this.props.getPersonalTrainers();
        this.props.getCourses();
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading && this.state.scheduleId === undefined) {
            const schedule = this.props.schedule.find(s => s.id === this.props.scheduleitem.scheduleId);
            this.setState({ scheduleId: schedule.id });
        }
    }

    submitDelete() {
        this.props.remove(this.props.scheduleitem.id);
    }

    render() {
        if (this.props.deleted) {
            const to = "/admin/schedule/details/" + this.state.scheduleId;
            return <Redirect exact to={to} />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }

        const scheduleItem = this.props.scheduleitem;
        const schedule = this.props.schedule.find(s => s.id === scheduleItem.scheduleId);
        const location = this.props.locations.find(l => l.id === schedule.locationId);
        const course = this.props.courses.find(c => c.id === scheduleItem.courseId);
        const trainer = scheduleItem.personalTrainerId === null || scheduleItem.personalTrainerId === ""
            ? "Niemand"
            : this.props.personaltrainers.find(t => t.id === scheduleItem.personalTrainerId).name;

        const to = "/admin/schedule/details" + schedule.id;
        return (
            <div>
                <Back to={`/admin/schedule/details/${schedule.id}`} header="Planning item details" />
                <ComboButtons to={`/admin/scheduleitems/edit/${scheduleItem.id}`} onSubmit={this.submitDelete}>
                    {this.deleteModalBody()}
                </ComboButtons>

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
                        <tr>
                            <th>Datum</th>
                            <td>{moment(scheduleItem.date).format("LL")}</td>
                        </tr>
                        <tr>
                            <th>Tijd</th>
                            <td>{moment(scheduleItem.start, "HH:mm:ss").format("HH:mm")} - {moment(scheduleItem.end, "HH:mm:ss").format("HH:mm")}</td>
                        </tr>
                        <tr>
                            <th>Les</th>
                            <td>{course.name}</td>
                        </tr>
                        <tr>
                            <th>Personal trainer</th>
                            <td>{trainer}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }

    deleteModalBody() {
        return (
            <div>
                <div className="alert alert-danger">
                    Weet je zeker dat je deze planning item wilt verwijderen? Deze actie kan <strong>niet</strong> ongedaan worden gemaakt!
                </div>
                <div className="alert alert-warning">
                    <p><strong>LET OP!</strong> Dit heeft invloed op de volgende gegevens:</p>
                    <ul>
                        <li>Klanten kunnen zich niet meer aanmelden voor deze item</li>
                        <li>Klanten die zich hebben aangemeld voor deze planning item worden afgemeld</li>
                        <li>De historie van alle aanmeldingen worden <em>wel</em> bewaard</li>
                    </ul>
                </div>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const scheduleitem = getResult(state, api.scheduleitems);
    const schedule = getResult(state, api.schedule);
    const personaltrainers = getResult(state, api.personaltrainers);
    const courses = getResult(state, api.courses);
    const locations = getResult(state, api.locations);

    const loading = isLoading(state, [api.scheduleitems, api.schedule, api.personaltrainers, api.courses, api.locations])
        || isArray(state, [api.scheduleitems]) || !isArray(state, [api.courses, api.locations, api.personaltrainers, api.schedule]);

    return {
        scheduleitem,
        schedule,
        personaltrainers,
        courses,
        locations,
        loading,
        deleted: state.api.hasOwnProperty(api.scheduleitems) && state.api[api.scheduleitems].deleted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getScheduleItem: id => dispatch(apiActions.get(api.scheduleitems, id)),
        getSchedule: () => dispatch(apiActions.get(api.schedule)),
        getLocations: () => dispatch(apiActions.get(api.locations)),
        getPersonalTrainers: () => dispatch(apiActions.get(api.personaltrainers)),
        getCourses: () => dispatch(apiActions.get(api.courses)),
        remove: id => dispatch(apiActions.remove(api.scheduleitems, id))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ScheduleItems);