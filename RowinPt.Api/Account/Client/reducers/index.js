import { combineReducers } from "redux";
import Authentication from "./authentication";
import Activation from "./activation";
import Settings from "./settings";

export default combineReducers({
    authentication: Authentication,
    activation: Activation,
    settings: Settings
});