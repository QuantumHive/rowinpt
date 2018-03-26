import React from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";

import Spinner from "../../common/spinner";
import Back from "../../common/back";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";

import moment from "moment";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";

class Agenda extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            modal: false
        };

        this.toggleModal = this.toggleModal.bind(this);
        this.submit = this.submit.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (this.props.deleted) {
            this.props.resetAgenda();
        }
    }

    componentDidMount() {
        this.props.getAgenda(this.props.match.params.id);
    }

    componentWillUnmount() {
        this.props.reset();
    }

    toggleModal() {
        this.setState({ modal: !this.state.modal });
    }

    submit() {
        this.toggleModal();
        this.props.remove(this.props.match.params.id);
    }

    render() {
        if (this.props.deleted) {
            return <Redirect exact to="/" />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }

        const date = moment(this.props.agenda.date);
        const startTime = moment(this.props.agenda.startTime, "HH:mm:ss");
        date.set("hour", startTime.get("hour"));
        date.set("minute", startTime.get("minute"));

        const canCancel = moment().add(1, "hour").isBefore(date);

        return (
            <div>
                <Back to="/" header="Agenda details" />

                <button type="button" className="btn btn-danger btn-block mb-3" onClick={this.toggleModal} disabled={!canCancel}>Afmelden {canCancel ? "" : " niet mogelijk"}</button>

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
                            <td>{date.format("dddd, LL")}</td>
                        </tr>
                        <tr>
                            <th>Tijd</th>
                            <td>{startTime.format("HH:mm")} - {moment(this.props.agenda.endTime, "HH:mm:ss").format("HH:mm")}</td>
                        </tr>
                        <tr>
                            <th>Personal Trainer</th>
                            <td>{this.props.agenda.trainer === "" ? "Niemand" : this.props.agenda.trainer}</td>
                        </tr>
                        <tr>
                            <th>Capaciteit</th>
                            <td>{this.props.agenda.capacity}</td>
                        </tr>
                        <tr>
                            <th>Aanmeldingen</th>
                            <td><span className="badge badge-pill badge-secondary">{this.props.agenda.registrations}</span></td>
                        </tr>
                    </tbody>
                </table>

                <Modal isOpen={this.state.modal} toggle={this.toggleModal}>
                    <ModalHeader toggle={this.toggleModal}>
                        <span className="text-danger">Afmelden</span>
                    </ModalHeader>
                    <ModalBody>
                        <div className="alert alert-danger">Weet je zeker dat je je wilt afmelden voor deze les?</div>
                            <p><strong>LET OP!</strong> Als je je afmeldt dan:</p>
                            <ul>
                                <li>wordt je tegoed gecorrigeerd</li>
                                <li>kun je een les binnen vier weken inhalen</li>
                            </ul>
                    </ModalBody>
                    <ModalFooter>
                        <button type="button" className="btn btn-danger" onClick={this.submit}>Afmelden</button>
                        <button type="button" className="btn btn-secondary" onClick={this.toggleModal}>Annuleren</button>
                    </ModalFooter>
                </Modal>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const agenda = getResult(state, api.agendacustomer);
    const loading = isLoading(state, [api.agendacustomer]) || isArray(state, [api.agendacustomer]);
    return {
        agenda,
        loading,
        deleted: state.api.hasOwnProperty(api.agenda) && state.api[api.agenda].deleted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getAgenda: id => dispatch(apiActions.get(api.agendacustomer, id)),
        remove: id => dispatch(apiActions.remove(api.agenda, id)),
        resetAgenda: () => dispatch(apiActions.reset(api.agendacustomer)),
        reset: () => dispatch(apiActions.reset(api.agenda))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Agenda);