import axios from "axios";

import {
    API_CALL_START,
    API_CALL_STOP,
    API_RESET,
    API_SET_RESULT,
    API_SUBMIT_SUCCESS,
    API_DELETE_SUCCESS,
    API_RESET_ALL
} from "../actionTypes";
import { api } from "../api";

import { openValidation } from "./validation";
import { openError, loadError } from "./error";

import moment from "moment";

const options = { withCredentials: true };

export function startCall(id) {
    return {
        type: API_CALL_START,
        id
    };
}

export function stopCall(id) {
    return {
        type: API_CALL_STOP,
        id
    };
}

export function resetAll() {
    return {
        type: API_RESET_ALL
    }
}

export function reset(id) {
    return {
        type: API_RESET,
        id
    };
}

export function setResult(id, result) {
    return {
        type: API_SET_RESULT,
        id,
        result
    };
}

export function submitSuccess(id) {
    return {
        type: API_SUBMIT_SUCCESS,
        id
    };
}

export function deleteSuccess(id) {
    return {
        type: API_DELETE_SUCCESS,
        id
    };
}

export function get(id, extendedRoute) {
    return dispatch => {
        dispatch(startCall(id));

        return axios
            .get(api()[id] + `${extendedRoute === undefined ? "" : `/${extendedRoute}`}`, options)
            .then(response => {
                dispatch(setResult(id, response.data));
                dispatch(stopCall(id));
            })
            .catch(_ => {
                dispatch(loadError());
            });
    };
}

export function post(id, payload, extendedRoute) {
    return dispatch => {
        dispatch(startCall(id));

        normalizeMoment(payload);

        return axios
            .post(api()[id] + `${extendedRoute === undefined ? "" : `/${extendedRoute}`}`, payload, options)
            .then(response => {
                dispatch(submitSuccess(id));
            })
            .catch(error => {
                if (error.response !== undefined && error.response.status === 400) {
                    dispatch(openValidation(error.response.data[0].validationMessage));
                } else { //if (status >= 500) {
                    dispatch(openError());
                }

                dispatch(stopCall(id));
            });
    };
}

export function put(id, payload, extendedRoute) {
    return dispatch => {
        dispatch(startCall(id));

        normalizeMoment(payload);

        return axios
            .put(api()[id] + `${extendedRoute === undefined ? "" : `/${extendedRoute}`}`, payload, options)
            .then(response => {
                dispatch(submitSuccess(id));
            })
            .catch(error => {
                if (error.response !== undefined && error.response.status === 400) {
                    dispatch(openValidation(error.response.data[0].validationMessage));
                } else { //if (status >= 500) {
                    dispatch(openError());
                }

                dispatch(stopCall(id));
            });
    };
}

export function remove(id, extendedRoute) {
    return dispatch => {
        dispatch(startCall(id))

        return axios
            .delete(api()[id] + `${extendedRoute === undefined ? "" : `/${extendedRoute}`}`, options)
            .then(response => {
                dispatch(deleteSuccess(id));
            })
            .catch(error => {
                if (error.response !== undefined && error.response.status === 400) {
                    dispatch(openValidation(error.response.data[0].validationMessage));
                } else { //if (status >= 500) {
                    dispatch(openError());
                }

                dispatch(stopCall(id));
            });
    }
}

function normalizeMoment(payload) {
    for (const key in payload) {
        const value = payload[key];
        if (moment.isMoment(value)) {
            value.utcOffset(0, true).format();
        } else if (!!value && (value.constructor === Array || value.constructor === Object)) {
            normalizeMoment(value);
        }
    }
}