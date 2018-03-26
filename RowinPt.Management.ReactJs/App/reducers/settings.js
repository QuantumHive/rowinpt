import {
    APPLICATION_SETTINGS_SET
} from "../actionTypes";

export default (state = { loading: true }, action) => {
    switch (action.type) {
        case APPLICATION_SETTINGS_SET:
            return {  ...action.settings, loading: false };
        default:
            return state;
    }
}