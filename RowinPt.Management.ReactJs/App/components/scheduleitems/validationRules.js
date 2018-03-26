import { isRequired } from "../../common/validationRules";

export default {
    repeat: [
        {
            rule: isRequired,
            message: "Als je niet vooruit wilt plannen, voer dan 0 in"
        },
        {
            rule: input => input >= 0,
            message: "Je kunt alleen vooruit plannen"
        },
        {
            rule: input => input % 1 === 0,
            message: "Voer een geheel getal in"
        }
    ]
};