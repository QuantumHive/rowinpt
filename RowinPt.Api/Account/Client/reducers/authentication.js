import {
    AUTHENTICATION_AUTHENTICATE_START,
    AUTHENTICATION_AUTHENTICATE_SUCCESS,
    AUTHENTICATION_AUTHENTICATE_FAIL,
    AUTHENTICATION_AUTHENTICATE_RESET
} from "../actionTypes";

export default (state = {
    authenticating: false,
    authenticated: false,
    failed: false,
    redirect: ""
}, action) => {
    switch (action.type) {
        case AUTHENTICATION_AUTHENTICATE_START:
            return {
                authenticating: true,
                authenticated: false,
                failed: false,
                redirect: ""
            };
        case AUTHENTICATION_AUTHENTICATE_SUCCESS:
            return {
                authenticating: false,
                authenticated: true,
                failed: false,
                redirect: action.redirect
            };
        case AUTHENTICATION_AUTHENTICATE_FAIL:
            return {
                authenticating: false,
                authenticated: false,
                failed: true,
                redirect: ""
            };
        case AUTHENTICATION_AUTHENTICATE_RESET:
            return {
                authenticating: false,
                authenticated: false,
                failed: false,
                redirect: ""
            };
        default:
            return state;
    }
}