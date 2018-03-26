import React from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";

import Spinner from "../../common/spinner";
import Back from "../../common/back";
import ComboButtons from "../../common/comboButtonsDetails";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";

import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";

class PersonalTrainers extends React.Component {
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
        this.props.getPersonalTrainer(this.props.match.params.id);
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading) {
            this.setState({ data: nextProps.personltrainer });
        }
    }

    toggleModal() {
        this.setState({ modal: false });
        this.props.getPersonalTrainer(this.props.match.params.id);
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
            return <Redirect exact to="/admin/personaltrainers/list" />;
        }

        if (this.props.loading && !this.props.submitted) {
            return <Spinner />;
        }

        return (
            <div>
                <Back to="/admin/personaltrainers/list" header="Personal trainer details" />
                <ComboButtons to={`/admin/personaltrainers/edit/${this.state.data.id}`} onSubmit={this.submitDelete} action="Deactiveren">
                    {this.deleteModalBody()}
                </ComboButtons>

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
                            <th>Admin</th>
                            <td>{this.state.data.isAdmin ? "Ja" : "Nee"}</td>
                        </tr>
                    </tbody>
                </table>

                <Modal isOpen={this.state.modal && this.props.submitted} toggle={this.toggleModal}>
                    <ModalHeader toggle={this.toggleModal}>
                        <span className="text-success">Verstuurd</span>
                    </ModalHeader>
                    <ModalBody>
                        <div className="alert alert-success">
                            Er is opnieuw een activatie mail verstuurd naar de personal trainer.
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
                    Weet je zeker dat je deze personal trainer wilt deactiveren? Deze actie kan <strong>niet</strong> ongedaan worden gemaakt!
                    </div>
                <div className="alert alert-warning">
                    <p><strong>LET OP!</strong> Dit heeft invloed op de volgende gegevens:</p>
                    <ul>
                        <li>De personal trainer verliest toegang tot de app</li>
                        <li>De historie van de personal trainer wordt <em>wel</em> bewaard</li>
                        <li>De planning items voor deze personal trainer worden ontkoppeld</li>
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
                        <strong>Let op!</strong> Het account van deze personal trainer is niet geactiveerd.
                    </div>
                    <button disabled={!this.props.isAdmin} type="button" className="btn btn-warning btn-block mb-3" onClick={this.resend}>Nieuwe activatie mail versturen</button>
                </div>
            );
        }
        return null;
    }
}

function mapStateToProps(state) {
    const personltrainer = getResult(state, api.personaltrainers);
    const loading = isLoading(state, [api.personaltrainers]) || isArray(state, [api.personaltrainers]);
    return {
        personltrainer,
        loading,
        deleted: state.api.hasOwnProperty(api.personaltrainers) && state.api[api.personaltrainers].deleted,
        submitted: state.api.hasOwnProperty(api.personaltrainers) && state.api[api.personaltrainers].submitted,
        isAdmin: state.user.isAdmin
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getPersonalTrainer: id => dispatch(apiActions.get(api.personaltrainers, id)),
        remove: id => dispatch(apiActions.remove(api.personaltrainers, id)),
        resend: id => dispatch(apiActions.post(api.personaltrainers, null, "resendactivation/" + id))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(PersonalTrainers);