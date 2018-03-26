import React from "react";
import { Link } from "react-router-dom";
import Header from "./header";

export default ({ to, header = null }) => {
    const back = <Link to={to} className="btn btn-secondary btn-sm align-self-center">Terug</Link>;

    if (header !== null) {
        return (
            <div className="d-flex justify-content-between">
                <Header>{header}</Header>
                {back}
            </div>
        );
    }

    return back;
}