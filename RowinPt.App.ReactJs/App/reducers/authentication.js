import {
    AUTHENTICATION_START_LOADER,
    AUTHENTICATION_LOGOUT_SUCCESS,
    AUTHENTICATION_CHALLENGE_SUCCESS
} from "../actionTypes";

export default (state = {
    loading: false,
    authenticated: false,
    redirect: ""
}, action) => {
    switch (action.type) {
        case AUTHENTICATION_START_LOADER:
            return { ...state, loading: true, redirect: "" };
        case AUTHENTICATION_CHALLENGE_SUCCESS:
            return { ...state, authenticated: true, loading: false, redirect: action.redirect };
        case AUTHENTICATION_LOGOUT_SUCCESS:
            return { ...state, loading: false, authenticated: false, redirect: "" };
        default:
            return state;
    }
}