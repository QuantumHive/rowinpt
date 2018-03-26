import React from "react";
import { connect } from "react-redux";
import { Route } from "react-router-dom";

import Navigation from "./navigation";

import Agenda from "./components/agenda";
import AgendaDetails from "./components/agenda/details";
import Schedule from "./components/schedule";
import Profile from "./components/profile";
import About from "./components/settings/about";
import ChangePassword from "./components/settings/changepassword";

import ErrorPage from "./components/error";
import ErrorHandler from "./common/errorHandler";
import ValidationModal from "./common/validationModal";

class App extends React.Component {
    render() {
        return (
            <div>
                <div className="mb-4" id="root">
                    <header className="">
                        <Route path="/" render={() => <Navigation logout={this.props.logout} />} />
                    </header>
                    <section className="container pt-3">
                        <Route exact path="/" component={Agenda} />
                        <Route exact path="/agenda/:id" component={AgendaDetails} />
                        <Route exact path="/schedule" component={Schedule} />
                        <Route exact path="/profile" component={Profile} />
                        <Route exact path="/settings/about" component={About} />
                        <Route exact path="/settings/changepassword" component={ChangePassword} />

                        <Route exact path="/error" component={ErrorPage} />
                    </section>

                    <ValidationModal />
                    <ErrorHandler />
                </div>
            </div>
        );
    }
}

export default App;