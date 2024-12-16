import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import { Button } from 'react-bootstrap';

function CheckOutPage() {
  const [checkData, setCheckData] = useState(null);
  const [error, setError] = useState(null);
  
  const { vehicleId } = useParams();  // Ophalen van voertuigID uit URL

  useEffect(() => {
    // Ophalen van gegevens voor het voertuig op basis van het vehicleId
    axios.get(`/api/voertuigen/${vehicleId}`)
      .then(response => setCheckData(response.data))
      .catch(error => setError("Er is een fout opgetreden bij het ophalen van voertuiggegevens."));
  }, [vehicleId]);

  const handleSubmit = () => {
    axios.post(`/api/checkout-voertuig/${vehicleId}`)
      .then(response => {
        alert("Check-out succesvol verwerkt!");
      })
      .catch(error => {
        setError(error.response?.data?.message || "Er is een fout opgetreden.");
      });
  };

  return (
    <div className="check-out-page">
      <h1>Check-out Voertuig</h1>

      {error && <div className="error-message">{error}</div>}

      {checkData ? (
        <div>
          <p><strong>Voertuig:</strong> {checkData.voertuig}</p>
          <p><strong>Huurder:</strong> {checkData.huurder}</p>

          {/* Submit formulier */}
          <Button variant="primary" onClick={handleSubmit}>
            Verwerk Check-out
          </Button>
        </div>
      ) : (
        <p>Gegevens laden...</p>
      )}
    </div>
  );
}

export default CheckOutPage;
