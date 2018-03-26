import React from "react";
import moment from "moment";
import { DateTimePicker } from "react-widgets";

class Subscription extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            data: {
                courseTypeId: props.courseTypes[0].id,
                credits: 1,
                notes: "",
                startDate: moment().startOf("day"),
            },
            validation: undefined
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.submit = this.submit.bind(this);
        this.remove = this.remove.bind(this);
        this.handleStartDate = this.handleStartDate.bind(this);
    }

    handleStartDate(value) {
        this.setState(prevState => ({ data: { ...prevState.data, startDate: value === null || value === "" ? null : moment(value) } }));
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState(prevState => {
            const newState = { ...prevState };
            newState.data = { ...prevState.data };
            newState.data[name] = value;
            return newState;
        });
    }

    submit() {
        let validation = undefined;
        if (this.state.data.startDate === "" || this.state.data.startDate === null) {
            validation = "Ingangsdatum is verplicht";
        }

        this.setState({ validation });

        if (validation === undefined) {
            const subscriptions = [...this.props.subscriptions];

            const index = subscriptions.findIndex(s => s.courseTypeId === this.state.data.courseTypeId);
            if (index === -1) {
                subscriptions.push({ ...this.state.data });
            } else {
                subscriptions[index] = { ...this.state.data };
            }

            this.props.chooseSubscription(subscriptions);
        }
    }

    remove(courseTypeId) {
        const subscriptions = [...this.props.subscriptions];
        const index = subscriptions.findIndex(s => s.courseTypeId === courseTypeId);
        subscriptions.splice(index, 1);
        this.props.chooseSubscription(subscriptions);
    }

    render() {
        return (
            <div>
                <hr className="mt-4" />
                <h4>Abonnementen</h4>
                <div className="form-group">
                    <label>Abonnement</label>
                    <select className="custom-select" onChange={this.handleInputChange} value={this.state.data.courseTypeId} name="courseTypeId" >
                        {
                            this.props.courseTypes.map(courseType => {
                                return (
                                    <option key={courseType.id} value={courseType.id}>{courseType.name}</option>
                                );
                            })
                        }
                    </select>
                </div>
                <div className="form-group">
                    <label>Wekelijkse tegoed</label>
                    <select className="custom-select" onChange={this.handleInputChange} name="credits" value={this.state.data.credits}>
                        {
                            [1, 2, 3, 4, 5, 6, 7].map(credit => {
                                return (
                                    <option key={credit} value={credit}>{credit} x</option>
                                );
                            })
                        }
                    </select>
                </div>

                <div className="form-group">
                    <label>Ingangsdatum</label>
                    <DateTimePicker time={false} placeholder="dd-mm-yyyy" value={this.state.data.startDate === null ? null : this.state.data.startDate.toDate()} onChange={this.handleStartDate} />
                    {
                        this.props.validation === null ? null :
                            <small className="form-text text-danger">{this.state.validation}</small>
                    }
                </div>

                <div className="form-group">
                    <label>Notities</label>
                    <input type="text" className="form-control" placeholder="Notities" onChange={this.handleInputChange} value={this.state.data.notes} name="notes" />
                </div>


                <button type="button" className="btn btn-success mb-3" onClick={this.submit}>Abonnement toevoegen</button>

                <div className="form-group">
                    <h6>Gekozen abonnementen</h6>
                    {
                        this.props.subscriptions.length === 0 ?
                            <div className="alert alert-info">
                                Geen gekozen abonnementen
                            </div> :
                            <ul className="list-group">
                                {
                                    this.props.subscriptions.map(subscription => {
                                        const courseTypeSubscription = this.props.courseTypes.find(c => c.id === subscription.courseTypeId).name;
                                        let notes = subscription.notes !== "" && subscription.notes !== null ? <p className="mb-1">Notities: <em>{subscription.notes}</em></p> : null;
                                        return (
                                            <li key={subscription.courseTypeId} className="list-group-item flex-column">
                                                <div className="d-flex justify-content-between align-items-start">
                                                    <h5>{courseTypeSubscription}</h5>
                                                    <span className="badge badge-secondary badge-pill">{subscription.credits}x</span>
                                                </div>
                                                <div className="d-flex flex-column align-items-start">
                                                    {notes}
                                                    <button type="button" className="btn btn-outline-danger btn-sm align-self-end" onClick={() => this.remove(subscription.courseTypeId)}>Abonnement opheffen</button>
                                                </div>
                                                <hr className="mb-0" />
                                                <small>Ingangsdatum: {subscription.startDate.format("L")}</small>
                                            </li>
                                        );
                                    })
                                }
                            </ul>
                    }
                </div>
            </div>
        );
    }
}

export default Subscription;