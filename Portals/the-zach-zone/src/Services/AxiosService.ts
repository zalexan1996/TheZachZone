import Axios from "axios";

export const TZZ_API_BASE_URL = "https://localhost:8001"
export const TGZ_API_BASE_URL = "https://localhost:8011"
export const AxiosInstance = Axios.create({
    withCredentials: true
})

AxiosInstance.interceptors.response.use(res => {
    return res
}, err => {
    return Promise.reject(err)
})