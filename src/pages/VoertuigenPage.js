// src/VoertuigenPage.js

import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";

const VoertuigenPage = () => {
  const navigate = useNavigate();
  const [voertuigen, setVoertuigen] = useState([]);

  const fetchVoertuigen = async () => {
    // Dummy data
    const dummyData = [
      {
        chassisNumber: "ABC123456",
        make: "Tesla",
        model: "Model S",
        licensePlate: "1-ABC-123",
        seats: 5,
        doors: 4,
        color: "Red",
        fuelType: "Electric",
      },
      {
        chassisNumber: "XYZ987654",
        make: "BMW",
        model: "X5",
        licensePlate: "1-XYZ-987",
        seats: 7,
        doors: 5,
        color: "Black",
        fuelType: "Diesel",
      },
    ];

    setVoertuigen(dummyData);
  };

  useEffect(() => {
    fetchVoertuigen();
  }, []);

  return (
    <div className="container mt-4">
      <h1>Voertuigen Overzicht</h1>
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
        <thead>
          <tr>
            <th>Chassisnummer</th>
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
                <td>{voertuig.chassisNumber}</td>
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
              <td colSpan="7" className="text-center">
                Geen voertuigen gevonden
              </td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
};

export default VoertuigenPage;
