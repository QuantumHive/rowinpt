import React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

import Spinner from "../../common/spinner";
import Header from "../../common/header";
import Create from "../../common/create";
import NoResults from "../../common/noresult";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";
import groupBy from "../../utils/groupBy";

class Schedule extends React.Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.props.getSchedules();
        this.props.getLocations();
    }

    render() {
        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Header>Planning</Header>
                <Create to="/admin/schedule/new">Planning toevoegen</Create>
                {this.list()}
            </div>
        );
    }

    list() {
        if (this.props.schedule.length === 0) {
            return <NoResults />;
        }

        return groupBy(this.props.schedule, "locationId").map(group => {
            const location = this.props.locations.find(l => l.id === group.key).name;
            return (
                <div key={group.key}>
                    <p className="lead">
                        <i className="fa fa-map-marker" /> {location}
                    </p>
                    <div className="list-group mb-3">
                        {
                            group.values.map(schedule => {
                                const to = "/admin/schedule/details/" + schedule.id;
                                return (
                                    <Link key={schedule.id} to={to} className="list-group-item list-group-item-action d-flex justify-content-between align-items-center">
                                        {schedule.name}
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
    const schedule = getResult(state, api.schedule);
    const locations = getResult(state, api.locations);
    const loading = isLoading(state, [api.schedule, api.locations]) || !isArray(state, [api.schedule, api.locations]);

    return {
        schedule,
        locations,
        loading
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getSchedules: () => dispatch(apiActions.get(api.schedule)),
        getLocations: () => dispatch(apiActions.get(api.locations))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Schedule);