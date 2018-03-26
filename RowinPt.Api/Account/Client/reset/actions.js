import axios from "axios";
import {
    ACTIVATION_ERROR,
    ACTIVATION_START,
    ACTIVATION_SUCCESS
} from "../actionTypes";

function start() {
    return {
        type: ACTIVATION_START
    };
}

function fail() {
    return {
        type: ACTIVATION_ERROR
    };
}

function success() {
    return {
        type: ACTIVATION_SUCCESS
    };
}

export function reset(payload) {
    return dispatch => {
        dispatch(start());

        return axios.post("/account/password/reset", payload).then(response => {
            if (response.status === 204) {
                dispatch(success());
            } else {
                dispatch(fail());
            }
        }).catch(error => {
            dispatch(fail());
        });
    }
}