import React from 'react'
import { createBrowserRouter } from 'react-router-dom'
import { Routes } from 'shared/paths'
import { ChatPage, Login } from 'pages'

export const router = createBrowserRouter([
    {
        path: Routes.CHAT,
        element: <ChatPage />,
    },
    {
        path: Routes.LOGIN,
        element: <Login />,
    },
])
