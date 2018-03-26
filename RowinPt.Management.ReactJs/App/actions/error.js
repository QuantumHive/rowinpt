import {
    ERROR_MODAL,
    ERROR_LOAD,
    ERROR_RESET
} from "../actionTypes";

export function openError() {
    return {
        type: ERROR_MODAL
    };
}

export function loadError() {
    return {
        type: ERROR_LOAD
    };
}

export function resetError() {
    return {
        type: ERROR_RESET
    };
}