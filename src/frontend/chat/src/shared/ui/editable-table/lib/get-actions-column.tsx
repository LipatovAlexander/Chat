import React from 'react'
import { CloseOutlined, DeleteOutlined, EditOutlined, SaveOutlined } from '@ant-design/icons'
import { Button, Popconfirm, Space, TableColumnType, Tooltip } from 'antd'
import styled from 'styled-components'

type Action<T> = (record: T) => void

export const getActionsColumn = <T,>(
    isEditableRow: (record: T) => boolean,
    onEdit: Action<T>,
    onDelete: Action<T>,
    onSave: Action<T>,
    onCancel: Action<T>,
): TableColumnType<T> => {
    return {
        title: '',
        align: 'center',
        render: (_: any, record: T) => {
            return (
                <>
                    <Space>
                        {!isEditableRow(record) && (
                            <ButtonsBlock>
                                <Tooltip placement="left" title={`Изменить`}>
                                    <Button icon={<EditOutlined />} onClick={() => onEdit(record)} />
                                </Tooltip>
                                <Tooltip placement="left" title={`Удалить`}>
                                    <Popconfirm
                                        title="Вы точно хотите удалить элемент?"
                                        onConfirm={() => onDelete(record)}
                                    >
                                        <Button danger icon={<DeleteOutlined />} />
                                    </Popconfirm>
                                </Tooltip>
                            </ButtonsBlock>
                        )}
                        {isEditableRow(record) && (
                            <ButtonsBlock>
                                <Tooltip placement="left" title={`Сохранить`}>
                                    <Popconfirm
                                        title="Вы точно хотите применить изменения?"
                                        onConfirm={() => onSave(record)}
                                    >
                                        <Button icon={<SaveOutlined />} />
                                    </Popconfirm>
                                </Tooltip>
                                <Tooltip placement="left" title={`Отменить`}>
                                    <Button icon={<CloseOutlined />} onClick={() => onCancel(record)} />
                                </Tooltip>
                            </ButtonsBlock>
                        )}
                    </Space>
                </>
            )
        },
    }
}

const ButtonsBlock = styled.div`
    display: flex;
    grid-row-gap: 5px;
    flex-direction: column;
`
