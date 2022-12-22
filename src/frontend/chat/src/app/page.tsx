import { userModel } from 'entities/user'
import React from 'react'
import { Navigate, Outlet, useLocation } from 'react-router-dom'
import { Routes } from 'shared/paths'

const Page = () => {
    const { isAuthenticated } = userModel.useUser()

    const location = useLocation()

    if (!isAuthenticated) {
        return <Navigate to={Routes.LOGIN} state={{ from: location }} replace />
    }

    return <Outlet />
}

export default Page
