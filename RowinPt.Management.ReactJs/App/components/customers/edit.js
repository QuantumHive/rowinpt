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

import Subscription from "./subscription";
import { DateTimePicker } from "react-widgets";
import moment from "moment";

class Customers extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.submit = this.submit.bind(this);
        this.chooseSubscription = this.chooseSubscription.bind(this);
        this.handleBirthDate = this.handleBirthDate.bind(this);
    }

    componentDidMount() {
        this.props.getCustomer(this.props.match.params.id);
        this.props.getCourseTypes();
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading) {
            const customer = nextProps.customer;
            if (customer.length === null) {
                customer.length = "";
            }
            if (customer.subscriptions.length > 0) {
                for (const index in customer.subscriptions) {
                    const subscription = customer.subscriptions[index];
                    subscription.startDate = moment(subscription.startDate);
                }
            }
            this.setState(customer);
        }
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
            return <Redirect exact to={`/admin/customers/details/${this.state.id}`} />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Back to={`/admin/customers/details/${this.state.id}`} header="Klant bewerken" />

                <form>
                    <div className="form-group">
                        <label>Naam</label>
                        <input type="text" name="name" className={validation.formControl(this.state.name, validationRules.name, this.state.validate)} placeholder="Naam" value={this.state.name} onChange={this.handleInputChange} />
                        <ValidationFeedback validate={this.state.name} rules={validationRules.name} />
                    </div>

                    {
                        this.state.emailConfirmed ?
                            null :
                            <div className="form-group">
                                <label>Email</label>
                                <input type="email" name="email" className={validation.formControl(this.state.email, validationRules.email, this.state.validate)} placeholder="Email" value={this.state.email} onChange={this.handleInputChange} />
                                <small className="form-text text-muted">Wanneer de email wordt gewijzigd, wordt er opnieuw een activatie mail verstuurd.</small>

                                <ValidationFeedback validate={this.state.email} rules={validationRules.email} />
                            </div>
                    }

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
                            value={moment.isMoment(this.state.birthDate) ? this.state.birthDate.toDate() : moment(this.state.birthDate).toDate()} onChange={this.handleBirthDate} />
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
                        <textarea value={this.state.goal} onChange={this.handleInputChange} rows="3" className="form-control" name="goal" placeholder="Doelstelling" />
                    </div>

                    <Subscription courseTypes={this.props.coursetypes} subscriptions={this.state.subscriptions} chooseSubscription={this.chooseSubscription} />
                    
                    <Submit onSubmit={this.submit}>Opslaan</Submit>
                </form>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const customer = getResult(state, api.customers);
    const coursetypes = getResult(state, api.coursetypes);
    const loading = isLoading(state, [api.customers]) || isArray(state, [api.customers])
                 || isLoading(state, [api.coursetypes]) || !isArray(state, [api.coursetypes]);
    return {
        coursetypes,
        customer,
        loading,
        submitted: state.api.hasOwnProperty(api.customers) && state.api[api.customers].submitted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getCourseTypes: () => dispatch(apiActions.get(api.coursetypes)),
        getCustomer: id => dispatch(apiActions.get(api.customers, id)),
        submit: payload => dispatch(apiActions.put(api.customers, payload))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Customers);