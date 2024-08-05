import Axios from "axios";

export const API_BASE_URL = "https://localhost:8011"
export const AxiosInstance = Axios.create({
    withCredentials: true
})
