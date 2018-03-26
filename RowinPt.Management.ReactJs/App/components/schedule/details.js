import React from "react";
import { connect } from "react-redux";
import { Link, Redirect } from "react-router-dom";

import Spinner from "../../common/spinner";
import Back from "../../common/back";
import NoResults from "../../common/noresult";
import ComboButtons from "../../common/comboButtonsDetails";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";
import groupBy from "../../utils/groupBy";

import moment from "moment";

class Schedule extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
        };

        this.submitDelete = this.submitDelete.bind(this);
    }

    componentDidMount() {
        this.props.getSchedule(this.props.match.params.id);
        this.props.getScheduleItems(this.props.match.params.id);

        this.props.getLocations();
        this.props.getPersonalTrainers();
        this.props.getCourses();
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading) {
            this.setState(nextProps.schedule);
        }
    }

    submitDelete() {
        this.props.remove(this.state.id);
    }

    render() {
        if (this.props.deleted) {
            return <Redirect exact to="/admin/schedule/list" />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }

        const location = this.props.locations.find(l => l.id === this.state.locationId);

        return (
            <div>
                <Back to="/admin/schedule/list" header="Planning details" />
                <ComboButtons to={`/admin/schedule/edit/${this.state.id}`} onSubmit={this.submitDelete}>
                    {this.deleteModalBody()}
                </ComboButtons>

                <table className="table">
                    <tbody>
                        <tr>
                            <th>Naam</th>
                            <td>{this.state.name}</td>
                        </tr>
                        <tr>
                            <th>Locatie</th>
                            <td>{location.name}</td>
                        </tr>
                    </tbody>
                </table>

                <h3>Planning items</h3>

                {
                    this.props.isAdmin ?
                        <Link to={`/admin/scheduleitems/new/${this.state.id}`} className="btn btn-success btn-block mb-3">Inplannen</Link> :
                        null
                }
                

                {
                    this.props.scheduleitems.length === 0 ?
                        <NoResults /> :
                        groupBy(this.props.scheduleitems, "date").sort((a, b) => {
                            if (moment(a.key).isBefore(moment(b.key))) return -1;
                            if (moment(a.key).isAfter(moment(b.key))) return 1;
                            return 0;
                        }).map(group => {
                            const date = moment(group.key).format("dddd, LL");
                            return (
                                <div key={date}>
                                    <p className="lead">
                                        {date}
                                    </p>
                                    <div className="list-group mb-3">
                                        {
                                            group.values.sort((a, b) => {
                                                if (moment(a.start, "HH:mm:ss").isBefore(moment(b.start, "HH:mm:ss"))) return -1;
                                                if (moment(a.start, "HH:mm:ss").isAfter(moment(b.start, "HH:mm:ss"))) return 1;
                                                return 0;
                                            }).map(scheduleItem => {
                                                const to = `/admin/scheduleitems/details/${scheduleItem.id}`;
                                                return (
                                                    <Link to={to} key={scheduleItem.id} className="list-group-item list-group-item-action py-2 pl-3">
                                                        <div className="d-flex align-items-center">
                                                            <div className="d-flex flex-column pr-2" style={{ borderRight: "solid #343a40 .15rem" }}>
                                                                <span>{moment(scheduleItem.start, "HH:mm:ss").format("HH:mm")}</span>
                                                                <span className="text-muted">{moment(scheduleItem.end, "HH:mm:ss").format("HH:mm")}</span>
                                                            </div>
                                                            <h6 className="ml-3 font-weight-normal">
                                                                {this.props.courses.find(c => c.id === scheduleItem.courseId).name}
                                                            </h6>
                                                            <div className="d-flex flex-row-reverse ml-auto align-self-end">
                                                                <small className="text-muted">
                                                                    <strong>
                                                                        {
                                                                            scheduleItem.personalTrainerId === null || scheduleItem.personalTrainerId === ""
                                                                                ? "Niemand"
                                                                                : this.props.personaltrainers.find(p => p.id === scheduleItem.personalTrainerId).name
                                                                        }
                                                                    </strong>
                                                                </small>
                                                            </div>
                                                        </div>
                                                    </Link>
                                                );
                                            })
                                        }
                                    </div>
                                </div>
                            );
                        })
                }
            </div>
        );
    }

    deleteModalBody() {
        return (
            <div>
                <div className="alert alert-danger">
                    Weet je zeker dat je deze planning wilt verwijderen? Deze actie kan <strong>niet</strong> ongedaan worden gemaakt!
                        </div>
                <div className="alert alert-warning">
                    <p><strong>LET OP!</strong> Dit heeft invloed op de volgende gegevens:</p>
                    <ul>
                        <li>De volledige planning wordt verwijderd en klanten kunnen zich niet meer aanmelden voor de items onder deze planning</li>
                        <li>Alle aanmeldingen worden afgemeld</li>
                        <li>De historie van alle aanmeldingen worden <em>wel</em> bewaard</li>
                    </ul>
                </div>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const schedule = getResult(state, api.schedule);
    const scheduleitems = getResult(state, api.scheduleitems);
    const locations = getResult(state, api.locations);
    const personaltrainers = getResult(state, api.personaltrainers);
    const courses = getResult(state, api.courses);
    const loading = isLoading(state, [api.schedule, api.scheduleitems, api.locations, api.personaltrainers, api.courses])
        || isArray(state, [api.schedule]) || !isArray(state, [api.scheduleitems, api.locations, api.personaltrainers, api.courses]);

    return {
        schedule,
        scheduleitems,
        locations,
        personaltrainers,
        courses,
        loading,
        deleted: state.api.hasOwnProperty(api.schedule) && state.api[api.schedule].deleted,
        isAdmin: state.user.isAdmin
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getSchedule: id => dispatch(apiActions.get(api.schedule, id)),
        getScheduleItems: id => dispatch(apiActions.get(api.scheduleitems, "byschedule/" + id)),
        getLocations: () => dispatch(apiActions.get(api.locations)),
        getPersonalTrainers: () => dispatch(apiActions.get(api.personaltrainers)),
        getCourses: () => dispatch(apiActions.get(api.courses)),
        remove: id => dispatch(apiActions.remove(api.schedule, id))
    };
}
export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Schedule);