import React, { useState, useEffect } from 'react';
import axios from 'axios';
import '../App.css';  // <-- Import hier toevoegen

function VoertuigPage() {
  const [vehicles, setVehicles] = useState([]);
  const [newVehicle, setNewVehicle] = useState({
    model: '',
    brand: '',
    chassisNumber: '',
    licensePlate: '',
    fuelType: '',
    vehicleType: '',
    color: '',
    seats: '',
    doors: '',
  });
  const [error, setError] = useState(null);
  const [showForm, setShowForm] = useState(false);

  useEffect(() => {
    axios.get('/api/voertuigen')
      .then(response => setVehicles(response.data))
      .catch(error => setError("Er is een fout opgetreden bij het ophalen van voertuigen."));
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewVehicle({
      ...newVehicle,
      [name]: value
    });
  };

  const handleAddVehicle = (e) => {
    e.preventDefault();
    axios.post('/api/voertuigen', newVehicle)
      .then(response => {
        setVehicles([...vehicles, response.data]);
        setNewVehicle({
          model: '',
          brand: '',
          chassisNumber: '',
          licensePlate: '',
          fuelType: '',
          vehicleType: '',
          color: '',
          seats: '',
          doors: '',
        });
        setShowForm(false); // Sluit het formulier na toevoeging
      })
      .catch(error => setError("Er is een fout opgetreden bij het toevoegen van het voertuig."));
  };

  const handleDeleteVehicle = (chassisNumber) => {
    axios.delete(`/api/voertuigen/${chassisNumber}`)
      .then(() => {
        setVehicles(vehicles.filter(vehicle => vehicle.chassisNumber !== chassisNumber));
      })
      .catch(error => setError("Er is een fout opgetreden bij het verwijderen van het voertuig."));
  };

  return (
    <div className="page-container">
      <h1>Voertuig Pagina</h1>
      {error && <div className="error-message">{error}</div>}

      <div className="vehicle-actions">
        <button onClick={() => setShowForm(true)}>Nieuw Voertuig Toevoegen</button>
      </div>

      {showForm && (
        <form onSubmit={handleAddVehicle} className="add-vehicle-form">
          <h2>Voertuig Toevoegen</h2>
          <div>
            <label>Model:</label>
            <input
              type="text"
              name="model"
              value={newVehicle.model}
              onChange={handleInputChange}
              required
            />
          </div>
          <div>
            <label>Merk:</label>
            <input
              type="text"
              name="brand"
              value={newVehicle.brand}
              onChange={handleInputChange}
              required
            />
          </div>
          <div>
            <label>Chassisnummer:</label>
            <input
              type="text"
              name="chassisNumber"
              value={newVehicle.chassisNumber}
              onChange={handleInputChange}
              required
            />
          </div>
          <div>
            <label>Kenteken:</label>
            <input
              type="text"
              name="licensePlate"
              value={newVehicle.licensePlate}
              onChange={handleInputChange}
              required
            />
          </div>
          <div>
            <label>Brandstoftype:</label>
            <input
              type="text"
              name="fuelType"
              value={newVehicle.fuelType}
              onChange={handleInputChange}
              required
            />
          </div>
          <div>
            <label>Voertuigtype:</label>
            <input
              type="text"
              name="vehicleType"
              value={newVehicle.vehicleType}
              onChange={handleInputChange}
              required
            />
          </div>
          <div>
            <label>Kleur:</label>
            <input
              type="text"
              name="color"
              value={newVehicle.color}
              onChange={handleInputChange}
              required
            />
          </div>
          <div>
            <label>Aantal stoelen:</label>
            <input
              type="number"
              name="seats"
              value={newVehicle.seats}
              onChange={handleInputChange}
              required
            />
          </div>
          <div>
            <label>Aantal deuren:</label>
            <input
              type="number"
              name="doors"
              value={newVehicle.doors}
              onChange={handleInputChange}
              required
            />
          </div>
          <button type="submit">Voertuig Toevoegen</button>
          <button type="button" onClick={() => setShowForm(false)}>Annuleren</button>
        </form>
      )}

      <div className="vehicle-list">
        {vehicles.map(vehicle => (
          <div key={vehicle.chassisNumber} className="vehicle-item">
            <h3>{vehicle.model}</h3>
            <p>{vehicle.brand}</p>
            <button onClick={() => handleDeleteVehicle(vehicle.chassisNumber)}>Verwijderen</button>
          </div>
        ))}
      </div>
    </div>
  );
}

export default VoertuigPage;
