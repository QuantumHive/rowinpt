import {
    FILTERS_SET
} from "../actionTypes";

export function set(filter, key) {
    return {
        type: FILTERS_SET,
        filter,
        key
    };
}