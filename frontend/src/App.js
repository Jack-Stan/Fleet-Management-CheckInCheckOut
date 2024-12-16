import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import HomePage from "./pages/HomePage";
import VoertuigenPage from "./pages/VoertuigenPage";
import ChauffeursPage from "./pages/ChauffeursPage";
import ReserveringenPage from "./pages/ReserveringenPage";
import VoertuigenDetailsPage from "./pages/VoertuigenDetailsPage";
import NavBar from "./navigation/NavBar";
import LoginPage from "./pages/LoginPage";
import CheckInPage from "./pages/CheckInPage";
import CheckOutPage from "./pages/CheckOutPage";

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
          <Route path="/login" element={<LoginPage />} />
          <Route path="/checkIn" element={<CheckInPage />} />
          <Route path="/checkOut" element={<CheckOutPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
