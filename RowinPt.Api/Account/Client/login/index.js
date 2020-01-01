import React from "react";
import { connect } from "react-redux";
import { Link } from 'react-router-dom';

import Spinner from "../common/spinner";

import { login, challenge } from "./actions";

class Login extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            email: "",
            password: ""
        };

        this.login = this.login.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    componentDidMount() {
        this.props.challenge();
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState({ [name]: value });
    }

    login(event) {
        event.preventDefault();
        this.props.login(this.state.email, this.state.password);
    }

    render() {
        if (this.props.authenticating) {
            return <Spinner />
        }

        if (this.props.authenticated) {
            window.location.replace(this.props.redirect);
            return <Spinner />
        }

        return (
            <form onSubmit={this.login} id="login" className="d-flex flex-column justify-content-center align-items-center" style={{ height: "100vh" }}>
                <img className="mb-4 align-self-center" src={this.props.settings.blobStorageAccount + "/img/logo_login.png"} alt="logo" />
                <h1 className="h4 mb-3 font-weight-normal">Inloggen</h1>

                {
                    this.props.failed ?
                        <p>
                            <small className="text-danger">
                                Combinatie email/wachtwoord is onjuist
                            </small>
                        </p>
                        : null
                }

                <div className="w-75 d-block d-sm-none">
                    {this.renderLogin()}
                </div>

                <div className="w-50 d-none d-sm-block d-md-none">
                    {this.renderLogin()}
                </div>

                <div className="d-none d-md-block" style={{ minWidth: "330px" }}>
                    {this.renderLogin()}
                </div>

                <p className="text-muted">&copy; 2018 - 2020</p>
            </form>
        );
    }

    renderLogin() {
        return (
            <div>
                <input type="email" name="email" className="form-control " placeholder="Email" value={this.state.email} onChange={this.handleInputChange} />
                <input type="password" name="password" className="form-control mb-3" placeholder="Wachtwoord" value={this.state.password} onChange={this.handleInputChange} />
                <Link to="/account/forgot">Wachtwoord vergeten?</Link>
                <button type="submit" className="btn btn-lg btn-primary btn-block my-4">Log in</button>
            </div>
        );
    }
}

function mapStateToProps(state) {
    return { ...state.authentication, settings: { ...state.settings } };
}

function mapDispatchToProps(dispatch) {
    return {
        challenge: () => dispatch(challenge()),
        login: (email, password) => dispatch(login(email, password))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Login);