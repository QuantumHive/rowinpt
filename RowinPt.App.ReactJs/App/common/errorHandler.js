import React from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";

import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";

import { resetError } from "../actions/error";

class Error extends React.Component {
    constructor(props) {
        super(props);
        this.close = this.close.bind(this);
    }

    close() {
        this.props.reset();
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.loadError) {
            this.props.reset();
        }
    }

    render() {
        if (this.props.loadError) {
            return <Redirect to="/error" />
        }

        return (
            <Modal isOpen={this.props.openErrorModal} toggle={this.close}>
                <ModalHeader toggle={this.close}>
                    <span className="text-danger">Fout</span>
                </ModalHeader>

                <ModalBody>
                    <div className="alert alert-danger">
                        <strong>Oeps!</strong> Er is een onverwachte fout opgetreden en het verzoek kan niet worden voltooid. Neem contact op met de technische dienst indien de fout blijft optreden.
                    </div>
                </ModalBody>

                <ModalFooter>
                    <button type="button" className="btn btn-primary" onClick={this.close}>Ok</button>
                </ModalFooter>
            </Modal>
        );
    }
}

function mapStateToProps(state) {
    return { ...state.error };
}

function mapDispatchToProps(dispatch) {
    return {
        reset: () => dispatch(resetError())
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Error);