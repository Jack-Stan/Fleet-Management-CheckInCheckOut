// src/ReserveringenPage.js

import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";

const ReserveringenPage = () => {
  const [reserveringen, setReserveringen] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    // Hier kun je de API aanroepen om reserveringen op te halen
    const fetchReserveringen = async () => {
      try {
        const response = await fetch("URL_VAN_JE_API");
        const data = await response.json();
        setReserveringen(data);
      } catch (error) {
        console.error("Fout bij het ophalen van reserveringen:", error);
      }
    };

    fetchReserveringen();
  }, []);

  return (
    <div className="container mt-4">
      {" "}
      {/* Bootstrap container voor marge */}
      <h1>Reserveringen</h1>
      {/* Terug-knop */}
      <button className="btn btn-secondary mb-3" onClick={() => navigate(-1)}>
        Terug
      </button>
      {/* Voeg Nieuwe Reservering Toe-knop */}
      <button
        className="btn btn-primary mb-3"
        onClick={() => navigate("/reserveringen/add")}
      >
        Voeg Nieuwe Reservering Toe
      </button>
      <table className="table table-striped">
        {" "}
        <thead>
          <tr>
            <th>Voertuig</th>
            <th>Chauffeur</th>
            <th>Startdatum</th>
            <th>Einddatum</th>
            <th>Details</th>
          </tr>
        </thead>
        <tbody>
          {reserveringen.length > 0 ? (
            reserveringen.map((reservering) => (
              <tr key={reservering.id}>
                <td>{reservering.voertuig}</td>
                <td>{reservering.chauffeur}</td>
                <td>{new Date(reservering.startDatum).toLocaleDateString()}</td>
                <td>{new Date(reservering.eindDatum).toLocaleDateString()}</td>
                <td>
                  <Link
                    to={`/reserveringen/${reservering.id}`}
                    className="btn btn-info"
                  >
                    {/* Bootstrap knop voor details */}
                    Details
                  </Link>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="5">Geen reserveringen gevonden</td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
};

export default ReserveringenPage;
