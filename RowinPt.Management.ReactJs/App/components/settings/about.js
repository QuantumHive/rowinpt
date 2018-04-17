import React from "react";
import { connect } from "react-redux";

class About extends React.Component {
    render() {
        return (
            <div>
                <div className="jumbotron py-4">
                    <h2 className="font-weight-normal">Informatie</h2>

                    <table className="table table-sm">
                        <tbody>
                            <tr>
                                <th>App</th>
                                <td>{this.props.applicationTitle} Beheer</td>
                            </tr>
                            <tr>
                                <th>Client Versie</th>
                                <td>{this.props.version}</td>
                            </tr>
                            <tr>
                                <th>API Versie</th>
                                <td>{this.props.apiVersion}</td>
                            </tr>
                            <tr>
                                <th>Type</th>
                                <td>Web App</td>
                            </tr>
                        </tbody>
                        
                    </table>

                    <hr className="my-4" />

                    <p className="lead">
                        Technologie:
                    </p>

                    <table className="table table-sm">
                        <tbody>
                            <tr>
                                <td><a href="https://dotnet.github.io/" className="">.NET Core</a></td>
                                <td><a href="https://azure.microsoft.com/" className="">Azure</a></td>
                                <td><a href="https://reactjs.org/" className="">React</a></td>
                                <td><a href="http://getbootstrap.com/" className="">Bootstrap</a></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <p>
                    Copyright <i className="fa fa-copyright"/> 2018
                    <br />
                    Rowin Enckhof Personal Training
                    <br />
                    Gemaakt door <em>Alper Aslan Apps</em>
                </p>
            </div>
        );
    }
}

function mapStateToProps(state) {
    return {
        ...state.settings
    };
}

export default connect(
    mapStateToProps
)(About);