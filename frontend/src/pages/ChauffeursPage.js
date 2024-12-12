import React, { useState, useEffect } from 'react';
import axios from 'axios';

function ChauffeursPage() {
  const [drivers, setDrivers] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios.get('/api/chauffeurs')
      .then(response => setDrivers(response.data))
      .catch(error => setError("Er is een fout opgetreden bij het ophalen van chauffeurs."));
  }, []);

  return (
    <div className="page-container">
      <h1>Chauffeurs Pagina</h1>
      {error && <div className="error-message">{error}</div>}
      <div className="driver-list">
        {drivers.map(driver => (
          <div key={driver.id} className="driver-item">
            <h3>{driver.name}</h3>
            <p>{driver.license}</p>
          </div>
        ))}
      </div>
    </div>
  );
}

export default ChauffeursPage;
