import React, { useState, useEffect } from 'react';
import axios from 'axios';

function CheckPage() {
  const [checkData, setCheckData] = useState(null);
  const [error, setError] = useState(null);
  const [damageImage, setDamageImage] = useState(null);
  const [damagePreview, setDamagePreview] = useState(null);
  const [actionType, setActionType] = useState('checkin');  // 'checkin' or 'checkout'

  useEffect(() => {
    axios.get('/api/check')
      .then(response => setCheckData(response.data))
      .catch(error => setError("Er is een fout opgetreden bij het ophalen van check-in gegevens."));
  }, []);

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

  const handleCheckInCheckOut = (event) => {
    event.preventDefault();
    if (!damageImage) {
      setError("Er moet een schadefoto worden geüpload.");
      return;
    }

    const formData = new FormData();
    formData.append('foto', damageImage);  // De naam moet overeenkomen met de backend
    formData.append('reserveringId', checkData.reservationNumber);
    formData.append('actionType', actionType);  // 'checkin' of 'checkout'

    axios.post(`/api/reservering/${checkData.reservationNumber}/upload-foto`, formData)
      .then(response => {
        alert(`${actionType === 'checkin' ? 'Check-in' : 'Check-out'} succesvol verwerkt!`);
      })
      .catch(error => {
        setError(error.response?.data?.message || "Er is een fout opgetreden bij het verwerken van de check-in/check-out.");
      });
  };

  if (error) return <div className="error-message">{error}</div>;

  return (
    <div className="page-container">
      <h1>Check Pagina</h1>
      {checkData ? (
        <div className="check-data">
          <h3>Reserveringen Check</h3>
          <p><strong>Reserveringsnummer:</strong> {checkData.reservationNumber}</p>
          <p><strong>Datum:</strong> {checkData.date}</p>
          <p><strong>Voertuig:</strong> {checkData.vehicleModel}</p>

          {/* Schade-upload sectie */}
          <div className="damage-upload">
            <h3>Schade Uploaden</h3>
            <input
              type="file"
              accept="image/*"
              capture="camera"  // Hiermee kan de gebruiker een foto nemen met hun camera (voor mobiel)
              onChange={handleDamageUpload}
            />
            {damagePreview && (
              <div className="damage-preview">
                <h4>Schade Preview:</h4>
                <img src={damagePreview} alt="Schade preview" />
              </div>
            )}
          </div>

          {/* Actie kiezen voor Check-in of Check-out */}
          <div className="action-selection">
            <label>
              <input
                type="radio"
                name="actionType"
                value="checkin"
                checked={actionType === 'checkin'}
                onChange={() => setActionType('checkin')}
              />
              Check-in
            </label>
            <label>
              <input
                type="radio"
                name="actionType"
                value="checkout"
                checked={actionType === 'checkout'}
                onChange={() => setActionType('checkout')}
              />
              Check-out
            </label>
          </div>

          {/* Check-in/Check-out knop */}
          <button onClick={handleCheckInCheckOut}>Verstuur Foto en Verwerk {actionType === 'checkin' ? 'Check-in' : 'Check-out'}</button>
        </div>
      ) : (
        <p>Gegevens laden...</p>
      )}
    </div>
  );
}

export default CheckPage;
