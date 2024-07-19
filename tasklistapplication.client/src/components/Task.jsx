import React, { useState } from 'react';
import SubTask from './SubTask';
import { updateTask } from '../services/taskService';
import TaskModal from './TaskModal';
import './css/Task.css';

const Task = ({ task, onDelete, refreshTaskData }) => {
    const [showSubTasks, setShowSubTasks] = useState(false);
    const [isModalOpen, setIsModalOpen] = useState(false);

    const handleUpdateTask = async (title, isComplete) => {
        await updateTask(task.id, { ...task, title: title, isComplete: isComplete });
        setIsModalOpen(false);
        refreshTaskData();
    };

    return (
        <div className="task-card" onClick={() => setIsModalOpen(true)}>
            <div className="task-header">
                <h2>{task.title}</h2>
                <div className="task-actions">
                    <label>
                        <input
                            className="tickbox"
                            type="checkbox"
                            checked={task.isComplete}
                            readOnly
                        />
                    </label>
                </div>
            </div>
            {showSubTasks && (
                <div className="subtask-section">
                    <div className="subtask-list">
                        {task.subTasks.map(subTask => (
                            <SubTask key={subTask.id} subTask={subTask} refreshTaskData={refreshTaskData} />
                        ))}
                    </div>
                </div>
            )}
            <div className="button-group">
                <button className="delete-button" onClick={(e) => { e.stopPropagation(); onDelete(task.id); }}>Delete Task</button>
            </div>
            <TaskModal
                task={task}
                isOpen={isModalOpen}
                onRequestClose={() => setIsModalOpen(false)}
                onSave={handleUpdateTask}
                refreshTaskData={refreshTaskData}
            />
        </div>
    );
};

export default Task;
