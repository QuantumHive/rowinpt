import React from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";

import Spinner from "../../common/spinner";
import Back from "../../common/back";
import ComboButtons from "../../common/comboButtonsDetails";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";

class Locations extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
        };

        this.submitDelete = this.submitDelete.bind(this);
    }

    componentDidMount() {
        this.props.getLocation(this.props.match.params.id);
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading) {
            this.setState(nextProps.location);
        }
    }

    submitDelete() {
        this.props.remove(this.state.id);
    }

    render() {
        if (this.props.deleted) {
            return <Redirect exact to="/admin/locations/list" />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Back to="/admin/locations/list" header="Locatie details" />
                <ComboButtons to={`/admin/locations/edit/${this.state.id}`} onSubmit={this.submitDelete}>
                    {this.deleteModalBody()}
                </ComboButtons>

                <table className="table">
                    <tbody>
                        <tr>
                            <th>Naam</th>
                            <td>{this.state.name}</td>
                        </tr>
                        <tr>
                            <th>Adres</th>
                            <td style={{whiteSpace: "pre-wrap"}}>{this.state.address}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }

    deleteModalBody() {
        return (
            <div>
                <div className="alert alert-danger">
                    Weet je zeker dat je deze locatie wilt verwijderen? Deze actie kan <strong>niet</strong> ongedaan worden gemaakt!
                        </div>
                <div className="alert alert-warning">
                    <p><strong>LET OP!</strong> Dit heeft invloed op de volgende gegevens:</p>
                    <ul>
                        <li>Klanten die zich hebben aangemeld in de agenda voor de planning van deze locatie worden afgemeld</li>
                        <li>De planning van deze locatie wordt verwijderd</li>
                    </ul>
                </div>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const location = getResult(state, api.locations);
    const loading = isLoading(state, [api.locations]) || isArray(state, [api.locations]);
    return {
        location,
        loading,
        deleted: state.api.hasOwnProperty(api.locations) && state.api[api.locations].deleted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getLocation: id => dispatch(apiActions.get(api.locations, id)),
        remove: id => dispatch(apiActions.remove(api.locations, id))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Locations);