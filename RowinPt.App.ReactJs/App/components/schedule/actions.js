﻿import axios from "axios";
import {
    startCall,
    stopCall,
    setResult
} from "../../actions/api";
import * as api from "../../api";
import { loadError } from "../../actions/error";
const options = { withCredentials: true };

export function getPlanDates(id, courseId, locationId) {
    return dispatch => {

        dispatch(startCall(id));

        axios.get(api.api()[api.plan] + `/dates?courseId=${courseId}&locationId=${locationId}`, options)
            .then(response => {
                dispatch(setResult(id, response.data));
                dispatch(stopCall(id));
            }).catch(() => {
                dispatch(loadError());
            });
    };
}