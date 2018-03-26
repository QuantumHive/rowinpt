import axios from "axios";
import {
    AUTHENTICATION_START_LOADER,
    AUTHENTICATION_LOGOUT_SUCCESS,
    AUTHENTICATION_CHALLENGE_SUCCESS
} from "../actionTypes";

const options = { withCredentials: true };

export function startLoader() {
    return {
        type: AUTHENTICATION_START_LOADER
    }
}

export function logoutSuccess() {
    return {
        type: AUTHENTICATION_LOGOUT_SUCCESS
    };
}

export function challengeSuccess(redirect) {
    return {
        type: AUTHENTICATION_CHALLENGE_SUCCESS,
        redirect
    }
}

export function logout(endpoint) {
    return dispatch => {
        dispatch(startLoader());

        return axios.post(endpoint + "/account/logout", null, options).then(response => {
            dispatch(logoutSuccess());
        });
    }
}

export function challenge(endpoint) {
    return dispatch => {
        return axios.get(endpoint + "/account/challenge", options).then(response => {
            if (response.status === 202) {
                dispatch(challengeSuccess(response.headers.location));
            }
        }).catch(error => {
            if (error.response.status === 401) {
                window.location.replace(endpoint + "/authentication/login");
            }
        });
    }
}