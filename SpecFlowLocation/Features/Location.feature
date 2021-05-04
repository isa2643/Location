Feature: Location

Background:
	Given suivant les clients existants
	| identifiant | mdp  |
	| isa         | toto |

@mytag
Scenario: Connection client - Pas d'utilisateur reconnu
Given mon identifiant est "bob"
And mon mot de passe est "toto"
When J'essaie de me connecter avec mon compte
Then la connexion est refusée
And le message d'erreur est "Identifiant non reconnu"


Scenario: Connection client - utilisateur reconnu 
Given mon identifiant est "isa"
And mon mot de passe est "toto"
When J'essaie de me connecter avec mon compte
Then la connexion est etabli


Scenario: Connection client - utilisateur reconnu mais pas le mot de passe
Given mon identifiant est "isa"
And mon mot de passe est "toto2"
When J'essaie de me connecter avec mon compte
Then la connexion est refusée
And le message d'erreur est "Mot de passe incorrect"
