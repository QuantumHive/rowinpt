import React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";


class CreateButton extends React.Component {
    render() {
        if (!this.props.isAdmin) {
            return null;
        }

        const { to, children } = this.props;
        return <Link to={to} className="btn btn-success btn-block mb-3">{children === undefined ? "Toevoegen" : children}</Link>;
    }
}

function mapStateToProps(state) {
    return {
        isAdmin: state.user.isAdmin
    };
}

export default connect(
    mapStateToProps
)(CreateButton);