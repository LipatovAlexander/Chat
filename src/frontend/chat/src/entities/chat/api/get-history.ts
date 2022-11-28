import { api } from 'shared/api'
import { Message } from '../types'

const getHistory = async () => {
    return await api.get<Message[]>('History')
}

export default getHistory
