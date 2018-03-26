import React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

import Spinner from "../../common/spinner";
import Header from "../../common/header";
import NoResults from "../../common/noresult";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, getResult, isArray } from "../../utils/apiHelpers";
import * as filterActions from "../../actions/filters";

import moment from "moment";

class Absentees extends React.Component {
    constructor(props) {
        super(props);

        this.state = props.filters === null ? {
            weeks: 2
        } : props.filters;

        this.changeWeek = this.changeWeek.bind(this);
    }

    componentDidMount() {
        this.props.loadAbsentees(this.state.weeks);
    }

    changeWeek(event) {
        const target = event.target;
        const value = target.value;

        this.setState({ weeks: value });
        this.props.loadAbsentees(value);
        this.props.setFilter({ weeks: value });
    }

    render() {
        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Header>Afwezigen</Header>

                <form>
                    <div className="form-group">
                        <label>Weken afwezig</label>
                        <select className="custom-select" value={this.state.weeks} onChange={this.changeWeek}>
                            {
                                [1, 2, 3, 4, 5, 6, 7, 8].map(week => {
                                    return (
                                        <option key={week} value={week}>{week}</option>
                                    );
                                })
                            }
                        </select>
                    </div>
                </form>

                {this.list()}
            </div>
        );
    }

    list() {
        if (this.props.absentees.length === 0) {
            return <NoResults />;
        }

        const absentees =
            this.props.absentees.filter(customer => customer.lastSeen !== null)
                .sort((a, b) => {
                    if (moment(a.lastSeen).isBefore(moment(b.lastSeen))) return -1;
                    if (moment(a.lastSeen).isAfter(moment(b.lastSeen))) return 1;
                    return a.name.localeCompare(b.name);
                });

        const newCustomers =
            this.props.absentees.filter(customer => customer.lastSeen === null)
                .sort((a, b) => a.name.localeCompare(b.name));

        return (
            <div>
                <div className="mb-3">Aantal afwezigen: <span className="badge badge-secondary">{this.props.absentees.length}</span></div>
                <div className="list-group">
                    {
                        absentees.map(customer => {
                            const to = "/admin/absentees/" + customer.id;
                            return (
                                <Link to={to} key={customer.id} className="list-group-item list-group-item-action d-flex flex-column">
                                    <span className="mb-2">{customer.name}</span>
                                    <small className="text-muted">Laatste aanmelding: <strong>{moment(customer.lastSeen).format("LL")}</strong></small>
                                </Link>
                            );
                        })
                    }
                    {
                        newCustomers.map(customer => {
                            const to = "/admin/absentees/" + customer.id;
                            return (
                                <Link to={to} key={customer.id} className="list-group-item list-group-item-action d-flex flex-column">
                                    <span className="mb-2">{customer.name}</span>
                                    <small className="text-muted">Laatste aanmelding: <strong>nooit</strong></small>
                                </Link>
                            );
                        })
                    }
                </div>
            </div>
        );
    }
}


function mapStateToProps(state) {
    const absentees = getResult(state, api.absentees);
    const loading = isLoading(state, [api.absentees]) || !isArray(state, [api.absentees]);

    return {
        loading,
        absentees,
        filters: ("absentees" in state.filters) ? state.filters["absentees"] : null
    };
}

function mapDispatchToProps(dispatch) {
    return {
        loadAbsentees: weeks => dispatch(apiActions.get(api.absentees, weeks)),
        setFilter: filter => dispatch(filterActions.set(filter, "absentees"))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Absentees);