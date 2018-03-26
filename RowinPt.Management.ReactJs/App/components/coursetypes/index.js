import React from "react";
import { Route } from "react-router-dom";

import List from "./list";
import New from "./new";
import Details from "./details";
import Edit from "./edit";

import withResetApiOnRouteChange from "../../common/withResetApiOnRouteChange";
import * as api from "../../api";

class CourseTypes extends React.Component {
    render() {
        return (
            <div className="">
                <Route exact path="/admin/coursetypes/list" component={List} />
                <Route exact path="/admin/coursetypes/new" component={New} />
                <Route exact path="/admin/coursetypes/details/:id" component={Details} />
                <Route exact path="/admin/coursetypes/edit/:id" component={Edit} />
            </div>
        );
    }
}

export default withResetApiOnRouteChange(CourseTypes, api.coursetypes);