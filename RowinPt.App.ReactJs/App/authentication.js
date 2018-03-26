import React from "react";
import { connect } from "react-redux";
import { BrowserRouter } from "react-router-dom"

import Spinner from "./common/spinner";
import axios from "axios";
import { challenge, logout } from "./actions/authentication";
import { loadUserInformation } from "./actions/user";
import App from "./app";

class Authentication extends React.Component {
    constructor(props) {
        super(props);

        this.logout = this.logout.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.authenticated && !nextProps.loading) {
            this.props.challenge(this.props.apiEndpoint);    
        }
        if (nextProps.authenticated && !nextProps.loading && nextProps.user === null) {
            this.props.loadUserInformation();
        }
    }

    componentDidMount() {
        this.props.challenge(this.props.apiEndpoint);
    }

    logout() {
        this.props.logout(this.props.apiEndpoint);
    }

    render() {
        if (!this.props.authenticated || this.props.loading || this.props.user === null) {
            return <Spinner />;
        }

        if (this.props.authenticated) {
            const origin = window.location.origin;
            if (origin !== this.props.redirect) {
                window.location.replace(this.props.redirect);
                return <Spinner />;
            }
        }

        return (
            <BrowserRouter basename="/" >
                <App logout={this.logout} />
            </BrowserRouter>
        );
    }
}

function mapStateToProps(state) {
    return {
        apiEndpoint: state.settings.apiEndpoint,
        authenticated: state.authentication.authenticated,
        loading: state.authentication.loading,
        user: state.user,
        redirect: state.authentication.redirect
    };
}

function mapDispatchToProps(dispatch) {
    return {
        challenge: endpoint => dispatch(challenge(endpoint)),
        logout: endpoint => dispatch(logout(endpoint)),
        loadUserInformation: () => dispatch(loadUserInformation())
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Authentication);