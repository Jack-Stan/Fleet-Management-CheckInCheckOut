import React from "react";
import { Link, useLocation } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "../NavBar.css";  // Zorg ervoor dat NavBar.css correct wordt geïmporteerd

const NavBar = () => {
  const location = useLocation();  // Huidige URL ophalen

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
      <div className="navbar-container">
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav">
            <li className="nav-item">
              <Link
                className={`nav-link ${location.pathname === "/" ? "active" : ""}`}
                to="/"
              >
                Home
              </Link>
            </li>
            <li className="nav-item">
              <Link
                className={`nav-link ${location.pathname === "/voertuigen" ? "active" : ""}`}
                to="/voertuigen"
              >
                Voertuigen
              </Link>
            </li>
            <li className="nav-item">
              <Link
                className={`nav-link ${location.pathname === "/chauffeurs" ? "active" : ""}`}
                to="/chauffeurs"
              >
                Chauffeurs
              </Link>
            </li>
            <li className="nav-item">
              <Link
                className={`nav-link ${location.pathname === "/reserveringen" ? "active" : ""}`}
                to="/reserveringen"
              >
                Reserveringen
              </Link>
            </li>
            <li className="nav-item">
              <Link
                className={`nav-link ${location.pathname === "/login" ? "active" : ""}`}
                to="/login"
              >
                Login
              </Link>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default NavBar;
