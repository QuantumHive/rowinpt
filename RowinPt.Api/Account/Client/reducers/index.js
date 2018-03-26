import { combineReducers } from "redux";
import Authentication from "./authentication";
import Activation from "./activation";

export default combineReducers({
    authentication: Authentication,
    activation: Activation
});