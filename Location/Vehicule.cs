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

        public DateTime DateDebutV { get; private set; }

        public DateTime DateFinV { get; private set; }

        public string EstSelect { get; set; }

        public Vehicule(string immatriculation, string marque, string modele, string couleur, double prixReservation, double tarifKilometrique, int puissanceFiscale)
        {
            this.Immatriculation = immatriculation;
            this.Marque = marque;
            this.Modele = modele;
            this.Couleur = couleur;
            this.PrixReservation = prixReservation;
            this.TarifKilometrique = tarifKilometrique;
            this.PuissanceFiscale = puissanceFiscale;
        }

        public Vehicule(string immatriculation, string marque, string modele, string couleur, double prixReservation, double tarifKilometrique, int puissanceFiscale, DateTime d, DateTime f, string estSelect)
        {
            this.Immatriculation = immatriculation;
            this.Marque = marque;
            this.Modele = modele;
            this.Couleur = couleur;
            this.PrixReservation = prixReservation;
            this.TarifKilometrique = tarifKilometrique;
            this.PuissanceFiscale = puissanceFiscale;
            this.DateDebutV = d;
            this.DateFinV = f;
            this.EstSelect = estSelect;
        }

        public Boolean estDisponible(DateTime deb, DateTime fin)
        {
            Boolean dispo = false;

            if ((fin > DateDebutV && deb < DateFinV))
            {
                dispo = false;
            }
            else
            {
                if ((deb < DateDebutV && fin < DateDebutV))
                {
                    dispo = true;
                }
                else
                {
                    dispo = true;
                }
            }

            return dispo;
        }

        public Boolean EstSelectionne(Client c)
        {
            Boolean sel = true;

            return sel;
        }
    }
}
