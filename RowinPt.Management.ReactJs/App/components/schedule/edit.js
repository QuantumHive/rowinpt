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

class Schedule extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.submit = this.submit.bind(this);
    }

    componentDidMount() {
        this.props.getSchedule(this.props.match.params.id);
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading) {
            this.setState(nextProps.schedule);
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
            return <Redirect exact to={`/admin/schedule/details/${this.state.id}`} />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }


        const backTo = `/admin/schedule/details/${this.props.match.params.id}`;

        return (
            <div>
                <Back to={`/admin/schedule/details/${this.state.id}`} header="Planning bewerken" />

                <form>
                    <div className="form-group">
                        <label>Naam</label>
                        <input type="text" name="name" className={validation.formControl(this.state.name, validationRules.name, this.state.validate)} placeholder="Naam" value={this.state.name} onChange={this.handleInputChange} />
                        <ValidationFeedback validate={this.state.name} rules={validationRules.name} />
                    </div>

                    <Submit onSubmit={this.submit}>Opslaan</Submit>
                </form>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const schedule = getResult(state, api.schedule);
    const loading = isLoading(state, [api.schedule]) || isArray(state, [api.schedule]);
    return {
        schedule,
        loading,
        submitted: state.api.hasOwnProperty(api.schedule) && state.api[api.schedule].submitted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getSchedule: id => dispatch(apiActions.get(api.schedule, id)),
        submit: payload => dispatch(apiActions.put(api.schedule, payload))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Schedule);