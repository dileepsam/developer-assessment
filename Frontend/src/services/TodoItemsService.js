import axios from 'axios'

class TodoItemsService {
  base_url = `${process.env.REACT_APP_BACKEND_URL}/TodoItems`


  async getAll() {
    return axios.get(this.base_url)
  }

  async create(item) {
    return axios.post(this.base_url, item)
  }

  async update(id, item) {
    return axios.put(`${this.base_url}/${id}`,item)
  }

  async get(id) {
    return axios.get(`${this.base_url}/${id}`)
  }
}

const todoItemService = new TodoItemsService()

export default todoItemService
