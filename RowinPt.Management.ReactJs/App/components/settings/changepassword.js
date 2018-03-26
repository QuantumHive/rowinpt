import React from "react";
import { connect } from "react-redux";

import { Redirect } from "react-router-dom";

import Spinner from "../../common/spinner";
import Header from "../../common/header";

import * as api from "../../api";
import * as apiActions from "../../actions/api";

import * as validation from "../../common/validation";
import ValidationFeedback from "../../common/validationFeedback";
import { isRequired } from "../../common/validationRules";

import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";

class ChangePassword extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            oldPassword: "",
            newPassword: "",
            repeatPassword: "",
            validate: false,
            modal: false
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.submit = this.submit.bind(this);
        this.closeModal = this.closeModal.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.submitted) {
            this.setState({ modal: true });
        }
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState({ [name]: value });
    }

    submit() {
        if (validation.isValid(this.state, this.validationRules) && this.isValid()) {
            this.props.submit({ ...this.state });
        } else {
            this.setState({ validate: true });
        }
    }

    validationRules() {
        return {
            oldPassword: [
                {
                    rule: isRequired,
                    message: "Vul je oude wachtwoord in"
                }
            ],
            newPassword: [
                {
                    rule: isRequired,
                    message: "Vul je nieuwe wachtwoord in"
                },
                {
                    rule: input => input !== this.state.oldPassword,
                    message: "Je nieuwe wachtwoord mag niet hetzelfde zijn als je oude wachtwoord"
                }
            ],
            repeatPassword: [
                {
                    rule: input => input === this.state.newPassword,
                    message: "Wachtwoorden komen niet overeen"
                }
            ]
        };
    }

    isValid() {
        if (this.state.oldPassword === this.state.newPassword) {
            return false;
        }

        if (this.state.repeatPassword !== this.state.newPassword) {
            return false;
        }

        return true;
    }

    closeModal() {
        this.setState({ modal: false });
        this.props.stopCall();
    }

    render() {
        if (this.props.loading && !this.props.submitted) {
            return <Spinner />;
        }

        return (
            <div>
                <Header>Wachtwoord wijzigen</Header>

                <form>
                    <div className="form-group">
                        <label>Oude wachtwoord</label>
                        <input type="password" name="oldPassword" className={validation.formControl(this.state.oldPassword, this.validationRules().oldPassword, this.state.validate)} placeholder="Oude wachtwoord" value={this.state.oldPassword} onChange={this.handleInputChange} />
                        <ValidationFeedback validate={this.state.oldPassword} rules={this.validationRules().oldPassword} />
                    </div>

                    <div className="form-group">
                        <label>Nieuwe wachtwoord</label>
                        <input type="password" name="newPassword" className={validation.formControl(this.state.newPassword, this.validationRules().newPassword, this.state.validate)} placeholder="Nieuwe wachtwoord" value={this.state.newPassword} onChange={this.handleInputChange} />
                        <ValidationFeedback validate={this.state.newPassword} rules={this.validationRules().newPassword} />
                    </div>

                    <div className="form-group">
                        <label>Herhaal nieuwe wachtwoord</label>
                        <input type="password" name="repeatPassword" className={validation.formControl(this.state.repeatPassword, this.validationRules().repeatPassword, this.state.validate)} placeholder="Herhaal nieuwe wachtwoord" value={this.state.repeatPassword} onChange={this.handleInputChange} />
                        <ValidationFeedback validate={this.state.repeatPassword} rules={this.validationRules().repeatPassword} />
                    </div>

                    <button type="button" className="btn btn-primary btn-lg btn-block" onClick={this.submit}>Wijzigen</button>
                </form>

                <Modal isOpen={this.state.modal && this.props.submitted} toggle={this.closeModal}>
                    <ModalHeader toggle={this.closeModal}>
                        <span className="text-success">Wachtwoord gewijzigd</span>
                    </ModalHeader>
                    <ModalBody>
                        <div className="alert alert-success">
                            Je wachtwoord is met succes gewijzigd!
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        <button type="button" className="btn btn-primary" onClick={this.closeModal}>Ok</button>
                    </ModalFooter>
                </Modal>
            </div>
        );
    }
}

function mapStateToProps(state) {
    return {
        loading: state.api.hasOwnProperty(api.changepassword) && state.api[api.changepassword].loading,
        submitted: state.api.hasOwnProperty(api.changepassword) && state.api[api.changepassword].submitted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        submit: payload => dispatch(apiActions.put(api.changepassword, payload)),
        stopCall: () => dispatch(apiActions.stopCall(api.changepassword))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ChangePassword);