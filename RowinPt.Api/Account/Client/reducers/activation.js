import {
    ACTIVATION_ERROR,
    ACTIVATION_START,
    ACTIVATION_SUCCESS
} from "../actionTypes";

export default (state = {
    error: false,
    success: false,
    loading: false
}, action) => {
    switch (action.type) {
        case ACTIVATION_ERROR:
            return {
                error: true,
                success: false,
                loading: false
            };
        case ACTIVATION_START:
            return {
                error: false,
                success: false,
                loading: true
            };
        case ACTIVATION_SUCCESS:
            return {
                error: false,
                success: true,
                loading: false
            };
        default:
            return state;
    }
}