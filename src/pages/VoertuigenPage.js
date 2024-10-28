// src/VoertuigenPage.js

import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";

const VoertuigenPage = () => {
  const navigate = useNavigate();
  const [voertuigen, setVoertuigen] = useState([]);

  // Functie om voertuigen op te halen
  const fetchVoertuigen = async () => {
    try {
      const response = await fetch("https://your-api-url/api/vehicles"); // Vervang dit door je API URL
      const data = await response.json();
      setVoertuigen(data);
    } catch (error) {
      console.error("Error fetching vehicles:", error);
    }
  };

  useEffect(() => {
    fetchVoertuigen();
  }, []);

  return (
    <div className="container mt-4">
      {" "}
      {/* Bootstrap container voor marge */}
      <h1>Voertuigen Overzicht</h1>
      {/* Terug-knop */}
      <button className="btn btn-secondary mb-3" onClick={() => navigate(-1)}>
        Terug
      </button>
      <button
        className="btn btn-primary mb-3"
        onClick={() => navigate("/voertuigen/add")}
      >
        Voeg Nieuw Voertuig Toe
      </button>
      <table className="table table-striped">
        {" "}
        {/* Bootstrap tabel met styling */}
        <thead>
          <tr>
            <th>Merk</th>
            <th>Model</th>
            <th>Kenteken</th>
            <th>Aantal Stoelen</th>
            <th>Aantal Deuren</th>
            <th>Acties</th>
          </tr>
        </thead>
        <tbody>
          {voertuigen.length > 0 ? (
            voertuigen.map((voertuig) => (
              <tr key={voertuig.chassisNumber}>
                <td>{voertuig.make}</td>
                <td>{voertuig.model}</td>
                <td>{voertuig.licensePlate}</td>
                <td>{voertuig.seats}</td>
                <td>{voertuig.doors}</td>
                <td>
                  <button
                    className="btn btn-info"
                    onClick={() =>
                      navigate(`/voertuigen/${voertuig.chassisNumber}`)
                    }
                  >
                    Bekijken
                  </button>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="6" className="text-center">
                Geen voertuigen gevonden
              </td>{" "}
              {/* Geen voertuigen gevonden melding */}
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
};

export default VoertuigenPage;
