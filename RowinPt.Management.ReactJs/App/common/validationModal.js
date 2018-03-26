import React from "react";
import { connect } from "react-redux";

import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";

import { closeValidation } from "../actions/validation";

class ValidationModal extends React.Component {
    constructor(props) {
        super(props);
        this.closeModal = this.closeModal.bind(this);
    }

    closeModal() {
        this.props.closeValidation();
    }

    render() {
        return (
            <Modal isOpen={this.props.isOpen} toggle={this.closeModal}>
                <ModalHeader toggle={this.closeModal}>
                    <span className="text-warning">Validatie fout</span>
                </ModalHeader>

                <ModalBody>
                    <div className="alert alert-warning">
                        {this.props.message}
                    </div>
                </ModalBody>

                <ModalFooter>
                    <button type="button" className="btn btn-warning" onClick={this.closeModal}>Ok</button>
                </ModalFooter>
            </Modal>
        );
    }
}

function mapStateToProps(state) {
    return {
        isOpen: state.validation.isOpen,
        message: state.validation.message
    };
}

function mapDispatchToProps(dispatch) {
    return {
        closeValidation: () => dispatch(closeValidation())
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ValidationModal);