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

class Locations extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.submit = this.submit.bind(this);
    }

    componentDidMount() {
        this.props.getLocation(this.props.match.params.id);
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading) {
            this.setState(nextProps.location);
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
            return <Redirect exact to={`/admin/locations/details/${this.state.id}`} />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Back to={`/admin/locations/details/${this.state.id}`} header="Locatie bewerken" />

                <form>
                    <div className="form-group">
                        <label>Naam</label>
                        <input type="text" name="name" className={validation.formControl(this.state.name, validationRules.name, this.state.validate)} placeholder="Naam" value={this.state.name} onChange={this.handleInputChange} />
                        <ValidationFeedback validate={this.state.name} rules={validationRules.name} />
                    </div>

                    <div className="form-group">
                        <label>Adres</label>
                        <textarea value={this.state.address} onChange={this.handleInputChange} rows="3" className={validation.formControl(this.state.address, validationRules.address, this.state.validate)} name="address" placeholder="Adres" />
                        <ValidationFeedback validate={this.state.address} rules={validationRules.address} />
                    </div>

                    <Submit onSubmit={this.submit}>Opslaan</Submit>
                </form>
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
        submitted: state.api.hasOwnProperty(api.locations) && state.api[api.locations].submitted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getLocation: id => dispatch(apiActions.get(api.locations, id)),
        submit: payload => dispatch(apiActions.put(api.locations, payload))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Locations);