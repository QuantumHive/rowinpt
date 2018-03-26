import React from "react";
import { connect } from "react-redux";
import { Link, Redirect } from "react-router-dom";

import Spinner from "../../common/spinner";
import Back from "../../common/back";
import ComboButtons from "../../common/comboButtonsDetails";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";

import moment from "moment";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";

class Customers extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            data: {},
            modal: false
        };

        this.submitDelete = this.submitDelete.bind(this);
        this.resend = this.resend.bind(this);
        this.toggleModal = this.toggleModal.bind(this);
    }

    componentDidMount() {
        this.props.getCustomer(this.props.match.params.id);
        this.props.getCourseTypes();
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading) {
            this.setState({ data: nextProps.customer });
        }
    }

    toggleModal() {
        this.setState({ modal: false });
        this.props.getCustomer(this.props.match.params.id);
    }

    submitDelete() {
        this.props.remove(this.state.data.id);
    }

    resend() {
        this.props.resend(this.state.data.id);
        this.setState({ modal: true });
    }

    render() {
        if (this.props.deleted) {
            return <Redirect exact to="/admin/customers/list" />;
        }

        if (this.props.loading && !this.props.submitted) {
            return <Spinner />;
        }

        return (
            <div>
                <Back to="/admin/customers/list" header="Klant details" />
                <ComboButtons to={`/admin/customers/edit/${this.state.data.id}`} onSubmit={this.submitDelete} action="Deactiveren">
                    {this.deleteModalBody()}
                </ComboButtons>
                <Link to={`/admin/customers/measurements/${this.state.data.id}`} className="btn btn-success btn-block mb-3">Meetgegevens</Link>

                {this.verifiedNotification()}

                <table className="table">
                    <tbody>
                        <tr>
                            <th>Naam</th>
                            <td>{this.state.data.name}</td>
                        </tr>
                        <tr>
                            <th>Email</th>
                            <td style={{ wordBreak: "break-word" }}><a href={`mailto:${this.state.data.email}`}><i className="fa fa-envelope" /> {this.state.data.email}</a></td>
                        </tr>
                        <tr>
                            <th>Telefoon</th>
                            <td><a href={`tel:${this.state.data.phoneNumber}`}><i className="fa fa-phone" /> {this.state.data.phoneNumber}</a></td>
                        </tr>
                        <tr>
                            <th>Geslacht</th>
                            <td>{this.state.data.sex === 1 ? "Man" : "Vrouw"}</td>
                        </tr>
                        <tr>
                            <th>Geboortedatum</th>
                            <td>{this.state.data.birthDate === null ? "-" : moment(this.state.data.birthDate).format("L")}</td>
                        </tr>
                        <tr>
                            <th>Lengte</th>
                            <td>{this.state.data.length === null ? "-" : `${this.state.data.length} cm`}</td>
                        </tr>
                        <tr>
                            <th>Klantnummer</th>
                            <td>{this.state.data.number === "" ? "-" : this.state.data.number}</td>
                        </tr>
                    </tbody>
                </table>

                <h4>Abonnementen</h4>
                {
                    this.state.data.subscriptions.length === 0 ?
                        <div className="alert alert-info">
                            Geen gekozen abonnementen
                            </div> :
                        <ul className="list-group">
                            {
                                this.state.data.subscriptions.map(subscription => {
                                    const courseTypeSubscription = this.props.coursetypes.find(c => c.id === subscription.courseTypeId).name;
                                    let notes = subscription.notes !== "" && subscription.notes !== null ? <p className="mb-1">Notities: <em>{subscription.notes}</em></p> : null;
                                    return (
                                        <li key={subscription.courseTypeId} className="list-group-item flex-column">
                                            <div className="d-flex justify-content-between align-items-start">
                                                <h5>{courseTypeSubscription}</h5>
                                                <span className="badge badge-secondary badge-pill">{subscription.credits}x</span>
                                            </div>
                                            {notes}
                                            <hr className="mb-0" />
                                            <small>Ingangsdatum: {moment(subscription.startDate).format("L")}</small>
                                        </li>
                                    );
                                })
                            }
                        </ul>
                }

                <Modal isOpen={this.state.modal && this.props.submitted} toggle={this.toggleModal}>
                    <ModalHeader toggle={this.toggleModal}>
                        <span className="text-success">Verstuurd</span>
                    </ModalHeader>
                    <ModalBody>
                        <div className="alert alert-success">
                            Er is opnieuw een activatie mail verstuurd naar de klant.
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        <button type="button" className="btn btn-primary" onClick={this.toggleModal}>Ok</button>
                    </ModalFooter>
                </Modal>
            </div>
        );
    }

    deleteModalBody() {
        return (
            <div>
                <div className="alert alert-danger">
                    Weet je zeker dat je deze klant wilt deactiveren? Deze actie kan <strong>niet</strong> ongedaan worden gemaakt!
                        </div>
                <div className="alert alert-warning">
                    <p><strong>LET OP!</strong> Dit heeft invloed op de volgende gegevens:</p>
                    <ul>
                        <li>De klant verliest toegang tot de app</li>
                        <li>De historie van de klant wordt <em>wel</em> bewaard</li>
                    </ul>
                </div>
            </div>
        );
    }

    verifiedNotification() {
        if (!this.state.data.emailConfirmed) {
            return (
                <div>
                    <div className="alert alert-warning">
                        <strong>Let op!</strong> Het account van deze klant is niet geactiveerd.
                    </div>
                    <button disabled={!this.props.isAdmin} type="button" className="btn btn-warning btn-block mb-3" onClick={this.resend}>Nieuwe activatie mail versturen</button>
                </div>
            );
        }
        return null;
    }
}

function mapStateToProps(state) {
    const customer = getResult(state, api.customers);
    const coursetypes = getResult(state, api.coursetypes);
    const loading = isLoading(state, [api.customers, api.coursetypes])
        || isArray(state, [api.customers]) || !isArray(state, [api.coursetypes]);
    return {
        coursetypes,
        customer,
        loading,
        deleted: state.api.hasOwnProperty(api.customers) && state.api[api.customers].deleted,
        submitted: state.api.hasOwnProperty(api.customers) && state.api[api.customers].submitted,
        isAdmin: state.user.isAdmin
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getCourseTypes: () => dispatch(apiActions.get(api.coursetypes)),
        getCustomer: id => dispatch(apiActions.get(api.customers, id)),
        remove: id => dispatch(apiActions.remove(api.customers, id)),
        resend: id => dispatch(apiActions.post(api.customers, null, "resendactivation/" + id))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Customers);