Feature: Location

Background:
	Given suivant les clients existants
	| identifiant | mdp    | prenom   | nom    | DateDeNaissance | DateObtentionPermis | NumeroPermis    |
	| isa         | toto   | isabelle | smith  | Jun 15, 1994    | Jun 15, 2015        | 12A456789101112 |
	| denis       | xop    | denis    | crysen | Jun 16, 2004    | Jun 15, 2022        | 12A456789101113 |
	| clark       | super1 | clark    | kent   | Jun 17, 2001    | Jun 15, 2019        | 12A456789101114 |
	| lois        | super2 | lois     | lane   | Jun 18, 1997    | Jun 15, 2018        | 12A456789101115 |
	| perry       | super3 | perry    | white  | Jul 10, 1975    | Jul 15, 1990        | 12A456789101116 |
	And suivant les vehicules existants
	| immatriculation | marque  | modele  | couleur | PrixReservation | PrixKm | ChevauxFiscaux | dateDebut    | dateFin      | Selectionne |
	| aa-123-aa       | renault | clio 4  | gris    | 30.00           | 0.30   | 5              | Jun 5, 2022  | Jun 6, 2022  | non         |
	| bb-456-bb       | audi    | a3      | noir    | 50.00           | 0.70   | 7              | Jun 10, 2022 | Jun 12, 2022 | non         |
	| cc-789-cc       | dacia   | sandero | bleu    | 20.00           | 0.30   | 5              | Jun 17, 2022 | Jun 20, 2022 | non         |
	| dd-101-dd       | porsche | 911     | rouge   | 75.00           | 1      | 10             | Jun 12, 2022 | Jun 16, 2022 | non         |
	| ee-112-ee       | porsche | careara | rouge   | 100.00          | 2      | 15             | Jun 10, 2022 | Jun 11, 2022 | non         |

@mytag
Scenario: (1a) Connection client - Pas d'utilisateur reconnu
Given mon identifiant est "bob"
And mon mot de passe est "toto"
When J'essaie de me connecter avec mon compte
Then la connexion est refusée
And le message d'erreur est "Identifiant non reconnu"


Scenario: (1b) Connection client - utilisateur reconnu 
Given mon identifiant est "isa"
And mon mot de passe est "toto"
When J'essaie de me connecter avec mon compte
Then la connexion est etabli


Scenario: (1c) Connection client - utilisateur reconnu mais pas le mot de passe
Given mon identifiant est "isa"
And mon mot de passe est "toto2"
When J'essaie de me connecter avec mon compte
Then la connexion est refusée
And le message d'erreur est "Mot de passe incorrect"


Scenario: (1d) Inscription client - création possible
Given je renseigne mon prenom "nathalie"
And je renseigne mon nom "portman"
And je renseigne mon identifiant "nathp"
And je renseigne mon mot de passe "plop"
And je renseigne ma date de naissance "Jun 01, 1985"
And je renseigne ma date d'obtention de permis "Jun 30, 2000"
And je renseigne mon numéro de permis "12A456789101122"
When J'essaie de me créer mon compte
Then mon compte est bien crée
And le message est "Le compte a bien été crée"


Scenario: (1e) Inscription client - création impossible client déjà existant
Given je renseigne mon prenom "isabelle"
And je renseigne mon nom "smith"
And je renseigne mon identifiant "isa3"
And je renseigne mon mot de passe "toto"
And je renseigne ma date de naissance "Jun 15, 1994"
And je renseigne ma date d'obtention de permis "Jun 15, 2015"
And je renseigne mon numéro de permis "12A456789101112"
When J'essaie de me créer mon compte
Then mon compte n'est pas crée
And le message est "Le compte est déjà existant"

Scenario: (1f) Inscription client - création impossible numéro de permis pas valide
Given je renseigne mon prenom "tom"
And je renseigne mon nom "cruise"
And je renseigne mon identifiant "tomc"
And je renseigne mon mot de passe ""
And je renseigne ma date de naissance "Jun 15, 1970"
And je renseigne ma date d'obtention de permis "Jan 12, 2085"
And je renseigne mon numéro de permis "12A456789101112"
When J'essaie de me créer mon compte
Then mon compte n'est pas crée
And le message est "Ce numéro de permis est déjà dans notre base"


