import {
    API_CALL_START,
    API_CALL_STOP,
    API_RESET,
    API_SET_RESULT,
    API_SUBMIT_SUCCESS,
    API_DELETE_SUCCESS,
    API_RESET_ALL
} from "../actionTypes";

const defaultState = {
    loading: true,
    result: null,
    submitted: false,
    deleted: false
};

export default (state = {}, action) => {
    const id = action.id;
    const newState = { ...state };
    if (!newState.hasOwnProperty(id)) {
        newState[id] = { ...defaultState };
    }

    switch (action.type) {
        case API_CALL_START:
            newState[id].submitted = false;
            newState[id].loading = true;
            break;
        case API_CALL_STOP:
            newState[id].submitted = false;
            newState[id].loading = false;
            break;
        case API_SET_RESULT:
            newState[id].submitted = false;
            newState[id].deleted = false;
            newState[id].result = action.result;
            break;
        case API_SUBMIT_SUCCESS:
            newState[id].result = null;
            newState[id].submitted = true;
            newState[id].deleted = false;
            break;
        case API_DELETE_SUCCESS:
            const resetState = {};
            resetState[id] = { ...defaultState };
            resetState[id].deleted = true;
            resetState[id].loading = true;
            return resetState;
        case API_RESET:
            newState[id] = { ...defaultState };
            break;
        case API_RESET_ALL:
            return {};
        default:
            return state;
    }

    return newState;
}