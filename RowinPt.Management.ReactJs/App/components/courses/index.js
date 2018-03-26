import React from "react";
import { Route } from "react-router-dom";

import List from "./list";
import New from "./new";
import Details from "./details";
import Edit from "./edit";

import withResetApiOnRouteChange from "../../common/withResetApiOnRouteChange";
import * as api from "../../api";

class Courses extends React.Component {
    render() {
        return (
            <div className="">
                <Route exact path="/admin/courses/list" component={List} />
                <Route exact path="/admin/courses/new" component={New} />
                <Route exact path="/admin/courses/details/:id" component={Details} />
                <Route exact path="/admin/courses/edit/:id" component={Edit} />
            </div>
        );
    }
}

export default withResetApiOnRouteChange(Courses, api.courses);