import React from "react";
import { connect } from "react-redux";

import Header from "../../common/header";
import Spinner from "../../common/spinner";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";

import Chart from "./chart";

class Profile extends React.Component {
    componentDidMount() {
        this.props.loadProfile();
    }

    render() {
        if (this.props.loading || this.props.result === null) {
            return <Spinner />;
        }

        return (
            <div>
                <Header>Profiel</Header>

                <div className="row">
                    <div className="col-lg-6">
                        <h4 className="font-weight-normal">Gewicht</h4>
                        <Chart measurements={this.props.result} type="weight" />
                    </div>

                    <div className="col-lg-6">
                        <h4 className="font-weight-normal">Vetpercentage</h4>
                        <Chart measurements={this.props.result} type="fatPercentage" />
                    </div>
                </div>

                <h2 className="font-weight-normal">Omvang</h2>

                <div className="row">
                    <div className="col-lg-6">
                        <h4 className="font-weight-normal">Schouder</h4>
                        <Chart measurements={this.props.result} type="shoulderSize" />
                    </div>

                    <div className="col-lg-6">
                        <h4 className="font-weight-normal">Arm</h4>
                        <Chart measurements={this.props.result} type="armSize" />
                    </div>
                </div>

                <div className="row">
                    <div className="col-lg-6">
                        <h4 className="font-weight-normal">Buik</h4>
                        <Chart measurements={this.props.result} type="bellySize" />
                    </div>

                    <div className="col-lg-6">
                        <h4 className="font-weight-normal">Heup</h4>
                        <Chart measurements={this.props.result} type="waistSize" />
                    </div>
                </div>

                <div className="row">
                    <div className="col-lg-6">
                        <h4 className="font-weight-normal">Bovenbeen</h4>
                        <Chart measurements={this.props.result} type="upperLegSize" />
                    </div>
                </div>
                
            </div>
        );
    }
}

function mapStateToProps(state) {
    const result = getResult(state, api.profile);
    const loading = isLoading(state, [api.profile]);

    return {
        loading,
        result
    }
}

function mapDispatchToProps(dispatch) {
    return {
        loadProfile: () => dispatch(apiActions.get(api.profile))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Profile);