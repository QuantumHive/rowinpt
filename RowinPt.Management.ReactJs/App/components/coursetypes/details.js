import React from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";

import Spinner from "../../common/spinner";
import Back from "../../common/back";
import ComboButtons from "../../common/comboButtonsDetails";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";

class CourseTypes extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
        };

        this.submitDelete = this.submitDelete.bind(this);
    }

    componentDidMount() {
        this.props.getCourseType(this.props.match.params.id);
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading) {
            this.setState(nextProps.coursetype);
        }
    }

    submitDelete() {
        this.props.remove(this.state.id);
    }

    render() {
        if (this.props.deleted) {
            return <Redirect exact to="/admin/coursetypes/list" />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Back to="/admin/coursetypes/list" header="Lesvorm details" />
                <ComboButtons to={`/admin/coursetypes/edit/${this.state.id}`} onSubmit={this.submitDelete}>
                    {this.deleteModalBody()}
                </ComboButtons>

                <table className="table">
                    <tbody>
                        <tr>
                            <th>Naam</th>
                            <td>{this.state.name}</td>
                        </tr>
                        <tr>
                            <th>Capaciteit</th>
                            <td>{this.state.capacity}</td>
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
                    Weet je zeker dat je deze lesvorm wilt verwijderen? Deze actie kan <strong>niet</strong> ongedaan worden gemaakt!
                        </div>
                <div className="alert alert-warning">
                    <p><strong>LET OP!</strong> Dit heeft invloed op de volgende gegevens:</p>
                    <ul>
                        <li>Klanten die een abonnement hebben op deze lesvorm, verliezen hun abonnement</li>
                        <li>Klanten die zich hebben aangemeld in de agenda voor de lessen die onder deze lesvorm vallen worden afgemeld</li>
                        <li>Lessen die onder deze lesvorm vallen worden van de planning verwijderd</li>
                        <li>Lessen die onder deze lesvorm vallen worden verwijderd</li>
                    </ul>
                </div>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const coursetype = getResult(state, api.coursetypes);
    const loading = isLoading(state, [api.coursetypes]) || isArray(state, [api.coursetypes]);
    return {
        coursetype,
        loading,
        deleted: state.api.hasOwnProperty(api.coursetypes) && state.api[api.coursetypes].deleted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getCourseType: id => dispatch(apiActions.get(api.coursetypes, id)),
        remove: id => dispatch(apiActions.remove(api.coursetypes, id))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CourseTypes);