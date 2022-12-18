import { userModel } from 'entities/user'
import { LoginForm } from 'features/login'
import React from 'react'
import { Navigate } from 'react-router-dom'
import { Routes } from 'shared/paths'

const Login = () => {
    const { isAuthenticated } = userModel.useUser()

    if (isAuthenticated) {
        return <Navigate to={Routes.CHAT} />
    }

    return <LoginForm />
}

export default Login
