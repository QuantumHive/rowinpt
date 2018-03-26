import React from "react";
import { render } from "react-dom";
import { AppContainer } from "react-hot-loader";

import ConfigureStore from "./configureStore";
import Root from "./root";

import "bootstrap";
import "../node_modules/bootstrap/scss/bootstrap.scss";
import "../node_modules/font-awesome/scss/font-awesome.scss";
import "../node_modules/react-widgets/lib/scss/react-widgets.scss";
import "./styles/spinner.scss";
import "./styles/app.scss";

import moment from "moment";
import momentLocalizer from "react-widgets-moment";

moment.locale("nl");
momentLocalizer();

const store = ConfigureStore();

render(
    <AppContainer>
        <Root store={store} />
    </AppContainer>,
    document.getElementById("app")
);

if (module.hot) {
    module.hot.accept("./root", () => {
        const NewRoot = require("./root").default;
        render(
            <AppContainer>
                <NewRoot store={store} />
            </AppContainer>,
            document.getElementById("app")
        );
    });
}