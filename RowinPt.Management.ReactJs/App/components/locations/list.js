import React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

import Spinner from "../../common/spinner";
import Header from "../../common/header";
import Create from "../../common/create";
import NoResults from "../../common/noresult";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";

class Locations extends React.Component {
    componentDidMount() {
        this.props.getLocations();
    }

    render() {
        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Header>Locaties</Header>
                <Create to="/admin/locations/new">Locaties toevoegen</Create>
                {this.list()}
            </div>
        );
    }

    list() {
        if (this.props.locations.length === 0) {
            return <NoResults />;
        }

        return (
            <div className="list-group">
                {
                    this.props.locations.map(location => {
                        const to = "/admin/locations/details/" + location.id;
                        return (
                            <Link key={location.id} to={to} className="list-group-item list-group-item-action">
                                {location.name}
                            </Link>
                        );
                    })
                }
            </div>
        );
    }
}

function mapStateToProps(state) {
    const locations = getResult(state, api.locations);
    const loading = isLoading(state, [api.locations]) || !isArray(state, [api.locations]);
    return {
        locations,
        loading
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getLocations: () => dispatch(apiActions.get(api.locations))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Locations);