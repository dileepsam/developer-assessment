import { render, cleanup, waitFor } from '@testing-library/react'
import TodoItems from './TodoItems'

jest.mock('../services/TodoItemsService')

describe('todo items tests', () => {
  afterEach(cleanup)

  it('should render a table', async () => {
    const { container } = render(<TodoItems addedItem={undefined} />)
    const table = container.getElementsByTagName('table')
    expect(table).toHaveLength(1)
    await waitFor(() => { expect(container).not.toBeNull() })
  })
})
