import React, { useState } from 'react';
import { createSubTask } from '../services/subTaskService';

const SubTaskForm = ({ taskId, refreshTaskData }) => {
    const [title, setTitle] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        await createSubTask({ title, isComplete: false, taskId });
        setTitle('');
        refreshTaskData();
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                placeholder="SubTask Title"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                required
            />
            <button className="add-button" type="submit">Add</button>
        </form>
    );
};

export default SubTaskForm;
