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

        public DateTime DateNaissance { get; private set; }

        public DateTime DateObtentionPermis { get; private set; }

        public int NumeroPermis { get; private set; }


        public Client(string identifiant, string mdp)
        {
            this.Identifiant = identifiant;
            this.Mdp = mdp;
        }
    }
}
