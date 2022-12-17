const storageKeys = {
    TOKEN: 'auth',
}

export const setToken = (token: string) => {
    localStorage.setItem(storageKeys.TOKEN, token)
}

export const getToken = () => {
    return localStorage.getItem(storageKeys.TOKEN)
}
