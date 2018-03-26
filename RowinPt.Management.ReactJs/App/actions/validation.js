import {
    VALIDATION_OPEN_MODAL,
    VALIDATION_CLOSE_MODAL
} from "../actionTypes";

export function openValidation(message) {
    return {
        type: VALIDATION_OPEN_MODAL,
        message
    };
}

export function closeValidation() {
    return {
        type: VALIDATION_CLOSE_MODAL
    };
}