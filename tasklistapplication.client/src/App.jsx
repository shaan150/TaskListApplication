import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import TaskList from './components/TaskList';
import './App.css';

const App = () => {
    return (
        <Router>
            <div className="App">
                <Routes>
                    <Route path="/" element={<TaskList />} />
                    {/* Add more routes here as needed */}
                </Routes>
            </div>
        </Router>
    );
};

export default App;
