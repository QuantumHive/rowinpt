let url = "";
export function setApiEndpoint(apiEndpoint) {
    url = apiEndpoint;
}
export function getApiEndpoint() {
    return url;
}

export const coursetypes = 1;
export const courses = 2;
export const locations = 3;
export const customers = 4;
export const personaltrainers = 5;
export const schedule = 6;
export const scheduleitems = 7;
export const plan = 8;
export const agendacustomer = 9;
export const agenda = 10;
export const changepassword = 11;
export const profile = 12;

export function api() {
    return {
        1: `${url}/coursetypes`,
        2: `${url}/courses`,
        3: `${url}/locations`,
        4: `${url}/customers`,
        5: `${url}/personaltrainers`,
        6: `${url}/schedule`,
        7: `${url}/scheduleitems`,
        8: `${url}/plan`,
        9: `${url}/agenda/customer`,
        10: `${url}/agenda`,
        11: `${url}/account/password/change`,
        12: `${url}/profile`
    };
}