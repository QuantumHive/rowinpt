import React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

import Spinner from "../../common/spinner";
import Back from "../../common/back";
import Submit from "../../common/submit";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";

import * as validation from "../../common/validation";
import ValidationFeedback from "../../common/validationFeedback";
import validationRules from "./validationRules";

import DatetimePicker from "./datetimepicker";
import moment from "moment";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";

class ScheduleItems extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            data: {
                scheduleId: "",
                date: moment().startOf("day"),
                start: "12:00:00",
                end: "13:00:00",
                personalTrainerId: "",
                courseId: "",
                repeat: 0
            },
            validate: false,
            modal: false
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.submit = this.submit.bind(this);
        this.changeDate = this.changeDate.bind(this);
        this.closeModal = this.closeModal.bind(this);
    }

    componentDidMount() {
        this.props.getSchedule(this.props.match.params.id);
        this.props.getLocations();
        this.props.getPersonalTrainers();
        this.props.getCourses();
        this.props.getCourseTypes();
        this.props.stopCall();
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.loading) {
            if (this.state.data.courseId === "" && nextProps.courses.length > 0) {
                const course = nextProps.courses[0];
                this.setState(prevState => ({ data: { ...prevState.data, courseId: course.id } }));
            }
        }

        if (nextProps.submitted) {
            this.setState({ modal: true });
        }
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState(prevState => ({ data: { ...prevState.data, [name]: value } }));
    }

    changeDate(datetime) {
        this.setState(prevState => ({
            data: {
                ...prevState.data,
                date: datetime.date,
                start: datetime.start,
                end: datetime.end
            }
        }));
    }

    closeModal() {
        this.setState({ modal: false });
        this.props.stopCall();
    }

    submit() {
        if (validation.isValid(this.state.data, validationRules)) {
            const data = { ...this.state.data };
            data.scheduleId = this.props.match.params.id
            this.props.submit({ ...data }, this.props.match.params.id);
        } else {
            this.setState({ validate: true });
        }
    }

    render() {
        if (this.props.loading && !this.props.submitted) {
            return <Spinner />;
        }

        if (this.props.courses.length === 0) {
            return (
                <div className="alert alert-warning text-center">
                    <p>Er bestaan nog geen lessen. Maak eerst een les aan.</p>
                    <Link to="/admin/courses/new" className="btn btn-primary">Les aanmaken</Link>
                </div>
            );
        }

        const location = this.props.locations.find(l => l.id === this.props.schedule.locationId);

        return (
            <div>
                <Back to={`/admin/schedule/details/${this.props.schedule.id}`} header="Planning items" />

                <table className="table">
                    <tbody>
                        <tr>
                            <th>Naam</th>
                            <td>{this.props.schedule.name}</td>
                        </tr>
                        <tr>
                            <th>Locatie</th>
                            <td>{location.name}</td>
                        </tr>
                    </tbody>
                </table>

                <form>
                    <DatetimePicker onChange={this.changeDate} data={this.state.data} />

                    <div className="form-group">
                        <label>Personal trainer</label>
                        <select name="personalTrainerId" className="custom-select" value={this.state.data.personalTrainerId} onChange={this.handleInputChange}>
                            <option value="">Niemand</option>
                            {
                                this.props.personaltrainers.map(trainer => {
                                    return (
                                        <option key={trainer.id} value={trainer.id}>{trainer.name}</option>
                                    );
                                })
                            }
                        </select>
                    </div>

                    <div className="form-group">
                        <label>Les</label>
                        <select name="courseId" className="custom-select" value={this.state.data.courseId} onChange={this.handleInputChange}>
                            {
                                this.props.coursetypes.map(courseType => {
                                    const courses = this.props.courses.filter(c => c.courseTypeId === courseType.id);
                                    return (
                                        <optgroup key={courseType.id} label={courseType.name}>
                                            {
                                                courses.map(course => {
                                                    return (
                                                        <option key={course.id} value={course.id}>{course.name}</option>
                                                    );
                                                })
                                            }
                                        </optgroup>
                                    );
                                })
                            }
                        </select>
                    </div>

                    <div className="form-group">
                        <label>Vooruit plannen</label>
                        <div className="input-group">
                            <input type="number" className={validation.formControl(this.state.data.repeat, validationRules.repeat, this.state.validate)} placeholder="Vooruit plannen" value={this.state.data.repeat} onChange={this.handleInputChange} name="repeat" />
                            <div className="input-group-append">
                                <span className="input-group-text" >weken</span>
                            </div>
                            <ValidationFeedback validate={this.state.data.repeat} rules={validationRules.repeat} />
                        </div>
                    </div>

                    <Submit onSubmit={this.submit}>Inplannen</Submit>
                </form>

                <Modal isOpen={this.state.modal && this.props.submitted} toggle={this.closeModal}>
                    <ModalHeader toggle={this.closeModal}>
                        <span className="text-success">Item ingepland</span>
                    </ModalHeader>
                    <ModalBody>
                        <div className="alert alert-success">
                            De planning item is met succes toegevoegd aan de planning!
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        <button type="button" className="btn btn-primary" onClick={this.closeModal}>Ok</button>
                    </ModalFooter>
                </Modal>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const schedule = getResult(state, api.schedule);
    const locations = getResult(state, api.locations);
    const personaltrainers = getResult(state, api.personaltrainers);
    const courses = getResult(state, api.courses);
    const coursetypes = getResult(state, api.coursetypes);

    const loading = isLoading(state, [api.schedule, api.locations, api.personaltrainers, api.courses, api.coursetypes])
        || isArray(state, [api.schedule]) || !isArray(state, [api.locations, api.personaltrainers, api.courses, api.coursetypes])
        || (state.api.hasOwnProperty(api.scheduleitems) && state.api[api.scheduleitems].loading )

    return {
        schedule,
        locations,
        personaltrainers,
        courses,
        coursetypes,
        loading,
        submitted: state.api.hasOwnProperty(api.scheduleitems) && state.api[api.scheduleitems].submitted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getSchedule: id => dispatch(apiActions.get(api.schedule, id)),
        getLocations: () => dispatch(apiActions.get(api.locations)),
        getPersonalTrainers: () => dispatch(apiActions.get(api.personaltrainers)),
        getCourses: () => dispatch(apiActions.get(api.courses)),
        getCourseTypes: () => dispatch(apiActions.get(api.coursetypes)),
        submit: payload => dispatch(apiActions.post(api.scheduleitems, payload)),
        stopCall: () => dispatch(apiActions.stopCall(api.scheduleitems))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ScheduleItems);