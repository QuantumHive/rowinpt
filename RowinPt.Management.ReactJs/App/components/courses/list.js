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

class Courses extends React.Component {
    componentDidMount() {
        this.props.getCourses();
        this.props.getCourseTypes();
    }

    render() {
        if (this.props.loading) {
            return <Spinner />;
        }

        return (
            <div>
                <Header>Lessen</Header>
                <Create to="/admin/courses/new">Les toevoegen</Create>
                {this.list()}
            </div>
        );
    }

    list() {
        if (this.props.courses.length === 0) {
            return <NoResults />;
        }

        return groupBy(this.props.courses, "courseTypeId").map(group => {
            return (
                <div key={group.key}>
                    <p className="lead">
                        {this.props.coursetypes.find(ct => ct.id === group.key).name}
                    </p>
                    <div className="list-group mb-3">
                        {
                            group.values.map(course => {
                                const to = "/admin/courses/details/" + course.id;
                                return (
                                    <Link key={course.id} to={to} className="list-group-item list-group-item-action">
                                        {course.name}
                                    </Link>
                                );
                            })
                        }
                    </div>
                </div>
            );
        });
    }
}

function mapStateToProps(state) {
    const courses = getResult(state, api.courses);
    const coursetypes = getResult(state, api.coursetypes);
    const loading = isLoading(state, [api.coursetypes, api.courses]) || !isArray(state, [api.coursetypes, api.courses]);

    return {
        courses,
        coursetypes,
        loading
    };
}

function mapDispatchToProps(dispatch) {
    return {
        getCourses: () => dispatch(apiActions.get(api.courses)),
        getCourseTypes: () => dispatch(apiActions.get(api.coursetypes))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Courses);