import Page from 'app/page'
import { ChatPage, Login } from 'pages'
import React from 'react'
import { Route, Routes } from 'react-router-dom'
import { Routes as Paths } from 'shared/paths'

const AppRoutes = () => {
    return (
        <Routes>
            <Route path={Paths.LOGIN} element={<Login />} />
            <Route path={Paths.CHAT} element={<Page />}>
                <Route index element={<ChatPage />} />
            </Route>
        </Routes>
    )
}

export default AppRoutes
