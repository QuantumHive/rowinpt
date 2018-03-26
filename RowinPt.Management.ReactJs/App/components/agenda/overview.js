import React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

import Spinner from "../../common/spinner";
import Header from "../../common/header";
import NoResults from "../../common/noresult";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, getResult, isArray } from "../../utils/apiHelpers";

import { Calendar } from 'react-widgets';
import moment from "moment";
import * as filterActions from "../../actions/filters";

class Agenda extends React.Component {
    constructor(props) {
        super(props);

        this.state = props.filters === null ? {
            locationId: "",
            date: moment().startOf("day")
        } : props.filters;

        this.changeLocation = this.changeLocation.bind(this);
        this.changeDate = this.changeDate.bind(this);
    }

    componentDidMount() {
        if (this.state.locationId !== "") {
            this.props.loadAgenda(this.state.locationId, this.state.date);
        }

        this.props.getLocations();
    }

    componentWillReceiveProps(nextProps) {
        if (this.state.locationId === "" && !nextProps.locationsLoading && nextProps.locations.length > 0) {
            this.setState({ locationId: nextProps.locations[0].id });
            this.props.loadAgenda(nextProps.locations[0].id, this.state.date);
        }
    }

    changeLocation(event) {
        const target = event.target;
        const value = target.value;

        this.setState({ locationId: value });
        this.props.loadAgenda(value, this.state.date);
        this.props.setFilter({ locationId: value, date: this.state.date });
    }

    changeDate(value) {
        this.setState({ date: moment(value) });
        this.props.loadAgenda(this.state.locationId, moment(value));
        this.props.setFilter({ locationId: this.state.locationId, date: moment(value) });
    }

    render() {
        if (this.props.locationsLoading) {
            return <Spinner />;
        }

        if (this.props.locations.length === 0) {
            return (
                <div className="alert alert-warning text-center">
                    <p>Er bestaan nog geen locaties. Maak eerst een locatie aan.</p>
                    <Link to="/admin/locations/new" className="btn btn-primary">Locatie aanmaken</Link>
                </div>
            );
        }

        if (this.props.agendaLoading) {
            return <Spinner />;
        }

        return (
            <div>
                <Header>Agenda</Header>

                <form>
                    <div className="form-group">
                        <label>Locatie</label>
                        <select name="locationId" className="custom-select" value={this.state.locationId} onChange={this.changeLocation}>
                            {
                                this.props.locations.map(location => {
                                    return (
                                        <option key={location.id} value={location.id}>{location.name}</option>
                                    );
                                })
                            }
                        </select>
                    </div>

                    <div className="form-group">
                        <label>Datum</label>
                        <Calendar
                            min={moment().startOf("day").toDate()}
                            value={this.state.date.toDate()}
                            onChange={this.changeDate} />
                    </div>
                </form>

                {
                    this.props.agenda.length === 0
                        ?
                        <div className="alert alert-warning text-center">
                            Er is geen planning voor deze dag
                        </div>
                        :
                    <table className="table table-hover">
                            <tbody>
                                {
                                    this.props.agenda.sort((a, b) => {
                                        if (moment(a.start, "HH:mm:ss").isBefore(moment(b.start, "HH:mm:ss"))) return -1;
                                        if (moment(a.start, "HH:mm:ss").isAfter(moment(b.start, "HH:mm:ss"))) return 1;
                                        return 0;
                                    }).map(item => {
                                        const to = "/agenda/" + item.id;
                                        return (
                                            <tr key={item.id}>
                                                <td>
                                                    <Link to={to} className="d-flex unstyle-anchor">
                                                        <div className="d-flex flex-column pr-2 align-self-center" style={{ borderRight: "solid #343a40 .15rem" }}>
                                                            <span>{moment(item.start, "HH:mm:ss").format("HH:mm")}</span>
                                                            <span className="text-muted">{moment(item.end, "HH:mm:ss").format("HH:mm")}</span>
                                                        </div>

                                                        <div className="d-flex justify-content-between w-100 align-self-stretch">
                                                            <h5 className="ml-3 align-self-center font-weight-normal">
                                                                {item.course}
                                                            </h5>
                                                            <div className="d-flex flex-column justify-content-between">
                                                                <span className="badge badge-pill badge-secondary align-self-end">{item.registrations}/{item.capacity}</span>
                                                                <small className="text-muted align-self-start">
                                                                    <strong>{item.trainer === "" ? "Niemand" : item.trainer}</strong>
                                                                </small>
                                                            </div>
                                                        </div>
                                                    </Link>
                                                </td>
                                            </tr>
                                        );
                                    })
                                }
                            </tbody>
                        </table>
                }

                
            </div>
        );
    }
}


function mapStateToProps(state) {
    const locations = getResult(state, api.locations);
    const agenda = getResult(state, api.agenda);
    
    const locationsLoading = !state.api.hasOwnProperty(api.locations) || state.api[api.locations].loading || !isArray(state, [api.locations]);
    const agendaLoading = isLoading(state, [api.agenda]) || !isArray(state, [api.agenda]);

    return {
        agendaLoading,
        agenda,
        locations,
        locationsLoading,
        filters: ("agendaOverview" in state.filters) ? state.filters["agendaOverview"] : null
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getLocations: () => dispatch(apiActions.get(api.locations)),
        loadAgenda: (locationId, date) => dispatch(apiActions.get(api.agenda, `load/${locationId}?date=${date.format("YYYY-MM-DD")}`)),
        setFilter: filter => dispatch(filterActions.set(filter, "agendaOverview"))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Agenda);