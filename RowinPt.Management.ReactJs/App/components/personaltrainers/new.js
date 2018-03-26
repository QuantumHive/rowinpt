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

class PersonalTrainers extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            name: "",
            email: "",
            phoneNumber: "",
            sex: 1,
            isAdmin: false,
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

        if (name === "admin") {
            this.setState({ isAdmin: !this.state.isAdmin });
        }
        else if (name === "male") {
            this.setState({ sex: 1 });
        }
        else if (name === "female") {
            this.setState({ sex: 2 });
        }
        else {
            this.setState({ [name]: value });
        }
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
            return <Redirect exact to="/admin/personaltrainers/list" />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Back to="/admin/personaltrainers/list" header="Personal Trainer" />

                <form>
                    <div className="form-group">
                        <label>Naam</label>
                        <input type="text" name="name" className={validation.formControl(this.state.name, validationRules.name, this.state.validate)} placeholder="Naam" value={this.state.name} onChange={this.handleInputChange} />
                        <ValidationFeedback validate={this.state.name} rules={validationRules.name} />
                    </div>

                    <div className="form-group">
                        <label>Email</label>
                        <input type="email" name="email" className={validation.formControl(this.state.email, validationRules.email, this.state.validate)} placeholder="Email" value={this.state.email} onChange={this.handleInputChange} />
                        <ValidationFeedback validate={this.state.email} rules={validationRules.email} />
                    </div>

                    <div className="form-group">
                        <label>Telefoon</label>
                        <input type="tel" name="phoneNumber" className={validation.formControl(this.state.phoneNumber, validationRules.phoneNumber, this.state.validate)} placeholder="Telefoon" value={this.state.phoneNumber} onChange={this.handleInputChange} />
                        <ValidationFeedback validate={this.state.phoneNumber} rules={validationRules.phoneNumber} />
                    </div>

                    <div className="form-group">
                        <div className="custom-control custom-radio custom-control-inline">
                            <input id="male" type="radio" className="custom-control-input" checked={this.state.sex === 1} onChange={this.handleInputChange} name="male" />
                            <label className="custom-control-label" htmlFor="male">Man</label>
                        </div>
                        <div className="custom-control custom-radio custom-control-inline">
                            <input id="female" type="radio" className="custom-control-input" checked={this.state.sex === 2} onChange={this.handleInputChange} name="female" />
                            <label className="custom-control-label" htmlFor="female">Vrouw</label>
                        </div>
                    </div>

                    <div className="form-group">
                        <div className="custom-control custom-checkbox">
                            <input type="checkbox" className="custom-control-input" id="admin" name="admin" checked={this.state.isAdmin} onChange={this.handleInputChange} />
                            <label className="custom-control-label" htmlFor="admin">Admin</label>
                        </div>
                    </div>

                    <Submit onSubmit={this.submit}>Aanmaken</Submit>
                </form>
            </div>
        );
    }
}

function mapStateToProps(state) {
    return { ...state.api[api.personaltrainers] };
}

function mapDispatchToProps(dispatch) {
    return {
        unload: () => dispatch(apiActions.stopCall(api.personaltrainers)),
        submit: payload => dispatch(apiActions.post(api.personaltrainers, payload))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(PersonalTrainers);