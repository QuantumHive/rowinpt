import {
    SET_USER_INFORMATION
} from "../actionTypes";

export default (state = null, action) => {
    switch (action.type) {
        case SET_USER_INFORMATION:
            return { ...action.user };
        default:
            return state;
    }
}