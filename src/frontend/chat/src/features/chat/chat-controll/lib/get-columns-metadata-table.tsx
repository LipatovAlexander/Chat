import React from 'react'
import { Input } from 'antd'
import { EditableTableColumnType } from 'shared/ui'
import { MetadataForm } from '../types/file-with-metadata-form'

export const getColumnsMetadataTable = (): EditableTableColumnType<MetadataForm>[] => [
    {
        title: 'Название',
        dataField: 'name',
        formField: {
            input: <Input />,
            name: 'name',
            required: true,
        },
    },
    {
        title: 'Значение',
        dataField: 'value',
        formField: {
            input: <Input />,
            name: 'value',
            required: true,
        },
    },
]
