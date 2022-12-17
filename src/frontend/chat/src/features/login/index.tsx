import { Button, Form, Input } from 'antd'
import { userModel } from 'entities/user'
import React from 'react'
import styled from 'styled-components'

interface AuthForm {
    username: string
}

export const LoginForm = () => {
    const authenticating = userModel.useAuthenticating()

    const onFinish = (form: AuthForm) => {
        userModel.effects.authFx(form)
    }

    return (
        <FormBlock>
            <Form<AuthForm> onFinish={onFinish}>
                <Form.Item name="username" key="username" id="username">
                    <Input />
                </Form.Item>
                <Button disabled={authenticating} htmlType="submit">
                    Войти
                </Button>
            </Form>
        </FormBlock>
    )
}

const FormBlock = styled.div`
    margin-left: auto;
    margin-right: auto;
`
