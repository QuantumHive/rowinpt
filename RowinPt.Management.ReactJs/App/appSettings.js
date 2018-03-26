import React from "react";
import { connect } from "react-redux";

import Spinner from "./common/spinner";

import Authentication from "./authentication";

import { setApiEndpoint } from "./api";
import { getSettings } from "./actions/settings";

class AppSettings extends React.Component {
    componentDidMount() {
        this.props.getSettings();
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading) {
            setApiEndpoint(nextProps.apiEndpoint);
        }
    }

    render() {
        if (this.props.loading) {
            return <Spinner />;
        }

        return <Authentication />;
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