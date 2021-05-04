using System;
using System.Collections.Generic;
using System.Text;

namespace Location
{
    public class Vehicule
    {
        public string Immatriculation  { get; private set; }

        public string Marque { get; private set; }

        public string Modele { get; private set; }

        public string Couleur { get; private set; }

        public double PrixReservation { get; private set; }

        public double TarifKilometrique { get; private set; }

        public int PuissanceFiscale { get; private set; }

        public Vehicule(string immatriculation, string marque, string modele, string couleur, double prixreservation, double tarifKilometrique, int puissanceFiscale)
        {
            this.Immatriculation = immatriculation;
            this.Marque = marque;
            this.Modele = modele;
            this.Couleur = couleur;
            this.PrixReservation = prixreservation;
            this.TarifKilometrique = tarifKilometrique;
            this.PuissanceFiscale = puissanceFiscale;
        }
    }
}
