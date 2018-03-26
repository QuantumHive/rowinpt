import React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

import Spinner from "../../common/spinner";
import Back from "../../common/back";
import Submit from "../../common/submit";

import * as apiActions from "../../actions/api";
import * as api from "../../api";
import { isLoading, getResult, isArray } from "../../utils/apiHelpers";

import moment from "moment";

class CustomerActivity extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            timeOutId: "",
            init: false,
            notes: ""
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.submit = this.submit.bind(this);
    }

    componentDidMount() {
        this.props.getAbsentCustomer(this.props.match.params.id);
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.submitted) {
            this.props.getAbsentCustomer(this.props.match.params.id);
            this.setState({ init: true });
        } else if (this.state.init && !nextProps.loading && this.state.timeOutId === "") {
            const timeOutId = window.setTimeout(() => {
                this.setState({ timeOutId: "" });
            }, 3000);

            this.setState({ timeOutId });
        }

        if (!nextProps.loading) {
            this.setState({ notes: nextProps.customer.notes });
        }
    }

    componentWillUnmount() {
        if (this.state.timeOutId !== "") {
            window.clearTimeout(this.state.timeOutId);
        }
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        this.setState({ notes: value });
    }

    submit() {
        this.props.saveNotes({
            notes: this.state.notes,
            customerId: this.props.match.params.id
        });
    }

    render() {
        if (this.props.loading) {
            return <Spinner />;
        }

        const toCustomer = "/admin/customers/details/" + this.props.match.params.id;

        return (
            <div>
                <Back to="/admin/absentees" header="Afwezigheid" />

                <Link to={toCustomer} className="btn btn-warning btn-block">Klant details</Link>

                <table className="table mt-3">
                    <tbody>
                        <tr>
                            <th>Naam</th>
                            <td>{this.props.customer.name}</td>
                        </tr>
                        <tr>
                            <th>Email</th>
                            <td style={{ wordBreak: "break-word" }}><a href={`mailto:${this.props.customer.email}`}><i className="fa fa-envelope" /> {this.props.customer.email}</a></td>
                        </tr>
                        <tr>
                            <th>Telefoon</th>
                            <td><a href={`tel:${this.props.customer.phone}`}><i className="fa fa-phone" /> {this.props.customer.phone}</a></td>
                        </tr>
                    </tbody>
                </table>

                <hr className="my-3" />

                <h4 className="font-weight-normal">Activiteit</h4>
                <div className="list-group mb-3">
                    {
                        this.props.customer.activity.map(activity => {
                            return (
                                <div key={activity.courseTypeId} className="list-group-item d-flex flex-column">
                                    <span className="mb-2">{activity.subscription}</span>
                                    <small className="text-muted">Recentste aanmelding: <strong>{activity.lastSeen === null ? "nooit" : moment(activity.lastSeen).format("LL")}</strong></small>
                                </div>
                            );
                        })
                    }
                </div>

                <form>
                    <div className="form-group">
                        <label>Notities</label>
                        <textarea className="form-control" rows="3" value={this.state.notes} onChange={this.handleInputChange} />
                        <small className="form-text text-muted">
                            Laatst gewijzigd op: <strong>{this.props.customer.lastUpdatedOn === null ? "-" : moment(this.props.customer.lastUpdatedOn).format("DD-MM-YYYY, HH:mm")}</strong>
                        </small>
                        <small className="form-text text-muted">
                            Laatst gewijzigd door: <strong>{this.props.customer.lastUpdatedBy === null ? "-" : this.props.customer.lastUpdatedBy}</strong>
                        </small>
                    </div>

                    <Submit onSubmit={this.submit}>Opslaan</Submit>
                </form>

                <div className={"alert alert-primary toast " + (this.state.timeOutId === "" ? "invisible" : "visible")} role="alert">
                    Notities opgeslagen!
                </div>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const customer = getResult(state, api.absentees);
    const loading = isLoading(state, [api.absentees]) || isArray(state, [api.absentees]);

    return {
        customer,
        loading,
        submitted: state.api.hasOwnProperty(api.absentees) && state.api[api.absentees].submitted
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getAbsentCustomer: id => dispatch(apiActions.get(api.absentees, `details/${id}`)),
        saveNotes: payload => dispatch(apiActions.put(api.absentees, payload, "save/notes"))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CustomerActivity);