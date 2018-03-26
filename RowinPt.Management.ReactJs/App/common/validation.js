export function formControl(validate, rules, activate) {
    if (activate) {
        for (const index in rules) {
            const validationRule = rules[index];
            if (!validationRule.rule(validate)) {
                return "form-control is-invalid";
            }
        }
    }
    return "form-control";
}

export function isValid(validate, allRules) {
    for (const ruledex in allRules) {
        const validationRules = allRules[ruledex];
        for (const index in validationRules) {
            const validationRule = validationRules[index];
            if (!validationRule.rule(validate[ruledex])) {
                return false;

            }
        }
    }
    return true;
}