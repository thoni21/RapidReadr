import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

import Home from './Pages/HomePage.tsx';
import Login from './Pages/Login.tsx';
import Register from './Pages/Register.tsx';
import ChoosePdfToReadPage from './Pages/ChoosePdfToReadPage.tsx';
import DisplayPdfToReadPage from './Pages/DisplayPdfToReadPage.tsx';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/" element={<Home />} />
                <Route path="/choose" element={<ChoosePdfToReadPage />} />
                <Route path="/display/:id" element={<DisplayPdfToReadPage />} />
            </Routes>
        </BrowserRouter>
    );
}
export default App;