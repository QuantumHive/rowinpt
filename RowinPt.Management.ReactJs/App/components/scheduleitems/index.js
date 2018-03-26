import React from "react";
import { Route } from "react-router-dom";

import New from "./new";
import Details from "./details";
import Edit from "./edit";

import withResetApiOnRouteChange from "../../common/withResetApiOnRouteChange";
import * as api from "../../api";

class ScheduleItems extends React.Component {
    render() {
        return (
            <div className="">
                <Route exact path="/admin/scheduleitems/new/:id" component={New} />
                <Route exact path="/admin/scheduleitems/details/:id" component={Details} />
                <Route exact path="/admin/scheduleitems/edit/:id" component={Edit} />
            </div>
        );
    }
}

export default withResetApiOnRouteChange(ScheduleItems, api.scheduleitems);