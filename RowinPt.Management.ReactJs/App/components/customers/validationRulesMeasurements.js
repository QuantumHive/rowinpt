import { isRequired, range } from "../../common/validationRules";

export default {
    weight: [
        {
            rule: input => !isRequired(input) || range(0, 1000)(input),
            message: "Gewicht moet tussen de 0 en 1000 zijn"
        }
    ],
    fatPercentage: [
        {
            rule: input => !isRequired(input) || range(0, 1000)(input),
            message: "Vet percentage moet tussen de 0 en 1000 zijn"
        }
    ],
    shoulderSize: [
        {
            rule: input => !isRequired(input) || range(0, 1000)(input),
            message: "Omvang van de schouder moet tussen de 0 en 1000 zijn"
        }
    ],
    armSize: [
        {
            rule: input => !isRequired(input) || range(0, 1000)(input),
            message: "Omvang van de arm moet tussen de 0 en 1000 zijn"
        }
    ],
    bellySize: [
        {
            rule: input => !isRequired(input) || range(0, 1000)(input),
            message: "Omvang van de buik moet tussen de 0 en 1000 zijn"
        }
    ],
    waistSize: [
        {
            rule: input => !isRequired(input) || range(0, 1000)(input),
            message: "Omvang van de heup moet tussen de 0 en 1000 zijn"
        }
    ],
    upperLegSize: [
        {
            rule: input => !isRequired(input) || range(0, 1000)(input),
            message: "Omvang van het bovenbeen moet tussen de 0 en 1000 zijn"
        }
    ]
};