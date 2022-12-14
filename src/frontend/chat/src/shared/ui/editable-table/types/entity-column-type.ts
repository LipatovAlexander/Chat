import React from 'react'
import { TableColumnType } from 'antd'
import { FormField } from 'shared/types/ui'

export type EditableTableColumnType<T> = Omit<TableColumnType<T>, 'dataIndex'> & {
    dataField: keyof T
    formField: FormField
}
