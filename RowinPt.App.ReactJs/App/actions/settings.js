import axios from "axios";

import {
    APPLICATION_SETTINGS_SET
} from "../actionTypes";

export function setSettings(settings) {
    return {
        type: APPLICATION_SETTINGS_SET,
        settings
    };
}

export function getSettings() {
    return dispatch => {
        return axios.get("/information").then(response => {
            dispatch(setSettings(response.data));
        });
    }
}