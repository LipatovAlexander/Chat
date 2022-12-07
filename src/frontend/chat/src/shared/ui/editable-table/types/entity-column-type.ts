import { TableColumnType } from 'antd'
import React from 'react'

interface FormField {
    name: string
    input: React.ReactNode
    required?: boolean
    valuePropName?: string
}

export type EditableTableColumnType<T> = Omit<TableColumnType<T>, 'dataIndex'> & {
    dataField: keyof T
    formField: FormField
}
