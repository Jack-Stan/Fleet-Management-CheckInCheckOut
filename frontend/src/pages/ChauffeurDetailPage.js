import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getChauffeurById } from '../api'; // Functie toevoegen aan api.js

function ChauffeurDetailPage() {
  const [chauffeur, setChauffeur] = useState(null);
  const [loading, setLoading] = useState(true);
  const { id } = useParams();

  useEffect(() => {
    const fetchChauffeur = async () => {
      try {
        const response = await getChauffeurById(id); // Gebruik de functie uit api.js
        setChauffeur(response.data);
      } catch (error) {
        console.error('Er is een fout opgetreden bij het ophalen van de chauffeur', error);
      } finally {
        setLoading(false);
      }
    };

    fetchChauffeur();
  }, [id]);

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <h2>{chauffeur.name}</h2>
      <p>{chauffeur.email}</p>
      <p>{chauffeur.telefoon}</p>
    </div>
  );
}

export default ChauffeurDetailPage;
