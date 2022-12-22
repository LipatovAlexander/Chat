import { userModel } from 'entities/user'
import React, { useEffect } from 'react'
import AppRoutes from './router'

const App = () => {
    useEffect(() => {
        userModel.effects.loadUserFx()
    }, [])

    return <AppRoutes />
}

export default App
