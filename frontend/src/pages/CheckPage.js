import React, { useState, useEffect } from 'react';
import axios from 'axios';

function CheckPage() {
  const [checkData, setCheckData] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios.get('/api/check')
      .then(response => setCheckData(response.data))
      .catch(error => setError("Er is een fout opgetreden bij het ophalen van check-in gegevens."));
  }, []);

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
        </div>
      ) : (
        <p>Gegevens laden...</p>
      )}
    </div>
  );
}

export default CheckPage;
