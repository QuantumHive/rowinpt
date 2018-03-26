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

class CourseTypes extends React.Component {
    componentDidMount() {
        this.props.getCourseTypes();
    }

    render() {
        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Header>Lesvormen</Header>
                <Create to="/admin/coursetypes/new">Lesvorm toevoegen</Create>
                {this.list()}
            </div>
        );
    }

    list() {
        if (this.props.coursetypes.length === 0) {
            return <NoResults />;
        }

        return (
            <div className="list-group">
                {
                    this.props.coursetypes.map(courseType => {
                        const to = "/admin/coursetypes/details/" + courseType.id;
                        return (
                            <Link key={courseType.id} to={to} className="list-group-item list-group-item-action">
                                {courseType.name}
                            </Link>
                        );
                    })
                }
            </div>
        );
    }
}

function mapStateToProps(state) {
    const coursetypes = getResult(state, api.coursetypes);
    const loading = isLoading(state, [api.coursetypes]) || !isArray(state, [api.coursetypes]);
    return {
        coursetypes,
        loading
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getCourseTypes: () => dispatch(apiActions.get(api.coursetypes))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CourseTypes);