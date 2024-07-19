import React, { useState, useEffect } from 'react';
import { updateSubTask } from '../services/subTaskService';
import './css/SubTaskForm.css';

const SubTask = ({ subTask, onDelete, refreshTaskData }) => {
    const [isEditing, setIsEditing] = useState(false);
    const [editedTitle, setEditedTitle] = useState(subTask.title);
    const [editedIsComplete, setEditedIsComplete] = useState(subTask.isComplete);
    const [isChanged, setIsChanged] = useState(false);

    useEffect(() => {
        setEditedTitle(subTask.title);
        setEditedIsComplete(subTask.isComplete);
    }, [subTask]);

    const handleTitleClick = () => {
        setIsEditing(true);
    };

    const handleTitleChange = (e) => {
        setEditedTitle(e.target.value);
        setIsChanged(true);
    };

    const handleCheckboxChange = (e) => {
        setEditedIsComplete(e.target.checked);
        setIsChanged(true);
    };

    const handleUpdateClick = async () => {
        await updateSubTask(subTask.id, { ...subTask, title: editedTitle, isComplete: editedIsComplete });
        setIsEditing(false);
        setIsChanged(false);
        refreshTaskData();
    };

    const handleDelete = async (id) => {
        await onDelete(id);
        refreshTaskData();
    }

    return (
        <div className="subtask-card">
            {isEditing ? (
                <div>
                    <input
                        type="text"
                        value={editedTitle}
                        onChange={handleTitleChange}
                        autoFocus
                    />
                </div>
            ) : (
                <h3 onClick={handleTitleClick}>{subTask.title}</h3>
            )}
            <label className="complete-label">
                Complete:
                <input
                    type="checkbox"
                    checked={editedIsComplete}
                    onChange={handleCheckboxChange}
                    disabled={!isEditing}
                />
            </label>
            {isEditing && (
                <button onClick={handleUpdateClick}>Update</button>
            )}
            <button className="delete-button" onClick={() => handleDelete(subTask.id)}>Delete</button>
        </div>
    );
};

export default SubTask;
