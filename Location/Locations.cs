using System;
using System.Collections.Generic;
using System.Linq;

namespace Location
{
    public class Locations
    {
        private IDataLayer _dataLayer;

        public bool UtilisateurConnecte { get; private set; }

        public bool UtilisateurCree { get; private set; }

        public Client client { get; private set; }
        public Vehicule vehicule { get; private set; }
        public Reservation reservation { get; private set; }
        public List<Vehicule> Vehicules { get; private set; }
        public List<Vehicule> VehiculesDispo { get; private set; }

        public Locations()
        {
            /// si on utilisait la librairie au travers d'une application
            this._dataLayer = new DataLayer();
        }

        public Locations(IDataLayer dataLayer)
        {
            this._dataLayer = dataLayer;
        }

        public Locations(List<Vehicule> v)
        {
            this.Vehicules = new List<Vehicule>(v);
            this.VehiculesDispo = new List<Vehicule>(v);
        }

        public string ConnexionUtilisateur(string identifiant, string mdp)
        {
            Client client = this._dataLayer.Clients.SingleOrDefault(_ => _.Identifiant == identifiant);
            if (client == null)
            {
                this.UtilisateurConnecte = false;
                return "Identifiant non reconnu";
            }
            else
            {
                if (client.Mdp == mdp)
                {
                    this.UtilisateurConnecte = true;
                }
                else
                {
                    this.UtilisateurConnecte = false;
                    return "Mot de passe incorrect";
                }
            }

            return "";
        }

        public string CreationUtilisateur(String prenom, String nom, String identifiant, String mdp, DateTime dateDeNaissance, DateTime dateObtentionPermis, String numeroPermis)
        {
            Client clientIden = this._dataLayer.Clients.SingleOrDefault(_ => _.Identifiant == identifiant);
            Client clientNom = this._dataLayer.Clients.SingleOrDefault(_ => _.Nom == nom);
            Client clientNumPermis = this._dataLayer.Clients.SingleOrDefault(_ => _.NumeroPermis == numeroPermis);

            if (clientIden == null && clientNom == null)
            {
                if (clientNumPermis == null)
                {
                    this.UtilisateurCree = true;
                    return "Le compte a bien été crée";
                }
                else
                {
                    this.UtilisateurCree = false;
                    return "Ce numéro de permis est déjà dans notre base";
                }
            }
            else
            {
                this.UtilisateurCree = false;
                return "Le compte est déjà existant";
            }

            return "";
        }

        public void AfficherVehiculeDispo(List<Vehicule> listeVehicule,Reservation r)
        {

            for (int i=0; i<listeVehicule.Count; i++)
            {
                if(listeVehicule[i].estDisponible(r.DateDebutR,r.DateFinR)== true)
                {
                    VehiculesDispo[i] = listeVehicule[i];
                }
            }
        }


        public string VerifierDateValide(DateTime dateDebutR, DateTime dateFinR)
        {
            if(dateDebutR>= dateFinR )
            {
                return "Merci de saisir des dates de reservation correctes";
            }

            return "";
        }

        public string CreationReservation(DateTime dateDebutR, DateTime dateFinR, Client client, Vehicule v, List<Vehicule> listeVehiculeDispo)
        {
            int ageC = 0;
            Boolean aNumPermis = false;

            ageC = client.estAgeDe(client.DateDeNaissance);
            Console.WriteLine("Age du client : " + ageC);

            if (String.IsNullOrEmpty(client.NumeroPermis))
            {
                aNumPermis = false;
            }
            else
            {
                if (client.NumeroPermis.Length == 15)
                {
                    aNumPermis = true; 
                }
                else
                {
                    aNumPermis = false;
                }
            }

            

            if ((ageC >= 18) && (aNumPermis == true))
            {
                if ((ageC < 21) && (v.PuissanceFiscale >= 8))
                {
                    return "La reservation n'a pas pu être crée : vous devez avoir plus de 21 ans pour conduire le véhicule sélectionné. Il a une puissance fiscale de plus de 8 chevaux";
                }
                else
                {
                    if ((((ageC >= 21) && v.PuissanceFiscale <13) || (ageC <=25) && v.PuissanceFiscale < 13) || (ageC >= 26))
                    {
                        return "La reservation a été crée";       
                    }
                    else
                    {
                        return "La reservation n'a pas pu être crée : vous devez avoir plus de 25 ans pour conduire le véhicule sélectionné. Il a une puissance fiscale de plus de 13 chevaux";
                    }
                }
            }
            else
            {
                return "La reservation n'a pas été crée : vous devez avoir plus de 18 ans et un numéro de permis";
            }


            return "";
        }

        public string selectionMultiple(List<Vehicule> listeVehiculeDispo, List<Vehicule> listeVehiculesSel)
        {
            int cpt = 0;
            for (int i = 0; i < listeVehiculeDispo.Count; i++)
            {
                Console.WriteLine("Liste" + i + "" + listeVehiculeDispo[i].EstSelect);
                Console.WriteLine("  ");
                Console.WriteLine("vehicule sel" + listeVehiculesSel[i].EstSelect);
                if (listeVehiculeDispo[i].EstSelect.Equals("oui") && (listeVehiculesSel[i].EstSelect.Equals("oui")))
                {
                    cpt++;
                }

                if (cpt >= 2)
                {
                    Console.WriteLine("Impossible de continuer la réservation");
                    return "Impossible de continuer la réservation, vous avez sélectionné deux véhicules";
                }
            }
            return "";
        }

        public double TarifEstimeeReservation(Vehicule v, int estimationKm)
        {
            double prix = 0;

            prix = v.PrixReservation + v.TarifKilometrique * estimationKm;

            return prix;
        }

        public double TarifFinalReservation(Vehicule v, int reelKm)
        {
            double prix = 0;

            prix = v.PrixReservation + v.TarifKilometrique * reelKm;

            return prix;
        }
    }
}
