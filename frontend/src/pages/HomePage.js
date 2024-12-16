import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Calendar, momentLocalizer } from 'react-big-calendar';
import moment from 'moment';
import { Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';

import 'react-big-calendar/lib/css/react-big-calendar.css';

const localizer = momentLocalizer(moment);

function HomePage() {
  const [events, setEvents] = useState([]);
  const [error, setError] = useState(null);
  const navigate = useNavigate(); // Gebruik useNavigate om te navigeren

  // Ophalen van de reserveringen van de backend API
  useEffect(() => {
    axios.get('/api/reserveringen')
      .then(response => {
        const formattedEvents = response.data.map(res => ({
          title: `${res.voertuig} - ${res.huurder}`,
          start: new Date(res.start),
          end: new Date(res.einde),
          id: res.id,
        }));
        setEvents(formattedEvents);
      })
      .catch(error => {
        setError("Er is een fout opgetreden bij het ophalen van reserveringen.");
      });
  }, []);

  // Handlers voor quick actions
  const handleCheckOut = () => {
    navigate('/checkout'); // Navigeren naar de CheckPage wanneer op de knop wordt geklikt
  };

  const handleCheckIn = () => {
    navigate('/checkin'); // Navigeren naar de CheckInPage wanneer op de knop wordt geklikt
  };

  const handleInspectVehicle = () => {
    // Voeg voertuig inspectie functionaliteit toe
    alert('Voertuig inspectie starten');
  };

  const handleAddDamage = () => {
    // Voeg schade toevoegen functionaliteit toe
    alert('Nieuwe schade toevoegen');
  };

  return (
    <div className="home-page">
      <h1></h1>

      {error && <div className="error-message">{error}</div>}

      <div className="calendar-container">
        <Calendar
          localizer={localizer}
          events={events}
          startAccessor="start"
          endAccessor="end"
          style={{ height: 500 }}
          selectable
        />
      </div>

      <div className="quick-actions">
        <Button variant="primary" onClick={handleCheckOut}>Check-out Voertuig</Button> 
        <Button variant="success" onClick={handleCheckIn}>Check-in Voertuig</Button>
        <Button variant="warning" onClick={handleInspectVehicle}>Voertuig Inspecteren</Button>
        <Button variant="danger" onClick={handleAddDamage}>Nieuwe Schade Toevoegen</Button>
      </div>
    </div>
  );
}

export default HomePage;
