// src/VoertuigDetailPage.js

import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";

const VoertuigDetailPage = () => {
  const { chassisNumber } = useParams();
  const navigate = useNavigate();
  const [voertuig, setVoertuig] = useState(null);

  // Dummy data
  const fetchVoertuigDetails = async () => {
    const dummyVehicleData = {
      chassisNumber: "ABC123456",
      make: "Tesla",
      model: "Model S",
      licensePlate: "1-ABC-123",
      seats: 5,
      doors: 4,
      color: "Red",
      fuelType: "Electric",
      mileage: "50,000 km",
      lastServiceDate: "2023-09-15",
    };

    setVoertuig(dummyVehicleData);
  };

  useEffect(() => {
    fetchVoertuigDetails();
  }, [chassisNumber]);

  if (!voertuig) return <p>Loading...</p>;

  return (
    <div className="container mt-4">
      <h1>Voertuig Details</h1>
      <button className="btn btn-secondary mb-3" onClick={() => navigate(-1)}>
        Terug
      </button>
      <div className="card">
        <div className="card-header">
          <h3>
            {voertuig.make} {voertuig.model}
          </h3>
        </div>
        <div className="card-body">
          <p>
            <strong>Chassisnummer:</strong> {voertuig.chassisNumber}
          </p>
          <p>
            <strong>Kenteken:</strong> {voertuig.licensePlate}
          </p>
          <p>
            <strong>Aantal Stoelen:</strong> {voertuig.seats}
          </p>
          <p>
            <strong>Aantal Deuren:</strong> {voertuig.doors}
          </p>
          <p>
            <strong>Kleur:</strong> {voertuig.color}
          </p>
          <p>
            <strong>Brandstoftype:</strong> {voertuig.fuelType}
          </p>
          <p>
            <strong>Kilometerstand:</strong> {voertuig.mileage}
          </p>
          <p>
            <strong>Laatste onderhoudsdatum:</strong> {voertuig.lastServiceDate}
          </p>
        </div>
      </div>
    </div>
  );
};

export default VoertuigDetailPage;
