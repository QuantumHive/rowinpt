import React from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";

import Spinner from "../../common/spinner";
import Back from "../../common/back";
import Submit from "../../common/submit";

import * as api from "../../api";
import * as apiActions from "../../actions/api";

import * as validation from "../../common/validation";
import ValidationFeedback from "../../common/validationFeedback";
import validationRules from "./validationRules";

class CourseTypes extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            name: "",
            capacity: "",
            validate: false
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.submit = this.submit.bind(this);
    }

    componentDidMount() {
        this.props.unload();
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
            return <Redirect exact to="/admin/coursetypes/list" />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Back to="/admin/coursetypes/list" header="Lesvorm" />

                <form>
                    <div className="form-group">
                        <label>Naam</label>
                        <input type="text" name="name" className={validation.formControl(this.state.name, validationRules.name, this.state.validate)} placeholder="Naam" value={this.state.name} onChange={this.handleInputChange} />
                        <ValidationFeedback validate={this.state.name} rules={validationRules.name} />
                    </div>

                    <div className="form-group">
                        <label>Capaciteit</label>
                        <input type="number" name="capacity" className={validation.formControl(this.state.capacity, validationRules.capacity, this.state.validate)} placeholder="Capaciteit" value={this.state.capacity} onChange={this.handleInputChange} />
                        <ValidationFeedback validate={this.state.capacity} rules={validationRules.capacity} />
                    </div>

                    <Submit onSubmit={this.submit}>Aanmaken</Submit>
                </form>
            </div>
        );
    }
}

function mapStateToProps(state) {
    return { ...state.api[api.coursetypes] };
}

function mapDispatchToProps(dispatch) {
    return {
        unload: () => dispatch(apiActions.stopCall(api.coursetypes)),
        submit: payload => dispatch(apiActions.post(api.coursetypes, payload))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CourseTypes);