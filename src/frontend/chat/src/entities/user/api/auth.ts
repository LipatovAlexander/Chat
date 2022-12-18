import { api } from 'shared/api'

interface AuthResponse {
    jwt: string
}

interface AuthRequest {
    username: string
}

export const auth = async (data: AuthRequest) => {
    return await api.post<AuthResponse>('/Auth', data)
}
