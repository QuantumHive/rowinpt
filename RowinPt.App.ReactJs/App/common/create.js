import React from "react";
import { Link } from "react-router-dom";

export default ({ to, children }) => {
    return <Link to={to} className="btn btn-success btn-block mb-3">{children === undefined ? "Toevoegen" : children}</Link>;
}