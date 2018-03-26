import { isRequired } from "../../common/validationRules";

export default {
    name: [
        {
            rule: isRequired,
            message: "Naam is verplicht"
        }
    ],
    capacity: [
        {
            rule: isRequired,
            message: "Capaciteit is verplicht"
        },
        {
            rule: input => input > 0,
            message: "Capaciteit moet minimaal 1 zijn"
        }
    ]
};