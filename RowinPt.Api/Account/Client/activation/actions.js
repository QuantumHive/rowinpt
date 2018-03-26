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

export function activate(payload) {
    return dispatch => {
        dispatch(start());

        return axios.post("/account/activate", payload).then(response => {
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