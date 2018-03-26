export const isRequired = input => input !== "" && input !== null;

export function range(min, max) {
    return input => input >= min && input <= max
}