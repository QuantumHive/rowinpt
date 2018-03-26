import React from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";

import Spinner from "../../common/spinner";
import Back from "../../common/back";
import Submit from "../../common/submit";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";

import * as validation from "../../common/validation";
import ValidationFeedback from "../../common/validationFeedback";
import validationRules from "./validationRules";

class Courses extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.submit = this.submit.bind(this);
    }

    componentDidMount() {
        this.props.getCourse(this.props.match.params.id);
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading) {
            this.setState(nextProps.course);
        }
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState({ [name]: value });
    }

    submit() {
        if (validation.isValid(this.state, validationRules)) {
            this.props.submit({ ...this.state });
        } else {
            this.setState({ validate: true });
        }
    }

    render() {
        if (this.props.submitted) {
            return <Redirect exact to={`/admin/courses/details/${this.state.id}`} />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Back to={`/admin/courses/details/${this.state.id}`} header="Les bewerken" />

                <form>
                    <div className="form-group">
                        <label>Naam</label>
                        <input type="text" name="name" className={validation.formControl(this.state.name, validationRules.name, this.state.validate)} placeholder="Naam" value={this.state.name} onChange={this.handleInputChange} />
                        <ValidationFeedback validate={this.state.name} rules={validationRules.name} />
                    </div>

                    <div className="form-group">
                        <label>Capaciteit</label>
                        <input type="number" name="capacity" className={validation.formControl(this.state.capacity, validationRules.capacity, this.state.validate)} placeholder="Capaciteit" value={this.state.capacity} onChange={this.handleInputChange} />
                        <small className="form-text text-muted">Een wijziging in capaciteit laat alle aanmeldingen in de agenda intact, ook al zou de wijziging de capaciteit overschrijden.</small>
                        <ValidationFeedback validate={this.state.capacity} rules={validationRules.capacity} />
                    </div>

                    <Submit onSubmit={this.submit}>Opslaan</Submit>
                </form>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const course = getResult(state, api.courses);
    const loading = isLoading(state, [api.courses]) || isArray(state, [api.courses]);
    return {
        course,
        loading,
        submitted: state.api.hasOwnProperty(api.courses) && state.api[api.courses].submitted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getCourse: id => dispatch(apiActions.get(api.courses, id)),
        submit: payload => dispatch(apiActions.put(api.courses, payload))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Courses);