import { FileFilled } from '@ant-design/icons'
import React from 'react'
import styled from 'styled-components'

interface LinkToFileProps {
    link: string
    className?: string
}

const LinkToFile = ({ link, className }: LinkToFileProps) => {
    return (
        <Link download href={link} className={className}>
            <FileFilled />
        </Link>
    )
}

const Link = styled.a`
    color: #1886ee;
    font-size: 22px;
`

export default React.memo(LinkToFile)
