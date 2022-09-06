import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
const axios = require('axios');
const axiosApiInstance = axios.create();

// Request interceptor for API calls
axiosApiInstance.interceptors.request.use(
  async config => {
    const token = localStorage.getItem("jwt");
    config.headers = { 
      'Authorization': `Bearer ${token}`
    }
    return config;
  },
  error => {
    Promise.reject(error)
});

// Response interceptor for API calls
axiosApiInstance.interceptors.response.use((response) => {
  return response
}, async function (error) {
  var errorMessage = error.response.data;
  if (error.response.status === 401) {
    errorMessage = "Your credentials are not valid, please log in again"
    localStorage.clear();
    window.location.href = "/login";
  }
  else if (error.response.status === 403) {
    errorMessage = "You don't have permission for this action"
  }
  else if (error.response.status === 404) {
    errorMessage = "Resource not found! Don't cry, it's just a 404 error!"
  }
  else if (error.response.status === 500) {
    errorMessage = "Oops... Something went wrong but, we are working on it"
  }
  
  toast.error(errorMessage);
  return Promise.reject(error);
});

export default axiosApiInstance;