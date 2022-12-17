import React from 'react'
import ReactDOM from 'react-dom/client'
import ru_RU from 'antd/locale/ru_RU'
import { ConfigProvider } from 'antd'
import { BrowserRouter } from 'react-router-dom'
import App from 'app'

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement)

root.render(
    <ConfigProvider locale={ru_RU}>
        <BrowserRouter>
            <App />
        </BrowserRouter>
    </ConfigProvider>,
)
