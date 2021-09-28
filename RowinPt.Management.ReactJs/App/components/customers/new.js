import React from "react";
import { connect } from "react-redux";
import { Link, Redirect } from "react-router-dom";

import Spinner from "../../common/spinner";
import Back from "../../common/back";
import Submit from "../../common/submit";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";

import * as validation from "../../common/validation";
import ValidationFeedback from "../../common/validationFeedback";
import validationRules from "./validationRules";

import Subscription from "./subscription";
import { DateTimePicker } from "react-widgets";
import moment from "moment";

class Customers extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            name: "",
            email: "",
            number: "",
            phoneNumber: "",
            sex: 1,
            length: "",
            birthDate: null,
            goal: null,
            medicalHistory: null,
            details: null,
            subscriptions: [],
            validate: false
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.submit = this.submit.bind(this);
        this.chooseSubscription = this.chooseSubscription.bind(this);
        this.handleBirthDate = this.handleBirthDate.bind(this);
    }

    componentDidMount() {
        this.props.unload();
        this.props.getCourseTypes();
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        if (name === "male") {
            this.setState({ sex: 1 });
        }
        else if (name === "female") {
            this.setState({ sex: 2 });
        }
        else {
            this.setState({ [name]: value });
        }
    }

    handleBirthDate(value) {
        this.setState({ birthDate: moment(value) });
    }

    chooseSubscription(subscriptions) {
        this.setState({ subscriptions: [...subscriptions] })
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
            return <Redirect exact to="/admin/customers/list" />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }

        if (this.props.coursetypes.length === 0) {
            return (
                <div className="alert alert-warning text-center">
                    <p>Er bestaan nog geen lesvormen. Maak eerst een lesvorm aan.</p>
                    <Link to="/admin/coursetypes/new" className="btn btn-primary">Lesvorm aanmaken</Link>
                </div>
            );
        }

        return (
            <div>
                <Back to="/admin/customers/list" header="Klant" />

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
                        <label>Geboortedatum</label>
                        <DateTimePicker time={false} placeholder="dd-mm-yyyy" defaultView="decade" defaultCurrentDate={new Date(2000, 0, 1)}
                            value={this.state.birthDate === null ? null : this.state.birthDate.toDate()} onChange={this.handleBirthDate} />
                    </div>

                    <div className="form-group">
                        <label>Lengte</label>
                        <div className="input-group">
                            <input type="number" name="length" className={validation.formControl(this.state.length, validationRules.length, this.state.validate)} placeholder="Lengte" value={this.state.length} onChange={this.handleInputChange} />
                            <div className="input-group-append">
                                <span className="input-group-text">cm</span>
                            </div>
                            <ValidationFeedback validate={this.state.length} rules={validationRules.length} />
                        </div>
                    </div>

                    <div className="form-group">
                        <label>Klantnummer</label>
                        <input type="text" name="number" className="form-control" placeholder="Klantnummer" value={this.state.number} onChange={this.handleInputChange} />
                    </div>

                    <div className="form-group">
                        <label>Doelstelling</label>
                        <textarea name="goal" className="form-control" placeholder="Doelstelling" value={this.state.goal} onChange={this.handleInputChange} rows="3" />
                    </div>

                    <div className="form-group">
                        <label>Medische historie</label>
                        <textarea name="medicalHistory" className="form-control" placeholder="Medische historie" value={this.state.medicalHistory} onChange={this.handleInputChange} rows="3" />
                    </div>

                    <div className="form-group">
                        <label>Bijzonderheden</label>
                        <textarea name="details" className="form-control" placeholder="Bijzonderheden" value={this.state.details} onChange={this.handleInputChange} rows="3" />
                    </div>

                    <Subscription courseTypes={this.props.coursetypes} subscriptions={this.state.subscriptions} chooseSubscription={this.chooseSubscription} />

                    <Submit onSubmit={this.submit}>Aanmaken</Submit>
                </form>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const coursetypes = getResult(state, api.coursetypes);
    const loading = isLoading(state, [api.coursetypes]) || !isArray(state, [api.coursetypes]) || state.api[api.customers].loading;
    return {
        coursetypes,
        loading,
        submitted: state.api.hasOwnProperty(api.customers) && state.api[api.customers].submitted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getCourseTypes: () => dispatch(apiActions.get(api.coursetypes)),
        submit: payload => dispatch(apiActions.post(api.customers, payload)),
        unload: () => dispatch(apiActions.stopCall(api.customers))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Customers);