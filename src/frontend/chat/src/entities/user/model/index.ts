import { createEffect, createEvent, createStore, forward, sample } from 'effector'
import { useStore } from 'effector-react'
import { getCurrentUserIp } from 'shared/utils'
import { defaultUserIp } from '../conf'

interface UserIp {
    ipV4: string
}

const $userIp = createStore<UserIp>({ ipV4: defaultUserIp })

const loadCurrentUserIp = createEvent()

const loadCurrentUserIpFx = createEffect(async () => {
    return await getCurrentUserIp()
})

forward({
    from: loadCurrentUserIp,
    to: loadCurrentUserIpFx,
})

sample({
    clock: loadCurrentUserIpFx.doneData,
    filter: (ip) => !!ip,
    fn: (ipV4) => ({
        ipV4: ipV4!,
    }),
    target: $userIp,
})

export const useCurrentUserIp = () => useStore($userIp)
export const useUserIpLoading = () => useStore(loadCurrentUserIpFx.pending)

export const events = {
    loadCurrentUserIp,
}
