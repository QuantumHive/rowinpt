import {
    ERROR_MODAL,
    ERROR_LOAD,
    ERROR_RESET
} from "../actionTypes";

const defaultState = {
    loadError: false,
    openErrorModal: false
};

export default (state = { ...defaultState }, action) => {
    switch (action.type) {
        case ERROR_MODAL:
            return { ...state, openErrorModal: true };
        case ERROR_LOAD:
            return { ...state, loadError: true };
        case ERROR_RESET:
            return { ...defaultState };
        default:
            return state;
    }
}