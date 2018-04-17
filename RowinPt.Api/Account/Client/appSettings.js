import React from "react";
import { connect } from "react-redux";

import Spinner from "./common/spinner";

import { BrowserRouter } from "react-router-dom"
import App from "./app";

import { getSettings } from "./actions/settings";

class AppSettings extends React.Component {
    componentDidMount() {
        this.props.getSettings();
    }

    render() {
        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <BrowserRouter basename="/" >
                <App />
            </BrowserRouter>
        );
    }
}

function mapStateToProps(state) {
    return { ...state.settings };
}

function mapDispatchToProps(dispatch) {
    return {
        getSettings: () => dispatch(getSettings())
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(AppSettings);