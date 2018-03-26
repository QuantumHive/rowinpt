import axios from "axios";

import {
    SET_USER_INFORMATION
} from "../actionTypes";

import { getApiEndpoint } from "../api";

const options = { withCredentials: true };

function setUserInformation(user) {
    return {
        type: SET_USER_INFORMATION,
        user
    };
}

export function loadUserInformation() {
    return dispatch => {
        return axios
            .get(getApiEndpoint() + "/information/user", options)
            .then(response => dispatch(setUserInformation(response.data)));
    };
}