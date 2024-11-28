import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';

function ReserveringDetailPage() {
  const { reservationId } = useParams();
  const [reservationDetails, setReservationDetails] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios.get(`/api/reserveringen/${reservationId}`)
      .then(response => setReservationDetails(response.data))
      .catch(error => setError("Er is een fout opgetreden bij het ophalen van reserveringsdetails."));
  }, [reservationId]);

  if (error) return <div className="error-message">{error}</div>;

  return (
    <div className="page-container">
      <h1>Reserveringsdetail Pagina</h1>
      {reservationDetails ? (
        <div className="reservation-detail">
          <h3>Reserveringsnummer: {reservationDetails.id}</h3>
          <p><strong>Datum:</strong> {reservationDetails.date}</p>
          <p><strong>Voertuig:</strong> {reservationDetails.vehicleModel}</p>
          <p><strong>Chauffeur:</strong> {reservationDetails.driverName}</p>
          <p><strong>Huurprijs:</strong> €{reservationDetails.rentPrice}</p>
        </div>
      ) : (
        <p>Gegevens laden...</p>
      )}
    </div>
  );
}

export default ReserveringDetailPage;
