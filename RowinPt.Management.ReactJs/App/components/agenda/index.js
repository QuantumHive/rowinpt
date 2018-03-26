import React from "react";
import { Route } from "react-router-dom";
import Overview from "./overview";
import Details from "./details";

class Agenda extends React.Component {
    render() {
        return (
            <div className="">
                <Route exact path="/" component={Overview} />
                <Route exact path="/agenda/:id" component={Details} />
            </div>
        );
    }
}

export default Agenda;