import { isRequired } from "../../common/validationRules";

export default {
    name: [
        {
            rule: isRequired,
            message: "Naam is verplicht"
        }
    ]
};