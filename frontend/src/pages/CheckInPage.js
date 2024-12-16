import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import { Button } from 'react-bootstrap';

function CheckInPage() {
  const [checkData, setCheckData] = useState(null);
  const [error, setError] = useState(null);
  const [damageImage, setDamageImage] = useState(null);
  const [damagePreview, setDamagePreview] = useState(null);
  
  const { vehicleId } = useParams();  // Ophalen van voertuigID uit URL

  useEffect(() => {
    // Ophalen van gegevens voor het voertuig op basis van het vehicleId
    axios.get(`/api/voertuigen/${vehicleId}`)
      .then(response => setCheckData(response.data))
      .catch(error => setError("Er is een fout opgetreden bij het ophalen van voertuiggegevens."));
  }, [vehicleId]);

  const handleDamageUpload = (event) => {
    const file = event.target.files[0];
    if (file) {
      setDamageImage(file);
      const reader = new FileReader();
      reader.onloadend = () => {
        setDamagePreview(reader.result);
      };
      reader.readAsDataURL(file);
    }
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    if (!damageImage) {
      setError("Er moet een schadefoto worden geüpload.");
      return;
    }

    const formData = new FormData();
    formData.append('foto', damageImage);

    axios.post(`/api/checkin-voertuig/${vehicleId}`, formData)
      .then(response => {
        alert("Check-in succesvol verwerkt!");
      })
      .catch(error => {
        setError(error.response?.data?.message || "Er is een fout opgetreden.");
      });
  };

  return (
    <div className="check-in-page">
      <h1>Check-in Voertuig</h1>

      {error && <div className="error-message">{error}</div>}

      {checkData ? (
        <div>
          <p><strong>Voertuig:</strong> {checkData.voertuig}</p>
          <p><strong>Huurder:</strong> {checkData.huurder}</p>

          {/* Schade uploaden */}
          <div>
            <input
              type="file"
              accept="image/*"
              capture="camera"
              onChange={handleDamageUpload}
            />
            {damagePreview && <img src={damagePreview} alt="Schade preview" />}
          </div>

          {/* Submit formulier */}
          <Button variant="primary" onClick={handleSubmit}>
            Verwerk Check-in
          </Button>
        </div>
      ) : (
        <p>Gegevens laden...</p>
      )}
    </div>
  );
}

export default CheckInPage;
