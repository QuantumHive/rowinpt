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
    ]
};