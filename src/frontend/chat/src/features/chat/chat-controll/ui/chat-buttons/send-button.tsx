import { SendOutlined } from '@ant-design/icons'
import { Button, ButtonProps, Tooltip } from 'antd'
import React from 'react'
import styled from 'styled-components'

type SendButtonProps = ButtonProps

export const SendButton = ({ ...props }: SendButtonProps) => {
    return (
        <Block>
            <Tooltip title={'Отправить'}>
                <Button icon={<SendOutlined />} {...props} />
            </Tooltip>
        </Block>
    )
}

const Block = styled.div`
    margin-left: 10px;
`
