import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7059/api' // Use environment variable or fallback to localhost
});

// Error handling function
const handleError = (error) => {
    console.error('API call error:', error);
    throw error; // Re-throw the error after logging it
};

// API functions
export const getTasks = async () => {
    try {
        const response = await api.get('/tasks/all');
        return response.data;
    } catch (error) {
        handleError(error);
    }
};

export const getTask = async (id) => {
    try {
        const response = await api.get(`/tasks/${id}`);
        return response.data;
    } catch (error) {
        handleError(error);
    }
};

export const createTask = async (task) => {
    try {
        const response = await api.post('/tasks/create', task);
        return response.data;
    } catch (error) {
        handleError(error);
    }
};

export const updateTask = async (id, task) => {
    try {
        const response = await api.put(`/tasks/update/${id}`, task);
        return response.data;
    } catch (error) {
        handleError(error);
    }
};

export const deleteTask = async (id) => {
    try {
        const response = await api.delete(`/tasks/delete/${id}`);
        return response.data;
    } catch (error) {
        handleError(error);
    }
};
