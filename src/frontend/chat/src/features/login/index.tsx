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
        <Container>
            <FormBock>
                <Header>Авторизация</Header>
                <StyledForm onFinish={onFinish}>
                    <Form.Item name="username" key="username" id="username" label="Логин">
                        <Input />
                    </Form.Item>
                    <SubmitButton disabled={authenticating} htmlType="submit">
                        Войти
                    </SubmitButton>
                </StyledForm>
            </FormBock>
        </Container>
    )
}

const StyledForm = styled(Form<AuthForm>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`

const FormBock = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center;
`

const SubmitButton = styled(Button)`
    width: 50%;
    margin: -10px 0;
`

const Header = styled.div`
    font-size: 1.7em;
    font-weight: 500;
    margin: 20px 0;
`

const Container = styled.div`
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
`
