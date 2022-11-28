import axios from 'axios'

interface IpInfo {
    IPv4: string
}

const urlForIp = 'https://geolocation-db.com/json/'

const getCurrentUserIp = async (): Promise<string | undefined> => {
    let currentIp: string | undefined

    try {
        const res = await axios.get<IpInfo>(urlForIp)
        currentIp = res.data.IPv4
    } catch {}

    return currentIp
}

export default getCurrentUserIp
