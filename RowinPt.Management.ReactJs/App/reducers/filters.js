import {
    FILTERS_SET
} from "../actionTypes";

export default (state = [], action) => {
    switch (action.type) {
        case FILTERS_SET:
            const copy = [...state];
            copy[action.key] = { ...action.filter };
            return copy;
        default:
            return state;
    }
}