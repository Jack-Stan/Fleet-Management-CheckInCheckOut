import axios from 'axios';

// Maak een nieuwe instantie van Axios met een basis-URL
const api = axios.create({
  baseURL: 'http://localhost:5000/api',  // Zorg ervoor dat dit het juiste backend URL is
  headers: {
    'Content-Type': 'application/json',
  },
});

// Functie voor het ophalen van gegevens
export const getVehicles = () => api.get('/voertuigen');

// Functie voor het toevoegen van een voertuig
export const addVehicle = (vehicle) => api.post('/voertuigen', vehicle);

// Voeg andere API-aanroepen toe (bijvoorbeeld voor chauffeurs, reserveringen, enz.)
export const getChauffeurs = () => api.get('/chauffeurs');
export const getReserveringen = () => api.get('/reserveringen');

export default api;
