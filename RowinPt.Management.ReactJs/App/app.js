import React from "react";
import { connect } from "react-redux";
import { Route } from "react-router-dom";

import Navigation from "./navigation";

import Agenda from "./components/agenda";
import CourseTypes from "./components/coursetypes";
import Courses from "./components/courses";
import Locations from "./components/locations";
import Customers from "./components/customers";
import PersonalTrainers from "./components/personaltrainers";
import Schedule from "./components/schedule";
import ScheduleItems from "./components/scheduleitems";
import About from "./components/settings/about";
import ChangePassword from "./components/settings/changepassword";
import Absentees from "./components/absentees";

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
                        <Route path="/" component={Agenda} />
                        <Route path="/admin/coursetypes" component={CourseTypes} />
                        <Route path="/admin/courses" component={Courses} />
                        <Route path="/admin/schedule" component={Schedule} />
                        <Route path="/admin/scheduleitems" component={ScheduleItems} />
                        <Route path="/admin/locations" component={Locations} />
                        <Route path="/admin/customers" component={Customers} />
                        <Route path="/admin/personaltrainers" component={PersonalTrainers} />
                        <Route exact path="/admin/settings/about" component={About} />
                        <Route exact path="/admin/settings/changepassword" component={ChangePassword} />
                        <Route path="/admin/absentees" component={Absentees} />

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