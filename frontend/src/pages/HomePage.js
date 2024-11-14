import React from "react";
import { useNavigate } from "react-router-dom";

const HomePage = () => {
  const navigate = useNavigate();

  // Dummy data voor de statistieken
  const stats = {
    vehicles: 50,
    drivers: 20,
    reservations: 15,
    activeReservations: 5,
  };

  return (
    <div className="container mt-5">
      {" "}
      <h1 className="text-center mb-4">Fleet Management Dashboard</h1>
      {/* Statistieken overzicht */}
      <div className="row mb-5">
        {" "}
        <div className="col-md-3 mb-4">
          {" "}
          <div className="card text-white bg-primary">
            <div className="card-header">Voertuigen</div>
            <div className="card-body">
              <h5 className="card-title">{stats.vehicles}</h5>
              <p className="card-text">Totaal voertuigen</p>
            </div>
          </div>
        </div>
        <div className="col-md-3 mb-4">
          <div className="card text-white bg-success">
            <div className="card-header">Chauffeurs</div>
            <div className="card-body">
              <h5 className="card-title">{stats.drivers}</h5>
              <p className="card-text">Totaal chauffeurs</p>
            </div>
          </div>
        </div>
        <div className="col-md-3 mb-4">
          <div className="card text-white bg-warning">
            <div className="card-header">Actieve Reserveringen</div>
            <div className="card-body">
              <h5 className="card-title">{stats.activeReservations}</h5>
              <p className="card-text">Actieve reserveringen</p>
            </div>
          </div>
        </div>
        <div className="col-md-3 mb-4">
          <div className="card text-white bg-danger">
            <div className="card-header">Totaal Reserveringen</div>
            <div className="card-body">
              <h5 className="card-title">{stats.reservations}</h5>
              <p className="card-text">Totaal reserveringen</p>
            </div>
          </div>
        </div>
      </div>
      {/* Veelgebruikte acties */}
      <h2 className="text-center mb-4">Acties</h2>
      <div className="d-flex justify-content-center mb-5">
        {" "}
        <button
          className="btn btn-primary me-3"
          onClick={() => navigate("/voertuigen/add")}
        >
          Nieuw Voertuig Toevoegen
        </button>
        <button
          className="btn btn-success me-3"
          onClick={() => navigate("/chauffeurs/add")}
        >
          Nieuwe Chauffeur Toevoegen
        </button>
        <button
          className="btn btn-warning me-3"
          onClick={() => navigate("/reserveringen/add")}
        >
          Nieuwe Reservering Maken
        </button>
        <button className="btn btn-info" onClick={() => navigate("/check")}>
          Check-in/Check-out
        </button>
      </div>
    </div>
  );
};

export default HomePage;
