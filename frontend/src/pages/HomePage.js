import React, { useState, useEffect } from 'react';
import axios from 'axios';

function HomePage() {
  const [vehicles, setVehicles] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios.get('/api/voertuigen')
      .then(response => setVehicles(response.data))
      .catch(error => setError("Er is een fout opgetreden bij het ophalen van voertuigen."));
  }, []);

  return (
    <div className="page-container">
      <h1>Home Pagina</h1>
      {error && <div className="error-message">{error}</div>}
      <div className="vehicle-list">
        {vehicles.map(vehicle => (
          <div key={vehicle.chassisNumber} className="vehicle-item">
            <h3>{vehicle.model}</h3>
            <p>{vehicle.brand}</p>
          </div>
        ))}
      </div>
    </div>
  );
}

export default HomePage;
