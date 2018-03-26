import React from "react";

function validationMessage(validate, rules) {
    for (const index in rules) {
        const validationRule = rules[index];
        if (!validationRule.rule(validate)) {
            return validationRule.message;
        }
    }
    return "";
}

export default ({ validate, rules }) => {
    return (
        <div className="invalid-feedback">
            {validationMessage(validate, rules)}
        </div>
    );
}