import React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";

class ModalDeleteButton extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            modal: false
        };

        this.toggleModal = this.toggleModal.bind(this);
        this.submit = this.submit.bind(this);
    }

    toggleModal() {
        this.setState({ modal: !this.state.modal });
    }

    submit() {
        this.toggleModal();
        this.props.onSubmit();
    }

    render() {
        if (!this.props.isAdmin) {
            return null;
        }

        const action = this.props.action === undefined ? "Verwijderen" : this.props.action;

        return (
            <div className="btn-group d-flex mb-3" role="group">
                <Link to={this.props.to} className="btn btn-warning w-50">Bewerken</Link>
                <button type="button" className="btn btn-danger w-50" onClick={this.toggleModal}>{action}</button>

                <Modal isOpen={this.state.modal} toggle={this.toggleModal}>
                    <ModalHeader toggle={this.toggleModal}>
                        <span className="text-danger">{action}</span>
                    </ModalHeader>
                    <ModalBody>
                        {this.props.children}
                    </ModalBody>
                    <ModalFooter>
                        <button type="button" className="btn btn-danger" onClick={this.submit}>{action}</button>
                        <button type="button" className="btn btn-secondary" onClick={this.toggleModal}>Annuleren</button>
                    </ModalFooter>
                </Modal>
            </div>
        );
    }
}

function mapStateToProps(state) {
    return {
        isAdmin: state.user.isAdmin
    };
}

export default connect(
    mapStateToProps
)(ModalDeleteButton);