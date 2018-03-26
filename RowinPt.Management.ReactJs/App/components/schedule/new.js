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

class Schedule extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            name: "",
            locationId: ""
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.submit = this.submit.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading && nextProps.locations.length > 0) {
            const location = nextProps.locations[0];
            this.setState({
                locationId: location.id
            });
        }
    }

    componentDidMount() {
        this.props.unload();
        this.props.getLocations();
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
            return <Redirect exact to="/admin/schedule/list" />;
        }

        if (this.props.loading) {
            return <Spinner />;
        }

        if (this.props.locations.length === 0) {
            return (
                <div className="alert alert-warning text-center">
                    <p>Er bestaan nog geen locaties. Maak eerst een locatie aan.</p>
                    <Link to="/admin/locations/new" className="btn btn-primary">Locatie aanmaken</Link>
                </div>
            );
        }

        return (
            <div>
                <Back to="/admin/schedule/list" header="Planning" />

                <form>
                    <div className="form-group">
                        <label>Naam</label>
                        <input type="text" name="name" className={validation.formControl(this.state.name, validationRules.name, this.state.validate)} placeholder="Naam" value={this.state.name} onChange={this.handleInputChange} />
                        <ValidationFeedback validate={this.state.name} rules={validationRules.name} />
                    </div>

                    <div className="form-group">
                        <label>Locatie</label>
                        <select name="locationId" className="custom-select" value={this.state.locationId} onChange={this.handleInputChange}>
                            {
                                this.props.locations.map(location => {
                                    return (
                                        <option key={location.id} value={location.id}>{location.name}</option>
                                    );
                                })
                            }
                        </select>
                    </div>

                    <Submit onSubmit={this.submit}>Aanmaken</Submit>
                </form>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const locations = getResult(state, api.locations);
    const loading = isLoading(state, [api.locations]) || !isArray(state, [api.locations]) || state.api[api.schedule].loading;
    return {
        locations,
        loading,
        submitted: state.api.hasOwnProperty(api.schedule) && state.api[api.schedule].submitted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getLocations: () => dispatch(apiActions.get(api.locations)),
        submit: data => dispatch(apiActions.post(api.schedule, data)),
        unload: () => dispatch(apiActions.stopCall(api.schedule))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Schedule);