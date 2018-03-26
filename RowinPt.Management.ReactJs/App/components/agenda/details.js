import React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

import Spinner from "../../common/spinner";
import Back from "../../common/back";

import * as apiActions from "../../actions/api";
import * as api from "../../api";
import { isLoading, getResult, isArray } from "../../utils/apiHelpers";

import moment from "moment";

class Agenda extends React.Component {
    componentDidMount() {
        this.props.getAgenda(this.props.match.params.id);
    }

    render() {
        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Back to="/" header="Agenda details" />

                <table className="table">
                    <tbody>
                        <tr>
                            <th>Locatie</th>
                            <td>{this.props.agenda.location}</td>
                        </tr>
                        <tr>
                            <th>Les</th>
                            <td>{this.props.agenda.course}</td>
                        </tr>
                        <tr>
                            <th>Datum</th>
                            <td>{moment(this.props.agenda.date).format("dddd, LL")}</td>
                        </tr>
                        <tr>
                            <th>Tijd</th>
                            <td>{moment(this.props.agenda.start, "HH:mm:ss").format("HH:mm")} - {moment(this.props.agenda.end, "HH:mm:ss").format("HH:mm")}</td>
                        </tr>
                        <tr>
                            <th>Personal Trainer</th>
                            <td>{this.props.agenda.trainer === "" ? "Niemand" : this.props.agenda.trainer}</td>
                        </tr>
                        <tr>
                            <th>Capaciteit</th>
                            <td>{this.props.agenda.capacity}</td>
                        </tr>
                    </tbody>
                </table>

                <hr className="my-3" />

                <h4 className="font-weight-normal">Aanmeldingen <small><span className="badge badge-pill badge-secondary">{this.props.agenda.registrations}</span></small></h4>
                {
                    this.props.agenda.users.length === 0 ?
                        <div className="alert alert-warning">
                            Er zijn geen aanmeldingen voor deze les
                        </div> :
                        <div className="list-group">
                            {
                                this.props.agenda.users.map(user => {
                                    const to = "/admin/customers/details/" + user.id;
                                    return (
                                        <Link key={user.id} to={to} className="list-group-item list-group-item-action">{user.name}</Link>
                                    );
                                })
                            }
                        </div>
                }
            </div>
        );
    }
}

function mapStateToProps(state) {
    const agenda = getResult(state, api.agenda);
    const loading = isLoading(state, [api.agenda]) || isArray(state, [api.agenda]);

    return {
        agenda,
        loading
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getAgenda: id => dispatch(apiActions.get(api.agenda, id))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Agenda);