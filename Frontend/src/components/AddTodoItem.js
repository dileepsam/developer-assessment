import React, { useState } from 'react'
import { Button, Col, Container, Form, Row, Stack } from 'react-bootstrap'
import todoItemService from 'services/TodoItemsService'

function AddTodoItem({ onItemAdded }) {
  const [description, setDescription] = useState('')
  const [errorMessage, setErrorMessage] = useState('')

  const handleDescriptionChange = (e) => {
    setDescription(e.target.value)
    setErrorMessage('')
  }

  const handleClear = () => {
    setDescription('')
    setErrorMessage('')
  }

  const handleAdd = async () => {
    try {
      const resp = await todoItemService.create({ description, isCompleted: false })
      onItemAdded(resp.data)
      setDescription('')
    } catch (e) {
      setErrorMessage(e.response.data)
    }
  }

  return (
    <Container>
      <h1>Add Item</h1>
      <Form.Group as={Row} className="mb-3" controlId="formAddTodoItem">
        <Form.Label column sm="2">
          Description
        </Form.Label>
        <Col md="6">
          <Form.Control
            type="text"
            placeholder="Enter description..."
            value={description}
            onChange={handleDescriptionChange}
          />
          <div className="text-start text-danger">{errorMessage}</div>
        </Col>
      </Form.Group>
      <Form.Group as={Row} className="mb-3 offset-md-2" controlId="formAddTodoItem">
        <Stack direction="horizontal" gap={2}>
          <Button variant="primary" data-testid="add-button" onClick={handleAdd}>
            Add Item
          </Button>
          <Button variant="secondary" onClick={handleClear}>
            Clear
          </Button>
        </Stack>
      </Form.Group>
    </Container>
  )
}

export default AddTodoItem