Scenario: (2a) Connection client - utilisateur reconnu - Age 26 - Reservation d'un véhicule avec la date de début et la date de fin
Given Le client s'identifie
| identifiant | mdp  | prenom   | nom   | DateDeNaissance | DateObtentionPermis | NumeroPermis    |
| isa         | toto | isabelle | smith | Jun 15, 1994    | Jun 15, 2015        | 12A456789101112 |
And les dates de la reservation
| datedebut    | datefin      |
| Jun 15, 2022 | Jun 20, 2022 |
And la liste des vehicules disponibles est presentee au client
| immatriculation | marque  | modele  | couleur | PrixReservation | PrixKm | ChevauxFiscaux | dateDebut    | dateFin      | Selectionne |
| aa-123-aa       | renault | clio 4  | gris    | 30.00           | 0.30   | 5              | Jun 5, 2022  | Jun 6, 2022  | non         |
| bb-456-bb       | audi    | a3      | noir    | 50.00           | 0.70   | 7              | Jun 10, 2022 | Jun 12, 2022 | non         |
| dd-101-dd       | porsche | 911     | rouge   | 75.00           | 1      | 10             | Jun 12, 2022 | Jun 16, 2022 | non         |
| ee-112-ee       | porsche | careara | rouge   | 100.00          | 2      | 15             | Jun 10, 2022 | Jun 11, 2022 | non         |
When le client selectionne un véhicule disponible
| immatriculation | marque  | modele | couleur | PrixReservation | PrixKm | ChevauxFiscaux | dateDebut   | dateFin     | Selectionne |
| aa-123-aa       | renault | clio 4 | gris    | 30.00           | 0.30   | 5              | Jun 5, 2022 | Jun 6, 2022 | oui         |
Then l'estimation de km est de 100
And Message informant le client : La reservation a été crée
And le prix estimée en euros est de 60.0 
And le client rend la voiture, les kilometres réels parcours sont 200
And la facture avec le prix final en euros est de 90.0
And le client devra (en euros) 30.0 sur la facture initiale 

Scenario: (2b) Connection client - utilisateur reconnu - Age 26 - Reservation d'un véhicule avec la date de début et la date de fin impossible
Given Le client s'identifie
| identifiant | mdp    | prenom | nom   | DateDeNaissance | DateObtentionPermis | NumeroPermis    |
| perry       | super3 | perry  | white | Jul 10, 1975    | Jul 15, 1990        | 12A456789101116 |
When les dates de la reservation remplis
| datedebut    | datefin      |
| Jun 20, 2022 | Jun 10, 2022 |
Then Message informant le client : Merci de saisir des dates de reservation correctes

Scenario: (2c) Connection client - utilisateur reconnu - Age 16 - Reservation d'un véhicule avec la date de début et la date de fin
Given Le client s'identifie
| identifiant | mdp | prenom | nom    | DateDeNaissance | DateObtentionPermis | NumeroPermis    |
| denis       | xop | denis  | crysen | Jun 16, 2004    | Jun 15, 2022        | 12A456789101113 |
And les dates de la reservation
| datedebut    | datefin      |
| Jun 25, 2022 | Jun 26, 2022 |
And la liste des vehicules disponibles est presentee au client
| immatriculation | marque  | modele  | couleur | PrixReservation | PrixKm | ChevauxFiscaux | dateDebut    | dateFin      | Selectionne |
| aa-123-aa       | renault | clio 4  | gris    | 30.00           | 0.30   | 5              | Jun 5, 2022  | Jun 6, 2022  | non         |
| bb-456-bb       | audi    | a3      | noir    | 50.00           | 0.70   | 7              | Jun 10, 2022 | Jun 12, 2022 | non         |
| cc-789-cc       | dacia   | sandero | bleu    | 20.00           | 0.30   | 5              | Jun 17, 2022 | Jun 20, 2022 | non         |
| dd-101-dd       | porsche | 911     | rouge   | 75.00           | 1      | 10             | Jun 12, 2022 | Jun 16, 2022 | non         |
| ee-112-ee       | porsche | careara | rouge   | 100.00          | 2      | 15             | Jun 10, 2022 | Jun 11, 2022 | non         |
When le client selectionne un véhicule disponible
| immatriculation | marque | modele  | couleur | PrixReservation | PrixKm | ChevauxFiscaux | dateDebut    | dateFin      | Selectionne |
| cc-789-cc       | dacia  | sandero | bleu    | 20.00           | 0.30   | 5              | Jun 17, 2022 | Jun 20, 2022 | oui         |
Then l'estimation de km est de 120
And Message informant le client : La reservation n'a pas été crée : vous devez avoir plus de 18 ans et un numéro de permis

Scenario: (2d) Connection client - utilisateur reconnu - Age 20 - Reservation d'un véhicule avec la date de début et la date de fin
Given Le client s'identifie
| identifiant | mdp   | prenom | nom  | DateDeNaissance | DateObtentionPermis | NumeroPermis    |
| clark       | super | clark  | kent | Jun 17, 2001    | Jun 15, 2019        | 12A456789101114 |
And les dates de la reservation
| datedebut    | datefin      |
| Jun 21, 2022 | Jun 22, 2022 |
And la liste des vehicules disponibles est presentee au client
| immatriculation | marque  | modele  | couleur | PrixReservation | PrixKm | ChevauxFiscaux | dateDebut    | dateFin      | Selectionne |
| aa-123-aa       | renault | clio 4  | gris    | 30.00           | 0.30   | 5              | Jun 5, 2022  | Jun 6, 2022  | non         |
| bb-456-bb       | audi    | a3      | noir    | 50.00           | 0.70   | 7              | Jun 10, 2022 | Jun 12, 2022 | non         |
| cc-789-cc       | dacia   | sandero | bleu    | 20.00           | 0.30   | 5              | Jun 17, 2022 | Jun 20, 2022 | non         |
| dd-101-dd       | porsche | 911     | rouge   | 75.00           | 1      | 10             | Jun 12, 2022 | Jun 16, 2022 | non         |
| ee-112-ee       | porsche | careara | rouge   | 100.00          | 2      | 15             | Jun 10, 2022 | Jun 11, 2022 | non         |
When le client selectionne un véhicule disponible
| immatriculation | marque  | modele  | couleur | PrixReservation | PrixKm | ChevauxFiscaux | dateDebut    | dateFin      | Selectionne |
| dd-101-dd       | porsche | 911     | rouge   | 75.00           | 1      | 10             | Jun 12, 2022 | Jun 16, 2022 | oui         |
Then l'estimation de km est de 150
And Message informant le client : La reservation n'a pas pu être crée : vous devez avoir plus de 21 ans pour conduire le véhicule sélectionné. Il a une puissance fiscale de plus de 8 chevaux

