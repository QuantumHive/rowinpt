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

class Courses extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            name: "",
            capacity: "",
            courseTypeId: "",
            validate: false
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.submit = this.submit.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading && nextProps.coursetypes.length > 0) {
            const courseType = nextProps.coursetypes[0];
            this.setState({
                capacity: courseType.capacity,
                courseTypeId: courseType.id
            });
        }
    }

    componentDidMount() {
        this.props.unload();
        this.props.getCourseTypes();
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        if (name === "courseTypeId") {
            const courseType = this.props.coursetypes.find(ct => ct.id === value);
            this.setState({
                capacity: courseType.capacity,
                courseTypeId: courseType.id
            });
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
            return <Redirect exact to="/admin/courses/list" />;
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
                <Back to="/admin/courses/list" header="Les" />

                <form>
                    <div className="form-group">
                        <label>Lesvorm</label>
                        <select name="courseTypeId" className="custom-select" value={this.state.courseTypeId} onChange={this.handleInputChange}>
                            {
                                this.props.coursetypes.map(courseType => {
                                    return (
                                        <option key={courseType.id} value={courseType.id}>{courseType.name}</option>
                                    );
                                })
                            }
                        </select>
                    </div>

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
    const coursetypes = getResult(state, api.coursetypes);
    const loading = isLoading(state, [api.coursetypes]) || !isArray(state, [api.coursetypes]) || state.api[api.courses].loading;
    return {
        coursetypes,
        loading,
        submitted: state.api.hasOwnProperty(api.courses) && state.api[api.courses].submitted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getCourseTypes: () => dispatch(apiActions.get(api.coursetypes)),
        submit: data => dispatch(apiActions.post(api.courses, data)),
        unload: () => dispatch(apiActions.stopCall(api.courses)),
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Courses);