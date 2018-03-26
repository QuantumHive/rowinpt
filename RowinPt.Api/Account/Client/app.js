import React from "react";
import { connect } from "react-redux";
import { Route } from "react-router-dom";

import Login from "./login";
import Forgot from "./forgot";
import Activation from "./activation";
import Reset from "./reset";

class App extends React.Component {
    
    render() {
        return (
            <div>
                <div className="mb-4" id="root">
                    <Route exact path="/authentication/login" component={Login} />
                    <Route exact path="/account/forgot" component={Forgot} />
                    <Route exact path="/account/activate" component={Activation} />
                    <Route exact path="/account/reset" component={Reset} />
                </div>
            </div>
        );
    }
}

export default App;