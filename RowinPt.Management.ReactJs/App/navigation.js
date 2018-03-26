import * as React from "react";
import { connect } from "react-redux";
import { Link, NavLink } from "react-router-dom";
import { Collapse } from "reactstrap";

class NavMenu extends React.Component {
    constructor(props) {
        super(props);

        this.navBarCollapseClick = this.navBarCollapseClick.bind(this);
    }

    navBarCollapseClick() {
        $("#navigationBar").collapse("hide");
    }

    render() {
        return (
            <nav className="navbar navbar-expand-md navbar-light bg-light">
                <div className="container">
                    <Link to="/" className="navbar-brand d-block d-md-none">RowinPt Beheer</Link>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigationBar">
                        <span className="navbar-toggler-icon" />
                    </button>

                    <div className="collapse navbar-collapse justify-content-between" id="navigationBar">
                        <div className="navbar-nav">
                            <NavLink exact to="/" className="nav-link nav-item" onClick={this.navBarCollapseClick}> Agenda</NavLink>
                            <NavLink exact to="/admin/schedule/list" className="nav-link nav-item" onClick={this.navBarCollapseClick}> Planning</NavLink>

                            <NavLink exact to="/admin/customers/list" className="nav-link nav-item" onClick={this.navBarCollapseClick}> Klanten</NavLink>
                            <NavLink exact to="/admin/personaltrainers/list" className="nav-link nav-item" onClick={this.navBarCollapseClick}> Trainers</NavLink>

                            <NavLink exact to="/admin/courses/list" className="nav-link nav-item" onClick={this.navBarCollapseClick}> Lessen</NavLink>
                            <NavLink exact to="/admin/coursetypes/list" className="nav-link nav-item" onClick={this.navBarCollapseClick}> Lesvormen</NavLink>
                            <NavLink exact to="/admin/locations/list" className="nav-link nav-item" onClick={this.navBarCollapseClick}> Locaties</NavLink>
                        </div>

                        <div className="navbar-nav">
                            <li className="nav-item dropdown">
                                <a className="nav-link dropdown-toggle" href="#" role="button" data-toggle="dropdown">
                                    <i className="fa fa-user fa-lg fa-fw" />
                                    <span>{this.props.user.name}</span>
                                </a>
                                <div className="dropdown-menu dropdown-menu-right">
                                    <h6 className="dropdown-header">{this.props.user.email}</h6>
                                    <Link to="/admin/absentees" className="dropdown-item" onClick={this.navBarCollapseClick}>Afwezigen</Link>
                                    <Link to="/admin/settings/changepassword" className="dropdown-item" onClick={this.navBarCollapseClick}>Wachtwoord wijzigen</Link>
                                    <Link to="/admin/settings/about" className="dropdown-item" onClick={this.navBarCollapseClick}><i className="fa fa-info fa-fw" />Informatie</Link>
                                    <div className="dropdown-divider"></div>
                                    <form className="mx-2">
                                        <button className="btn btn-block btn-danger" type="button" onClick={() => this.props.logout()}>Uitloggen</button>
                                    </form>
                                </div>
                            </li>
                        </div>
                    </div>
                </div>
            </nav>
        );
    }
}

function mapStateToProps(state) {
    return {
        user: { ...state.user }
    };
}

export default connect(
    mapStateToProps
)(NavMenu);