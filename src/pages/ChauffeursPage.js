import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";

const ChauffeursPage = () => {
  const [chauffeurs, setChauffeurs] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchChauffeurs = async () => {
      try {
        const response = await fetch("URL_VAN_JE_API"); // Vervang dit door je API URL
        const data = await response.json();
        setChauffeurs(data);
      } catch (error) {
        console.error("Fout bij het ophalen van chauffeurs:", error);
      }
    };

    fetchChauffeurs();
  }, []);

  return (
    <div className="container mt-4">
      {" "}
      {/* Bootstrap container voor marge */}
      <h1>Chauffeurs</h1>
      {/* Terug-knop */}
      <button className="btn btn-secondary mb-3" onClick={() => navigate(-1)}>
        Terug
      </button>
      {/* Voeg Nieuwe Chauffeur Toe-knop */}
      <button
        className="btn btn-primary mb-3"
        onClick={() => navigate("/chauffeurs/add")}
      >
        Voeg Nieuwe Chauffeur Toe
      </button>
      <table className="table table-striped">
        {" "}
        {/* Bootstrap tabel met styling */}
        <thead>
          <tr>
            <th>Naam</th>
            <th>Geboortedatum</th>
            <th>Rijbewijsnummer</th>
            <th>Details</th>
          </tr>
        </thead>
        <tbody>
          {chauffeurs.length > 0 ? (
            chauffeurs.map((chauffeur) => (
              <tr key={chauffeur.licenseNumber}>
                <td>{chauffeur.name}</td>
                <td>{new Date(chauffeur.birthDate).toLocaleDateString()}</td>
                <td>{chauffeur.driverLicenseNumber}</td>
                <td>
                  <Link
                    to={`/chauffeurs/${chauffeur.licenseNumber}`}
                    className="btn btn-info" // Bootstrap knop voor details
                  >
                    Details
                  </Link>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="4">Geen chauffeurs gevonden</td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
};

export default ChauffeursPage;
