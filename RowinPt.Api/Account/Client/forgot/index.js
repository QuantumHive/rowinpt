import React from "react";
import { connect } from "react-redux";
import { Link } from 'react-router-dom';
import axios from "axios";

class Forgot extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            email: "",
            invalid: false,
            sent: false
        };

        this.submit = this.submit.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState({ [name]: value });
    }

    submit () {
        if (this.state.email === "" || this.state.email === null) {
            this.setState({ invalid: true });
        } else {
            axios.post("/account/password/forgot", { email: this.state.email });
            this.setState({ sent: true });
        }
    }

    render() {
        if (this.state.sent) {
            return (
                <div className="container">
                    <div className="jumbotron my-5 py-4">
                        <div className="alert alert-success">
                            We hebben een mail verstuurd naar <em>{this.state.email}</em> met instructies om je wachtwoord te resetten.
                        </div>
                        <Link to="/authentication/login" className="btn btn-secondary">Terug naar login</Link>
                    </div>
                </div>
            );
        }

        return (
            <div className="container">
                <div className="jumbotron my-5 pb-4 pb-sm-5">
                    <p className="lead">Ben je je wachtwoord vergeten? Geen paniek!</p>
                    <p>Voer hieronder je email adres in en je ontvangt een email met daarin instructies om je wachtwoord te resetten.</p>
                    <hr className="my-4" />
                    <form className="form-inline">
                        <input type="email" name="email" className={`form-control mr-3 mb-3 mb-sm-0 ${this.state.invalid ? "is-invalid" : ""}`} placeholder="Email" autoFocus={true} value={this.state.email} onChange={this.handleInputChange} />
                        <button type="button" className="btn btn-primary" onClick={this.submit}>Versturen</button>
                    </form>
                    <div className="d-flex mt-1 mt-sm-5 flex-row-reverse">
                        <Link to="/authentication/login" className="btn btn-sm btn-secondary">Terug naar login</Link>
                    </div>
                </div>
            </div>
        );
    }
}

export default Forgot;