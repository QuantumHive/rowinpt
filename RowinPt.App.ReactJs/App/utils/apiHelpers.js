export function isLoading(state, api) {
    let anyLoaders = false;
    for (const index in api) {
        const apiIndex = api[index];
        if (state.api.hasOwnProperty(apiIndex)) {
            const apiState = state.api[apiIndex];
            const isApiLoading = apiState.loading || apiState.result === null;
            if (isApiLoading) {
                anyLoaders = true;
            }
        } else {
            return true;
        }
    }
    return anyLoaders;
}

export function isArray(state, api) {
    let allIsArray = true;
    for (const index in api) {
        const apiIndex = api[index];
        if (state.api.hasOwnProperty(apiIndex)) {
            const apiState = state.api[apiIndex];
            const isApiResultArray = apiState.result !== null && Array.isArray(apiState.result);
            if (!isApiResultArray) {
                allIsArray = false;
            }
        } else {
            return false;
        }
    }
    return allIsArray;
}

export function getResult(state, api) {
    return state.api.hasOwnProperty(api) ? state.api[api].result : null;
}