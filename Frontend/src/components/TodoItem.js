import React from 'react'
import { Button } from 'react-bootstrap'

function TodoItem(props) {
  const { id, description, onComplete } = props

  return (
    <tr>
      <td>{id}</td>
      <td>{description}</td>
      <td>
        <Button variant="warning" size="sm" onClick={() => onComplete({ id, description })}>
          Mark as completed
        </Button>
      </td>
    </tr>
  )
}

export default TodoItem