Scenario: (2e) Connection client - utilisateur reconnu - Age 24 - Reservation d'un véhicule avec la date de début et la date de fin
Given Le client s'identifie
| identifiant | mdp   | prenom | nom  | DateDeNaissance | DateObtentionPermis | NumeroPermis    |
| clark       | super | clark  | kent | Jun 17, 2001    | Jun 15, 2019        | 12A456789101114 |
And les dates de la reservation
| datedebut    | datefin      |
| Jun 23, 2022 | Jun 24, 2022 |
And la liste des vehicules disponibles est presentee au client
| immatriculation | marque  | modele  | couleur | PrixReservation | PrixKm | ChevauxFiscaux | dateDebut    | dateFin      | Selectionne |
| aa-123-aa       | renault | clio 4  | gris    | 30.00           | 0.30   | 5              | Jun 5, 2022  | Jun 6, 2022  | non         |
| bb-456-bb       | audi    | a3      | noir    | 50.00           | 0.70   | 7              | Jun 10, 2022 | Jun 12, 2022 | non         |
| cc-789-cc       | dacia   | sandero | bleu    | 20.00           | 0.30   | 5              | Jun 17, 2022 | Jun 20, 2022 | non         |
| dd-101-dd       | porsche | 911     | rouge   | 75.00           | 1      | 10             | Jun 12, 2022 | Jun 16, 2022 | non         |
| ee-112-ee       | porsche | careara | rouge   | 100.00          | 2      | 15             | Jun 10, 2022 | Jun 11, 2022 | non         |
When le client selectionne un véhicule disponible
| immatriculation | marque  | modele  | couleur | PrixReservation | PrixKm | ChevauxFiscaux | dateDebut    | dateFin      | Selectionne |
| ee-112-ee       | porsche | careara | rouge   | 100.00          | 2      | 15             | Jun 10, 2022 | Jun 11, 2022 | oui         |
Then l'estimation de km est de 150
And Message informant le client : La reservation n'a pas pu être crée : vous devez avoir plus de 21 ans pour conduire le véhicule sélectionné. Il a une puissance fiscale de plus de 8 chevaux

Scenario: (2f) Connection client - utilisateur reconnu - Age 26 - Reservation de plusieurs véhicules avec la date de début et la date de fin
Given Le client s'identifie
| identifiant | mdp    | prenom | nom   | DateDeNaissance | DateObtentionPermis | NumeroPermis    |
| perry       | super3 | perry  | white | Jul 10, 1975    | Jul 15, 1990        | 12A456789101116 |
And les dates de la reservation
| datedebut    | datefin      |
| Jul 15 , 2022 | Jul 16, 2022 |
And la liste des vehicules disponibles est presentee au client
| immatriculation | marque  | modele  | couleur | PrixReservation | PrixKm | ChevauxFiscaux | dateDebut    | dateFin      | Selectionne |
| aa-123-aa       | renault | clio 4  | gris    | 30.00           | 0.30   | 5              | Jun 5, 2022  | Jun 6, 2022  | non         |
| bb-456-bb       | audi    | a3      | noir    | 50.00           | 0.70   | 7              | Jun 10, 2022 | Jun 12, 2022 | non         |
| cc-789-cc       | dacia   | sandero | bleu    | 20.00           | 0.30   | 5              | Jun 17, 2022 | Jun 20, 2022 | non         |
| dd-101-dd       | porsche | 911     | rouge   | 75.00           | 1      | 10             | Jun 12, 2022 | Jun 16, 2022 | non         |
| ee-112-ee       | porsche | careara | rouge   | 100.00          | 2      | 15             | Jun 10, 2022 | Jun 11, 2022 | non         |
When le client selectionne des véhicules disponibles
| immatriculation | marque  | modele  | couleur | PrixReservation | PrixKm | ChevauxFiscaux | dateDebut    | dateFin      | Selectionne |
| aa-123-aa       | renault | clio 4  | gris    | 30.00           | 0.30   | 5              | Jun 5, 2022  | Jun 6, 2022  | oui         |
| cc-789-cc       | dacia   | sandero | bleu    | 20.00           | 0.30   | 5              | Jun 17, 2022 | Jun 20, 2022 | oui         |
Then Un message informe le client : Impossible de continuer la réservation, vous avez sélectionné deux véhicules
