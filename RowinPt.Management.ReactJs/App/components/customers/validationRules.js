import { isRequired } from "../../common/validationRules";

export default {
    name: [
        {
            rule: isRequired,
            message: "Naam is verplicht"
        }
    ],
    email: [
        {
            rule: isRequired,
            message: "Email is verplicht"
        },
        {
            rule: input => /\S+@\S+/.test(input),
            message: "Dit is geen geldig email adres"
        }
    ],
    phoneNumber: [
        {
            rule: isRequired,
            message: "Telefoon is verplicht"
        }
    ],
    length: [
        {
            rule: input => !isRequired(input) || (input >= 10 && input <= 250),
            message: "Lengte moet minimaal 10 cm en maximaal 250 cm zijn"
        }
    ]
};