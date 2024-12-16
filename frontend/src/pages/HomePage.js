import React, { useState, useEffect } from 'react';
import axios from 'axios';
import '../App.css'; // Zorg ervoor dat je App.css import vanuit dezelfde map

function HomePage() {
  const [vehicles, setVehicles] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios.get('/api/voertuigen')
      .then(response => setVehicles(response.data))
      .catch(error => setError("Er is een fout opgetreden bij het ophalen van voertuigen."));
  }, []);

  return (
    <div className="home-page page-container">
      <div className="container-header">
        <h1>Voertuigen</h1>
        <button>Nieuw Voertuig</button>
      </div>

      {error && <div className="error-message">{error}</div>}
      
      <div className="vehicle-list">
        {vehicles.length > 0 ? (
          vehicles.map(vehicle => (
            <div key={vehicle.chassisNumber} className="vehicle-item">
              <h3>{vehicle.model}</h3>
              <p>{vehicle.brand}</p>
              <div className="details">
                <p><strong>Chassis nummer:</strong> {vehicle.chassisNumber}</p>
                <p><strong>Prijs per dag:</strong> €{vehicle.pricePerDay}</p>
              </div>
            </div>
          ))
        ) : (
          <p>Geen voertuigen beschikbaar.</p>
        )}
      </div>
    </div>
  );
}

export default HomePage;
