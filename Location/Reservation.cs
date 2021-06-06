using System;
using System.Collections.Generic;
using System.Text;

namespace Location
{
    public class Reservation
    {
        public DateTime DateDebutR { get; private set; }

        public DateTime DateFinR { get; private set; }

        public int EstimationKm { get; private set; }

        public int RealiseKm { get; private set; }

        public double PrixLocation { get; private set; }

        public Client Client { get; private set; }

        public Vehicule Vehicule { get; private set; }

        public List<Vehicule> Vehicules { get; private set; }
        public List<Vehicule> VehiculesDispo { get; private set; }

        public Reservation(DateTime dateDebutR, DateTime dateFinR, Client c, Vehicule v, List<Vehicule> listeVehiculeDispo)
        {
            this.DateDebutR = dateDebutR;
            this.DateFinR = dateFinR;
            this.Client = c;
            this.Vehicule = v;
            this.Vehicules = new List<Vehicule>();

            this.VehiculesDispo = listeVehiculeDispo;
            this.EstimationKm = 0;
            this.RealiseKm = 0;
        }

        public Reservation(DateTime dateDebutR, DateTime dateFinR)
        {
            this.DateDebutR = dateDebutR;
            this.DateFinR = dateFinR;
        }
    }
}
