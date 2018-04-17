import React from "react";
import { connect } from "react-redux";
import { Redirect } from 'react-router-dom';

import Spinner from "../common/spinner";
import { activate } from "./actions";
import queryString from "query-string";

class Activation extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            password: "",
            repeatPassword: "",
            validatePassword: false,
            validateRepeat: false
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.activate = this.activate.bind(this);
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState({ [name]: value });
    }

    activate() {
        if (this.state.password === null || this.state.password.length < 4) {
            this.setState({ validatePassword: true });
        } else if (this.state.repeatPassword !== this.state.password) {
            this.setState({ validatePassword: false, validateRepeat: true });
        } else {
            this.setState({ validatePassword: false, validateRepeat: false });
            const queryParams = queryString.parse(this.props.location.search);
            const payload = {
                id: queryParams.id,
                token: queryParams.token,
                password: this.state.password
            };
            this.props.activate(payload);
        }
    }

    render() {
        if (this.props.success) {
            return <Redirect to="/authentication/login" />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div className="container">
                <div className="row mt-5">
                    <div className="col-md-6 d-md-none text-center mb-3">
                        <img className="img-fluid" src={this.props.settings.blobStorageAccount + "/img/logo_60x60.png"} alt="logo" />
                    </div>

                    <div className="col-6 d-none d-md-block">
                        <img className="img-fluid" src={this.props.settings.blobStorageAccount + "/img/logo_120x120.png"} alt="logo" />
                    </div>
                    <div className="col-md-6">
                        <form className="">
                            <h1 className="h4 font-weight-normal">Account activatie</h1>

                            <p className="lead">Stel je wachtwoord in en activeer je account</p>

                            <div className="form-group">
                                <label>Wachtwoord instellen</label>
                                <input type="password" className={`form-control ${this.state.validatePassword ? "is-invalid" : ""}`} placeholder="Wachtwoord" name="password" value={this.state.password} onChange={this.handleInputChange} />
                                <div className="invalid-feedback">
                                    Wachtwoord moet minimaal 4 karakters bevatten
                                </div>
                            </div>

                            <div className="form-group">
                                <label>Wachtwoord herhalen</label>
                                <input type="password" className={`form-control ${this.state.validateRepeat ? "is-invalid" : ""}`} placeholder="Herhaal wachtwoord" name="repeatPassword" value={this.state.repeatPassword} onChange={this.handleInputChange} />
                                <div className="invalid-feedback">
                                    Wachtwoorden komen niet overeen
                                </div>
                            </div>

                            {
                                this.props.error ?
                                    <p>
                                        <small className="text-danger">
                                            Er is een onverwachte fout opgetreden. Neem contact op met de technische dienst indien de fout blijft optreden.
                                        </small>
                                    </p>
                                    : null
                            }

                            <div className="text-md-right">
                                <button type="button" className="btn btn-outline-success" onClick={this.activate}>Activeren</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        );
    }
}

function mapStateToProps(state) {
    return { ...state.activation, settings: { ...state.settings } };
}

function mapDispatchToProps(dispatch) {
    return {
        activate: payload => dispatch(activate(payload))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Activation);