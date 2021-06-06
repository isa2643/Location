using FluentAssertions;
using Location;
using SpecFlowLocation.Fake;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlowLocation.Steps
{
    [Binding]
    public sealed class LocationStepDefinitions
    {

        private readonly ScenarioContext _scenarioContext;

        private string _identifiant;
        private string _mdp;
        private string _prenom;
        private string _nom;
        private DateTime _dateDeNaissance;
        private DateTime _dateObtentionPermis;
        private string _numeroPermis;
        private int _estimKm;
        private int _reelKm;
        private double _tarifEstimeeReservation;
        private double _tarifFinalReservation;


        private Reservation _reservation;
        private Client _client;
        private Vehicule _vehicule;
        private List<Vehicule> _vehiculesSel;
        private List<Vehicule> _vehiculesDispo;

        private string _lastErrorMessage;
        private Locations _target;
        private FakeDataLayer _fakeDataLayer;

        public LocationStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._fakeDataLayer = new FakeDataLayer();
            this._target = new Locations(this._fakeDataLayer);
        }

        #region Background

        [Given(@"suivant les clients existants")]
        public void GivenFollowingExistingClients(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                this._fakeDataLayer.Clients.Add(new Client(row[0], row[1], row[2], row[3], DateTime.Parse(row[4]), DateTime.Parse(row[5]), row[6]));
            }
        }

        [Given(@"suivant les vehicules existants")]
        public void GivenFollowingExistingVehicules(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                this._fakeDataLayer.Vehicules.Add(new Vehicule(row[0], row[1], row[2], row[3], double.Parse(row[4]), double.Parse(row[5]), int.Parse(row[6]), DateTime.Parse(row[7]), DateTime.Parse(row[8]),row[9]));
            }
        }

        #endregion

        [Given(@"mon identifiant est ""(.*)""")]
        public void GivenMyUsernameIs(string identifiant)
        {
            this._identifiant = identifiant;
        }

        [Given(@"mon mot de passe est ""(.*)""")]
        public void GivenMyPasswordIs(string mdp)
        {
            this._mdp = mdp;
        }

        [Given(@"je renseigne mon prenom ""(.*)""")]
        public void GivenJeRenseigneMonPrenom(string prenom)
        {
            this._prenom = prenom;
        }

        [Given(@"je renseigne mon nom ""(.*)""")]
        public void GivenJeRenseigneMonNom(string nom)
        {
            this._nom=nom;
        }

        [Given(@"je renseigne mon identifiant ""(.*)""")]
        public void GivenJeRenseigneMonIdentifiant(string id)
        {
            this._identifiant=id;
        }

        [Given(@"je renseigne mon mot de passe ""(.*)""")]
        public void GivenJeRenseigneMonMotDePasse(string mot)
        {
            this._mdp=mot;
        }

        [Given(@"je renseigne ma date de naissance ""(.*)""")]
        public void GivenJeRenseigneMaDateDeNaissance(DateTime dateNaissance)
        {
            this._dateDeNaissance=dateNaissance;
        }

        [Given(@"je renseigne ma date d'obtention de permis ""(.*)""")]
        public void GivenJeRenseigneMaDateDObtentionDePermis(DateTime dateObtention)
        {
            this._dateObtentionPermis = dateObtention;
        }

        [Given(@"je renseigne mon numéro de permis ""(.*)""")]
        public void GivenJeRenseigneMonNumeroDePermis(string numeroPermis)
        {
            this._numeroPermis=numeroPermis;
        }

        [Given(@"Le client s'identifie")]
        public void GivenLeClientSIdentifie(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                this._client = new Client(row[0], row[1], row[2], row[3], DateTime.Parse(row[4]), DateTime.Parse(row[5]), row[6]);
            }
        }

        [Given(@"les dates de la reservation")]
        public void GivenLesDatesDeLaReservation(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                if (DateTime.Parse(row[0]) <= DateTime.Parse(row[1])){
                    this._reservation = new Reservation(DateTime.Parse(row[0]), DateTime.Parse(row[1]), this._target.client, this._target.vehicule, this._target.VehiculesDispo);
                }
                else
                {
                    this._lastErrorMessage = this._target.VerifierDateValide(DateTime.Parse(row[0]), DateTime.Parse(row[1]));
                    Console.WriteLine(this._lastErrorMessage);
                }
            }    
        }

        [Given(@"la liste des vehicules disponibles est presentee au client")]
        public void WhenLaListeDesVehiculesDisponiblesEstPresenteeAuClient(Table table)
        {
            this._vehiculesDispo = new List<Vehicule>();
            foreach (TableRow row in table.Rows)
            {
                this._vehiculesDispo.Add(new Vehicule(row[0], row[1], row[2], row[3], double.Parse(row[4]), double.Parse(row[5]), int.Parse(row[6]), DateTime.Parse(row[7]), DateTime.Parse(row[8]), (row[9])));
                
                this._target = new Locations(this._vehiculesDispo);

                this._target.AfficherVehiculeDispo(this._vehiculesDispo, this._reservation);
            }
        }

        [When(@"J'essaie de me connecter avec mon compte")]
        public void WhenITryToConnectToMyAccount()
        {
            this._lastErrorMessage = this._target.ConnexionUtilisateur(this._identifiant, this._mdp);
        }

        [When(@"J'essaie de me créer mon compte")]
        public void WhenJEssaieDeMeCreerMonCompte()
        {
            this._lastErrorMessage = this._target.CreationUtilisateur(this._prenom, this._nom, this._identifiant, this._mdp, this._dateDeNaissance, this._dateObtentionPermis, this._numeroPermis);
        }

        [When(@"le client selectionne un véhicule disponible")]
        public void WhenLeClientSelectionneUnVehiculeDisponible(Table table)
        {
            Boolean a = false;
            foreach (TableRow row in table.Rows)
            {
                this._vehicule = new Vehicule(row[0], row[1], row[2], row[3], double.Parse(row[4]), double.Parse(row[5]), int.Parse(row[6]), DateTime.Parse(row[7]), DateTime.Parse(row[8]), (row[9]));
                a=this._vehicule.EstSelectionne(this._target.client);
            }


            for (int i=0;i< this._vehiculesDispo.Count;i++)
            {
                if (this._vehiculesDispo[i].Immatriculation.Equals(this._vehicule.Immatriculation))
                {
                    this._vehiculesDispo[i].EstSelect = "oui";

                    Console.WriteLine(this._vehiculesDispo[i].Modele);
                    Console.WriteLine(this._vehiculesDispo[i].EstSelect);
                }
            }
        }

        [When(@"le client selectionne des véhicules disponibles")]
        public void WhenLeClientSelectionneDesVehiculesDisponibles(Table table)
        {
            this._vehiculesSel = new List<Vehicule>();
            foreach (TableRow row in table.Rows)
            {
                this._vehiculesSel.Add(new Vehicule(row[0], row[1], row[2], row[3], double.Parse(row[4]), double.Parse(row[5]), int.Parse(row[6]), DateTime.Parse(row[7]), DateTime.Parse(row[8]), (row[9])));
            }

            for (int i = 0; i < this._vehiculesDispo.Count; i++)
            {
                if (this._vehiculesDispo[i].Immatriculation.Equals(this._vehiculesSel[0].Immatriculation))
                {
                    this._vehiculesDispo[i].EstSelect = "oui";

                    Console.WriteLine(this._vehiculesDispo[i].Modele);
                    Console.WriteLine(this._vehiculesDispo[i].EstSelect);
                }

                if (this._vehiculesDispo[i].Immatriculation.Equals(this._vehiculesSel[1].Immatriculation))
                {
                    this._vehiculesDispo[i].EstSelect = "oui";

                    Console.WriteLine(this._vehiculesDispo[i].Modele);
                    Console.WriteLine(this._vehiculesDispo[i].EstSelect);
                }
            }
        }

        [When(@"les dates de la reservation remplis")]
        public void WhenLesDatesDeLaReservationRemplis(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                if (DateTime.Parse(row[0]) <= DateTime.Parse(row[1]))
                {
                    this._reservation = new Reservation(DateTime.Parse(row[0]), DateTime.Parse(row[1]), this._target.client, this._target.vehicule, this._target.VehiculesDispo);
                }
                else
                {
                    this._reservation = new Reservation(DateTime.Parse(row[0]), DateTime.Parse(row[1]));
                }
            }
        }

        [Then(@"la connexion est refusée")]
        public void ThenTheConnectionIsRefused()
        {
            this._target.UtilisateurConnecte.Should().BeFalse();
        }

        [Then(@"le message d'erreur est ""(.*)""")]
        public void ThenTheErrorMessageIs(string errorMessage)
        {
            this._lastErrorMessage.Should().Be(errorMessage);
        }

        [Then(@"la connexion est etabli")]
        public void ThenTheConnectionIsEstablished()
        {
            this._target.UtilisateurConnecte.Should().BeTrue();
        }

        [Then(@"mon compte est bien crée")]
        public void ThenMonCompteEstBienCree()
        {
            this._target.UtilisateurCree.Should().BeTrue();
        }

        [Then(@"mon compte n'est pas crée")]
        public void ThenMonCompteNEstPasCree()
        {
            this._target.UtilisateurCree.Should().BeFalse();
        }

        [Then(@"le message est ""(.*)""")]
        public void ThenLeMessageEst(string errorMessage)
        {
            this._lastErrorMessage.Should().Be(errorMessage);
        }

        [Then(@"l'estimation de km est de (.*)")]
        public void ThenLEstimationDeKmEstDe(int estiKM)
        {
           this._estimKm = estiKM;
        }

        [Then(@"Message informant le client : (.*)")]
        public void ThenLaReservationEstCree(String message)
        {
            if (this._reservation.DateDebutR> this._reservation.DateFinR)
            {
                this._lastErrorMessage = this._target.VerifierDateValide(this._reservation.DateDebutR, this._reservation.DateFinR);
                this._lastErrorMessage.Should().Be(message);
                Console.WriteLine(this._lastErrorMessage);
            }
            else {
                String m = this._target.CreationReservation(this._reservation.DateDebutR, this._reservation.DateFinR, this._client, this._vehicule, this._vehiculesDispo);
                m.Should().Be(message);
            }
        }

        [Then(@"Un message informe le client : (.*)")]
        public void ThenUnMessageInformeLeClientImpossibleDeContinuerLaReservationVousAvezSelectionneDeuxVehicules(String message)
        {
            
        }


        [Then(@"le prix estimée en euros est de (.*)")]
        public void ThenLePrixEstimeeEstDe(double prixEstime)
        {
            if (this._estimKm == 0)
            {
                Console.WriteLine("Merci de saisir une valeur supérieure à 0");
            }
            else
            {
                this._target.TarifEstimeeReservation(this._vehicule, this._estimKm).Should().Be(prixEstime);
                _tarifEstimeeReservation = prixEstime;
            }
        }

        [Then(@"le client rend la voiture, les kilometres réels parcours sont (.*)")]
        public void ThenLeClientRendLaVoitureLesKilometresReelsParcoursSont(int kmReel)
        {
            if (kmReel != 0)
            {
                this._reelKm = kmReel;
            }
        }

        [Then(@"la facture avec le prix final en euros est de (.*)")]
        public void ThenLaFactureAvecLePrixFinalEnEurosEstDe(double prixReel)
        {
            this._target.TarifFinalReservation(this._vehicule, this._reelKm).Should().Be(prixReel);
            _tarifFinalReservation = prixReel;
        }   

        [Then(@"le client devra \(en euros\) (.*) sur la facture initiale")]
        public void ThenLeClientDevraEnEurosSurLaFactureInitiale(double p)
        {
            double a = _tarifFinalReservation - _tarifEstimeeReservation;
            if (a >= 0)
            {
                a.Should().Be(p);
            }
            else
            {
                a.Should().Be(p);
            }
        }

    }
}
