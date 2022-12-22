import { createEffect, createStore, forward, sample } from 'effector'
import { useStore } from 'effector-react'
import { chatModel } from 'entities/chat'
import jwtDecode from 'jwt-decode'
import { getToken, setToken } from 'shared/local-storage'
import { auth } from '../api/auth'
import { User } from '../types'

const $user = createStore<User>({ isAuthenticated: false, username: '', isAdmin: false })

const authFx = createEffect(async (data: { username: string }) => {
    const resp = await auth(data)
    const { jwt } = resp.data

    setToken(jwt)
})

const loadUserFx = createEffect(() => {
    const token = getToken()
    const userInfo = token ? jwtDecode<{ nameid: string }>(token) : null

    return {
        isAuthenticated: !!userInfo,
        username: userInfo?.nameid ?? '',
        isAdmin: userInfo?.nameid?.startsWith('admin') ?? false,
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

sample({
    clock: chatModel.connection.events.newUserJoinedToRoom,
    source: $user,
    filter: (user) => user.isAdmin,
    target: chatModel.messages.events.loadMessages,
})

sample({
    clock: chatModel.connection.events.interlocutorLeftFromRoom,
    source: $user,
    filter: (user) => user.isAdmin,
    target: chatModel.messages.events.clearMessage,
})

export const useUser = () => useStore($user)
export const useAuthenticating = () => useStore(authFx.pending)

export const effects = {
    authFx,
    loadUserFx,
}
