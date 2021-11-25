import axios, { AxiosResponse } from "axios";

axios.defaults.baseURL = "http://localhost:10274/api";

class BaseService {
  private responseData<T>(response: AxiosResponse<T>) {
    return response.data;
  }

  protected requests = {
    get: <T>(url: string, params = {}) => axios.get<T>(url, { params }).then(this.responseData),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(this.responseData),
    put: <T>(url: string, body: {}, params: {}) =>
      axios.put<T>(url, body, { params }).then(this.responseData),
    delete: <T>(url: string, params: {}) => axios.delete<T>(url, { params }).then(this.responseData)
  };
}

export default BaseService;
