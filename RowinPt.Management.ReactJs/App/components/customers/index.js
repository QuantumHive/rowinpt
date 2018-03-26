import React from "react";
import { Route } from "react-router-dom";

import List from "./list";
import New from "./new";
import Details from "./details";
import Edit from "./edit";
import Measurements from "./measurements";

import withResetApiOnRouteChange from "../../common/withResetApiOnRouteChange";
import * as api from "../../api";

class Customers extends React.Component {
    render() {
        return (
            <div className="">
                <Route exact path="/admin/customers/list" component={List} />
                <Route exact path="/admin/customers/new" component={New} />
                <Route exact path="/admin/customers/details/:id" component={Details} />
                <Route exact path="/admin/customers/edit/:id" component={Edit} />
                <Route exact path="/admin/customers/measurements/:id" component={Measurements} />
            </div>
        );
    }
}

export default withResetApiOnRouteChange(Customers, api.customers);