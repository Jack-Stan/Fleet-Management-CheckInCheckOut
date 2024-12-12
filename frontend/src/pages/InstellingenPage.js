import React, { useState, useEffect } from 'react';
import axios from 'axios';

function InstellingenPage() {
  const [settings, setSettings] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios.get('/api/instellingen')
      .then(response => setSettings(response.data))
      .catch(error => setError("Er is een fout opgetreden bij het ophalen van instellingen."));
  }, []);

  if (error) return <div className="error-message">{error}</div>;

  return (
    <div className="page-container">
      <h1>Instellingen Pagina</h1>
      {settings ? (
        <div className="settings">
          <h3>Algemene Instellingen</h3>
          <p><strong>Maximale reserveringen per dag:</strong> {settings.maxReservationsPerDay}</p>
          <p><strong>Werkuren:</strong> {settings.workingHours}</p>
        </div>
      ) : (
        <p>Gegevens laden...</p>
      )}
    </div>
  );
}

export default InstellingenPage;
