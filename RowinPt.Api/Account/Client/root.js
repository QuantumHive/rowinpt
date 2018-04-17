import React from "react";
import { Provider } from "react-redux"
import AppSettings from "./appSettings";

export default class Root extends React.Component {
    render() {
        return (
            <Provider store={this.props.store}>
                <AppSettings />
            </Provider>
        );
    }
}