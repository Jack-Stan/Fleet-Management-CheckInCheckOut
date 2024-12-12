import React, { useState, useEffect } from 'react';
import axios from 'axios';

function ReserveringenPage() {
  const [reservations, setReservations] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios.get('/api/reserveringen')
      .then(response => setReservations(response.data))
      .catch(error => setError("Er is een fout opgetreden bij het ophalen van reserveringen."));
  }, []);

  return (
    <div className="page-container">
      <h1>Reserveringen Pagina</h1>
      {error && <div className="error-message">{error}</div>}
      <div className="reservation-list">
        {reservations.map(reservation => (
          <div key={reservation.id} className="reservation-item">
            <h3>{reservation.vehicleModel}</h3>
            <p>{reservation.date}</p>
          </div>
        ))}
      </div>
    </div>
  );
}

export default ReserveringenPage;
