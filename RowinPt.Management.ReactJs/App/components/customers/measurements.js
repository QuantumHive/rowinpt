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
import validationRules from "./validationRulesMeasurements";

import { Calendar } from 'react-widgets';
import moment from "moment";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";

class CustomerMeasurements extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            data: this.defaultState(),
            validate: false,
            modal: false
        }

        this.handleInputChange = this.handleInputChange.bind(this);
        this.submit = this.submit.bind(this);
        this.changeDate = this.changeDate.bind(this);
        this.closeModal = this.closeModal.bind(this);
    }

    defaultState() {
        return {
            date: moment().startOf("month"),
            weight: "",
            fatPercentage: "",
            shoulderSize: "",
            armSize: "",
            bellySize: "",
            waistSize: "",
            upperLegSize: "",
        };
    }

    componentDidMount() {
        this.props.getMeasurements(this.props.match.params.id);
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading) {
            const measurement = this.props.measurements.find(m => moment(m.date).startOf("month").isSame(this.state.data.date));
            if (measurement !== undefined) {
                measurement.date = moment(measurement.date).startOf("month");
                this.setState({ data: measurement });
            }
        }
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState(prevState => ({ data: { ...prevState.data, [name]: value } }));
    }

    changeDate(value) {
        const date = moment(value).startOf("month");
        const measurement = this.props.measurements.find(m => moment(m.date).startOf("month").isSame(date, "month"));
        if (measurement !== undefined) {
            measurement.date = moment(measurement.date).startOf("month");
            this.setState({ data: measurement });
        } else {
            this.setState({ data: { ...this.defaultState(), date } });
        }
    }

    submit() {
        if (validation.isValid(this.state.data, validationRules) && this.validateMinimumRequired(this.state.data)) {
            this.props.submit({ ...this.state.data }, this.props.match.params.id);
            this.setState({ modal: true });
        } else {
            this.setState({ validate: true });
        }
    }

    closeModal() {
        this.props.getMeasurements(this.props.match.params.id);
    }

    validateMinimumRequired(state) {
        if (!this.isEmpty(state.weight)) return true;
        if (!this.isEmpty(state.fatPercentage)) return true;
        if (!this.isEmpty(state.shoulderSize)) return true;
        if (!this.isEmpty(state.armSize)) return true;
        if (!this.isEmpty(state.bellySize)) return true;
        if (!this.isEmpty(state.waistSize)) return true;
        if (!this.isEmpty(state.upperLegSize)) return true;

        return false;
    }

    isEmpty(data) {
        return data === null || data === "" || data === undefined;
    }

    render() {
        if (this.props.loading && !this.props.submitted) {
            return <Spinner />;
        }

        return (
            <div>
                <Back to={`/admin/customers/details/${this.props.match.params.id}`} header="Meetgegevens" />

                <form>
                    <label>Maand</label>
                    <div className="form-group">
                        <Calendar
                            value={this.state.data.date.toDate()}
                            onChange={this.changeDate}
                            views={["year", "decade", "century"]}
                            footerFormat={"MMMM YYYY"}
                            min={moment().startOf("month").subtract(1, "year").toDate()}
                            max={moment().startOf("month").add(1, "month").toDate()}/>
                    </div>

                    <div className="form-group">
                        <label>Gewicht</label>
                        <div className="input-group">
                            <input type="number" name="weight" className={validation.formControl(this.state.data.weight, validationRules.weight, this.state.validate)} placeholder="Gewicht" value={this.state.data.weight} onChange={this.handleInputChange} />
                            <div className="input-group-append">
                                <span className="input-group-text">kg</span>
                            </div>
                            <ValidationFeedback validate={this.state.data.weight} rules={validationRules.weight} />
                        </div>
                    </div>

                    <div className="form-group">
                        <label>Vet percentage</label>
                        <div className="input-group">
                            <input type="number" name="fatPercentage" className={validation.formControl(this.state.data.fatPercentage, validationRules.fatPercentage, this.state.validate)} placeholder="Vet percentage" value={this.state.data.fatPercentage} onChange={this.handleInputChange} />
                            <div className="input-group-append">
                                <span className="input-group-text">%</span>
                            </div>
                            <ValidationFeedback validate={this.state.data.fatPercentage} rules={validationRules.fatPercentage} />
                        </div>
                    </div>

                    <h5>Omvang</h5>

                    <div className="form-group">
                        <label>Schouder</label>
                        <div className="input-group">
                            <input type="number" name="shoulderSize" className={validation.formControl(this.state.data.shoulderSize, validationRules.shoulderSize, this.state.validate)} placeholder="Schouder" value={this.state.data.shoulderSize} onChange={this.handleInputChange} />
                            <div className="input-group-append">
                                <span className="input-group-text">cm</span>
                            </div>
                            <ValidationFeedback validate={this.state.data.shoulderSize} rules={validationRules.shoulderSize} />
                        </div>
                    </div>
                    <div className="form-group">
                        <label>Arm</label>
                        <div className="input-group">
                            <input type="number" name="armSize" className={validation.formControl(this.state.data.armSize, validationRules.armSize, this.state.validate)} placeholder="Arm" value={this.state.data.armSize} onChange={this.handleInputChange} />
                            <div className="input-group-append">
                                <span className="input-group-text">cm</span>
                            </div>
                            <ValidationFeedback validate={this.state.data.armSize} rules={validationRules.armSize} />
                        </div>
                    </div>
                    <div className="form-group">
                        <label>Buik</label>
                        <div className="input-group">
                            <input type="number" name="bellySize" className={validation.formControl(this.state.data.bellySize, validationRules.bellySize, this.state.validate)} placeholder="Buik" value={this.state.data.bellySize} onChange={this.handleInputChange} />
                            <div className="input-group-append">
                                <span className="input-group-text">cm</span>
                            </div>
                            <ValidationFeedback validate={this.state.data.bellySize} rules={validationRules.bellySize} />
                        </div>
                    </div>
                    <div className="form-group">
                        <label>Heup</label>
                        <div className="input-group">
                            <input type="number" name="waistSize" className={validation.formControl(this.state.data.waistSize, validationRules.waistSize, this.state.validate)} placeholder="Heup" value={this.state.data.waistSize} onChange={this.handleInputChange} />
                            <div className="input-group-append">
                                <span className="input-group-text">cm</span>
                            </div>
                            <ValidationFeedback validate={this.state.data.waistSize} rules={validationRules.waistSize} />
                        </div>
                    </div>
                    <div className="form-group">
                        <label>Bovenbeen</label>
                        <div className="input-group">
                            <input type="number" name="upperLegSize" className={validation.formControl(this.state.data.upperLegSize, validationRules.upperLegSize, this.state.validate)} placeholder="Bovenbeen" value={this.state.data.upperLegSize} onChange={this.handleInputChange} />
                            <div className="input-group-append">
                                <span className="input-group-text">cm</span>
                            </div>
                            <ValidationFeedback validate={this.state.data.upperLegSize} rules={validationRules.upperLegSize} />
                        </div>
                    </div>

                    {this.generalValidationMessage()}

                    <Submit onSubmit={this.submit}>Opslaan</Submit>
                </form>

                <Modal isOpen={this.state.modal && this.props.submitted} toggle={this.closeModal}>
                    <ModalHeader toggle={this.closeModal}>
                        <span className="text-success">Meting opgeslagen</span>
                    </ModalHeader>
                    <ModalBody>
                        <div className="alert alert-success">
                            De meetgegevens zijn met succes opgeslagen!
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        <button type="button" className="btn btn-primary" onClick={this.closeModal}>Ok</button>
                    </ModalFooter>
                </Modal>
            </div>
        );
    }

    generalValidationMessage() {
        if (!this.validateMinimumRequired(this.state.data) && this.state.validate) {
            return (
                <p className="text-danger">
                    Voer minimaal 1 meting in
            </p>
            );
        }
        return null;
    }
}

function mapStateToProps(state) {
    const measurements = getResult(state, api.customers);
    const loading = isLoading(state, [api.customers]) || !isArray(state, [api.customers]);

    if (!loading) {
        for (var index in measurements) {
            const measurement = measurements[index];
            if (measurement.weight === null) {
                measurement.weight = "";
            }
            if (measurement.fatPercentage === null) {
                measurement.fatPercentage = "";
            }
            if (measurement.armSize === null) {
                measurement.armSize = "";
            }
            if (measurement.shoulderSize === null) {
                measurement.shoulderSize = "";
            }
            if (measurement.bellySize === null) {
                measurement.bellySize = "";
            }
            if (measurement.waistSize === null) {
                measurement.waistSize = "";
            }
            if (measurement.upperLegSize === null) {
                measurement.upperLegSize = "";
            }
        }
    }
    

    return {
        measurements,
        loading,
        submitted: state.api.hasOwnProperty(api.customers) && state.api[api.customers].submitted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getMeasurements: id => dispatch(apiActions.get(api.customers, "measurements/" + id)),
        submit: (payload, id) => dispatch(apiActions.put(api.customers, payload, "measurements/" + id))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CustomerMeasurements);