using System;
using System.Collections.Generic;
using System.Text;

namespace Location
{
    public class Client
    {
        public string Identifiant { get; private set; }

        public string Mdp { get; private set; }

        public string Prenom { get; private set; }

        public string Nom { get; private set; }

        public DateTime DateDeNaissance { get; private set; }

        public DateTime DateObtentionPermis { get; private set; }

        public string NumeroPermis { get; private set; }

        public Client(string identifiant, string mdp, string prenom, string nom, DateTime dateDeNaissance, DateTime dateObtentionPermis, string numeroPermis)
        {
            this.Identifiant = identifiant;
            this.Mdp = mdp;
            this.Prenom = prenom;
            this.Nom = nom;
            this.DateDeNaissance = dateDeNaissance;
            this.DateObtentionPermis = dateObtentionPermis;
            this.NumeroPermis = numeroPermis;
        }

        public int estAgeDe(DateTime dateDeNaissance)
        {
            // Age théorique
            int age = DateTime.Now.Year - dateDeNaissance.Year;

            // Date de l'anniversaire de cette année
            DateTime DateAnniv = new DateTime(DateTime.Now.Year, dateDeNaissance.Month, dateDeNaissance.Day);

            // Si pas encore passé, retirer 1 an
            if (DateAnniv > DateTime.Now)
                age--;

            return age;
        }
    }
}
