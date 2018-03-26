import React from "react";
import { connect } from "react-redux";

import { reset, resetAll } from "../actions/api";

function withResetApiOnRouteChange(IndexComponent, api) {
    class Wrapper extends React.Component {
        constructor(props) {
            super(props);

            this.state = {
                location: props.location.pathname
            };
        }

        componentDidMount() {
            this.unsubscribe = this.props.history.listen((location, action) => {
                if (location.pathname !== this.state.location) {
                    this.props.reset();
                    this.setState({ location: location.pathname });
                }
            });
        }

        componentWillUnmount() {
            this.unsubscribe();
            this.props.resetAll();
        }

        render() {
            return <IndexComponent {...this.props } />
        }
    }

    function mapStateToProps(state) {
        return {};
    }

    function mapDispatchToProps(dispatch) {
        return {
            reset: () => dispatch(reset(api)),
            resetAll: () => dispatch(resetAll())
        };
    }

    return connect(
        mapStateToProps,
        mapDispatchToProps
    )(Wrapper);
}

export default withResetApiOnRouteChange;