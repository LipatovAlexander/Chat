import axios from 'axios'
import { getToken } from 'shared/local-storage'

export const api = axios.create({
    baseURL: `${process.env.REACT_APP_API_URL}/api`,
})

api.interceptors.request.use((config) => {
    if (!config.headers) {
        config.headers = {}
    }

    config.headers.Authorization = `Bearer ${getToken()}`
    return config
})
