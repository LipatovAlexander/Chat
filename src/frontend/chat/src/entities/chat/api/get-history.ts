import { api } from 'shared/api'
import { MessageResp } from '../types/message'

const getHistory = async () => {
    return await api.get<MessageResp[]>('History')
}

export default getHistory
