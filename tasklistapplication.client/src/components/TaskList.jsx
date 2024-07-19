import React, { useState, useEffect } from 'react';
import { getTasks, deleteTask } from '../services/taskService';
import Task from './Task';
import TaskForm from './TaskForm';
import './css/TaskList.css';

const TaskList = () => {
    const [tasks, setTasks] = useState([]);

    useEffect(() => {
        fetchTasks();
    }, []);

    const fetchTasks = async () => {
        const data = await getTasks();
        setTasks(data);
    };

    const refreshTaskData = () => {
        fetchTasks();
    };

    const handleDelete = async (id) => {
        await deleteTask(id);
        fetchTasks();
    };

    return (
        <div className="task-list-container">
            <h1 className="header">Task List</h1>
            <TaskForm onTaskCreated={fetchTasks} />
            <div className="task-list">
                {tasks.map(task => (
                    <Task key={task.id} task={task} onDelete={handleDelete} refreshTaskData={refreshTaskData} />
                ))}
            </div>
        </div>
    );
};

export default TaskList;
