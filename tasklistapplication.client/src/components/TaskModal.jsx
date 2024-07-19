import React, { useState } from 'react';
import Modal from 'react-modal';
import SubTask from './SubTask';
import SubTaskForm from './SubTaskForm';
import { deleteSubTask } from '../services/subTaskService';

Modal.setAppElement('#root');

const TaskModal = ({ task, isOpen, onRequestClose, onSave, refreshTaskData }) => {
    const [editableTitle, setEditableTitle] = useState(task.title);
    const [isComplete, setIsComplete] = useState(task.isComplete);

    const handleTitleChange = (e) => {
        setEditableTitle(e.target.value);
    };

    const handleStatusChange = (e) => {
        setIsComplete(e.target.checked);
    };

    const handleSave = () => {
        onSave(editableTitle, isComplete);
    };

    const handleDeleteSubTask = async (id) => {
        await deleteSubTask(id);
        refreshTaskData();
    };

    return (
        <Modal isOpen={isOpen} onRequestClose={onRequestClose} className="modal" overlayClassName="modal-overlay">
            <div className="modal-content">
                <h2>Edit Task</h2>
                <h3>{task.subTasksCount} Subtasks</h3>
                <div className="form-group">
                    <label>
                        Title:
                        <input type="text" value={editableTitle} onChange={handleTitleChange} />
                    </label>
                    <label>
                        Complete:
                        <input className="tickbox" type="checkbox" checked={isComplete} onChange={handleStatusChange} />
                    </label>
                </div>

                <div className="subtask-list">
                    {task.subTasks.map(subTask => (
                        <div className="subtask-list-item" key={subTask.id}>
                            <SubTask subTask={subTask} refreshTaskData={refreshTaskData} onDelete={ deleteSubTask} />
                        </div>
                    ))}
                </div>

                <SubTaskForm taskId={task.id} refreshTaskData={refreshTaskData} />
                <form>
                    <div className="button-group">
                        <button className="add-button" onClick={handleSave}>Save</button>
                        <button className="delete-button" onClick={onRequestClose}>Cancel</button>
                    </div>
                </form>
            </div>
        </Modal>
    );
};

export default TaskModal;
