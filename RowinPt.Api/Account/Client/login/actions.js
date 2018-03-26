import axios from "axios";
import {
    AUTHENTICATION_AUTHENTICATE_START,
    AUTHENTICATION_AUTHENTICATE_SUCCESS,
    AUTHENTICATION_AUTHENTICATE_FAIL,
    AUTHENTICATION_AUTHENTICATE_RESET
} from "../actionTypes";

export function authenticateStart() {
    return {
        type: AUTHENTICATION_AUTHENTICATE_START
    }
}

export function authenticateSuccess(redirect) {
    return {
        type: AUTHENTICATION_AUTHENTICATE_SUCCESS,
        redirect
    }
}

export function authenticateFail() {
    return {
        type: AUTHENTICATION_AUTHENTICATE_FAIL
    }
}

export function authenticateReset() {
    return {
        type: AUTHENTICATION_AUTHENTICATE_RESET
    };
}

export function login(email, password) {
    return dispatch => {
        dispatch(authenticateStart());

        return axios.post("/account/login", {
            email, password
        }).then(response => {
            if (response.status === 202) {
                dispatch(authenticateSuccess(response.headers.location));
            } else {
                dispatch(authenticateFail());
            }
        }).catch(error => {
            dispatch(authenticateFail());
        });
    }
}

export function challenge() {
    return dispatch => {
        dispatch(authenticateStart());

        return axios.get("/account/challenge", { withCredentials: true }).then(response => {
            if (response.status === 202) {
                dispatch(authenticateSuccess(response.headers.location));
            } else {
                dispatch(authenticateReset());
            }
        }).catch(error => {
            dispatch(authenticateReset());
        });
    }
}