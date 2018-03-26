import React from "react";
import { Route } from "react-router-dom";

import List from "./list";
import New from "./new";
import Details from "./details";
import Edit from "./edit";

import withResetApiOnRouteChange from "../../common/withResetApiOnRouteChange";
import * as api from "../../api";

class Schedule extends React.Component {
    render() {
        return (
            <div className="">
                <Route exact path="/admin/schedule/list" component={List} />
                <Route exact path="/admin/schedule/new" component={New} />
                <Route exact path="/admin/schedule/details/:id" component={Details} />
                <Route exact path="/admin/schedule/edit/:id" component={Edit} />
            </div>
        );
    }
}

export default withResetApiOnRouteChange(Schedule, api.schedule);