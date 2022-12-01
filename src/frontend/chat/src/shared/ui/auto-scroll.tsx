import React, { useRef, useEffect } from 'react'
import styled from 'styled-components'

interface AutoScrollProps {
    children: React.ReactNode
}

const AutoScroll = ({ children, ...props }: AutoScrollProps) => {
    const refToScrollingArea = useRef<HTMLDivElement | null>(null)

    useEffect(() => {
        if (refToScrollingArea.current) {
            refToScrollingArea.current.scrollTop = refToScrollingArea.current.scrollHeight
        }
    }, [children])

    return (
        <ScrollingArea ref={refToScrollingArea} {...props}>
            {children}
        </ScrollingArea>
    )
}

const ScrollingArea = styled.div`
    overflow-y: auto;
`

export default AutoScroll
