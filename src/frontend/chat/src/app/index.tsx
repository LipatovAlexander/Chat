import { userModel } from 'entities/user'
import React, { useEffect } from 'react'
import { RouterProvider, redirect } from 'react-router-dom'
import { Routes } from 'shared/paths'
import { router } from './router'

const App = () => {
    const { isAuthenticated } = userModel.useUser()

    useEffect(() => {
        userModel.effects.loadUserFx()
    }, [])

    useEffect(() => {
        isAuthenticated ? redirect(Routes.CHAT) : redirect(Routes.LOGIN)
    }, [isAuthenticated])

    return <RouterProvider router={router} />
}

export default App
