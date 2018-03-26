import React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

import Spinner from "../../common/spinner";
import Header from "../../common/header";
import Create from "../../common/create";
import NoResults from "../../common/noresult";

import * as api from "../../api";
import * as apiActions from "../../actions/api";
import { isLoading, isArray, getResult } from "../../utils/apiHelpers";
import groupBy from "../../utils/groupBy";

class PersonalTrainers extends React.Component {
    componentDidMount() {
        this.props.getPersonalTrainers();
    }

    render() {
        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Header>Personal Trainers</Header>
                <Create to="/admin/personaltrainers/new">Personal Trainer toevoegen</Create>
                {this.list()}
            </div>
        );
    }

    list() {
        if (this.props.personaltrainers.length === 0) {
            return <NoResults />;
        }


        return groupBy(this.props.personaltrainers, "emailConfirmed").map(group => {
            const href = group.key ? "confirmed" : "unconfirmed";
            return (
                <div key={group.key}>
                    <button className="btn btn-link" data-toggle="collapse" data-target={"#" + href} type="button">
                        <span className="lead">{group.key ? "Bevestigd" : "Onbevestigd"}</span> <i className="fa fa-caret-down" />
                    </button>

                    <div className="collapse show" id={href}>
                        <div className="list-group">
                            {
                                group.values.sort((a, b) => a.name.localeCompare(b.name)).map(trainer => {
                                    const to = "/admin/personaltrainers/details/" + trainer.id;
                                    const confirmed = trainer.emailConfirmed ? "" : " list-group-item-secondary"
                                    return (
                                        <Link key={trainer.id} to={to} className={"list-group-item list-group-item-action" + confirmed}>
                                            {trainer.name}
                                        </Link>
                                    );
                                })
                            }
                        </div>
                    </div>

                    <hr className="mt-4" />
                </div>
            );
        });


        if (this.props.personaltrainers.length === 0) {
            return <NoResults />;
        }

        return (
            <div className="list-group">
                {
                    this.props.personaltrainers.map(trainer => {
                        const to = "/admin/personaltrainers/details/" + trainer.id;
                        return (
                            <Link key={trainer.id} to={to} className="list-group-item list-group-item-action">
                                {trainer.name}
                            </Link>
                        );
                    })
                }
            </div>
        );
    }
}

function mapStateToProps(state) {
    const personaltrainers = getResult(state, api.personaltrainers);
    const loading = isLoading(state, [api.personaltrainers]) || !isArray(state, [api.personaltrainers]);
    return {
        personaltrainers,
        loading
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getPersonalTrainers: () => dispatch(apiActions.get(api.personaltrainers))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(PersonalTrainers);