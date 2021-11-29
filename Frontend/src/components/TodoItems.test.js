import { render, cleanup } from '@testing-library/react'
import TodoItems from './TodoItems'

jest.mock('../services/TodoItemsService')

describe('todo items tests', () => {
  afterEach(cleanup)

  it('should render a table', (done) => {
    const { container } = render(<TodoItems addedItem={undefined} />)
    const table = container.getElementsByTagName('table')
    done()
    expect(table).toHaveLength(1)
    expect(container).not.toBeNull()
  })
})
