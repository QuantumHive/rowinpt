import { combineReducers } from "redux";
import Api from "./api";
import Validation from "./validation";
import Error from "./error";
import Settings from "./settings";
import Authentication from "./authentication";
import User from "./user";

export default combineReducers({
    api: Api,
    validation: Validation,
    error: Error,
    settings: Settings,
    authentication: Authentication,
    user: User
});