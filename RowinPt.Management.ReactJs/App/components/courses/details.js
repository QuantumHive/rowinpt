import React from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";

import Spinner from "../../common/spinner";
import Back from "../../common/back";
import ComboButtons from "../../common/comboButtonsDetails";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";

class Courses extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
        };

        this.submitDelete = this.submitDelete.bind(this);
    }

    componentDidMount() {
        this.props.getCourse(this.props.match.params.id);
        this.props.getCourseTypes()
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading) {
            this.setState(nextProps.course);
        }
    }

    submitDelete() {
        this.props.remove(this.state.id);
    }

    render() {
        if (this.props.deleted) {
            return <Redirect exact to="/admin/courses/list" />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }

        const coursetype = this.props.coursetypes.find(c => c.id === this.state.courseTypeId);

        return (
            <div>
                <Back to="/admin/courses/list" header="Les details" />
                <ComboButtons to={`/admin/courses/edit/${this.state.id}`} onSubmit={this.submitDelete}>
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
                        <tr>
                            <th>Lesvorm</th>
                            <td>{coursetype.name}</td>
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
                    Weet je zeker dat je deze les wilt verwijderen? Deze actie kan <strong>niet</strong> ongedaan worden gemaakt!
                        </div>
                <div className="alert alert-warning">
                    <p><strong>LET OP!</strong> Dit heeft invloed op de volgende gegevens:</p>
                    <ul>
                        <li>Klanten die zich hebben aangemeld in de agenda voor deze les worden automatisch afgemeld</li>
                        <li>Deze les wordt van de agenda verwijderd</li>
                    </ul>
                </div>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const course = getResult(state, api.courses);
    const coursetypes = getResult(state, api.coursetypes);
    const loading = isLoading(state, [api.coursetypes, api.courses]) || !isArray(state, [api.coursetypes]) || isArray(state, [api.courses]);
    return {
        course,
        coursetypes,
        loading,
        deleted: state.api.hasOwnProperty(api.courses) && state.api[api.courses].deleted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getCourse: id => dispatch(apiActions.get(api.courses, id)),
        getCourseTypes: () => dispatch(apiActions.get(api.coursetypes)),
        remove: id => dispatch(apiActions.remove(api.courses, id))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Courses);