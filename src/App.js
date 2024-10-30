import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import HomePage from "./pages/HomePage";
import VoertuigenPage from "./pages/VoertuigenPage";
import ChauffeursPage from "./pages/ChauffeursPage";
import ReserveringenPage from "./pages/ReserveringenPage";
import NavBar from "./navigation/NavBar";
import "./App.css";

function App() {
  return (
    <Router>
      <div className="App">
        <NavBar />
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/voertuigen" element={<VoertuigenPage />} />
          <Route path="/chauffeurs" element={<ChauffeursPage />} />
          <Route path="/reserveringen" element={<ReserveringenPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
