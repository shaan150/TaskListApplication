import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7059/api' // Use environment variable or fallback to localhost
});

// Error handling function
const handleError = (error) => {
    console.error('API call error:', error);
    throw error; // Re-throw the error after logging it
};

// API functions for SubTasks
export const getSubTasks = async () => {
    try {
        const response = await api.get('/subtasks/all');
        return response.data;
    } catch (error) {
        handleError(error);
    }
};

export const getSubTask = async (id) => {
    try {
        const response = await api.get(`/subtasks/${id}`);
        return response.data;
    } catch (error) {
        handleError(error);
    }
};

export const createSubTask = async (subTask) => {
    try {
        const response = await api.post('/subtasks/create', subTask);
        return response.data;
    } catch (error) {
        handleError(error);
    }
};

export const updateSubTask = async (id, subTask) => {
    try {
        const response = await api.put(`/subtasks/update/${id}`, subTask);
        return response.data;
    } catch (error) {
        handleError(error);
    }
};

export const deleteSubTask = async (id) => {
    try {
        const response = await api.delete(`/subtasks/delete/${id}`);
        return response.data;
    } catch (error) {
        handleError(error);
    }
};
