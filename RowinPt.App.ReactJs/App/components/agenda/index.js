import React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

import Spinner from "../../common/spinner";
import Create from "../../common/create";
import Header from "../../common/header";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";
import groupBy from "../../utils/groupBy";

import moment from "moment";

class Agenda extends React.Component {
    componentDidMount() {
        if (this.props.agenda === null || !Array.isArray(this.props.agenda)) {
            this.props.getAgenda();
        }
    }

    render() {
        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Header>Agenda</Header>
                <Create to="/schedule">Inplannen</Create>

                {this.list()}
                
            </div>
        );
    }

    list() {
        if (this.props.agenda.length === 0) {
            return <div className="alert alert-warning text-center">Je hebt nog niets ingepland</div>;
        }

        const sortedByDate = this.props.agenda.sort((a, b) => {
            if (moment(a.date).isBefore(moment(b.date))) return -1;
            if (moment(a.date).isAfter(moment(b.date))) return 1;
            return 0;
        });

        return groupBy(sortedByDate, "date").map(group => {
            const sortedByStartTime = group.values.sort((a, b) => {
                if (moment(a.startTime, "HH:mm:ss").isBefore(moment(b.startTime, "HH:mm:ss"))) return -1;
                if (moment(a.startTime, "HH:mm:ss").isAfter(moment(b.startTime, "HH:mm:ss"))) return 1;
                return 0;
            }).filter(agenda => {
                const date = moment(group.key);
                const time = moment(agenda.endTime, "HH:mm:ss");
                date.set("hour", time.get("hour"));
                date.set("minute", time.get("minute"));
                return date.isAfter(moment());
            });

            if (sortedByStartTime.length === 0) return null;

            return (
                <div key={group.key}>
                    <p className="lead mb-2">
                        {moment(group.key).format("dddd, DD MMMM")}
                    </p>
                    <div className="list-group mb-3">
                        {
                            sortedByStartTime.map(agenda => {
                                const to = "/agenda/" + agenda.id;
                                return (
                                    <Link key={agenda.id} to={to} className="list-group-item list-group-item-action py-2 px-3">
                                        <div className="d-flex">
                                            <div className="d-flex flex-column pr-2 align-self-center" style={{ borderRight: "solid #343a40 .15em" }}>
                                                <span>{moment(agenda.startTime, "HH:mm:ss").format("HH:mm")}</span>
                                                <span className="text-muted">{moment(agenda.endTime, "HH:mm:ss").format("HH:mm")}</span>
                                            </div>
                                            <div className="d-flex justify-content-between w-100 align-self-stretch">
                                                <h5 className="ml-3 align-self-center font-weight-normal">
                                                    {agenda.course}
                                                </h5>
                                                <div className="d-flex flex-column justify-content-between">
                                                    <span className="badge badge-pill badge-secondary align-self-end">{agenda.registrations}/{agenda.capacity}</span>
                                                    <small className="text-muted align-self-start">
                                                        <i className="fa fa-map-marker"/> <strong>{agenda.location}</strong>
                                                    </small>
                                                </div>
                                            </div>
                                        </div>
                                    </Link>
                                );
                            })
                        }
                    </div>
                </div>
            );
        });
    }
}

function mapStateToProps(state) {
    const agenda = getResult(state, api.agendacustomer);
    const loading = isLoading(state, [api.agendacustomer]) || !isArray(state, [api.agendacustomer]);

    return {
        agenda,
        loading
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getAgenda: () => dispatch(apiActions.get(api.agendacustomer))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Agenda);