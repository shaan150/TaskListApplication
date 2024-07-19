import React, { useState } from 'react';
import { createTask } from '../services/taskService';
import './css/TaskForm.css';

const TaskForm = ({ onTaskCreated }) => {
    const [title, setTitle] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        await createTask({ title, isComplete: false, subTasks: [] });
        setTitle('');
        onTaskCreated();
    };

    return (
        <form onSubmit={handleSubmit} className="task-form">
            <input
                type="text"
                placeholder="Task Title"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                required
            />
            <button className="add-button" type="submit">Add</button>
        </form>
    );
};

export default TaskForm;