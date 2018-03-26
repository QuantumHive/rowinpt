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

class Customers extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            search: ""
        };

        this.handleInputChange = this.handleInputChange.bind(this);
    }

    componentDidMount() {
        this.props.getCustomers();
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        this.setState({ search: value });
    }

    render() {
        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Header>Klanten</Header>
                <Create to="/admin/customers/new">Klant toevoegen</Create>

                <div className="input-group ">
                    <div className="input-group-prepend">
                        <span className="input-group-text"><i className="fa fa-search" /></span>
                    </div>
                    <input type="text" className="form-control" placeholder="Zoek op naam of klantnummer" onChange={this.handleInputChange} value={this.state.search} />
                </div>

                {this.list()}
            </div>
        );
    }

    list() {
        const customers = this.props.customers.filter(customer => {
            const filter = this.state.search.toLocaleLowerCase();
            const name = customer.name.toLocaleLowerCase();
            const number = customer.number === null ? "" : customer.number.toLocaleLowerCase();

            if (filter !== "") {
                return name.includes(filter) || number.includes(filter);
            }

            return true;
        }).sort((a, b) => a.emailConfirmed || b.emailConfirmed);

        if (customers.length === 0) {
            return <div className="mt-3"><NoResults /></div>;
        }

        return groupBy(customers, "emailConfirmed").map(group => {
            const href = group.key ? "confirmed" : "unconfirmed";
            return (
                <div key={group.key}>
                    <button className="btn btn-link" data-toggle="collapse" data-target={"#" + href} type="button">
                        <span className="lead">{group.key ? "Bevestigd" : "Onbevestigd"}</span> <i className="fa fa-caret-down" />
                        <span className="badge badge-pill badge-secondary ml-2">{group.values.length}</span>
                    </button>
                    


                    <div className="collapse show" id={href}>
                        <div className="list-group">
                            {
                                group.values.sort((a, b) => a.name.localeCompare(b.name)).map(customer => {
                                    const to = "/admin/customers/details/" + customer.id;
                                    const confirmed = customer.emailConfirmed ? "" : " list-group-item-secondary"
                                    return (
                                        <Link key={customer.id} to={to} className={"list-group-item list-group-item-action" + confirmed}>
                                            {customer.name}
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
    }
}

function mapStateToProps(state) {
    const customers = getResult(state, api.customers);
    const loading = isLoading(state, [api.customers]) || !isArray(state, [api.customers]);
    return {
        customers,
        loading
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getCustomers: () => dispatch(apiActions.get(api.customers))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Customers);