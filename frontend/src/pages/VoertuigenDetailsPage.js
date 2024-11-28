import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';

function VoertuigenDetailsPage() {
  const { chassisNumber } = useParams();
  const [vehicle, setVehicle] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios.get(`/api/voertuigen/${chassisNumber}`)
      .then(response => setVehicle(response.data))
      .catch(error => setError("Er is een fout opgetreden bij het ophalen van voertuigdetails."));
  }, [chassisNumber]);

  if (error) return <div className="error-message">{error}</div>;

  return (
    <div className="page-container">
      <h1>Voertuig Details</h1>
      {vehicle ? (
        <div className="vehicle-item">
          <h3>{vehicle.model}</h3>
          <p>{vehicle.brand}</p>
          <p>{vehicle.year}</p>
          <p>{vehicle.registrationNumber}</p>
        </div>
      ) : (
        <p>Gegevens laden...</p>
      )}
    </div>
  );
}

export default VoertuigenDetailsPage;
