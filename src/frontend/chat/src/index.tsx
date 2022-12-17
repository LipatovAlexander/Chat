import React from 'react'
import ReactDOM from 'react-dom/client'
import App from 'app'
import ru_RU from 'antd/locale/ru_RU'
import { ConfigProvider } from 'antd'

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement)

root.render(
    <ConfigProvider locale={ru_RU}>
        <App />
    </ConfigProvider>,
)
