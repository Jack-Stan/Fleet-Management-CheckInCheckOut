import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import HomePage from "./pages/HomePage";
import VoertuigenPage from "./pages/VoertuigenPage";
import ChauffeursPage from "./pages/ChauffeursPage";
import ReserveringenPage from "./pages/ReserveringenPage";
import VoertuigenDetailsPage from "./pages/VoertuigenDetailsPage";
import NavBar from "./navigation/NavBar";


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
          <Route path="/voertuigen/:chassisNumber" element={<VoertuigenDetailsPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
