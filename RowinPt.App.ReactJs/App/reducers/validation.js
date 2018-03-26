import {
    VALIDATION_OPEN_MODAL,
    VALIDATION_CLOSE_MODAL
} from "../actionTypes";

export default (state = { isOpen: false, message: null }, action) => {
    switch (action.type) {
        case VALIDATION_OPEN_MODAL:
            return { isOpen: true, message: action.message };
        case VALIDATION_CLOSE_MODAL:
            return { isOpen: false, message: null };
        default:
            return state;
    }
}