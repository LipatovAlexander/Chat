import { createEffect, createStore, forward } from 'effector'
import { useStore } from 'effector-react'
import jwtDecode from 'jwt-decode'
import { getToken, setToken } from 'shared/local-storage'
import { auth } from '../api/auth'
import { User } from '../types'

const $user = createStore<User>({ isAuthenticated: false, username: '' })

const authFx = createEffect(async (data: { username: string }) => {
    const resp = await auth(data)
    const { jwt } = resp.data

    setToken(jwt)
})

const loadUserFx = createEffect(() => {
    const token = getToken()
    const userInfo = token ? jwtDecode<{ username: string }>(token) : null

    return {
        isAuthenticated: !!userInfo,
        username: userInfo?.username ?? '',
    }
})

forward({
    from: authFx.doneData,
    to: loadUserFx,
})

forward({
    from: loadUserFx.doneData,
    to: $user,
})

export const useUser = () => useStore($user)
export const useAuthenticating = () => useStore(authFx.pending)

export const effects = {
    authFx,
    loadUserFx,
}
