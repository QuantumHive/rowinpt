import React from "react";
import { Route } from "react-router-dom";

import List from "./list";
import New from "./new";
import Details from "./details";
import Edit from "./edit";

import withResetApiOnRouteChange from "../../common/withResetApiOnRouteChange";
import * as api from "../../api";

class Locations extends React.Component {
    render() {
        return (
            <div className="">
                <Route exact path="/admin/locations/list" component={List} />
                <Route exact path="/admin/locations/new" component={New} />
                <Route exact path="/admin/locations/details/:id" component={Details} />
                <Route exact path="/admin/locations/edit/:id" component={Edit} />
            </div>
        );
    }
}

export default withResetApiOnRouteChange(Locations, api.locations);