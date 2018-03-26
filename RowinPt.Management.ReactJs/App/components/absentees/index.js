import React from "react";
import { Route } from "react-router-dom";
import Overview from "./overview";
import Details from "./details";

class Absentees extends React.Component {
    render() {
        return (
            <div className="">
                <Route exact path="/admin/absentees" component={Overview} />
                <Route exact path="/admin/absentees/:id" component={Details} />
            </div>
        );
    }
}

export default Absentees;